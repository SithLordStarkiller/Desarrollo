using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
using GOB.SPF.ConecII.Entities.Plantilla;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business.Plantilla
{
    public partial class AdministraInstituciones : AdministraCrudReflection<Instituciones, InstitucionesDtoBuscar>
    {
        #region Propiedades

        RepositorioInstituciones mRepositorioInstituciones;

        #endregion

        #region Constructor

        public AdministraInstituciones()
        {
            mRepositorioInstituciones = new RepositorioInstituciones();
            base.Inicializa(mRepositorioInstituciones);
        }

        #endregion

        #region IAdministraInstituciones implementation

        public IEnumerable<Instituciones> FindItemsStored(InstitucionesDtoBuscar busqueda)
        {
            return mRepositorioInstituciones.GetPagedElementsStored(busqueda);
        }

        public IEnumerable<Instituciones> FindPagedStored(InstitucionesDtoBuscar busqueda)
        {
            return mRepositorioInstituciones.GetPagedElementsStored(busqueda);
        }

        public Instituciones FindById(int IdInstitucion)
        {
            Instituciones entity = new Instituciones() { IdInstitucion = (int)IdInstitucion };
            return mRepositorioInstituciones.GetById(entity);
        }
        #endregion


    }
}