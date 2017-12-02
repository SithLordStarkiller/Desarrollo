using System.Web;
using GOB.SPF.ConecII.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace GOB.SPF.ConecII.Web.Controllers
{
    using Models;
    using Resources;
    using Servicios;
    using Models.Seguridad;
    using Models.Generico;

    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    [Authorize]
    public class SeguridadController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #region Administracion de usuarios

        public ActionResult AdminUsuarios()
        {
            var roles = new List<Roles>
            {
                new Roles { IdRol = 0, NombreRol = "Selecciona un Rol"},
                new Roles { IdRol = 1, NombreRol = "Administrador"},
                new Roles { IdRol = 2, NombreRol = "Directivo"},
                new Roles { IdRol = 3, NombreRol = "Analista"}
            };

            var areas = new List<UiArea>
            {
               new UiArea{ Identificador = 0, Name = "Seleccion un Area"}
            };

            areas.AddRange(new ServicesCatalog().ObtenerAreas(1, 1000));

            var listaRoles = roles.Select(c => new SelectListItem
            {
                Value = c.IdRol.ToString(),
                Text = c.NombreRol,
                Selected = c.IdRol == 0
            }).ToArray();

            var listaAreas = areas.Select(c => new SelectListItem
            {
                Value = c.Identificador.ToString(),
                Text = c.Name,
                Selected = c.Identificador == 0
            }).ToArray();

            ViewBag.ListaRoles = listaRoles;
            ViewBag.ListaAreas = listaAreas;

            return View();
        }

        public PartialViewResult AdminUsuario(UiUsuario model)
        {
            var areas = new List<UiArea>
            {
               new UiArea{ Identificador = 0, Name = "Seleccion un Area" }
            };

            areas.AddRange(new ServicesCatalog().ObtenerAreas(1, 1000));

            var listaAreas = areas.Select(c => new SelectListItem
            {
                Value = c.Identificador.ToString(),
                Text = c.Name,
                Selected = c.Identificador == 0
            }).ToArray();

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear rol de usuario";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar rol de usuario";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle del rol de usuarios";
                    break;
                case UiEnumEntity.Add:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ViewBag.ListaAreas = listaAreas;

            return PartialView(model);
        }

        #endregion region

        #region Administracion de permisos

        public async Task<ActionResult> AdminPermisos()
        {
            var areas = new List<UiArea>
            {
               new UiArea{ Identificador = 0, Name = "Seleccion un Area" }
            };

            areas.AddRange(new ServicesCatalog().ObtenerAreas(1, 1000));

            var listaAreas = areas.Select(c => new SelectListItem
            {
                Value = c.Identificador.ToString(),
                Text = c.Name,
                Selected = c.Identificador == 0
            }).ToArray();

            ViewBag.ListaAreas = listaAreas;

            //roles

            var roles = new List<UiRol>
            {
               new UiRol{ Id = 0, Name = "Seleccion un rol" }
            };

            var peticionRoles = new ServicesRol();

            roles.AddRange(await peticionRoles.ObtenerPorTipoUsuario(new UiRol(0) { Activo = true }, false));

            var listaRoles = roles.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = c.Id == 0
            }).ToArray();

            ViewBag.ListaRoles = listaRoles;

            //modulos

            var modulos = new List<UiModulo>
            {
               new UiModulo{ Id = 0, Nombre = "Seleccion un modulo" }
            };

            var peticion = new UiPeticion<UiModulo> { Solicitud = new UiModulo { Activo = true }, Paginado = new Entities.Paging { All = true } };
            var resultado = await new ServicesCatalog().ObtenerModulosPorCriterio(peticion);
            modulos.AddRange(resultado);

            var listaModulos = modulos.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre,
                Selected = c.Id == 0
            }).ToArray();

            ViewBag.ListaModulos = listaModulos;

            return View();
        }

        public async Task<PartialViewResult> AdminPermiso(UiRolModulo model)
        {
            var areas = new List<UiArea>
            {
               new UiArea{ Identificador = 0, Name = "Seleccion un Area" }
            };

            areas.AddRange(new ServicesCatalog().ObtenerAreas(1, 1000));

            var listaAreas = areas.Select(c => new SelectListItem
            {
                Value = c.Identificador.ToString(),
                Text = c.Name,
                Selected = c.Identificador == 0
            }).ToArray();

            ViewBag.ListaAreas = listaAreas;

            //roles

            var roles = new List<UiRol>
            {
               new UiRol{ Id = 0, Name = "Seleccion un rol" }
            };

            var peticionRoles = new ServicesRol();

            roles.AddRange(await peticionRoles.ObtenerPorTipoUsuario(new UiRol(0) { Activo = true }, false));

            var listaRoles = roles.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = c.Id == 0
            }).ToArray();

            ViewBag.ListaRoles = listaRoles;

            //modulos

            var modulos = new List<UiModulo>
            {
               new UiModulo{ Id = 0, Nombre = "Seleccion un modulo" }
            };

            var peticion = new UiPeticion<UiModulo> { Solicitud = new UiModulo { Activo = true }, Paginado = new Entities.Paging { All = true } };
            var resultado = await new ServicesCatalog().ObtenerModulosPorCriterio(peticion);
            modulos.AddRange(resultado);

            var listaModulos = modulos.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre,
                Selected = c.Id == 0
            }).ToArray();

            ViewBag.ListaModulos = listaModulos;

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear de permiso de rol";
                    break;

                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar de permiso de rol";
                    break;

                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle de permiso de rol";
                    break;

                case UiEnumEntity.Add:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return PartialView(model);
        }

        public async Task<JsonResult> AdminPermisosConsultar(UiResultPage<UiRolModulo> model)
        {
            var uiResult = new UiResultPage<UiRolModulo> { Result = UiEnum.TransactionResult.Failed };

            try
            {
                if (model.Query == null)
                {
                    uiResult.List = (List<UiRolModulo>) await new ServicesSeguridad().RolesModulosObtenerTodos();
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
        
        [HttpPost]
        [AllowAnonymous]
        public JsonResult AdminPermisosGuardar(UiResultPage<UiPermisosRol> model)
        {
            var uiResult = new UiResultPage<UiRol> { Result = UiEnum.TransactionResult.Failed };

            try
            {
                var guardar = true;

                uiResult.Result = guardar ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = guardar ? "Se guardo correctamente" : "No fue posible guardar la configuracion";

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

        #region Administracion Modulos y submodulos

        public async Task<ActionResult> AdminModulos()
        {
            var modulos = new List<UiModulo>
            {
                new UiModulo { Id = 0, Nombre = "Selecciona un tipo de control"}
            };

            var peticion = new UiPeticion<UiModulo> { Solicitud = new UiModulo { Activo = true }, Paginado = new Entities.Paging { All = true } };
            modulos.AddRange(await new ServicesCatalog().ObtenerModulosPorCriterio(peticion));

            var listaModulos = modulos.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre,
                Selected = c.Id == 0
            }).ToArray();

            ViewBag.ListaModulos = listaModulos;

            var subModulos = new List<UiModulo>
            {
                new UiModulo { Id = 0, Nombre = "Selecciona un tipo de control"}
            };

            var listaSubModulos = subModulos.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre,
                Selected = c.Id == 0
            }).ToArray();

            ViewBag.ListaSubModulos = listaSubModulos;

            return View();
        }

        public async Task<PartialViewResult> AdminModulo(UiModulo model)
        {
            var tipoControles = new List<UiTipoControl>
                {
                    new UiTipoControl {Identificador = 0, Nombre = "Selecciona un tipo de control"}
                };

            tipoControles.AddRange(new ServicesCatalog().ObtenerTiposControl(1, 1000));

            var listaTipoControl = tipoControles.Select(c => new SelectListItem
            {
                Value = c.Identificador.ToString(),
                Text = c.Nombre,
                Selected = c.Identificador == 0
            }).ToArray();

            ViewBag.ListaTipoControl = listaTipoControl;

            var modulos = new List<UiModulo>
                {
                    new UiModulo {Id = 0, Nombre = "Sin dependencia"}
                };
            var peticion = new UiPeticion<UiModulo>
            {
                Solicitud = new UiModulo { Activo = true },
                Paginado = new Entities.Paging { All = true }
            };
            modulos.AddRange(await new ServicesCatalog().ObtenerModulosPorCriterio(peticion));

            var listaModulos = modulos.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre,
                Selected = c.Id == 0
            }).ToArray();

            ViewBag.ListaModulos = listaModulos;


            var modulo = await new ServicesCatalog().ObtenerModulosPorId(model.Id);

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    modulo.Action = UiEnumEntity.New;
                    ViewBag.Title = "Crear de modulo";
                    break;

                case UiEnumEntity.Edit:
                    modulo.Action = UiEnumEntity.Edit;
                    ViewBag.Title = "Modificar modulo";
                    break;

                case UiEnumEntity.View:
                    modulo.Action = UiEnumEntity.View;
                    ViewBag.Title = "Detalle modulo";
                    break;

                case UiEnumEntity.Add:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            return PartialView(modulo);

        }

        public async Task<JsonResult> AdminModuloConsultar(UiResultPage<UiModulo> model)
        {
            var uiResult = new UiResultPage<UiModulo> { Result = UiEnum.TransactionResult.Failed };

            try
            {
                var service = new ServicesCatalog();

                List<UiModulo> listaModulos;

                if (model.Query == null)
                {
                    var peticion = new UiPeticion<UiModulo> { Solicitud = new UiModulo { Activo = true }, Paginado = new Entities.Paging { All = true } };
                    listaModulos = (List<UiModulo>)await service.ObtenerModulosPorCriterio(peticion);
                }
                else
                {
                    var peticion = new UiPeticion<UiModulo> { Solicitud = model.Query, Paginado = new Entities.Paging { All = true } };
                    listaModulos = (List<UiModulo>)await service.ObtenerModulosPorCriterio(peticion);
                }
                uiResult.List = listaModulos.ToList();
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

        public async Task<PartialViewResult> ObtenerSubModulos(int idPadre)
        {
            var subModulos = new List<UiModulo>
            {
                new UiModulo { Id = 0, Nombre = "Selecciona un sub modulos"}
            };

            subModulos.AddRange(await new ServicesCatalog().ObtenerSubModulosPorIdPadre(idPadre));

            var listaSubModulos = subModulos.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre,
                Selected = c.Id == 0
            }).ToArray();

            ViewBag.Id = "ddlSubModulo";

            return PartialView(listaSubModulos);
        }

        public JsonResult ObtenerControles(UiResultPage<UiTipoControl> model)
        {
            var uiResult = new UiResultPage<UiTipoControl> { Result = UiEnum.TransactionResult.Failed };

            try
            {
                uiResult.List = null;// listaControles;
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

        public JsonResult ObtenerAcciones(UiResultPage<UiTipoControl> model)
        {
            var uiResult = new UiResultPage<UiTipoControl> { Result = UiEnum.TransactionResult.Failed };

            try
            {
                uiResult.List = null;// lista;
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
        public async Task<JsonResult> AdminModuloGuardar(UiModulo model)
        {
            var uiResult = new UiResultPage<UiModulo> { Result = UiEnum.TransactionResult.Failed };

            try
            {
                model.IdPadre = model.IdPadre == 0 ? null : model.IdPadre;

                var inserta = await new ServicesCatalog().GuardarModulo(model);
                var guardar = inserta > 0;
                uiResult.Result = guardar ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = guardar ? "Se guardo correctamente" : "No fue posible guardar el modulo";

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
        public async Task<JsonResult> AdminModuloActualizar(UiModulo model)
        {
            var uiResult = new UiResultPage<UiAreasValidadoras> { Result = UiEnum.TransactionResult.Failed };

            try
            {
                var clientService = new ServiceConfiguration();

                model.IdPadre = model.IdPadre == 0 ? null : model.IdPadre;

                var guardar = await new ServicesCatalog().GuardarModulo(model);

                uiResult.Result = guardar > 0 ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = guardar > 0 ? "El modulo se guardo correctamente" : "No fue posible guardar el modulo";
                uiResult.Paging.Pages = clientService.Pages;
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
        public async Task<JsonResult> AdminModuloCambiarEstatus(UiModulo model)
        {
            var uiResult = new UiResultPage<UiModulo> { Result = UiEnum.TransactionResult.Failed };

            try
            {
                var guardar = await new ServicesCatalog().CambiarEstatusModulo(model);

                uiResult.Result = guardar > 0 ? UiEnum.TransactionResult.Success : UiEnum.TransactionResult.Failed;
                uiResult.Message = guardar > 0 ? "El modulo se guardo correctamente" : "No fue posible guardar el modulo";
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

        #region Administracion de roles

        public ActionResult AdminRoles()
        {
            var roles = new List<UiRol>
            {
                new UiRol(0) {Name = "Selecciona un Rol" }
            };

            var areas = new List<UiArea>
            {
               new UiArea { Identificador = 0, Name = "Seleccion un Area" }
            };

            areas.AddRange(new ServicesCatalog().ObtenerAreas(1, 1000));

            var listaRoles = roles.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = c.Id == 0
            }).ToArray();

            var listaAreas = areas.Select(c => new SelectListItem
            {
                Value = c.Identificador.ToString(),
                Text = c.Name,
                Selected = c.Identificador == 0
            }).ToArray();

            ViewBag.ListaRoles = listaRoles;
            ViewBag.ListaAreas = listaAreas;

            return View();
        }

        public async Task<PartialViewResult> AdminRol(UiRol model)
        {
            var areas = new List<UiArea>
            {
               new UiArea{ Identificador = 0, Name = "Seleccion un Area" }
            };

            areas.AddRange(new ServicesCatalog().ObtenerAreas(1, 1000));

            var listaAreas = areas.Select(c => new SelectListItem
            {
                Value = c.Identificador.ToString(),
                Text = c.Name,
                Selected = c.Identificador == 0
            }).ToArray();

            var modelo = new UiRol(0);

            if (model.Id != 0)
            {
                var rol = new ServicesRol();
                modelo = await rol.FindByIdAsync(model.Id);
            }

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    modelo.Action = UiEnumEntity.New;
                    ViewBag.Title = "Crear rol de usuario";
                    break;
                case UiEnumEntity.Edit:
                    modelo.Action = UiEnumEntity.Edit;
                    ViewBag.Title = "Modificar rol de usuario";
                    break;
                case UiEnumEntity.View:
                    modelo.Action = UiEnumEntity.View;
                    ViewBag.Title = "Detalle del rol de usuarios";
                    break;
                case UiEnumEntity.Add:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ViewBag.ListaAreas = listaAreas;

            return PartialView(modelo);
        }

        public async Task<JsonResult> AdminRolConsultar(UiResultPage<UiRol> model)
        {
            var uiResult = new UiResultPage<UiRol> { Result = UiEnum.TransactionResult.Failed };

            try
            {
                var rol = new ServicesRol();

                IEnumerable<UiRol> lista;

                if (model.Query != null)
                {
                    model.Query.IdArea = (model.Query.IdArea == null || model.Query.IdArea == 0) ? null : model.Query.IdArea;
                    lista = await rol.ObtenerPorTipoUsuario(model.Query, model.Query.Externo);
                }
                else
                {
                    var modelo = new UiRol(0) { Activo = true, Externo = false, IdArea = 0 };
                    lista = await rol.ObtenerPorTipoUsuario(modelo, modelo.Externo);
                }

                var areas = new List<UiArea>
                {
                    new UiArea {Identificador = 0, Name = "Seleccion un Area"}
                };

                if (model.Query.Externo != true)
                {
                    areas.AddRange(new ServicesCatalog().ObtenerAreas(1, 1000));

                    foreach (var item in lista)
                    {
                        item.Area = areas.FirstOrDefault(x => x.Identificador == item.IdArea).Name;
                    }
                }
                else
                {
                    foreach (var item in lista)
                    {
                        item.Area = "Cliente";
                    }
                }

                uiResult.List = (List<UiRol>)lista;
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
        public async Task<JsonResult> AdminRolGuardar(UiRol model)
        {
            var uiResult = new UiResultPage<UiRol> { Result = UiEnum.TransactionResult.Failed };

            try
            {
                var rol = new ServicesRol();
                model.IdArea = (model.IdArea == null || model.IdArea == 0) ? null : model.IdArea;

                await rol.CreateAsync(model);

                uiResult.Result = UiEnum.TransactionResult.Success;
                uiResult.Message = "Se guardo correctamente";

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
        public async Task<JsonResult> RolActualizar(UiRol model)
        {
            var uiResult = new UiResultPage<UiRol> { Result = UiEnum.TransactionResult.Failed };

            try
            {
                var rol = new ServicesRol();
                model.IdArea = (model.IdArea == null || model.IdArea == 0) ? null : model.IdArea;
                await rol.CreateAsync(model);

                uiResult.Result = UiEnum.TransactionResult.Success;
                uiResult.Message = "Se guardo correctamente";
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
        public async Task<JsonResult> RolCambiarEstatus(UiRol model)
        {
            var uiResult = new UiResultPage<UiRol> { Result = UiEnum.TransactionResult.Failed };

            try
            {
                var rol = new ServicesRol();

                model.IdArea = (model.IdArea == null || model.IdArea == 0) ? null : model.IdArea;

                await rol.CambiarEstatusRol(model);

                uiResult.Result = UiEnum.TransactionResult.Success;

                uiResult.List = null;
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
}