// CAPA DATOS – registro de una venta con TRANSACCIÓN (ADO.NET).
// Regla: o se graban la cabecera, TODAS las líneas y el descuento de stock, o no se graba NADA.
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Entidades;

namespace Datos
{
    public class VentaDAL
    {
        // LINQ: Datos solo trae TODAS las líneas vendidas (ya unidas con cliente, vendedor y producto).
        // Filtrar, agrupar y ordenar lo hace LINQ en Negocio\ConsultaBLL.
        public System.Collections.Generic.List<VentaDetalleInfo> ListarDetalle()
        {
            var lista = new System.Collections.Generic.List<VentaDetalleInfo>();
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT v.IdVenta, v.Fecha, c.Documento, c.Nombre, u.Login, p.Nombre, d.Cantidad, d.PrecioUnitario " +
                "FROM DetalleVenta d " +
                "JOIN Venta v ON v.IdVenta = d.IdVenta " +
                "JOIN Cliente c ON c.IdCliente = v.IdCliente " +
                "JOIN Usuario u ON u.IdUsuario = v.IdUsuario " +
                "JOIN Producto p ON p.IdProducto = d.IdProducto", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                    while (dr.Read())
                        lista.Add(new VentaDetalleInfo
                        {
                            IdVenta = dr.GetInt32(0),
                            Fecha = dr.GetDateTime(1),
                            Documento = dr.GetString(2),
                            Cliente = dr.GetString(3),
                            Vendedor = dr.GetString(4),
                            Producto = dr.GetString(5),
                            Cantidad = dr.GetInt32(6),
                            PrecioUnitario = dr.GetDecimal(7)
                        });
            }
            return lista;
        }

        // TRANSACCIÓN EN SQL SERVER: la misma venta, pero la transacción vive DENTRO del procedimiento
        // dbo.usp_RegistrarVenta (BEGIN TRANSACTION / COMMIT / ROLLBACK). Las líneas viajan en un parámetro de tabla.
        public int RegistrarConProcedimiento(Venta v)
        {
            var tabla = new DataTable();
            tabla.Columns.Add("IdProducto", typeof(int));
            tabla.Columns.Add("Cantidad", typeof(int));
            foreach (DetalleVenta d in v.Detalles) tabla.Rows.Add(d.IdProducto, d.Cantidad);

            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand("dbo.usp_RegistrarVenta", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = v.IdCliente;
                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = v.IdUsuario;
                SqlParameter detalle = cmd.Parameters.Add("@Detalle", SqlDbType.Structured);
                detalle.TypeName = "dbo.TipoDetalleVenta";           // el tipo de tabla creado en SQL
                detalle.Value = tabla;
                SqlParameter salida = cmd.Parameters.Add("@IdVenta", SqlDbType.Int);
                salida.Direction = ParameterDirection.Output;

                cn.Open();
                try { cmd.ExecuteNonQuery(); }
                catch (SqlException ex) when (ex.Number == 50001)     // THROW 50001 del procedimiento
                {
                    throw new StockInsuficienteException(ex.Message);
                }
                v.Id = (int)salida.Value;

                // Lee los precios que usó el procedimiento para poder imprimir el comprobante
                using (SqlCommand q = new SqlCommand(
                    "SELECT IdProducto, PrecioUnitario FROM DetalleVenta WHERE IdVenta = @id", cn))
                {
                    q.Parameters.Add("@id", SqlDbType.Int).Value = v.Id;
                    using (SqlDataReader dr = q.ExecuteReader())
                        while (dr.Read())
                        {
                            int idProducto = dr.GetInt32(0);
                            v.Detalles.First(x => x.IdProducto == idProducto).PrecioUnitario = dr.GetDecimal(1);
                        }
                }
                v.Total = v.Detalles.Sum(x => x.Subtotal);
                return v.Id;
            }
        }

