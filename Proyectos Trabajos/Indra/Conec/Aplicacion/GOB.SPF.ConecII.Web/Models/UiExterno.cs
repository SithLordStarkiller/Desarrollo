using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiExterno : UiEntity
    {
        [DisplayName("No")]
        public int Identificador { get; set; }
        public int IdCliente { get; set; }
        public int IdTipoPersona { get; set; } 

        [DisplayName("Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre del contacto no puede quedar vacío")]
        public virtual string Nombre { get; set; }

        [DisplayName("Apellido paterno")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El apellido paterno del contacto no puede quedar vacío")]
        public virtual string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        [DisplayName("Cargo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El cargo del contacto no puede quedar vacío")]
        public virtual string Cargo { get; set; }

        public bool Activo { get; set; }

        public List<UiTelefonoContacto> Telefonos { get; set; }
        public List<UiCorreoContacto> Correos { get; set; }
    }
}