using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GOB.SPF.ConecII.Web.Models.Seguridad;
using GOB.SPF.ConecII.Web.Servicios;

namespace GOB.SPF.ConecII.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static Cache _cache;
        internal static Cache AppCache =>  HttpRuntime.Cache;
        private static void ActualizarCache(string key, object val, CacheItemRemovedReason reason)
        {
            Task.Run(ActualizarModulos);
        }
        private static async Task ActualizarModulos()
        {
            try
            {
                var modulos = await ServiceClient.GetObjectAsync<List<UiModulo>>("Seguridad", "ModulosObtenerRolesAutorizados");
                AppCache.Add("Modulos", modulos, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration,
                    CacheItemPriority.High, ActualizarCache);
            }
            catch(Exception ex)
            {
                var modulos =new List<UiModulo>();
                AppCache.Add("Modulos", modulos, null, DateTime.Now.AddSeconds(10), Cache.NoSlidingExpiration,
                    CacheItemPriority.High, ActualizarCache);
            }
        }

        public static List<UiModulo> Modulos => AppCache.Get("Modulos") as List<UiModulo>;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Task.WaitAll(ActualizarModulos());
        }
    }
}
