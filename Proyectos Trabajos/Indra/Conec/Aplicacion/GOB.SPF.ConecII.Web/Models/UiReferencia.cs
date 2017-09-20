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
        //[Range(1, 9999, ErrorMessage = "La longitud máxima es de 4 y mínima de 1")]
        public int ClaveReferencia { get; set; }

        [DisplayName("Descripción*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción de la referencia es requerida")]
        //[StringLength(100, MinimumLength = 10, ErrorMessage = "El rango mínimo es de 10 y máximo de 100")]
        public string Descripcion { get; set; }
        public bool EsProducto { get; set; }
        public bool IsActive { get; set; }

        public string TipoProducto => EsProducto ? "Es producto" : "Aprobechamiento";
    }
}

