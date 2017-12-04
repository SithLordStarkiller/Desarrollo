using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class DetalleConfiguracionServicio
    {
        public int IdRegimenFiscal { get; set; }
        public string RegimenFiscal { get; set; }

        public int IdTipoServicio { get; set; }
        public string TipoServicio { get; set; }

        public int IdTipoPago { get; set; }
        public string TipoPago { get; set; }

        public DateTime FechaRegistro { get; set; }
        public bool Estatus { get; set; }

        public int IdCentroCosto { get; set; }
    }
}
