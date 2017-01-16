using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using PubliPayments.Entidades;
using PubliPayments.Negocios;
using PubliPayments.Utiles;

namespace PubliPayments
{
    public class Global : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
            //routes.MapRoute(
            //    "Dashboard",
            //    "Dashboard",
            //    new { controller = "Dashboard2", action = "Index"});

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = "" });
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
            GlobalFilters.Filters.Add(new ExceptionPublisherExceptionFilter());
            GlobalFilters.Filters.Add(new LoggerTraceFilterAttribute());
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            GlobalFilters.Filters.Add(new HandlerExceptionsAttribute());

            ConexionSql.EstalecerConnectionString(
                ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            ConnectionDB.EstalecerConnectionString("SqlDefault",
                ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            Inicializa.Inicializar(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            var serviciosDatos = new EntUsuariosServicios().ObtenerUsuariosServicios(1);
            var dicServicios = serviciosDatos.ToDictionary(x => x.Usuario, x => x.Password);
            Inicializa.InicializarEmail(dicServicios);
            var app = Config.AplicacionActual();
            if (app == null) return;
            HttpContext.Current.Application["AplicacionLogos"] = "/imagenes/Logos" + app.Nombre + "/";
            HttpContext.Current.Application["Aplicacion"] = app.idAplicacion;
            HttpContext.Current.Application["NombreAplicacion"] = app.Nombre;
            new UsuariosServicios().AgregarConeccionesHistoricasBD();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            string sessionId = Session.SessionID;
            Debug.WriteLine("SessionID=" + sessionId);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var identity = HttpContext.Current.User.Identity as FormsIdentity;
                    if (identity != null)
                    {
                        FormsIdentity id =
                            identity;
                        FormsAuthenticationTicket ticket = id.Ticket;

                        // Get the stored user-data, in this case, our roles
                        string userData = ticket.UserData;
                        var rol = new string[1];

                        rol[0] = userData.Split(',')[1];
                        HttpContext.Current.User = new GenericPrincipal(id, rol);
                    }
                }
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception objErr = Server.GetLastError();
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Application_Error", objErr.Message);
                string err = "Error en " + Config.AplicacionActual().Nombre +
                             "\nError en: " + Request.Url +
                             "\nUsuario: " + SessionUsuario.ObtenerDato(SessionUsuarioDato.Dominio) + "\\" +
                             SessionUsuario.ObtenerDato(SessionUsuarioDato.NombreUsuario) + " - id:" +
                             SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario) +
                             "\nMessage: " + objErr.Message +
                             "\nStack Trace: " + objErr.StackTrace;
                if (objErr.InnerException != null)
                    err += "\nInnerExeption: " + objErr.InnerException.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Application_Error", err);
                if (objErr.GetType().Name != "HttpException" && !objErr.Message.Contains("no existe.") &&
                    !objErr.Message.Contains("was not found or does not implement IController."))
                {
                    Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                        "Excepcion - portal.publipayments.com", err);
                    Response.Redirect("Errores.html");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(DateTime.Now + " - " + ex.Message);
            }
            //EventLog.WriteEntry("Sample_WebApp", err, EventLogEntryType.Error);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            Persistencia.Detener();
        }
    }
}