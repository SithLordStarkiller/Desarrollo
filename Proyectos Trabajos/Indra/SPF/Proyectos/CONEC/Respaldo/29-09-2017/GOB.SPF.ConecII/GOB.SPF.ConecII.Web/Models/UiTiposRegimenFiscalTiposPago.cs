using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiTiposRegimenFiscalTiposPago
    {
        public int IdTipoRegimenFiscalTipoPago { set; get; }
        public UiConfiguracionTipoServicioArea ConfiguracionTipoServiciosArea { set; get; }
        public UiRegimenFiscal RegimenFiscal { set; get; }
        public UiTiposPago TiposPago { set; get; }
        public bool Aplica { set; get; }
        public UiTiposRegimenFiscalTiposPago()
        {
            ConfiguracionTipoServiciosArea = new UiConfiguracionTipoServicioArea();
            RegimenFiscal = new UiRegimenFiscal();
            TiposPago = new UiTiposPago();
        }
    }
}