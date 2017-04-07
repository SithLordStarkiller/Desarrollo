using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsServices01800.AppCode
{
    [Serializable]
    public class JInfonavitResponse
    {
        public string Nss { set; get; }
        public string RfcPrecalificacion { set; get; }
        public string Nombre { set; get; }
        public string PlazosInfo { set; get; }

    }
}