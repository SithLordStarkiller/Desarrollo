using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
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
            mRepositorioEtiquetasParrafo = new RepositoryEtiquetasParrafo();
        }

        #endregion
        
    }
}