using System;
using System.Collections.Generic;
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
        internal static Cache AppCache => HttpRuntime.Cache;
        private static void ActualizarCache(string key, CacheItemUpdateReason reason, out object val, out CacheDependency dependency,
            out DateTime absoluteExpiration, out TimeSpan slidingExpiration)
        {
            var modulos = ObtenerModulosAsync().GetAwaiter().GetResult();
            if (modulos == null)
            {
                absoluteExpiration = DateTime.Now.AddSeconds(10);
                val = new List<UiModulo>();
            }
            else
            {
                absoluteExpiration = DateTime.Now.AddMinutes(1);
                val = modulos;
            }

            dependency = null;
            slidingExpiration = Cache.NoSlidingExpiration;
        }
        private static async Task<List<UiModulo>> ObtenerModulosAsync()
        {
            var modulos = await ServiceClient.GetObjectAsync<List<UiModulo>>("Seguridad", "ModulosObtenerRolesAutorizados");
            return modulos ?? new List<UiModulo>();
        }

        private static async Task InicializarModulos()
        {
            var modulos = await ServiceClient.GetObjectAsync<List<UiModulo>>("Seguridad", "ModulosObtenerRolesAutorizados");
            if (modulos == null)
            {
                AppCache.Insert("Modulos", new List<UiModulo>(), null, DateTime.Now.AddSeconds(10),
                    Cache.NoSlidingExpiration, ActualizarCache);
            }
            else
            {
                AppCache.Insert("Modulos", modulos, null, DateTime.Now.AddMinutes(1),
                    Cache.NoSlidingExpiration, ActualizarCache);
            }
        }

        public static List<UiModulo> Modulos => AppCache.Get("Modulos") as List<UiModulo>;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Task.WaitAll(InicializarModulos());
        }
    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Caching;
//using System.Web.Mvc;
//using System.Web.Optimization;
//using System.Web.Routing;
//using GOB.SPF.ConecII.Web.Models.Seguridad;
//using GOB.SPF.ConecII.Web.Servicios;

//namespace GOB.SPF.ConecII.Web
//{
//    public class MvcApplication : System.Web.HttpApplication
//    {
//        private static Cache _cache;
//        internal static Cache AppCache =>  HttpRuntime.Cache;
//        private static void ActualizarCache(string key, object val, CacheItemRemovedReason reason)
//        {
//            Task.Run(ActualizarModulos);
//        }
//        private static async Task ActualizarModulos()
//        {
//            try
//            {
//                var modulos = await ServiceClient.GetObjectAsync<List<UiModulo>>("Seguridad", "ModulosObtenerRolesAutorizados");
//                AppCache.Add("Modulos", modulos, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration,
//                    CacheItemPriority.High, ActualizarCache);
//            }
//            catch(Exception ex)
//            {
//                var modulos =new List<UiModulo>();
//                AppCache.Add("Modulos", modulos, null, DateTime.Now.AddSeconds(10), Cache.NoSlidingExpiration,
//                    CacheItemPriority.High, ActualizarCache);
//            }
//        }

//        public static List<UiModulo> Modulos => AppCache.Get("Modulos") as List<UiModulo>;

//        protected void Application_Start()
//        {
//            AreaRegistration.RegisterAllAreas();
//            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
//            RouteConfig.RegisterRoutes(RouteTable.Routes);
//            BundleConfig.RegisterBundles(BundleTable.Bundles);
//            Task.WaitAll(ActualizarModulos());
//        }
//    }
//}
