using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiCorreo : UiEntity
    {
        public int Indice { get; set; }
        public int Identificador { get; set; }
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage ="La dirección de correo es requerida")]
        [EmailAddress(ErrorMessage = "El formato de correo electrónico es incorrecto")]
        public string CorreoElectronico { get; set; }
        public bool IsActive { get; set; }
    }
}