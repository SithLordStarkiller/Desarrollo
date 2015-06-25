
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
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

        [HttpPost]
        [SessionExpireFilter]
        public void CreaUsuarioAsync(string idPersonaLink)
        {
        }

        [SessionExpireFilter]
        public string CreaUsuarioCompleted()
        {
            return null;
        }
    }
}