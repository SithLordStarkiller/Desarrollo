using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
using GOB.SPF.ConecII.Entities.Plantilla;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business.Plantilla
{
    public partial class ContratantesBusiness : Business<Contratantes>
    {
        #region Propiedades

        //Contratantes AdministraContratantes.ContratantesActual { get; set; }

        RepositoryContratantes mRepositorioContratantes;

        #endregion

        #region Constructor

        public ContratantesBusiness()
        {
            mRepositorioContratantes = new RepositoryContratantes(MRepositorio.UoW);
            MRepositorio = mRepositorioContratantes;
        }

        #endregion

        

    }
}