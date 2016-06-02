using SunCorp.DataAccessSqlServer.CatalogsDataAccess;

namespace SunCorp.BusinessLogic.CatalogsBussinessLogic
{
    using System.Collections.Generic;
    using Entities.Generic;

    public class CatalogsBussinessLogic
    {
        #region CatalogsSystem

        public List<GenericTable> GetListCatalogsSystem()
        {
            return new CatalogsDataAccess().GetListCatalogsSystem();
        }

        #endregion
    }
}
