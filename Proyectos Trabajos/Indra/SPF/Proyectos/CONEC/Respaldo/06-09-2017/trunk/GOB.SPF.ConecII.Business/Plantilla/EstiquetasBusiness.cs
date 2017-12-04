using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
using GOB.SPF.ConecII.Entities.Plantilla;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business.Plantilla
{
    public partial class EstiquetasBusiness : Business<Estiquetas>
    {
        #region Propiedades

        RepositoryEstiquetas mRepositorioEstiquetas;

        #endregion

        #region Constructor

        public EstiquetasBusiness()
        {
            mRepositorioEstiquetas = new RepositoryEstiquetas();

        }

        #endregion
        
    }
}