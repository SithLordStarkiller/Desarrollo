using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using Universidad.Controlador.GestionCatalogos;
using Universidad.Controlador.Personas;
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
        public async void CrearCuentaAsync(string personaId)
        {
            var sesion = (Sesion)Session["Sesion"];
            var servicosCatalogos = new SVC_GestionCatalogos(sesion);
            var serviciosPersona = new SvcPersonas(sesion);

            AsyncManager.Parameters["personaId"] = personaId;

            var persona = await serviciosPersona.BuscarPersona(personaId);
            
            AsyncManager.Parameters["persona"] = persona;

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
        public ActionResult CrearCuentaCompleted(List<US_CAT_ESTATUS_USUARIO> listaEstatus, List<US_CAT_NIVEL_USUARIO> listaNivelUsuario, List<US_CAT_TIPO_USUARIO> listaTipoUsuario, PER_PERSONAS persona)
        {
            Sesion();

            ViewBag.ListaEstatusUsuario = listaEstatus.Select(c => new SelectListItem
            {
                Value = c.ID_ESTATUS_USUARIOS.ToString(CultureInfo.InvariantCulture),
                Text = c.ESTATUS_USUARIO
            }).ToArray();

            ViewBag.ListaNivelUSuario = listaNivelUsuario.Select(c => new SelectListItem
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
        public ActionResult EditarCuenta(string personaId)
        {
            Sesion();
            ViewBag.PersonaId = personaId;
            var usuario = new US_USUARIOS();
            return View(usuario);
        }

    }
}