namespace SunCorp.WebServerApplication.Services
{
    using System.Collections.Generic;
    using BusinessLogic.CatalogsBussinessLogic;
    using Entities.Generic;
    using Entities;

    public class CatalogsServer : ICatalogsServer
    {
        public List<GenericTable> GetListCatalogsSystem()
        {
            return new CatalogsBussinessLogic().GetListCatalogsSystem();
        }

        public List<SisArbolMenu> GetListMenuForUserType(UsUsuarios user)
        {
            return new CatalogsBussinessLogic().GetListMenuForUserType(user);
        }

        #region ListCatalogProducts

        public List<GenericTable> GetListCatalogsProducts()
        {
            return new CatalogsBussinessLogic().GetListCatalogsProducts();
        }

        #endregion
    }
}
