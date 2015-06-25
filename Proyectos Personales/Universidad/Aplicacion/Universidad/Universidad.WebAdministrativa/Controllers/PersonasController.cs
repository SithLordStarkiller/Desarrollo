using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;
using Universidad.Controlador.GestionCatalogos;
using Universidad.Controlador.Personas;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;
using Universidad.WebAdministrativa.Models;

namespace Universidad.WebAdministrativa.Controllers
{
    public class PersonasController : AsyncController
    {
        [SessionExpireFilter]
        public void Sesion()
        {
            var persona = ((PER_PERSONAS)Session["Persona"]);
            ViewBag.tipoUsuario = ((US_CAT_TIPO_USUARIO)Session["TipoPersona"]).TIPO_USUARIO;
            ViewBag.nombre = persona.NOMBRE + " " + persona.A_PATERNO + " " + persona.A_MATERNO;
        }

        public void CargaListas()
        {
            ViewBag.ListaPaises = Session["ListaPaises"];
            ViewBag.ListaTipoPersona = Session["ListaTipoPersona"];
            ViewBag.ListaSexo = Session["ListaSexo"];
        }

        [SessionExpireFilter]
        public void PersonaDefaultAsync()
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioCatalogos = new SVC_GestionCatalogos(sesion);

            servicioCatalogos.ObtenCatNacionalidadFinalizado += delegate(List<PER_CAT_NACIONALIDAD> lista)
            {
                AsyncManager.Parameters["listaPaises"] = lista;
                AsyncManager.OutstandingOperations.Decrement();
            };

            AsyncManager.OutstandingOperations.Increment();
            servicioCatalogos.ObtenCatNacionalidad();

            servicioCatalogos.ObtenCatTipoPersonaFinalizado += delegate(List<PER_CAT_TIPO_PERSONA> lista)
            {
                AsyncManager.Parameters["listaTiposPersona"] = lista;
                AsyncManager.OutstandingOperations.Decrement();
            };
            AsyncManager.OutstandingOperations.Increment();
            servicioCatalogos.ObtenCatTipoPersona();
        }

