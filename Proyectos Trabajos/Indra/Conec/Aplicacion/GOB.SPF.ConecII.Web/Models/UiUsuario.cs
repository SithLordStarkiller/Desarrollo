using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GOB.SPF.ConecII.Web.Models
{
    public class UiUsuario : UiEntity
    {
        public int Identificador { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre del Rol es requerido")]
        public int IdPersona { get; set; }

        public int IdExterno { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción del Rol es requerido")]
        public string Login { get; set; }
        public byte Password { get; set; }
        public bool IsActive { get; set; }
    }
}