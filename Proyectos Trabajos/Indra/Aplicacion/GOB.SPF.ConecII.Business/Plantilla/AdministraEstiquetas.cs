using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
using GOB.SPF.ConecII.Entities.Plantilla;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business.Plantilla
{
    public partial class AdministraEstiquetas : AdministraCrudReflection<Estiquetas, EstiquetasDtoBuscar>
    {
        #region Propiedades

        RepositorioEstiquetas mRepositorioEstiquetas;

        #endregion

        #region Constructor

        public AdministraEstiquetas()
        {
            mRepositorioEstiquetas = new RepositorioEstiquetas();
            Inicializa(mRepositorioEstiquetas);

        }

        #endregion

        #region IAdministraEstiquetas implementation

        public IEnumerable<Estiquetas> FindItemsStored(EstiquetasDtoBuscar busqueda)
        {
            return mRepositorioEstiquetas.GetPagedElementsStored(busqueda);
        }

        public IEnumerable<Estiquetas> FindPagedStored(EstiquetasDtoBuscar busqueda)
        {
            return mRepositorioEstiquetas.GetPagedElementsStored(busqueda);
        }

        public Estiquetas FindById(int IdEtiqueta)
        {
            Estiquetas entity = new Estiquetas() { IdEtiqueta = (int)IdEtiqueta };
            return mRepositorioEstiquetas.GetById(entity);
        }

        #endregion


    }
}