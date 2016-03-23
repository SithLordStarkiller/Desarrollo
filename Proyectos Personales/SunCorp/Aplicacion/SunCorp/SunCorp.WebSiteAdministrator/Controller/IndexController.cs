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

        public async Task<UsUsuarios> GetUsUsuario(string loginUser, string password)
        {
            var session = new UserSession()
            {
                UrlServer = Properties.Settings.Default.UrlServer
            };

            var servicio = new EntitiesController(session);

            var user = await servicio.GetUsUsuario(loginUser , password);

            return user;
        }
    }
}