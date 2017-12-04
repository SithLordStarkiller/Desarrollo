using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiReferencia : UiEntity
    {
        public int Identificador { get; set; }
      
        [DisplayName("Clave Referencia*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La clave referencia es requerida"), RegularExpression("([0-9]+)", ErrorMessage = "Favor de ingresar número")]
        public int ClaveReferencia { get; set; }

        [DisplayName("Descripción*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción de la referencia es requerida")]
        [MaxLength(300), MinLength(10)]
        public string Descripcion { get; set; }
        public bool EsProducto { get; set; }
        public bool IsActive { get; set; }
       

        

        //public string TipoProducto => EsProducto ? "Es producto" : "Aprobechamiento";
    }
}

