using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Newtonsoft.Json;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiInstalacion : UiEntity
    {
        [Key]
        [DisplayName("No.")]
        public int Identificador { get; set; }

        [ForeignKey("UiCliente"), Required(ErrorMessage = "El cliente es un dato requerido")]
        public UiCliente Cliente { get; set; }

        public int IdZona { get; set; }

        public int IdEstacion { get; set; }

        [DisplayName("Nombre *"), Required(AllowEmptyStrings = false, ErrorMessage = "El nombre de la instalación es un dato requerido.")]
        [RegularExpression("^[\\w\\d ]{3,100}$", ErrorMessage = "Verifique la información, formato proporcionado no es correcto.")]
        public string Nombre { get; set; }

        public UiTipoInstalacion TipoInstalacion { get; set; }
        [Column(TypeName = "date"), Required(ErrorMessage = "La fecha de inicio es requerida"), DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime FechaInicio { get; set; }

        [Column(TypeName = "date"), DisplayFormat(DataFormatString = "dd/MM/yyyy", ConvertEmptyStringToNull = true)]
        public DateTime? FechaFin { get; set; }

        [MaxLength(60), Required(AllowEmptyStrings = false, ErrorMessage = "La calle es un dato requerido")]
        public string Calle { get; set; }

        [MaxLength(30)]
        public string NumInterior { get; set; }

        [MaxLength(30), Required(AllowEmptyStrings = false, ErrorMessage = "El número exterior es un dato requerido.")]
        public string NumExterior { get; set; }

        [MaxLength(1000)]
        public string Referencia { get; set; }

        [MaxLength(3000)]
        public string Colindancia { get; set; }

        [StringLength(5), Required(AllowEmptyStrings = false, ErrorMessage = "El código postal es un dato requerido.")]
        public string CodigoPostal { get; set; }

        public int IdAsentamiento { get; set; }

        public int IdColonia { get; set; }

        public UiMunicipio Municipio { get; set; }

        public UiEstado Estado { get; set; }

        public decimal Latitud { get; set; }

        public decimal Longitud { get; set; }

        public short IdFraccion { get; set; }

        public bool Activo { get; set; }

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