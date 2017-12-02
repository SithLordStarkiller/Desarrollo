using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiTipoSolicitud : UiEntity
    {
        public int Identificador { get; set; }

        [DisplayName("Nombre*"), MaxLength(100)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [DisplayName("Descripción*"), MaxLength(500)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; }

        public bool IsActive { get; set; }
    }
}