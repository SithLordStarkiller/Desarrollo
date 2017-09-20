using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class ActividadesTiposDocumento : TEntity
    {
        public ConfiguracionTipoServicioArea ConfiguracionTipoServicioArea { set; get; }
        public TipoDocumento TipoDocumento { set; get; }
        public Actividad Actividad { set; get; }
        public bool Aplica { set; get; } 
        public bool Obligatoriedad { set; get; }

        public ActividadesTiposDocumento()
        {
            ConfiguracionTipoServicioArea = new ConfiguracionTipoServicioArea();
            TipoDocumento = new TipoDocumento();
            Actividad = new Actividad();
        }
    }
}
