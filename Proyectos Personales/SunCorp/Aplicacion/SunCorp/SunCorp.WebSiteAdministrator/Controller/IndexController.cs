namespace SunCorp.WebSiteAdministrator.Controller
{
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using Entities.Generic;
    using SunCorp.Controller.Entities;
    using Entities.Entities;

    public class IndexController : AsyncController
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        public async Task<string> GetUsUsuario(UserSession sesion)
        {
            var session = new UserSession()
            {
                UrlServer = Properties.Settings.Default.UrlServer
            };

            var servicio = new EntitiesController(session);

            var user = await servicio.GetUsUsuario(sesion.User , sesion.Password);

            //switch (usuario.ID_ESTATUS_USUARIOS)
            //{
            //    case 1:
                    System.Web.HttpContext.Current.Session["Usuario"] = "Usuario";
                    System.Web.HttpContext.Current.Session["Sesion"] = "Sesion";
                    System.Web.HttpContext.Current.Session["Persona"] = "Persona";
                    Session["Sesion"] = sesion;
                    Session["Usuario"] = user;

                    
                //    break;
                //case 2:

                //    resultado = "La cuenta se encuentra suspendida";

                //    break;
                //case 3:

                //    resultado = "La cuenta se encuentra cancelada";

                //    break;
            //}

            return "Correcto";
        }
    }
}