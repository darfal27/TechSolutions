// CAPA NEGOCIO – inicio de sesión (3 intentos) y registro de usuarios
using System;
using System.Collections.Generic;
using Datos;
using Entidades;
using Utilidades;

namespace Negocio
{
    public class UsuarioBLL
    {
        public const int MaxIntentos = 3;
        private readonly UsuarioDAL dal = new UsuarioDAL();

        public Usuario IniciarSesion(string login, string clave)
        {
            Usuario u = dal.BuscarPorLogin(login);     

            if (u == null)                                // usuario inexistente: mensaje genérico
                throw new UnauthorizedAccessException("Usuario o contraseña incorrectos.");

            if (u.Bloqueado)                            
                throw new UnauthorizedAccessException("Cuenta bloqueada por " + MaxIntentos +
                    " intentos fallidos. Pida al Administrador que la desbloquee.");

            if (!Seguridad.Verificar(clave, u.Sal, u.ClaveHash))    // 2) hash de lo escrito con esa sal vs. el guardado
            {
                bool quedoBloqueado = dal.SumarIntentoFallido(u.Id);   // 3) cuenta el intento; al 3.º bloquea
                throw new UnauthorizedAccessException(quedoBloqueado
                    ? "Contraseña incorrecta. La cuenta quedó BLOQUEADA tras " + MaxIntentos + " intentos fallidos."
                    : "Usuario o contraseña incorrectos.");
            }

            dal.ReiniciarIntentos(u.Id);                  // acertó: los fallos seguidos vuelven a 0
            u.IntentosFallidos = 0;
            Sesion.Iniciar(u);
            return u;
        }

        public void CerrarSesion() { Sesion.Cerrar(); }

        public bool HayUsuarios() { return dal.Contar() > 0; }

        public void Registrar(string login, string clave, string rol)
        {
            bool primero = !HayUsuarios();
            if (primero) rol = Permisos.Administrador;    // el primer usuario siempre es Administrador
            else Permisos.Exigir(Modulo.Usuarios);        // después, solo un Administrador crea usuarios

            if (string.IsNullOrWhiteSpace(login) || login.Length > 30)
                throw new ArgumentException("El usuario es obligatorio y tiene como máximo 30 caracteres.");
            if (clave == null || clave.Length < 8)
                throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.");
            if (Array.IndexOf(Permisos.Roles, rol) < 0)
                throw new ArgumentException("Rol no válido.");
            if (dal.BuscarPorLogin(login) != null)
                throw new ArgumentException("Ese usuario ya existe.");

            string sal = Seguridad.NuevaSal();
            string hash = Seguridad.Hash(clave, sal);
            dal.Insertar(new Usuario { Login = login, Sal = sal, ClaveHash = hash, Rol = rol });   // NO se guarda la clave
        }

        public List<Usuario> Listar()
        {
            Permisos.Exigir(Modulo.Usuarios);
            return dal.Listar();
        }

        public void Desbloquear(int idUsuario)
        {
            Permisos.Exigir(Modulo.Usuarios);
            dal.Desbloquear(idUsuario);
        }
    }
}
