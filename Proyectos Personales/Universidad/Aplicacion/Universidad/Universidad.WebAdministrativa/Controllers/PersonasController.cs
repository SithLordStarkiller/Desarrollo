using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Universidad.Entidades;

namespace Universidad.WebAdministrativa.Controllers
{
    public class PersonasController : AsyncController
    {
        public ActionResult PersonaDefault()
        {
            Sesion();
            return View();
        }

        [HttpPost]
        public void NuevaPersonaAsync()
        {
            var companyname = Request.Form["a"];
            var contactname = Request.Form["b"];
            var employeecount = Request.Form["c"];
        }

        public ActionResult NuevaPersonaCompleted()
        {
            Sesion();
            return View();
        }

        public void Sesion()
        {
            var persona = ((PER_PERSONAS)Session["Persona"]);
            ViewBag.tipoUsuario = ((US_CAT_TIPO_USUARIO)Session["TipoPersona"]).TIPO_USUARIO;
            ViewBag.nombre = persona.NOMBRE + " " + persona.A_PATERNO + " " + persona.A_MATERNO;
        }
    }
}