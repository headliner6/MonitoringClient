using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.DataStructures
{

    //TODO: mit SALT ergänzen, MD5Hashes können schnell entcripted werden
    public class CreatePasswordHash
    {
        public string GetMD5Hash(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Provider = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes = Encoding.Unicode.GetBytes(input);
            bytes = md5Provider.ComputeHash(bytes);
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
