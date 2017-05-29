using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.SolicitudBL.Model
{
    public class CargosDisposiciones
    {
        public string NumCuenta { set; get; }
        public int IdDisposicion { set; get; }
        public string NumAutorizacion { set; get; }
        public decimal Monto { set; get; }
    }
}