using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsServices01800.AppCode
{
    [Serializable]
    public class JBuroCreditoResponse
    {
        public string SujetoDeCredito { set; get; }
        public string Razon { get; set; }
        public string RutaPdf { get; set; }
    }
}