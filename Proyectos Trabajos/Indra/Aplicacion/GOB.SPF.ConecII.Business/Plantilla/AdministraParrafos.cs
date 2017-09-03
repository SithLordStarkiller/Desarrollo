using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
using GOB.SPF.ConecII.Entities.Plantilla;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business.Plantilla
{
    public partial class AdministraParrafos : AdministraCrudReflection<Parrafos, ParrafosDtoBuscar>
    {
        #region Propiedades

        RepositorioParrafos mRepositorioParrafos;

        #endregion

        #region Constructor

        public AdministraParrafos()
        {
            mRepositorioParrafos = new RepositorioParrafos();
            Inicializa(mRepositorioParrafos);
        }

        #endregion

        #region IAdministraParrafos implementation

        public IEnumerable<Parrafos> FindItemsStored(ParrafosDtoBuscar busqueda)
        {
            return mRepositorioParrafos.GetPagedElementsStored(busqueda);
        }

        public IEnumerable<Parrafos> FindPagedStored(ParrafosDtoBuscar busqueda)
        {
            return mRepositorioParrafos.GetPagedElementsStored(busqueda);
        }

        public Parrafos FindById(int IdParrafo)
        {
            Parrafos entity = new Parrafos() { IdParrafo = (int)IdParrafo };
            return mRepositorioParrafos.GetById(entity);
        }

        #endregion


    }
}