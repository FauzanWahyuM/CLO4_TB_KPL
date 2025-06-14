using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tubes_Kelompok_BisaYukk_Final.Modules
{
    internal class namespace_Security
    {
        public static class UsernameSecurity
        {
            private static readonly Regex _format =
                new("^[A-Za-z .]{3,40}$");          // huruf, spasi, titik

            public static bool IsValid(string name) => _format.IsMatch(name);

            public static string Pseudonym(string name)
            {
                using var sha = SHA256.Create();
                byte[] hash = sha.ComputeHash(
                    Encoding.UTF8.GetBytes(name.ToUpperInvariant()));
                return Convert.ToHexString(hash)[..16]; // 16 karakter cukup
            }
        }
    }
}
