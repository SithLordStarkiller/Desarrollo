using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiActividad : UiEntity
    {
        public int Identificador { get; set; }
        public int IdTipoPago { get; set; }
        public int IdFase { get; set; }

        [MaxLength(50), DisplayName("Nombre*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre de la división es requerido")]
        public string Name { get; set; }

        [MaxLength(100), DisplayName("Descripción*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción de la división es requerido")]
        public string Descripcion { get; set; }

        [DisplayName("Se PuedeAplicar Plazo*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción de la división es requerido")]
        public bool SePuedeAplicarPlazo { get; set; }

        public bool Validacion { get; set; }
        public bool IsActive { get; set; }
    }
}