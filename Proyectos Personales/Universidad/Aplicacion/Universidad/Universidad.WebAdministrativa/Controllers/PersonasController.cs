using System.Web.Mvc;
using Universidad.Entidades;

namespace Universidad.WebAdministrativa.Controllers
{
    public class PersonasController : AsyncController
    {
        public ActionResult PersonaDefault()
        {
            SesionActiva();
            Sesion();
            return View();
        }

        [HttpPost]
        public void NuevaPersonaAsync(string a, string b, string c)
        {
            SesionActiva();
            
        }

        public ActionResult NuevaPersonaCompleted()
        {
            SesionActiva();
            Sesion();
            return View();
        }

        public void Sesion()
        {
            var persona = ((PER_PERSONAS)Session["Persona"]);
            ViewBag.tipoUsuario = ((US_CAT_TIPO_USUARIO)Session["TipoPersona"]).TIPO_USUARIO;
            ViewBag.nombre = persona.NOMBRE + " " + persona.A_PATERNO + " " + persona.A_MATERNO;
        }

        public bool SesionActiva()
        {
            var sesion=Session["Sesion"];
            var usuario = Session["Usuario"];
            var persona = Session["Persona"];
            var tipoPersona = Session["TipoPersona"];

            var activa = (sesion != null || usuario != null || persona != null || tipoPersona != null);

            if (activa) return true;

            Session["Sesion"] = null;
            Session["Usuario"] = null;
            Session["Persona"] = null;
            Session["TipoPersona"] = null;

            Session.Remove("Sesion");
            Session.Remove("Usuario");
            Session.Remove("Persona");
            Session.Remove("TipoPersona");

            RedirectToAction("Index", "Index");
            return false;
        }
    }
}