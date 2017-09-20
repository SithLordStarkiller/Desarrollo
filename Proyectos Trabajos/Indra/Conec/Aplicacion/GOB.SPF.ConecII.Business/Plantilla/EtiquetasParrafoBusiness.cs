using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Plantilla;
using System;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business.Plantilla
{
    public partial class EtiquetasParrafoBusiness : Business<EtiquetasParrafo>
    {
        #region Propiedades


        RepositoryEtiquetasParrafo mRepositorioEtiquetasParrafo;

        #endregion

        #region Constructor

        public EtiquetasParrafoBusiness()
        {
            mRepositorioEtiquetasParrafo = new RepositoryEtiquetasParrafo(MRepositorio.UoW);
            MRepositorio = mRepositorioEtiquetasParrafo;
        }

        #endregion

        public List<EtiquetasParrafo> ObtenerPorIdParteDocumento(Paging paging, EtiquetasParrafo item)
        {
            List<EtiquetasParrafo> partesDocumento = mRepositorioEtiquetasParrafo.ObtenerPorIdParteDocumento(paging, item);
            uow.SaveChanges();
            return partesDocumento;

        }

    }
}