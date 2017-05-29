using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.RequestResponses
{
    /// <summary>
    /// Response pago de servicios
    /// </summary>

    [Serializable]
    public class PagoServicioResp
    {
        public long IdTransac { set; get; }
        public string Message { set; get; }
    }
}