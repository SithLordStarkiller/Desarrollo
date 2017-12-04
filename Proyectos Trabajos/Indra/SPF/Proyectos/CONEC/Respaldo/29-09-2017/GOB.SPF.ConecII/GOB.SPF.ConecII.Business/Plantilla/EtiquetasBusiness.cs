using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
using GOB.SPF.ConecII.Entities.Plantilla;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business.Plantilla
{
    public partial class EtiquetasBusiness : Business<Etiquetas>
    {
        #region Propiedades

        RepositoryEtiquetas mRepositorioEtiquetas;

        #endregion

        #region Constructor

        public EtiquetasBusiness()
        {
            mRepositorioEtiquetas = new RepositoryEtiquetas(MRepositorio.UoW);
            MRepositorio = mRepositorioEtiquetas;

        }

        #endregion
        
    }
}