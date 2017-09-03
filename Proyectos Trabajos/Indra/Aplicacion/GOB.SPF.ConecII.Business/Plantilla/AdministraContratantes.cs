using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
using GOB.SPF.ConecII.Entities.Plantilla;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business.Plantilla
{
    public partial class AdministraContratantes : AdministraCrudReflection<Contratantes, ContratantesDtoBuscar>
    {
        #region Propiedades

        //Contratantes AdministraContratantes.ContratantesActual { get; set; }

        RepositorioContratantes mRepositorioContratantes;

        #endregion

        #region Constructor

        public AdministraContratantes()
        {
            mRepositorioContratantes = new RepositorioContratantes();
            Inicializa(mRepositorioContratantes);
        }

        #endregion

        #region IAdministraContratantes implementation

        public IEnumerable<Contratantes> FindItemsStored(ContratantesDtoBuscar busqueda)
        {
            return mRepositorioContratantes.GetPagedElementsStored(busqueda);
        }

        public IEnumerable<Contratantes> FindPagedStored(ContratantesDtoBuscar busqueda)
        {
            return mRepositorioContratantes.GetPagedElementsStored(busqueda);
        }

        public Contratantes FindById(int IdContratante)
        {
            Contratantes entity = new Contratantes() { IdContratante = (int)IdContratante };
            return mRepositorioContratantes.GetById(entity);
        }



        #endregion


    }
}