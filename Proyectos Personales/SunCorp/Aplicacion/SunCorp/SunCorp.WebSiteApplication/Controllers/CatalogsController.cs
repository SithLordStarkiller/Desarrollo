using PagedList;

namespace SunCorp.WebSiteApplication.Controllers
{
    using System;
    using Newtonsoft.Json;
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using Entities;
    using Entities.Generic;
    using Controller.Catalogs;
    using Controller.Entities;
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

        #region Controller Catalog

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

        #endregion

        #region UsZona

        [SessionExpireFilter]
        public ActionResult EditCatalogUsZona(int? page)
        {
            UpdateBar();

            var sesion = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(sesion);

            const int pageSize = 2;
            var pageNumber = (page ?? 1);

            var list = servicio.GetListZonas().Result.Where(r => r.Borrado == false).ToList();
            var listZonas = list.ToPagedList(pageNumber, pageSize);

            ViewBag.ListCatalogsZonas = listZonas;

            return View("Tables/UsZona", listZonas);
        }

        [SessionExpireFilter]
        public ActionResult EditCatalogUsZonaPageList(int? page)
        {
            UpdateBar();

            var sesion = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(sesion);
            var totalRows = 10;

            var list = servicio.GetListUsZonaPageList(0, 2, ref totalRows, false).Result;

            ViewBag.ListCatalogsZonas = list;

            const int pageSize = 2;
            var pageNumber = (page ?? 1);
            var listaAux = list.ToPagedList(pageNumber, pageSize);

            return View("Tables/UsZonaPagedList", listaAux);
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

        [SessionExpireFilter]
        public async Task<ActionResult> EliminaCarCatCarreras(string idZona, string nombreZona, string descripcion)
        {
            UpdateBar();

            var sesion = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(sesion);

            var objeto = new UsZona
            {
                IdZona = Convert.ToInt32(idZona),
                NombreZona = nombreZona,
                Descripcion = descripcion,
                Borrado = true
            };

            await servicio.UpdateRegUsZona(objeto);

            return new RedirectToReturnUrlResult(() => RedirectToAction("EditCatalogUsZona", "Catalogs"));
        }

        #endregion


    }
}