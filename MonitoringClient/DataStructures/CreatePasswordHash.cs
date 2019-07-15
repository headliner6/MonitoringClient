using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MonitoringClient.DataStructures
{

    //TODO: mit SALT ergänzen, MD5Hashes können schnell entcripted werden
    public class CreatePasswordHash
    {
        public string GetSaltedHash(string input)
        {
            byte[] salt;
            var rngProvider = new RNGCryptoServiceProvider();
            var sha512Provider = new SHA512CryptoServiceProvider();
            rngProvider.GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(input,salt);

            var bytes = sha512Provider.ComputeHash(pbkdf2.GetBytes(20));
            StringBuilder s = new StringBuilder();
            foreach (byte b in bytes)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }
    }
}
