namespace Universidad.WebAdministrativa.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using Microsoft.Reporting.WebForms;
    using Newtonsoft.Json;
    using Controlador.GestionCatalogos;
    using Controlador.Personas;
    using Entidades.ControlUsuario;
    using Models;
    using PagedList;

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
        public ActionResult PersonaDefault()
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioCatalogos = new SvcGestionCatalogos(sesion);

            var listaPaises = servicioCatalogos.ObtenCatNacionalidad();

            var listaTiposPersona = servicioCatalogos.ObtenCatTipoPersona();

            var listTask = new List<Task>
            {
                listaPaises,
                listaTiposPersona
            };

            Task.WaitAll(listTask.ToArray());

            var paises = listaPaises.Result
                .Select(c => new SelectListItem
                {
                    Value = c.CVE_NACIONALIDAD.ToString(CultureInfo.InvariantCulture),
                    Text = c.NOMBRE_PAIS
                }).ToArray();

            var tiposPersona = listaTiposPersona.Result
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
        public async Task<ActionResult> WizardPersonaDireccion(ModelWizardPersonas modelo)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioCatalogos = new SvcGestionCatalogos(sesion);

            var listaEstados = await servicioCatalogos.ObtenCatEstados();

            var estados = listaEstados
                .Select(c => new SelectListItem
                {
                    Value = c.IDESTADO.ToString(CultureInfo.InvariantCulture),
                    Text = c.NOMBREESTADO
                }).ToArray();

            ViewBag.ListaEstados = estados;
            Session["listaEstados"] = estados;

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
        public async Task<ActionResult> WizardPersonaMediosElectronicos(ModelWizardPersonas modelo)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioCatalogos = new SvcGestionCatalogos(sesion);

            var listaEstados = await servicioCatalogos.ObtenCatEstados();

            var estados = listaEstados
                .Select(c => new SelectListItem
                {
                    Value = c.IDESTADO.ToString(CultureInfo.InvariantCulture),
                    Text = c.NOMBREESTADO
                }).ToArray();
            Session["listaEstados"] = estados;
            ViewBag.ListaEstados = estados;

            return View(modelo);
        }

        [SessionExpireFilter]
        public async Task<ActionResult> ObtenMunicipios(int estado)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioCatalogos = new SvcGestionCatalogos(sesion);

            var listaMunicipios = await servicioCatalogos.ObtenMunicipios(estado);

            var lista = listaMunicipios.Select(c => new SelectListItem
            {
                Value = c.IDMUNICIPIO.ToString(),
                Text = c.NOMBREDELGMUNICIPIO
            }).ToArray();

            var resultado = JsonConvert.SerializeObject(lista);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public async Task<ActionResult> ObtenColonias(int estado, int municipio)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioCatalogos = new SvcGestionCatalogos(sesion);

            var listaColonias = await servicioCatalogos.ObtenColonias(estado, municipio);

            var lista = listaColonias.Select(c => new SelectListItem
            {
                Value = c.IDCOLONIA.ToString(CultureInfo.InvariantCulture),
                Text = c.NOMBRECOLONIA
            }).ToArray();

            var resultado = JsonConvert.SerializeObject(lista);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public async Task<ActionResult> ObtenCodigoPostal(int estado, int municipio, int colonia)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioCatalogos = new SvcGestionCatalogos(sesion);

            var direccion = await servicioCatalogos.ObtenCodigoPostal(estado, municipio, colonia);
            var resultado = JsonConvert.SerializeObject(direccion);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public async Task<ActionResult> ObtenColoniasPorCp(int codigoPostal)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioCatalogos = new SvcGestionCatalogos(sesion);

            var lista = await servicioCatalogos.ObtenColoniasPorCpPersona(codigoPostal);

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
                ID_TIPO_PERSONA = Convert.ToInt32(persona.Datos.IdTipoPersona),
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

        [SessionExpireFilter]
        public async void ListadoPersonasAsync()
        {
            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcPersonas(sesion);

            AsyncManager.OutstandingOperations.Increment();
            await Task.Factory.StartNew(() =>
            {
                var resultado = servicio.ObtenListaPersonas();
                AsyncManager.Parameters["listaPersonas"] = resultado.Result;
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        [SessionExpireFilter]
        public ActionResult ListadoPersonasCompleted(List<PER_PERSONAS> listaPersonas)
        {
            Sesion();
            return View(listaPersonas);
        }

        [SessionExpireFilter]
        public async void OrdenaListadoPersonasAsync(string ordenamiento)
        {
            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcPersonas(sesion);

            AsyncManager.OutstandingOperations.Increment();
            await Task.Factory.StartNew(() =>
            {
                var lista = servicio.ObtenListaPersonas().Result;
                AsyncManager.Parameters["listaPersonas"] = lista;
                AsyncManager.Parameters["ordenamiento"] = ordenamiento;
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        [SessionExpireFilter]
        public ActionResult OrdenaListadoPersonasCompleted(List<PER_PERSONAS> listaPersonas, string ordenamiento)
        {
            Sesion();
            switch (ordenamiento)
            {
                case "Paterno_A":
                    listaPersonas = listaPersonas.OrderBy(r => r.A_PATERNO).ToList();
                    break;
                case "Paterno_D":
                    listaPersonas = listaPersonas.OrderByDescending(r => r.A_PATERNO).ToList();
                    break;
                case "Alta_A":
                    listaPersonas = listaPersonas.OrderBy(r => r.FECHAINGRESO).ToList();
                    break;
                case "Alta_D":
                    listaPersonas = listaPersonas.OrderByDescending(r => r.FECHAINGRESO).ToList();
                    break;
            }
            return View("ListadoPersonas", null, listaPersonas);
        }

        [SessionExpireFilter]
        public void ReportePersona(int? personaId)
        {
            var parametros = new ReportParameterCollection
            {
                new ReportParameter("PersonaId", personaId.ToString())
            };

            var param = JsonConvert.SerializeObject(parametros);

            Response.Redirect("../VisorReportes.aspx?TipoPeticion=MuestraReportePorNombreConParametros&nombreReporte=DescripcionPersona&parametros=" + param);
        }

        [SessionExpireFilter]
        public async Task<ActionResult> EnlistarPersonasAsync(int? page, string fechaInicio, string fechaFin, int? idTipoPersona, string idPersona)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioPersonas = new SvcPersonas(sesion);

            List<PER_PERSONAS> listaPersonas;

            if (fechaInicio == null && fechaFin == null && idPersona == null && idTipoPersona == null)
            {
                listaPersonas = await servicioPersonas.ObtenListaPersonas();
            }
            else
            {
                DateTime? fechaInicioS;

                if (fechaInicio == "")
                {
                    fechaInicioS = null;
                }
                else
                {
                    fechaInicioS = Convert.ToDateTime(fechaInicio);
                }

                DateTime? fechaFinS;

                if (fechaFin == "")
                {
                    fechaFinS = null;
                }
                else
                {
                    fechaFinS = Convert.ToDateTime(fechaFin);
                }

                listaPersonas = await servicioPersonas.ObtenListaPersonasFiltro(idPersona, fechaInicioS, fechaFinS, idTipoPersona);
            }

            var listaTipoPersona = await servicioPersonas.ObtenCatTipoPersona();

            var enlistarTipoPersona = listaTipoPersona.Select(c => new SelectListItem
            {
                Value = c.ID_TIPO_PERSONA.ToString(CultureInfo.InvariantCulture),
                Text = c.TIPO_PERSONA
            }).ToArray();

            ViewBag.ListaTipoPersona = enlistarTipoPersona;

            ViewData["fechaInicial"] = fechaInicio;
            ViewData["fechaFinal"] = fechaFin;
            ViewData["idTipoPersona"] = idTipoPersona;
            ViewData["idPersona"] = idPersona;

            const int pageSize = 6;
            var pageNumber = (page ?? 1);
            var listaAux = listaPersonas.ToPagedList(pageNumber, pageSize);

            return View(listaAux);
        }

        [SessionExpireFilter]
        public async Task<ActionResult> EnlistarPersonasFiltro(int? page)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioPersonas = new SvcPersonas(sesion);

            var listaPersonas = await servicioPersonas.ObtenListaPersonas();
            var listaTipoPersona = await servicioPersonas.ObtenCatTipoPersona();

            var enlistarTipoPersona = listaTipoPersona.Select(c => new SelectListItem
            {
                Value = c.ID_TIPO_PERSONA.ToString(CultureInfo.InvariantCulture),
                Text = c.TIPO_PERSONA
            }).ToArray();

            ViewBag.ListaTipoPersona = enlistarTipoPersona;

            const int pageSize = 7;
            var pageNumber = (page ?? 1);

            return View("EnlistarPersonas", listaPersonas.ToPagedList(pageNumber, pageSize));
        }

        [SessionExpireFilter]
        public async Task<ActionResult> ObtenDatosPersona(string idPersonaLink)
        {
            var session = (Sesion)Session["Sesion"];
            var servicioPersonas = new SvcPersonas(session);

            var per = await servicioPersonas.BuscarPersonaCompleta(idPersonaLink);

            var resultado = JsonConvert.SerializeObject(per);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public async Task<ActionResult> EnlistarPersonasEditarAsync(int? page, string fechaInicio, string fechaFin, int? idTipoPersona, string idPersona)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioPersonas = new SvcPersonas(sesion);

            List<PER_PERSONAS> listaPersonas;

            if (fechaInicio == null && fechaFin == null && idPersona == null && idTipoPersona == null)
            {
                listaPersonas = await servicioPersonas.ObtenListaPersonas();
            }
            else
            {
                DateTime? fechaInicioS;

                if (fechaInicio == "")
                {
                    fechaInicioS = null;
                }
                else
                {
                    fechaInicioS = Convert.ToDateTime(fechaInicio);
                }

                DateTime? fechaFinS;

                if (fechaFin == "")
                {
                    fechaFinS = null;
                }
                else
                {
                    fechaFinS = Convert.ToDateTime(fechaFin);
                }

                listaPersonas = await servicioPersonas.ObtenListaPersonasFiltro(idPersona, fechaInicioS, fechaFinS, idTipoPersona);
            }

            var listaTipoPersona = await servicioPersonas.ObtenCatTipoPersona();

            var enlistarTipoPersona = listaTipoPersona.Select(c => new SelectListItem
            {
                Value = c.ID_TIPO_PERSONA.ToString(CultureInfo.InvariantCulture),
                Text = c.TIPO_PERSONA
            }).ToArray();

            ViewBag.ListaTipoPersona = enlistarTipoPersona;

            ViewData["fechaInicial"] = fechaInicio;
            ViewData["fechaFinal"] = fechaFin;
            ViewData["idTipoPersona"] = idTipoPersona;
            ViewData["idPersona"] = idPersona;

            const int pageSize = 6;
            var pageNumber = (page ?? 1);

            return View(listaPersonas.ToPagedList(pageNumber, pageSize));
        }

        [SessionExpireFilter]
        public async Task<ActionResult> EnlistarPersonasEditarFiltro(int? page)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicioPersonas = new SvcPersonas(sesion);

            var listaPersonas = await servicioPersonas.ObtenListaPersonas();
            var listaTipoPersona = await servicioPersonas.ObtenCatTipoPersona();

            var enlistarTipoPersona = listaTipoPersona.Select(c => new SelectListItem
            {
                Value = c.ID_TIPO_PERSONA.ToString(CultureInfo.InvariantCulture),
                Text = c.TIPO_PERSONA
            }).ToArray();

            ViewBag.ListaTipoPersona = enlistarTipoPersona;

            const int pageSize = 7;
            var pageNumber = (page ?? 1);

            return View("EnlistarPersonasEditar", listaPersonas.ToPagedList(pageNumber, pageSize));
        }

        [SessionExpireFilter]
        public async Task<ActionResult> EditarPersona(string idPersona)
        {
            Sesion();
            var sesion = (Sesion)Session["Sesion"];
            var servicioPersona = new SvcPersonas(sesion);
            var servicioCatalogos = new SvcGestionCatalogos(sesion);

            var personaDatos = await servicioPersona.BuscarPersona(idPersona);
            var direccion = servicioPersona.ObtenDireccion(personaDatos);
            var telefonos = servicioPersona.ObtenTelefonos(personaDatos);
            var mediosElectronicos = servicioPersona.ObtenMediosElectronicos(personaDatos);
            var fotografia = servicioPersona.ObtenFotografia(personaDatos);
            var tipoPersona = servicioPersona.ObtenTipoPersona(Convert.ToInt32(personaDatos.ID_TIPO_PERSONA));
            var pais = servicioPersona.ObtenPersonaPais(Convert.ToInt32(personaDatos.CVE_NACIONALIDAD));
            var listaTipoPersona = servicioCatalogos.ObtenCatTipoPersona();
            var listaNacionalidad = servicioCatalogos.ObtenCatNacionalidad();

            var listTask = new List<Task>
            {
                direccion,
                telefonos,
                mediosElectronicos,
                fotografia,
                tipoPersona,
                pais,
                listaNacionalidad,
                listaTipoPersona
            };

            Task.WaitAll(listTask.ToArray());

            string cadenaBinario;

            using (var stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, fotografia.Result.FOTOGRAFIA);
                var imageBase64 = Convert.ToBase64String(fotografia.Result.FOTOGRAFIA);
                cadenaBinario = string.Format("data:image/{0};base64,{1}", fotografia.Result.EXTENCION, imageBase64);
            }

            var catalogoTipoPersona = listaTipoPersona.Result.Select(c => new SelectListItem
            {
                Value = c.ID_TIPO_PERSONA.ToString(CultureInfo.InvariantCulture),
                Text = c.TIPO_PERSONA
            }).ToArray();

            var catalogoNacionalidad = listaNacionalidad.Result.Select(c => new SelectListItem
            {
                Value = c.CVE_NACIONALIDAD.ToString(CultureInfo.InvariantCulture),
                Text = c.NOMBRE_PAIS
            }).ToArray();

            var catalogoSexo = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "M"},
                new SelectListItem {Value = "2", Text = "F"}
            }.ToArray();
            
            var listaEstados = await servicioCatalogos.ObtenCatEstados();

            var catalogoEstados = listaEstados
                .Select(c => new SelectListItem
                {
                    Value = c.IDESTADO.ToString(CultureInfo.InvariantCulture),
                    Text = c.NOMBREESTADO
                }).ToArray();

            catalogoEstados.FirstOrDefault(r => r.Value == direccion.Result.IDESTADO.ToString()).Selected = true;

            ViewBag.CatalogoEstados = catalogoEstados;

            var listaMunicipios = await servicioCatalogos.ObtenMunicipios((int) direccion.Result.IDESTADO);

            var catalogoMunicipios = listaMunicipios
                .Select(c => new SelectListItem
                {
                    Value = c.IDMUNICIPIO.ToString(),
                    Text = c.NOMBREDELGMUNICIPIO
                }).ToArray();

            catalogoMunicipios.FirstOrDefault(r => r.Value == direccion.Result.IDMUNICIPIO.ToString()).Selected = true;

            ViewBag.CatalogoMunicipios = catalogoMunicipios;

            var listaColonias = await servicioCatalogos.ObtenColonias((int)direccion.Result.IDESTADO,(int)direccion.Result.IDMUNICIPIO);

            var catalogoColonias = listaColonias
                .Select(c => new SelectListItem
                {
                    Value = c.IDCOLONIA.ToString(),
                    Text = c.NOMBRECOLONIA
                }).ToArray();

            catalogoColonias.FirstOrDefault(r => r.Value == direccion.Result.IDCOLONIA.ToString()).Selected = true;

            var colonia = listaColonias.FirstOrDefault(r => r.IDCOLONIA == direccion.Result.IDCOLONIA);

            ViewBag.Colonia = colonia;

            ViewBag.CatalogoColonias = catalogoColonias;

            ((catalogoSexo.FirstOrDefault(r => r.Text == (personaDatos.SEXO == "Masculino" ? "M" : "F")))).Selected = true;
            ((catalogoNacionalidad.FirstOrDefault(r => r.Value == personaDatos.CVE_NACIONALIDAD.ToString()))).Selected = true;
            ((catalogoTipoPersona.FirstOrDefault(r => r.Value == personaDatos.ID_TIPO_PERSONA.ToString()))).Selected = true;

            ViewBag.Datos = personaDatos;
            ViewBag.Direccion = direccion.Result;
            ViewBag.Telefonos = telefonos.Result;
            ViewBag.MedElec = mediosElectronicos.Result;
            ViewBag.Fotografia = fotografia.Result;
            ViewBag.Nacionalidad = pais.Result;
            ViewBag.TipoPersona = tipoPersona.Result;
            ViewBag.CatalogoNacionalidad = catalogoNacionalidad;
            ViewBag.CatalogoTipoPersona = catalogoTipoPersona;
            ViewBag.CatalogoSexo = catalogoSexo;
            ViewBag.StringBinario = cadenaBinario;

            return View();
        }
    }
}