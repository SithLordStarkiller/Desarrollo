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

        public JsonResult ObtenerTipoDocumento (string term)
        {
            UiResultPage<UiTiposDocumento> TDList = new UiResultPage<UiTiposDocumento>();

            ServicesCatalog clientService = new ServicesCatalog();

            TDList.List = clientService.ObtenerTiposDocumento(1, 20);

            var queryTD = TDList.List.Where(x => x.Name.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Identificador, value = x.Name }).OrderBy(x => x.value).ToList();

            return Json(queryTD, JsonRequestBehavior.AllowGet);
        }

        // GET: Catalogo
        #region DIVISIONES
        public ActionResult Divisiones()
        {
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

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveDivisiones(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
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
            return Json(uiResult);
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

        public JsonResult ObtenerFactorDDL(int IdFacEntidFed, int IdClasificador)
        {
            try
            {

                UiResultPage<UiFactor> factorList = new UiResultPage<UiFactor>();

                ServicesCatalog clientService = new ServicesCatalog();

                factorList.List = clientService.ObtenerFactor(1, 20);



                var query = factorList.List.Where(x => x.IdClasificacionFactor == IdClasificador).Select(x => new { x.Identificador, x.Nombre }).OrderBy(x => x.Nombre).ToList();
                ViewBag.Factores = new SelectList(query, "Identificador", "Name");

                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { query = "Error", JsonRequestBehavior.AllowGet });
            }
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
            if(model.Estados != null)
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
                    result.ObjectResult.Factores.RemoveAll(f => !f.Identificador.Equals(model.IdFactor));
                    result.ObjectResult.ClasificacionesFactor.RemoveAll(d => !d.Identificador.Equals(model.IdClasificadorFactor));
                    ViewBag.Clasificaciones = new SelectList(result.ObjectResult.ClasificacionesFactor, "Identificador", "Nombre");
                    ViewBag.Factores = new SelectList(result.ObjectResult.Factores, "Identificador", "Nombre", idFac);

                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle del Factor Entidad Federativa";
                    break;
            }

            //var idEst = model.IdEntidFed;

            //var queryC = clasList.List.Select(x => new { x.Identificador, x.Nombre }).OrderBy(x => x.Nombre).ToList();
            //ViewBag.Clasificacion = new SelectList(queryC, "Identificador", "Name", idCl);

            //var queryE = EstadoList.List.Select(x => new { x.Identificador, x.Nombre }).OrderBy(x => x.Nombre).ToList();
            //ViewBag.Estado = new SelectList(queryE, "Identificador", "Name");

            //var queryEs = EstadoList.List.Where(x => x.Identificador == idEst).Select(x => new { x.Identificador, x.Nombre }).OrderBy(x => x.Nombre).ToList();
            //ViewBag.Estado1 = new SelectList(queryEs, "Identificador", "Name");

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

        #endregion FRACCIONES

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
    }
}