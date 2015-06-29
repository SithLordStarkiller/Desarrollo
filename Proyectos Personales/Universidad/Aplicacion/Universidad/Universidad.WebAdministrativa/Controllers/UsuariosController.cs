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
        public ActionResult CrearCuenta(string personaId)
        {
            var sesion = (Sesion)Session["Sesion"];
            var servicosCotalogos = new SVC_GestionCatalogos(sesion);
            Sesion();
            ViewBag.PersonaId = personaId;
            var usuario = new US_USUARIOS();
            return View(usuario);
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