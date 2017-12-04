using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
using GOB.SPF.ConecII.Entities.Plantilla;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business.Plantilla
{
    public partial class PartesDocumentoBusiness : Business<PartesDocumento>
    {
        #region Propiedades

        RepositoryPartesDocumento mRepositorioPartesDocumento;

        #endregion

        #region Constructor

        public PartesDocumentoBusiness()
        {
            mRepositorioPartesDocumento = new RepositoryPartesDocumento();
            
        }

        #endregion
        
    }
}