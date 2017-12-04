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
using GOB.SPF.ConecII.Web.Models;
using GOB.SPF.ConecII.Web.Servicios;
using System.Diagnostics;
using GOB.SPF.ConecII.Web.Resources;

namespace GOB.SPF.ConecII.Web.Controllers
{
    public class SolicitudController : Controller
    {
        // GET: Solicitud
        public ActionResult Clientes()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public JsonResult GuardarSolicitud(UiResultPage<UiSolicitud> model)
        {
            UiResultPage<UiSolicitud> uiResult = new UiResultPage<UiSolicitud>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                //uiResult.Result = clientService.SaveTiposServicio(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                //uiResult.List = clientService.ObtenerTiposServicio(model.Paging.CurrentPage, model.Paging.Rows);
                //uiResult.Paging.Pages = clientService.Pages;
                //uiResult.Paging.Rows = model.Paging.Rows;
                //uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
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


        public JsonResult ClientesConsulta(int page, int rows, UiResultPage<UiCliente> model)
        {
            UiResultPage<UiCliente> uiResult = new UiResultPage<UiCliente>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesSolicitud clientService = new ServicesSolicitud();

                if (model.Query != null)
                {
                    uiResult.List = clientService.ObtenerClientes(page, rows, model.Query);
                }
                else
                {
                    uiResult.List = clientService.ObtenerClientes(page, rows);
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

        public JsonResult ClienteConsultaCriterio(UiResultPage<UiCliente> model)
        {
            UiResultPage<UiCliente> uiResult = new UiResultPage<UiCliente>();
            uiResult.Result = UiEnum.TransactionResult.Failed;
            try
            {
                ServicesSolicitud clientService = new ServicesSolicitud();
                /*****************/
                uiResult.List = clientService.ObtenerClientes(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
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

        public JsonResult ClientesObtenerPorRazonSocial(string term)
        {
            UiResultPage<UiCliente> clienteList = new UiResultPage<UiCliente>();
            ServicesSolicitud clientService = new ServicesSolicitud();

            clienteList.List = clientService.ClientesObtenerPorRazonSocial(term);

            var query = clienteList.List.Select(x => new { id = x.Identificador, value = x.RazonSocial }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClientesObtenerPorNombreCorto(string term)
        {
            UiResultPage<UiCliente> clienteList = new UiResultPage<UiCliente>();
            ServicesSolicitud clientService = new ServicesSolicitud();

            clienteList.List = clientService.ClientesObtenerPorNombreCorto(term);

            var query = clienteList.List.Select(x => new { id = x.Identificador, value = x.NombreCorto }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        

       [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> Cliente(UiCliente model)
        {
            ServicesSolicitud clientServiceSolicitud = new ServicesSolicitud();
            ServicesCatalog clientServiceCatalog = new ServicesCatalog();

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear un nuevo cliente";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar cliente";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle del cliente";
                    break;
            }

            var regimenFiscalList = clientServiceCatalog.ObtenerRegimenFiscal(1, 1); //No importa sí se pide paginado ya que entrega toda la lista
            ViewBag.RegimenFiscalList = new SelectList(regimenFiscalList, "Identificador", "Name", model.IdRegimenFiscal);

            var sectorList = clientServiceCatalog.ObtenerSector();
            ViewBag.SectorList = new SelectList(sectorList, "Identificador", "Descripcion", model.IdSector);
            
            return PartialView(model);
        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Solicitantes(int idCliente)
        {
            //ServicesSolicitud clientServiceSolicitud = new ServicesSolicitud();
            //var solicitante = clientServiceSolicitud.ObtenerSolicitantes(model.Identificador);
            List<UiSolicitante> solicitanteList = new List<UiSolicitante>();
            solicitanteList.Add(new UiSolicitante { Identificador = 1, IdCliente = 1, Nombre = "Gerardo", ApellidoPaterno = "Ortiz", ApellidoMaterno = "Chavez" });

            return PartialView(solicitanteList);
        }

        [AllowAnonymous]
        public ActionResult Solicitante(UiSolicitante model)
        {
            return PartialView(model);
        }
    }
}