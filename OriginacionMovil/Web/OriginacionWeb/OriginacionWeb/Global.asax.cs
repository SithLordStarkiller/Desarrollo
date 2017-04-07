
namespace OriginacionWeb
{
    using System.Configuration;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using PubliPayments.Entidades;
    using PubliPayments.Utiles;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            ConnectionDB.EstalecerConnectionString("SqlDefault",
                ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            Inicializa.Inicializar(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);


            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
