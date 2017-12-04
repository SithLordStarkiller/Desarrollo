using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GOB.SPF.ConecII.Web.Models
{
    public class UiConfiguracionServicio : UiEntity
    {
        [Required( ErrorMessage = "El identificador del tipo de servicio es necesario")]
        public int IdTipoServicio { get; set; }

        [Required(ErrorMessage = "El identificador del centro de costo es necesario")]
        public string IdCentroCostos { get; set; }

        [Required(ErrorMessage = "El identificador del regimen fiscal")]
        public int IdRegimenFiscal { get; set; }

        [Required(ErrorMessage = "El identificador del tipo de pago es necesario")]
        public int IdTipoPago { get; set; }


        public int IdActividad { get; set; }
        public int IdTipoDocumento { get; set; }
        public int Tiempo { get; set; }
        public bool Aplica { get; set; }
        public bool Obigatoriedad { get; set; }

        
    
    }
}