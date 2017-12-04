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

            if (cliente == null)
                return PartialView(model);
            else
                return PartialView(cliente);
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
                    uiResult.List = clientService.ObtenerClientes(model.Query);
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
                uiResult.List = clientService.ObtenerClientes(model.ObjectResult);
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
        public async Task<PartialViewResult> Registro(UiCliente model)
        {

            ServicesSolicitud clientServiceSolicitud = new ServicesSolicitud();
            ServicesCatalog catalogService = new ServicesCatalog();
            List<UiSector> sector = new List<UiSector>();
            List<UiRegimenFiscal> regimenfiscal = new List<UiRegimenFiscal>();
            UiCliente cliente = null;

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Nuevo Registro";
                    break;
                case UiEnumEntity.Add:
                case UiEnumEntity.Edit:
                case UiEnumEntity.View:
                    /* Buscamos al cliente completo desde Cliente hasta Teléfonos y Correos */
                    cliente = clientServiceSolicitud.ClienteObtenerPorId(model.Identificador);
                    cliente.Action = model.Action;
                    ViewBag.Title = model.Action == UiEnumEntity.Add ? "Agregar Solicitud del Cliente" : model.Action == UiEnumEntity.Edit ? "Modificar Cliente" : "Detalle del Cliente";
                    break;
            }

            sector = catalogService.ObtenerSector();
            regimenfiscal = catalogService.ObtenerRegimenFiscal(1, 20);
            ViewBag.RegimenFiscal = new SelectList(regimenfiscal.OrderBy(x => x.Name), "Identificador", "Name", model.IdRegimenFiscal);
            ViewBag.Sector = new SelectList(sector.OrderBy(x => x.Descripcion), "Identificador", "Descripcion", model.IdSector);

            if (cliente == null)
                return PartialView(model);
            else
                return PartialView(cliente);
        }

        public JsonResult RegistrosConsulta(int page, int rows, UiResultPage<UiSolicitudes> model)
        {
            UiResultPage<UiSolicitudes> uiResult = new UiResultPage<UiSolicitudes>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesSolicitud clientService = new ServicesSolicitud();

                uiResult.List = clientService.ObtenerSolicitudes(page, rows);

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

        public JsonResult RegistroConsultaCriterio(UiResultPage<UiSolicitudes> model)
        {
            UiResultPage<UiSolicitudes> uiResult = new UiResultPage<UiSolicitudes>();
            uiResult.Result = UiEnum.TransactionResult.Failed;
            try
            {
                ServicesSolicitud clientService = new ServicesSolicitud();
                /*****************/
                uiResult.List = clientService.SolicitudesObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
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

        public JsonResult TraerClienteDatos(int Identificador)
        {
            try
            {
                ServicesSolicitud clientService = new ServicesSolicitud();
                UiCliente cliente = null;
                cliente = clientService.ClienteObtenerPorId(Identificador);
                var query = cliente;
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("No existe el cliente" + ex.Message + "", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult RegistroGuardar(UiResultPage<UiCliente> model)
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
            ViewBag.Editable = model.Action.Equals(UiEnumEntity.View) ? " disabled=disabled" : "";
            ViewBag.Visible = model.Action.Equals(UiEnumEntity.View) ? "hidden=hidden" : "";

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

            var resultados = new[] { taskZonas, taskEstacion, taskInstalacion, taskFracciones, taskGrupos, taskDiviciones };
            
            var instalacion = new UiInstalacion();

            if (model.Instalaciones.FirstOrDefault() != null)
            {
                model.Instalaciones.FirstOrDefault();
            }

            instalacion.IdCliente = model.Identificador;
            instalacion.NombreCorto = model.NombreCorto;
            instalacion.RazonSocial = model.RazonSocial;
            

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
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ViewBag.Cliente = client;

            return PartialView(instalacion);
        }

        public JsonResult InstalacionConsulta(UiResultPage<UiCliente> cliente)
        {

            var uiResult = new UiResultPage<UiCliente> { Result = UiEnum.TransactionResult.Failed };
            List<UiCliente> clientes;

            if (!cliente.Query.Instalaciones.Any())
            {
                clientes = new ServicesSolicitud().ObtenerClientes(new UiCliente { IsActive = true});
                clientes.Add(new UiCliente {Identificador = 10, NombreCorto = "Prueba", RazonSocial = "PruebaRazonSocial"});
                clientes.ForEach(c =>
                {
                    c.Instalaciones = new List<UiInstalacion>
                    {
                        new UiInstalacion { Identificador = 1, NombreCorto = "AAAAAA", Activo = true },
                        new UiInstalacion { Identificador = 2, NombreCorto = "BBBBBB", Activo = true },
                        new UiInstalacion { Identificador = 3, NombreCorto = "CCCCCC", Activo = true },
                        new UiInstalacion { Identificador = 4, NombreCorto = "DDDDDD", Activo = true }
                    };
                });

            }
            else
            {
                var filtro = new UiCliente
                {
                    RFC = cliente.Query.RazonSocial,
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

                clientes = new ServicesSolicitud().ObtenerClientes(filtro);
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

        //public JsonResult InstalacionConsultaCriterio(UiResultPage<UiCliente> cliente)
        //{
        //    var uiResult = new UiResultPage<UiCliente> { Result = UiEnum.TransactionResult.Failed };

        //    List<UiCliente> instalaciones;

        //    if (cliente.Query == null)
        //    {
        //        instalaciones = new ServicesSolicitud().ObtenerClientes(new UiCliente { IsActive = cliente.Query.IsActive });
        //    }
        //    else
        //    {
        //        var filtro = new UiCliente
        //        {
        //            RFC = cliente.Query.RazonSocial,
        //            NombreCorto = cliente.Query.NombreCorto,
        //            RazonSocial = cliente.Query.RazonSocial,
        //            IsActive = cliente.Query.IsActive
        //        };

        //        instalaciones = new ServicesSolicitud().ObtenerClientes(filtro);
        //    }
        //    try
        //    {
        //        uiResult.List = instalaciones;
        //        uiResult.Paging = uiResult.Paging;

        //        uiResult.Result = UiEnum.TransactionResult.Success;
        //    }
        //    catch (UiException e)
        //    {
        //        EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
        //        uiResult.Message = ErrorMessage.GenericMessage;
        //    }
        //    catch (Exception e)
        //    {
        //        EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
        //        uiResult.Message = ErrorMessage.GenericMessage;
        //    }

        //    return Json(uiResult, JsonRequestBehavior.AllowGet);
        //}

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