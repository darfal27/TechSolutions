// CAPA NEGOCIO – quién inició sesión (lo consultan los permisos de cada operación)
using Entidades;

namespace Negocio
{
    public static class Sesion
    {
        public static Usuario Actual { get; private set; }
        public static void Iniciar(Usuario u) { Actual = u; }
        public static void Cerrar() { Actual = null; }
    }
}
