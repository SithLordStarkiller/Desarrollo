using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiConfiguracionTipoServicioArea
    {
        #region Propiedades



        public UiTiposServicio TipoServicio { get; set; }

        public UiArea Area { set; get; }

        public bool Aplica { set; get; }

        #endregion Propiedades

        #region PropiedadesNavegacion

        public List<UiTiposRegimenFiscalTiposPago>  ListaTiposRegimenFiscalTiposPago;
        public List<UiTiposPagoActividades>         ListaTiposPagoActividades;
        public List<UiActividadesTiposDocumento>    ListaActividadesTiposDocumento;

        #endregion PropiedadesNavegacion

        public UiConfiguracionTipoServicioArea()
        {
            TipoServicio = new UiTiposServicio();
            Area = new UiArea();
            ListaTiposRegimenFiscalTiposPago    = new List<UiTiposRegimenFiscalTiposPago>();
            ListaTiposPagoActividades           = new List<UiTiposPagoActividades>();
            ListaActividadesTiposDocumento      = new List<UiActividadesTiposDocumento>();
        }
    }
}