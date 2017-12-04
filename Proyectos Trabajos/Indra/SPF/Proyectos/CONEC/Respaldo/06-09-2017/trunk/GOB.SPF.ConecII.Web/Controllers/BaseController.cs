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
        BaseServices<TEntity> clientService;

        public BaseController(string controllerName)
        {
            ControllerName = controllerName;
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
                
                uiResult.List = clientService.ObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
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
        public virtual async Task<PartialViewResult> Index(TEntity model)
        {
            UiResultPage<UiActividad> actList = new UiResultPage<UiActividad>();

            

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



            var query = actList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            //ViewBag.IdActividad = new SelectList(query, "Identificador", "Name", idAc);

            return PartialView(model);
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
                uiResult.List = clientService.Obtener(model.Paging.CurrentPage, model.Paging.Rows);
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
                uiResult.List = clientService.ObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
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
    }
}