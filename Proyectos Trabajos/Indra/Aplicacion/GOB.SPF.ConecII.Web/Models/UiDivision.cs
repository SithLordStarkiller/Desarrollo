using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiDivision: UiEntity
    {
        public int Identificador { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage = "El nombre de la división es requerido")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción de la división es requerido")]
        public string Descripcion { get; set; }

        public bool IsActive { get; set; }
    }
}