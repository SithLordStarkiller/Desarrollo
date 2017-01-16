using System.ComponentModel.DataAnnotations;

namespace PubliPayments.Models
{
    public class UsuarioMapa
    {
        [Key]
        public int IdUduario { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
    }
}