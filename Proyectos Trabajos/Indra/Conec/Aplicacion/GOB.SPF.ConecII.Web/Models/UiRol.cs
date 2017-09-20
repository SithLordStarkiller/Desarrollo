using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiRol : UiEntity
    {
        public int Identificador { get; set; }
        public int IdentificadorSubRol { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre del Rol es requerido")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción del Rol es requerido")]
        public string Descripcion { get; set; }
        public bool IsActive { get; set; }
    }
}