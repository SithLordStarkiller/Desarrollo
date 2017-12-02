using GOB.SPF.ConecII.Web.Models;
using GOB.SPF.ConecII.Web.Resources;
using GOB.SPF.ConecII.Web.Servicios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GOB.SPF.ConecII.Web.Models.Solicitud;

namespace GOB.SPF.ConecII.Web.Controllers
{
    [Authorize]
    public class SolicitudController : Controller
    {
        public int TipoRolUsuario { get { return 1; } }
        #region Clientes
        // GET: Solicitud vista inicial
        public ActionResult Clientes()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> Cliente(UiCliente model)
        {
            ServicesSolicitud clientServiceSolicitud = new ServicesSolicitud();
            ServicesCatalog clientServiceCatalog = new ServicesCatalog();
            UiCliente cliente = null;

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear un nuevo cliente";
                    break;
                case UiEnumEntity.Edit:
                case UiEnumEntity.View:
                    /* Buscamos al cliente completo desde Cliente hasta Teléfonos y Correos */
                    cliente = clientServiceSolicitud.ClienteObtenerPorId(model.Identificador);
                    cliente.Action = model.Action;
                    ViewBag.Title = model.Action == UiEnumEntity.Edit ? "Modificar cliente" : "Detalle del cliente";
                    break;
            }

            var regimenFiscalList = clientServiceCatalog.ObtenerRegimenFiscal(1, 1); //No importa sí se pide paginado ya que entrega toda la lista
            ViewBag.RegimenFiscalList = new SelectList(regimenFiscalList, "Identificador", "Name", model.IdRegimenFiscal);

            var sectorList = clientServiceCatalog.ObtenerSector();
            ViewBag.SectorList = new SelectList(sectorList, "Identificador", "Descripcion", model.IdSector);

            Guid UniqueId = Guid.NewGuid();
            if (cliente == null)
            {
                model.UniqueId = UniqueId;
                return PartialView(model);
            }
            else
            {
                cliente.UniqueId = UniqueId;
                return PartialView(cliente);
            }
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
                    uiResult.List = clientService.ClientesObtenerPorCriterio(model.Query);
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
                uiResult.List = clientService.ClientesObtenerPorCriterio(model.ObjectResult);
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

