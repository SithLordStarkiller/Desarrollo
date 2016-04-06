namespace SunCorp.WebSiteAdministrator.Controller
{
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using Entities;
    using Entities.Generic;
    using SunCorp.Controller.Entities;

    public class HomeController : AsyncController
    {
        [SessionExpireFilter]
        public async Task<ActionResult> Site()
        {
            var session = (UserSession)Session["Sesion"];
            var user = (UsUsuarios)Session["Usuario"];

            var entitiesServer = new EntitiesEndpointController(session);

            var listZonasUser = await entitiesServer.GetListUsZonasUser(user);

            ViewBag.Session = session;
            ViewBag.User = user;
            ViewBag.ListUser = listZonasUser;

            return View();
        }

        [SessionExpireFilter]
        public async Task<ActionResult> GetTreeMenu()
        {
            var session = (UserSession)Session["Sesion"];
            var user = (UsUsuarios)Session["Usuario"];

            var entitiesServer = new EntitiesEndpointController(session);

            var listZonasUser = await entitiesServer.GetListUsZonasUser(user);

            return null;
        }
    }
}