namespace GOB.SPF.ConecII.Web.Controllers
{
    using Models;
    using Newtonsoft.Json;
    using Resources;
    using Servicios;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class CatalogoController : Controller
    {

        /// <summary>
        /// AUTOCOMPLETE'S DANIEL GIL
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>

        #region AUTOCOMPLETE

        public JsonResult ObtenerDivision(string term)
        {
            UiResultPage<UiDivision> DivList = new UiResultPage<UiDivision>();

            ServicesCatalog clientService = new ServicesCatalog();

            DivList.List = clientService.ObtenerDivisiones(1, 20);

            var queryD = DivList.List.Where(x => x.Name.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Identificador, value = x.Name }).OrderBy(x => x.value).ToList();

            return Json(queryD, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerGrupos(string term)
        {
            UiResultPage<UiGrupo> GruList = new UiResultPage<UiGrupo>();

            ServicesCatalog clientService = new ServicesCatalog();

            GruList.List = clientService.GrupoObtener(1, 20);

            var queryG = GruList.List.Where(x => x.Name.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Identificador, value = x.Name }).OrderBy(x => x.value).ToList();

            return Json(queryG, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerTipoServicio(string term)
        {
            UiResultPage<UiTiposServicio> TSList = new UiResultPage<UiTiposServicio>();

            ServicesCatalog clientService = new ServicesCatalog();

            TSList.List = clientService.ObtenerTiposServicio(1, 20);

            var queryT = TSList.List.Where(x => x.Name.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Identificador, value = x.Name }).OrderBy(x => x.value).ToList();

            return Json(queryT, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerClasificaciones(string term)
        {
            UiResultPage<UiClasificacionFactor> ClFList = new UiResultPage<UiClasificacionFactor>();

            ServicesCatalog clientService = new ServicesCatalog();

            ClFList.List = clientService.ObtenerClasificacionFactor(1, 20);

            var queryC = ClFList.List.Where(x => x.Nombre.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Identificador, value = x.Nombre }).OrderBy(x => x.value).ToList();

            return Json(queryC, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtieneFactores(string term)
        {
            UiResultPage<UiFactor> FacList = new UiResultPage<UiFactor>();

            ServicesCatalog clientService = new ServicesCatalog();

            FacList.List = clientService.ObtenerFactor(1, 20);

            var queryF = FacList.List.Where(x => x.Nombre.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Identificador, value = x.Nombre }).OrderBy(x => x.value).ToList();

            return Json(queryF, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerAnios(string term)
        {
            UiResultPage<UiAnio> AnioList = new UiResultPage<UiAnio>();

            ServicesCatalog clientService = new ServicesCatalog();

            AnioList.List = clientService.ObtenerAnio(1, 20);

            var queryA = AnioList.List.Where(x => x.Identificador.ToString().Contains(term)).Select(x => new { id = x.Identificador, value = x.Identificador }).OrderBy(x => x.value).ToList();

            return Json(queryA, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtieneDependencia(string term)
        {
            UiResultPage<UiDependencias> DepList = new UiResultPage<UiDependencias>();

            ServicesCatalog clientService = new ServicesCatalog();

            DepList.List = clientService.ObtenerDependencias(1, 20);

            var queryD = DepList.List.Where(x => x.Name.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Identificador, value = x.Name }).OrderBy(x => x.value).ToList();

            return Json(queryD, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerTipoDocumento(string term)
        {
            UiResultPage<UiTiposDocumento> TDList = new UiResultPage<UiTiposDocumento>();

            ServicesCatalog clientService = new ServicesCatalog();

            TDList.List = clientService.ObtenerTiposDocumento(1, 20);

            var queryTD = TDList.List.Where(x => x.Name.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Identificador, value = x.Name }).OrderBy(x => x.value).ToList();

            return Json(queryTD, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerPeriodo(string term)
        {
            UiResultPage<UiPeriodo> PerList = new UiResultPage<UiPeriodo>();

            ServicesCatalog clientService = new ServicesCatalog();

            PerList.List = clientService.ObtenerPeriodos(1, 20);

            var queryD = PerList.List.Where(x => x.Name.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Identificador, value = x.Name }).OrderBy(x => x.value).ToList();

            return Json(queryD, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerEstados(string term)
        {
            UiResultPage<UiEstado> EstList = new UiResultPage<UiEstado>();

            ServicesCatalog clientService = new ServicesCatalog();

            EstList.List = clientService.ObtenerEstado(1, 20);

            var queryE = EstList.List.Where(x => x.Nombre.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Identificador, value = x.Nombre }).OrderBy(x => x.value).ToList();

            return Json(queryE, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerReferencias(string term)
        {
            UiResultPage<UiReferencia> PerList = new UiResultPage<UiReferencia>();

            ServicesCatalog clientService = new ServicesCatalog();

            PerList.List = clientService.ObtenerReferencias(1, 20);

            var queryD = PerList.List.Where(x => x.ClaveReferencia.ToString().Contains(term.ToLower())).Select(x => new { id = x.Identificador, value = x.ClaveReferencia }).OrderBy(x => x.value).ToList();

            //return Json(queryD, JsonRequestBehavior.AllowGet);
            return Json(queryD, JsonRequestBehavior.AllowGet);

        }
        public JsonResult ObtenerGastosInherentes(string term)
        {
            UiResultPage<UiGastosInherente> PerList = new UiResultPage<UiGastosInherente>();

            ServicesCatalog clientService = new ServicesCatalog();

            PerList.List = clientService.ObtenerGastosInherentes(1, 20);

            var queryD = PerList.List.Where(x => x.Name.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Identificador, value = x.Name }).OrderBy(x => x.value).ToList();

            return Json(queryD, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerCuotasConcepto(string term)
        {
            UiResultPage<UiCuota> PerList = new UiResultPage<UiCuota>();

            ServicesCatalog clientService = new ServicesCatalog();

            PerList.List = clientService.ObtenerCuotas(1, 20);

            var queryD = PerList.List.Where(x => x.Concepto.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Concepto, value = x.Concepto }).OrderBy(x => x.value).ToList();

            return Json(queryD, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerCuotasFechaEntradaVigor(string term)
        {
            UiResultPage<UiCuota> PerList = new UiResultPage<UiCuota>();

            ServicesCatalog clientService = new ServicesCatalog();

            PerList.List = clientService.CuotaObtenerAnos(1, 20);

            var queryD = PerList.List.Where(x => x.Ano.ToString().Contains(term.ToLower())).Select(x => new { id = x.Ano, value = x.Ano }).OrderBy(x => x.value).ToList();

            return Json(queryD, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerCuotasEsProducto(string term)
        {


            UiResultPage<UiCuota> PerList = new UiResultPage<UiCuota>();

            ServicesCatalog clientService = new ServicesCatalog();

            PerList.List = clientService.ObtenerCuotas(1, 20);

            var queryD = new[] { new { id = true, value = "Productos" }, new { id = false, value = "Aprovechamientos" } };
            //var queryD = PerList.List.Where(x => x.TipoProducto.ToString().Contains(term.ToLower())).Select(x => new { id = x.TipoProducto, value = x.TipoProducto }).OrderBy(x => x.value).ToList();


            return Json(queryD, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerArea(string term)
        {

            ServicesCatalog clientService = new ServicesCatalog();

            var listArea = clientService.ObtenerAreas(0, 100000)
                .Where(f => f.Name.ToUpper().Contains(term.ToUpper()))
                .Select(f => new { id = f.Identificador, value = f.Name });

            return Json(listArea, JsonRequestBehavior.AllowGet);
        }
        #endregion        

        // GET: Catalogo
        #region ROLESMODULOSCONTROL
        public ActionResult RolModuloControl()
        {
            return View();
        }

        public JsonResult RolModuloControlConsulta(int page, int rows)
        {
            UiResultPage<UiRolModuloControl> uiResult = new UiResultPage<UiRolModuloControl>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerRolesModulosControl(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RolesModulosControlObtenerListado()
        {
            UiResultPage<UiRolModuloControl> uiResult = new UiResultPage<UiRolModuloControl>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerRolesModulosControlListado();
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RolesmodulosControlConsultaCriterio(UiResultPage<UiRolModuloControl> model)
        {
            UiResultPage<UiRolModuloControl> uiResult = new UiResultPage<UiRolModuloControl>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.RolesModulosControlObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> RolModuloControl(UiRolModuloControl model)
        {
            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Rol Modulo Control";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Rol Modulo Control";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle de Rol Modulo Control";
                    break;
            }
            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult RolModuloControlGuardar(UiResultPage<UiRolModuloControl> model)
        {
            UiResultPage<UiRolModuloControl> uiResult = new UiResultPage<UiRolModuloControl>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveRolesModulosControl(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ObtenerRolesModulosControl(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult RolModuloControlCambiarEstatus(UiResultPage<UiRolModuloControl> model)
        {
            UiResultPage<UiRolModuloControl> uiResult = new UiResultPage<UiRolModuloControl>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.RolesModulosControlCambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.RolesModulosControlObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        #endregion ROLESMODULOSCONTROL

        #region CONTROLES
        public ActionResult Control()
        {
            return View();
        }

        public JsonResult ControlesConsulta(int page, int rows)
        {
            UiResultPage<UiControl> uiResult = new UiResultPage<UiControl>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerControles(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ControlesObtenerListado()
        {
            UiResultPage<UiControl> uiResult = new UiResultPage<UiControl>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerControlesListado();
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ControlesConsultaCriterio(UiResultPage<UiControl> model)
        {
            UiResultPage<UiControl> uiResult = new UiResultPage<UiControl>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ControlesObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> Control(UiControl model)
        {
            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Control";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Control";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle de Control";
                    break;
            }
            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ControlGuardar(UiResultPage<UiControl> model)
        {
            UiResultPage<UiControl> uiResult = new UiResultPage<UiControl>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveControles(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ObtenerControles(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ControlCambiarEstatus(UiResultPage<UiControl> model)
        {
            UiResultPage<UiControl> uiResult = new UiResultPage<UiControl>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.ControlesCambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ControlesObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        #endregion CONTROLES

        #region ROLESMODULOS
        public ActionResult RolModulo()
        {
            return View();
        }

        public JsonResult RolesModulosConsulta(int page, int rows)
        {
            UiResultPage<UiRolModulo> uiResult = new UiResultPage<UiRolModulo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerRolesModulos(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RolesModulosObtenerListado()
        {
            UiResultPage<UiRolModulo> uiResult = new UiResultPage<UiRolModulo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerRolesModulosListado();
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RolesModulosConsultaCriterio(UiResultPage<UiRolModulo> model)
        {
            UiResultPage<UiRolModulo> uiResult = new UiResultPage<UiRolModulo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.RolesModulosObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> RolModulo(UiRolModulo model)
        {
            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Rol Módulo";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Rol Módulo";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle de Rol Módulo";
                    break;
            }
            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult RolModuloGuardar(UiResultPage<UiRolModulo> model)
        {
            UiResultPage<UiRolModulo> uiResult = new UiResultPage<UiRolModulo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveRolesModulos(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ObtenerRolesModulos(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult RolModuloCambiarEstatus(UiResultPage<UiRolModulo> model)
        {
            UiResultPage<UiRolModulo> uiResult = new UiResultPage<UiRolModulo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.RolesModulosCambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.RolesModulosObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        #endregion ROLESMODULOS

        #region ROLESUSUARIOS
        public ActionResult RolUsuario()
        {
            return View();
        }

        public JsonResult RolesUsuariosConsulta(int page, int rows)
        {
            UiResultPage<UiRolUsuario> uiResult = new UiResultPage<UiRolUsuario>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerRolesUsuarios(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RolesUsuariosObtenerListado()
        {
            UiResultPage<UiRolUsuario> uiResult = new UiResultPage<UiRolUsuario>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerRolesUsuariosListado();
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RolesUsuariosConsultaCriterio(UiResultPage<UiRolUsuario> model)
        {
            UiResultPage<UiRolUsuario> uiResult = new UiResultPage<UiRolUsuario>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.RolesUsuariosObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> RolUsuario(UiUsuario model)
        {
            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Rol Usuario";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Rol Usuario";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle de Rol Usuario";
                    break;
            }
            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult RolUsuarioGuardar(UiResultPage<UiRolUsuario> model)
        {
            UiResultPage<UiRolUsuario> uiResult = new UiResultPage<UiRolUsuario>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveRolesUsuarios(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ObtenerRolesUsuarios(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult RolUsuarioCambiarEstatus(UiResultPage<UiRolUsuario> model)
        {
            UiResultPage<UiRolUsuario> uiResult = new UiResultPage<UiRolUsuario>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.RolesUsuariosCambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.RolesUsuariosObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        #endregion ROLESUSUARIOS

        #region USUARIOS
        public ActionResult Usuarios()
        {
            return View();
        }

        public JsonResult UsuariosConsulta(int page, int rows)
        {
            UiResultPage<UiUsuario> uiResult = new UiResultPage<UiUsuario>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerUsuarios(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UsuariosObtenerListado()
        {
            UiResultPage<UiUsuario> uiResult = new UiResultPage<UiUsuario>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerUsuariosListado();
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UsuariosConsultaCriterio(UiResultPage<UiUsuario> model)
        {
            UiResultPage<UiUsuario> uiResult = new UiResultPage<UiUsuario>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.UsuariosObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> Usuario(UiUsuario model)
        {
            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Usuario";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Usuario";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle de Usuario";
                    break;
            }
            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult UsuarioGuardar(UiResultPage<UiUsuario> model)
        {
            UiResultPage<UiUsuario> uiResult = new UiResultPage<UiUsuario>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveUsuarios(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ObtenerUsuarios(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult UsuarioCambiarEstatus(UiResultPage<UiUsuario> model)
        {
            UiResultPage<UiUsuario> uiResult = new UiResultPage<UiUsuario>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.UsuariosCambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.UsuariosObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        #endregion USUARIOS

        #region ROL
        public ActionResult Roles()
        {
            return View();
        }

        public JsonResult RolesConsulta(int page, int rows)
        {
            UiResultPage<UiRol> uiResult = new UiResultPage<UiRol>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerRoles(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RolesObtenerListado()
        {
            UiResultPage<UiRol> uiResult = new UiResultPage<UiRol>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerRolesListado();
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RolesConsultaCriterio(UiResultPage<UiRol> model)
        {
            UiResultPage<UiRol> uiResult = new UiResultPage<UiRol>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.RolesObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> Rol(UiRol model)
        {
            UiResultPage<UiRol> ls = new UiResultPage<UiRol>();
            //UiResultPage<UiArea> lsAreas = new UiResultPage<UiArea>();
            List<UiRol> list = new List<UiRol>();
            //List<UiArea> lsArea = new List<UiArea>();
            //List<UiZona> lsZona = new List<UiZona>();

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Rol";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Rol";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle del Rol";
                    break;
            }

            ServicesCatalog clientService = new ServicesCatalog();

            ls.List = clientService.ObtenerRoles(1, 20);

            var IdentificadorSubRol = model.IdentificadorSubRol;

            var queryC = ls.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            ViewBag.Roles = new SelectList(queryC, "Identificador", "Name", IdentificadorSubRol);

            //lsArea = clientService.ObtenerAreas(1, 20);
            //var IdArea = model.IdArea;

            //var queryArea = lsArea.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            //ViewBag.Areas = new SelectList(queryArea, "Identificador", "Name", IdArea);

            //lsZona = clientService.ObtenerZonas(1,20);
            //var IdZona = model.IdZona;

            //var queryZona = lsZona.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            //ViewBag.Zonas = new SelectList(queryZona, "Identificador", "Name", IdZona);

            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult RolGuardar(UiResultPage<UiRol> model)
        {
            UiResultPage<UiRol> uiResult = new UiResultPage<UiRol>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveRoles(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ObtenerRoles(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult RolCambiarEstatus(UiResultPage<UiRol> model)
        {
            UiResultPage<UiRol> uiResult = new UiResultPage<UiRol>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.RolCambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.RolesObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        #endregion ROLES

        #region MÓDULOS
        public ActionResult Modulos()
        {
            return View();
        }

        public JsonResult ModulosConsulta(int page, int rows)
        {
            UiResultPage<UiModulo> uiResult = new UiResultPage<UiModulo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerModulos(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModulosObtenerListado()
        {
            UiResultPage<UiModulo> uiResult = new UiResultPage<UiModulo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerModulosListado();
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModulosConsultaCriterio(UiResultPage<UiModulo> model)
        {
            UiResultPage<UiModulo> uiResult = new UiResultPage<UiModulo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ModulosObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> Modulo(UiModulo model)
        {
            UiResultPage<UiModulo> ls = new UiResultPage<UiModulo>();

            List<UiGrupo> list = new List<UiGrupo>();

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Módulo";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Módulo";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle del Módulo";
                    break;
            }

            ServicesCatalog clientService = new ServicesCatalog();

            ls.List = clientService.ObtenerModulos(1, 20);

            var IdPadre = model.IdPadre;



            var queryC = ls.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            ViewBag.Modulos = new SelectList(queryC, "Identificador", "Name", IdPadre);
            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ModuloGuardar(UiResultPage<UiModulo> model)
        {
            UiResultPage<UiModulo> uiResult = new UiResultPage<UiModulo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveModulos(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ObtenerModulos(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ModuloCambiarEstatus(UiResultPage<UiModulo> model)
        {
            UiResultPage<UiModulo> uiResult = new UiResultPage<UiModulo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.ModulosCambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ModulosObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        #endregion MÓDULOS

        #region DIVISIONES
        public ActionResult Divisiones()
        {
            UiResultPage<UiDivision> DivList = new UiResultPage<UiDivision>();

            ServicesCatalog clientService = new ServicesCatalog();

            DivList.List = clientService.ObtenerDivisiones(1, 20);

            var queryF = DivList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            ViewBag.Divisiones = new SelectList(queryF, "Identificador", "Name");

            return View();
        }

        public JsonResult DivisionesConsulta(int page, int rows)
        {
            UiResultPage<UiDivision> uiResult = new UiResultPage<UiDivision>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerDivisiones(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DivisionesObtenerListado()
        {
            UiResultPage<UiDivision> uiResult = new UiResultPage<UiDivision>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerDivisionesListado();
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DivisionesConsultaCriterio(UiResultPage<UiDivision> model)
        {
            UiResultPage<UiDivision> uiResult = new UiResultPage<UiDivision>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.DivisionesObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> Division(UiDivision model)
        {
            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear División";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar División";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle de la División";
                    break;
            }
            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult DivisionGuardar(UiResultPage<UiDivision> model)
        {
            UiResultPage<UiDivision> uiResult = new UiResultPage<UiDivision>();
            uiResult.Result = UiEnum.TransactionResult.Failed;
            string mesaje = string.Empty;
            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveDivisiones(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador);
                uiResult.List = clientService.ObtenerDivisiones(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult DivisionCambiarEstatus(UiResultPage<UiDivision> model)
        {
            UiResultPage<UiDivision> uiResult = new UiResultPage<UiDivision>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.DivisionCambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador,
                                                                                                                        model.ObjectResult.IsActive);
                uiResult.List = clientService.DivisionesObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        #endregion

        #region TIPOS_DOCUMENTO
        public ActionResult TiposDocumento()
        {
            //UiResultPage<UiTiposDocumento> ls = new UiResultPage<UiTiposDocumento>();

            //ServicesCatalog clientService = new ServicesCatalog();
            //ls.List = clientService.ObtenerTiposDocumento(1, 20);

            //var queryF = ls.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            //ViewBag.TiposDocumentos = new SelectList(queryF, "Identificador", "Name");

            return View();
        }

        public JsonResult TipoDocumentoConsulta(int page, int rows)
        {
            UiResultPage<UiTiposDocumento> uiResult = new UiResultPage<UiTiposDocumento>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerTiposDocumento(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TipoDocumentoConsultaCriterio(UiResultPage<UiTiposDocumento> model)
        {
            UiResultPage<UiTiposDocumento> uiResult = new UiResultPage<UiTiposDocumento>();
            uiResult.Result = UiEnum.TransactionResult.Failed;
            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.TipoDocumentoObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> TipoDocumento(UiTiposDocumento model)
        {
            UiResultPage<UiActividad> actList = new UiResultPage<UiActividad>();

            ServicesCatalog clientService = new ServicesCatalog();

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Tipo Documento";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Tipo Documento";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle Tipo Documento";
                    break;
            }


            actList.List = clientService.ObtenerActividades(1, 20);


            var idAc = model.IdActividad;

            var query = actList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            ViewBag.IdActividad = new SelectList(query, "Identificador", "Name", idAc);

            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult TipoDocumentoGuardar(UiResultPage<UiTiposDocumento> model)
        {
            UiResultPage<UiTiposDocumento> uiResult = new UiResultPage<UiTiposDocumento>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveTipoDocumento(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador);
                uiResult.List = clientService.ObtenerTiposDocumento(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult TipoDocumentoCambiarEstatus(UiResultPage<UiTiposDocumento> model)
        {
            UiResultPage<UiTiposDocumento> uiResult = new UiResultPage<UiTiposDocumento>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.TipoDocumentoCambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador,
                                                                                                                        model.ObjectResult.IsActive);
                uiResult.List = clientService.TipoDocumentoObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            return Json(uiResult);
        }

        public JsonResult GrupoConsultaPorIdDivision(int id)
        {
            UiResultPage<UiGrupo> uiResult = new UiResultPage<UiGrupo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                UiFracciones fraccion = new UiFracciones();
                fraccion.IdDivision = id;
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.GrupoObtenerPorIdDivisionFraccion(fraccion);

                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }
        #endregion

        // Daniel Gil

        #region TIPOS_SERVICIO

        public ActionResult TiposServicios()
        {
            List<UiTiposServicio> Servicios = new List<UiTiposServicio>();

            ServicesCatalog clientService = new ServicesCatalog();

            Servicios = clientService.ObtenerTodosTiposServicio(1, 10);

            var query = Servicios.OrderBy(x => x.Name).ToList();
            ViewBag.Servicios = new SelectList(query, "Identificador", "Name");
            return View();
        }

        public JsonResult TiposServicioConsulta(int page, int rows)
        {
            UiResultPage<UiTiposServicio> uiResult = new UiResultPage<UiTiposServicio>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerTiposServicio(1, 10);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TiposServicioConsultaCriterio(UiResultPage<UiTiposServicio> model)
        {
            UiResultPage<UiTiposServicio> uiResult = new UiResultPage<UiTiposServicio>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.TiposServicioObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> TiposServicio(UiTiposServicio model)
        {

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Tipo Servicio";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Tipo Servicio";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle del Tipo Servicio";
                    break;
            }
            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult TiposServicioGuardar(UiResultPage<UiTiposServicio> model)
        {
            UiResultPage<UiTiposServicio> uiResult = new UiResultPage<UiTiposServicio>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveTiposServicio(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                       uiResult.Result,
                                                                                                                       model.ObjectResult.Identificador);
                uiResult.List = clientService.ObtenerTiposServicio(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult TiposServicioCambiarEstatus(UiResultPage<UiTiposServicio> model)
        {
            UiResultPage<UiTiposServicio> uiResult = new UiResultPage<UiTiposServicio>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.CambiarEstatusTiposServicio(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                       uiResult.Result,
                                                                                                                       model.ObjectResult.Identificador,
                                                                                                                       model.ObjectResult.IsActive);
                uiResult.List = clientService.TiposServicioObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                //uiResult.List = clientService.ObtenerTiposServicio(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        #endregion

        #region GRUPOS 
        public ActionResult Grupos()
        {
            UiResultPage<UiDivision> DivList = new UiResultPage<UiDivision>();
            UiResultPage<UiGrupo> GruList = new UiResultPage<UiGrupo>();

            ServicesCatalog clientService = new ServicesCatalog();

            DivList.List = clientService.ObtenerDivisiones(1, 20);

            GruList.List = clientService.GrupoObtener(1, 20);

            var queryD = DivList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            ViewBag.Divisiones = new SelectList(queryD, "Identificador", "Name");

            var query = GruList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            ViewBag.Grupos = new SelectList(query, "Identificador", "Name");

            return View();
        }

        public JsonResult GrupoConsulta(int page, int rows)
        {
            UiResultPage<UiGrupo> uiResult = new UiResultPage<UiGrupo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.GrupoObtener(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GrupoConsultaCriterio(UiResultPage<UiGrupo> model)
        {
            UiResultPage<UiGrupo> uiResult = new UiResultPage<UiGrupo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.GrupoObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> Grupo(UiGrupo model)
        {
            UiResultPage<UiDivision> DivList = new UiResultPage<UiDivision>();

            List<UiGrupo> list = new List<UiGrupo>();
            try
            {
                switch (model.Action)
                {
                    case UiEnumEntity.New:
                        ViewBag.Title = "Crear grupo";
                        break;
                    case UiEnumEntity.Edit:
                        ViewBag.Title = "Modificar grupo";
                        break;
                    case UiEnumEntity.View:
                        ViewBag.Title = "Ver grupo";
                        break;
                }


                ServicesCatalog clientService = new ServicesCatalog();

                DivList.List = clientService.ObtenerDivisiones(1, 20);

                var idDiv = model.IdDivision;

                var queryC = DivList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
                ViewBag.Divisiones = new SelectList(queryC, "Identificador", "Name", idDiv);

                return PartialView(model);
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }

            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GrupoGuardar(UiResultPage<UiGrupo> model)
        {
            UiResultPage<UiGrupo> uiResult = new UiResultPage<UiGrupo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.GrupoGuardar(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador);
                uiResult.List = clientService.GrupoObtener(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GrupoCambiarEstatus(UiResultPage<UiGrupo> model)
        {
            UiResultPage<UiGrupo> uiResult = new UiResultPage<UiGrupo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.GrupoActualizar(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador,
                                                                                                                        model.ObjectResult.IsActive);
                uiResult.List = clientService.GrupoObtener(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }


        #endregion

        #region CLASIFICACIÓN_FACTOR

        public ActionResult ClasificacionesFactor()
        {
            UiResultPage<UiClasificacionFactor> DivList = new UiResultPage<UiClasificacionFactor>();

            ServicesCatalog clientService = new ServicesCatalog();

            DivList.List = clientService.ObtenerClasificacionFactor(1, 20);

            var query = DivList.List.Select(x => new { x.Identificador, x.Nombre }).OrderBy(x => x.Nombre).ToList();
            ViewBag.Clasificaciones = new SelectList(query, "Identificador", "Nombre");

            return View();
        }

        public JsonResult ClasificacionFactorConsulta(int page, int rows)
        {
            UiResultPage<UiClasificacionFactor> uiResult = new UiResultPage<UiClasificacionFactor>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerClasificacionFactor(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClasificacionFactorConsultaCriterio(UiResultPage<UiClasificacionFactor> model)
        {
            UiResultPage<UiClasificacionFactor> uiResult = new UiResultPage<UiClasificacionFactor>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ClasificacionFactorObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> ClasificacionFactor(UiClasificacionFactor model)
        {
            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Clasificación Factor";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Clasificación Factor";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle de la Clasificación Factor";
                    break;
            }
            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ClasificacionFactorGuardar(UiResultPage<UiClasificacionFactor> model)
        {
            UiResultPage<UiClasificacionFactor> uiResult = new UiResultPage<UiClasificacionFactor>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveClasificacionFactor(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador);
                uiResult.List = clientService.ObtenerClasificacionFactor(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ClasificacionFactorCambiarEstatus(UiResultPage<UiClasificacionFactor> model)
        {
            UiResultPage<UiClasificacionFactor> uiResult = new UiResultPage<UiClasificacionFactor>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.CambiarEstatusClasificacionFactor(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador,
                                                                                                                        model.ObjectResult.IsActive);
                uiResult.List = clientService.ClasificacionFactorObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                //uiResult.List = clientService.ObtenerClasificacionFactor(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        #endregion

        #region DEPENDENCIAS

        public ActionResult Dependencias()
        {
            List<UiDependencias> dependenciasList = new List<UiDependencias>();

            ServicesCatalog clientService = new ServicesCatalog();
            dependenciasList = clientService.ObtenerTodosDependencias(1, 20);
            var query = dependenciasList.OrderBy(x => x.Name).ToList();
            ViewBag.Dependencias = new SelectList(query, "Identificador", "Name");
            return View();
        }

        public JsonResult DependenciaConsulta(int page, int rows)
        {
            UiResultPage<UiDependencias> uiResult = new UiResultPage<UiDependencias>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerDependencias(1, 10);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DependenciaConsultaCriterio(UiResultPage<UiDependencias> model)
        {
            UiResultPage<UiDependencias> uiResult = new UiResultPage<UiDependencias>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.DependenciasObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> Dependencia(UiDependencias model)
        {
            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Dependencia";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Dependencia";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle de la Dependencia";
                    break;
            }
            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult DependenciaGuardar(UiResultPage<UiDependencias> model)
        {
            UiResultPage<UiDependencias> uiResult = new UiResultPage<UiDependencias>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveDependencias(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador);
                uiResult.List = clientService.ObtenerDependencias(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult DependenciaCambiarEstatus(UiResultPage<UiDependencias> model)
        {
            UiResultPage<UiDependencias> uiResult = new UiResultPage<UiDependencias>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.CambiarEstatusDependencias(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador,
                                                                                                                        model.ObjectResult.IsActive);
                uiResult.List = clientService.DependenciasObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                //uiResult.List = clientService.ObtenerDependencias(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        #endregion

        #region FACTOR_ENTIDAD_FEDERATIVA
        public ActionResult FactorEntidadesFederativas()
        {
            List<UiClasificacionFactor> clasificaciones = new List<UiClasificacionFactor>();
            List<UiEstado> estados = new List<UiEstado>();
            List<UiFactor> factores = new List<UiFactor>();
            ServicesCatalog clientService = new ServicesCatalog();

            clasificaciones = clientService.ObtenerClasificacionFactor(1, 20);
            factores = clientService.ObtenerFactor(1, 20);
            estados = clientService.ObtenerEstado(1, 20);
            ViewBag.Clasificaciones = new SelectList(clasificaciones.OrderBy(x => x.Nombre), "Identificador", "Nombre");
            ViewBag.Estados = new SelectList(estados, "Identificador", "Nombre");
            ViewBag.Factores = new SelectList(factores, "Identificador", "Nombre");
            return View();
        }

        public JsonResult FactorEntidadFederativaConsulta(int page, int rows)
        {
            UiResultPage<UiFactorEntidadFederativa> uiResult = new UiResultPage<UiFactorEntidadFederativa>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerFactorEntidadFederativaAgrupados(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FactorEntidadFederativaConsultaCriterio(UiResultPage<UiFactorEntidadFederativa> model)
        {
            UiResultPage<UiFactorEntidadFederativa> uiResult = new UiResultPage<UiFactorEntidadFederativa>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.FactorEntidadFederativaObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult FiltroClasificacionObtieneFactor(UiFactorEntidadFederativa model)
        {
            try
            {
                UiPaging paging = new UiPaging { All = 1 };
                UiResultPage<UiFactor> factorList = new UiResultPage<UiFactor>();

                ServicesCatalog clientService = new ServicesCatalog();

                factorList.List = clientService.ObtenerFactor(1, 20);

                factorList.List = (from f in factorList.List
                                   where f.IdClasificacionFactor == model.IdClasificadorFactor
                                   select f).ToList();

                return Json(factorList.List, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { query = "Error", JsonRequestBehavior.AllowGet });
            }
        }

        public JsonResult ObtenerFactores(UiFactorEntidadFederativa model)
        {
            try
            {
                UiPaging paging = new UiPaging { All = 1 };
                UiResultPage<UiFactorEntidadFederativa> facEFList = new UiResultPage<UiFactorEntidadFederativa>();
                UiResultPage<UiFactor> factorList = new UiResultPage<UiFactor>();
                UiResultPage<UiEstado> estados = new UiResultPage<UiEstado>();

                ServicesCatalog clientService = new ServicesCatalog();

                factorList.List = clientService.ObtenerFactor(1, 20);
                facEFList.ObjectResult = clientService.FactorEntidadFederativaObtener(paging);
                facEFList.List = clientService.ClasificacionObtieneEstados(model.IdClasificadorFactor);
                // obtener los estados que existen en esta lista
                estados.List = clientService.ObtenerEstado(1, 20);
                facEFList.ObjectResult.Factores.RemoveAll(d => factorList.List.Exists(b => b.Identificador == d.Identificador));
                //facEFList.ObjectResult.Factores.RemoveAll(f => !f.Identificador.Equals(model.IdFactor));
                ViewBag.Factores = new SelectList(facEFList.ObjectResult.Factores, "Identificador", "Nombre");

                estados.List.RemoveAll(d => facEFList.List.Exists(b => b.IdEstado == d.Identificador));
                // mandar el query correcto para pintar los datos en un listado
                var query = estados.List.Select(e => new { e.Identificador, e.Nombre });

                ViewBag.Estados = new SelectList(query.ToArray(), "Identificador", "Nombre");

                return Json(new { estados.List, facEFList.ObjectResult.Factores }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { query = "Error", JsonRequestBehavior.AllowGet });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> FactorEntidadFederativa(UiFactorEntidadFederativa model)
        {
            UiPaging paging = new UiPaging { All = 1 };
            UiResultPage<UiFactorEntidadFederativa> result = new UiResultPage<Models.UiFactorEntidadFederativa>();
            UiResultPage<UiEstado> estados = new UiResultPage<UiEstado>();
            UiResultPage<UiFactorEntidadFederativa> facEFList = new UiResultPage<UiFactorEntidadFederativa>();
            UiResultPage<UiFactor> factorList = new UiResultPage<UiFactor>();

            ServicesCatalog clientService = new ServicesCatalog();
            result.ObjectResult = clientService.FactorEntidadFederativaObtener(paging);
            facEFList.List = clientService.ClasificacionObtieneEstados(model.IdClasificadorFactor);
            // obtener los estados que existen en esta lista
            estados.List = clientService.ObtenerEstado(1, 20);
            if (model.Estados != null)
            {
                result.ObjectResult.Estados.RemoveAll(d => model.Estados.Exists(b => b.Identificador == d.Identificador));
                ViewBag.EntidadesFederativas = new SelectList(model.Estados, "Identificador", "Nombre");
            }


            ViewBag.Clasificaciones = new SelectList(result.ObjectResult.ClasificacionesFactor, "Identificador", "Nombre");

            estados.List.RemoveAll(d => facEFList.List.Exists(b => b.IdEstado == d.Identificador));
            // mandar el query correcto para pintar los datos en un listado
            ViewBag.Estados = new SelectList(estados.List, "Identificador", "Nombre");


            var idCl = model.IdClasificadorFactor;

            var idFac = model.IdFactor;

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Factor Entidad Federativa";
                    break;

                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Factor Entidad Federativa";
                    // se llenan los DropDownList
                    result.ObjectResult.Factores.RemoveAll(f => !f.Identificador.Equals(model.IdFactor));
                    ViewBag.Factores = new SelectList(result.ObjectResult.Factores, "Identificador", "Nombre", idFac);
                    result.ObjectResult.ClasificacionesFactor.RemoveAll(d => !d.Identificador.Equals(model.IdClasificadorFactor));
                    ViewBag.Clasificaciones = new SelectList(result.ObjectResult.ClasificacionesFactor, "Identificador", "Nombre");

                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle del Factor Entidad Federativa";
                    result.ObjectResult.Factores.RemoveAll(f => !f.Identificador.Equals(model.IdFactor));
                    ViewBag.Factores = new SelectList(result.ObjectResult.Factores, "Identificador", "Nombre", idFac);
                    break;
            }

            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult FactorEntidadFederativaGuardar(UiResultPage<UiFactorEntidadFederativa> model)
        {
            UiResultPage<UiFactorEntidadFederativa> uiResult = new UiResultPage<UiFactorEntidadFederativa>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveFactorEntidadFederativa(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ObtenerFactorEntidadFederativaAgrupados(1, 20);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }
        #endregion

        #region FACTOR_MUNICIPIO
        public ActionResult FactoresMunicipio()
        {
            List<UiClasificacionFactor> clasificaciones = new List<UiClasificacionFactor>();
            List<UiEstado> estados = new List<UiEstado>();
            List<UiFactor> factores = new List<UiFactor>();
            ServicesCatalog clientService = new ServicesCatalog();

            clasificaciones = clientService.ObtenerClasificacionFactor(1, 20);
            factores = clientService.ObtenerFactor(1, 20);
            estados = clientService.ObtenerEstado(1, 20);
            ViewBag.Clasificaciones = new SelectList(clasificaciones.OrderBy(x => x.Nombre), "Identificador", "Nombre");
            ViewBag.Estados = new SelectList(estados, "Identificador", "Nombre");
            ViewBag.Factores = new SelectList(factores, "Identificador", "Nombre");
            ViewBag.Municipios = new SelectList("", "Identificador", "Nombre");

            return View();
        }

        public JsonResult FactorMunicipioConsulta(UiResultPage<UiFactorMunicipio> model)
        {
            UiResultPage<UiFactorMunicipio> uiResult = new UiResultPage<UiFactorMunicipio>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.FactorMunicipioObtener(1, 20);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FactorMunicipioConsultaCriterio(UiResultPage<UiFactorMunicipio> model)
        {
            UiResultPage<UiFactorMunicipio> uiResult = new UiResultPage<UiFactorMunicipio>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerPorCriterioFactorMunicipio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> FactorMunicipio(UiFactorMunicipio model)
        {
            UiPaging paging = new UiPaging { All = 1 };
            UiResultPage<UiFactorMunicipio> result = new UiResultPage<Models.UiFactorMunicipio>();
            UiResultPage<UiClasificacionFactor> clasList = new UiResultPage<UiClasificacionFactor>();
            UiResultPage<UiFactor> factorList = new UiResultPage<UiFactor>();
            UiResultPage<UiEstado> EstadoList = new UiResultPage<UiEstado>();
            UiResultPage<UiMunicipio> Municipio1List = new UiResultPage<UiMunicipio>();

            ServicesCatalog clientService = new ServicesCatalog();

            clasList.List = clientService.ObtenerClasificacionFactor(1, 20);
            EstadoList.List = clientService.ObtenerEstado(1, 20);
            factorList.List = clientService.ObtenerFactor(1, 20);
            Municipio1List.List = clientService.ObtenerMunicipios(model.IdEstado);

            if (model.Municipios != null)
            {
                ViewBag.MunicipiosDestino = new SelectList(model.Municipios, "Identificador", "Nombre");
            }

            ViewBag.Clasificaciones = new SelectList(clasList.List, "Identificador", "Nombre", model.IdClasificacionFactor);

            ViewBag.Estados = new SelectList(EstadoList.List, "Identificador", "Nombre", model.IdEstado);


            var idCl = model.IdClasificacionFactor;

            var idFac = model.IdFactor;

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Factor Municipio";
                    ViewBag.Factores = new SelectList(factorList.List, "Identificador", "Nombre");
                    ViewBag.MunicipiosDestino = new SelectList(model.Municipios, "Identificador", "Nombre");
                    ViewBag.Municipios = new SelectList(Municipio1List.List, "Identificador", "Nombre");
                    break;

                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Factor Municipio";
                    // se llenan los DropDownList
                    Municipio1List.List.RemoveAll(d => model.Municipios.Exists(b => b.Identificador == d.Identificador));
                    ViewBag.Municipios = new SelectList(Municipio1List.List, "Identificador", "Nombre");
                    clasList.List.RemoveAll(d => !d.Identificador.Equals(model.IdClasificacionFactor));
                    ViewBag.Clasificaciones = new SelectList(clasList.List, "Identificador", "Nombre");
                    factorList.List.RemoveAll(f => !f.Identificador.Equals(model.IdFactor));
                    ViewBag.Factores = new SelectList(factorList.List, "Identificador", "Nombre", idFac);

                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle del Factor Municipio";
                    factorList.List.RemoveAll(f => !f.Identificador.Equals(model.IdFactor));
                    ViewBag.Factores = new SelectList(factorList.List, "Identificador", "Nombre", idFac);
                    break;
            }

            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult FactorMunicipioGuardar(UiResultPage<UiFactorMunicipio> model)
        {
            UiResultPage<UiFactorMunicipio> uiResult = new UiResultPage<UiFactorMunicipio>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveFactorMunicipio(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.FactorMunicipioObtener(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult FactorMunicipioCambiarEstatus(UiResultPage<UiFactorEntidadFederativa> model)
        {
            UiResultPage<UiFactorEntidadFederativa> uiResult = new UiResultPage<UiFactorEntidadFederativa>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.CambiarEstatusFactorEntidadFederativa(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.FactorEntidadFederativaObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                //uiResult.List = clientService.ObtenerPeriodosPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult IdEstadosObtenerMunicipios(int id)
        {
            try
            {
                UiResultPage<UiMunicipio> municipio = new UiResultPage<UiMunicipio>();

                ServicesCatalog clientService = new ServicesCatalog();

                // obtiene el servicio de municipios mandando el id del estado
                municipio.List = clientService.ObtenerMunicipios(id);
                municipio.Result = UiEnum.TransactionResult.Success;

                var query = municipio.List.Select(e => new { e.Identificador, e.Nombre });

                ViewBag.Municipios = new SelectList(municipio.List, "Identificador", "Nombre");

                return Json(new SelectList(municipio.List, "Identificador", "Nombre"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { query = "Error", JsonRequestBehavior.AllowGet });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult FactoresMunicipioObtenerMunicipios(UiFactorMunicipio model)
        {
            try
            {
                UiResultPage<UiMunicipio> Mlist = new UiResultPage<UiMunicipio>();
                List<UiMunicipio> municipios = new List<UiMunicipio>();
                UiResultPage<UiFactorMunicipio> factorMList = new UiResultPage<UiFactorMunicipio>();

                ServicesCatalog clientService = new ServicesCatalog();

                // obtener los registros si existen la clasificacion con su respectivo estado y municipio
                factorMList.List = clientService.ClasificacionObtieneMunicipios(model.IdClasificacionFactor);
                // obtiene el servicio de municipios mandando el id del estado
                Mlist.List = clientService.ObtenerMunicipios(model.IdEstado);

                Mlist.List.RemoveAll(d => factorMList.List.Exists(b => b.IdMunicipio == d.Identificador && b.IdEstado == model.IdEstado));
                // mandar el query correcto para pintar los datos en un listado

                var query = Mlist.List.Select(e => new { e.Identificador, e.Nombre });

                ViewBag.Municipios = new SelectList(Mlist.List, "Identificador", "Nombre");

                return Json(new SelectList(Mlist.List, "Identificador", "Nombre"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { query = "Error", JsonRequestBehavior.AllowGet });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult ClasificacionObtieneFactor(UiFactorMunicipio model)
        {
            try
            {
                UiPaging paging = new UiPaging { All = 1 };
                UiResultPage<UiFactorMunicipio> facMList = new UiResultPage<UiFactorMunicipio>();
                UiResultPage<UiFactor> factorList = new UiResultPage<UiFactor>();

                ServicesCatalog clientService = new ServicesCatalog();

                factorList.List = clientService.ObtenerFactor(1, 20);
                //facMList.List = clientService.FactorMunicipioObtener(1, 20);

                factorList.List = (from f in factorList.List
                                   where f.IdClasificacionFactor == model.IdClasificacionFactor
                                   select f).ToList();

                //factorList.List.RemoveAll(d => facMList.List.Exists(b => b.IdFactor == d.Identificador && b.IdClasificacionFactor == d.IdClasificacionFactor));

                return Json(factorList.List, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { query = "Error", JsonRequestBehavior.AllowGet });
            }
        }

        #endregion

        #region FACTOR_ACTIVIDAD_ECONOMICA
        public ActionResult FactoresActividadEconomica()
        {
            List<UiDivision> divisiones = new List<UiDivision>();
            List<UiGrupo> grupos = new List<UiGrupo>();
            List<UiFactor> factores = new List<UiFactor>();
            List<UiClasificacionFactor> clasificaciones = new List<UiClasificacionFactor>();

            ServicesCatalog clientService = new ServicesCatalog();

            clasificaciones = clientService.ObtenerClasificacionFactor(1, 20);
            grupos = clientService.GrupoObtener(1, 20);
            divisiones = clientService.ObtenerDivisiones(1, 20);
            factores = clientService.ObtenerFactor(1, 20);
            ViewBag.Clasificaciones = new SelectList(clasificaciones.OrderBy(x => x.Nombre), "Identificador", "Nombre");
            ViewBag.Divisiones = new SelectList(divisiones.OrderBy(x => x.Name), "Identificador", "Name");
            ViewBag.Grupos = new SelectList(grupos.OrderBy(x => x.Name), "Identificador", "Name");
            ViewBag.Factores = new SelectList(factores.OrderBy(x => x.Nombre), "Identificador", "Nombre");
            return View();
        }

        public JsonResult FactorActividadEconomicaConsulta(UiResultPage<UiFactorActividadEconomica> model)
        {
            UiResultPage<UiFactorActividadEconomica> uiResult = new UiResultPage<UiFactorActividadEconomica>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerFactorActividadEconomicaAgrupados(1, 20);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FactorActividadEconomicaConsultaCriterio(UiResultPage<UiFactorActividadEconomica> model)
        {
            UiResultPage<UiFactorActividadEconomica> uiResult = new UiResultPage<UiFactorActividadEconomica>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.FactorActividadEconomicaObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> FactorActividadEconomica(UiFactorActividadEconomica model)
        {
            UiPaging paging = new UiPaging { All = 1 };
            UiResultPage<UiFactorActividadEconomica> result = new UiResultPage<Models.UiFactorActividadEconomica>();
            UiResultPage<UiClasificacionFactor> clasificaciones = new UiResultPage<UiClasificacionFactor>();
            UiResultPage<UiFactor> factores = new UiResultPage<UiFactor>();
            List<UiFracciones> fracciones = new List<UiFracciones>();
            List<UiDivision> divisiones = new List<UiDivision>();
            List<UiGrupo> grupos = new List<UiGrupo>();

            ServicesCatalog clientService = new ServicesCatalog();

            clasificaciones.List = clientService.ObtenerClasificacionFactor(1, 20);
            factores.List = clientService.ObtenerFactor(1, 20);
            fracciones = clientService.ObtenerTodosFracciones(1, 20);
            grupos = clientService.GrupoObtener(1, 20);
            divisiones = clientService.ObtenerDivisiones(1, 20);

            if (model.Fracciones != null)
            {
                ViewBag.FraccionesTodos = new SelectList(model.Fracciones, "Identificador", "Nombre");
            }

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Factor Actividad Económica";
                    ViewBag.Divisiones = new SelectList(divisiones.OrderBy(x => x.Name), "Identificador", "Name");
                    ViewBag.Grupos = new SelectList(grupos.OrderBy(x => x.Name), "Identificador", "Name");
                    ViewBag.Clasificaciones = new SelectList(clasificaciones.List.OrderBy(x => x.Nombre), "Identificador", "Nombre", model.IdClasificacionFactor);
                    ViewBag.Factores = new SelectList(factores.List.OrderBy(x => x.Nombre), "Identificador", "Nombre");
                    break;

                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Actividad Económica";
                    // se llenan los DropDownList
                    grupos.RemoveAll(d => !d.Identificador.Equals(model.IdGrupo));
                    ViewBag.Grupos = new SelectList(grupos.OrderBy(x => x.Name), "Identificador", "Name");
                    divisiones.RemoveAll(d => !d.Identificador.Equals(model.IdDivision));
                    ViewBag.Divisiones = new SelectList(divisiones.OrderBy(x => x.Name), "Identificador", "Name");
                    clasificaciones.List.RemoveAll(d => !d.Identificador.Equals(model.IdClasificacionFactor));
                    ViewBag.Clasificaciones = new SelectList(clasificaciones.List.OrderBy(x => x.Nombre), "Identificador", "Nombre");
                    factores.List.RemoveAll(f => !f.Identificador.Equals(model.IdFactor));
                    ViewBag.Factores = new SelectList(factores.List.OrderBy(x => x.Nombre), "Identificador", "Nombre");
                    fracciones.RemoveAll(d => model.Fracciones.Exists(b => b.Identificador == d.Identificador));
                    var query = fracciones.Where(x => x.IdGrupo == model.IdGrupo);
                    ViewBag.Fracciones = new SelectList(query.OrderBy(x => x.Nombre), "Identificador", "Nombre");

                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle del Factor Actividad Económica";
                    grupos.RemoveAll(d => !d.Identificador.Equals(model.IdGrupo));
                    ViewBag.Grupos = new SelectList(grupos.OrderBy(x => x.Name), "Identificador", "Name");
                    divisiones.RemoveAll(d => !d.Identificador.Equals(model.IdDivision));
                    ViewBag.Divisiones = new SelectList(divisiones.OrderBy(x => x.Name), "Identificador", "Name");
                    clasificaciones.List.RemoveAll(d => !d.Identificador.Equals(model.IdClasificacionFactor));
                    ViewBag.Clasificaciones = new SelectList(clasificaciones.List.OrderBy(x => x.Nombre), "Identificador", "Nombre");
                    factores.List.RemoveAll(f => !f.Identificador.Equals(model.IdFactor));
                    ViewBag.Factores = new SelectList(factores.List.OrderBy(x => x.Nombre), "Identificador", "Nombre");
                    break;
            }

            return PartialView(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult FiltroDivisionObtieneGrupos(UiFactorActividadEconomica model)
        {
            try
            {
                UiResultPage<UiGrupo> grupos = new UiResultPage<UiGrupo>();

                ServicesCatalog clientService = new ServicesCatalog();

                grupos.List = clientService.GrupoObtener(1, 20);

                grupos.List = (from f in grupos.List
                               where f.IdDivision == model.IdDivision
                               select f).ToList();

                return Json(grupos.List, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { query = "Error", JsonRequestBehavior.AllowGet });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult FiltroGrupoObtieneFracciones(UiFactorActividadEconomica model)
        {
            try
            {
                UiResultPage<UiFracciones> fracciones = new UiResultPage<UiFracciones>();
                List<UiFactorActividadEconomica> economica = new List<UiFactorActividadEconomica>();

                ServicesCatalog clientService = new ServicesCatalog();
                fracciones.List = clientService.ObtenerTodosFracciones(1, 20);
                economica = clientService.FactorActividadEconomicaObtenerSinAgrupar(1, 20);

                var IdsFraccion = economica.Where(g => g.IdClasificacionFactor == model.IdClasificacionFactor && g.IdFactor == model.IdFactor).Select(x => x.IdFraccion);

                foreach (var item in IdsFraccion)
                {
                    fracciones.List = (from f in fracciones.List
                                       where f.IdGrupo == model.IdGrupo && f.Identificador != item
                                       select f).ToList();

                }

                return Json(fracciones.List, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { query = "Error", JsonRequestBehavior.AllowGet });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GrupoObtieneFracciones(UiFactorActividadEconomica model)
        {
            try
            {
                UiResultPage<UiFracciones> fracciones = new UiResultPage<UiFracciones>();

                ServicesCatalog clientService = new ServicesCatalog();
                fracciones.List = clientService.ObtenerTodosFracciones(1, 20);

                fracciones.List = (from f in fracciones.List
                                   where f.IdGrupo == model.IdGrupo
                                   select f).ToList();

                return Json(fracciones.List, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { query = "Error", JsonRequestBehavior.AllowGet });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult FactorActividadEconomicaGuardar(UiResultPage<UiFactorActividadEconomica> model)
        {
            UiResultPage<UiFactorActividadEconomica> uiResult = new UiResultPage<UiFactorActividadEconomica>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveFactorActividadEconomica(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ObtenerFactorActividadEconomicaAgrupados(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        #endregion

        #region FACTOR_LEY_INGRESO
        public ActionResult FactorLeyIngresos()
        {
            List<UiAnio> aniosList = new List<UiAnio>();
            ServicesCatalog clientService = new ServicesCatalog();

            aniosList = clientService.ObtenerAnio(1, 20);

            var query = aniosList.Select(x => new { x.Identificador, Name = x.Identificador }).OrderBy(x => x.Identificador).ToList();
            ViewBag.Anios = new SelectList(query, "Identificador", "Name");
            return View();
        }

        public JsonResult FactorLeyIngresoConsulta(int page, int rows)
        {
            UiResultPage<UiFactorLeyIngreso> uiResult = new UiResultPage<UiFactorLeyIngreso>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerFactorLeyIngreso(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FactorLeyIngresoConsultaCriterio(UiResultPage<UiFactorLeyIngreso> model)
        {
            UiResultPage<UiFactorLeyIngreso> uiResult = new UiResultPage<UiFactorLeyIngreso>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.FactorLeyIngresoObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> FactorLeyIngreso(UiFactorLeyIngreso model)
        {

            UiResultPage<UiAnio> AnioList = new UiResultPage<UiAnio>();
            UiResultPage<UiMes> MesList = new UiResultPage<UiMes>();

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Factor Ley Ingreso";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Factor Ley Ingreso";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle del Factor Ley Ingreso";
                    break;
            }

            ServicesCatalog clientService = new ServicesCatalog();

            AnioList.List = clientService.ObtenerAnio(1, 20);

            MesList.List = clientService.ObtenerMes(1, 20);

            var idAnio = model.IdAnio;
            var idMes = model.IdMes;

            if (model.FactorTexto != null)
            {
                var precio = model.FactorTexto.Replace("$", "");
                model.FactorTexto = precio;
            }

            var queryA = AnioList.List.Select(x => new { x.Identificador, Anios = x.Identificador }).OrderBy(x => x.Identificador).ToList();
            ViewBag.Anios = new SelectList(queryA, "Identificador", "Anios", idAnio);

            var queryM = MesList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Identificador).ToList();
            ViewBag.Meses = new SelectList(queryM, "Identificador", "Name", idMes);

            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult FactorLeyIngresoGuardar(UiResultPage<UiFactorLeyIngreso> model)
        {
            UiResultPage<UiFactorLeyIngreso> uiResult = new UiResultPage<UiFactorLeyIngreso>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveFactorLeyIngreso(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ObtenerFactorLeyIngreso(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult FactorLeyIngresoCambiarEstatus(UiResultPage<UiFactorLeyIngreso> model)
        {
            UiResultPage<UiFactorLeyIngreso> uiResult = new UiResultPage<UiFactorLeyIngreso>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.CambiarEstatusFactorLeyIngreso(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador,
                                                                                                                        model.ObjectResult.IsActive);
                uiResult.List = clientService.FactorLeyIngresoObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                //uiResult.List = clientService.ObtenerPeriodosPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        #endregion

        #region FACTOR
        public ActionResult Factores()
        {
            UiResultPage<UiTiposServicio> tiposServicioList = new UiResultPage<UiTiposServicio>();
            UiResultPage<UiClasificacionFactor> clasificacionesList = new UiResultPage<UiClasificacionFactor>();
            UiResultPage<UiFactor> factoresList = new UiResultPage<UiFactor>();

            ServicesCatalog clientService = new ServicesCatalog();

            tiposServicioList.List = clientService.ObtenerTiposServicio(1, 20);

            clasificacionesList.List = clientService.ObtenerClasificacionFactor(1, 20);

            factoresList.List = clientService.ObtenerTodosFactor(1, 20);

            var queryT = tiposServicioList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            ViewBag.TipoServicio = new SelectList(queryT, "Identificador", "Name");

            var queryC = clasificacionesList.List.Select(x => new { x.Identificador, x.Nombre }).OrderBy(x => x.Nombre).ToList();
            ViewBag.Clasificacion = new SelectList(queryC, "Identificador", "Nombre");

            var queryF = factoresList.List.Select(x => new { x.Identificador, x.Nombre }).OrderBy(x => x.Nombre).ToList();
            ViewBag.Factor = new SelectList(queryF, "Identificador", "Nombre");

            return View();
        }

        public JsonResult FactorConsulta(int page, int rows)
        {
            UiResultPage<UiFactor> uiResult = new UiResultPage<UiFactor>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerFactor(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FactorConsultaCriterio(UiResultPage<UiFactor> model)
        {
            UiResultPage<UiFactor> uiResult = new UiResultPage<UiFactor>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.FactorObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> Factor(UiFactor model)
        {
            UiResultPage<UiClasificacionFactor> clasList = new UiResultPage<UiClasificacionFactor>();
            UiResultPage<UiTiposServicio> tipoServList = new UiResultPage<UiTiposServicio>();
            UiResultPage<UiMedidaCobro> medCobList = new UiResultPage<UiMedidaCobro>();

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Factor";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Factor";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle del Factor";
                    break;
            }

            ServicesCatalog clientService = new ServicesCatalog();

            clasList.List = clientService.ObtenerClasificacionFactor(1, 20);

            tipoServList.List = clientService.ObtenerTiposServicio(1, 20);

            medCobList.List = clientService.ObtenerMedidaCobro(1, 20);

            var idTS = model.IdTipoServicio;
            var idCl = model.IdClasificacionFactor;
            var idMC = model.IdMedidaCobro;
            if (model.CuotaTexto != null)
            {
                var precio = model.CuotaTexto.Replace("$", "");
                model.CuotaTexto = precio;
            }

            var query = clasList.List.Select(x => new { x.Identificador, x.Nombre }).OrderBy(x => x.Nombre).ToList();
            ViewBag.Clasificacion = new SelectList(query, "Identificador", "Nombre", idCl);

            var query1 = tipoServList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            ViewBag.TipoServicio = new SelectList(query1, "Identificador", "Name", idTS);

            var query2 = medCobList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            ViewBag.MedidaCobro = new SelectList(query2, "Identificador", "Name", idMC);

            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult FactorGuardar(UiResultPage<UiFactor> model)
        {
            UiResultPage<UiFactor> uiResult = new UiResultPage<UiFactor>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveFactor(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador);
                uiResult.List = clientService.ObtenerFactor(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult FactorCambiarEstatus(UiResultPage<UiFactor> model)
        {
            UiResultPage<UiFactor> uiResult = new UiResultPage<UiFactor>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.CambiarEstatusFactor(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador,
                                                                                                                        model.ObjectResult.IsActive);
                uiResult.List = clientService.FactorObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                //uiResult.List = clientService.ObtenerPeriodosPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult FactorObtenerPorClasificacion(int id)
        {
            UiResultPage<UiFactor> uiResult = new UiResultPage<UiFactor>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerFactorPorClasificacion(id);
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        #endregion

        // Josue Zaragoza
        #region REFERENCIAS
        public ActionResult Referencias()
        {
            List<UiReferencia> referencias = new List<UiReferencia>();

            ServicesCatalog clientService = new ServicesCatalog();

            referencias = clientService.ObtenerReferencias(1, 20);

            var query = referencias.OrderBy(x => x.ClaveReferencia).ToList();
            ViewBag.Referencias = new SelectList(query, "Identificador", "ClaveReferencia");
            return View();
        }

        public JsonResult ReferenciasConsulta(int page, int rows)
        {
            UiResultPage<UiReferencia> uiResult = new UiResultPage<UiReferencia>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerReferencias(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReferenciasConsultaCriterio(UiResultPage<UiReferencia> model)
        {
            UiResultPage<UiReferencia> uiResult = new UiResultPage<UiReferencia>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ReferenciasObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> Referencia(UiReferencia model)
        {
            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Referencia";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Referencia";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle de la Referencia";
                    break;
            }
            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ReferenciaGuardar(UiResultPage<UiReferencia> model)
        {
            UiResultPage<UiReferencia> uiResult = new UiResultPage<UiReferencia>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveReferencias(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador);
                uiResult.List = clientService.ObtenerReferencias(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ReferenciaCambiarEstatus(UiResultPage<UiReferencia> model)
        {
            UiResultPage<UiReferencia> uiResult = new UiResultPage<UiReferencia>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.ReferenciaCambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador,
                                                                                                                        model.ObjectResult.IsActive);
                uiResult.List = clientService.ReferenciasObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                //uiResult.List = clientService.ObtenerReferenciasPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }
        #endregion

        #region GASTOS_INHERENTES
        public ActionResult GastosInherentes()
        {
            List<UiGastosInherente> gastosList = new List<UiGastosInherente>();
            ServicesCatalog clientService = new ServicesCatalog();
            gastosList = clientService.ObtenerGastosInherentes(1, 20);

            var queryT = gastosList.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            ViewBag.GastosInherentes = new SelectList(queryT, "Identificador", "Name");


            return View();
        }

        public JsonResult GastosInherentesConsulta(int page, int rows)
        {
            UiResultPage<UiGastosInherente> uiResult = new UiResultPage<UiGastosInherente>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerGastosInherentes(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GastosInherentesConsultaCriterio(UiResultPage<UiGastosInherente> model)
        {
            UiResultPage<UiGastosInherente> uiResult = new UiResultPage<UiGastosInherente>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.GastosInherentesObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> GastosInherente(UiGastosInherente model)
        {
            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Gasto Inherente";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Gasto Inherente";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle del Gasto Inherente";
                    break;
            }
            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GastosInherenteGuardar(UiResultPage<UiGastosInherente> model)
        {
            UiResultPage<UiGastosInherente> uiResult = new UiResultPage<UiGastosInherente>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveGastosInherentes(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador);
                uiResult.List = clientService.ObtenerGastosInherentes(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GastosInherenteCambiarEstatus(UiResultPage<UiGastosInherente> model)
        {
            UiResultPage<UiGastosInherente> uiResult = new UiResultPage<UiGastosInherente>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.GastosInherenteCambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador,
                                                                                                                        model.ObjectResult.IsActive);
                uiResult.List = clientService.GastosInherentesObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                //uiResult.List = clientService.ObtenerTiposServicio(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        #endregion

        #region PERIODOS
        public ActionResult Periodos()
        {
            List<UiPeriodo> periodoList = new List<UiPeriodo>();

            ServicesCatalog clientService = new ServicesCatalog();

            periodoList = clientService.ObtenerPeriodos(1, 20);

            var query = periodoList.OrderBy(x => x.Name).ToList();
            ViewBag.Periodos = new SelectList(query, "Identificador", "Name");
            return View();
        }

        public JsonResult PeriodosConsulta(int page, int rows)
        {
            UiResultPage<UiPeriodo> uiResult = new UiResultPage<UiPeriodo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerPeriodos(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PeriodosConsultaCriterio(UiResultPage<UiPeriodo> model)
        {
            UiResultPage<UiPeriodo> uiResult = new UiResultPage<UiPeriodo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.PeriodosObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> Periodo(UiPeriodo model)
        {
            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Periodo";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Periodo";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle del Periodo";
                    break;
            }
            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult PeriodoGuardar(UiResultPage<UiPeriodo> model)
        {
            UiResultPage<UiPeriodo> uiResult = new UiResultPage<UiPeriodo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SavePeriodos(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador);

                uiResult.List = clientService.ObtenerPeriodos(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult PeriodoCambiarEstatus(UiResultPage<UiPeriodo> model)
        {
            UiResultPage<UiPeriodo> uiResult = new UiResultPage<UiPeriodo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.PeriodoCambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador,
                                                                                                                        model.ObjectResult.IsActive);

                uiResult.List = clientService.PeriodosObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }
        #endregion

        //Lucio Juarez

        #region FRACCIONES

        public ActionResult Fracciones()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> Fraccion(UiFracciones model)
        {
            UiResultPage<UiDivision> DivList = new UiResultPage<UiDivision>();
            List<UiGrupo> GrupoList = new List<UiGrupo>();

            var idDiv = model.IdDivision;
            var idGru = model.IdGrupo;


            ServicesCatalog clientService = new ServicesCatalog();
            DivList.List = clientService.ObtenerDivisionesListado().Where(x => x.IsActive = true).ToList<UiDivision>();

            if (idDiv != 0)
                GrupoList = clientService.GrupoObtenerPorIdDivisionFraccion(model);

            var queryD = DivList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            var queryG = GrupoList.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();

            ViewBag.Grupoes = new SelectList(GrupoList, "Identificador", "Name", idGru);
            ViewBag.Divisiones = new SelectList(DivList.List, "Identificador", "Name", idDiv);

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Fracción";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Fracción";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle de la Fracción";

                    break;
            }
            return PartialView(model);
        }

        public JsonResult FraccionConsulta(int page, int rows, UiResultPage<UiFracciones> model)
        {
            UiResultPage<UiFracciones> uiResult = new UiResultPage<UiFracciones>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();

                if (model.Query != null)
                {
                    uiResult.List = clientService.ObtenerFraccionesPorCriterio(page, rows, model.Query);
                }
                else
                {
                    uiResult.List = clientService.ObtenerFracciones(page, rows);
                }

                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FraccionConsultaCriterio(UiResultPage<UiFracciones> model)
        {
            UiResultPage<UiFracciones> uiResult = new UiResultPage<UiFracciones>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerFraccionesPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<PartialViewResult> Fraccion(UiFracciones model)
        //{
        //    UiResultPage<UiDivision> DivList = new UiResultPage<UiDivision>();
        //    List<UiGrupo> GrupoList = new List<UiGrupo>();

        //    var idDiv = model.IdDivision;
        //    var idGru = model.IdGrupo;


        //    ServicesCatalog clientService = new ServicesCatalog();
        //    DivList.List = clientService.ObtenerDivisiones(1, 20).Where(x => x.IsActive = true).ToList<UiDivision>();

        //    if (idDiv != 0)
        //        GrupoList = clientService.GrupoObtenerPorIdDivisionFraccion(model);

        //    var queryD = DivList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
        //    var queryG = GrupoList.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();

        //    ViewBag.Grupoes = new SelectList(GrupoList, "Identificador", "Name", idGru);
        //    ViewBag.Divisiones = new SelectList(DivList.List, "Identificador", "Name", idDiv);

        //    switch (model.Action)
        //    {
        //        case UiEnumEntity.New:
        //            ViewBag.Title = "Crear Fracción";
        //            break;
        //        case UiEnumEntity.Edit:
        //            ViewBag.Title = "Modificar Fracción";
        //            break;
        //        case UiEnumEntity.View:
        //            ViewBag.Title = "Detalle de la Fracción";

        //            break;
        //    }
        //    return PartialView(model);
        //}

        [HttpPost]
        [AllowAnonymous]
        public JsonResult FraccionGuardar(UiResultPage<UiFracciones> model)
        {
            UiResultPage<UiFracciones> uiResult = new UiResultPage<UiFracciones>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveFracciones(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador);
                if (model.Query != null)
                {
                    uiResult.List = clientService.ObtenerFraccionesPorCriterio(model.Paging.CurrentPage,
                                                                               model.Paging.Rows,
                                                                               model.Query);
                }
                else
                {
                    uiResult.List = clientService.ObtenerFracciones(model.Paging.CurrentPage, model.Paging.Rows);
                }

                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult FraccionCambiarEstatus(UiResultPage<UiFracciones> model)
        {
            UiResultPage<UiFracciones> uiResult = new UiResultPage<UiFracciones>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.FraccionCambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador,
                                                                                                                        model.ObjectResult.IsActive);
                if (model.Query != null)
                {
                    uiResult.List = clientService.ObtenerFraccionesPorCriterio(model.Paging.CurrentPage,
                                                                               model.Paging.Rows,
                                                                               model.Query);
                }
                else
                {
                    uiResult.List = clientService.ObtenerFracciones(model.Paging.CurrentPage, model.Paging.Rows);
                }


                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [AllowAnonymous]
        public JsonResult FraccionEnlistarGrupoPorDivision(int IdDivision)
        {
            try
            {
                UiFracciones Fraccion = new UiFracciones();
                List<UiGrupo> grupoList = new List<UiGrupo>();
                ServicesCatalog clientService = new ServicesCatalog();
                Fraccion.IdDivision = IdDivision;
                grupoList = clientService.GrupoObtenerPorIdDivisionFraccion(Fraccion);
                ViewBag.Grupoes = new SelectList(grupoList, "Identificador", "Name", IdDivision);

                return Json(grupoList, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { query = "Error", JsonRequestBehavior.AllowGet });
            }
        }

        #endregion FRACCIONES

        //JZR

        #region CUOTAS

        public ActionResult Cuotas()
        {
            ServicesCatalog clientService = new ServicesCatalog();
            List<UiTiposServicio> servicios = new List<UiTiposServicio>();
            List<UiCuota> conceptos = new List<UiCuota>();
            List<UiCuota> anios = new List<UiCuota>();
            List<UiCuota> productosAprovechamientos = new List<UiCuota>();
            servicios = clientService.ObtenerTiposServicio(1, 20);
            conceptos = clientService.ObtenerCuotasConceptos(1, 20);
            anios = clientService.CuotaObtenerAnos(1, 20);
            productosAprovechamientos = clientService.ObtenerCuotas(1, 20);

            ViewBag.Servicios = new SelectList(servicios, "Identificador", "Name");
            ViewBag.Conceptos = new SelectList(conceptos, "Identificador", "Concepto");
            ViewBag.Anios = new SelectList(anios, "Ano", "Ano");
            var queryD = new[] { new { id = true, value = "Productos" }, new { id = false, value = "Aprovechamientos" } };
            ViewBag.ProductosAprovechamientos = new SelectList(queryD, "id", "value");




            return View();
        }

        public JsonResult CuotasConsulta(int page, int rows)
        {
            UiResultPage<UiCuota> uiResult = new UiResultPage<UiCuota>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerCuotas(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CuotaConsultaCriterio(UiResultPage<UiCuota> model)
        {
            UiResultPage<UiCuota> uiResult = new UiResultPage<UiCuota>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.CuotasObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> Cuota(UiCuota model)
        {
            UiResultPage<UiTiposServicio> TiposServicioList = new UiResultPage<UiTiposServicio>();
            UiResultPage<UiReferencia> ReferenciaList = new UiResultPage<UiReferencia>();
            UiResultPage<UiDependencias> DependenciasList = new UiResultPage<UiDependencias>();
            UiResultPage<UiJerarquia> JerarquiaList = new UiResultPage<UiJerarquia>();
            UiResultPage<UiGrupoTarifario> GrupoTarifarioList = new UiResultPage<UiGrupoTarifario>();
            UiResultPage<UiMedidaCobro> MedidaCobroList = new UiResultPage<UiMedidaCobro>();

            ServicesCatalog clientService = new ServicesCatalog();

            TiposServicioList.List = clientService.ObtenerTiposServicio(1, 20);
            ReferenciaList.List = clientService.ObtenerReferencias(1, 20);
            DependenciasList.List = clientService.ObtenerDependencias(1, 20);
            JerarquiaList.List = clientService.ObtenerJerarquias(1, 20);
            GrupoTarifarioList.List = clientService.ObtenerGrupoTarifario(1, 20);
            MedidaCobroList.List = clientService.ObtenerMedidaCobro(1, 20);

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Cuota";

                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Cuota";

                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle de la Cuota";
                    break;
            }
            var idTiposServicio = model.IdTipoServicio;
            var idReferencia = model.IdReferencia;
            var idDependencia = model.IdDependencia;
            var idJerarquia = model.IdJerarquia;
            var idGrupoTarifario = model.IdGrupoTarifario;
            var idMedidaCobro = model.IdMedidaCobro;

            var queryT1 = TiposServicioList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            ViewBag.TiposServicio = new SelectList(queryT1, "Identificador", "Name", idTiposServicio);

            var queryR1 = ReferenciaList.List.Select(x => new { x.Identificador, x.ClaveReferencia }).OrderBy(x => x.ClaveReferencia).ToList();
            ViewBag.Referencia = new SelectList(queryR1, "Identificador", "ClaveReferencia", idReferencia);

            var queryD1 = DependenciasList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            ViewBag.Dependencia = new SelectList(queryD1, "Identificador", "Name", idDependencia);

            var queryJ1 = JerarquiaList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            ViewBag.Jerarquia = new SelectList(queryJ1, "Identificador", "Name", idJerarquia);

            var queryG1 = GrupoTarifarioList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            ViewBag.GrupoTarifario = new SelectList(queryG1, "Identificador", "Name", idGrupoTarifario);

            var queryM1 = MedidaCobroList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            ViewBag.MedidaCobro = new SelectList(queryM1, "Identificador", "Name", idMedidaCobro);
            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult CuotaGuardar(UiResultPage<UiCuota> model)
        {
            UiResultPage<UiCuota> uiResult = new UiResultPage<UiCuota>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveCuotas(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador);
                uiResult.List = clientService.ObtenerCuotas(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult CuotaCambiarEstatus(UiResultPage<UiCuota> model)
        {
            UiResultPage<UiCuota> uiResult = new UiResultPage<UiCuota>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.CuotaCambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ObtenerCuotas(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,
                                                                                                                        model.ObjectResult.Identificador);

                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (ConecWebException ex)
            {
                uiResult.Message = ex.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        #endregion CUOTAS

        [HttpGet]
        [AllowAnonymous]
        public JsonResult MunicipiosObtener(int id)
        {
            UiResultPage<UiMunicipio> uiResult = new UiResultPage<UiMunicipio>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerMunicipios(id);
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerEstado(int page, int rows)
        {
            UiResultPage<UiEstado> uiResult = new UiResultPage<UiEstado>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerEstado(page, rows);
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AsentamientosObtener(int idEstado, int idMunicipio, string codigoPostal)
        {

            UiResultPage<UiAsentamiento> uiResult = new UiResultPage<UiAsentamiento>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                var list = clientService.ObtenerAsentamientos(new UiAsentamiento
                {
                    Estado = new UiEstado { Identificador = idEstado },
                    Municipio = new UiMunicipio { Identificador = idMunicipio },
                    CodigoPostal = codigoPostal
                });

                uiResult.List = list;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        #region AREAS

        public JsonResult AreasConsulta(int page, int rows)
        {
            UiResultPage<UiArea> uiResult = new UiResultPage<UiArea>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerAreas(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region REGIMEN FISCAL

        public JsonResult RegimenFiscalConsulta(int page, int rows)
        {
            UiResultPage<UiRegimenFiscal> uiResult = new UiResultPage<UiRegimenFiscal>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerRegimenFiscal(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region TIPOPAGO

        public JsonResult TipoPagoConsulta(int page, int rows)
        {
            UiResultPage<UiTiposPago> uiResult = new UiResultPage<UiTiposPago>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerTiposPago(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ACTIVIDADES

        public JsonResult ActividadesConsulta(int page, int rows)
        {
            UiResultPage<UiActividad> uiResult = new UiResultPage<UiActividad>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerActividades(page, rows);
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = rows;
                uiResult.Paging.CurrentPage = page;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region MENSAJES

        public string ObtenerMensajeOperacion(string tipo, UiEnum.TransactionResult Resultado, int id, bool? estatusActivo = null)
        {
            string mensaje = string.Empty;

            tipo = tipo != null ? tipo : string.Empty;

            if (tipo == typeof(UiCuota).Name)
            {
                if (id > 0)
                {
                    if (estatusActivo == null)
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.CuotasUpdateSuccess : ErrorMessage.FailSaveMessage);
                    else
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.CuotasActiveSuccess : ErrorMessage.FailSaveMessage);
                }
                else
                    mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.CuotasInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiClasificacionFactor).Name)
            {
                if (id > 0)
                {
                    if (estatusActivo == null)
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.ClasificacionFactorUpdateSuccess : ErrorMessage.FailSaveMessage);
                    else
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.ClasificacionFactorActiveSuccess : ErrorMessage.FailSaveMessage);
                }
                else
                    mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.ClasificacionFactorInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiDependencias).Name)
            {
                if (id > 0)
                {
                    if (estatusActivo == null)
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.DependenciaUpdateSuccess : ErrorMessage.FailSaveMessage);
                    else
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.DependenciaActiveSuccess : ErrorMessage.FailSaveMessage);
                }
                else
                    mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.DependenciaInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiDivision).Name)
            {
                if (id > 0)
                {
                    if (estatusActivo == null)
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.DivisionUpdateSuccess : ErrorMessage.FailSaveMessage);
                    else
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.DivisionActiveSuccess : ErrorMessage.FailSaveMessage);
                }
                else
                    mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.DivisionInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiFactor).Name)
            {
                if (id > 0)
                {
                    if (estatusActivo == null)
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.FactorUpdateSuccess : ErrorMessage.FailSaveMessage);
                    else
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.FactorActiveSuccess : ErrorMessage.FailSaveMessage);
                }
                else
                    mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.FactorInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiFactorLeyIngreso).Name)
            {
                if (id > 0)
                {
                    if (estatusActivo == null)
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.FactorLeyIngresoUpdateSuccess : ErrorMessage.FailSaveMessage);
                    else
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.FactorLeyIngresoActiveSuccess : ErrorMessage.FailSaveMessage);
                }
                else
                    mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.FactorLeyIngresoInsertSuccess : ErrorMessage.FailSaveMessage);

            }
            else if (tipo == typeof(UiFactorEntidadFederativa).Name)
            {
                if (id > 0)
                    mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.FactorEntidadFederativaUpdateSuccess : ErrorMessage.FailSaveMessage);
                else
                    mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.FactorEntidadFederativaInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiFracciones).Name)
            {
                if (id > 0)
                {
                    if (estatusActivo == null)
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.FraccionUpdateSuccess : ErrorMessage.FailSaveMessage);
                    else
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.FraccionActiveSuccess : ErrorMessage.FailSaveMessage);
                }
                else
                    mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.FraccionInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiGastosInherente).Name)
            {
                if (id > 0)
                {
                    if (estatusActivo == null)
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.GastosInherentesUpdateSuccess : ErrorMessage.FailSaveMessage);
                    else
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.GastosInherentesActiveSuccess : ErrorMessage.FailSaveMessage);
                }
                else
                    mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.GastosInherentesInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiGrupo).Name)
            {
                if (id > 0)
                {
                    if (estatusActivo == null)
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.GrupoUpdateSuccess : ErrorMessage.FailSaveMessage);
                    else
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.GrupoActiveSuccess : ErrorMessage.FailSaveMessage);
                }
                else
                    mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.GrupoInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiPeriodo).Name)
            {
                if (id > 0)
                {
                    if (estatusActivo == null)
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.PeriodoUpdateSuccess : ErrorMessage.FailSaveMessage);
                    else
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.PeriodoActiveSuccess : ErrorMessage.FailSaveMessage);
                }
                else
                    mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.PeriodoInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiReferencia).Name)
            {
                if (id > 0)
                {
                    if (estatusActivo == null)
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.ReferenciaUpdateSuccess : ErrorMessage.FailSaveMessage);
                    else
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.ReferenciaActiveSuccess : ErrorMessage.FailSaveMessage);
                }
                else
                    mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.ReferenciaInsertSuccess : ErrorMessage.FailSaveMessage);

            }
            else if (tipo == typeof(UiTiposDocumento).Name)
            {
                if (id > 0)
                {
                    if (estatusActivo == null)
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.TipoDocumentoUpdateSuccess : ErrorMessage.FailSaveMessage);
                    else
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.TipoDocumentoActiveSuccess : ErrorMessage.FailSaveMessage);
                }
                else
                    mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.TipoDocumentoInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiTiposServicio).Name)
            {
                if (id > 0)
                {
                    if (estatusActivo == null)
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.TipoServicioUpdateSuccess : ErrorMessage.FailSaveMessage);
                    else
                        mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.TipoServicioActiveSuccess : ErrorMessage.FailSaveMessage);
                }
                else
                    mensaje = (Resultado == UiEnum.TransactionResult.Success ? NotificationMessage.TipoServicioInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else
            {
                mensaje = string.Empty;
            }

            if (!string.IsNullOrEmpty(mensaje) && id > 0 && estatusActivo != null)
                mensaje = mensaje.Replace("@", estatusActivo == true ? NotificationMessage.Estatus_Row_Active : NotificationMessage.Estatus_Row_Inactive);

            return mensaje;

        }

        #endregion MENSAJES

        #region SECTORES
        public JsonResult SectorConsulta()
        {
            UiResultPage<UiSector> uiResult = new UiResultPage<UiSector>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerSector();
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}