        public JsonResult ClienteConsultaPorCriterio(UiResultPage<UiCliente> model)
        {
            UiResultPage<UiCliente> uiResult = new UiResultPage<UiCliente>();
            uiResult.Result = UiEnum.TransactionResult.Failed;
            try
            {
                ServicesSolicitud clientService = new ServicesSolicitud();
                /*****************/
                uiResult.List = clientService.ClientesObtenerPorCriterio(model.ObjectResult);
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
        public JsonResult ClienteGuardar(UiResultPage<UiCliente> model)
        {
            var cliente = model.ObjectResult;
            UiResultPage<UiCliente> uiResult = new UiResultPage<UiCliente>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                /// Antes de enviar a guardar cargamos los archivos del cliente si es que tiene
                ServicesSolicitud clienteService = new ServicesSolicitud();
                if (cliente.Documentos?.Count > 0)
                    foreach (var documento in cliente.Documentos)
                    {
                        if (documento.DocumentId.ToString() != (new Guid()).ToString())
                        {
                            string path = Server.MapPath($"~/TemporaryFiles/{cliente.UniqueId}");
                            string filePath = Path.Combine(path, documento.DocumentId.ToString() + Path.GetExtension(documento.Nombre));
                            /* Cargamos el archivo que se guardó, lo metemos en base 64 y eso es lo que enviaremos al servicio a guardar xD */
                            Stream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            documento.Base64 = file.ToBase64();
                        }
                    }

                // Envia a guardar el objeto del cliente
                var result = clienteService.SaveCliente(cliente);
                if (result)
                {
                    try
                    {
                        ServicesSolicitud clientService = new ServicesSolicitud();

                        if (model.Query != null)
                        {
                            uiResult.List = clientService.ObtenerClientes(model.Query);
                        }
                        else
                        {
                            uiResult.List = clientService.ObtenerClientes(1, 20);
                        }

                        uiResult.Paging.Pages = clientService.Pages;
                        uiResult.Paging.Rows = 20;
                        uiResult.Paging.CurrentPage = 1;
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

                }
            }
            catch (ConecWebException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = e.Message;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ClienteCambiarEstatus(UiResultPage<UiCliente> model)
        {
            var cliente = model.ObjectResult;
            UiResultPage<UiCliente> uiResult = new UiResultPage<UiCliente>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            // Envia a guardar el objeto del cliente
            try
            {
                ServicesSolicitud clienteService = new ServicesSolicitud();
                var result = clienteService.ClienteCambiarEstatus(cliente);
                if (result)
                {
                    try
                    {
                        ServicesSolicitud clientService = new ServicesSolicitud();

                        if (model.Query != null)
                        {
                            uiResult.List = clientService.ObtenerClientes(model.Query);
                        }
                        else
                        {
                            uiResult.List = clientService.ObtenerClientes(1, 20);
                        }

                        uiResult.Paging.Pages = clientService.Pages;
                        uiResult.Paging.Rows = 20;
                        uiResult.Paging.CurrentPage = 1;
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

                }
            }
            catch (ConecWebException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = e.Message;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClienteDocumentos(UiCliente cliente)
        {
            /* Metodo para obtener los documentos ya almacenados */
            return Json(new { });
        }
        #endregion
        #region SOLICITUDES
        public ActionResult Solicitudes()
        {
            List<UiTiposServicio> Servicios = new List<UiTiposServicio>();

            ServicesCatalog clientService = new ServicesCatalog();

            Servicios = clientService.ObtenerTodosTiposServicio(1, 10);

            var query = Servicios.OrderBy(x => x.Name).ToList();
            ViewBag.TipoServicio = new SelectList(query, "Identificador", "Name");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> Solicitud(GOB.SPF.ConecII.Web.Models.Solicitud.UiSolicitud model)
        {
            //obtiene los datos de las listas de los combos y regresa la vista, evalua y almacena la seleccion de elementos de la pantalla actual.
            //UiResultPage<UiDivision> DivList = new UiResultPage<UiDivision>();
            //List<UiGrupo> GrupoList = new List<UiGrupo>();

            //var idDiv = model.IdDivision;
            //var idGru = model.IdGrupo;


            //ServicesCatalog clientService = new ServicesCatalog();
            //DivList.List = clientService.ObtenerDivisionesListado().Where(x => x.IsActive = true).ToList<UiDivision>();

            //if (idDiv != 0)
            //    GrupoList = clientService.GrupoObtenerPorIdDivisionFraccion(model);

            //var queryD = DivList.List.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();
            //var queryG = GrupoList.Select(x => new { x.Identificador, x.Name }).OrderBy(x => x.Name).ToList();

            //ViewBag.Grupoes = new SelectList(GrupoList, "Identificador", "Name", idGru);
            //ViewBag.Divisiones = new SelectList(DivList.List, "Identificador", "Name", idDiv);

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Solicitud";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar solicitud";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle de la solicitud";

                    break;
            }
            return PartialView(model);
        }


        public JsonResult SolicitudConsulta(int page, int rows, UiResultPage<GOB.SPF.ConecII.Web.Models.Solicitud.UiSolicitud> model)
        {
            UiResultPage<GOB.SPF.ConecII.Web.Models.Solicitud.UiSolicitud> uiResult = new UiResultPage<GOB.SPF.ConecII.Web.Models.Solicitud.UiSolicitud>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();

                if (model.Query != null)
                {
                    //llama el metodo que obtiene las solicitudes en base a un criterio de busqueda
                    // uiResult.List = clientService.ObtenerFraccionesPorCriterio(page, rows, model.Query);
                }
                else
                {
                    //si no hay criterios de busqueda, se obtienen todas las solicitudes
                    //uiResult.List = clientService.ObtenerFracciones(page, rows);
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


        public JsonResult SolicitudConsultaCriterio(UiResultPage<GOB.SPF.ConecII.Web.Models.Solicitud.UiSolicitud> model)
        {
            UiResultPage<GOB.SPF.ConecII.Web.Models.Solicitud.UiSolicitud> uiResult = new UiResultPage<GOB.SPF.ConecII.Web.Models.Solicitud.UiSolicitud>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                //ejecuta la busqueda de solicitudes por criterio
                //ServicesCatalog clientService = new ServicesCatalog();
                //uiResult.List = clientService.ObtenerFraccionesPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
                //uiResult.Paging.Pages = clientService.Pages;
                //uiResult.Paging.Rows = model.Paging.Rows;
                //uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                //uiResult.Result = UiEnum.TransactionResult.Success;
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
        public JsonResult SolicitudGuardar(UiResultPage<GOB.SPF.ConecII.Web.Models.Solicitud.UiSolicitud> model)
        {
            UiResultPage<GOB.SPF.ConecII.Web.Models.Solicitud.UiSolicitud> uiResult = new UiResultPage<GOB.SPF.ConecII.Web.Models.Solicitud.UiSolicitud>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                //ejecujta metodo para guardar la solicitud
                //ServicesCatalog clientService = new ServicesCatalog();
                //uiResult.Result = clientService.SaveFracciones(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                //uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                //                                                                                                        uiResult.Result,
                //                                                                                                        model.ObjectResult.Identificador);

                //obtiene la lista de solicitudes para devolverlas a la vista
                //uiResult.List = clientService.ObtenerFracciones(model.Paging.CurrentPage, model.Paging.Rows);
                //uiResult.Paging.Pages = clientService.Pages;
                //uiResult.Paging.Rows = model.Paging.Rows;
                //uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                //uiResult.Result = UiEnum.TransactionResult.Success;
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
        public JsonResult SolicitudCambiarEstatus(UiResultPage<GOB.SPF.ConecII.Web.Models.Solicitud.UiSolicitud> model)
        {
            UiResultPage<GOB.SPF.ConecII.Web.Models.Solicitud.UiSolicitud> uiResult = new UiResultPage<GOB.SPF.ConecII.Web.Models.Solicitud.UiSolicitud>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                //ejecuta metodo para realizar el cambio de estatus y obtiene el listado de solicitudes para refrescar la pagina
                //ServicesCatalog clientService = new ServicesCatalog();
                //uiResult.Result = clientService.FraccionCambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                //uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                //                                                                                                        uiResult.Result,
                //                                                                                                        model.ObjectResult.Identificador,
                //                                                                                                        model.ObjectResult.IsActive);
                //uiResult.List = clientService.ObtenerFraccionesPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.Query);
                //uiResult.Paging.Pages = clientService.Pages;
                //uiResult.Paging.Rows = model.Paging.Rows;
                //uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                //uiResult.Result = UiEnum.TransactionResult.Success;
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
        #region REGISTROS
        // GET: Solicitud vista inicial
        public ActionResult Registros()
        {
            List<UiCliente> clientes = new List<UiCliente>();
            List<UiSector> sector = new List<UiSector>();
            List<UiRegimenFiscal> regimenfiscal = new List<UiRegimenFiscal>();
            ServicesSolicitud clientService = new ServicesSolicitud();
            ServicesCatalog catalogService = new ServicesCatalog();

            clientes = clientService.ObtenerClientes(1, 20);
            sector = catalogService.ObtenerSector();
            regimenfiscal = catalogService.ObtenerRegimenFiscal(1, 20);
            ViewBag.RazonSocial = new SelectList(clientes.OrderBy(x => x.RazonSocial), "Identificador", "RazonSocial");
            ViewBag.NombreCorto = new SelectList(clientes.OrderBy(x => x.NombreCorto), "Identificador", "NombreCorto");
            ViewBag.RFC = new SelectList(clientes.OrderBy(x => x.RFC), "Identificador", "RFC");
            ViewBag.RegimenFiscal = new SelectList(regimenfiscal.OrderBy(x => x.Name), "Identificador", "Name");
            ViewBag.Sector = new SelectList(sector.OrderBy(x => x.Descripcion), "Identificador", "Descripcion");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public PartialViewResult ServicioCaptura(long idTipoServicio)
        {
            List<UiCuota> concepto = new List<UiCuota>();
            List<UiTipoInstalacion> tipoInstalacion = new List<UiTipoInstalacion>();
            ServicesSolicitud clientService = new ServicesSolicitud();
            ServicesCatalog catalogService = new ServicesCatalog();
            ServiceConfiguration configuracion = new ServiceConfiguration();

            concepto = catalogService.ObtenerCuotasConceptos(idTipoServicio);
            tipoInstalacion = clientService.ObtenerTipoInstalacion(1, 20);
            ViewBag.Concepto = new SelectList(concepto.OrderBy(x => x.Concepto), "Identificador", "Concepto");
            ViewBag.TipoInstalacion = new SelectList(tipoInstalacion.OrderBy(x => x.Nombre), "Identificador", "Nombre");

            // TODO: Consultar la configuracion del Tipo de Servicio y Agregarlo al model en la propiedad ConfiguracionCampos
            var modelo = new UiSolicitudServicio();
            modelo.ConfiguracionCampos = new UiCampoServicio();

            modelo.ConfiguracionCampos = configuracion.ObtenerServicioPorIdTipoServicio((int)idTipoServicio);



            return PartialView(modelo);
        }
        
        [HttpPost, AllowAnonymous]
        public JsonResult SolicitudInstalaciones(UiSolicitud solicitud)
        {
            ServicesSolicitud service = new ServicesSolicitud();
            var instalaciones = service.InstalacionObtenerPorIdSolicitud(solicitud).ToList();

            return Json(instalaciones, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> Registro(UiCliente model)
        {

            ServicesSolicitud clientServiceSolicitud = new ServicesSolicitud();
            ServicesCatalog catalogService = new ServicesCatalog();
            List<UiSector> sector = new List<UiSector>();
            List<UiRegimenFiscal> regimenfiscal = new List<UiRegimenFiscal>();
            List<UiTiposServicio> tiposervicio = new List<UiTiposServicio>();
            UiCliente cliente = null;
            List<Models.Solicitud.UiSolicitud> solicitud = null;

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Nuevo Registro";
                    break;
                case UiEnumEntity.Add:
                case UiEnumEntity.Edit:
                case UiEnumEntity.View:
                    /* Buscamos al cliente completo desde Cliente hasta Teléfonos y Correos */
                    cliente = clientServiceSolicitud.ClienteSolicitudObtenerPorId(model.Identificador);
                    cliente.Action = model.Action;
                    ViewBag.Title = model.Action == UiEnumEntity.Add ? "Agregar Solicitud del Cliente" : model.Action == UiEnumEntity.Edit ? "Modificar Cliente" : "Detalle del Cliente";
                    break;
            }

            tiposervicio = catalogService.ObtenerTiposServicio(1, 20);
            sector = catalogService.ObtenerSector();
            regimenfiscal = catalogService.ObtenerRegimenFiscal(1, 20);
            ViewBag.TipoServicio = new SelectList(tiposervicio.OrderBy(x => x.Name), "Identificador", "Name");
            ViewBag.RegimenFiscal = new SelectList(regimenfiscal.OrderBy(x => x.Name), "Identificador", "Name", model.IdRegimenFiscal);
            ViewBag.Sector = new SelectList(sector.OrderBy(x => x.Descripcion), "Identificador", "Descripcion", model.IdSector);

            Guid UniqueId = Guid.NewGuid();
            if (cliente == null)
            {
                model.UniqueId = UniqueId;
                return PartialView(model);
            }
            else
            {
                cliente.UniqueId = UniqueId;
                return PartialView(cliente);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public PartialViewResult ModificarSolicitud()
        {
            ViewBag.Title = "Solicitud por Modificación de Contrato";
            return PartialView();
        }

        [HttpPost]
        public JsonResult ModificarSolicitudes(int Identificador)
        {
            UiResultPage<Models.Solicitud.UiSolicitud> uiResult = new UiResultPage<Models.Solicitud.UiSolicitud>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesSolicitud solicitud = new ServicesSolicitud();
                uiResult.List = solicitud.ClienteListaSolicitudObtenerPorId(Identificador);
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
        public JsonResult SaveSolicitud(UiResultPage<UiCliente> model)
        {
            var cliente = model.ObjectResult;
            UiResultPage<UiCliente> uiResult = new UiResultPage<UiCliente>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            // Envia a guardar el objeto del cliente
            try
            {
                ServicesSolicitud clienteService = new ServicesSolicitud();
                var result = clienteService.SaveCliente(cliente);
                if (result)
                {
                    try
                    {
                        ServicesSolicitud clientService = new ServicesSolicitud();

                        if (model.Query != null)
                        {
                            uiResult.List = clientService.ObtenerClientes(model.Query);
                        }
                        else
                        {
                            uiResult.List = clientService.ObtenerClientes(1, 20);
                        }

                        uiResult.Paging.Pages = clientService.Pages;
                        uiResult.Paging.Rows = 20;
                        uiResult.Paging.CurrentPage = 1;
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

                }
            }
            catch (ConecWebException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = e.Message;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerServicio(UiCliente cliente)
        {

            return Json(new { });
        }

        [HttpPost]
        public JsonResult AgregarServicio(int Id, string Nombre)
        {
            UiTiposServicio servicio = new UiTiposServicio();

            bool isSerivice = false;
            string message = "Ningún archivo fue cargado.";

            if (Nombre != null && Nombre != "")
            {
                try
                {
                    isSerivice = true;
                    message = "Servicio cargado satisfactoriamente!";

                    servicio.Identificador = Id;
                    servicio.Name = Nombre;
                    servicio.IsActive = true;
                }
                catch (Exception ex)
                {
                    Nombre = string.Empty;
                    isSerivice = true;
                    message = $"La carga del servicio falló: {ex.Message}";
                }
            }

            return Json(new { isSerivice = isSerivice, message = message, servicio = servicio });
        }

        #endregion

        #region Autocomplete Clientes
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

        public JsonResult ClientesObtenerPorRFC(string term)
        {
            UiResultPage<UiCliente> clienteList = new UiResultPage<UiCliente>();
            ServicesSolicitud clientService = new ServicesSolicitud();

            clienteList.List = clientService.ObtenerClientes(1, 20);

            var query = clienteList.List.Where(y => y.RFC.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Identificador, value = x.RFC }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Domicilio Fiscal
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Domicilio(UiCliente model)
        {
            ServicesSolicitud clientServiceSolicitud = new ServicesSolicitud();
            ServicesCatalog serviceCatalog = new Servicios.ServicesCatalog();
            UiDomicilioFiscal domicilioFiscal = new UiDomicilioFiscal();
            domicilioFiscal = clientServiceSolicitud.ObtenerDomicilioFiscal(model.Identificador);
            domicilioFiscal.Action = model.Action;

            if (model.Action != UiEnumEntity.View)
                ViewBag.Estados = new SelectList(serviceCatalog.ObtenerEstado(1, 40), "Identificador", "Nombre");

            return PartialView(domicilioFiscal);
        }
        #endregion

        #region Solicitantes
        [HttpPost]
        [AllowAnonymous]
        public PartialViewResult Solicitante(UiSolicitante solicitante)
        {
            return PartialView(solicitante);
        }
        #endregion

        #region Contactos
        [HttpPost]
        [AllowAnonymous]
        public PartialViewResult Contacto(UiClienteContacto contacto)
        {
            ServicesCatalog client = new ServicesCatalog();
            ViewBag.TiposContactos = (new SelectList(client.TiposContacto(), "Identificador", "Name", contacto.Identificador));
            return PartialView(contacto);
        }
        #endregion

        #region Telefonos
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Telefonos()
        {
            ServicesCatalog client = new ServicesCatalog();
            var telefono = new UiTelefono();
            var tiposTelefono = client.TiposTelefono();

            ViewBag.TiposTelefono = new SelectList(tiposTelefono, "Identificador", "Name", "Seleccione");
            return PartialView(new UiTelefono());
        }
        #endregion

        #region Correos
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Correos()
        {
            return PartialView(new UiCorreo());
        }
        #endregion

        #region Instalaciones

        public ActionResult Instalaciones()
        {
            ViewBag.Tittle = "Instalacion";
            return View();
        }
        [HttpPost]
        public PartialViewResult Instalacion(UiCliente model)
        {
            ViewBag.Editable = model.Action.Equals(UiEnumEntity.View) ? " disabled = disabled" : "";
            ViewBag.Visible = model.Action.Equals(UiEnumEntity.View) ? " hidden = hidden " : "";

            var client = new ServicesCatalog();

            var taskDiviciones = Task.Factory.StartNew(() =>
            {
                var listaDivicion = new List<UiDivision>
                {
                    new UiDivision { Identificador = 0, Name = "Selecciona una division"}
                };

                var resultGrupos = client.ObtenerDivisiones(1, 20);

                listaDivicion.AddRange(resultGrupos);

                var divisiones = listaDivicion.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name,
                    Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.Divisiones = divisiones;
            });

            var taskGrupos = Task.Factory.StartNew(() =>
            {
                var listaGrupos = new List<UiGrupo>
                {
                    new UiGrupo { Identificador = 0, Name = "Selecciona un grupo"}
                };

                var resultGrupos = client.GrupoObtener(1, 20);

                listaGrupos.AddRange(resultGrupos);

                var grupos = listaGrupos.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name,
                    Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.Grupos = grupos;
            });

            var taskFracciones = Task.Factory.StartNew(() =>
            {
                var listaFracciones = new List<UiFracciones>
                {
                    new UiFracciones { Identificador = 0, Nombre = "Selecciona una fraccion"}
                };

                var resultFracciones = client.ObtenerTodosFracciones(1, 20);

                listaFracciones.AddRange(resultFracciones);

                var fracciones = listaFracciones.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Nombre,
                    Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.Fracciones = fracciones;
            });

            var taskZonas = Task.Factory.StartNew(() =>
            {
                var zona = new List<UiFases>
                {
                    new UiFases {Identificador = 0, Name = "Selecciona una Zona"}
                };

                var lista = client.ObtenerFases(1, 100000);

                zona.AddRange(lista);

                var listaZonas = zona.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name,
                    Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.ListaZonas = listaZonas;
            });

            var taskEstacion = Task.Factory.StartNew(() =>
            {
                var estacion = new List<UiEstaciones>
                {
                    new UiEstaciones() {IdEstacion = 0, Nombre = "Selecciona una estacion"}
                };

                var lista = client.ObtenerEstaciones(1, 100000);

                estacion.AddRange(lista);

                var listaEstacion = estacion.Select(c => new SelectListItem
                {
                    Value = c.IdEstacion.ToString(),
                    Text = c.Nombre,
                    Selected = c.IdEstacion == 0
                }).ToArray();

                ViewBag.ListaEstacion = listaEstacion;
            });

            var taskTipoInstalacion = Task.Factory.StartNew(() =>
            {
                var tipoInstalacion = new List<UiTipoInstalacion>
                {
                    new UiTipoInstalacion {Identificador = 0, Nombre = "Selecciona una estacion"}
                };

                var lista = client.TipoInstalacionObtener();

                tipoInstalacion.AddRange(lista);

                var listaTiposInstalacion = tipoInstalacion.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Nombre,
                    Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.ListaTiposInstalacion = listaTiposInstalacion;
            });

            var taskEstado = Task.Factory.StartNew(() =>
            {
                var estado = new List<UiEstado>
                {
                    new UiEstado {Identificador = 0, Nombre = "Selecciona un estado"}
                };

                var municipio = new List<UiMunicipio>
                {
                    new UiMunicipio {Identificador = 0, Nombre = "Selecciona un Municipio"}
                };

                var asentamiento = new List<UiAsentamiento>
                {
                    new UiAsentamiento {Identificador = 0, Nombre = "Selecciona un asentamiento"}
                };

                var lista = client.ObtenerEstado(1, 100000);

                estado.AddRange(lista);

                var listaEstados = estado.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Nombre,
                    Selected = c.Identificador == 0
                }).ToArray();

                if (model.Action.Equals(UiEnumEntity.Edit) || model.Action.Equals(UiEnumEntity.View))
                {
                    var asentamientoModel = model.Instalaciones.FirstOrDefault().Asentamiento;

                    municipio.AddRange(
                        client.ObtenerMunicipios(asentamientoModel.Estado.Identificador));

                    asentamiento.AddRange(
                        client.ObtenerAsentamientos(asentamientoModel));
                }

                var listaMunicipios = municipio.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Nombre,
                    //Selected = c.Identificador == 0
                }).ToArray();

                var listaAsentamientos = asentamiento.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Nombre,
                    //Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.ListaEstados = listaEstados;
                ViewBag.ListaMunicipios = listaMunicipios;
                ViewBag.ListaAsentamientos = listaAsentamientos;
            });

            var resultados = new[] { taskZonas, taskEstacion, taskFracciones, taskGrupos, taskDiviciones, taskTipoInstalacion, taskEstado };

            var instalacion = new UiInstalacion();

            if (model.Action.Equals(UiEnumEntity.Edit) || model.Action.Equals(UiEnumEntity.View))
            {
                if (model.Instalaciones.FirstOrDefault() != null)
                {
                    instalacion = new ServicesSolicitud().InstalacionObtenerPorId(model.Instalaciones.FirstOrDefault());
                }
            }

            instalacion.IdCliente = model.Identificador;
            instalacion.NombreCorto = model.NombreCorto;
            instalacion.RazonSocial = model.RazonSocial;


            Task.WaitAll(resultados);

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    instalacion.Action = UiEnumEntity.New;
                    ViewBag.Tittle = "Crear nueva instalacion";
                    break;
                case UiEnumEntity.Edit:
                    instalacion.Action = UiEnumEntity.Edit;
                    ViewBag.Tittle = "Modificar la instalacion";
                    break;
                case UiEnumEntity.View:
                    instalacion.Action = UiEnumEntity.View;
                    ViewBag.Tittle = "Detalle de la instalacion";
                    ViewBag.Editable = model.Action.Equals(UiEnumEntity.View) ? "\"disabled\"" : "\"false\"";
                    ViewBag.Visible = model.Action.Equals(UiEnumEntity.View) ? " hidden = hidden " : "";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ViewBag.Cliente = client;

            return PartialView(instalacion);
        }

        public JsonResult InstalacionConsulta(UiResultPage<UiCliente> cliente)
        {

            var uiResult = new UiResultPage<UiCliente> { Result = UiEnum.TransactionResult.Failed };
            var clientes = new List<UiCliente>();

            if (!cliente.Query.Instalaciones.Any())
            {
                clientes = new ServicesSolicitud().InstalacionObtenerTodos(1, 1000);

                //Parallel.ForEach(clientes, client =>
                //{
                //    var inst = client.Instalaciones;

                //    inst.Remove(inst.Find(x => x.Identificador == 0));
                //});
            }
            else
            {
                var filtro = new UiCliente
                {
                    RFC = cliente.Query.RFC,
                    NombreCorto = cliente.Query.NombreCorto,
                    RazonSocial = cliente.Query.RazonSocial,

                    Instalaciones = new List<UiInstalacion>
                    {
                        new UiInstalacion
                        {
                            Nombre = cliente.Query.Instalaciones.FirstOrDefault().Nombre,
                            Activo = cliente.Query.Instalaciones.FirstOrDefault().Activo
                        }
                    }
                };

                clientes = new ServicesSolicitud().InstalacionObtenerPorCriterio(filtro);

                //Parallel.ForEach(clientes, client =>
                //{
                //    var inst = client.Instalaciones;

                //    inst.Remove(inst.Find(x => x.Identificador == 0));
                //});
            }
            try
            {
                uiResult.List = clientes;
                uiResult.Paging = uiResult.Paging;

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
        public ActionResult TelefonosInstalacion()
        {
            var client = new ServicesCatalog();
            var tiposTelefono = client.TiposTelefono();

            var listaTiposTelefono = tiposTelefono.Select(c => new SelectListItem
            {
                Value = c.Identificador.ToString(),
                Text = c.Name,
                Selected = c.Identificador == 0
            }).ToArray();

            ViewBag.TiposTelefono = listaTiposTelefono;

            return PartialView(new UiTelefonoInstalacion());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult CorreosInstalacion()
        {
            return PartialView(new UiCorreoInstalacion());
        }

        public JsonResult InstalacionGuardar(UiInstalacion model)
        {
            var cliente = new UiCliente
            {
                Identificador = model.Cliente.Identificador,
                Instalaciones = new List<UiInstalacion> { model }
            };
            var uiResult = new UiResultPage<UiInstalacion>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            // Envia a guardar el objeto del cliente

            try
            {
                var clienteService = new ServicesSolicitud();
                var result = clienteService.InstalacionGuardar(cliente);
                if (result)
                {
                    try
                    {
                        uiResult.Result = UiEnum.TransactionResult.Success;
                        uiResult.Message = "Se guardo correctamente la configuracion";
                    }
                    catch (UiException e)
                    {
                        EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                        uiResult.Message = "Ocurrio un error interno";
                    }
                    catch (Exception e)
                    {
                        EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                        uiResult.Message = "Ocurrio un error interno";
                    }

                }
                else
                {
                    uiResult.Result = UiEnum.TransactionResult.Failed;
                    uiResult.Message = "No fue podible guardar la configuracion correctamente";
                }
            }
            catch (ConecWebException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = e.Message;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InstalacionCambiarEstatus(UiResultPage<UiInstalacion> model)
        {

            var uiResult = new UiResultPage<UiInstalacion> { Result = UiEnum.TransactionResult.Failed };

            try
            {
                var clienteService = new ServicesSolicitud();
                var result = clienteService.InstalacionCambiarEstatus(model.ObjectResult);
                if (result)
                {
                    try
                    {
                        uiResult.Result = UiEnum.TransactionResult.Success;
                        uiResult.Message = "Se modifico el estatus de la configuracion";
                    }
                    catch (UiException e)
                    {
                        EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                        uiResult.Message = "Ocurrio un error interno";
                    }
                    catch (Exception e)
                    {
                        EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                        uiResult.Message = "Ocurrio un error interno";
                    }

                }
                else
                {
                    uiResult.Result = UiEnum.TransactionResult.Failed;
                    uiResult.Message = "No fue podible modificar la configuracion correctamente";
                }
            }
            catch (ConecWebException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = e.Message;
            }

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SolicitudContraprestacion

        public ActionResult Contraprestaciones()
        {
            ServicesCatalog clientServiceCatalog = new ServicesCatalog();
            var cuotas = clientServiceCatalog.ObtenerCuotas(1, 1000);

            return View(cuotas);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GuardarSolicitudContraprestacion(UiResultPage<Models.Solicitud.UiSolicitud> model)
        {
            UiResultPage<Models.Solicitud.UiSolicitud> uiResult = new UiResultPage<Models.Solicitud.UiSolicitud>();
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

        #endregion

        #region Solicitud Servicios

        public ActionResult SolicitudServicioFormularios1()
        {
            UiConfiguracionTipoServicioArea uiConfiguracion = new UiConfiguracionTipoServicioArea();
            return View(uiConfiguracion);
        }

        public ActionResult SolicitudesServicio()
        {
            return View();
        }

        public JsonResult SolicitudesServicioConsulta(int page, int rows)
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

        public JsonResult SolicitudServicioConsultaCriterio(UiResultPage<UiCuota> model)
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
        public async Task<PartialViewResult> SolicitudServicio(UiCuota model)
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
        public JsonResult SolicitudServicioGuardar(UiResultPage<UiCuota> model)
        {
            UiResultPage<UiCuota> uiResult = new UiResultPage<UiCuota>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveCuotas(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                //uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                //                                                                                                        uiResult.Result,
                //                                                                                                        model.ObjectResult.Identificador);
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
        public JsonResult SolicitudServicioCambiarEstatus(UiResultPage<UiCuota> model)
        {
            UiResultPage<UiCuota> uiResult = new UiResultPage<UiCuota>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.CuotaCambiarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ObtenerCuotas(model.Paging.CurrentPage, model.Paging.Rows);
                //uiResult.Message = !string.IsNullOrEmpty(uiResult.Message) ? uiResult.Message : ObtenerMensajeOperacion(model.ObjectResult.GetType().Name,
                //                                                                                                        uiResult.Result,
                //                                                                                                        model.ObjectResult.Identificador);

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

        #endregion Solicitud Servicios
        
        #region TestSolicitud

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> GuardarSolicitud(UiResultPage<Models.Solicitud.UiSolicitud> solicitud)
        {
            var sol = solicitud.ObjectResult;
            UiResultPage<Models.Solicitud.UiSolicitud> uiResult = new UiResultPage<Models.Solicitud.UiSolicitud>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesSolicitud solicitudServicio = new ServicesSolicitud();

                if (sol.Documento.DocumentId != null)
                {
                    string path = Server.MapPath($"~/TemporaryFiles/{sol.Cliente.UniqueId}");
                    string filePath = Path.Combine(path, sol.Documento.DocumentId.ToString() + Path.GetExtension(sol.Documento.Nombre));
                    /* Cargamos el archivo que se guardó, lo metemos en base 64 y eso es lo que enviaremos al servicio a guardar xD */
                    Stream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    sol.Documento.Base64 = file.ToBase64();
                    sol.Documento.TipoDocumento = new UiTiposDocumento { Identificador = 1 };
                    sol.Documento.FechaRegistro = System.DateTime.Now;
                }
                foreach (var servicio in sol.Servicios)
                {
                    foreach (var documentos in servicio.Documentos)
                    {
                        if (documentos.DocumentId != null)
                        {
                            string path = Server.MapPath($"~/TemporaryFiles/{sol.Cliente.UniqueId}");
                            string filePath = Path.Combine(path, documentos.DocumentId.ToString() + Path.GetExtension(documentos.Nombre));
                            /* Cargamos el archivo que se guardó, lo metemos en base 64 y eso es lo que enviaremos al servicio a guardar xD */
                            Stream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            documentos.Base64 = file.ToBase64();
                            documentos.TipoDocumento = new UiTiposDocumento { Identificador = 1 };
                            documentos.FechaRegistro = System.DateTime.Now;
                        }
                    }
                }

                var result = await solicitudServicio.Guardar(sol);
                if (result.Success)
                {
                    try
                    {
                        ServicesSolicitud clientService = new ServicesSolicitud();

                        //uiResult.List = clientService.ObtenerClientes(1, 20);

                        uiResult.Paging.Pages = clientService.Pages;
                        uiResult.Paging.Rows = 20;
                        uiResult.Paging.CurrentPage = 1;
                        uiResult.Result = UiEnum.TransactionResult.Success;
                    }
                    catch (UiException e)
                    {
                        EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                        uiResult.Message = ErrorMessage.GenericMessage + " " + e.Message;
                    }
                    catch (Exception e)
                    {
                        EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                        uiResult.Message = ErrorMessage.GenericMessage + " " + e.Message;
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

        #endregion

        #region [Complemento de Solicitud]
        public ActionResult ComplementarSolicutudLista()
        {
            var services = new ServicesCatalog();

            var estatus = services.EstatusObtenerPorCriterio(2);

            var listaEstatus = new List<SelectListItem>();

            listaEstatus.Add(new SelectListItem() { Value = "-1", Text = "Todos" });
            listaEstatus.AddRange(estatus.Select(p => new SelectListItem()
            {
                Value = p.Identificador.ToString(),
                Text = p.Nombre
            }));
            ViewBag.Estatuses = listaEstatus;
            ViewBag.TipoRolUsuario = TipoRolUsuario;

            return View();
        }


        public JsonResult ComplementarSolicutudListaGrid(UiResultPage<Models.Solicitud.UiSolicitud> model)
        {
            var result = new List<Models.Solicitud.UiSolicitud>();
            var uiResult = new UiResultPage<Models.Solicitud.UiSolicitud>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesSolicitud srv = new ServicesSolicitud();
                result = srv.ServiciObtener(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.List = result;
                uiResult.Paging.Pages = srv.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;
                ViewBag.TipoRolUsuario = TipoRolUsuario;
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

        /// <summary>
        /// Complemento de Solicitud
        /// 
        /// ABC 09-Sept-2017
        /// </summary>
        /// <returns></returns>
        // GET: Complemento de Solicitud
        public ActionResult ComplementoSolicitud(UiResultPage<Models.Solicitud.UiSolicitud> model)
        {
            var solicitud = new GOB.SPF.ConecII.Web.Models.UiSolicitudComplemento()
            {
                IdSolicitud = 1,
                RazonSocial = "RRRRRRR",
                RFC = "RFC0000",
                TipoServicio = "TSTSTST",
                NumInstalaciones = 3,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now,
                TipoInstalaciones = "kjdlajdkl jasl",
                HorasDuracion = 4
            };
            var solicitudInstalacion = new List<SolicitudInstalacion>();
            solicitudInstalacion.Add(new SolicitudInstalacion()
            {
                IdSolicitudInstalacion = -1,
                IdInstalacion = 1,
                Nombre = "Install 1",
                Direccion = "Dir 1",
                Seleccionado = false
            });
            solicitudInstalacion.Add(new SolicitudInstalacion()
            {
                IdSolicitudInstalacion = -1,
                IdInstalacion = 2,
                Nombre = "Install 2",
                Direccion = "Dir 2",
                Seleccionado = true
            });
            solicitudInstalacion.Add(new SolicitudInstalacion()
            {
                IdSolicitudInstalacion = -1,
                IdInstalacion = 3,
                Nombre = "Install 3",
                Direccion = "Dir 3",
                Seleccionado = false
            });
            solicitud.Instalaciones = solicitudInstalacion;
            solicitud.NumInstalaciones = solicitudInstalacion.Count();

            var signatarios = new List<Signatario>();

            signatarios.Add(new Signatario()
            {
                IdSignatario = 1,
                NombreCompleto = "Signatario 1",
                AreaFuncional = "Área 1",
                Cargo = "Cargo 1",
                EsActivo = true
            });
            signatarios.Add(new Signatario()
            {
                IdSignatario = 2,
                NombreCompleto = "Signatario 2",
                AreaFuncional = "Área 2",
                Cargo = "Cargo 2",
                EsActivo = true
            });
            signatarios.Add(new Signatario()
            {
                IdSignatario = 3,
                NombreCompleto = "Signatario 3",
                AreaFuncional = "Área 3",
                Cargo = "Cargo 3",
                EsActivo = true
            });

            solicitud.Signatarios = signatarios;

            var factorPorInstalacion = new List<FactorPorInstalacion>();

            factorPorInstalacion.Add(new FactorPorInstalacion()
            {
                IdInstalaccion = 1,
                Nombre = "Instalación 1",
                Division = "División 1",
                Grupo = "Grupo 1",
                Fraccion = "Fracción 1",
                Actividad = "Actividad 1",
                Distancia = "Distancia 1",
                Criminalidad = "Criminalidad 1"
            });

            var estadoFuerza = new List<EstadoFuerza>();
            estadoFuerza.Add(new EstadoFuerza()
            {
                Concepto = "Concepto 1",
                Descripcion = "Descripción 1",
                Turnos = "24x24",
                Cantidad = 23,
                Lunes = 23,
                Martes = 23,
                Miercoles = 23,
                Jueves = 23,
                Viernes = 23,
                Sabado = 23,
                Domingo = 23
            });

            var suministros = new List<Suministro>();
            suministros.Add(new Suministro()
            {
                Concepto = "Concepto 1",
                Turnos = "24x24",
                Cantidad = 1,
                Masculinos = 1,
                Femeninos = 1,
                Indistintos = 1,
                ArmaLarga = 1,
                ArmaCorta = 1,
                Municiones = 1,
                VestuarioUniforme = 1,
                VestuarioMediaGala = 1,
                VestuarioGala = 1,
                EquTaser = 1,
                EquAntimotin = 1,
                EquTactico = 1
            });
            factorPorInstalacion[0].EstadosFuerza = estadoFuerza;
            factorPorInstalacion[0].Suministros = suministros;
            solicitud.FactoresPorInstalacion = factorPorInstalacion;

            var doctos = new List<DocumentoServicio>();
            doctos.Add(new DocumentoServicio()
            {
                IdDocumentoServicio = 1,
                TipoDocumento = "Tipo documento",
                Version = "1",
                FechaEnvioValidacion = DateTime.Now,
                Observaciones = "jkdj skljd kslaj dlkasj ldkjsal",
                FechaObservaciones = "jkljdlk jsalkj aldskdjlasj dlkjas",
                EsActivo = true

            });
            solicitud.DocumentosServicio = doctos;

            var conceptos = new List<Concepto>();
            conceptos.Add(new Concepto() { Key = "-1", Descripcion = "[Seleccione uno]" });
            conceptos.Add(new Concepto() { Key = "1", Descripcion = "Concepto 1" });
            conceptos.Add(new Concepto() { Key = "2", Descripcion = "Concepto 2" });
            conceptos.Add(new Concepto() { Key = "3", Descripcion = "Concepto 3" });
            solicitud.Conceptos = conceptos;

            var gastos = new List<Gasto>();
            gastos.Add(new Gasto() { Key = "1", Descripcion = "Gasto 1" });
            gastos.Add(new Gasto() { Key = "2", Descripcion = "Gasto 2" });
            gastos.Add(new Gasto() { Key = "3", Descripcion = "Gasto 3" });
            solicitud.Gastos = gastos;

            var IdGasto = new List<int>();
            IdGasto.Add(2);
            solicitud.IdGasto = IdGasto;


            var instalaciones = new List<SolicitudInstalacion>();
            instalaciones.Add(new SolicitudInstalacion()
            {
                IdSolicitudInstalacion = 1,
                IdInstalacion = 1,
                Nombre = "Instalación 1",
                Direccion = "Dirección 1",
                Seleccionado = true,
                IdConcepto = 1,
                NumCelulas = 12,
                DescripcionConcepto = "Concepto 1"
            });
            instalaciones.Add(new SolicitudInstalacion()
            {
                IdSolicitudInstalacion = 2,
                IdInstalacion = 2,
                Nombre = "Instalación 2",
                Direccion = "Dirección 2",
                Seleccionado = true,
                IdConcepto = 2,
                NumCelulas = 13,
                DescripcionConcepto = "Concepto 2"
            });
            instalaciones.Add(new SolicitudInstalacion()
            {
                IdSolicitudInstalacion = 3,
                IdInstalacion = 3,
                Nombre = "Instalación 3",
                Direccion = "Dirección 3",
                Seleccionado = true,
                IdConcepto = 3,
                NumCelulas = 13,
                DescripcionConcepto = "Concepto 3"
            });
            solicitud.Instalaciones = instalaciones;


            //if (id != null)
            //{
            //    var srvSolicitudes = new ServicesSolicitud();
            //    solicitud = srvSolicitudes.ObtenerSolicitudAcuerdosPorId(id.Value);
            //}
            return PartialView(solicitud);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ComplementoSolicitud(Models.Solicitud.UiSolicitud model)
        {
            var _ServicesSolicitud = new Servicios.ServicesSolicitud();
            var _ServicesCatalog = new Servicios.ServicesCatalog();
            var _UiSolicitud = _ServicesSolicitud.ServicioSolicitudObtenerPorId(1);
            ViewBag.Conceptos = _ServicesCatalog.CuotasObtenerPorTipoServicio(_UiSolicitud.Servicio.TipoServicio.Identificador);
            ViewBag.GastosInhe = _ServicesCatalog.ObtenerGastosInherentes();

            #region [Comment]
            //var solicitud = new GOB.SPF.ConecII.Web.Models.UiSolicitudComplemento()
            //{
            //    IdSolicitud = 1,
            //    RazonSocial = "RRRRRRR",
            //    RFC = "RFC0000",
            //    TipoServicio = "TSTSTST",
            //    NumInstalaciones = 3,
            //    TipoRolUsuario = 5,
            //    FechaInicio = DateTime.Now,
            //    FechaFin = DateTime.Now,
            //    TipoInstalaciones = "kjdlajdkl jasl",
            //    HorasDuracion = 4
            //};
            //var solicitudInstalacion = new List<SolicitudInstalacion>();
            //solicitudInstalacion.Add(new SolicitudInstalacion()
            //{
            //    IdSolicitudInstalacion = -1,
            //    IdInstalacion = 1,
            //    Nombre = "Install 1",
            //    Direccion = "Dir 1",
            //    Seleccionado = false
            //});
            //solicitudInstalacion.Add(new SolicitudInstalacion()
            //{
            //    IdSolicitudInstalacion = -1,
            //    IdInstalacion = 2,
            //    Nombre = "Install 2",
            //    Direccion = "Dir 2",
            //    Seleccionado = true
            //});
            //solicitudInstalacion.Add(new SolicitudInstalacion()
            //{
            //    IdSolicitudInstalacion = -1,
            //    IdInstalacion = 3,
            //    Nombre = "Install 3",
            //    Direccion = "Dir 3",
            //    Seleccionado = false
            //});
            //solicitud.Instalaciones = solicitudInstalacion;
            //solicitud.NumInstalaciones = solicitudInstalacion.Count();

            //var signatarios = new List<Signatario>();

            //signatarios.Add(new Signatario()
            //{
            //    IdSignatario = 1,
            //    NombreCompleto = "Signatario 1",
            //    AreaFuncional = "Área 1",
            //    Cargo = "Cargo 1",
            //    EsActivo = true
            //});
            //signatarios.Add(new Signatario()
            //{
            //    IdSignatario = 2,
            //    NombreCompleto = "Signatario 2",
            //    AreaFuncional = "Área 2",
            //    Cargo = "Cargo 2",
            //    EsActivo = true
            //});
            //signatarios.Add(new Signatario()
            //{
            //    IdSignatario = 3,
            //    NombreCompleto = "Signatario 3",
            //    AreaFuncional = "Área 3",
            //    Cargo = "Cargo 3",
            //    EsActivo = true
            //});

            //solicitud.Signatarios = signatarios;

            //var factorPorInstalacion = new List<FactorPorInstalacion>();

            //factorPorInstalacion.Add(new FactorPorInstalacion()
            //{
            //    IdInstalaccion = 1,
            //    Nombre = "Instalación 1",
            //    Division = "División 1",
            //    Grupo = "Grupo 1",
            //    Fraccion = "Fracción 1",
            //    Actividad = "Actividad 1",
            //    Distancia = "Distancia 1",
            //    Criminalidad = "Criminalidad 1"
            //});

            //var estadoFuerza = new List<EstadoFuerza>();
            //estadoFuerza.Add(new EstadoFuerza()
            //{
            //    Concepto = "Concepto 1",
            //    Descripcion = "Descripción 1",
            //    Turnos = "24x24",
            //    Cantidad = 23,
            //    Lunes = 23,
            //    Martes = 23,
            //    Miercoles = 23,
            //    Jueves = 23,
            //    Viernes = 23,
            //    Sabado = 23,
            //    Domingo = 23
            //});

            //var suministros = new List<Suministro>();
            //suministros.Add(new Suministro()
            //{
            //    Concepto = "Concepto 1",
            //    Turnos = "24x24",
            //    Cantidad = 1,
            //    Masculinos = 1,
            //    Femeninos = 1,
            //    Indistintos = 1,
            //    ArmaLarga = 1,
            //    ArmaCorta = 1,
            //    Municiones = 1,
            //    VestuarioUniforme = 1,
            //    VestuarioMediaGala = 1,
            //    VestuarioGala = 1,
            //    EquTaser = 1,
            //    EquAntimotin = 1,
            //    EquTactico = 1
            //});
            //factorPorInstalacion[0].EstadosFuerza = estadoFuerza;
            //factorPorInstalacion[0].Suministros = suministros;
            //solicitud.FactoresPorInstalacion = factorPorInstalacion;

            //var doctos = new List<DocumentoServicio>();
            //doctos.Add(new DocumentoServicio()
            //{
            //    IdDocumentoServicio = 1,
            //    TipoDocumento = "Tipo documento",
            //    Version = "1",
            //    FechaEnvioValidacion = DateTime.Now,
            //    Observaciones = "jkdj skljd kslaj dlkasj ldkjsal",
            //    FechaObservaciones = "jkljdlk jsalkj aldskdjlasj dlkjas",
            //    EsActivo = true

            //});
            //solicitud.DocumentosServicio = doctos;

            //var conceptos = new List<Concepto>();
            //conceptos.Add(new Concepto() { Key = "-1", Descripcion = "[Seleccione uno]" });
            //conceptos.Add(new Concepto() { Key = "1", Descripcion = "Concepto 1" });
            //conceptos.Add(new Concepto() { Key = "2", Descripcion = "Concepto 2" });
            //conceptos.Add(new Concepto() { Key = "3", Descripcion = "Concepto 3" });
            //solicitud.Conceptos = conceptos;

            //var gastos = new List<Gasto>();
            //gastos.Add(new Gasto() { Key = "1", Descripcion = "Gasto 1" });
            //gastos.Add(new Gasto() { Key = "2", Descripcion = "Gasto 2" });
            //gastos.Add(new Gasto() { Key = "3", Descripcion = "Gasto 3" });
            //solicitud.Gastos = gastos;

            //var IdGasto = new List<int>();
            //IdGasto.Add(2);
            //solicitud.IdGasto = IdGasto;


            //var instalaciones = new List<SolicitudInstalacion>();
            //instalaciones.Add(new SolicitudInstalacion()
            //{
            //    IdSolicitudInstalacion = 1,
            //    IdInstalacion = 1,
            //    Nombre = "Instalación 1",
            //    Direccion = "Dirección 1",
            //    Seleccionado = true,
            //    IdConcepto = 1,
            //    NumCelulas = 12,
            //    DescripcionConcepto = "Concepto 1"
            //});
            //instalaciones.Add(new SolicitudInstalacion()
            //{
            //    IdSolicitudInstalacion = 2,
            //    IdInstalacion = 2,
            //    Nombre = "Instalación 2",
            //    Direccion = "Dirección 2",
            //    Seleccionado = true,
            //    IdConcepto = 2,
            //    NumCelulas = 13,
            //    DescripcionConcepto = "Concepto 2"
            //});
            //instalaciones.Add(new SolicitudInstalacion()
            //{
            //    IdSolicitudInstalacion = 3,
            //    IdInstalacion = 3,
            //    Nombre = "Instalación 3",
            //    Direccion = "Dirección 3",
            //    Seleccionado = true,
            //    IdConcepto = 3,
            //    NumCelulas = 13,
            //    DescripcionConcepto = "Concepto 3"
            //});
            //solicitud.Instalaciones = instalaciones;


            //if (id != null)
            //{
            //    var srvSolicitudes = new ServicesSolicitud();
            //    solicitud = srvSolicitudes.ObtenerSolicitudAcuerdosPorId(id.Value);
            //}
            #endregion
            return PartialView(_UiSolicitud);
        }

        public JsonResult GetSolicitud(int id)
        {
            var solicitud = new Models.Solicitud.UiSolicitud();

            try
            {
                var _ServicesSolicitud = new Servicios.ServicesSolicitud();
                var _ServicesCatalog = new Servicios.ServicesCatalog();
                solicitud = _ServicesSolicitud.ServicioSolicitudObtenerPorId(1);
                /*
                var Servicio = new UiServicio();
                var uiTiposDocumento = new UiTiposDocumento()
                {
                    Identificador = 1,
                    Name = "Hey hey hey",
                    Nombre = "xXx",
                    Descripcion = "YyY",
                    IsActive = true,
                    IdActividad = 1,
                    Actividad = "JoKo",
                    Confidencial = true
                };

                var uiServicioDocumentoList = new List<UiServicioDocumento>();
                uiServicioDocumentoList.Add(new UiServicioDocumento()
                {
                    Numero = 1,
                    Identificador = 1,
                    TipoDocumento = uiTiposDocumento,
                    IdTipoDocumento = 1,
                    DocumentoSoporte = 1,
                    Base64 = "jkljlkjlkjlkjlkjs dfjsdlk fjlksdjf lksdjlfkjsdl",
                    Nombre = "Nombre",
                    Extension = ".doc",
                    FechaRegistro = DateTime.Now,
                    IsActive = true,
                    DocumentId = Guid.NewGuid()
                });

                var uiTiposServicio = new UiTiposServicio()
                {
                    Identificador = 1,
                    Name = "Tipo servicio ajkdajklsjda",
                    Descripcion = "kldakldlkask ldkjaslkd jaslkjd las",
                    Clave = "KJL",
                    IsActive = true
                };
                var ListUiTiposServicio = new List<UiTiposServicio>();
                ListUiTiposServicio.Add(uiTiposServicio);

                var uiReferencia = new UiReferencia()
                {
                    Identificador = 1,
                    ClaveReferencia = 1,
                    Descripcion = "jdklasjldkjsa",
                    EsProducto = false,
                    IsActive = true
                };
                var ListUiReferencia = new List<UiReferencia>();
                ListUiReferencia.Add(uiReferencia);

                var uiDependencias = new UiDependencias()
                {
                    Identificador = 1,
                    Name = "Dependecia nkladjalks",
                    Descripcion = "kajskjasl jdlaskdjlas",
                    IsActive = true
                };
                var ListUiDependencias = new List<UiDependencias>();
                ListUiDependencias.Add(uiDependencias);

                var uiJerarquia = new UiJerarquia()
                {
                    Identificador = 1,
                    Name = "Jerarquia ansdkljsa",
                    Nivel = 1,
                    IsActive = true
                };
                var ListUiJerarquia = new List<UiJerarquia>();
                ListUiJerarquia.Add(uiJerarquia);

                var uiGrupoTarifario = new UiGrupoTarifario() {
                    Identificador = 1,
                    Name = "Jerarquia ansdkljsa",
                    Descripcion= "jdklajdlkas jldkas",
                    Nivel = 1,
                    IsActive = true

                };
                var ListUiGrupoTarifario = new List<UiGrupoTarifario>();
                ListUiGrupoTarifario.Add(uiGrupoTarifario);

                var uiMedidaCobro = new UiMedidaCobro();
                var ListUiMedidaCobro = new List<UiMedidaCobro>();
                ListUiMedidaCobro.Add(uiMedidaCobro);

                var uiCuota = new UiCuota() {
                    Identificador = 1,
                    IdTipoServicio = 1,
                    TipoServicio = "mdlkakmslmlkl a akjsld",
                    TipoServicios = ListUiTiposServicio,
                    IdReferencia = 13,
                    Referencia = "REF09984",
                    Referencias = ListUiReferencia,
                    IdDependencia = 1,
                    Dependencia = "jdkoajdlks",
                    DescripcionDependencia = "jsklajlkdjalkdjla",
                    Dependencias = ListUiDependencias,
                    Concepto = "dkasjdjlkas lkjd",
                    IdJerarquia = 34,
                    Jerarquia = "djklasjdlaskjd l",
                    Jerarquias = ListUiJerarquia,
                    IdGrupoTarifario = 1,
                    GrupoTarifario = "kdaskjd lksa",
                    GrupoTarifarios = ListUiGrupoTarifario,
                    CuotaBase = 8490,
                    IdMedidaCobro = 1,
                    MedidaCobro = "hjdkajdlksajl",
                    MedidaCobros = ListUiMedidaCobro,
                    Iva = 0.16m,
                    FechaAutorizacion = DateTime.Now,
                    FechaEntradaVigor = DateTime.Now,
                    FechaTermino = DateTime.Now,
                    FechaPublicaDof = DateTime.Now,
                    IsActive = true,
                    EsProducto = true,
                    Ano = 2017

                };

                var _UiTipoServicio = new GOB.SPF.ConecII.Web.Models.UiTipoServicio() {
                    Identificador = 1,
                    Nombre = "jdklajdlaskdlñ asñlkd ñlask dñasl",
                    Descripcion = "jkdlajdlkjsalkd jaslkj dla",
                    Clave = "009984",
                    FechaInicial = DateTime.Now,
                    FechaFinal = DateTime.Now,
                    Activo = true
                };

                var _UiTipoInstalacionesCapacitacion = new UiTipoInstalacionesCapacitacion() {
                    Identificador=3,
                    Nombre="kladlalkjsd klasjd la"
                };

                var _UiAsistente = new UiAsistente() {
                    Identificador = 34,
                    idPersona = Guid.NewGuid(),
                    Activo = true
                };

                var ListUiAsistente = new List<UiAsistente>();
                ListUiAsistente.Add(_UiAsistente);

                var _UiAcuerdo = new UiAcuerdo() {
                };
                var ListUiAcuerdo = new List<UiAcuerdo>();
                ListUiAcuerdo.Add(_UiAcuerdo);

                var _UiInstalacion = new UiInstalacion() { };
                var ListUiInstalacion = new List<UiInstalacion>();
                ListUiInstalacion.Add(_UiInstalacion);

                var _UiEstatus = new UiEstatus() {
                    Identificador=78,
                    Nombre = "jdklajsk",
                    EntidadNegocio = new UiEntidadNegocio() {
                        Identificador=46,
                        Descripcion="jdkokasjld asl"
                    }
                };

                var uiServicio = new UiServicio() {
                    Identificador = 1,
                    FechaInicio = DateTime.Now,
                    FechaFin = DateTime.Now,
                    Integrantes = 50,
                    Documentos = uiServicioDocumentoList,
                    NumeroPersonas = 12,
                    FechaInicial = DateTime.Now,
                    FechaFinal = DateTime.Now,
                    Observaciones = "",
                    TieneComite = true,
                    ObservacionesComite = "",
                    BienCustodia = "",
                    Viable = true,
                    FechaComite = DateTime.Now,
                    Cuota = uiCuota,
                    TipoServicio = _UiTipoServicio,
                    HorasCurso = 1,
                    TipoInstalacionesCapacitacion = _UiTipoInstalacionesCapacitacion,
                    Asistentes = ListUiAsistente,
                    Acuerdos = ListUiAcuerdo,
                    Instalaciones = ListUiInstalacion,
                    Estatus = _UiEstatus

                };

                var _UiDocumento = new UiDocumento() {
                    Numero = 1,
                    Identificador = 1,
                    TipoDocumento = uiTiposDocumento,
                    IdTipoDocumento = 1,
                    DocumentoSoporte = 1,
                    Base64 = "jkljlkjlkjlkjlkjs dfjsdlk fjlksdjf lksdjlfkjsdl",
                    Nombre = "Nombre",
                    Extension = ".doc",
                    FechaRegistro = DateTime.Now,
                    IsActive = true,
                    DocumentId = Guid.NewGuid()
                };

                var ListUiDocumento = new List<UiDocumento>();
                ListUiDocumento.Add(_UiDocumento);


                var _UiDomicilioFiscal = new UiDomicilioFiscal() {
                    Identificador = 64,
                    IdPais = 52,
                    IdEstado = 8,
                    Estado = "jdklasjdlas",
                    IdMunicipio = 23,
                    Municipio = "jdklasjdkj kajs",
                    IdAsentamiento = 32,
                    Asentammiento = "djkklasjdkl jsalk",
                    CodigoPostal = "78896",
                    Calle = "kjfkl jsalkf jalf",
                    NoInterior = "335",
                    NoExterior = "300",
                    IdCliente = 1
                };


                var _UiTelefonoContacto = new UiTelefonoContacto() {
                    Indice = 1,

                    IdTipoTelefono = 66,
                    TipoTelefono = "jdklasjdkl",
                    Numero = "7897898098",
                    Extension = "789",
                    IsActive = true
                };
                var ListUiTelefonoContacto = new List<UiTelefonoContacto>();
                ListUiTelefonoContacto.Add(_UiTelefonoContacto);

                var _UiCorreoContacto = new UiCorreoContacto() {
                    Indice = 1,
                    Identificador = 23,
                    CorreoElectronico = "jjdkla@hdkals.net",
                    IsActive = true,
                    IdExterno = 1
                };
                var ListUiCorreoContacto = new List<UiCorreoContacto>();
                ListUiCorreoContacto.Add(_UiCorreoContacto);

                var _UiSolicitante = new UiSolicitante() {
                    Identificador = 33,
                    IdCliente = 1,
                    IdTipoPersona = 44,
                    Nombre = "kjsljkfljdslk fjdslkj flkdsj fl",
                    ApellidoPaterno = "djkalkjdlas kjlda",
                    ApellidoMaterno = "jfajsl ajdsl kajdl ka",
                    Cargo = "djklasjd kaljsldaslkjlk als",
                    IsActive = true,
                    Telefonos = ListUiTelefonoContacto,
                    Correos = ListUiCorreoContacto
                };
                var ListUiSolicitante = new List<UiSolicitante>();
                ListUiSolicitante.Add(_UiSolicitante);

                var _UiClienteContacto = new UiClienteContacto() {
                    Identificador = 33,
                    IdCliente = 1,
                    IdTipoPersona = 44,
                    Nombre = "kjsljkfljdslk fjdslkj flkdsj fl",
                    ApellidoPaterno = "djkalkjdlas kjlda",
                    ApellidoMaterno = "jfajsl ajdsl kajdl ka",
                    Cargo = "djklasjd kaljsldaslkjlk als",
                    IsActive = true,
                    Telefonos = ListUiTelefonoContacto,
                    Correos = ListUiCorreoContacto,
                    Numero = 321,
                    IdTipoContacto = 54,
                    TipoContacto = "jdklasjdljdlkasjl djsalk das"
                };
                var ListUiClienteContacto = new List<UiClienteContacto>();
                ListUiClienteContacto.Add(_UiClienteContacto);

                var _UiSolicitud1 = new Models.Solicitud.UiSolicitud()
                {
                    Identificador=1
                };
                var _UiSolicitud2 = new Models.Solicitud.UiSolicitud()
                {
                    Identificador = 2
                };
                var _UiSolicitud3 = new Models.Solicitud.UiSolicitud()
                {
                    Identificador = 3
                };
                var ListUiSolicitud = new List<Models.Solicitud.UiSolicitud>();
                ListUiSolicitud.Add(_UiSolicitud1);
                ListUiSolicitud.Add(_UiSolicitud2);
                ListUiSolicitud.Add(_UiSolicitud3);

                var Cliente = new UiCliente() {
                    Identificador = 1,
                    RazonSocial = "jdlkajdl aksldñk asñlkd ñaslk",
                    IdRazonSocial = 34,
                    NombreCorto = "dklakdlñk sañld kkl",
                    IdNombreCorto = 1,
                    IdRegimenFiscal = 23,
                    RegimenFiscal = "kldaklñd asñ",
                    IdSector = 23,
                    Sector = "dklñaskñ",
                    RFC = "JKLK123456I89",
                    IdRFC = 54,
                    IsActive = true,
                    DomicilioFiscal = _UiDomicilioFiscal,
                    Solicitantes = ListUiSolicitante,
                    Contactos = ListUiClienteContacto,
                    Instalaciones = ListUiInstalacion,
                    Documentos = ListUiDocumento,
                    Solicitudes = ListUiSolicitud,
                    UniqueId = Guid.NewGuid()
                   };
                var TipoSolicitud = new UiTipoSolicitud() {
                    Identificador = 77,
                    Nombre = "jdk alsj lda",
                    Descripcion = "jdklasjldk jaslkdj alskjd lkasjld a",
                    IsActive = true
                };
                solicitud.Identificador = 1;
                solicitud.Folio = 789;
                solicitud.FechaRegistro = DateTime.Now;
                solicitud.DocumentoSoporte = 23;
                solicitud.Minuta = 0;
                solicitud.Documento = _UiDocumento;
                solicitud.Cliente = Cliente;
                solicitud.Cancelado = false;
                solicitud.TipoSolicitud=TipoSolicitud;
                */
            }
            catch (UiException e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }
            catch (ConecWebException ex)
            {
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }

            return Json(solicitud, JsonRequestBehavior.AllowGet);
        }



        #endregion


    }
}