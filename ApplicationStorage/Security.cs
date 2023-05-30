using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ApplicationStorage
{
    class Security
    {
        public static string hasPassword(string pas)
        {
            MD5 mD5 = MD5.Create();

            byte[] b = Encoding.ASCII.GetBytes(pas);
            byte[] hash = mD5.ComputeHash(b);

            StringBuilder sb = new StringBuilder();
            foreach(var a in hash)
            {
                sb.Append(a.ToString("X2"));
            }

            return Convert.ToString(sb);
        }
    }
}
