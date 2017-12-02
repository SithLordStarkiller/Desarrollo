using System.Xml.Schema;

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
    [Authorize]
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

        //[HttpPost]
        //[AllowAnonymous]
        //public PartialViewResult Plantilla(UiDivision model)
        //{
        //    switch (model.Action)
        //    {
        //        case UiEnumEntity.New:
        //            ViewBag.Title = "Crear División";
        //            break;
        //        case UiEnumEntity.Edit:
        //            ViewBag.Title = "Modificar División";
        //            break;
        //        case UiEnumEntity.View:
        //            ViewBag.Title = "Detalle de la División";
        //            break;
        //    }
        //    return PartialView(model);
        //}

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
                    model.Paging.Rows).Where(x => x.Activo).ToList();

                uiResult.List = listaNotificaciones;
                uiResult.Paging.Pages = listaNotificaciones.Count / model.Paging.Rows;
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
                    new Roles {IdRol = 1, NombreRol = "Administrador"},
                    new Roles { IdRol = 2,NombreRol = "Directivo"},
                    new Roles { IdRol = 3, NombreRol = "Analista"}
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

            var resultados = new[] { taskActividades, taskAreas, taskFaces, taskRoles, taskTipoServicio };

            Task.WaitAll(resultados);

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
                    new UiNotificacion { IdNotificacion = model.Query.IdNotificacion });

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

        public JsonResult ConsultaIntegrantes(UiResultPage<UiIntegrante> model)
        {
            var uiResult = new UiResultPage<UiIntegrante>
            {
                Result = UiEnum.TransactionResult.Failed
            };

            try
            {
                var clientService = new ServicesCatalog();

                var listaIntegrantes = new List<UiIntegrante>();

                switch (model.Query.IdTipoReceptor)
                {
                    case 1:
                        listaIntegrantes =
                            listaIntegrantes.Where(x => x.IdJerarquia == 26 /*model.Query.IdJerarquia*/)
                                .Take(5)
                                .ToList();
                        break;

                    case 2:
                        if (!string.IsNullOrEmpty(model.Query.Nombre))
                        {
                            var query = model.Query.Nombre;
                            listaIntegrantes =
                                new ServicesCatalog().ObtenerIntegrantes(1, 100000)
                                    .Where(
                                    x =>
                                    x.Area.Contains(query)
                                    || x.Nombre.Contains(query)
                                    || x.ApPaterno.Contains(query)
                                    || x.ApMaterno.Contains(query)
                                    || x.Correo.Contains(query)
                                    )
                                    .ToList();
                        }
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

        public JsonResult AgregarReceptor(UiResultPage<UiReceptorAlerta> model)
        {
            var uiResult = new UiResultPage<UiReceptorAlerta>
            {
                Result = UiEnum.TransactionResult.Failed
            };

            try
            {
                if (Session["ListaReceptores"] == null)
                {
                    Session["ListaReceptores"] = new List<UiReceptorAlerta>();
                }

                var listaReceptores = (List<UiReceptorAlerta>)Session["ListaReceptores"];

                var clientService = new ServicesCatalog();

                switch (model.Query.IdTipoReceptor)
                {
                    //Rol
                    case 1:
                        var exiteRol = listaReceptores.FirstOrDefault(x => x.IdRol == model.Query.IdRol);

                        if (exiteRol == null)
                        {

                            listaReceptores.Add(model.Query);


                            Session["ListaReceptore"] = listaReceptores;

                            uiResult.List = listaReceptores;
                            uiResult.Paging.Pages = clientService.Pages;
                            uiResult.Paging.Rows = model.Paging.Rows;
                            uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                            uiResult.Message = "El rol fue añadido correctamente";

                            uiResult.Result = UiEnum.TransactionResult.Success;
                        }
                        else
                        {
                            uiResult.List = listaReceptores;
                            uiResult.Message = "El rol seleccionado ya fue añadido, seleccione uno diferente";

                            uiResult.Result = UiEnum.TransactionResult.Failed;
                        }
                        break;

                    //Integrante
                    case 2:
                        var existeIntegrante =
                            listaReceptores.FirstOrDefault(
                                x =>
                                    x.IdTipoReceptor == model.Query.IdTipoReceptor &&
                                    x.IdPersona == model.Query.IdPersona);

                        if (existeIntegrante == null)
                        {

                            var integrante = new UiReceptorAlerta
                            {
                                IdPersona = model.Query.IdPersona,
                                IdTipoReceptor = model.Query.IdTipoReceptor,
                                TipoReceptor = model.Query.TipoReceptor,
                                Descripcion = model.Query.Descripcion,
                                EsCopia = model.Query.EsCopia,
                                Correo = model.Query.Correo
                            };

                            listaReceptores.Add(integrante);

                            Session["ListaReceptore"] = listaReceptores;

                            uiResult.List = listaReceptores;
                            uiResult.Paging.Pages = clientService.Pages;
                            uiResult.Paging.Rows = model.Paging.Rows;
                            uiResult.Paging.CurrentPage = model.Paging.CurrentPage;
                            uiResult.Message = "El integrante fue añadido correctamente";

                            uiResult.Result = UiEnum.TransactionResult.Success;
                        }
                        else
                        {
                            uiResult.List = listaReceptores;
                            uiResult.Message = "El integrante seleccionado ya fue añadido, seleccione uno diferente";

                            uiResult.Result = UiEnum.TransactionResult.Failed;
                        }
                        break;

                    //Cliente
                    case 3:
                        var clientes = clientService.TiposContacto();

                        var listaClientes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(model.Message);

                        var clientesExistentes =
                            listaReceptores.Where(x => listaClientes.Any(z => x.IdTipoContacto == z)).ToList();

                        if (clientesExistentes.Count <= 0)
                        {
                            listaReceptores.AddRange(listaClientes.Select(item => new UiReceptorAlerta
                            {
                                IdTipoContacto = item,
                                Descripcion = clientes.FirstOrDefault(x => x.Identificador == item).Name,
                                IdTipoReceptor = 3,
                                TipoReceptor = "Cliente",
                                Correo = "N/A",
                                EsCopia = model.Query.EsCopia
                            }));


                            Session["ListaReceptore"] = listaReceptores;

                            uiResult.List = listaReceptores;
                            uiResult.Message = "Los clientes fueron añadidos correctamente";

                            uiResult.Result = UiEnum.TransactionResult.Success;
                        }
                        else
                        {
                            uiResult.List = listaReceptores;
                            uiResult.Message = "Uno o varios Clientes ya fue añadidos, seleccione uno diferente";

                            uiResult.Result = UiEnum.TransactionResult.Failed;
                        }

                        break;

                    //Alternativo
                    case 4:
                        var existeAlternativo =
                            listaReceptores.FirstOrDefault(x => x.IdTipoReceptor == 4 && x.Correo == model.Query.Correo);
                        if (existeAlternativo == null)
                        {
                            var alternativo = new UiReceptorAlerta
                            {
                                IdTipoReceptor = 4,
                                TipoReceptor = "Alternativo",
                                Descripcion = "Correo Adicional",
                                Correo = model.Query.Correo,
                                EsCopia = model.Query.EsCopia
                            };

                            listaReceptores.Add(alternativo);

                            uiResult.List = listaReceptores;
                            uiResult.Message = "El correo fue añadido correctamente";

                            uiResult.Result = UiEnum.TransactionResult.Success;
                        }
                        else
                        {
                            uiResult.List = listaReceptores;
                            uiResult.Message = "El correo ya fue añadido, ingrese uno diferente";

                            uiResult.Result = UiEnum.TransactionResult.Failed;
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
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

        public JsonResult QuitarReceptor(UiResultPage<UiReceptorAlerta> model)
        {

            var uiResult = new UiResultPage<UiReceptorAlerta>
            {
                Result = UiEnum.TransactionResult.Failed
            };

            try
            {
                if (Session["ListaReceptores"] == null)
                {
                    Session["ListaReceptores"] = new List<UiReceptorAlerta>();
                }

                var listaReceptores = (List<UiReceptorAlerta>)Session["ListaReceptores"];

                var objeto =
                    listaReceptores.FirstOrDefault(
                        x =>
                            (x.IdTipoReceptor == model.Query.IdTipoReceptor) &&
                            (
                               x.IdRol == model.Query.IdRol
                            || x.IdPersona == model.Query.IdPersona
                            || x.IdTipoContacto == model.Query.IdTipoReceptor
                            || x.Correo == model.Query.Correo));

                if (objeto != null)
                {

                    listaReceptores.Remove(objeto);

                    Session["ListaReceptore"] = listaReceptores;
                    uiResult.Message = "El receptor fue eliminado correctamente";
                }
                else
                {
                    uiResult.Message = "El receptor no fue eliminado o ocurrio un error interno";
                }

                uiResult.List = listaReceptores;

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

        public ActionResult ObtenerReceptores(UiResultPage<UiReceptorAlerta> model)
        {
            var idNotificacion = Convert.ToInt32(model.Message);
            if (Session["ListaReceptores"] == null)
            {
                Session["ListaReceptores"] = new List<UiReceptorAlerta>();
            }

            var listaReceptores = (List<UiReceptorAlerta>)Session["ListaReceptores"];

            var uiResult = new UiResultPage<UiReceptorAlerta>
            {
                Result = UiEnum.TransactionResult.Failed
            };

            try
            {
                var clientService = new ServiceConfiguration();

                var receptores = clientService.ReceptoresAlertaObtenerTodos(1, 10000,
                    new UiReceptorAlerta { IdNotificacion = idNotificacion });

                var listaClientes = new ServicesCatalog().TiposContacto();
                var listaRoles = new List<Roles>
                {
                    new Roles {IdRol = 0, NombreRol = "Selecciona un Rol"},
                    new Roles {IdRol = 1, NombreRol = "Administrador"},
                    new Roles { IdRol = 2,NombreRol = "Directivo"},
                    new Roles { IdRol = 3, NombreRol = "Analista"}
                };
                var listaIntegrantes = new ServicesCatalog().ObtenerIntegrantes(1, 1000);

                foreach (var receptor in receptores)
                {
                    if (receptor?.IdRol > 0)
                    {
                        var rol = listaRoles.FirstOrDefault(x => x.IdRol == receptor.IdRol);
                        if (rol != null)
                        {
                            receptor.Descripcion = rol.NombreRol;
                            receptor.Correo = "N/A";
                            receptor.IdTipoReceptor = 1;
                            receptor.TipoReceptor = "Rol";
                        }
                    }
                    else if (receptor?.IdPersona != new Guid())
                    {
                        var integrante = listaIntegrantes.FirstOrDefault(x => x.Identificador == receptor.IdPersona);
                        if (integrante != null)
                        {
                            receptor.Descripcion = integrante.NombreCompleto;
                            receptor.Correo = integrante.CorreoTrabajo;
                            receptor.IdTipoReceptor = 2;
                            receptor.TipoReceptor = "Integrante";
                        }
                    }
                    else if (receptor?.IdTipoContacto > 0)
                    {
                        var cliente = listaClientes.FirstOrDefault(x => x.Identificador == receptor.IdTipoContacto);
                        if (cliente != null)
                        {
                            receptor.Descripcion = cliente.Name;
                            receptor.Correo = "N/A";
                            receptor.IdTipoReceptor = 3;
                            receptor.TipoReceptor = "Cliente";
                        }
                    }
                    else if (!string.IsNullOrEmpty(receptor.Correo))
                    {
                        receptor.Descripcion = "Correo Alternativo";
                        receptor.IdTipoReceptor = 4;
                        receptor.TipoReceptor = "Alternativo";

                    }
                }

                listaReceptores.AddRange(receptores);

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

        [HttpPost]
        public ActionResult GuardarNotificacion(UiResultPage<UiNotificacion> model)
        {
            var uiResult = new UiResultPage<UiReceptorAlerta> { Result = UiEnum.TransactionResult.Failed };

            var query = model.Query;

            var listaRecetoresAlerta = ((List<UiReceptorAlerta>)Session["ListaReceptores"]).Select(x => new ReceptorAlerta
            {
                IdNotificacion = 0,
                IdPersona = x.IdPersona,
                IdRol = x.IdRol,
                IdTipoReceptor = x.IdTipoReceptor,
                IdTipoContacto = x.IdTipoContacto,
                Correo = x.Correo,
                EsCopia = x.EsCopia
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


            if (listaRecetoresAlerta.Count >= 0)
            {
                try
                {
                    var clientService = new ServiceConfiguration();

                    var result = clientService.ReceptoresAlertaGuardarLista(listaRecetoresAlerta, notificacion)
                        ? UiEnum.TransactionResult.Success
                        : UiEnum.TransactionResult.Failed;

                    uiResult.Result = result;
                    uiResult.Message = "Se guardo correctamente la notificaion";
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
            else
            {
                uiResult.Message = "Es nesesario que se agrege un receptor";
                uiResult.Result = UiEnum.TransactionResult.Success;
            }
            return Json(uiResult);
        }

        public ActionResult EditarNotificacion(int idNotificacion)
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
                    new Roles {IdRol = 1, NombreRol = "Administrador"},
                    new Roles { IdRol = 2,NombreRol = "Directivo"},
                    new Roles { IdRol = 3, NombreRol = "Analista"}
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


            var resultados = new[] { taskActividades, taskAreas, taskFaces, taskRoles, taskTipoServicio };

            Task.WaitAll(resultados);
            var notificacion = new ServiceConfiguration().ObtenerNotificacionPorId(1, 1000, new UiNotificacion { IdNotificacion = idNotificacion });

            ViewBag.IdNotificacion = idNotificacion;

            return View(notificacion);
        }

        [HttpPost]
        public ActionResult ActualizarNotificacion(UiResultPage<UiNotificacion> model)
        {
            var uiResult = new UiResultPage<UiReceptorAlerta> { Result = UiEnum.TransactionResult.Failed };

            var query = model.Query;

            var listaRecetoresAlerta = ((List<UiReceptorAlerta>)Session["ListaReceptores"]).Select(x => new ReceptorAlerta
            {
                IdNotificacion = x.IdNotificacion,
                IdPersona = x.IdPersona,
                IdRol = x.IdRol,
                IdTipoReceptor = x.IdTipoReceptor,
                IdTipoContacto = x.IdTipoContacto,
                Correo = x.Correo,
                EsCopia = x.EsCopia
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


            if (listaRecetoresAlerta.Count >= 0)
            {
                try
                {
                    var clientService = new ServiceConfiguration();

                    var result = clientService.ReceptoresAlertaActualizar(listaRecetoresAlerta, notificacion)
                        ? UiEnum.TransactionResult.Success
                        : UiEnum.TransactionResult.Failed;

                    uiResult.Result = result;
                    uiResult.Message = "Se guardo correctamente la notificaion";
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
            else
            {
                uiResult.Message = "Es nesesario que se agrege un receptor";
                uiResult.Result = UiEnum.TransactionResult.Success;
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

                var areasValidadas = new ServiceConfiguration().AreasValidadorasObtenerTodos(1, 11000);

                var resultadoTiposServicio = clientService.ObtenerTiposServicio(1, 1000);

                tipoServicio.AddRange(resultadoTiposServicio);

                var listaTipoServicio = tipoServicio.Select(c => new SelectListItem
                {
                    Value = c.Identificador.ToString(),
                    Text = c.Name,
                    Selected = c.Identificador == model.IdTipoServicio
                }).ToArray();

                ViewBag.ListaTipoServicio = listaTipoServicio;
                ViewBag.AreasValidadas = areasValidadas;
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

            var resultados = new[] { taskActividades, taskAreas, taskTiposServicios };

            var areaValidadoras = new List<UiAreasValidadoras>();

            switch (model.Action)
            {
                case UiEnumEntity.New:

                    ViewBag.Title = "Crea la configuracion de un area validadora";

                    break;
                case UiEnumEntity.Edit:

                    areaValidadoras = new ServiceConfiguration().AreasValidadorasObtenerTodos(1, 1000)
                        .Where(x => x.IdTipoServicio == model.IdTipoServicio).ToList();

                    var centroCostos = new ServicesCatalog().ObtenerAreas(1, 10000);
                    var actividades = new ServicesCatalog().ObtenerActividades(1, 10000);

                    Parallel.ForEach(areaValidadoras, area =>
                    {
                        area.Actividad =
                            actividades
                                .FirstOrDefault(x => x.Identificador == area.IdActividad)
                                .Name;
                        area.CentroCostos = centroCostos
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
            var uiResult = new UiResultPage<UiAreasValidadoras> { Result = UiEnum.TransactionResult.Failed };

            try
            {
                var clientService = new ServiceConfiguration();

                var listaServicios = new ServicesCatalog().ObtenerTiposServicio(1, 1000);
                var listaCompleta = clientService.AreasValidadorasObtenerTodos(1, 1000).Where(x => x.EsActivo == model.Query.EsActivo).ToList();
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

            areasValidadoras = (List<UiAreasValidadoras>)Session["ListaValidadores"];

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

                var listaValidadores = (List<UiAreasValidadoras>)Session["ListaValidadores"];

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

                var listaValidadores = (List<UiAreasValidadoras>)Session["ListaValidadores"];

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
            var uiResult = new UiResultPage<UiAreasValidadoras> { Result = UiEnum.TransactionResult.Failed };

            var listaValidadores = (List<UiAreasValidadoras>)Session["ListaValidadores"];

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
            var uiResult = new UiResultPage<UiAreasValidadoras> { Result = UiEnum.TransactionResult.Failed };

            var listaValidadores = (List<UiAreasValidadoras>)Session["ListaValidadores"];

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
            var uiResult = new UiResultPage<UiAreasValidadoras> { Result = UiEnum.TransactionResult.Failed };

            try
            {
                var clientService = new ServiceConfiguration();
                var listaServicios = new ServicesCatalog().ObtenerTiposServicio(1, 1000);

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