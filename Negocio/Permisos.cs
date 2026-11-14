// CAPA NEGOCIO – niveles de acceso. UNA sola tabla decide qué rol entra a qué módulo.
// La usan el menú (para ocultar opciones) y las clases BLL (para rechazar la operación aunque alguien
// llegue a abrir la pantalla): la seguridad no depende solo de ocultar botones.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio
{
    public enum Modulo { Clientes, Productos, Ventas, Usuarios, Consultas }

    public static class Permisos
    {
        public const string Administrador = "Administrador";
        public const string Vendedor = "Vendedor";
        public const string Almacenero = "Almacenero";
        public static readonly string[] Roles = { Administrador, Vendedor, Almacenero };

        //                    Clientes  Productos  Ventas  Usuarios  Consultas
        // Administrador        sí        sí         sí       sí        sí
        // Vendedor             sí        no         sí       no        sí
        // Almacenero           no        sí         no       no        sí
        // (dentro de Consultas, cada consulta exige además el módulo de los datos que muestra)
        private static readonly Dictionary<string, Modulo[]> matriz = new Dictionary<string, Modulo[]>
        {
            { Administrador, new[] { Modulo.Clientes, Modulo.Productos, Modulo.Ventas, Modulo.Usuarios, Modulo.Consultas } },
            { Vendedor,      new[] { Modulo.Clientes, Modulo.Ventas, Modulo.Consultas } },
            { Almacenero,    new[] { Modulo.Productos, Modulo.Consultas } }
        };

        public static bool Puede(string rol, Modulo modulo)
        {
            Modulo[] permitidos;
            return rol != null && matriz.TryGetValue(rol, out permitidos) && permitidos.Contains(modulo);
        }

        public static bool PuedeActual(Modulo modulo)
        {
            return Sesion.Actual != null && Puede(Sesion.Actual.Rol, modulo);
        }

        public static bool PuedeAlguno(params Modulo[] modulos)
        {
            return modulos.Any(PuedeActual);
        }

        // Lanza UnauthorizedAccessException si el usuario actual no tiene ninguno de los módulos indicados.
        public static void Exigir(params Modulo[] modulos)
        {
            if (!PuedeAlguno(modulos))
                throw new UnauthorizedAccessException("Su rol no tiene permiso para esta operación.");
        }
    }
}
