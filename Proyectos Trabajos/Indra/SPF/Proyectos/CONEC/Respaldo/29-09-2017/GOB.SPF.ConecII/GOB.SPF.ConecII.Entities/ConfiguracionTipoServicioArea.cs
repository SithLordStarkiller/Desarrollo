using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class ConfiguracionTipoServicioArea : TEntity
    {
        #region Propiedades

        public TipoServicio TipoServicio {get; set; }

        public Area Area { set; get; }

        public bool Aplica { set; get; }
        #endregion Propiedades

        #region PropiedadesNavegacion

        public List<TiposRegimenFiscalTiposPago> ListaTiposRegimenFiscalTiposPago;
        public List<TiposPagoActividades> ListaTiposPagoActividades;
        public List<ActividadesTiposDocumento> ListaActividadesTiposDocumento;
        
        #endregion PropiedadesNavegacion

        public ConfiguracionTipoServicioArea()
        {
            TipoServicio = new TipoServicio();
            Area = new Area();
            ListaTiposRegimenFiscalTiposPago = new List<TiposRegimenFiscalTiposPago>();
            ListaTiposPagoActividades = new List<TiposPagoActividades>();
            ListaActividadesTiposDocumento = new List<ActividadesTiposDocumento>();
        }

        
    }
}
