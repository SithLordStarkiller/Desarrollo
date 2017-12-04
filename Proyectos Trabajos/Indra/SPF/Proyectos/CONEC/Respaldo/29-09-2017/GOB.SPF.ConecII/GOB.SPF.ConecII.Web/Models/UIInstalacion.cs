using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiInstalacion : UiEntity
    {
        [Key]
        [DisplayName(@"No.")]
        public int Identificador { get; set; }

        [ForeignKey("UiCliente"), Required(ErrorMessage = @"El cliente es un dato requerido")]
        public UiCliente Cliente { get; set; }

        [Required(ErrorMessage = @"Campo requerido")]
        [Range(1,1000,ErrorMessage = @"Se debe seleccionar una zona")]
        public int IdZona { get; set; }

        [Required(ErrorMessage = @"Campo requerido")]
        [Range(1, 1000, ErrorMessage = @"Se debe seleccionar una estacion")]
        public int IdEstacion { get; set; }

        [DisplayName(@"Nombre del la instalación*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"El nombre de la instalación es un dato requerido.")]
        [RegularExpression("^[\\w\\d ]{3,100}$", ErrorMessage = @"Verifique la información, formato proporcionado no es correcto.")]
        public string Nombre { get; set; }

        public UiTipoInstalacion TipoInstalacion { get; set; }
        
        [Column(TypeName = "date")]
        [Required(ErrorMessage = @"La fecha de inicio es requerida")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime FechaInicio { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
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

        [StringLength(5)]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"El código postal es un dato requerido.")]
        public string CodigoPostal { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = @"La división es requerida")]
        [DisplayName(@"División*:")]
        public int IdDivision { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = @"El grupo es requerido")]
        [DisplayName(@"Grupo*:")]
        public int IdGrupo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = @"La fracción es requerida")]
        [DisplayName(@"Fracción*:")]
        public int IdFraccion { get; set; }

        public int IdAsentamiento { get; set; }

        public int IdColonia { get; set; }

        public UiMunicipio Municipio { get; set; }

        public UiEstado Estado { get; set; }

        public decimal Latitud { get; set; }

        public decimal Longitud { get; set; }
        public bool Activo { get; set; }

        public int IdCliente { get; set; }
        public string Rfc { get; set; }
        public string NombreCorto { get; set; }
        public string RazonSocial { get; set; } 

        public string CorreoElectronico { get; set; }
        public UiFracciones Fracciones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<UiCorreoInstalacion> CorreosInstalacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<UiTelefonoInstalacion> TelefonosInstalacion { get; set; }
    }
}