namespace SunCorp.BusinessLogic.CatalogsBussinessLogic
{
    using System.Linq;
    using DataAccessSqlServer.CatalogsDataAccess;
    using DataAccessSqlServer.EntitiesDataAccesss;
    using Entities;
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

        #region TreeMenu

        public List<SisArbolMenu> GetListMenuForUserType(UsUsuarios user)
        {
            var listMenus = new CatalogsDataAccess().GetListMenu();
            var listPermissions = new EntitiesAccess().GetListMenusForTypeUser(user);
            
            listMenus = listMenus.Where(r => listPermissions.Any(x => x.IdMenu == r.IdMenu)).ToList();
            
            return listMenus;
        }

        #endregion
    }
}
