using System;
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

        public void CargaListas()
        {
            ViewBag.ListaPaises = Session["ListaPaises"];
            ViewBag.ListaTipoPersona = Session["ListaTipoPersona"];
            ViewBag.ListaSexo = Session["ListaSexo"];
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

            Session["ListaPaises"] = paises;
            Session["ListaTipoPersona"] = tiposPersona;
            Session["ListaSexo"] = sexo.ToArray();

            CargaListas();
            return View();
        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult NuevaPersona(ModelPersonaDatos modelo)
        {
            if (ModelState.IsValid)
            {
                var persona = new PER_PERSONAS
                {
                    NOMBRE = modelo.Nombre,
                    A_PATERNO = modelo.ApellidoP,
                    A_MATERNO = modelo.ApellidoM,
                    CURP = modelo.Curp,
                    RFC = modelo.Rfc,
                    IMSS = modelo.Nss,
                    CVE_NACIONALIDAD = Convert.ToInt32(modelo.IdNacionalidad),
                    ID_TIPO_PERSONA = Convert.ToInt32(modelo.IdTipoPersona),
                    SEXO = modelo.IdSexo == "1" ? "M" : "F",
                    FECHA_NAC = modelo.FechaNacimiento,
                };

                RegistraPersonaAsync(persona);
                Sesion();
                CargaListas();
                return View("PersonaDefault");
            }
            else
            {
                Sesion();
                CargaListas();
                return View("PersonaDefault",modelo);
            }
        }

        [SessionExpireFilter]
        public void RegistraPersonaAsync(PER_PERSONAS persona)
        {

        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult RegistraPersonaCompleted(ModelPersonaDatos x)
        {
            Sesion();
            return View();
        }
    }
}