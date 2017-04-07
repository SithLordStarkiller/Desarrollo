using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Utilerias
{
    public class JSonParser
    {
        public static string ObjectToJson(Object o)
        {
            return JsonConvert.SerializeObject(o);
        }
    }
}
