using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code
{
    [Serializable]
    public class DisposicionResponse
    {
        public bool Valida = false;
        public String UserResponse = String.Empty;
        public string NumeroAutorizacion = String.Empty;
    }
}