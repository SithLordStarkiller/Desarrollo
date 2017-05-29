using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel
{
    public class TransferenciasATerceros
    {
        public DateTime FechaCreacion { get; set; }
        public string CodigoAutorizacion { get; set; }
        public string Remitente { get; set; }
        public string Destinatario { get; set; }
        public decimal Comision { get; set; }
        public string ConceptoTransferencia { get; set; }
    }
}