        // REPORTE: procedimiento dbo.usp_ReporteVentas con parámetros @Desde y @Hasta
        public System.Collections.Generic.List<VentaDetalleInfo> ListarReporte(DateTime desde, DateTime hasta)
        {
            var lista = new System.Collections.Generic.List<VentaDetalleInfo>();
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand("dbo.usp_ReporteVentas", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Desde", SqlDbType.Date).Value = desde.Date;
                cmd.Parameters.Add("@Hasta", SqlDbType.Date).Value = hasta.Date;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                    while (dr.Read())
                        lista.Add(new VentaDetalleInfo
                        {
                            IdVenta = (int)dr["IdVenta"],
                            Fecha = (DateTime)dr["Fecha"],
                            Cliente = dr["Cliente"].ToString(),
                            Vendedor = dr["Vendedor"].ToString(),
                            Producto = dr["Producto"].ToString(),
                            Cantidad = (int)dr["Cantidad"],
                            PrecioUnitario = (decimal)dr["PrecioUnitario"]
                        });
            }
            return lista;
        }

        // Devuelve el IdVenta. Si algo falla (stock insuficiente, error de BD...) hace Rollback y relanza el error.
        public int Registrar(Venta v)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            {
                cn.Open();
                SqlTransaction tx = cn.BeginTransaction();      // (1) INICIA la transacción
                try
                {
                    // (2) Cabecera. El total se calcula al final con los precios que trae la BD.
                    int idVenta;
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Venta (IdCliente, IdUsuario, Total) OUTPUT inserted.IdVenta " +
                        "VALUES (@cli, @usu, 0)", cn, tx))        // cada comando debe recibir la MISMA tx
                    {
                        cmd.Parameters.Add("@cli", SqlDbType.Int).Value = v.IdCliente;
                        cmd.Parameters.Add("@usu", SqlDbType.Int).Value = v.IdUsuario;
                        idVenta = (int)cmd.ExecuteScalar();
                    }

                    decimal total = 0;
                    foreach (DetalleVenta d in v.Detalles)
                    {
                        // (3) Descuenta el stock SOLO si alcanza, y devuelve el precio vigente (OUTPUT).
                        //     Si no alcanza, el UPDATE no afecta ninguna fila y ExecuteScalar devuelve null.
                        object precio;
                        using (SqlCommand cmd = new SqlCommand(
                            "UPDATE Producto SET Stock = Stock - @cant OUTPUT inserted.Precio " +
                            "WHERE IdProducto = @prod AND Stock >= @cant", cn, tx))
                        {
                            cmd.Parameters.Add("@cant", SqlDbType.Int).Value = d.Cantidad;
                            cmd.Parameters.Add("@prod", SqlDbType.Int).Value = d.IdProducto;
                            precio = cmd.ExecuteScalar();
                        }
                        if (precio == null)
                            throw new StockInsuficienteException(
                                "Stock insuficiente para \"" + d.Producto + "\". No se registró la venta.");

                        d.PrecioUnitario = (decimal)precio;

                        // (4) Línea de detalle
                        using (SqlCommand cmd = new SqlCommand(
                            "INSERT INTO DetalleVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario) " +
                            "VALUES (@venta, @prod, @cant, @precio)", cn, tx))
                        {
                            cmd.Parameters.Add("@venta", SqlDbType.Int).Value = idVenta;
                            cmd.Parameters.Add("@prod", SqlDbType.Int).Value = d.IdProducto;
                            cmd.Parameters.Add("@cant", SqlDbType.Int).Value = d.Cantidad;
                            SqlParameter p = cmd.Parameters.Add("@precio", SqlDbType.Decimal);
                            p.Precision = 10; p.Scale = 2; p.Value = d.PrecioUnitario;
                            cmd.ExecuteNonQuery();
                        }
                        total += d.Subtotal;
                    }

                    // (5) Total de la venta
                    using (SqlCommand cmd = new SqlCommand("UPDATE Venta SET Total = @total WHERE IdVenta = @id", cn, tx))
                    {
                        SqlParameter p = cmd.Parameters.Add("@total", SqlDbType.Decimal);
                        p.Precision = 12; p.Scale = 2; p.Value = total;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = idVenta;
                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();                                  // (6) TODO salió bien: se confirma
                    v.Id = idVenta;
                    v.Total = total;
                    return idVenta;
                }
                catch
                {
                    try { tx.Rollback(); }                        // (7) ALGO falló: se deshace TODO
                    catch (InvalidOperationException) { }         // la transacción ya había terminado
                    throw;                                        // el error sube a Negocio / Presentación
                }
            }
        }
    }
}
