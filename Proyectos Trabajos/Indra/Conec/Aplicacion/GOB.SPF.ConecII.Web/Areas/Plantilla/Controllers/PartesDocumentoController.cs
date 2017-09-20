using GOB.SPF.ConecII.Web.Models;
using GOB.SPF.ConecII.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using GOB.SPF.ConecII.Web.Resources;
using GOB.SPF.ConecII.Web.Servicios;

namespace GOB.SPF.ConecII.Web.Areas.Plantilla
{
    public class PartesDocumentoController : BaseController<UiPartesDocumento>
    {
        #region Members

        public ServicesPartesDocumento clientServicePartesDocumento;


        #endregion

        #region Constructor

        public PartesDocumentoController() : base("PartesDocumento", "Secciones documento", RutaPlantilla)
        {
            clientServicePartesDocumento = new ServicesPartesDocumento("PartesDocumento");
        }

        #endregion

        #region Actions

        public virtual ActionResult Formato(int id)
        {
            
            
            UiResultPage<UiPartesDocumento> uiResult = new UiResultPage<UiPartesDocumento>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            UiPartesDocumento partesDocumento = new UiPartesDocumento() { IdTipoDocumento = id, Activo = true };
            try
            {
                partesDocumento = clientServicePartesDocumento.ObtenerPorIdTipoDocumento(partesDocumento);

                //uiResult.List = clientService.ObtenerPorId(partesDocumento);
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

            return View(partesDocumento);
        }


        [HttpPost]
        [AllowAnonymous]
        public virtual JsonResult GuardarFormato(UiResultPage<UiPartesDocumento> model)
        {
            UiResultPage<UiPartesDocumento> uiResult = new UiResultPage<UiPartesDocumento>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {

                uiResult.Result = clientService.Save(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result, 0//,
                                                                                                                                          //model.ObjectResult.Identificador
                                                                                                                        );
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

        /*
        [HttpPost]
        [AllowAnonymous]
        public virtual JsonResult GuardarImagen(UiResultPage<UiArchivo> model)
        {
            UiResultPage<int> uiResult = new UiResultPage<int>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {

                uiResult.Query = clientServicePartesDocumento.SaveImagen(model.ObjectResult);
                uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                                                                                                                        uiResult.Result, 0//,
                                                                                                                                          //model.ObjectResult.Identificador
                                                                                                                        );

                
                uiResult.Result = uiResult.Query.Equals(0) ? UiEnum.TransactionResult.Failed : UiEnum.TransactionResult.Success;
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
        */
        #endregion

        #region Drop

        #endregion

    }
}
