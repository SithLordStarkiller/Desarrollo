using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Plantilla;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business.Plantilla
{
    public partial class TiposDocumentoBusiness : Business<TiposDocumento>
    {
        #region Propiedades

        AccessData.Repositories.Plantilla.RepositoryTiposDocumento mRepositorioTiposDocumento;

        #endregion

        #region Constructor

        public TiposDocumentoBusiness()
        {

            mRepositorioTiposDocumento = new AccessData.Repositories.Plantilla.RepositoryTiposDocumento(MRepositorio.UoW);
            MRepositorio = mRepositorioTiposDocumento;
        }

        #endregion
        

    }
}