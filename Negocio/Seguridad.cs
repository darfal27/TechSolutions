// CAPA NEGOCIO – contraseñas con hash y sal (PBKDF2 + SHA-256, 600 000 iteraciones). Requiere .NET Framework 4.7.2+.
using System;
using System.Security.Cryptography;

namespace Negocio
{
    public static class Seguridad
    {
        public static string NuevaSal()
        {
            byte[] sal = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(sal);
            return Convert.ToBase64String(sal);
        }

        public static string Hash(string clave, string salBase64)
        {
            byte[] sal = Convert.FromBase64String(salBase64);
            using (var pbkdf2 = new Rfc2898DeriveBytes(clave, sal, 600000, HashAlgorithmName.SHA256))
                return Convert.ToBase64String(pbkdf2.GetBytes(32));
        }

        public static bool Verificar(string clave, string salBase64, string hashGuardado)
        {
            return Hash(clave, salBase64) == hashGuardado;
        }
    }
}
