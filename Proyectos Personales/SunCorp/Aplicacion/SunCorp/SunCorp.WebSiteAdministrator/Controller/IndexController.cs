namespace SunCorp.WebSiteAdministrator.Controller
{
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using Entities.Generic;
    using SunCorp.Controller.Entities;
    //using Entities.Entities;

    public class IndexController : AsyncController
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> GetUsUsuario(string user, string password)
        {
            var userSession = new UserSession
            {
                User = user,
                Password = password,
                UrlServer = Properties.Settings.Default.UrlServer
            };

            var servicio = new EntitiesEndpointController(userSession);

            var usUsuario = await servicio.GetUsUsuario(userSession);

            switch (usUsuario.IdTipoUsuario)
            {
               case 1:
                    System.Web.HttpContext.Current.Session["Usuario"] = "Usuario";
                    System.Web.HttpContext.Current.Session["Sesion"] = "Sesion";
                    System.Web.HttpContext.Current.Session["Persona"] = "Persona";
                    Session["Sesion"] = userSession;
                    Session["Usuario"] = usUsuario;

                    return "Correcto";
                case 2:

                    return "La cuenta se encuentra suspendida";
                    //case 3:

                    //    resultado = "La cuenta se encuentra cancelada";

                    //    break;
                
            }
            return null;
        }
    }
}