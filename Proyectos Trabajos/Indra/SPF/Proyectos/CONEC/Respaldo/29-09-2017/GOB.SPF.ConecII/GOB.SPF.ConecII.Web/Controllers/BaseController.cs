using GOB.SPF.ConecII.Entities.DTO;
using GOB.SPF.ConecII.Web.Models;
using GOB.SPF.ConecII.Web.Resources;
using GOB.SPF.ConecII.Web.Servicios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GOB.SPF.ConecII.Web.Controllers
{
    public class BaseController<TEntity> : Controller
        where TEntity : UiEntity, new()
    {
        

        public string ControllerName { get; set; }
        public string Titulo { get; set; }

        public string Ruta { get; set; }

        public static string RutaPlantilla { get { return "~/Areas/Plantilla/Views/"; } }

        public BaseServices<TEntity> clientService;

        public BaseController(string controllerName, string titulo, string ruta)
        {
            ControllerName = controllerName;
            Titulo = titulo;
            Ruta = ruta;
            clientService = new BaseServices<TEntity>(controllerName);
        }

        // GET: Base
        public virtual ActionResult Index()
        {
            return View();
        }


        public virtual JsonResult Consulta(int page, int rows)
        {
            UiResultPage<TEntity> uiResult = new UiResultPage<TEntity>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                
                uiResult.List = clientService.Obtener(page, rows);
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

        public virtual JsonResult ConsultaCriterio(UiResultPage<TEntity> model)
        {
            UiResultPage<TEntity> uiResult = new UiResultPage<TEntity>();
            uiResult.Result = UiEnum.TransactionResult.Failed;
            try
            {
                
                uiResult.List = clientService.ObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Paging.All, model.ObjectResult);
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
        public JsonResult Consulta(UiResultPage<TEntity> model)
        {
            UiResultPage<TEntity> uiResult = new UiResultPage<TEntity>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                
                uiResult.List = clientService.ObtenerPorId(model.ObjectResult);
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
        public virtual async Task<PartialViewResult> Item(TEntity model)
        {
            return AccionItem(model);
        }

        internal PartialViewResult AccionItem(TEntity model)
        {
            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear " + Titulo;
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar " + Titulo;
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle " + Titulo;
                    break;
            }

            return PartialView(Ruta + ControllerName + "/EditorTemplates/" + ControllerName + ".cshtml", model);
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual JsonResult Guardar(UiResultPage<TEntity> model)
        {
            UiResultPage<TEntity> uiResult = new UiResultPage<TEntity>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                
                uiResult.Result = clientService.Save(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result,0//,
                                                                                                                        //model.ObjectResult.Identificador
                                                                                                                        );
                uiResult.List = clientService.ObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Paging.All, model.ObjectSearch);
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
        public virtual JsonResult CambiarEstatus(UiResultPage<TEntity> model)
        {
            UiResultPage<TEntity> uiResult = new UiResultPage<TEntity>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                
                uiResult.Result = clientService.CambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result, 0//,
                                                                                                                                          //model.ObjectResult.Identificador
                                                                                                                        );
                uiResult.List = clientService.ObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Paging.All, model.ObjectSearch);
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
        public virtual JsonResult ConsultaList()
        {
            UiResultPage<DropDto> uiResult = new UiResultPage<DropDto>();
            uiResult.Result = UiEnum.TransactionResult.Failed;
            try
            {
                uiResult.List = clientService.ConsultaList();
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
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.FactorLeyIngresoUpdateSuccess : ErrorMessage.FailSaveMessage);
                else
                    mensaje = (Result == UiEnum.TransactionResult.Success ? NotificationMessage.FactorLeyIngresoInsertSuccess : ErrorMessage.FailSaveMessage);
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