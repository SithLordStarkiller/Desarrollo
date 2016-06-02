namespace SunCorp.WebSiteAdministrator.Controller
{
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using Entities;
    using Entities.Generic;
    using SunCorp.Controller.Catalogs;
    using SunCorp.Controller.Entities;

    public class CatalogsController : AsyncController
    {
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

        // GET: catalogs
        [SessionExpireFilter]
        public async Task<ActionResult> ListCatalogs()
        {
            var catalogsService = new CatalogsEndPointController((UserSession)Session["Sesion"]);
            UpdateBar();
            var listTable = await catalogsService.GetListCatalogsSystem();
            ViewBag.ListCatalogSystem = listTable;
            return View();
        }
    }
}