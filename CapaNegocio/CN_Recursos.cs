using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Recursos
    {
        public static string ConvertirSha256(string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                throw new ArgumentNullException(nameof(texto), "El texto no puede ser nulo o vacío.");
            }

            StringBuilder sb = new StringBuilder();

            using (SHA256 has = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = has.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }
            return sb.ToString();
        }
    }
}
