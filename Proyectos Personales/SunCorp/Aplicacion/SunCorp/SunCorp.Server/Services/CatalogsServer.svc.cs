namespace SunCorp.Server.Services
{
    using System.Collections.Generic;
    using BusinessLogic.CatalogsBussinessLogic;
    using Entities.Generic;


    public class CatalogsServer : ICatalogsServer
    {
        public List<GenericTable> GetListCatalogsSystem()
        {
            return new CatalogsBussinessLogic().GetListCatalogsSystem();
        }
    }
}
