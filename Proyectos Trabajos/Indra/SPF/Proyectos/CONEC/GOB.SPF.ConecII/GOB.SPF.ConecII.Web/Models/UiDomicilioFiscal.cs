
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiDomicilioFiscal:UiDomicilio
    {
        public int IdCliente { get; set; }
    }
}