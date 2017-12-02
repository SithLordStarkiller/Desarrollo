using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiClasificacionFactor : UiEntity
    {
        public int Identificador { get; set; }

        [DisplayName("Nombre*"), MaxLength(150)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre de la clasificación factor es requerido")]
        public string Nombre { get; set; }

        [DisplayName("Descripción*"), MaxLength(250)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción de la clasificación factor es requerido")]
        public string Descripcion { get; set; }

        public bool IsActive { get; set; }
    }
}