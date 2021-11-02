using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_Backend_Data.Data
{
    public class Utilities
    {
        public static string HashPassword(string password)
        {
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();

            byte[] password_bytes = Encoding.ASCII.GetBytes(password);
            byte[] encrypted_bytes = SHA1.ComputeHash(password_bytes);
            return Convert.ToBase64String(encrypted_bytes);
        }

        public static string RandomString()
        {
            Random random = new Random();
            string b = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int length = 20;
            string rnd = "";
            for (int i = 0; i < length; i++)
            {
                int a = random.Next(60);
                rnd = rnd + b.ElementAt(a);
            }
            return rnd;
        }
    }
}
