using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiCliente : UiEntity
    {
        public UiCliente()
        {
            Instalaciones = new List<UiInstalacion>();
        }

        #region Datos para la tabla de Cliente
        [DisplayName("No")]
        public int Identificador { get; set; }

        [DisplayName("Razón social (Dependencia o Empresa Privada)*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La razón social del cliente es requerida")]
        public string RazonSocial { get; set; }

        [DisplayName("Razón Social*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La razón social del cliente es requerida")]
        public int IdRazonSocial { get; set; }

        [DisplayName("Nombre corto*:")]
        [Required(ErrorMessage = "El nombre corto del cliente es requerido")]
        public string NombreCorto { get; set; }

        [DisplayName("Nombre Corto*:")]
        [Required(ErrorMessage = "El nombre corto del cliente es requerido")]
        public int IdNombreCorto { get; set; }

        [DisplayName("Régimen Fiscal*:")]
        [Required(ErrorMessage = "El régimen fiscal del cliente es requerido")]
        public int IdRegimenFiscal { get; set; }

        /// <summary>
        /// Es el valor de la descripción del Régimen fiscal almacenado para el Cliente
        /// </summary>
        public string RegimenFiscal { get; set; }

        [DisplayName("Sector*:")]
        [Required(ErrorMessage = "El sector del cliente es requerido")]
        public int IdSector { get; set; }

        /// <summary>
        /// Es el valor de la descripcion del Sector almacenado para el Cliente
        /// </summary>
        public string Sector { get; set; }

        [DisplayName("RFC*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El rfc del cliente es requerido")]
        [RegularExpression(@"^[A-Za-z]{1}([A-Za-z]{2}|[AEIOUXaeioux]{1}[A-Za-z]{2})((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229)[0-9a-zA-Z]{3}$",
            ErrorMessage = "El RFC no tiene el formato correcto")]
        public string RFC { get; set; }

        [DisplayName("RFC*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El rfc del cliente es requerido")]
        public int IdRFC { get; set; }

        public bool? IsActive { get; set; }
        #endregion

        public UiDomicilioFiscal DomicilioFiscal { get; set; }
        public List<UiSolicitante> Solicitantes { get; set; }
        public List<UiClienteContacto> Contactos { get; set; }

        public List<UiInstalacion> Instalaciones { get; set; }
    }

    public class UiClienteDPE : UiEntity
    {
        [DisplayName("No")]
        public int Identificador { get; set; }

        [DisplayName("Razón social (Dependencia o Empresa Privada)*"), MaxLength(500)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La razón social del cliente es requerida")]
        public string RazonSocial { get; set; }

        [DisplayName("Nombre corto*"), MaxLength(60)]
        [Required(ErrorMessage = "El nombre corto del cliente es requerido")]
        public string NombreCorto { get; set; }
        
        public bool IsActive { get; set; }
    }
}