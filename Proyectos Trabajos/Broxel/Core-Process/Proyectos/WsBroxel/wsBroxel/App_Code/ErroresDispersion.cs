using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code
{
    public class Error
    {
        public String Message { get; set; }
        public String Code { get; set; }
    }

    public class ErroresDispersion
    {
        public Int32 CodigoRespuesta = 999;
        public String DescripcionCodigoResp = "Error Desconocido";
        public String CausaComunCodigoResp = " ";
        public List<String> CuentasConError = new List<string>();
    }
}