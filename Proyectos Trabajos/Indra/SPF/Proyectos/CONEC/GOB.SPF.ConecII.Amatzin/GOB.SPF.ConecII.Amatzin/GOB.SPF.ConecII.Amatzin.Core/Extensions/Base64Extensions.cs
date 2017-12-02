using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Amatzin.Core.Extensions
{
    public static class Base64Extensions
    {
        public static bool IsBase64(this string base64String)
        {

            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
                || base64String.Contains(" ") || base64String.Contains("\t") || 
                base64String.Contains("\r") || base64String.Contains("\n"))
                return false;

            try
            {
                var valor = Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
