using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Universidad.WebAdministrativa.Models
{
    public class ModelPersonaTelMedios
    {
        [DisplayName("Telefono Trabajo")]
        [RegularExpression(@"\(?\d{2,3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "El formato del telefono del trabajo es incorrecto")]
        public string TelefonoFijoTrabajo { get; set; }

        [DisplayName("Telefono Domicilio")]
        [Required(ErrorMessage = "El telefono de domicilio es un campo obligatorio")]
        [RegularExpression(@"\(?\d{2,3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "El formato del telefono personal es incorrecto")]
        public string TelefonoFijoCasa { get; set; }

        [DisplayName("Movil Personal")]
        [Required(ErrorMessage = "El mobil personal es un campo obligatorio")]
        [RegularExpression(@"\(?\d{2,3}\)?-? *\d{3}-? *-?\d{4}",ErrorMessage = "El formato del mobil personal es incorrecto")]
        public string TelefonoMovilPersonal { get; set; }

        [DisplayName("Movil Trabajo")]
        [RegularExpression(@"\(?\d{2,3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "El formato del mobil trabajo es incorrecto")]
        public string TelefonoMovilTrabajo { get; set; }

        [DisplayName("Fax")]
        [RegularExpression(@"\(?\d{2,3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "El formato del fax es incorrecto")]
        public string Fax { get; set; }

        [DisplayName("Correo Electronico Personal")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "El formato del correo electronico personal es incorrecto")]
        public string CorreoElectronicoPersonal { get; set; }

        [DisplayName("Correo Electronico Universidad")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "El formato del correo electronico de la universidad es incorrecto")]
        public string CorreoElectronicoTrabajo { get; set; }

        [DisplayName("Red Social 1")]
        public string RedSocial1 { get; set; }

        [DisplayName("Red Social 2")]
        public string RedSocial2 { get; set; }
    }
}