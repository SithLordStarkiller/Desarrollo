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

        [DisplayName("Nombre"), MaxLength(60)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre del contacto no puede quedar vacío")]
        public virtual string Nombre { get; set; }

        [DisplayName("Apellido paterno"), MaxLength(60)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El apellido paterno del contacto no puede quedar vacío")]
        public virtual string ApellidoPaterno { get; set; }
        [MaxLength (60)]
        public string ApellidoMaterno { get; set; }

        [DisplayName("Cargo"), MaxLength(60)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El cargo del contacto no puede quedar vacío")]
        public virtual string Cargo { get; set; }

        public bool IsActive { get; set; }

        public List<UiTelefonoContacto> Telefonos { get; set; }
        public List<UiCorreoContacto> Correos { get; set; }
    }
}