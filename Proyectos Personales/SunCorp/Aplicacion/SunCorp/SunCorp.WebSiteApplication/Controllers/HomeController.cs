namespace SunCorp.WebSiteApplication.Controllers
{
    using System.Web.Mvc;
    using Entities;
    using Entities.Generic;
    using System.Linq;
    using System.Collections.Generic;
    using SunCorp.Controller.Catalogs;
    using SunCorp.Controller.Entities;

    public class HomeController : AsyncController
    {
        private void UpdateBar()
        {
            var entitiesService = new EntitiesEndpointController((UserSession) Session["Sesion"]);

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

            return View("../Index/Index");
        }

        [SessionExpireFilter]
        public ActionResult Site()
        {
            var session = (UserSession)Session["Sesion"];
            var user = (UsUsuarios)Session["Usuario"];

            var entitiesServer = new EntitiesEndpointController(session);

            var listZonasUser = entitiesServer.GetListUsZonasUser(user).Result;

            ViewBag.Session = session;
            ViewBag.User = user;
            ViewBag.ListUser = listZonasUser;

            UpdateBar();

            return View();
        }

        [SessionExpireFilter]
        public List<TreeMenuMvc> GetTreeMenuList(List<SisArbolMenu> listaArbol, int? parentid)
        {
            return (from men in listaArbol
                    where men.IdMenuPadre == parentid
                    select new TreeMenuMvc
                    {
                        IdChild = men.IdMenuPadre,
                        IdParent = men.IdMenuPadre,
                        Name = men.NombreMenu,
                        Controller = men.Controller,
                        Method = men.Method,
                        Url = men.Url,
                        ListChilds = GetTreeMenuList(listaArbol, men.IdMenu)
                    }).ToList();
        }

        [SessionExpireFilter]
        public PartialViewResult GetTreeMenu()
        {
            var sesion = (UserSession)Session["Sesion"];
            var usuario = (UsUsuarios)Session["Usuario"];

            var catalogsService = new CatalogsEndPointController(sesion);

            var listaArbol = catalogsService.GetListMenuForUserType(usuario).Result;

            var lista = GetTreeMenuList(listaArbol, null);
            ViewBag.listaArbol = lista;

            return PartialView("PrintTreeMenu");
        }
    }
}