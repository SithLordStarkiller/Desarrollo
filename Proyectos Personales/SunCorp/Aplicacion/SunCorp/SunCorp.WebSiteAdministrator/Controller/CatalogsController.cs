namespace SunCorp.WebSiteAdministrator.Controller
{
    using System;
    using Newtonsoft.Json;
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using Entities;
    using Entities.Generic;
    using SunCorp.Controller.Catalogs;
    using SunCorp.Controller.Entities;
    using System.Linq;

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

        [SessionExpireFilter]
        public ActionResult EditCatalog(string nombreTabla)
        {
            UpdateBar();
            switch (nombreTabla)
            {
                case "UsZona":
                    return new RedirectToReturnUrlResult(() => RedirectToAction("EditCatalogUsZona", "Catalogs"));
            }
            return null;
        }

        #region UsZona

        [SessionExpireFilter]
        public ActionResult EditCatalogUsZona()
        {
            UpdateBar();

            var sesion = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(sesion);

            var listZonas = servicio.GetListZonas().Result.Where(r => r.Borrado == false).ToList();

            ViewBag.ListCatalogsZonas = listZonas;

            return View("Tables/UsZona");
        }

        [SessionExpireFilter]
        public async Task<ActionResult> NewRegUsZona(string idZona, string nombreZona, string descripcion)
        {
            UpdateBar();

            var session = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(session);

            var obj = new UsZona
            {
                IdZona = Convert.ToInt32(idZona),
                NombreZona = nombreZona,
                Descripcion = descripcion,
                Borrado = false
            };

            var newReg = await servicio.NewRegUsZona(obj);

            var result = JsonConvert.SerializeObject(newReg);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public async Task<bool> UpdateRegUsZonas(string idZona, string nombreZona, string descripcion)
        {
            UpdateBar();

            var sesion = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(sesion);

            var objeto = new UsZona
            {
                IdZona = Convert.ToInt32(idZona),
                NombreZona = nombreZona,
                Descripcion = descripcion,
                Borrado = false
            };

            var actualizado = await servicio.UpdateRegUsZona(objeto);

            return actualizado;
        }

        #endregion


    }
}