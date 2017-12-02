using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiTelefono : UiEntity
    {
        public int Indice { get; set; }

        [Required(ErrorMessage = "Elija un elemento")]
        public int IdTipoTelefono { get; set; }
        public string TipoTelefono { get; set; }

        [Required(ErrorMessage = "El número de teléfono es requerido"), StringLength(10),RegularExpression(@"^[0-9]*$"/*,@"Solo se adminten numeros"*/)]
        public string Numero { get; set; }
        [MaxLength(50)]
        public string Extension { get; set; }
        public bool IsActive { get; set; }
    }
}