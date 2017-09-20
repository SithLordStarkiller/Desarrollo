
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiDomicilioFiscal : UiEntity
    {
        public int Identificador { get; set; }
        public int IdCliente { get; set; }
        public int IdPais { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public int IdMunicipio { get; set; }
        public string Municipio { get; set; }
        public int IdAsentamiento { get; set; }
        public string Asentammiento { get; set; }
        
        [StringLength(5, MinimumLength = 5, ErrorMessage = "El código postal es de 5 dígitos")]
        public string CodigoPostal { get; set; }
        public string Calle { get; set; }
        public string NoInterior { get; set; }
        public string NoExterior { get; set; }
    }
}