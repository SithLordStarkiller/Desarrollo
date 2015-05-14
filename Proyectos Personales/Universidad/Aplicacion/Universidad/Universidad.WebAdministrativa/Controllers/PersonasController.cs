using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Universidad.Controlador.GestionCatalogos;
using Universidad.Entidades;
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

        [SessionExpireFilter]
        public void PersonaDefaultAsync()
        {
            Sesion();

            var sesion = (Entidades.ControlUsuario.Sesion)Session["Sesion"];
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
        public ActionResult PersonaDefaultCompleted(List<PER_CAT_NACIONALIDAD> listaPaises, List<PER_CAT_TIPO_PERSONA> listaTiposPersona)
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

            ViewData["paises"] = paises;
            ViewData["tipoPersona"] = tiposPersona;
            ViewData["sexo"] = sexo.ToArray();

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [SessionExpireFilter]
        //[ActionName("Complex")]
        public void NuevaPersonaAsync(ModelPersonaDatos xPersonaDatos)
        {
        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult NuevaPersonaCompleted(ModelPersonaDatos x)
        {
            Sesion();
            return View();
        }
    }
}