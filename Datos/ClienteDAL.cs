using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class ClienteDAL
    {
        // REPORTE: aquí .NET se conecta con el PROCEDIMIENTO ALMACENADO dbo.usp_ReporteClientes.
        public List<Cliente> ListarPorNombre(string nombre)
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection cn = Conexion.Instancia.Crear())                            // 1) conexión (App.config)
            using (SqlCommand cmd = new SqlCommand("dbo.usp_ReporteClientes", cn))          // 2) NOMBRE del procedimiento
            {
                cmd.CommandType = CommandType.StoredProcedure;                               // 3) «es un procedimiento»
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = nombre;        // 4) mismo nombre y tipo que en SQL

                cn.Open();                                                                    // 5) abrir la conexión
                using (SqlDataReader dr = cmd.ExecuteReader())                               // 6) ejecutar el procedimiento
                {
                    while (dr.Read())                                                         // 7) una vuelta por cada fila
                    {
                        lista.Add(new Cliente
                        {
                            Documento = dr["Documento"].ToString(),                           //    columna → propiedad
                            Nombre = dr["Nombre"].ToString(),
                            Email = dr["Email"].ToString(),
                            Telefono = dr["Telefono"].ToString()
                        });
                    }
                }
            }                                                                                 // 8) using cierra la conexión
            return lista;
        }

        public void Insertar(Cliente c)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Cliente (Documento, Nombre, Email, Telefono) VALUES (@doc, @nom, @mail, @tel)", cn))
            {
                cmd.Parameters.Add("@doc", SqlDbType.VarChar, 15).Value = c.Documento;
                cmd.Parameters.Add("@nom", SqlDbType.VarChar, 100).Value = c.Nombre;
                cmd.Parameters.Add("@mail", SqlDbType.VarChar, 100).Value = (object)c.Email ?? DBNull.Value;
                cmd.Parameters.Add("@tel", SqlDbType.VarChar, 15).Value = (object)c.Telefono ?? DBNull.Value;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Cliente> Listar()
        {
            var lista = new List<Cliente>();
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT IdCliente, Documento, Nombre, Email, Telefono FROM Cliente ORDER BY Nombre", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                    while (dr.Read())
                        lista.Add(new Cliente
                        {
                            Id = dr.GetInt32(0),
                            Documento = dr.GetString(1),
                            Nombre = dr.GetString(2),
                            Email = dr.IsDBNull(3) ? "" : dr.GetString(3),
                            Telefono = dr.IsDBNull(4) ? "" : dr.GetString(4)
                        });
            }
            return lista;
        }
    }
}
