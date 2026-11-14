using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class ProductoDAL
    {
        public void Insertar(Producto p)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Producto (Codigo, Nombre, Precio, Stock) VALUES (@cod, @nom, @precio, @stock)", cn))
            {
                cmd.Parameters.Add("@cod", SqlDbType.VarChar, 20).Value = p.Codigo;
                cmd.Parameters.Add("@nom", SqlDbType.VarChar, 100).Value = p.Nombre;
                SqlParameter precio = cmd.Parameters.Add("@precio", SqlDbType.Decimal);
                precio.Precision = 10; precio.Scale = 2; precio.Value = p.Precio;
                cmd.Parameters.Add("@stock", SqlDbType.Int).Value = p.Stock;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Producto> Listar()
        {
            var lista = new List<Producto>();
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT IdProducto, Codigo, Nombre, Precio, Stock FROM Producto ORDER BY Nombre", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                    while (dr.Read())
                        lista.Add(new Producto
                        {
                            Id = dr.GetInt32(0),
                            Codigo = dr.GetString(1),
                            Nombre = dr.GetString(2),
                            Precio = dr.GetDecimal(3),
                            Stock = dr.GetInt32(4)
                        });
            }
            return lista;
        }

        // Entrada de mercadería: suma stock.
        public void SumarStock(int idProducto, int cantidad)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Producto SET Stock = Stock + @cant WHERE IdProducto = @id", cn))
            {
                cmd.Parameters.Add("@cant", SqlDbType.Int).Value = cantidad;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = idProducto;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
