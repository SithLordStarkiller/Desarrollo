using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
using GOB.SPF.ConecII.Entities.Plantilla;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business.Plantilla
{
    public partial class ParrafosBusiness : Business<Parrafos>
    {
        #region Propiedades

        RepositoryParrafos mRepositorioParrafos;

        #endregion

        #region Constructor

        public ParrafosBusiness()
        {
            mRepositorioParrafos = new RepositoryParrafos();
        }

        #endregion
        
    }
}