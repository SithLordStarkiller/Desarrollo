using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiGrupo : UiEntity
    {
        public int Identificador { get; set; }

        [DisplayName("Grupo*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre es requerido")]
        public string Name { get; set; }

        [DisplayName("Descripción*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La Descripción es requerida")]
        public string Descripcion { get; set; }

        [DisplayName("Divisiones")]
        [Required(ErrorMessage = "Se requiere seleccionar una división")]
        public int IdDivision { get; set; }

        public string Division { get; set; }

        public bool IsActive { get; set; }
    }
}