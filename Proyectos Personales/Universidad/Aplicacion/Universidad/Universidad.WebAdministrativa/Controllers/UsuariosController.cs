using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using Universidad.Controlador.GestionCatalogos;
using Universidad.Controlador.Personas;
using Universidad.Controlador.Usuarios;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.WebAdministrativa.Controllers
{
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
        public void CrearCuentaAsync(string personaId)
        {
            var sesion = (Sesion)Session["Sesion"];
            var servicosCatalogos = new SVC_GestionCatalogos(sesion);

            AsyncManager.Parameters["personaId"] = personaId;

            servicosCatalogos.ObtenTablaUsCatEstatusUsuarioAFinalizado +=
                delegate(List<US_CAT_ESTATUS_USUARIO> listaEstatusUsuarios)
                {
                    AsyncManager.Parameters["listaEstatus"] = listaEstatusUsuarios;
                    AsyncManager.OutstandingOperations.Decrement();
                };

            servicosCatalogos.ObtenTablaUsCatNivelUsuarioFinalizado +=
                delegate(List<US_CAT_NIVEL_USUARIO> listaNivelUsuarios)
                {
                    AsyncManager.Parameters["listaNivelUsuario"] = listaNivelUsuarios;
                    AsyncManager.OutstandingOperations.Decrement();
                };

            servicosCatalogos.ObtenTablaUsCatTipoUsuariosFinalizado +=
                delegate(List<US_CAT_TIPO_USUARIO> listaTipoUsuarios)
                {
                    AsyncManager.Parameters["listaTipoUsuario"] = listaTipoUsuarios;
                    AsyncManager.OutstandingOperations.Decrement();
                };

            AsyncManager.OutstandingOperations.Increment(3);
            servicosCatalogos.ObtenTablaUsCatTipoUsuario();
            servicosCatalogos.ObtenTablaUsCatEstatusUsuario();
            servicosCatalogos.ObtenTablaUsCatNivelUsuario();

            ViewBag.PersonaId = personaId;
        }

        [SessionExpireFilter]
        public ActionResult CrearCuentaCompleted(List<US_CAT_ESTATUS_USUARIO> listaEstatus, List<US_CAT_NIVEL_USUARIO> listaNivelUsuario, List<US_CAT_TIPO_USUARIO> listaTipoUsuario, string personaId)
        {
            Sesion();

            var sesion = (Sesion)Session["Sesion"];

            var serviciosPersona = new SvcPersonas(sesion);

            var persona = serviciosPersona.BuscarPersona(personaId);

            ViewBag.Persona = persona;

            ViewBag.ListaEstatusUsuario = listaEstatus.Select(c => new SelectListItem
            {
                Value = c.ID_ESTATUS_USUARIOS.ToString(CultureInfo.InvariantCulture),
                Text = c.ESTATUS_USUARIO
            }).ToArray();

            ViewBag.ListaNivelUsuario = listaNivelUsuario.Select(c => new SelectListItem
            {
                Value = c.ID_NIVEL_USUARIO.ToString(CultureInfo.InvariantCulture),
                Text = c.NIVEL_USUARIO
            }).ToArray();

            ViewBag.ListaTipoUsuario = listaTipoUsuario.Select(c => new SelectListItem
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
        public void EditarCuentaAsync(string personaId)
        {
            var sesion = (Sesion)Session["Sesion"];
            var servicosCatalogos = new SVC_GestionCatalogos(sesion);

            AsyncManager.Parameters["personaId"] = personaId;

            servicosCatalogos.ObtenTablaUsCatEstatusUsuarioAFinalizado +=
                delegate(List<US_CAT_ESTATUS_USUARIO> listaEstatusUsuarios)
                {
                    AsyncManager.Parameters["listaEstatus"] = listaEstatusUsuarios;
                    AsyncManager.OutstandingOperations.Decrement();
                };

            servicosCatalogos.ObtenTablaUsCatNivelUsuarioFinalizado +=
                delegate(List<US_CAT_NIVEL_USUARIO> listaNivelUsuarios)
                {
                    AsyncManager.Parameters["listaNivelUsuario"] = listaNivelUsuarios;
                    AsyncManager.OutstandingOperations.Decrement();
                };

            servicosCatalogos.ObtenTablaUsCatTipoUsuariosFinalizado +=
                delegate(List<US_CAT_TIPO_USUARIO> listaTipoUsuarios)
                {
                    AsyncManager.Parameters["listaTipoUsuario"] = listaTipoUsuarios;
                    AsyncManager.OutstandingOperations.Decrement();
                };

            AsyncManager.OutstandingOperations.Increment(3);
            servicosCatalogos.ObtenTablaUsCatTipoUsuario();
            servicosCatalogos.ObtenTablaUsCatEstatusUsuario();
            servicosCatalogos.ObtenTablaUsCatNivelUsuario();
        }

        [SessionExpireFilter]
        public ActionResult EditarCuentaCompleted(List<US_CAT_ESTATUS_USUARIO> listaEstatus, List<US_CAT_NIVEL_USUARIO> listaNivelUsuario, List<US_CAT_TIPO_USUARIO> listaTipoUsuario, string personaId)
        {

            Sesion();

            var sesion = (Sesion)Session["Sesion"];

            var serviciosPersona = new SvcPersonas(sesion);

            var persona = (serviciosPersona.BuscarPersona(personaId)).Result;

            ViewBag.Persona = persona;

            ViewBag.ListaEstatusUsuario = listaEstatus.Select(c => new SelectListItem
            {
                Value = c.ID_ESTATUS_USUARIOS.ToString(CultureInfo.InvariantCulture),
                Text = c.ESTATUS_USUARIO
            }).ToArray();

            ViewBag.ListaNivelUsuario = listaNivelUsuario.Select(c => new SelectListItem
            {
                Value = c.ID_NIVEL_USUARIO.ToString(CultureInfo.InvariantCulture),
                Text = c.NIVEL_USUARIO
            }).ToArray();

            ViewBag.ListaTipoUsuario = listaTipoUsuario.Select(c => new SelectListItem
            {
                Value = c.ID_TIPO_USUARIO.ToString(CultureInfo.InvariantCulture),
                Text = c.TIPO_USUARIO
            }).ToArray();

            var servicio = new SvcUsuarios(sesion);

            var idUsuario = (persona).ID_USUARIO;
            
            if (idUsuario == null) return View();
            
            var usuario = (servicio.ObtenUsuarioPorId((int) idUsuario)).Result;

            ViewBag.PersonaId = personaId;

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