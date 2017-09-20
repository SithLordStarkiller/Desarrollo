using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class TiposRegimenFiscalTiposPago : TEntity
    {
        public ConfiguracionTipoServicioArea ConfiguracionTipoServiciosArea{set; get;}
        public RegimenFiscal RegimenFiscal { set; get; }
        public TiposPago TiposPago { set; get; }
        public bool Aplica { set; get; }
        public TiposRegimenFiscalTiposPago()
        {
            ConfiguracionTipoServiciosArea = new ConfiguracionTipoServicioArea();
            RegimenFiscal = new RegimenFiscal();
            TiposPago = new TiposPago();
        }
    }
}
