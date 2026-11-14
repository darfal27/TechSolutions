// Proyecto: Datos (Biblioteca de clases) · Referencias: System.Configuration
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Datos
{
    // PATRÓN SINGLETON: existe UNA sola instancia que guarda la cadena de conexión.
    public sealed class Conexion
    {
        // Lazy<T> crea la instancia la primera vez que se usa y es seguro con varios hilos.
        private static readonly Lazy<Conexion> instancia = new Lazy<Conexion>(() => new Conexion());
        private readonly string cadena;

        private Conexion()                              // constructor privado: nadie hace "new Conexion()"
        {
            cadena = ConfigurationManager.ConnectionStrings["TechSolutions"].ConnectionString;
        }

        public static Conexion Instancia { get { return instancia.Value; } }

        // Cada operación crea y cierra SU PROPIA conexión (no se comparte abierta).
        public SqlConnection Crear()
        {
            return new SqlConnection(cadena);
        }
    }
}
