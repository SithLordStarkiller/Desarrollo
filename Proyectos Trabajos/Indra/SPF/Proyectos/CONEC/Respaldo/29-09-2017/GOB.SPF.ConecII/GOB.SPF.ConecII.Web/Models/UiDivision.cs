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

        [MaxLength(50), Required(AllowEmptyStrings =false,ErrorMessage = "El Nombre es requerido")]
        public string Name { get; set; }

        [MaxLength(300), Required(AllowEmptyStrings = false, ErrorMessage = "La Descripción es requerida")]
        public string Descripcion { get; set; }

        public bool IsActive { get; set; }

    }
}