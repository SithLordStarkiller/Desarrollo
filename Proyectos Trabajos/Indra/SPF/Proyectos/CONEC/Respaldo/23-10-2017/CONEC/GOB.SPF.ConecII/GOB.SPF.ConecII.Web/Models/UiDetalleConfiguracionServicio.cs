using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiDetalleConfiguracionServicio:UiEntity
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