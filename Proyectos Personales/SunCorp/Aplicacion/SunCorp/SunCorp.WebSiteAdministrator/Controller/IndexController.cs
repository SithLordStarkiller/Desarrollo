namespace SunCorp.WebSiteAdministrator.Controller
{
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using Entities.Generic;
    using SunCorp.Controller.Entities;

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

            switch (usUsuario.IdEstatusUsuario)
            {
               case 1:
                    System.Web.HttpContext.Current.Session["Usuario"] = "Usuario";
                    System.Web.HttpContext.Current.Session["Sesion"] = "Sesion";

                    Session["Sesion"] = userSession;
                    Session["Usuario"] = usUsuario;

                    return "Correcto";
                case 2:

                    return "La cuenta se encuentra suspendida";
                case 3:

                    return "La cuenta se encuentra cancelada";

            }
            return null;
        }
    }
}