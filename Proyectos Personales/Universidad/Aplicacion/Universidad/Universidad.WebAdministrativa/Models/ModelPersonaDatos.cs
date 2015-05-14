using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Universidad.WebAdministrativa.Models
{
    public class ModelPersonaDatos
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El nombre es un campo obligatorio")]
        [StringLength(30)]
        [RegularExpression("^[A-Za-z]*$",ErrorMessage = "El nombre no es valido")]
        public string Nombre { get; set; }

        [DisplayName("Apellido Paterno")]
        [Required(ErrorMessage = "El apellido paterno es un campo obligatorio")]
        [StringLength(30)]
        [RegularExpression("^[A-Za-z]*$",ErrorMessage = "El apellido no es valido")]
        public string ApellidoP { get; set; }

        [DisplayName("Apellido Materno")]
        [StringLength(30)]
        [RegularExpression("^[A-Za-z]*$",ErrorMessage = "El apellido no es valido")]
        public string ApellidoM { get; set; }

        [DisplayName("CURP")]
        [RegularExpression("^[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[HM]{1}(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)[B-DF-HJ-NP-TV-Z]{3}[0-9A-Z]{1}[0-9]{1}$"
            , ErrorMessage = "El CURP no es valido")]
        public string Curp { get; set; }

        [DisplayName("RFC")]
        [RegularExpression("^[A-Z]{4}([0-9]{2})(1[0-2]|0[1-9])([0-3][0-9])([ -]?)([A-Z0-9]{3,4})$"
            , ErrorMessage = "El RFC no es valido")]
        public string Rfc { get; set; }

        [DisplayName("NSS")]
        [StringLength(11)]
        [RegularExpression("^[0-9]{11}$",ErrorMessage = "El numero de seguro social no es valido")]
        public string Nss { get; set; }

        [DisplayName("Fecha de nacimiento")]
        [Required(ErrorMessage = "La fecha de nacimiento es un campo obligatorio")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [DisplayName("Nacionalidad")]
        [Required(ErrorMessage = "La nacionalidad es un campo obligatorio")]
        public string IdNacionalidad { get; set; }

        [DisplayName("Rol")]
        [Required(ErrorMessage = "El tipo de rol es un campo obligatorio")]
        public string IdTipoPersona { get; set; }

        [DisplayName("Sexo")]
        [Required(ErrorMessage = "El genero es un campo obligatorio")]
        public string IdSexo { get; set; }
    }
}