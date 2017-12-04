using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiCete : UiEntity
    {
        public int Identificador { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TasaRendimiento { get; set; }
        public Guid UniqueId { get; set; }
        public UiDocumento Documento { get; set; }
    }
}