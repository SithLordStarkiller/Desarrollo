using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
using GOB.SPF.ConecII.Entities.Plantilla;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business.Plantilla
{
    public partial class InstitucionesBusiness : Business<Instituciones>
    {
        #region Propiedades

        RepositoryInstituciones mRepositorioInstituciones;

        #endregion

        #region Constructor

        public InstitucionesBusiness()
        {
            mRepositorioInstituciones = new RepositoryInstituciones();
        }

        #endregion
        
    }
}