
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Web.Mvc;

namespace PubliPayments.Models
{
    public class Login
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(40)]
        public string tbDominio { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [StringLength(40)]
        public string tbUsuario { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20)]
        public string tbPassword { get; set; }
        
        [Display(Name = "Recordar mis Datos")]
        public bool cbRecordarme { get; set; }
    }

    public class OlvidasteContasenia
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(40)]
        public string tbDom { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(40)]
        public string tbUser { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(80)]
        public string tbMail { get; set; }
    }

    public class CambiarContrasenia
    {
        [Required]
        [StringLength(40)]
        public string tbAPDominio { get; set; }
        
        [Required]
        [StringLength(40)]
        public string tbAPUsuario { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20)]
        public string tbAPNPassword { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20)]
        public string tbAPCPassword { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string tbAPPassword { get; set; }
        
        [HiddenInput]
        public string tbidUsuario { get; set; }
        
        [HiddenInput]
        public string tbReturnUrl { get; set; }
        
        [HiddenInput]
        public string tbADominio { get; set; }
        
        [HiddenInput]
        public bool tipo { get; set; }
    }

    public class RecuperarContrasenia
    {
        [ReadOnly(true)]
        public string lblDominio { get; set; }
        [ReadOnly(true)]
        public string lblUsuario { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string tbPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string tbVerificar { get; set; }
        [HiddenInput]
        public string key { get; set; }
    }
}