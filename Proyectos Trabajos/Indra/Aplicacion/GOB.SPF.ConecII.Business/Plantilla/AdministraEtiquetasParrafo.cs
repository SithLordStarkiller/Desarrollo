using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
using GOB.SPF.ConecII.Entities.Plantilla;
using System;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business.Plantilla
{
    public partial class AdministraEtiquetasParrafo : AdministraCrudReflection<EtiquetasParrafo, EtiquetasParrafoDtoBuscar>
    {
        #region Propiedades


        RepositorioEtiquetasParrafo mRepositorioEtiquetasParrafo;

        #endregion

        #region Constructor

        public AdministraEtiquetasParrafo()
        {
            mRepositorioEtiquetasParrafo = new RepositorioEtiquetasParrafo();
            Inicializa(mRepositorioEtiquetasParrafo);
        }

        #endregion

        #region IAdministraEtiquetasParrafo implementation

        public IEnumerable<EtiquetasParrafo> FindItemsStored(EtiquetasParrafoDtoBuscar busqueda)
        {
            return mRepositorioEtiquetasParrafo.GetPagedElementsStored(busqueda);
        }

        public IEnumerable<EtiquetasParrafo> FindPagedStored(EtiquetasParrafoDtoBuscar busqueda)
        {
            return mRepositorioEtiquetasParrafo.GetPagedElementsStored(busqueda);
        }

        public EtiquetasParrafo FindById(int IdEtiquetaParrafo)
        {
            EtiquetasParrafo entity = new EtiquetasParrafo() { IdEtiquetaParrafo = (int)IdEtiquetaParrafo };
            return mRepositorioEtiquetasParrafo.GetById(entity);
        }

        public List<EtiquetasParrafo> GetPorIdTipoDocumento(int IdTipoDocumento)
        {
            try
            {
                return mRepositorioEtiquetasParrafo.GetPorIdTipoDocumento(IdTipoDocumento);
            }
            catch (Exception ex)
            {
                //MensajeOperacion.MensajeException("Management_EtiquetasParrafo_ERROR_FindRelaciones", "Error al buscar EtiquetasParrafo", "AdministraEtiquetasParrafo_ERROR_FindRelaciones", "Error al buscar EtiquetasParrafo", ex);
                throw ex;
            }
        }

        #endregion


    }
}