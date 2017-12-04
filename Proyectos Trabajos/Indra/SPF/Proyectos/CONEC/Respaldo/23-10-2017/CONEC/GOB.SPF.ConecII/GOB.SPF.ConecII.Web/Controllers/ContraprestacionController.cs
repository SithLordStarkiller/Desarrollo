using GOB.SPF.ConecII.Web.Models;
using GOB.SPF.ConecII.Web.Resources;
using GOB.SPF.ConecII.Web.Servicios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GOB.SPF.ConecII.Web.Controllers
{
    [Authorize]
    public class ContraprestacionController : Controller
    {
        #region Cetes
        // GET: Contraprestacion
        public ActionResult Cetes()
        {
            ServicesContraprestacion client = new ServicesContraprestacion();
            ViewBag.Anios = new SelectList(client.ObtenerAniosCetes(), "Identificador", "Name");
            ViewBag.Meses = new SelectList(client.ObtenerMeses(), "Identificador", "Name");

            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> Cete()
        {
            UiCete cete = new UiCete { UniqueId = Guid.NewGuid() };
            return PartialView(cete);
        }

        [HttpPost]
        public JsonResult CetesConsulta(UiResultPage<UiCeteFiltro> model)
        {
            List<UiCete> cetes = new List<UiCete>();
            ServicesContraprestacion client = new ServicesContraprestacion();
            UiResultPage<UiCete> uiResult = new UiResultPage<UiCete>();
            uiResult.Result = UiEnum.TransactionResult.Failed;
            var filter = model.ObjectResult;

            try
            {
                // Obtenemos el Rango de fechas
                if (filter == null) filter = new UiCeteFiltro();

                if (filter?.Anio != null && filter.Mes != null)
                {
                    filter.FechaInicial = new DateTime(filter.Anio.Value, filter.Mes.Value, 1);
                    filter.FechaFinal = filter.FechaInicial?.AddMonths(1).AddDays(-1);
                }
                var result = client.CetesObtenerPorCriterio(filter, uiResult.Paging.Pages, uiResult.Paging.Rows);

                uiResult.List = result;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (ConecWebException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = e.Message;
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
        public JsonResult CetesGuardar(UiResultPage<UiCete> cetes)
        {
            UiResultPage<UiCete> uiResult = new UiResultPage<UiCete>();
            uiResult.Result = UiEnum.TransactionResult.Failed;
            try
            {
                ServicesContraprestacion clienteService = new ServicesContraprestacion();
                var saveList = cetes.List;
                bool result = clienteService.SaveCete(saveList);

                if (result)
                {
                    uiResult.List = clienteService.CetesObtenerPorCriterio(new UiCeteFiltro(), 1, 20);
                    if (cetes.Query != null)
                    {
                        //uiResult.List = clienteService.ObtenerClientes(model.Query);
                    }
                    else
                    {
                        //uiResult.List = clienteService.ObtenerClientes(1, 20);
                    }

                    uiResult.Paging.Pages = clienteService.Pages;
                    uiResult.Paging.Rows = 20;
                    uiResult.Paging.CurrentPage = 1;
                    uiResult.Result = UiEnum.TransactionResult.Success;
                }
            }
            catch (ConecWebException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = e.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = e.Message;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CetesPreview(UiCete model)
        {
            UiResultPage<UiCete> uiResult = new UiResultPage<UiCete>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            /* Aquí enviamos el archivo */
            var documento = model.Documento;
            try
            {
                /// Antes de enviar a guardar cargamos los archivos del cliente si es que tiene
                ServicesContraprestacion ceteService = new ServicesContraprestacion();

                if (documento.DocumentId.ToString() != (new Guid()).ToString())
                {
                    string path = Server.MapPath($"~/TemporaryFiles/{model.UniqueId}");
                    string filePath = Path.Combine(path, documento.DocumentId.ToString() + Path.GetExtension(documento.Nombre));
                    /* Cargamos el archivo que se guardó, lo metemos en base 64 y eso es lo que enviaremos al servicio a guardar xD */
                    Stream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    documento.Base64 = file.ToBase64();
                }

                // Envia a guardar el objeto del cliente
                var result = ceteService.ProcesarArchivoCetes(documento);
                if (result != null && result.Count > 0)
                {
                    try
                    {
                        uiResult.Result = UiEnum.TransactionResult.Success;
                        uiResult.List = result;
                    }
                    catch (UiException e)
                    {
                        EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                        uiResult.Message = e.Message;
                    }
                    catch (Exception e)
                    {
                        EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                        uiResult.Message = ErrorMessage.GenericMessage;
                    }
                }
            }
            catch (ConecWebException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = e.Message;
            }
            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CeteDocumentos(UiCliente cliente)
        {
            /* Metodo para obtener los documentos ya almacenados */
            return Json(new { });
        }
        #endregion

        #region Enteros Tesofe

        public ActionResult EnterosTesofe()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> EnteroTesofe()
        {
            UiEnteroTesofe cete = new UiEnteroTesofe { UniqueId = Guid.NewGuid() };
            return PartialView(cete);
        }
        [HttpPost]
        public JsonResult EnterosTesofeConsulta(UiResultPage<UiEnteroTesofeFiltro> model)
        {
            List<UiEnteroTesofe> enteros = new List<UiEnteroTesofe>();
            ServicesContraprestacion client = new ServicesContraprestacion();
            UiResultPage<UiEnteroTesofe> uiResult = new UiResultPage<UiEnteroTesofe>();
            uiResult.Result = UiEnum.TransactionResult.Failed;
            var filter = model.ObjectResult;

            try
            {
                if(filter == null) filter = new UiEnteroTesofeFiltro();
                var result = client.EnterosObtenerPorCriterio(filter, uiResult.Paging.Pages, uiResult.Paging.Rows);
                uiResult.List = result;
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            catch (ConecWebException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = e.Message;
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
        public JsonResult EnterosTesofeGuardar(UiResultPage<UiEnteroTesofe> enteros)
        {
            UiResultPage<UiEnteroTesofe> uiResult = new UiResultPage<UiEnteroTesofe>();
            uiResult.Result = UiEnum.TransactionResult.Failed;
            try
            {
                ServicesContraprestacion clienteService = new ServicesContraprestacion();
                var saveList = enteros.List;
                bool result = clienteService.SaveEntero(saveList);

                if (result)
                {
                    uiResult.List = clienteService.EnterosObtenerPorCriterio(new UiEnteroTesofeFiltro(), 1, 20);
                    if (enteros.Query != null)
                    {
                        //uiResult.List = clienteService.ObtenerClientes(model.Query);
                    }
                    else
                    {
                        //uiResult.List = clienteService.ObtenerClientes(1, 20);
                    }

                    uiResult.Paging.Pages = clienteService.Pages;
                    uiResult.Paging.Rows = 20;
                    uiResult.Paging.CurrentPage = 1;
                    uiResult.Result = UiEnum.TransactionResult.Success;
                }
            }
            catch (ConecWebException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = e.Message;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = e.Message;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EnterosTesofePreview(UiEnteroTesofe model)
        {
            UiResultPage<UiEnteroTesofe> uiResult = new UiResultPage<UiEnteroTesofe>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            /* Aquí enviamos el archivo */
            var documento = model.Documento;
            try
            {
                /// Antes de enviar a guardar cargamos los archivos del cliente si es que tiene
                ServicesContraprestacion ceteService = new ServicesContraprestacion();

                if (documento.DocumentId.ToString() != (new Guid()).ToString())
                {
                    string path = Server.MapPath($"~/TemporaryFiles/{model.UniqueId}");
                    string filePath = Path.Combine(path, documento.DocumentId.ToString() + Path.GetExtension(documento.Nombre));
                    /* Cargamos el archivo que se guardó, lo metemos en base 64 y eso es lo que enviaremos al servicio a guardar xD */
                    Stream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    documento.Base64 = file.ToBase64();
                }

                // Envia a guardar el objeto del cliente
                var result = ceteService.ProcesarArchivoEnterosTesofe(documento);
                if (result != null && result.Count > 0)
                {
                    try
                    {
                        uiResult.Result = UiEnum.TransactionResult.Success;
                        uiResult.List = result;
                    }
                    catch (UiException e)
                    {
                        EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                        uiResult.Message = e.Message;
                    }
                    catch (Exception e)
                    {
                        EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                        uiResult.Message = ErrorMessage.GenericMessage;
                    }
                }
            }
            catch (ConecWebException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = e.Message;
            }
            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EnterosTesofeDocumentos(UiCliente cliente)
        {
            /* Metodo para obtener los documentos ya almacenados */
            return Json(new { });
        }
        #endregion
    }
}