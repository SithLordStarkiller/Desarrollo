using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiTiposServicio : UiEntity
    {
        public int Identificador { get; set; }

        [DisplayName("Nombre*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre del tipo servicio es requerido")]
        public string Name { get; set; }

        [DisplayName("Descripción*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción del tipo servicio es requerido")]
        public string Descripcion { get; set; }

        [DisplayName("Clave*")]
        [StringLength(4, MinimumLength = 1, ErrorMessage = "El rango mínimo es de 1 y máximo de 4")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La clave del tipo servicio es requerido")]
        public string Clave { get; set; }

        public bool IsActive { get; set; }
    }
}