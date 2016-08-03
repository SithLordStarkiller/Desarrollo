namespace SunCorp.WebSiteApplication.Controllers
{
    using System;
    using Newtonsoft.Json;
    using PagedList;
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using Entities;
    using Entities.Generic;
    using Controller.Catalogs;
    using Controller.Entities;
    using System.Collections.Generic;
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
        public ActionResult ListCatalogs()
        {
            var catalogsService = new CatalogsEndPointController((UserSession)Session["Sesion"]);

            UpdateBar();

            var taskListCatalogsUser = catalogsService.GetListCatalogsSystem();
            var taskListCatalogsProducts = catalogsService.GetListCatalogsProducts();

            var tasks = new Task[] { taskListCatalogsUser, taskListCatalogsProducts };

            Task.WaitAll(tasks);

            ViewBag.ListCatalogsUser = ((Task<List<GenericTable>>)tasks[0]).Result;
            ViewBag.ListCatalogsProducts = ((Task<List<GenericTable>>)tasks[1]).Result;

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

                case "ProCatMarca":
                    return new RedirectToReturnUrlResult(() => RedirectToAction("EditCatalogProCatMarca", "Catalogs"));

                case "ProCatModelo":
                    return new RedirectToReturnUrlResult(() => RedirectToAction("EditCatalogProCatModelo", "Catalogs"));

                case "ProDiviciones":
                    return new RedirectToReturnUrlResult(() => RedirectToAction("EditCatalogProDiviciones", "Catalogs"));
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

        #region ProCatMarca

        [SessionExpireFilter]
        public ActionResult EditCatalogProCatMarca(int? page)
        {
            UpdateBar();

            var sesion = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(sesion);

            const int pageSize = 10;
            var pageNumber = (page ?? 1);

            var list = servicio.GetListProCatMarca().Result;
            var listCatMarca = list.ToPagedList(pageNumber, pageSize);

            return View("Tables/ProCatMarca", listCatMarca);
        }

        [SessionExpireFilter]
        public async Task<ActionResult> NewRegProCatMarca(string idMarca, string nombreMarca, string descripcion)
        {
            UpdateBar();

            var session = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(session);

            var obj = new ProCatMarca
            {
                IdMarca = Convert.ToInt32(idMarca),
                NombreMarca = nombreMarca,
                Descripcion = descripcion,
                Creador = session.User,
                FechaCreacion = DateTime.Now,
                Borrado = false
            };

            var newReg = await servicio.NewRegProCatMarca(obj);

            var result = JsonConvert.SerializeObject(newReg);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public async Task<bool> UpdateRegProCatMarca(string idMarca, string nombreMarca, string descripcion, string creador, string fechaCreacion, bool borrado)
        {
            UpdateBar();

            var session = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(session);

            var obj = new ProCatMarca
            {
                IdMarca = Convert.ToInt32(idMarca),
                NombreMarca = nombreMarca,
                Descripcion = descripcion,
                Creador = creador,
                FechaCreacion = Convert.ToDateTime(fechaCreacion),
                Modificado = session.User,
                FechaModificacion = DateTime.Now,
                Borrado = Convert.ToBoolean(borrado)
            };

            var actualizado = await servicio.UpdateRegProCatMarca(obj);

            return actualizado;
        }

        [SessionExpireFilter]
        public async Task<ActionResult> DeleteProCatMarca(string idMarca, string nombreMarca, string descripcion, string creador, string fechaCreacion, bool borrado)
        {
            UpdateBar();

            var session = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(session);

            var obj = new ProCatMarca
            {
                IdMarca = Convert.ToInt32(idMarca),
                NombreMarca = nombreMarca,
                Descripcion = descripcion,
                Creador = creador,
                FechaCreacion = Convert.ToDateTime(fechaCreacion),
                Modificado = session.User,
                FechaModificacion = DateTime.Now,
                Borrado = true
            };

            await servicio.UpdateRegProCatMarca(obj);

            return new RedirectToReturnUrlResult(() => RedirectToAction("EditCatalogProCatMarca", "Catalogs"));
        }

        #endregion

        #region ProCatModelo

        [SessionExpireFilter]
        public ActionResult EditCatalogProCatModelo(int? page, int? marca)
        {
            UpdateBar();

            var sesion = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(sesion);

            const int pageSize = 10;
            var pageNumber = (page ?? 1);

            var listModelo = (List<ProCatModelo>)Session["PrimaryList"];
            var listMarca = (List<ProCatMarca>)Session["CatalogOne"];

            if (listMarca == null && listModelo == null)
            {
                var taskListMarca = servicio.GetListProCatMarca();
                var taskListModelo = servicio.GetListProCatModelo();

                var tasks = new Task[] { taskListMarca, taskListModelo };

                Task.WaitAll(tasks);

                listModelo = ((Task<List<ProCatModelo>>)tasks[1]).Result;
                listMarca = ((Task<List<ProCatMarca>>)tasks[0]).Result;

                listMarca.Add(new ProCatMarca {IdMarca = 0, NombreMarca = "Todos"});

                Session["PrimaryList"] = listModelo;
                Session["CatalogOne"] = listMarca;
            }



            if (marca != null)
            {
                if (marca != 0)
                {
                    listModelo.RemoveAll(r => r.IdMarca != marca);
                    Session["PrimaryList"] = listModelo;
                    pageNumber = 1;
                }
                else if (marca == 0)
                {
                    listModelo = servicio.GetListProCatModelo().Result;
                    Session["PrimaryList"] = listModelo;
                    pageNumber = 1;
                }
            }

            var listCatMarca = listMarca
                .Select(c => new SelectListItem
                {
                    Value = c.IdMarca.ToString(),
                    Text = c.NombreMarca
                }).ToArray();

            ViewBag.ListMarcas = listCatMarca;

            var listCatModelo = listModelo.ToPagedList(pageNumber, pageSize);

            return View("Tables/ProCatModelo", listCatModelo);
        }

        [SessionExpireFilter]
        public async Task<ActionResult> NewRegProCatModelo(string idMarca, string nombreMarca, string descripcion)
        {
            UpdateBar();

            var session = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(session);

            var obj = new ProCatMarca
            {
                IdMarca = Convert.ToInt32(idMarca),
                NombreMarca = nombreMarca,
                Descripcion = descripcion,
                Creador = session.User,
                FechaCreacion = DateTime.Now,
                Borrado = false
            };

            var newReg = await servicio.NewRegProCatMarca(obj);

            var result = JsonConvert.SerializeObject(newReg);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public async Task<bool> UpdateRegProCatModelo(string idMarca, string nombreMarca, string descripcion, string creador, string fechaCreacion, bool borrado)
        {
            UpdateBar();

            var session = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(session);

            var obj = new ProCatMarca
            {
                IdMarca = Convert.ToInt32(idMarca),
                NombreMarca = nombreMarca,
                Descripcion = descripcion,
                Creador = creador,
                FechaCreacion = Convert.ToDateTime(fechaCreacion),
                Modificado = session.User,
                FechaModificacion = DateTime.Now,
                Borrado = Convert.ToBoolean(borrado)
            };

            var actualizado = await servicio.UpdateRegProCatMarca(obj);

            return actualizado;
        }

        [SessionExpireFilter]
        public async Task<ActionResult> DeleteProCatModelo(string idMarca, string nombreMarca, string descripcion, string creador, string fechaCreacion, bool borrado)
        {
            UpdateBar();

            var session = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(session);

            var obj = new ProCatMarca
            {
                IdMarca = Convert.ToInt32(idMarca),
                NombreMarca = nombreMarca,
                Descripcion = descripcion,
                Creador = creador,
                FechaCreacion = Convert.ToDateTime(fechaCreacion),
                Modificado = session.User,
                FechaModificacion = DateTime.Now,
                Borrado = true
            };

            await servicio.UpdateRegProCatMarca(obj);

            return new RedirectToReturnUrlResult(() => RedirectToAction("EditCatalogProCatMarca", "Catalogs"));
        }

        #endregion

        #region ProCatMarca

        [SessionExpireFilter]
        public ActionResult EditCatalogProDiviciones(int? page)
        {
            UpdateBar();

            var sesion = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(sesion);

            const int pageSize = 10;
            var pageNumber = (page ?? 1);

            var list = servicio.GetListProCatDiviciones().Result;
            var listProDiviciones = list.ToPagedList(pageNumber, pageSize);

            return View("Tables/ProDiviciones", listProDiviciones);
        }

        [SessionExpireFilter]
        public async Task<ActionResult> NewRegProDiviciones(string idMarca, string nombreMarca, string descripcion)
        {
            UpdateBar();

            var session = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(session);

            var obj = new ProCatMarca
            {
                IdMarca = Convert.ToInt32(idMarca),
                NombreMarca = nombreMarca,
                Descripcion = descripcion,
                Creador = session.User,
                FechaCreacion = DateTime.Now,
                Borrado = false
            };

            var newReg = await servicio.NewRegProCatMarca(obj);

            var result = JsonConvert.SerializeObject(newReg);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public async Task<bool> UpdateRegProDiviciones(string idMarca, string nombreMarca, string descripcion, string creador, string fechaCreacion, bool borrado)
        {
            UpdateBar();

            var session = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(session);

            var obj = new ProCatMarca
            {
                IdMarca = Convert.ToInt32(idMarca),
                NombreMarca = nombreMarca,
                Descripcion = descripcion,
                Creador = creador,
                FechaCreacion = Convert.ToDateTime(fechaCreacion),
                Modificado = session.User,
                FechaModificacion = DateTime.Now,
                Borrado = Convert.ToBoolean(borrado)
            };

            var actualizado = await servicio.UpdateRegProCatMarca(obj);

            return actualizado;
        }

        [SessionExpireFilter]
        public async Task<ActionResult> DeleteProDiviciones(string idMarca, string nombreMarca, string descripcion, string creador, string fechaCreacion, bool borrado)
        {
            UpdateBar();

            var session = (UserSession)Session["Sesion"];
            var servicio = new EntitiesEndpointController(session);

            var obj = new ProCatMarca
            {
                IdMarca = Convert.ToInt32(idMarca),
                NombreMarca = nombreMarca,
                Descripcion = descripcion,
                Creador = creador,
                FechaCreacion = Convert.ToDateTime(fechaCreacion),
                Modificado = session.User,
                FechaModificacion = DateTime.Now,
                Borrado = true
            };

            await servicio.UpdateRegProCatMarca(obj);

            return new RedirectToReturnUrlResult(() => RedirectToAction("EditCatalogProDiviciones", "Catalogs"));
        }

        #endregion

    }
}