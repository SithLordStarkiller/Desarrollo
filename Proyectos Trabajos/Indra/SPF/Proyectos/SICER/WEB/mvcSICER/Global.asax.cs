using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace mvcSICER
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }
         /*protected void Session_OnEnd()
        {
            System.Web.HttpContext.Current.Application[(string)System.Web.HttpContext.Current.Session["inicioSesion"]] = null;
            System.Web.HttpContext.Current.Session["inicioSesion"] = null;//Cerramos la sesión activa
        }
        
        protected void Application_End()
        {
            System.Web.HttpContext.Current.Application[(string)System.Web.HttpContext.Current.Session["inicioSesion"]] = null;
            System.Web.HttpContext.Current.Session["inicioSesion"] = null;//Cerramos la sesión activa
        }
        
        protected void Session_End()
        {
            System.Web.HttpContext.Current.Application[(string)System.Web.HttpContext.Current.Session["inicioSesion"]] = null;
            System.Web.HttpContext.Current.Session["inicioSesion"] = null;//Cerramos la sesión activa
        }*/
    }
}