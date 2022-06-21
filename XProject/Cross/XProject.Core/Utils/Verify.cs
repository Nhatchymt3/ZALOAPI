using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XProject.Core.Utils
{
    public class Verify
    {
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null) return false;
            
            if (password == null) throw new ArgumentNullException("Password can't be empty");
           
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0)) return false;
            
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8)) buffer4 = bytes.GetBytes(0x20);
            
            return buffer3.SequenceEqual(buffer4);
        }

        public static string HashPassword(string password)
        {
            byte[] buffer4;

            if (password == null) throw new ArgumentNullException("Password can't be empty");

            byte[] dst = new byte[0x10];

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8)) buffer4 = bytes.GetBytes(0x20);
            
            return StringUltis.ByteArrayToHexString(buffer4);
        }

        public static bool CheckHash(string obj, string hash)
        {
            var key = StringUltis.ToByteArray(SystemHelper.Setting.SecretKey);
            var newobj = StringUltis.ToByteArray(obj);

            var hasher = new HMACSHA256(key);

            var newhash = hasher.ComputeHash(newobj);

            return hash.Equals(StringUltis.ByteArrayToHexString(newhash));
        }
    }
}
