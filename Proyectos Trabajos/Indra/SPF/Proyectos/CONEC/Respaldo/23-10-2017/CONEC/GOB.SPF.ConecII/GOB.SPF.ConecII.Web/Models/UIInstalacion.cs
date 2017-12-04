namespace GOB.SPF.ConecII.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UiInstalacion : UiEntity
    {
        [Key]
        [DisplayName(@"No.")]
        public int Identificador { get; set; }

        [ForeignKey("UiCliente"), Required(ErrorMessage = @"El cliente es un dato requerido")]
        public UiCliente Cliente { get; set; }

        [Required(ErrorMessage = @"Campo requerido")]
        [Range(1,1000,ErrorMessage = @"Se debe seleccionar una zona")]
        public UiZona Zona { get; set; }

        public string NombreZona => Zona == null ? "" : Zona.Nombre;

        [Required(ErrorMessage = @"Campo requerido")]
        [Range(1, 1000, ErrorMessage = @"Se debe seleccionar una fase")]
        public UiFases Fases { get; set; }

        [Required(ErrorMessage = @"Campo requerido")]
        [Range(1, 1000, ErrorMessage = @"Se debe seleccionar una estacion")]
        public UiEstaciones Estacion { get; set; }

        public string NombreEstacion => Estacion == null ? "" : Estacion.Nombre;

        [DisplayName(@"Nombre de la instalación*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"El nombre de la instalación es un dato requerido.")]
        [RegularExpression("^[\\w\\d ]{3,100}$", ErrorMessage = @"Verifique la información, formato proporcionado no es correcto.")]
        public string Nombre { get; set; }

        public UiTipoInstalacion TipoInstalacion { get; set; }

        public List<UiTelefonoInstalacion> TelefonosInstalacion { get; set; }
        public List<UiCorreoInstalacion> CorreosInstalacion { get; set; }

        //[Column(TypeName = "date")]
        //[Required(ErrorMessage = @"La fecha de inicio es requerida")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        //[Column(TypeName = "date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaFin { get; set; }

        [MaxLength(60)]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"La calle es un dato requerido")]
        public string Calle { get; set; }

        [MaxLength(30)]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"El número exterior es un dato requerido.")]
        public string NumExterior { get; set; }

        [MaxLength(30, ErrorMessage = @"Se a alcanzado el maximo en el numero de caracteres")]
        public string NumInterior { get; set; }

        [MaxLength(1000, ErrorMessage = @"Se a alcanzado el maximo en el numero de caracteres")]
        public string Referencia { get; set; }

        [MaxLength(3000, ErrorMessage = @"Se a alcanzado el maximo en el numero de caracteres")]
        public string Colindancia { get; set; }

        public UiAsentamiento Asentamiento { get; set; }

        public string NombreEstado =>Asentamiento == null ? "" : Asentamiento.Estado.Nombre;
        public string NombreMunicipio => Asentamiento == null ? "" : Asentamiento.Municipio.Nombre;

        [Required(AllowEmptyStrings = false, ErrorMessage = @"La división es requerida")]
        [DisplayName(@"División*:")]
        public UiDivision Divicion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = @"El grupo es requerido")]
        [DisplayName(@"Grupo*:")]
        public UiGrupo Grupo{ get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = @"La fracción es requerida")]
        [DisplayName(@"Fracción*:")]
        public UiFracciones Fraccion { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public bool Activo { get; set; }
        

        public int IdCliente { get; set; }
        public string Rfc { get; set; }
        public string NombreCorto { get; set; }
        public string RazonSocial { get; set; }
        public bool Seleccionado { get; set; }
    }
}