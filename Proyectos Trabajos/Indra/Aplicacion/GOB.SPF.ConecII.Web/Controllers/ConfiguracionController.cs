namespace GOB.SPF.ConecII.Web.Controllers
{
    using Models;
    using Resources;
    using Servicios;

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
                uiResult.List = client.PlantillaObtenerPorCriterio(model.Paging.CurrentPage, model.Paging.Rows, model.ObjectResult);
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
                uiResult.Result = clientService.SaveDivisiones(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
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
                var listaNotificaciones = clientService.ObtenerNotificaciones(model.Paging.CurrentPage, model.Paging.Rows);

                uiResult.List = listaNotificaciones;
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

        public ActionResult CreacionDeNotifacionesAlertas(UiResultPage<UiIntegrantes> model)
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

            var resultados = new[] { taskActividades, taskAreas, taskFaces, taskRoles, taskTipoServicio };

            Task.WaitAll(resultados);

            return View();
        }

        public JsonResult ConsultaIntegrantes(UiResultPage<UiIntegrante> model)
        {
            var uiResult = new UiResultPage<UiIntegrante>();
            uiResult.Result = UiEnum.TransactionResult.Failed;

            try
            {
                var clientService = new ServicesCatalog();
                var listaIntegrantes = clientService.ObtenerIntegrantes(1, 100000);

                uiResult.List = listaIntegrantes;
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

            return Json(uiResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult NotificacionGuardar(UiResultPage<UiNotificacion> model)
        {
            var uiResult = new UiResultPage<UiNotificacion>
            {
                Result = UiEnum.TransactionResult.Failed
            };

            try
            {
                var clientService = new ServiceConfiguration();
                uiResult.Result = clientService.NotificacionGuardar(model.ObjectResult) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                //uiResult.List = clientService.ObtenerDivisiones(model.Paging.CurrentPage, model.Paging.Rows);
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

        public ActionResult ModificacionDeNotifacionesAlertas(UiResultPage<UiIntegrantes> model)
        {
            return View();
        }

        #endregion


        #region SERVICOS

        public ActionResult Servicios()
        {
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
                uiResult.Result = clientService.SaveConfiguracionServicio(model.List) ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.List = clientService.ConfiguracionServicioObtenerConfiguracionPaginado(model.Paging.CurrentPage, model.Paging.Rows);
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
    }

    public class Roles
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
    }
}