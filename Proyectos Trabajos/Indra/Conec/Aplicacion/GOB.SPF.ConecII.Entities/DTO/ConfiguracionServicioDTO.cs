using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.DTO
{
    public class ConfiguracionServicioDTO
    {
        public int IdTipoServicio { get; set; }
        public string IdCentroCostos { get; set; }
        public int IdRegimenFiscal { get; set; }
        public int IdTipoPago { get; set; }
        public int? IdActividad { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string TipoServicio { get; set; }
        public string CentroCostos { get; set; }
        public string RegimenFiscal { get; set; }
        public string TipoPago { get; set; }
        public string Activad { get; set; }
        public string TipoDocumento { get; set; }
        public decimal? Tiempo { get; set; }
        public bool? Aplica { get; set; }
        public bool? Obigatoriedad { get; set; }
    }
}
