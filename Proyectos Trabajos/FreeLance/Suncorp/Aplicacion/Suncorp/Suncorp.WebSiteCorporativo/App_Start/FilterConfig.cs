using System.Web;
using System.Web.Mvc;

namespace Suncorp.WebSiteCorporativo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
