using System.ComponentModel.DataAnnotations;

namespace PubliPayments.Models
{
    public class DespachoModel
    {        
        [Required]
        [Display(Name = "Nombre")]
        public string tbDespacho { get; set; }

        [Required]
        [Display(Name = "Nombre Corto")]
        public string tbNombreCorto { get; set; }

        [Required]
        [Display(Name = "Usuario Administrador")]
        public string tbNombreUsuario { get; set; }

        [Required]
        [Display(Name = "Nombre Administrador")]
        public string tbNombre { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string tbEmail { get; set; }
    }

    public class DespachoEditModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string ADNombre { get; set; }

        [Required]
        [Display(Name = "Nombre Corto")]
        public string ADNCorto { get; set; }

        [Required]
        [Display(Name = "Estatus")]
        public bool ADEstatus { get; set; }

        [Required]
        public int idDominio { get; set; }

        public string ADEstatusH { get; set; }
    }
}