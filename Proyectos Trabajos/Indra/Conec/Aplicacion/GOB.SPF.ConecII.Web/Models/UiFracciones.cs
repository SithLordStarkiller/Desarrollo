using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiFracciones : UiEntity
    {
        public int Identificador { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre de la fracción es requerido")]
        public string Nombre { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción de la fracción es requerido")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage ="Debe seleccionar un grupo")]
        public int? IdGrupo { get; set; }
        [Required(ErrorMessage = "Debe seleccionar una división")]
        public int IdDivision { get; set; }
        public bool IsActive { get; set; }
        /*propiedades para información*/
        public string Grupo { get; set; }
        public string Division { get; set; }
    }
}