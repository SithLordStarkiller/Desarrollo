using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.RequestResponses
{
    [Serializable]
    public class RealizarCargoMontosResp
    {
        public CargoDetalleMonto DetalleCargoRealizado { get; set; }
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public string MensajeError { get; set; }
    }

   [Serializable]
    public class CargoDetalleMonto
    {
       public int IdComercio { get; set; }
       public string Folio { get; set; }
       public bool hizoCargoMasivo { get; set; }
    }


}