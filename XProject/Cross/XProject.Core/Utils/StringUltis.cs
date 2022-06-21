using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XProject.Core.Utils
{
    public class StringUltis
    {
        public static byte[] ToByteArray(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static string ByteArrayToHexString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", String.Empty);
        }
    }
}
