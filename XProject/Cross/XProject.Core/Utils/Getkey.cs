using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XProject.Core.Utils
{
    public class Getkey
    {
        public static string GetResourceString(string key)
        {
            try
            {
                return ResourceUtil.Instance.GetString(key);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
  

}
