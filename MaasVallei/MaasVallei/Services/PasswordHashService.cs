using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MaasVallei.Services
{
    public class PasswordHashService
    {
        // Hash password with SHA1 and return hashed string
        public string Hash(string password)
        {
            var data = Encoding.ASCII.GetBytes(password);

            var sha1 = new SHA1CryptoServiceProvider();
            var sha1data = sha1.ComputeHash(data);
            var encoder = new ASCIIEncoding();
            return encoder.GetString(sha1data);
        }
    }
}
