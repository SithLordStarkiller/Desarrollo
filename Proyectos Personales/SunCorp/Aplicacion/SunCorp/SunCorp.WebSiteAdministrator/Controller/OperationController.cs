namespace SunCorp.WebSiteAdministrator.Controller
{
    using System.Web.Mvc;
    using SunCorp.Controller.Entities;
    using Entities;
    using Entities.Generic;

    public class OperationController : AsyncController
    {
        // GET: Operation
        private void UpdateBar()
        {
            var entitiesService = new EntitiesEndpointController((UserSession)Session["Sesion"]);

            var session = (UserSession)Session["Sesion"];
            var user = (UsUsuarios)Session["Usuario"];
            var typeUser = entitiesService.GetTypeUser(user).Result;
            var listZonas = entitiesService.GetListUsZonasUser(user).Result;

            ViewBag.TypeUser = typeUser;
            ViewBag.User = user;
            ViewBag.Session = session;
            ViewBag.ListZonas = listZonas;
        }

        [SessionExpireFilter]
        public ActionResult CloseSession()
        {
            Session["Sesion"] = null;
            Session["Usuario"] = null;

            return View("../Index/index");
        }

        [SessionExpireFilter]
        public ActionResult ListServiceTickets()
        {
            return View();
        }
    }
}