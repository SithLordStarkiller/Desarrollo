using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiModulo : UiEntity
    {
        public int Identificador { get; set; }
        public int IdPadre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre del Modulo es requerido")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción del Modulo es requerido")]
        public string Descripcion { get; set; }
        /*esto se subio*/
        public string Enlace { get; set; }

        public bool IsActive { get; set; }
    }
}