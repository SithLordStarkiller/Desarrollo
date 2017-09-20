using GOB.SPF.ConecII.Web.Models;
using GOB.SPF.ConecII.Web.Resources;
using GOB.SPF.ConecII.Web.Servicios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GOB.SPF.ConecII.Web.Controllers
{
    public class SolicitudController : Controller
    {
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

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ClienteGuardar(UiResultPage<UiCliente> model)
        {
            var cliente = model.ObjectResult;

            cliente.Solicitantes = (Session["listadoDeSolicitantes"] != null ? (List<UiSolicitante>)Session["listadoDeSolicitantes"] : new List<UiSolicitante>());
            cliente.Contactos = (Session["listadoDeContactos"] != null ? (List<UiClienteContacto>)Session["listadoDeContactos"] : new List<UiClienteContacto>());

            // Envia a guardar el objeto del cliente
            return Json(model);
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
        #endregion

        #region Domicilio Fiscal
        [HttpPost]
        [AllowAnonymous]
        public ActionResult DomicilioFiscal(UiCliente model)
        {
            ServicesSolicitud clientServiceSolicitud = new ServicesSolicitud();
            var domicilioFiscal = clientServiceSolicitud.ObtenerDomicilioFiscal(model.Identificador);

            return PartialView(domicilioFiscal);
        }
        #endregion

        #region Solicitantes
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Solicitantes(UiCliente model)
        {
            // Extraemos los solicitantes del cliente con model.Identificador

            //ServicesSolicitud clientServiceSolicitud = new ServicesSolicitud();
            //var solicitantes = clientServiceSolicitud.ObtenerSolicitantes(model.Identificador);

            List<UiSolicitante> solicitanteList = new List<UiSolicitante>();

            if (Session["listadoDeSolicitantes"] == null)
            {
                solicitanteList.Add(new UiSolicitante { Numero = 1, Identificador = 1, IdCliente = 1, Nombre = "Gerardo", ApellidoPaterno = "Ortiz", ApellidoMaterno = "Chavez", DbSaved = true });
                Session["listadoDeSolicitantes"] = solicitanteList;
            }
            else
            {
                solicitanteList = (List<UiSolicitante>)Session["listadoDeSolicitantes"];
            }

            return PartialView(solicitanteList);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Solicitante(UiSolicitante solicitante)
        {
            var model = new UiSolicitante();
            
            switch (solicitante.Action)
            {
                case UiEnumEntity.New:
                    break;
                case UiEnumEntity.Edit:
                case UiEnumEntity.View:
                    model = ((List<UiSolicitante>)Session["listadoDeSolicitantes"]).FirstOrDefault(p => p.Numero == solicitante.Numero);
                    break;
            }

            model.IdCliente = solicitante.IdCliente;
            model.Action = solicitante.Action;

            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SolicitanteGuardar(UiSolicitante solicitante)
        {
            if (Session["listadoDeSolicitantes"] == null)
                Session["listadoDeSolicitantes"] = new List<UiSolicitante>();

            var model = ((List<UiSolicitante>)Session["listadoDeSolicitantes"]).ToList();

            if(solicitante.Action == UiEnumEntity.New)
            {
                solicitante.Numero = model.Count() > 0 ? model.Max(p => p.Numero) + 1 : 1;
                model.Add(solicitante);
            }
            else
            {
                var tmp = model.FirstOrDefault(p => p.Numero == solicitante.Numero);
                tmp.Nombre = solicitante.Nombre;
                tmp.ApellidoPaterno = solicitante.ApellidoPaterno;
                tmp.ApellidoMaterno = solicitante.ApellidoMaterno;
                tmp.Cargo = solicitante.Cargo;
                tmp.Activo = solicitante.Activo;
            }

            Session["listadoDeSolicitantes"] = model;
            return PartialView("Solicitantes", model);
        }
        #endregion

        #region Contactos
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Contactos(UiCliente model)
        {
            //ServicesSolicitud clientServiceSolicitud = new ServicesSolicitud();
            //var solicitante = clientServiceSolicitud.ObtenerSolicitantes(model.Identificador);

            List<UiClienteContacto> contactosList = new List<UiClienteContacto>();

            if (Session["listadoDeContactos"] == null)
            {
                contactosList.Add(new UiClienteContacto { Numero = 1, Identificador = 1, IdCliente = 1, Nombre = "Miguel", ApellidoPaterno = "Alcantara", ApellidoMaterno = "Gutierrez", DbSaved = true });
                Session["listadoDeContactos"] = contactosList;
            }
            else
            {
                contactosList = (List<UiClienteContacto>)Session["listadoDeContactos"];
            }

            return PartialView(contactosList);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Contacto(UiClienteContacto contacto)
        {
            var model = new UiClienteContacto();
            switch (contacto.Action)
            {
                case UiEnumEntity.New:
                    break;
                case UiEnumEntity.Edit:
                case UiEnumEntity.View:
                    model = ((List<UiClienteContacto>)Session["listadoDeContactos"]).FirstOrDefault(p => p.Numero == contacto.Numero);
                    break;
            }

            model.IdCliente = contacto.IdCliente;
            model.Action = contacto.Action;

            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ContactoGuardar(UiClienteContacto contacto)
        {
            if (Session["listadoDeContactos"] == null)
                Session["listadoDeContactos"] = new List<UiClienteContacto>();

            var model = ((List<UiClienteContacto>)Session["listadoDeContactos"]).ToList();

            if (contacto.Action == UiEnumEntity.New)
            {
                contacto.Numero = model.Max(p => p.Numero) + 1;
                model.Add(contacto);
            }
            else
            {
                var tmp = model.FirstOrDefault(p => p.Numero == contacto.Numero);
                tmp.Nombre = contacto.Nombre;
                tmp.ApellidoPaterno = contacto.ApellidoPaterno;
                tmp.ApellidoMaterno = contacto.ApellidoMaterno;
                tmp.Cargo = contacto.Cargo;
                tmp.Activo = contacto.Activo;
            }

            Session["listadoDeContactos"] = model;
            return PartialView("Contactos", model);
        }
        #endregion

        #region Telefonos
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Telefonos(UiExterno model)
        {
            ServicesSolicitud serviceClient = new ServicesSolicitud();



            return PartialView();
        }


        #endregion

        #region Instalaciones


        public ActionResult Instalaciones()
        {
            ViewBag.Tittle = "Instalacion";
            return View();
        }
        [HttpPost]
        public PartialViewResult Instalacion(UiInstalacion model)
        {
            ViewBag.Editable = model.Action.Equals(UiEnumEntity.View) ? " disabled=disabled" : "";
            ViewBag.Visible = model.Action.Equals(UiEnumEntity.View) ? "hidden=hidden" : "";

            var client = new ServicesCatalog();

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
                var zona = new List<UiFases>
                {
                    new UiFases {Identificador = 0, Name = "Selecciona una estacion"}
                };

                var lista = client.ObtenerFases(1, 100000);

                zona.AddRange(lista);

                var listaEstacion = zona.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name,
                    Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.ListaEstacion = listaEstacion;
            });

            var taskInstalacion = Task.Factory.StartNew(() =>
            {
                var zona = new List<UiFases>
                {
                    new UiFases {Identificador = 0, Name = "Selecciona un tipo de instalacion"}
                };

                var lista = client.ObtenerFases(1, 100000);

                zona.AddRange(lista);

                var listaInstalacion = zona.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name,
                    Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.ListaInstalacion = listaInstalacion;
            });

            var resultados = new[] { taskZonas,taskEstacion, taskInstalacion };

            Task.WaitAll(resultados);

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Tittle = "Crear nueva instalacion";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Tittle = "Modificar la instalacion";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Tittle = "Detalle de la instalacion";
                    break;
            }

            return PartialView(model);
        }

        public JsonResult InstalacionConsulta(int page, int rows)
        {
            var uiResult = new UiResultPage<UiInstalacion> { Result = UiEnum.TransactionResult.Failed };

            try
            {
                uiResult.List = new List<UiInstalacion>
                {
                    new UiInstalacion { Identificador = 1, RazonSocial = "Razon Social 1", Activo = true},
                    new UiInstalacion { Identificador = 2, RazonSocial = "Razon Social 2", Activo = true}
                };
                uiResult.Paging.Pages = 1;
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

        [HttpPost]
        public JsonResult InstalacionConsultaCriterio(UiResultPage<UiInstalacion> model)
        {
            //var filtro = model.Query; 
            //var uiResult = new UiResultPage<UiInstalacion> {Result = UiEnum.TransactionResult.Failed};

            //try
            //{
            //    var clientService = new ServicesCatalog();
            //    uiResult.List = clientService.(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
            //    uiResult.Paging.Pages = clientService.Pages;
            //    uiResult.Paging.Rows = model.Paging.Rows;
            //    uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
            //    uiResult.Result = UiEnum.TransactionResult.Success;
            //}
            //catch (UiException e)
            //{
            //    EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            //    uiResult.Message = ErrorMessage.GenericMessage;
            //}
            //catch (Exception e)
            //{
            //    EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            //    uiResult.Message = ErrorMessage.GenericMessage;
            //}

            return Json("uiResult", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SolicitudContraprestacion

        public ActionResult Contraprestaciones()
        {

            ServicesCatalog clientServiceCatalog = new ServicesCatalog();
             var cuotas=clientServiceCatalog.ObtenerCuotas(1, 1000);

            return View(cuotas);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GuardarSolicitudContraprestacion(UiResultPage<UiSolicitud> model)
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
    }
}