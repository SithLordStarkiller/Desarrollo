using GOB.SPF.ConecII.Web.Models;
using GOB.SPF.ConecII.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GOB.SPF.ConecII.Web.Servicios;
using System.Diagnostics;
using GOB.SPF.ConecII.Web.Resources;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities.DTO;

namespace GOB.SPF.ConecII.Web.Areas.Plantilla
{
    public class EtiquetasParrafoController : BaseController<UiEtiquetasParrafo>
    {
        #region Members

        public ServicesEtiquetasParrafo clientServiceEtiquetasParrafo;

        #endregion

        #region Constructor

        public EtiquetasParrafoController():base("EtiquetasParrafo", "Etiqueta del Parrafo", RutaPlantilla)
        {
            clientServiceEtiquetasParrafo = new ServicesEtiquetasParrafo("EtiquetasParrafo");
        }

        #endregion

        #region Actions

        public virtual ActionResult ObtenerPorParteDocumento(UiResultPage<UiEtiquetasParrafo> model)
        {


            UiResultPage<UiEtiquetasParrafo> uiResult = new UiResultPage<UiEtiquetasParrafo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            
            try
            {
                uiResult.List = clientServiceEtiquetasParrafo.ObtenerPorIdParteDocumento(model.Paging.CurrentPage, model.Paging.Rows, model.Paging.All, model.ObjectResult);
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

        public override async Task<PartialViewResult> Item(UiEtiquetasParrafo model)
        {
            BaseServices<UiParrafos> servicesParrafos = new BaseServices<UiParrafos>("Parrafos");

            UiParrafos parrafos = new UiParrafos() { IdParteDocumento = model.IdParteDocumento, Activo = true };
            List<DropDto> lstParrafos = servicesParrafos.ConsultaListCriterio(parrafos);
            ViewBag.lstParrafos = lstParrafos;

            return AccionItem(model);

        }


        [HttpPost]
        [AllowAnonymous]
        public JsonResult GuardarEtiqueta(UiResultPage<UiEtiquetasParrafo> model)
        {
            UiResultPage<UiEtiquetasParrafo> uiResult = new UiResultPage<UiEtiquetasParrafo>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {

                uiResult.Result = clientService.Save(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result, 0//,
                                                                                                                                          //model.ObjectResult.Identificador
                                                                                                                        );
                uiResult.List = clientServiceEtiquetasParrafo.ObtenerPorIdParteDocumento(model.Paging.CurrentPage, model.Paging.Rows, model.Paging.All, model.ObjectSearch);
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

        #region Drop

        #endregion

    }
}
