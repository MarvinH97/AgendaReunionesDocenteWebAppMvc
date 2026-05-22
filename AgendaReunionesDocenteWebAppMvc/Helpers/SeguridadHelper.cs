using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AgendaReunionesDocenteWebAppMvc.Helpers
{
    public static class SeguridadHelper
    {
        public static (byte[] hash, byte[] salt) CrearPasswordHash(string password)
        {
            using (var hmac = new HMACSHA256())
            {
                var salt = hmac.Key; // clave aleatoria generada
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return (hash, salt);
            }
        }

        public static bool ValidarPassword(string password, byte[] hashGuardado, byte[] saltGuardado)
        {
            using (var hmac = new HMACSHA256(saltGuardado))
            {
                var hashCalculado = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return hashCalculado.SequenceEqual(hashGuardado);
            }
        }
    }
}