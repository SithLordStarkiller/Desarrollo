using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
using GOB.SPF.ConecII.Entities.Plantilla;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business.Plantilla
{
    public partial class TiposSeccionBusiness : Business<TiposSeccion>
    {
        #region Propiedades

        RepositoryTiposSeccion mRepositorioTiposSeccion;

        #endregion

        #region Constructor

        public TiposSeccionBusiness()
        {            
            mRepositorioTiposSeccion = new RepositoryTiposSeccion(MRepositorio.UoW);
            MRepositorio = mRepositorioTiposSeccion;
        }

        #endregion
        
    }
}