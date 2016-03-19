namespace SunCorp.WebSiteAdministrator.Controller
{
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using Entities.Generic;
    using SunCorp.Controller.Entities;

    public class IndexController : AsyncController
    {
        // GET: Index
        public async Task<ActionResult> Index()
        {
            var session = new UserSession()
            {
                UrlServer = Properties.Settings.Default.UrlServer
            };

            var servicio = new EntitiesController(session);

            var user = await servicio.GetUsUsuario("usuario", "1234");

            //ViewBag.Titulo = user.Usuario;

            return View();
        }
    }
}