        [SessionExpireFilter]
        public ActionResult PersonaDefaultCompleted(List<PER_CAT_NACIONALIDAD> listaPaises,
            List<PER_CAT_TIPO_PERSONA> listaTiposPersona)
        {
            var paises = listaPaises
                .Select(c => new SelectListItem
                {
                    Value = c.CVE_NACIONALIDAD.ToString(CultureInfo.InvariantCulture),
                    Text = c.NOMBRE_PAIS
                }).ToArray();

            var tiposPersona = listaTiposPersona
                .Select(c => new SelectListItem
                {
                    Value = c.ID_TIPO_PERSONA.ToString(CultureInfo.InvariantCulture),
                    Text = c.TIPO_PERSONA
                }).ToArray();

            var sexo = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "M"},
                new SelectListItem {Value = "2", Text = "F"}
            };

            Session["ListaPaises"] = paises;
            Session["ListaTipoPersona"] = tiposPersona;
            Session["ListaSexo"] = sexo.ToArray();

            var modelo = Session["Modelo"];
            CargaListas();

            return modelo == null ? View() : View(modelo);
        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult NuevaPersona(ModelWizardPersonas modeloPersona)
        {
            if (ModelState.IsValid)
            {
                Sesion();
                CargaListas();
                Session["Modelo"] = modeloPersona;
                return new RedirectToReturnUrlResult(() => RedirectToAction("WizardPersonaDireccion", "Personas"));
            }

            Sesion();
            CargaListas();

            return View("PersonaDefault", modeloPersona);
        }

        [HttpGet]
        [SessionExpireFilter]
        public void WizardPersonaDireccionAsync(ModelWizardPersonas modelo)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioCatalogos = new SVC_GestionCatalogos(sesion);

            servicioCatalogos.ObtenCatEstadosFinalizado += delegate(List<DIR_CAT_ESTADO> lista)
            {
                AsyncManager.Parameters["listaEstados"] = lista;
                AsyncManager.OutstandingOperations.Decrement();
            };

            AsyncManager.OutstandingOperations.Increment();
            servicioCatalogos.ObtenCatEstados();
        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult WizardPersonaDireccionCompleted(List<DIR_CAT_ESTADO> listaEstados)
        {
            var estados = listaEstados
                .Select(c => new SelectListItem
                {
                    Value = c.IDESTADO.ToString(CultureInfo.InvariantCulture),
                    Text = c.NOMBREESTADO
                }).ToArray();

            ViewBag.ListaEstados = estados;
            Session["listaEstados"] = estados;

            var modelo = Session["Modelo"];

            return View(modelo);
        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult DireccionPersona(ModelWizardPersonas modeloPersona)
        {
            if (ModelState.IsValid)
            {
                Sesion();
                CargaListas();
                Session["Modelo"] = modeloPersona;
                return new RedirectToReturnUrlResult(() => RedirectToAction("WizardPersonaDireccion", "Personas"));
            }

            Sesion();
            CargaListas();

            return View("PersonaDefault", modeloPersona);
        }

        [HttpGet]
        [SessionExpireFilter]
        public void WizardPersonaMediosElectronicosAsync(ModelWizardPersonas modelo)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioCatalogos = new SVC_GestionCatalogos(sesion);

            servicioCatalogos.ObtenCatEstadosFinalizado += delegate(List<DIR_CAT_ESTADO> lista)
            {
                AsyncManager.Parameters["listaEstados"] = lista;
                AsyncManager.OutstandingOperations.Decrement();
            };

            AsyncManager.OutstandingOperations.Increment();
            servicioCatalogos.ObtenCatEstados();
        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult WizardPersonaMediosElectronicosCompleted(List<DIR_CAT_ESTADO> listaEstados)
        {
            var estados = listaEstados
                .Select(c => new SelectListItem
                {
                    Value = c.IDESTADO.ToString(CultureInfo.InvariantCulture),
                    Text = c.NOMBREESTADO
                }).ToArray();
            Session["listaEstados"] = estados;
            ViewBag.ListaEstados = estados;

            var modelo = Session["Modelo"];

            return View(modelo);
        }

        [SessionExpireFilter]
        public void ObtenMunicipiosAsync(int estado)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioCatalogos = new SVC_GestionCatalogos(sesion);

            servicioCatalogos.ObtenMunicipiosFinalizado += delegate(List<DIR_CAT_DELG_MUNICIPIO> lista)
            {
                AsyncManager.Parameters["listaMunicipios"] = lista;
                AsyncManager.OutstandingOperations.Decrement();
            };

            AsyncManager.OutstandingOperations.Increment();
            servicioCatalogos.ObtenMunicipios(estado);
        }

        [SessionExpireFilter]
        public ActionResult ObtenMunicipiosCompleted(List<DIR_CAT_DELG_MUNICIPIO> listaMunicipios)
        {
            var lista = listaMunicipios.Select(c => new SelectListItem
            {
                Value = c.IDMUNICIPIO.ToString(),
                Text = c.NOMBREDELGMUNICIPIO
            }).ToArray();

            var resultado = JsonConvert.SerializeObject(lista);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public void ObtenColoniasAsync(int estado, int municipio)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioCatalogos = new SVC_GestionCatalogos(sesion);

            servicioCatalogos.ObtenColoniasFinalizado += delegate(List<DIR_CAT_COLONIAS> lista)
            {
                AsyncManager.Parameters["listaColonias"] = lista;
                AsyncManager.OutstandingOperations.Decrement();
            };

            AsyncManager.OutstandingOperations.Increment();
            servicioCatalogos.ObtenColonias(estado, municipio);
        }

        [SessionExpireFilter]
        public ActionResult ObtenColoniasCompleted(List<DIR_CAT_COLONIAS> listaColonias)
        {
            var lista = listaColonias.Select(c => new SelectListItem
            {
                Value = c.IDCOLONIA.ToString(CultureInfo.InvariantCulture),
                Text = c.NOMBRECOLONIA
            }).ToArray();

            var resultado = JsonConvert.SerializeObject(lista);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public void ObtenCodigoPostalAsync(int estado, int municipio, int colonia)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioCatalogos = new SVC_GestionCatalogos(sesion);

            servicioCatalogos.ObtenCodigoPostalFinalizado += delegate(DIR_CAT_COLONIAS rColonia)
            {
                AsyncManager.Parameters["colonia"] = rColonia;
                AsyncManager.OutstandingOperations.Decrement();
            };

            AsyncManager.OutstandingOperations.Increment();
            servicioCatalogos.ObtenCodigoPostal(estado, municipio, colonia);
        }

        [SessionExpireFilter]
        public ActionResult ObtenCodigoPostalCompleted(DIR_CAT_COLONIAS colonia)
        {
            var resultado = JsonConvert.SerializeObject(colonia);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public void ObtenColoniasPorCpAsync(int codigoPostal)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioCatalogos = new SVC_GestionCatalogos(sesion);

            servicioCatalogos.ObtenColoniasPorCpFinalizado += delegate(List<DIR_CAT_COLONIAS> lista)
            {
                AsyncManager.Parameters["lista"] = lista;
                AsyncManager.OutstandingOperations.Decrement();
            };

            AsyncManager.OutstandingOperations.Increment();
            servicioCatalogos.ObtenColoniasPorCpPersona(codigoPostal);
        }

        [SessionExpireFilter]
        public ActionResult ObtenColoniasPorCpCompleted(List<DIR_CAT_COLONIAS> lista)
        {
            var resultado = JsonConvert.SerializeObject(lista);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult GuardarDatosDireccion(ModelWizardPersonas modeloDireccion)
        {

            if (ModelState.IsValid)
            {
                Sesion();
                CargaListas();
                var modelo = (ModelWizardPersonas)Session["Modelo"];
                modelo.Direccion = modeloDireccion.Direccion;
                Session["Modelo"] = modelo;
                return
                    new RedirectToReturnUrlResult(() => RedirectToAction("WizardPersonaMediosElectronicos", "Personas"));
            }

            Sesion();
            CargaListas();
            ViewBag.listaEstados = Session["listaEstados"];
            return View("WizardPersonaDireccion", modeloDireccion);
        }

        [SessionExpireFilter]
        public ActionResult GuardarDatosTel(ModelWizardPersonas modeloPersona)
        {
            if (ModelState.IsValid)
            {
                Sesion();
                CargaListas();
                var modelo = (ModelWizardPersonas)Session["Modelo"];
                modelo.TelMed = modeloPersona.TelMed;
                Session["Modelo"] = modelo;
                return View("WizardPersonaFotografia", modelo);
            }

            Sesion();
            CargaListas();
            return View("WizardPersonaMediosElectronicos", modeloPersona);
        }

        [SessionExpireFilter]
        [HttpGet]
        public ActionResult WizardPersonaFotografia(ModelWizardPersonas model)
        {
            Sesion();
            return View();
        }

        [SessionExpireFilter]
        [HttpPost]
        public async void GuardaPersonaAsync(ModelWizardPersonas model)
        {
            var sesion = (Sesion)Session["Sesion"];
            var persona = (ModelWizardPersonas)Session["Modelo"];

            Sesion();

            //persona.Fotografia = model.Fotografia;

            var direccion = new DIR_DIRECCIONES
            {
                IDCOLONIA = Convert.ToInt32(persona.Direccion.IdColonia),
                IDMUNICIPIO = Convert.ToInt32(persona.Direccion.IdMunicipio),
                IDESTADO = Convert.ToInt32(persona.Direccion.IdEstado),
                CALLE = persona.Direccion.Calle,
                NOEXT = persona.Direccion.NoExterior,
                NOINT = persona.Direccion.NoInterior,
                REFERENCIAS = persona.Direccion.ReferenciasAdicionalies
            };

            var telefonos = new PER_CAT_TELEFONOS
            {
                TELEFONO_CELULAR_PERSONAL = persona.TelMed.TelefonoMovilPersonal,
                TELEFONO_CELULAR_TRABAJO = persona.TelMed.TelefonoMovilTrabajo,
                TELEFONO_FIJO_DOMICILIO = persona.TelMed.TelefonoFijoCasa,
                TELEFONO_FIJO_TRABAJO = persona.TelMed.TelefonoFijoTrabajo,
                FAX = persona.TelMed.Fax
            };

            var medios = new PER_MEDIOS_ELECTRONICOS
            {
                CORREO_ELECTRONICO_PERSONAL = persona.TelMed.CorreoElectronicoPersonal,
                CORREO_ELECTRONICO_UNIVERSIDAD = persona.TelMed.CorreoElectronicoTrabajo,
            };

            var fotografia = new PER_FOTOGRAFIA
            {
                NOMBRE = "sin foto"
            };

            var datos = new PER_PERSONAS
            {
                CVE_NACIONALIDAD = Convert.ToInt32(persona.Datos.IdNacionalidad),
                ID_PERSONA = Convert.ToInt32(persona.Datos.IdTipoPersona),
                NOMBRE = persona.Datos.Nombre,
                A_PATERNO = persona.Datos.ApellidoP,
                A_MATERNO = persona.Datos.ApellidoM,
                CURP = persona.Datos.Curp,
                IMSS = persona.Datos.Nss,
                RFC = persona.Datos.Rfc,
                FECHA_NAC = persona.Datos.FechaNacimiento,
                SEXO = persona.Datos.IdSexo == "1" ? "Masculino" : "Femenino"
            };

            var servicio = new SvcPersonas(sesion);

            AsyncManager.OutstandingOperations.Increment();
            await Task.Factory.StartNew(() =>
            {
                var resultado = servicio.InsertarPersona(telefonos, medios, fotografia, datos, direccion);
                AsyncManager.Parameters["objeto"] = resultado;
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        [SessionExpireFilter]
        public ActionResult GuardaPersonaCompleted(PER_PERSONAS objeto)
        {
            return View();
        }
    }

    public class RedirectToReturnUrlResult : ActionResult
    {
        private readonly Func<ActionResult> _funcIfNoReturnUrl;

        public RedirectToReturnUrlResult(Func<ActionResult> funcIfNoReturnUrl)
        {
            if (funcIfNoReturnUrl == null) throw new ArgumentNullException("funcIfNoReturnUrl");
            _funcIfNoReturnUrl = funcIfNoReturnUrl;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            string returnUrl;

            if (TryGetReturnUrl(context, out returnUrl))
            {
                new RedirectResult(returnUrl).ExecuteResult(context);
            }
            else
            {
                _funcIfNoReturnUrl().ExecuteResult(context);
            }
        }

        private bool TryGetReturnUrl(ControllerContext context, out string returnUrl)
        {
            try
            {
                var queryString = context.HttpContext.Request.QueryString;
                returnUrl = queryString["ReturnUrl"];
                return !string.IsNullOrEmpty(returnUrl);
            }

            catch (Exception)
            {
                returnUrl = null;
                return false;
            }
        }
    }
}