namespace GOB.SPF.ConecII.Web.Controllers
{
    using Models;
    using Resources;
    using Servicios;
    using Entities;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class ConfiguracionController : Controller
    {
        #region Plantillas

        public ActionResult Plantillas()
        {
            UiPlantilla model = new UiPlantilla();
            return View(model);
        }

        public JsonResult PlantillaConsulta(UiResultPage<UiPlantilla> model)
        {
            UiResultPage<UiPlantilla> uiResult = new UiResultPage<UiPlantilla>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServiceConfiguration client = new ServiceConfiguration();
                uiResult.List = client.PlantillaObtener(model.Paging.CurrentPage, model.Paging.Rows);
                uiResult.Paging.Pages = client.Pages;
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

        public JsonResult PlantillaConsultaCriterio(UiResultPage<UiPlantilla> model)
        {
            UiResultPage<UiPlantilla> uiResult = new UiResultPage<UiPlantilla>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServiceConfiguration client = new ServiceConfiguration();
                uiResult.List = client.PlantillaObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows,
                    model.ObjectResult);
                uiResult.Paging.Pages = client.Pages;
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
        public PartialViewResult Plantilla(UiDivision model)
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
        public JsonResult PlantillaGuardar(UiResultPage<UiDivision> model)
        {
            UiResultPage<UiDivision> uiResult = new UiResultPage<UiDivision>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveDivisiones(model.ObjectResult)
                    ? UiEnum.TransactionResult.Success
                    : UiEnum.TransactionResult.Failed;
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


        #endregion

        #region Notificaciones y Alertas

        public ActionResult NotifacionesAlertas()
        {
            return View();
        }

        public JsonResult ObtenerNotificaciones(UiResultPage<UiNotificacion> model)
        {
            var uiResult = new UiResultPage<UiNotificacion>
            {
                Result = UiEnum.TransactionResult.Failed
            };

            try
            {
                var clientService = new ServiceConfiguration();
                var listaNotificaciones = clientService.ObtenerNotificaciones(model.Paging.CurrentPage,
                    model.Paging.Rows);

                uiResult.List = listaNotificaciones;
                uiResult.Paging.Pages = listaNotificaciones.Count/model.Paging.Rows;
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

        public ActionResult EditarNotificacion(int idNotificacion)
        {
            var taskAreas = Task.Factory.StartNew(() =>
            {
                var areas = new List<UiArea>
                {
                    new UiArea {Identificador = 0, Name = "Selecciona un area"}
                };

                var resultadoAreas = new ServicesCatalog().ObtenerAreas(1, 100000);

                areas.AddRange(resultadoAreas);

                var listaAreas = areas.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name,
                    Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.ListaAreas = listaAreas;
            });

            var taskTipoServicio = Task.Factory.StartNew(() =>
            {
                var tiposServicios = new List<UiTiposServicio>
                {
                    new UiTiposServicio {Identificador = 0, Name = "Selecciona un tipo de sevicio"}
                };

                var resultadoTipoServicios = new ServicesCatalog().ObtenerTiposServicio(1, 100000);

                tiposServicios.AddRange(resultadoTipoServicios);

                var listaTiposServicios = tiposServicios.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name,
                    Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.ListaTiposServicios = listaTiposServicios;
            });

            var taskRoles = Task.Factory.StartNew(() =>
            {
                var roles = new List<Roles>
                {
                    new Roles {IdRol = 0, NombreRol = "Selecciona un Rol"},
                    new Roles {IdRol = 1, NombreRol = "Administrador"}
                };
                var listaRoles = roles.Select(c => new SelectListItem
                {
                    Value = c.IdRol.ToString(),
                    Text = c.NombreRol,
                    Selected = c.IdRol == 0
                }).ToArray();

                ViewBag.ListaRoles = listaRoles;
            });

            var taskFaces = Task.Factory.StartNew(() =>
            {
                var faces = new List<UiFases>
                {
                    new UiFases {Identificador = 0, Name = "Selecciona un Face"}
                };

                var resultadoFaces = new ServicesCatalog().ObtenerFases(1, 10000);

                faces.AddRange(resultadoFaces);

                var listaFaces = faces.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name,
                    Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.ListaFaces = listaFaces;
            });

            var taskActividades = Task.Factory.StartNew(() =>
            {
                var actividades = new List<UiActividad>
                {
                    new UiActividad {Identificador = 0, Name = "Selecciona una Actividad"}
                };

                var resultadoFaces = new ServicesCatalog().ObtenerActividades(1, 100000);

                actividades.AddRange(resultadoFaces);

                var listaActividades = actividades.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name,
                    Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.ListaActividades = listaActividades;
            });

            var resultados = new[] {taskActividades, taskAreas, taskFaces, taskRoles, taskTipoServicio};

            Task.WaitAll(resultados);

            ViewBag.IdNotificacion = idNotificacion;

            return View();
        }

        public ActionResult ObtenerNotificacion(UiResultPage<UiNotificacion> model)
        {
            var uiResult = new UiResultPage<UiNotificacion>
            {
                Result = UiEnum.TransactionResult.Failed
            };

            try
            {
                var clientService = new ServiceConfiguration();

                var notificacion = clientService.ObtenerNotificacionPorId(1, 20000,
                    new UiNotificacion {IdNotificacion = model.Query.IdNotificacion});

                uiResult.ObjectResult = notificacion;
                uiResult.Paging.Pages = 1;
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

        public ActionResult ObtenerReceptores(UiResultPage<UiIntegrante> model)
        {
            if (Session["ListaReceptores"] == null)
            {
                Session["ListaReceptores"] = new List<UiIntegrante>();
            }

            var listaReceptores = (List<UiIntegrante>) Session["ListaReceptores"];

            var uiResult = new UiResultPage<UiIntegrante>
            {
                Result = UiEnum.TransactionResult.Failed
            };

            try
            {
                var clientService = new ServiceConfiguration();

                var receptores = clientService.ReceptoresAlertaObtenerTodos(1, 10000,
                    new UiReceptorAlerta {IdNotificacion = Convert.ToInt32(model.Message)});

                var listaIntegrantes = receptores.Select(x => new UiIntegrante
                {
                    IdTipoReceptor = x.IdTipoReceptor,
                    Identificador = x.IdPersona,
                    IdUsuario = x.IdUsuario,
                    Correo = x.Correo,
                    IdRol = x.IdRol
                }).ToList();

                var client = new ServicesCatalog();

                Parallel.ForEach(listaIntegrantes, item =>
                {
                    if (item.Identificador == new Guid()) return;

                    var integrante =
                        client.ObtenerIntegrantes(1, 100000).FirstOrDefault(x => x.Identificador == item.Identificador);

                    if (integrante == null) return;

                    item.Correo = integrante.Correo;
                    item.Nombre = integrante.Nombre;
                    item.ApMaterno = integrante.ApMaterno;
                    item.ApPaterno = integrante.ApPaterno;
                    item.Area = integrante.Area;
                });

                listaReceptores.AddRange(listaIntegrantes);

                Session["ListaReceptores"] = listaReceptores;

                uiResult.List = listaReceptores;
                uiResult.Paging.Pages = 1;
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

        public ActionResult CreacionDeNotifacionesAlertas(UiResultPage<UiIntegrante> model)
        {
            Session.Remove("ListaReceptores");

            var taskAreas = Task.Factory.StartNew(() =>
            {
                var areas = new List<UiArea>
                {
                    new UiArea {Identificador = 0, Name = "Selecciona un area"}
                };

                var resultadoAreas = new ServicesCatalog().ObtenerAreas(1, 100000);

                areas.AddRange(resultadoAreas);

                var listaAreas = areas.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name,
                    Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.ListaAreas = listaAreas;
            });

            var taskTipoServicio = Task.Factory.StartNew(() =>
            {
                var tiposServicios = new List<UiTiposServicio>
                {
                    new UiTiposServicio {Identificador = 0, Name = "Selecciona un tipo de sevicio"}
                };

                var resultadoTipoServicios = new ServicesCatalog().ObtenerTiposServicio(1, 100000);

                tiposServicios.AddRange(resultadoTipoServicios);

                var listaTiposServicios = tiposServicios.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name,
                    Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.ListaTiposServicios = listaTiposServicios;
            });

            var taskRoles = Task.Factory.StartNew(() =>
            {
                var roles = new List<Roles>
                {
                    new Roles {IdRol = 0, NombreRol = "Selecciona un Rol"},
                    new Roles {IdRol = 1, NombreRol = "Administrador"}
                };
                var listaRoles = roles.Select(c => new SelectListItem
                {
                    Value = c.IdRol.ToString(),
                    Text = c.NombreRol,
                    Selected = c.IdRol == 0
                }).ToArray();

                ViewBag.ListaRoles = listaRoles;
            });

            var taskFaces = Task.Factory.StartNew(() =>
            {
                var faces = new List<UiFases>
                {
                    new UiFases {Identificador = 0, Name = "Selecciona un Face"}
                };

                var resultadoFaces = new ServicesCatalog().ObtenerFases(1, 10000);

                faces.AddRange(resultadoFaces);

                var listaFaces = faces.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name,
                    Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.ListaFaces = listaFaces;
            });

            var taskActividades = Task.Factory.StartNew(() =>
            {
                var actividades = new List<UiActividad>
                {
                    new UiActividad {Identificador = 0, Name = "Selecciona una Actividad"}
                };

                var resultadoFaces = new ServicesCatalog().ObtenerActividades(1, 100000);

                actividades.AddRange(resultadoFaces);

                var listaActividades = actividades.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name,
                    Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.ListaActividades = listaActividades;
            });

            var resultados = new[] {taskActividades, taskAreas, taskFaces, taskRoles, taskTipoServicio};

            Task.WaitAll(resultados);

            return View("NotifacionesAlertas");
        }

        public JsonResult ConsultaIntegrantes(UiResultPage<UiIntegrante> model)
        {
            var uiResult = new UiResultPage<UiIntegrante>
            {
                Result = UiEnum.TransactionResult.Failed
            };

            try
            {
                var clientService = new ServicesCatalog();

                var listaIntegrantes = clientService.ObtenerIntegrantes(model.Paging.CurrentPage, model.Paging.Rows);

                switch (model.Query.IdTipoReceptor)
                {
                    case 1:
                        listaIntegrantes =
                            listaIntegrantes.Where(x => x.IdJerarquia == 26 /*model.Query.IdJerarquia*/)
                                .Take(5)
                                .ToList();
                        break;

                    case 2:
                        listaIntegrantes =
                            listaIntegrantes
                                //.Where(
                                //x => x.Area.Contains(model.Query.Area) ||
                                //x.Nombre.Contains(model.Query.Nombre) ||
                                //x.ApPaterno.Contains(model.Query.ApPaterno) ||
                                //x.ApMaterno.Contains(model.Query.ApMaterno) ||
                                //x.Correo.Contains(model.Query.Correo))
                                .Take(5).ToList();
                        break;
                    case 3:
                        break;
                    case 4:

                        break;



                }

                uiResult.List = listaIntegrantes;
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

        public JsonResult AgregarReceptor(UiResultPage<UiIntegrante> model)
        {
            var uiResult = new UiResultPage<UiIntegrante>
            {
                Result = UiEnum.TransactionResult.Failed
            };

            try
            {
                if (Session["ListaReceptores"] == null)
                {
                    Session["ListaReceptores"] = new List<UiIntegrante>();
                }

                var listaReceptores = (List<UiIntegrante>) Session["ListaReceptores"];

                var clientService = new ServicesCatalog();

                switch (model.Query.IdTipoReceptor)
                {
                    case 1:

                        var listaIntegrantes =
                            clientService.ObtenerIntegrantes(model.Paging.CurrentPage, model.Paging.Rows)
                                .Where(x => x.IdJerarquia == 26 /*model.Query.IdJerarquia*/)
                                .Take(10)
                                .ToList();

                        Parallel.ForEach(listaIntegrantes, item =>
                        {
                            item.IdTipoReceptor = 1;
                            item.TipoReceptor = "Rol";
                            item.IdRol = model.Query.IdRol;
                        });

                        listaReceptores.AddRange(listaIntegrantes);

                        break;

                    case 2:

                        var integrante = clientService.ObtenerIntegrantes(model.Paging.CurrentPage, model.Paging.Rows)
                            //.FirstOrDefault(
                            //    x => x.Area.Contains(model.Query.Area) ||
                            //    x.Nombre.Contains(model.Query.Nombre) ||
                            //    x.ApPaterno.Contains(model.Query.ApPaterno) ||
                            //    x.ApMaterno.Contains(model.Query.ApMaterno) ||
                            //    x.Correo.Contains(model.Query.Correo)
                            //    );
                            .FirstOrDefault();

                        if (integrante != null)
                        {
                            integrante.IdTipoReceptor = 2;
                            integrante.TipoReceptor = "Integrante";

                            listaReceptores.Add(integrante);
                        }

                        break;

                    case 4:

                        integrante = new UiIntegrante
                        {
                            IdTipoReceptor = 4,
                            TipoReceptor = "Alternativo",
                            Correo = model.Query.Correo
                        };

                        listaReceptores.Add(integrante);

                        break;

                    case 3:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Session["ListaReceptore"] = listaReceptores;

                uiResult.List = listaReceptores;
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

        public JsonResult QuitarReceptor(UiResultPage<UiIntegrante> model)
        {

            var uiResult = new UiResultPage<UiIntegrante>
            {
                Result = UiEnum.TransactionResult.Failed
            };

            try
            {
                if (Session["ListaReceptores"] == null)
                {
                    Session["ListaReceptores"] = new List<UiIntegrante>();
                }

                var listaReceptores = (List<UiIntegrante>) Session["ListaReceptores"];

                var objeto =
                    listaReceptores.FirstOrDefault(
                        x =>
                            (x.IdTipoReceptor == model.Query.IdTipoReceptor) &&
                            (x.Identificador == model.Query.Identificador || x.IdUsuario == model.Query.IdUsuario ||
                             x.Correo == model.Query.Correo));

                listaReceptores.Remove(objeto);

                Session["ListaReceptore"] = listaReceptores;

                uiResult.List = listaReceptores;
                uiResult.Paging.Pages = 1;
                uiResult.Paging.Rows = 1000;
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
        public JsonResult NotificacionGuardar(UiResult<UiNotificacion> model)
        {
            var uiResult = new UiResultPage<UiNotificacion>
            {
                Result = UiEnum.TransactionResult.Failed
            };

            try
            {
                var clientService = new ServiceConfiguration();
                uiResult.Result = clientService.NotificacionGuardar(model.ObjectResult)
                    ? UiEnum.TransactionResult.Success
                    : UiEnum.TransactionResult.Failed;
                //uiResult.List = clientService.ObtenerDivisiones(model.Paging.CurrentPage, model.Paging.Rows);
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
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecWeb", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                uiResult.Message = ErrorMessage.GenericMessage;
            }

            return Json(uiResult);
        }

        public ActionResult ModificacionDeNotifacionesAlertas(UiResultPage<UiIntegrantes> model)
        {
            return View();
        }

        [HttpPost]
        public ActionResult GuardarNotificacion(UiResultPage<UiNotificacion> model)
        {
            var uiResult = new UiResultPage<UiReceptorAlerta> {Result = UiEnum.TransactionResult.Failed};

            var query = model.Query;

            var listaRecetoresAlerta = ((List<UiIntegrante>) Session["ListaReceptores"]).Select(x => new ReceptorAlerta
            {
                IdNotificacion = 0,
                IdPersona = x.Identificador,
                IdRol = x.IdRol,
                IdUsuario = x.IdUsuario,
                IdTipoReceptor = x.IdTipoReceptor,
                Correo = x.Correo
            }).ToList();

            var notificacion = new Notificaciones
            {
                IdNotificacion = query.IdNotificacion,
                IdTipoServicio = query.IdTipoServicio,
                IdActividad = query.IdActividad,
                IdFase = query.IdFase,
                CuerpoCorreo = query.CuerpoCorreo,
                EsCorreo = query.EsCorreo,
                EsSistema = query.EsSistema,
                EmitirAlerta = query.EmitirAlerta,
                TiempoAlerta = query.TiempoAlerta,
                Frecuencia = query.Frecuencia,
                AlertaEsCorreo = query.AlertaEsCorreo,
                AlertaEsSistema = query.AlertaEsSistema,
                CuerpoAlerta = query.CuerpoAlerta,
                Activo = true
            };

            try
            {
                var clientService = new ServiceConfiguration();

                var result = clientService.ReceptoresAlertaGuardarLista(listaRecetoresAlerta, notificacion)
                    ? UiEnum.TransactionResult.Success
                    : UiEnum.TransactionResult.Failed;

                uiResult.Result = result;
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

        #region SERVICOS

        public ActionResult Servicios()
        {
            int page = 1, rows = 100000;
            ServicesCatalog servicesCatalog = new ServicesCatalog();
            var tipoPago = servicesCatalog.ObtenerTiposPago(page, rows);
            var areas = servicesCatalog.ObtenerAreas(page, rows).Select(f => new { Identificador = f.Identificador, Name = f.Name }).ToList();
            var servicios = servicesCatalog.ObtenerTiposServicio(page, rows).Select(f => new { Identificador = f.Identificador, Name = f.Name }).ToList();
            var actividades = servicesCatalog.ObtenerActividades(page, rows);
            var regimenFiscal = servicesCatalog.ObtenerRegimenFiscal(page, rows);

            ViewBag.TipoPago = tipoPago;
            ViewBag.Areas = new SelectList(areas, "Identificador", "Name");
            ViewBag.Servicios = new SelectList(servicios, "Identificador", "Name");
            ViewBag.Actividades = actividades;
            ViewBag.RegimenFiscal = regimenFiscal;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ConfiguracionServicioGuardar(UiResultPage<UiConfiguracionServicio> model)
        {
            UiResultPage<UiConfiguracionServicio> uiResult = new UiResultPage<UiConfiguracionServicio>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.Result = clientService.SaveConfiguracionServicio(model.List)
                    ? UiEnum.TransactionResult.Success
                    : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ConfiguracionServicioObtenerConfiguracionPaginado(
                    model.Paging.CurrentPage, model.Paging.Rows);
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
        public JsonResult ConsultaConfiguracionServicioPorIds(UiResultPage<UiConfiguracionServicio> model)
        {
            UiResultPage<UiConfiguracionServicio> uiResult = new UiResultPage<UiConfiguracionServicio>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                uiResult.List =
                    clientService.ConfiguracionServicioObtenerPorIdTipoServicioIdCentroCosto(model.ObjectResult);
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

        public ActionResult DocumentoConfigurado(List<ActividadesConfiguradas> listConfiguracion)
        {
            int page = 1, rows = 100000;
            DocumetoConfiguracion configuracioon = new DocumetoConfiguracion();
            List<UiTiposDocumento> listDocumentos = new List<UiTiposDocumento>();
            List<UiConfiguracionServicio> servicioConfigurado = new List<UiConfiguracionServicio>();
            ServicesCatalog clientService = new ServicesCatalog();



            if (listConfiguracion != null)
            {
                foreach (var configuracion in listConfiguracion)
                {


                    var existe = clientService.TipoDocumentoObtenerPorCriterio(page, rows, new UiTiposDocumento()
                    {
                        IdActividad = configuracion.IdActividad
                    });
                    if (existe != null)
                        if (existe.Count() > 0)
                            listDocumentos.AddRange(existe);
                    var preconfigurado =
                        clientService.ConfiguracionServicioObtenerPorIdTipoServicioIdCentroCosto(new UiConfiguracionServicio
                            ()
                        {
                            IdCentroCostos = configuracion.IdArea,
                            IdTipoServicio = configuracion.IdServicio
                        });
                    if (preconfigurado != null)
                        servicioConfigurado.AddRange(preconfigurado);
                }
                configuracioon.Actividades = listConfiguracion;
                configuracioon.Documentos = listDocumentos;
                configuracioon.ConfiguracionServicio = servicioConfigurado;
            }



            return PartialView("DocumentoConfigurado", configuracioon);
        }

        public ActionResult ConfiguracionServicioObtenerConfiguracionPaginado(int page, int rows)
        {
            UiResultPage<UiDetalleConfiguracionServicio> uiResult = new UiResultPage<UiDetalleConfiguracionServicio>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                var configurado = clientService.ConfiguracionDetalle(page, rows);
                List<UiDetalleConfiguracionServicio> listConfiguracion = new List<UiDetalleConfiguracionServicio>();

                uiResult.List = listConfiguracion;
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

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ConfiguracionDetalle(UiResultPage<UiConfiguracionServicio> model)
        {
            UiResultPage<UiDetalleConfiguracionServicio> uiResult = new UiResultPage<UiDetalleConfiguracionServicio>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                ServicesCatalog clientService = new ServicesCatalog();
                List<UiDetalleConfiguracionServicio> listUiDetalleConfiguracionServicio = new List<UiDetalleConfiguracionServicio>();
                var listDetalle = clientService.ConfiguracionDetalle(model.Paging.Pages, model.Paging.Rows);
                foreach (var detalle in listDetalle)
                {
                    var existe = listUiDetalleConfiguracionServicio.Where(f => f.IdTipoServicio == detalle.IdTipoServicio && f.IdCentroCosto == detalle.IdCentroCosto).FirstOrDefault();
                    if (existe != null)
                    {
                        existe.TipoPago = (existe.TipoPago.Contains(detalle.TipoPago) ? existe.TipoPago : existe.TipoPago + "/" + detalle.TipoPago);
                        existe.RegimenFiscal = (existe.RegimenFiscal.Contains(detalle.RegimenFiscal) ? existe.RegimenFiscal : existe.RegimenFiscal + "/" + detalle.RegimenFiscal);
                    }
                    else
                        listUiDetalleConfiguracionServicio.Add(detalle);
                }


                if (model.ObjectSearch != null)
                    uiResult.List = listUiDetalleConfiguracionServicio.Where(f => f.IdTipoServicio == model.ObjectSearch.IdTipoServicio).ToList();
                else
                    uiResult.List = listUiDetalleConfiguracionServicio;
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

        #region Areas validadoras

        public ActionResult AreasValidadoras()
        {
            return View();
        }

        public PartialViewResult AreasValidadora(UiAreasValidadoras model)
        {
            Session.Remove("ListaValidadores");

            var clientService = new ServicesCatalog();

            var taskAreas = Task.Factory.StartNew(() =>
            {
                var areas = new List<UiArea>();

                var resultadoAreas = clientService.ObtenerAreas(1, 100000);

                areas.AddRange(resultadoAreas);

                var listaAreas = areas.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name,
                    Selected = c.Identificador == 0
                }).ToArray();

                ViewBag.ListaAreas = listaAreas;
            });

            var taskTiposServicios = Task.Factory.StartNew(() =>
            {
                var tipoServicio = new List<UiTiposServicio>();
                //{
                //    new UiTiposServicio {Identificador = 0, Name = "Selecciona un tipo de servicio"}
                //};

                var resultadoTiposServicio = clientService.ObtenerTiposServicio(1, 1000);

                tipoServicio.AddRange(resultadoTiposServicio);

                var listaTipoServicio = tipoServicio.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name,
                    Selected = c.Identificador == model.IdTipoServicio
                }).ToArray();

                ViewBag.ListaTipoServicio = listaTipoServicio;
            });

            var taskActividades = Task.Factory.StartNew(() =>
            {
                var actividad = new List<UiActividad>();
                //{
                //    new UiActividad {Identificador = 0, Name = "Selecciona una Actividad"}
                //};

                var resultadoAreas = clientService.ObtenerActividades(1, 1000).Where(x => x.Validacion).ToList();

                actividad.AddRange(resultadoAreas);

                var listaActividades = actividad.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name
                }).ToArray();

                ViewBag.ListaActividades = listaActividades;
            });

            var resultados = new[] {taskActividades, taskAreas, taskTiposServicios};

            var areaValidadoras = new List<UiAreasValidadoras>();

            switch (model.Action)
            {
                case UiEnumEntity.New:

                    ViewBag.Title = "Crea la configuracion de un area validadora";

                    break;
                case UiEnumEntity.Edit:

                    areaValidadoras = new ServiceConfiguration().AreasValidadorasObtenerTodos(1, 1000)
                        .Where(x => x.IdTipoServicio == model.IdTipoServicio).ToList();

                    Parallel.ForEach(areaValidadoras, area =>
                    {
                        area.Actividad =
                            new ServicesCatalog().ObtenerActividades(1, 10000)
                                .FirstOrDefault(x => x.Identificador == area.IdActividad)
                                .Name;
                        area.CentroCostos = new ServicesCatalog().ObtenerAreas(1, 10000)
                            .FirstOrDefault(x => x.Identificador == Convert.ToInt32(area.IdCentroCosto))
                            .Name;
                    });

                    Session["ListaValidadores"] = areaValidadoras;

                    ViewBag.Title = "Modificar la configuracion de un area validadora";

                    break;
                case UiEnumEntity.View:

                    areaValidadoras =
                        new ServiceConfiguration().AreasValidadorasObtenerTodos(1, 1000)
                            .Where(x => x.IdTipoServicio == model.IdTipoServicio).ToList();

                    Parallel.ForEach(areaValidadoras, area =>
                    {
                        area.Actividad =
                            new ServicesCatalog().ObtenerActividades(1, 10000)
                                .FirstOrDefault(x => x.Identificador == area.IdActividad)
                                .Name;
                        area.CentroCostos = new ServicesCatalog().ObtenerAreas(1, 10000)
                            .FirstOrDefault(x => x.Identificador == Convert.ToInt32(area.IdCentroCosto))
                            .Name;
                    });

                    Session["ListaValidadores"] = areaValidadoras;

                    ViewBag.Title = "Detalle de la configuracion de un area validadora";

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Task.WaitAll(resultados);

            return PartialView(model);
        }

        public JsonResult AreasValidadorasConsulta(UiResultPage<UiAreasValidadoras> model)
        {
            var uiResult = new UiResultPage<UiAreasValidadoras> {Result = UiEnum.TransactionResult.Failed};

            try
            {
                var clientService = new ServiceConfiguration();

                var listaServicios = new ServicesCatalog().ObtenerTiposServicio(1, 1000);
                var listaCompleta = clientService.AreasValidadorasObtenerTodos(1, 1000).Where(x=>x.EsActivo ==model.Query.EsActivo).ToList();
                var lista = listaCompleta.GroupBy(u => u.IdTipoServicio)
                    .Select(grp => new UiAreasValidadoras
                    {
                        IdTipoServicio = grp.Key,
                        TipoServicio = listaServicios.FirstOrDefault(x => x.Identificador == grp.Key).Name,
                        EsActivo = listaCompleta.FirstOrDefault().EsActivo
                    }).ToList();

                uiResult.List = lista;
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = 1;
                uiResult.Paging.CurrentPage = 10000;
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

        public ActionResult ObtenerValidadores(UiAreasValidadoras model)
        {
            List<UiAreasValidadoras> areasValidadoras;

            if (Session["ListaValidadores"] == null)
            {
                areasValidadoras = new List<UiAreasValidadoras>();
                Session["ListaValidadores"] = areasValidadoras;

            }

            areasValidadoras = (List<UiAreasValidadoras>) Session["ListaValidadores"];

            var uiResult = new UiResultPage<UiAreasValidadoras>
            {
                Result = UiEnum.TransactionResult.Failed
            };

            try
            {
                uiResult.List = areasValidadoras;
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

        public JsonResult AgregarValidador(UiResultPage<UiAreasValidadoras> model)
        {
            var uiResult = new UiResultPage<UiAreasValidadoras>
            {
                Result = UiEnum.TransactionResult.Failed
            };

            var validador = new UiAreasValidadoras
            {
                IdTipoServicio = Convert.ToInt32(model.Message),
                TipoServicio = model.Query.TipoServicio,
                IdActividad = model.Query.IdActividad,
                Actividad = model.Query.Actividad,
                IdCentroCosto = model.Query.IdCentroCosto,
                CentroCostos = model.Query.CentroCostos,
                Obligatorio = model.Query.Obligatorio
            };

            try
            {
                if (Session["ListaValidadores"] == null)
                {
                    Session["ListaValidadores"] = new List<UiAreasValidadoras>();
                }

                var listaValidadores = (List<UiAreasValidadoras>) Session["ListaValidadores"];

                var existe =
                    listaValidadores.FirstOrDefault(
                        x =>
                            x.IdTipoServicio == validador.IdTipoServicio && x.IdActividad == validador.IdActividad &&
                            x.IdCentroCosto == validador.IdCentroCosto);

                if (existe != null)
                {

                    Session["ListaValidadores"] = listaValidadores;

                    uiResult.List = listaValidadores;
                    //uiResult.Paging.Pages = clientService.Pages;
                    //uiResult.Paging.Rows = model.Paging.Rows;
                    uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                    uiResult.Message = "Ya fue añadida esta area";
                    uiResult.Result = UiEnum.TransactionResult.Success;
                }
                else
                {
                    listaValidadores.Add(validador);
                    Session["ListaValidadores"] = listaValidadores;

                    uiResult.List = listaValidadores;
                    //uiResult.Paging.Pages = clientService.Pages;
                    //uiResult.Paging.Rows = model.Paging.Rows;
                    uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                    uiResult.Message = "Se agrego el validador correctamente validador";
                    uiResult.Result = UiEnum.TransactionResult.Success;
                }

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

        public JsonResult QuitarValidador(UiResultPage<UiAreasValidadoras> model)
        {

            var uiResult = new UiResultPage<UiAreasValidadoras>
            {
                Result = UiEnum.TransactionResult.Failed
            };

            try
            {
                if (Session["ListaValidadores"] == null)
                {
                    Session["ListaValidadores"] = new List<UiAreasValidadoras>();
                }

                var listaValidadores = (List<UiAreasValidadoras>) Session["ListaValidadores"];

                var objeto =
                    listaValidadores.FirstOrDefault(
                        x =>
                            (x.IdActividad == model.Query.IdActividad) && (x.IdCentroCosto == model.Query.IdCentroCosto));

                listaValidadores.Remove(objeto);

                Session["ListaValidadores"] = listaValidadores;

                uiResult.List = listaValidadores;
                uiResult.Paging.Pages = 1;
                uiResult.Paging.Rows = 1000;
                uiResult.Message = "Se elimino al validador";
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
        public JsonResult AreasValidadorasGuardar(UiResultPage<UiAreasValidadoras> model)
        {
            var uiResult = new UiResultPage<UiAreasValidadoras> {Result = UiEnum.TransactionResult.Failed};

            var listaValidadores = (List<UiAreasValidadoras>) Session["ListaValidadores"];

            try
            {
                var clientService = new ServiceConfiguration();

                var guardar = clientService.AreasValidadorasGuadar(listaValidadores);

                uiResult.Result = guardar ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = guardar ? "Se guardo correctamente" : "No fue posible guardar la configuracion";
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;

                Session.Remove("ListaValidadores");
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
        public JsonResult AreasValidadorasActualizar(UiResultPage<UiAreasValidadoras> model)
        {
            var uiResult = new UiResultPage<UiAreasValidadoras> {Result = UiEnum.TransactionResult.Failed};

            var listaValidadores = (List<UiAreasValidadoras>) Session["ListaValidadores"];

            try
            {
                var clientService = new ServiceConfiguration();

                var guardar = clientService.AreasValidadorasActualizar(listaValidadores);

                uiResult.Result = guardar ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = guardar ? "Se guardo correctamente" : "No fue posible guardar la configuracion";
                uiResult.Paging.Pages = clientService.Pages;
                uiResult.Paging.Rows = model.Paging.Rows;
                uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                uiResult.Result = UiEnum.TransactionResult.Success;

                Session.Remove("ListaValidadores");
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
        public JsonResult AreasValidadorasCambiarEstatus(UiResultPage<UiAreasValidadoras> model)
        {
            var uiResult = new UiResultPage<UiAreasValidadoras> {Result = UiEnum.TransactionResult.Failed};

            try
            {
                var clientService = new ServiceConfiguration();
                var listaServicios = new ServicesCatalog().ObtenerTiposServicio(1, 1000);

                model.ObjectResult.EsActivo = model.Query.EsActivo;

                uiResult.Result = clientService.AreasValidadorasModificarEstatus(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;

                var lista = clientService.AreasValidadorasObtenerTodos(10, 1000).Where(x => x.EsActivo == model.Query.EsActivo).ToList();

                uiResult.List = 
                   lista.GroupBy(u => u.IdTipoServicio)
                    .Select(grp => new UiAreasValidadoras
                    {
                        IdTipoServicio = grp.Key,
                        TipoServicio = listaServicios.FirstOrDefault(x => x.Identificador == grp.Key).Name,
                        EsActivo = lista.FirstOrDefault().EsActivo
                    }).ToList();
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
    }

    #region Dummies
    public class Roles
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
    }

    public class DocumetoConfiguracion
    {
        public List<UiTiposDocumento> Documentos { get; set; }
        public List<ActividadesConfiguradas> Actividades { get; set; }
        public List<UiConfiguracionServicio> ConfiguracionServicio { get; set; }
    }

    public class ActividadesConfiguradas
    {
        public string Nombre { get; set; }
        public int IdActividad { get; set; }
        public int IdRegimen { get; set; }
        public int Tiempo { get; set; }
        public bool Activo { get; set; }
        public bool Requerido { get; set; }
        public string IdArea { get; set; }
        public int IdServicio { get; set; }
    }
    #endregion
}