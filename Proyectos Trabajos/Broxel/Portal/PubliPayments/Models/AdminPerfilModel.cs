using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PubliPayments.Models
{
    public class AdminPerfilModel
    {
        [ReadOnly(true)]
        public string AuUsuario { get; set; }
        public string AuNombre { get; set; }
        public string AuEmail { get; set; }
        [DataType(DataType.Password)]
        public string AuNuevoPassword { get; set; }
        [DataType(DataType.Password)]
        public string AuConfirmarPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string AuPassword { get; set; }
    }
}
