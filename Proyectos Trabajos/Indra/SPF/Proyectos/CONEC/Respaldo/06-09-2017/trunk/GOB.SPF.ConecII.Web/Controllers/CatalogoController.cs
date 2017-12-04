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
        public JsonResult ObtenerGrupo(string term)
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
        public JsonResult ObtenerClasificacion(string term)
        {
            UiResultPage<UiClasificacionFactor> ClFList = new UiResultPage<UiClasificacionFactor>();

            ServicesCatalog clientService = new ServicesCatalog();

            ClFList.List = clientService.ObtenerClasificacionFactor(1, 20);

            var queryC = ClFList.List.Where(x => x.Nombre.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Identificador, value = x.Nombre }).OrderBy(x => x.value).ToList();

            return Json(queryC, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerFactor(string term)
        {
            UiResultPage<UiFactor> FacList = new UiResultPage<UiFactor>();

            ServicesCatalog clientService = new ServicesCatalog();

            FacList.List = clientService.ObtenerFactor(1, 20);

            var queryF = FacList.List.Where(x => x.Nombre.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Identificador, value = x.Nombre }).OrderBy(x => x.value).ToList();

            return Json(queryF, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerAnio(string term)
        {
            UiResultPage<UiAnio> AnioList = new UiResultPage<UiAnio>();

            ServicesCatalog clientService = new ServicesCatalog();

            AnioList.List = clientService.ObtenerAnio(1, 20);

            var queryA = AnioList.List.Where(x => x.Identificador.ToString().Contains(term)).Select(x => new { id = x.Identificador, value = x.Name }).OrderBy(x => x.id).ToList();

            return Json(queryA, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerDependencia(string term)
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
        public ActionResult Usuario()
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
        public ActionResult Rol()
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

        #endregion

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

        public JsonResult GrupoConsultaPorIdDivision(int idDivision)
        {
            UiResultPage<UiGrupo> uiResult = new UiResultPage<UiGrupo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                UiFracciones fraccion = new UiFracciones();
                fraccion.IdDivision = idDivision;
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
            return View();
        }

        public JsonResult TiposServicioConsulta(int page, int rows)
        {
            UiResultPage<UiTiposServicio> uiResult = new UiResultPage<UiTiposServicio>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerTiposServicio(page, rows);
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
            return View();
        }

        public JsonResult DependenciaConsulta(int page, int rows)
        {
            UiResultPage<UiDependencias> uiResult = new UiResultPage<UiDependencias>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerDependencias(page, rows);
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
            return View();
        }

        public JsonResult FactorEntidadFederativaConsulta(int page, int rows)
        {
            UiResultPage<UiFactorEntidadFederativa> uiResult = new UiResultPage<UiFactorEntidadFederativa>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerFactorEntidadFederativa(page, rows);
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

        public JsonResult ObtenerFactores(int IdClasificador)
        {
            try
            {

                UiResultPage<UiFactor> factorList = new UiResultPage<UiFactor>();

                ServicesCatalog clientService = new ServicesCatalog();

                factorList.List = clientService.ObtenerFactor(1, 20);

                var query = factorList.List.Where(x => x.IdClasificacionFactor == IdClasificador).Select(x => new { x.Identificador, x.Nombre }).OrderBy(x => x.Nombre).ToList();
                ViewBag.Factores = new SelectList(query, "Identificador", "Nombre", IdClasificador);

                return Json(query, JsonRequestBehavior.AllowGet);
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

            //UiResultPage<UiClasificacionFactor> clasList = new UiResultPage<UiClasificacionFactor>();
            UiResultPage<UiFactor> factorList = new UiResultPage<UiFactor>();
            //UiResultPage<UiEstado> EstadoList = new UiResultPage<UiEstado>();

            ServicesCatalog clientService = new ServicesCatalog();
            result.ObjectResult = clientService.FactorEntidadFederativaObtener(paging);
            if (model.Estados != null)
            {
                result.ObjectResult.Estados.RemoveAll(d => model.Estados.Exists(b => b.Identificador == d.Identificador));
                ViewBag.EntidadesFederativas = new SelectList(model.Estados, "Identificador", "Nombre");
            }


            ViewBag.Clasificaciones = new SelectList(result.ObjectResult.ClasificacionesFactor, "Identificador", "Nombre");

            ViewBag.Estados = new SelectList(result.ObjectResult.Estados, "Identificador", "Nombre");


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
                    result.ObjectResult.ClasificacionesFactor.RemoveAll(d => !d.Identificador.Equals(model.IdClasificadorFactor));
                    ViewBag.Clasificaciones = new SelectList(result.ObjectResult.ClasificacionesFactor, "Identificador", "Nombre");
                    ViewBag.Factores = new SelectList(result.ObjectResult.Factores, "Identificador", "Nombre", idFac);

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
                uiResult.List = clientService.ObtenerFactorEntidadFederativa(model.Paging.CurrentPage, model.Paging.Rows);
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
        public JsonResult FactorEntidadFederativaCambiarEstatus(UiResultPage<UiFactorEntidadFederativa> model)
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
        public JsonResult ClasificacionObtieneEstados(int id)
        {
            try
            {
                UiPaging paging = new UiPaging { All = 1 };

                UiResultPage<UiFactorEntidadFederativa> factorList = new UiResultPage<UiFactorEntidadFederativa>();
                UiResultPage<UiFactorEntidadFederativa> result = new UiResultPage<UiFactorEntidadFederativa>();
                UiResultPage<UiFactorEntidadFederativa> resultF = new UiResultPage<UiFactorEntidadFederativa>();
                List<UiEstado> estados = new List<UiEstado>();
                var idClasificacion = 0;

                ServicesCatalog clientService = new ServicesCatalog();

                // obtiene todos los servicios de clasificacion, factor y estados
                factorList.ObjectResult = clientService.FactorEntidadFederativaObtener(paging);
                // obtiene los registros de factor entidad federativa
                result.List = clientService.ObtenerFactorEntidadFederativa(1, 20);
                // recorre los registros factor entidad federativa
                foreach (var item in result.List)
                {
                    // obtiene el id
                    idClasificacion = item.IdClasificadorFactor;
                }
                // si existe en el registro
                if (idClasificacion == id)
                {
                    // recorre la lista para obtener los estados
                    foreach (var item in result.List)
                    {
                        // obtener los estados que existen en esta lista
                        estados = item.Estados.ToList();
                    }
                    // realizar la validación que no se repitan los estados y removerlos
                    factorList.ObjectResult.Estados.RemoveAll(d => estados.Exists(b => b.Identificador == d.Identificador));
                }
                // obtener el resultado final
                resultF = factorList;
                // mandar el query correcto para pintar los datos en un listado
                var query = resultF.ObjectResult.Estados.Select(e => new { id = e.Identificador, name = e.Nombre });

                ViewBag.Estados = new SelectList(resultF.ObjectResult.Estados, "Identificador", "Nombre");


                return Json(new SelectList(query.ToArray(), "id", "name"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { query = "Error", JsonRequestBehavior.AllowGet });
            }
        }

        #endregion

        #region FACTOR_LEY_INGRESO
        public ActionResult FactorLeyIngresos()
        {
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

            var queryA = AnioList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Identificador).ToList();
            ViewBag.Anios = new SelectList(queryA, "Identificador", "Name", idAnio);

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
            UiResultPage<UiTiposServicio> TSList = new UiResultPage<UiTiposServicio>();
            UiResultPage<UiClasificacionFactor> ClaList = new UiResultPage<UiClasificacionFactor>();
            UiResultPage<UiFactor> FacList = new UiResultPage<UiFactor>();

            ServicesCatalog clientService = new ServicesCatalog();

            TSList.List = clientService.ObtenerTiposServicio(1, 20);

            ClaList.List = clientService.ObtenerClasificacionFactor(1, 20);

            FacList.List = clientService.ObtenerFactor(1, 20);

            var queryT = TSList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            ViewBag.TipoServicio = new SelectList(queryT, "Identificador", "Name");

            var queryC = ClaList.List.Select(x => new { x.Identificador, x.Nombre }).OrderBy(x => x.Nombre).ToList();
            ViewBag.Clasificacion = new SelectList(queryC, "Identificador", "Nombre");

            var queryF = FacList.List.Select(x => new { x.Identificador, x.Nombre }).OrderBy(x => x.Nombre).ToList();
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

        [HttpGet]
        [AllowAnonymous]
        public JsonResult ClasificacionObtieneFactor(int id)
        {
            UiResultPage<UiFactor> uiResult = new UiResultPage<UiFactor>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ClasificacionObtieneFactor(id);
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

        #region FACTOR_MUNICIPIO
        public ActionResult FactoresMunicipio()
        {
            return View();
        }

        public JsonResult FactorMunicipioConsulta(int page, int rows)
        {
            UiResultPage<UiFactorEntidadFederativa> uiResult = new UiResultPage<UiFactorEntidadFederativa>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List = clientService.ObtenerFactorEntidadFederativa(page, rows);
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

        public JsonResult FactorMunicipioConsultaCriterio(UiResultPage<UiFactorMunicipio> model)
        {
            UiResultPage<UiFactorMunicipio> uiResult = new UiResultPage<UiFactorMunicipio>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.ObjectResult = clientService.ObtenerFactorMunicipio(model.Paging);
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
            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Factor Municipio";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Factor Municipio";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle del Factor Municipio";
                    break;
            }

            ServicesCatalog clientService = new ServicesCatalog();
            UiFactorMunicipio factorMunicipio = clientService.ObtenerFactorMunicipio(paging);
            ViewBag.Clasificaciones = new SelectList(factorMunicipio.Clasificaciones, "Identificador", "Nombre");
            ViewBag.Factores = new SelectList(new List<UiFactor>(), "Identificador", "Nombre");
            ViewBag.Estados = new SelectList(factorMunicipio.Estados, "Identificador", "Nombre");
            ViewBag.Municipios = new SelectList(new List<UiMunicipio>(), "Identificador", "Nombre");
            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult FactorMunicipioGuardar(UiResultPage<UiFactorMunicipio> model)
        {
            UiResultPage<UiFactorEntidadFederativa> uiResult = new UiResultPage<UiFactorEntidadFederativa>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.FactorMunicipioGuardar(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ObtenerFactorEntidadFederativa(model.Paging.CurrentPage, model.Paging.Rows);
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

        #endregion


        // Josue Zaragoza
        #region REFERENCIAS
        public ActionResult Referencias()
        {
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
                uiResult.List = clientService.PeriodosObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
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
                    ViewBag.Title = "Crear Fracción";

                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Fracción";

                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle de la Fracción";
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

        public string ObtenerMensajeOperacion(string tipo, UiEnum.TransactionResult Result, int id)
        {
            string mensaje = string.Empty;

            tipo = tipo != null ? tipo : string.Empty;
            
            if (tipo == typeof(UiCuota).Name)
            {
                if (id > 0)
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.CuotasUpdateSuccess : ErrorMessage.FailSaveMessage);
                else
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.CuotasInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiClasificacionFactor).Name)
            {
                if (id > 0)
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.ClasificacionFactorUpdateSuccess : ErrorMessage.FailSaveMessage);
                else
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.ClasificacionFactorInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiDependencias).Name)
            {
                if (id > 0)
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.DependenciaUpdateSuccess : ErrorMessage.FailSaveMessage);
                else
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.DependenciaInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiDivision).Name)
            {
                if (id > 0)
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.DivisionUpdateSuccess : ErrorMessage.FailSaveMessage);
                else
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.DivisionInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiFactor).Name)
            {
                if (id > 0)
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.FactorUpdateSuccess : ErrorMessage.FailSaveMessage);
                else
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.FactorInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiFactorLeyIngreso).Name)
            {
                if (id > 0)
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.FactorLeyIngresoSuccessUpdate : ErrorMessage.FailSaveMessage);
                else
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.FactorLeyIngresoSuccessInsert : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiFactorEntidadFederativa).Name)
            {
                if (id > 0)
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.FactorEntidadFederativaUpdateSuccess : ErrorMessage.FailSaveMessage);
                else
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.FactorEntidadFederativaInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiFracciones).Name)
            {
                if (id > 0)
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.FraccionUpdateSuccess : ErrorMessage.FailSaveMessage);
                else
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.FraccionInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiGastosInherente).Name)
            {
                if (id > 0)
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.GastosInherentesUpdateSuccess : ErrorMessage.FailSaveMessage);
                else
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.GastosInherentesInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiGrupo).Name)
            {
                if (id > 0)
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.GrupoUpdateSuccess : ErrorMessage.FailSaveMessage);
                else
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.GrupoInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiPeriodo).Name)
            {
                if (id > 0)
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.PeriodoUpdateSuccess : ErrorMessage.FailSaveMessage);
                else
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.PeriodoInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiReferencia).Name)
            {
                if (id > 0)
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.ReferenciaUpdateSuccess : ErrorMessage.FailSaveMessage);
                else
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.ReferenciaInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiTiposDocumento).Name)
            {
                if (id > 0)
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.TipoDocumentoUpdateSuccess : ErrorMessage.FailSaveMessage);
                else
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.TipoDocumentoInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else if (tipo == typeof(UiTiposServicio).Name)
            {
                if (id > 0)
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.TipoServicioUpdateSuccess : ErrorMessage.FailSaveMessage);
                else
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.TipoServicioInsertSuccess : ErrorMessage.FailSaveMessage);
            }
            else
            {
                mensaje = string.Empty;   
            }

            return mensaje;

        }
        
        #endregion MENSAJES



    }
}