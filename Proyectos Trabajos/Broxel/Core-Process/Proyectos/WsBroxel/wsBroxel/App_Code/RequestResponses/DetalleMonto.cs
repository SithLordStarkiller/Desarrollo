using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.RequestResponses
{
    [Serializable]
    public class DetalleMonto
    {
        public string NumCuenta { get; set; }
        public decimal Monto { get; set; }
        public string Referencia { set; get; }
    }
}