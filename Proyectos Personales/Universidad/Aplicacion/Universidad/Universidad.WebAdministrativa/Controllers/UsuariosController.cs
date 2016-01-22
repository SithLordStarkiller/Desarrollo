namespace Universidad.WebAdministrativa.Controllers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Newtonsoft.Json;
    using Controlador.GestionCatalogos;
    using Controlador.Personas;
    using Controlador.Usuarios;
    using Entidades.ControlUsuario;

    public class UsuariosController : AsyncController
    {
        [SessionExpireFilter]
        public void Sesion()
        {
            var persona = ((PER_PERSONAS)Session["Persona"]);
            ViewBag.tipoUsuario = ((US_CAT_TIPO_USUARIO)Session["TipoPersona"]).TIPO_USUARIO;
            ViewBag.nombre = persona.NOMBRE + " " + persona.A_PATERNO + " " + persona.A_MATERNO;
        }

        [SessionExpireFilter]
        public void RegistrarUsuarioAsync()
        {
        }

        [SessionExpireFilter]
        public ActionResult RegistrarUsuarioCompleted()
        {
            Sesion();
            return View();
        }

        [SessionExpireFilter]
        public async Task<ActionResult> BuscaPersona(string idPersona)
        {
            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcPersonas(sesion);

            var persona = await servicio.BuscarPersonaCompleta(idPersona);
            var resultado = JsonConvert.SerializeObject(persona);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public ActionResult CrearCuenta(string personaId)
        {
            var sesion = (Sesion)Session["Sesion"];
            var servicosCatalogos = new SvcGestionCatalogos(sesion);
            var serviciosPersona = new SvcPersonas(sesion);

            AsyncManager.Parameters["personaId"] = personaId;
            
            Sesion();

            var listaEstatusUsuarios = servicosCatalogos.ObtenTablaUsCatEstatusUsuario();

            var listaNivelUsuarios = servicosCatalogos.ObtenTablaUsCatNivelUsuario();

            var listaTipoUsuarios = servicosCatalogos.ObtenTablaUsCatTipoUsuario();

            var persona = serviciosPersona.BuscarPersona(personaId);

            var listTask = new List<Task>
            {
                listaEstatusUsuarios,
                listaNivelUsuarios,
                listaTipoUsuarios,
                persona
            };

            Task.WaitAll(listTask.ToArray());

            ViewBag.PersonaId = personaId;

            ViewBag.Persona = persona;

            ViewBag.ListaEstatusUsuario = listaEstatusUsuarios.Result.Select(c => new SelectListItem
            {
                Value = c.ID_ESTATUS_USUARIOS.ToString(CultureInfo.InvariantCulture),
                Text = c.ESTATUS_USUARIO
            }).ToArray();

            ViewBag.ListaNivelUsuario = listaNivelUsuarios.Result.Select(c => new SelectListItem
            {
                Value = c.ID_NIVEL_USUARIO.ToString(CultureInfo.InvariantCulture),
                Text = c.NIVEL_USUARIO
            }).ToArray();

            ViewBag.ListaTipoUsuario = listaTipoUsuarios.Result.Select(c => new SelectListItem
                {
                    Value = c.ID_TIPO_USUARIO.ToString(CultureInfo.InvariantCulture),
                    Text = c.TIPO_USUARIO
                }).ToArray();

            return View();
        }

        [SessionExpireFilter]
        public async Task<ActionResult> ObtenUsuario(string usuario)
        {
            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcUsuarios(sesion);

            var persona = await servicio.ObtenUsuario(usuario);
            var resultado = JsonConvert.SerializeObject(persona);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionExpireFilter]
        public async Task<int> GuardaCuentaUsuario(string usuario, string contrasena, int tipoUsuario, int nivelUsuario, int estatusUsuario, string personaId)
        {
            var nuevoUsuario = new US_USUARIOS
            {
                USUARIO = usuario,
                CONTRASENA = contrasena,
                ID_TIPO_USUARIO = tipoUsuario,
                ID_NIVEL_USUARIO = nivelUsuario,
                ID_ESTATUS_USUARIOS = estatusUsuario
            };

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcUsuarios(sesion);

            nuevoUsuario = await servicio.CreaCuentaUsuario(nuevoUsuario, personaId);

            return nuevoUsuario == null ? 0 : nuevoUsuario.ID_USUARIO;
        }

        [SessionExpireFilter]
        public ActionResult EditarCuenta(string personaId)
        {
            var sesion = (Sesion)Session["Sesion"];

            var servicosCatalogos = new SvcGestionCatalogos(sesion);

            var serviciosPersona = new SvcPersonas(sesion);

            AsyncManager.Parameters["personaId"] = personaId;

            var listaEstatusUsuarios = servicosCatalogos.ObtenTablaUsCatEstatusUsuario();

            var listaNivelUsuarios = servicosCatalogos.ObtenTablaUsCatNivelUsuario();

            var listaTipoUsuarios = servicosCatalogos.ObtenTablaUsCatTipoUsuario();


            var persona = serviciosPersona.BuscarPersona(personaId);

            var listTask = new List<Task>
            {
                listaEstatusUsuarios,
                listaNivelUsuarios,
                listaTipoUsuarios,
                persona
            };

            Task.WaitAll(listTask.ToArray());

            ViewBag.Persona = persona.Result;

            ViewBag.ListaEstatusUsuario = listaEstatusUsuarios.Result.Select(c => new SelectListItem
            {
                Value = c.ID_ESTATUS_USUARIOS.ToString(CultureInfo.InvariantCulture),
                Text = c.ESTATUS_USUARIO
            }).ToArray();

            ViewBag.ListaNivelUsuario = listaNivelUsuarios.Result.Select(c => new SelectListItem
            {
                Value = c.ID_NIVEL_USUARIO.ToString(CultureInfo.InvariantCulture),
                Text = c.NIVEL_USUARIO
            }).ToArray();

            ViewBag.ListaTipoUsuario = listaTipoUsuarios.Result.Select(c => new SelectListItem
            {
                Value = c.ID_TIPO_USUARIO.ToString(CultureInfo.InvariantCulture),
                Text = c.TIPO_USUARIO
            }).ToArray();

            var servicio = new SvcUsuarios(sesion);

            var idUsuario = (persona.Result).ID_USUARIO;
            
            if (idUsuario == null) return View();
            
            var usuario = (servicio.ObtenUsuarioPorId((int) idUsuario)).Result;

            ViewBag.PersonaId = personaId;

            Sesion();

            return View(usuario);
        }

        [HttpPost]
        [SessionExpireFilter]
        public async Task<bool> EditaCuentaUsuario(int idUsuario,string usuario, string contrasena, int tipoUsuario, int nivelUsuario, int estatusUsuario)
        {
            var nuevoUsuario = new US_USUARIOS
            {
                ID_USUARIO = idUsuario,
                USUARIO = usuario,
                CONTRASENA = contrasena,
                ID_TIPO_USUARIO = tipoUsuario,
                ID_NIVEL_USUARIO = nivelUsuario,
                ID_ESTATUS_USUARIOS = estatusUsuario
            };

            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcUsuarios(sesion);

            nuevoUsuario = await servicio.ActualizaCuentaUsuario(nuevoUsuario);

            return nuevoUsuario != null;
        }

    }
}