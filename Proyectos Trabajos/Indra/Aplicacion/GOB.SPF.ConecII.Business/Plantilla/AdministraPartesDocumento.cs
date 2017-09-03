using GOB.SPF.ConecII.AccessData.Repositories.Plantilla;
using GOB.SPF.ConecII.Entities.Plantilla;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business.Plantilla
{
    public partial class AdministraPartesDocumento : AdministraCrudReflection<PartesDocumento, PartesDocumentoDtoBuscar>
    {
        #region Propiedades

        RepositorioPartesDocumento mRepositorioPartesDocumento;

        #endregion

        #region Constructor

        public AdministraPartesDocumento()
        {
            mRepositorioPartesDocumento = new RepositorioPartesDocumento();
            Inicializa(mRepositorioPartesDocumento);
        }

        #endregion

        #region IAdministraPartesDocumento implementation

        public IEnumerable<PartesDocumento> FindItemsStored(PartesDocumentoDtoBuscar busqueda)
        {
            return mRepositorioPartesDocumento.GetPagedElementsStored(busqueda);
        }

        public IEnumerable<PartesDocumento> FindPagedStored(PartesDocumentoDtoBuscar busqueda)
        {
            return mRepositorioPartesDocumento.GetPagedElementsStored(busqueda);
        }

        public PartesDocumento FindById(int IdParteDocumento)
        {
            PartesDocumento entity = new PartesDocumento() { IdParteDocumento = (int)IdParteDocumento };
            return mRepositorioPartesDocumento.GetById(entity);
        }

        #endregion


    }
}