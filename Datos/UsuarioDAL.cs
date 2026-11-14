// CAPA DATOS – tabla Usuario. Todas las consultas son parametrizadas (evitan inyección SQL).
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class UsuarioDAL
    {
        private const string Columnas = "IdUsuario, Login, ClaveHash, Sal, Rol, IntentosFallidos, Bloqueado";

        public Usuario BuscarPorLogin(string login)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand("SELECT " + Columnas + " FROM Usuario WHERE Login = @login", cn))
            {
                cmd.Parameters.Add("@login", SqlDbType.VarChar, 30).Value = login;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                    return dr.Read() ? Leer(dr) : null;
            }
        }

        public List<Usuario> Listar()
        {
            var lista = new List<Usuario>();
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand("SELECT " + Columnas + " FROM Usuario ORDER BY Login", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                    while (dr.Read()) lista.Add(Leer(dr));
            }
            return lista;
        }

        public void Insertar(Usuario u)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Usuario (Login, ClaveHash, Sal, Rol) VALUES (@login, @hash, @sal, @rol)", cn))
            {
                cmd.Parameters.Add("@login", SqlDbType.VarChar, 30).Value = u.Login;
                cmd.Parameters.Add("@hash", SqlDbType.VarChar, 100).Value = u.ClaveHash;
                cmd.Parameters.Add("@sal", SqlDbType.VarChar, 50).Value = u.Sal;
                cmd.Parameters.Add("@rol", SqlDbType.VarChar, 20).Value = u.Rol;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int Contar()
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Usuario", cn))
            {
                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        // Suma un intento fallido y, al llegar a 3, bloquea la cuenta. Devuelve true si quedó bloqueada.
        // Se hace en UN solo UPDATE para que dos intentos simultáneos no se pisen.
        public bool SumarIntentoFallido(int idUsuario)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Usuario SET IntentosFallidos = IntentosFallidos + 1, " +
                "Bloqueado = CASE WHEN IntentosFallidos + 1 >= 3 THEN 1 ELSE Bloqueado END " +
                "OUTPUT inserted.Bloqueado WHERE IdUsuario = @id", cn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = idUsuario;
                cn.Open();
                object r = cmd.ExecuteScalar();
                return r != null && (bool)r;
            }
        }

        public void ReiniciarIntentos(int idUsuario)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand("UPDATE Usuario SET IntentosFallidos = 0 WHERE IdUsuario = @id", cn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = idUsuario;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Desbloquear(int idUsuario)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Usuario SET Bloqueado = 0, IntentosFallidos = 0 WHERE IdUsuario = @id", cn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = idUsuario;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Columna (tipo SQL) -> propiedad (tipo C#):  INT -> int · VARCHAR -> string · BIT -> bool
        private Usuario Leer(SqlDataReader dr)
        {
            return new Usuario
            {
                Id = dr.GetInt32(0),
                Login = dr.GetString(1),
                ClaveHash = dr.GetString(2),
                Sal = dr.GetString(3),
                Rol = dr.GetString(4),
                IntentosFallidos = dr.GetInt32(5),
                Bloqueado = dr.GetBoolean(6)
            };
        }
    }
}
