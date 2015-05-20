namespace Universidad.WebAdministrativa.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ModelPersonaDireccion
    {
        [DisplayName("Estado")]
        [Required(ErrorMessage = "El estado es requerido")]
        public string IdEstado { get; set; }

        [DisplayName("Municipio")]
        [Required(ErrorMessage = "El municipio es requerido")]
        public string IdMunicipio { get; set; }

        [DisplayName("Colonia")]
        [Required(ErrorMessage = "La colonia es un valor obligatorio")]
        public string IdColonia { get; set; }

        [DisplayName("Codigo Postal")]
        [RegularExpression("^([0-9]{5})$", ErrorMessage = "El formato de codigo postal es incorrecto")]
        [Required(ErrorMessage = "El codigo postal es un valor requerido requerido")]
        public string CodigoPostal { get; set; }

        [DisplayName("Calle")]
        [Required(ErrorMessage = "La calle es un campo obligatorio")]
        [StringLength(30, ErrorMessage = "El nombre de la calle es muy largo"), MinLength(2, ErrorMessage = "El nombre de la calle es muy corto")]
        public string Calle { get; set; }

        [DisplayName("No Exterior")]
        [Required(ErrorMessage = "El No Exterior es un campo obligatorio")]
        public string NoExterior { get; set; }
        public string NoInterior { get; set; }
        public string ReferenciasAdicionalies { get; set; }
    }
}