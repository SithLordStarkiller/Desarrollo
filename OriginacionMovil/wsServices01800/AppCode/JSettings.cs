using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsServices01800.AppCode
{
    [Serializable]
    public class JSettings
    {
        public string ReadOnly { get; set; }
        public string Requested { get; set; }
        public string Visible { get; set; }
    }
}