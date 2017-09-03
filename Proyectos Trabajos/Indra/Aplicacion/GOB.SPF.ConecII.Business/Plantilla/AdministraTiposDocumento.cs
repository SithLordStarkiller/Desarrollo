using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
using GOB.SPF.ConecII.Entities.Plantilla;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business.Plantilla
{
    public partial class AdministraTiposDocumento : AdministraCrudReflection<TiposDocumento, TiposDocumentoDtoBuscar>
    {
        #region Propiedades

        RepositorioTiposDocumento mRepositorioTiposDocumento;

        #endregion

        #region Constructor

        public AdministraTiposDocumento()
        {
            mRepositorioTiposDocumento = new RepositorioTiposDocumento();
            Inicializa(mRepositorioTiposDocumento);
        }

        #endregion

        #region IAdministraTiposDocumento implementation

        public IEnumerable<TiposDocumento> FindItemsStored(TiposDocumentoDtoBuscar busqueda)
        {
            return mRepositorioTiposDocumento.GetPagedElementsStored(busqueda);
        }

        public IEnumerable<TiposDocumento> FindPagedStored(TiposDocumentoDtoBuscar busqueda)
        {
            return mRepositorioTiposDocumento.GetPagedElementsStored(busqueda);
        }

        public TiposDocumento FindById(int IdTipoDocumento)
        {
            TiposDocumento entity = new TiposDocumento() { IdTipoDocumento = (int)IdTipoDocumento };
            return mRepositorioTiposDocumento.GetById(entity);
        }

        #endregion


    }
}