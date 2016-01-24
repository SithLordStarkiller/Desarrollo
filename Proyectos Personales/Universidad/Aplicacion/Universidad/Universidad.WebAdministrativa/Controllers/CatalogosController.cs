namespace Universidad.WebAdministrativa.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Controlador.GestionCatalogos;
    using Entidades.ControlUsuario;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Entidades;

    public class CatalogosController : AsyncController
    {
        [SessionExpireFilter]
        public void Sesion()
        {
            var persona = ((PER_PERSONAS)Session["Persona"]);
            ViewBag.tipoUsuario = ((US_CAT_TIPO_USUARIO)Session["TipoPersona"]).TIPO_USUARIO;
            ViewBag.nombre = persona.NOMBRE + " " + persona.A_PATERNO + " " + persona.A_MATERNO;
        }

        [SessionExpireFilter]
        public async Task<ActionResult> CatalogosAsync()
        {
            var sesion = (Sesion)Session["sesion"];
            var servicioCatalogos = new SvcGestionCatalogos(sesion);

            var lista = await servicioCatalogos.ObtenTablasCatalogos();

            Sesion();

            ViewBag.ListaTablasCatalogos = lista.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(CultureInfo.InvariantCulture),
                Text = c.Nombre
            }).ToArray();

            return View();
        }

        [SessionExpireFilter]
        public async Task<ActionResult> ObtenCatalogoAsync(string tabla)
        {
            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);
            string lista, tipo;

            switch (tabla)
            {
                case "DIR_CAT_COLONIAS":
                    var colonias = await servicio.ObtenCatalogosColonias();
                    tipo = (colonias.GetType().GetGenericArguments()[0]).Name;
                    lista = JsonConvert.SerializeObject(colonias);
                    Sesion();
                    ViewBag.Lista = lista;
                    ViewBag.Tipo = tipo;
                    break;

                case "DIR_CAT_DELG_MUNICIPIO":
                    var municipios = await servicio.ObtenCatalogosMunicipios();
                    tipo = (municipios.GetType().GetGenericArguments()[0]).Name;
                    lista = JsonConvert.SerializeObject(municipios);
                    ViewBag.Lista = lista;
                    ViewBag.Tipo = tipo;
                    break;
            }

            return View();
        }

        [SessionExpireFilter]
        public ActionResult EditarCatalogos()
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var lista = servicio.ObtenCatalogosSistema().Result;

            ViewBag.ListaCatalogos = lista;

            return View();
        }

        [SessionExpireFilter]
        public ActionResult EditaCatalogo(string nombreTabla)
        {
            Sesion();
            switch (nombreTabla)
            {
                case "AUL_CAT_TIPO_AULA":
                    return new RedirectToReturnUrlResult(() => RedirectToAction("EditarAUL_CAT_TIPO_AULA", "Catalogos"));
                case "HOR_CAT_TURNO":
                    return new RedirectToReturnUrlResult(() => RedirectToAction("EditaCatalogoHorCatTurno", "Catalogos"));
                case "HOR_CAT_DIAS_SEMANA":
                    return new RedirectToReturnUrlResult(() => RedirectToAction("EditaCatalogoHorCatDiasSemana", "Catalogos"));
                case "CAR_CAT_ESPECIALIDAD":
                    return new RedirectToReturnUrlResult(() => RedirectToAction("EditaCatalogoCarCatEspecialidad", "Catalogos"));
                case "HOR_CAT_HORAS":
                    return new RedirectToReturnUrlResult(() => RedirectToAction("EditarCatalogoHorCatHoras", "Catalogos"));
                case "CAR_CAT_CARRERAS":
                    return new RedirectToReturnUrlResult(() => RedirectToAction("EditarCatalogoCarCatCarreras", "Catalogos"));
            }
            return null;
        }

        [SessionExpireFilter]
        public async Task<ActionResult> EditarAUL_CAT_TIPO_AULA()
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicios = new SvcGestionCatalogos(sesion);

            var lista = await servicios.ObntenListaAUL_CAT_TIPO_AULA();

            ViewBag.ListaCatalogo = lista;

            return View("Tablas/AUL_CAT_TIPO_AULA");
        }

        [SessionExpireFilter]
        public async Task<bool> ActualizaRegistroAUL_CAT_TIPO_AULA(string idTipoAula, string tipoAula, string descripcion)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var objeto = new AUL_CAT_TIPO_AULA
            {
                IDTIPOAULA = Convert.ToInt16(idTipoAula),
                TIPOAULA = tipoAula,
                DESCRIPCION = descripcion
            };

            var actualizado = await servicio.ActualizaRegistroAUL_CAT_TIPO_AULA(objeto);

            return actualizado;
        }

        [SessionExpireFilter]
        public async Task<ActionResult> NuevoRegistroAUL_CAT_TIPO_AULA(string idTipoAula, string tipoAula, string descripcion)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var objeto = new AUL_CAT_TIPO_AULA
            {
                IDTIPOAULA = Convert.ToInt16(idTipoAula),
                TIPOAULA = tipoAula,
                DESCRIPCION = descripcion
            };

            var nuevo = await servicio.InsertaRegistroAUL_CAT_TIPO_AULA(objeto);

            var resultado = JsonConvert.SerializeObject(nuevo);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public async Task<bool> EliminaRegistroAUL_CAT_TIPO_AULA(int idTipoAula)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var nuevo = await servicio.EliminaResgistroAUL_CAT_TIPO_AULA(idTipoAula);

            return nuevo;
        }

        [SessionExpireFilter]
        public async Task<ActionResult> EditaCatalogoHorCatTurno()
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicios = new SvcGestionCatalogos(sesion);

            var lista = await servicios.ObtenListaHorCatTurno();

            ViewBag.ListaCatalogo = lista;

            return View("Tablas/HorCatTurno");
        }

        [SessionExpireFilter]
        public async Task<bool> ActualizaHorCatTurno(string idTurno, string nombreTurno)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var objeto = new HOR_CAT_TURNO
            {
                IDTURNO = Convert.ToInt16(idTurno),
                NOMBRETURNO = nombreTurno
            };

            var actualizado = await servicio.ActualizaHorCatTurno(objeto);

            return actualizado;
        }

        [SessionExpireFilter]
        public async Task<ActionResult> NuevoRegistroHorCatTurno(string idTurno, string nombreTurno)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var objeto = new HOR_CAT_TURNO
            {
                IDTURNO = Convert.ToInt16(idTurno),
                NOMBRETURNO = nombreTurno
            };

            var nuevo = await servicio.InsertaHorCatTurno(objeto);

            var resultado = JsonConvert.SerializeObject(nuevo);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public async Task<ActionResult> EliminaRegistroHorCatTurno(string idTurno, string nombreTurno)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var obj = new HOR_CAT_TURNO
            {
                IDTURNO = Convert.ToInt16(idTurno),
                NOMBRETURNO = nombreTurno
            };

            await servicio.EliminaHorCatTurno(obj);

            return new RedirectToReturnUrlResult(() => RedirectToAction("EditaCatalogoHorCatTurno", "Catalogos"));
        }

        [SessionExpireFilter]
        public async Task<ActionResult> EditaCatalogoHorCatDiasSemana()
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicios = new SvcGestionCatalogos(sesion);

            var lista = await servicios.ObtenListaHorCatDiasSemana();

            ViewBag.ListaCatalogo = lista;

            return View("Tablas/HorCatDiasSemana");
        }

        [SessionExpireFilter]
        public async Task<bool> ActualizaHorCatDiasSemana(string idDia, string diaSemana)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var objeto = new HOR_CAT_DIAS_SEMANA
            {
                IDDIA = Convert.ToInt16(idDia),
                DIASEMANA = diaSemana
            };

            var actualizado = await servicio.ActualizaHorCatDiasSemana(objeto);

            return actualizado;
        }

        [SessionExpireFilter]
        public async Task<ActionResult> NuevoRegistroHorCatDiasSemana(string idDia, string diaSemana)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var objeto = new HOR_CAT_DIAS_SEMANA
            {
                IDDIA = Convert.ToInt16(idDia),
                DIASEMANA = diaSemana
            };

            var nuevo = await servicio.InsertaHorCatDiasSemana(objeto);

            var resultado = JsonConvert.SerializeObject(nuevo);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public async Task<ActionResult> EliminaHorCatDiasSemana(string idDia, string diaSemana)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var obj = new HOR_CAT_DIAS_SEMANA
            {
                IDDIA = Convert.ToInt16(idDia),
                DIASEMANA = diaSemana
            };

            await servicio.EliminaHorCatDiasSemana(obj);

            return new RedirectToReturnUrlResult(() => RedirectToAction("EditaCatalogoHorCatDiasSemana", "Catalogos"));
        }

        [SessionExpireFilter]
        public async Task<ActionResult> EditaCatalogoCarCatEspecialidad()
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicios = new SvcGestionCatalogos(sesion);

            var lista = await servicios.ObtenListaCarCatEspecialidad();

            ViewBag.ListaCatalogo = lista;

            return View("Tablas/CarCatEspecialidad");
        }

        [SessionExpireFilter]
        public async Task<bool> ActualizaCarCatEspecialidad(string idEspecialidad, string especialidad)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var objeto = new CAR_CAT_ESPECIALIDAD
            {
                IDESPECIALIDAD = Convert.ToInt16(idEspecialidad),
                ESPECIALIDAD = especialidad
            };

            var actualizado = await servicio.ActualizaCarCatEspecialidad(objeto);

            return actualizado;
        }

        [SessionExpireFilter]
        public async Task<ActionResult> NuevoRegistroCarCatEspecialidad(string idEspecialidad, string especialidad)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var objeto = new CAR_CAT_ESPECIALIDAD
            {
                IDESPECIALIDAD = Convert.ToInt16(idEspecialidad),
                ESPECIALIDAD = especialidad
            };

            var nuevo = await servicio.InsertaCarCatEspecialidad(objeto);

            var resultado = JsonConvert.SerializeObject(nuevo);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public async Task<ActionResult> EliminaCarCatEspecialidad(string idEspecialidad, string especialidad)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var obj = new CAR_CAT_ESPECIALIDAD
            {
                IDESPECIALIDAD = Convert.ToInt16(idEspecialidad),
                ESPECIALIDAD = especialidad
            };

            await servicio.EliminaCarCatEspecialidad(obj);

            return new RedirectToReturnUrlResult(() => RedirectToAction("EditaCatalogoCarCatEspecialidad", "Catalogos"));
        }

        #region Operaciones Catalogo HOR_CAT_HORAS

        [SessionExpireFilter]
        public ActionResult EditarCatalogoHorCatHoras()
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var listaTurnos = servicio.ObtenListaHorCatTurno();
            var listaHoras = servicio.ObtenListaHorCatHoras();

            var listTask = new List<Task>
            {
                listaTurnos,
                listaHoras
            };

            Task.WaitAll(listTask.ToArray());

            ViewBag.listaHoras = listaHoras.Result;

            ViewBag.listaTurnos = listaTurnos.Result.Select(c => new SelectListItem
            {
                Value = c.IDTURNO.ToString(CultureInfo.InvariantCulture),
                Text = c.NOMBRETURNO
            }).ToArray();

            return View("Tablas/HorCatHoras");
        }

        [SessionExpireFilter]
        public async Task<ActionResult> NuevoRegistroHorCatHoras(string idHora, string nombreHora, string horaInicio, string horaFinaliza, string idTurno, string descripcion)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var objeto = new HOR_CAT_HORAS
            {
                IDHORA = Convert.ToInt16(idHora),
                NOMBREHORA = nombreHora,
                HORAINICIO = Convert.ToDateTime(horaInicio),
                HORATERMINO = Convert.ToDateTime(horaFinaliza),
                DESCRIPCION = descripcion,
                IDTURNO = Convert.ToInt16(idTurno)
            };

            var nuevo = await servicio.InsertaCarCatHoras(objeto);

            var resultado = JsonConvert.SerializeObject(nuevo);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public async Task<bool> ActualizaHorCatHoras(string idHora, string nombreHora, string horaInicio, string horaFinaliza, string idTurno, string descripcion)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var objeto = new HOR_CAT_HORAS
            {
                IDHORA = Convert.ToInt16(idHora),
                NOMBREHORA = nombreHora,
                HORAINICIO = Convert.ToDateTime(horaInicio),
                HORATERMINO = Convert.ToDateTime(horaFinaliza),
                DESCRIPCION = descripcion,
                IDTURNO = Convert.ToInt16(idTurno)
            };

            var actualizado = await servicio.ActualizaHorCatHoras(objeto);

            return actualizado;
        }

        [SessionExpireFilter]
        public async Task<ActionResult> EliminaCarCatHoras(string idHora, string nombreHora, string horaInicio, string horaFinaliza, string idTurno, string descripcion)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var objeto = new HOR_CAT_HORAS
            {
                IDHORA = Convert.ToInt16(idHora),
                NOMBREHORA = nombreHora,
                HORAINICIO = Convert.ToDateTime(horaInicio),
                HORATERMINO = Convert.ToDateTime(horaFinaliza),
                DESCRIPCION = descripcion,
                IDTURNO = Convert.ToInt16(idTurno)
            };

            await servicio.EliminaHorCatHoras(objeto);

            return new RedirectToReturnUrlResult(() => RedirectToAction("EditarCatalogoHorCatHoras", "Catalogos"));
        }

        #endregion

        #region Operaciones Catalogo CAR_CAT_CARRERAS

        [SessionExpireFilter]
        public ActionResult EditarCatalogoCarCatCarreras()
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var listaEspecialidad = servicio.ObtenListaCarCatEspecialidad();
            var listaCarreras = servicio.ObtenListaCarCatCarreras();

            var listTask = new List<Task>
            {
                listaEspecialidad,
                listaCarreras
            };

            Task.WaitAll(listTask.ToArray());

            ViewBag.listaCarreras = listaCarreras.Result;

            ViewBag.listaEspecialidad = listaEspecialidad.Result.Select(c => new SelectListItem
            {
                Value = c.IDESPECIALIDAD.ToString(CultureInfo.InvariantCulture),
                Text = c.ESPECIALIDAD
            }).ToArray();

            return View("Tablas/CarCatCarreras");
        }

        [SessionExpireFilter]
        public async Task<ActionResult> NuevoRegistroCarCatCarreras(string idCarrera, string nombreCarrera, string idEspecialidad)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var objeto = new CAR_CAT_CARRERAS
            {
                IDCARRERA = Convert.ToInt16(idCarrera),
                IDESPECIALIDAD = Convert.ToInt16(idEspecialidad),
                NOMBRECARRERA = nombreCarrera
            };

            var nuevo = await servicio.InsertaCarCatCarreras(objeto);

            var resultado = JsonConvert.SerializeObject(nuevo);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public async Task<bool> ActualizaCalCatCarreras(string idCarrera, string nombreCarrera, string idEspecialidad)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var objeto = new CAR_CAT_CARRERAS
            {
                IDCARRERA = Convert.ToInt16(idCarrera),
                IDESPECIALIDAD = Convert.ToInt16(idEspecialidad),
                NOMBRECARRERA = nombreCarrera
            };

            var actualizado = await servicio.ActualizaCarCatCarreras(objeto);

            return actualizado;
        }

        [SessionExpireFilter]
        public async Task<ActionResult> EliminaCarCatCarreras(string idCarrera, string nombreCarrera, string idEspecialidad)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var objeto = new CAR_CAT_CARRERAS
            {
                IDCARRERA = Convert.ToInt16(idCarrera),
                IDESPECIALIDAD = Convert.ToInt16(idEspecialidad),
                NOMBRECARRERA = nombreCarrera
            };

            await servicio.EliminaCarCatCarreras(objeto);

            return new RedirectToReturnUrlResult(() => RedirectToAction("EditarCatalogoCarCatCarreras", "Catalogos"));
        }

        #endregion

        #region Operaciones Catalogo MAT_CAT_MATERIAS

        [SessionExpireFilter]
        public ActionResult EditarCatalogoMatCatMaterias()
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var listaCarreras = servicio.ObtenListaCarCatCarreras();
            var listaMaterias = servicio.ObtenListaMatCatMaterias();

            var listTask = new List<Task>
            {
                listaMaterias,
                listaCarreras
            };

            Task.WaitAll(listTask.ToArray());

            ViewBag.listaCarreras = listaCarreras.Result;

            ViewBag.listaCarreras = listaCarreras.Result.Select(c => new SelectListItem
            {
                Value = c.IDCARRERA.ToString(CultureInfo.InvariantCulture),
                Text = c.NOMBRECARRERA
            }).ToArray();

            return View("Tablas/MatCatMaterias");
        }

        [SessionExpireFilter]
        public async Task<ActionResult> NuevoRegistroMatCatMaterias(string idMateria, string nombreMateria, string creditos, string optativa)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);

            var objeto = new MAT_CAT_MATERIAS
            {
                IDMATERIA = Convert.ToInt16(idMateria),
                NOMBREMATERIA = nombreMateria,
                CREDITOS = Convert.ToDecimal(creditos),
                OPTATIVA = Convert.ToBoolean(optativa)
            };

            var nuevo = await servicio.InsertaMatCatMaterias(objeto);

            var resultado = JsonConvert.SerializeObject(nuevo);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}