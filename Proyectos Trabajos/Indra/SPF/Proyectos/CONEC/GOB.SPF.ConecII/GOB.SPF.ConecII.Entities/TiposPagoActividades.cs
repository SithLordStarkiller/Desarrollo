using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class TiposPagoActividades : TEntity
    {
        public ConfiguracionTipoServicioArea ConfiguracionTipoServicioArea { set; get; }
        public TiposPago TiposPago { set; get; }
        public Actividad Actividad { set; get; }
        public bool Aplica { set; get; }
        public int Tiempo { set; get; }
        public TiposPagoActividades()
        {
            ConfiguracionTipoServicioArea = new ConfiguracionTipoServicioArea();
            TiposPago = new TiposPago();
            Actividad = new Actividad();
        }
    }
}
