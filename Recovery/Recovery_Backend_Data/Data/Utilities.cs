using Microsoft.Extensions.Configuration;
using Recovery_Models.Models;
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

        private IConfiguration _config;

        public Utilities(IConfiguration config)
        {
            _config = config;
        }
        public string HashPassword(string password)
        {
            string input = password + _config["Secret"];

            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static string RandomString(RegisterModel user)
        {
            var concat = user + Convert.ToString(DateTime.Now);

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(concat));

            var sBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sBuilder.Append(result[i].ToString("x2"));
            }

            var formattedKey = FormatLicenseKey(sBuilder.ToString());

            return formattedKey;
        }
        public static string KeyGeneratorPT(PTModel user)
        {
            var concat = user + Convert.ToString(DateTime.Now);

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(concat));

            var sBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sBuilder.Append(result[i].ToString("x2"));
            }

            var formattedKey = FormatLicenseKey(sBuilder.ToString());

            return formattedKey;
        }
        private static string FormatLicenseKey(string productIdentifier)
        {
            // productIdentifier = productIdentifier.Substring(0, 28).ToUpper();
            productIdentifier = productIdentifier.ToUpper();
            char[] serialArray = productIdentifier.ToCharArray();
            StringBuilder licenseKey = new StringBuilder();

            int j = 0;
            for (int i = 0; i < 28; i++)
            {
                for (j = i; j < 4 + i; j++)
                {
                    licenseKey.Append(serialArray[j]);
                }
                if (j == 28)
                {
                    break;
                }
                else
                {
                    i = (j) - 1;
                    licenseKey.Append("-");
                }
            }
            return licenseKey.ToString();
        }
    }
}
