using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiGastosInherente : UiEntity
    {
        public int Identificador { get; set; }

        [DisplayName("Nombre*")]
        [Required(AllowEmptyStrings =false,ErrorMessage = "El nombre de la división es requerido")]
        [MaxLength(150)]
        public string Name { get; set; }

        [DisplayName("Descripción*")]
        [MaxLength(250)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción de la división es requerido")]
        public string Descripcion { get; set; }

        public bool IsActive { get; set; }
    }
}