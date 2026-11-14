// CAPA NEGOCIO – reglas de la venta. La transacción en sí vive en Datos\VentaDAL.
using System;
using System.Linq;
using Datos;
using Entidades;

namespace Negocio
{
    public class VentaBLL
    {
        private readonly VentaDAL dal = new VentaDAL();

        // usarProcedimiento = false: la transacción la maneja .NET (SqlTransaction, VentaDAL.Registrar)
        // usarProcedimiento = true : la transacción la maneja SQL Server (dbo.usp_RegistrarVenta)
        // Devuelve la venta ya grabada (con su Id, los precios y el total) para poder imprimir el comprobante.
        public Venta Registrar(int idCliente, System.Collections.Generic.IEnumerable<DetalleVenta> lineas, bool usarProcedimiento)
        {
            Permisos.Exigir(Modulo.Ventas);
            if (idCliente <= 0) throw new ArgumentException("Seleccione un cliente.");

            // Si el mismo producto se agregó dos veces, se junta en una sola línea (LINQ).
            var detalles = lineas
                .GroupBy(l => l.IdProducto)
                .Select(g => new DetalleVenta
                {
                    IdProducto = g.Key,
                    Producto = g.First().Producto,
                    Cantidad = g.Sum(x => x.Cantidad)
                })
                .ToList();

            if (detalles.Count == 0) throw new ArgumentException("Agregue al menos un producto.");
            if (detalles.Any(d => d.Cantidad <= 0)) throw new ArgumentException("Las cantidades deben ser mayores que cero.");

            var venta = new Venta
            {
                IdCliente = idCliente,
                IdUsuario = Sesion.Actual.Id,       // el vendedor sale de la sesión, no de la pantalla
                Detalles = detalles
            };
            if (usarProcedimiento) dal.RegistrarConProcedimiento(venta);   // transacción en SQL Server
            else dal.Registrar(venta);                                     // transacción en .NET
            return venta;
        }

        // REPORTE de ventas por rango de fechas (procedimiento almacenado con parámetros)
        public System.Collections.Generic.List<VentaDetalleInfo> Reporte(DateTime desde, DateTime hasta)
        {
            Permisos.Exigir(Modulo.Ventas);
            if (desde.Date > hasta.Date) throw new ArgumentException("La fecha inicial no puede ser mayor que la final.");
            return dal.ListarReporte(desde, hasta);
        }
    }
}
