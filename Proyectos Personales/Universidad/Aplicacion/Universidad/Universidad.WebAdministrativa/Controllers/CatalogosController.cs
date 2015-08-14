using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Universidad.Controlador.GestionCatalogos;
using Universidad.Entidades;
using Universidad.Entidades.Catalogos;
using Universidad.Entidades.ControlUsuario;
using Newtonsoft.Json;

namespace Universidad.WebAdministrativa.Controllers
{
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
        public void CatalogosAsync()
        {
            var sesion = (Sesion)Session["sesion"];
            var servicioCatalogos = new SVC_GestionCatalogos(sesion);

            servicioCatalogos.ObtenTablasCatalogosFinalizado += delegate(List<ListasGenerica> lista)
            {
                AsyncManager.Parameters["lista"] = lista;
                AsyncManager.OutstandingOperations.Decrement();
            };

            AsyncManager.OutstandingOperations.Increment();
            servicioCatalogos.ObtenTablasCatalogos();
        }

        public ActionResult CatalogosCompleted(List<ListasGenerica> lista)
        {
            Sesion();

            ViewBag.ListaTablasCatalogos = lista.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(CultureInfo.InvariantCulture),
                Text = c.Nombre
            }).ToArray();

            return View();
        }

        [SessionExpireFilter]
        public void ObtenCatalogoAsync(string tabla)
        {
            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SVC_GestionCatalogos(sesion);

            switch (tabla)
            {
                case "DIR_CAT_COLONIAS":
                    servicio.ObtenCatalogosColoniasFinalizado += delegate(List<DIR_CAT_COLONIAS> colonias)
                    {
                        var tipo = (colonias.GetType().GetGenericArguments()[0]).Name;
                        var lista = JsonConvert.SerializeObject(colonias);
                        AsyncManager.Parameters["lista"] = lista;
                        AsyncManager.Parameters["tipo"] = tipo;
                        AsyncManager.OutstandingOperations.Decrement();
                    };
                    AsyncManager.OutstandingOperations.Increment();
                    servicio.ObtenCatalogosColonias();
                    break;

                case "DIR_CAT_DELG_MUNICIPIO":
                    servicio.ObtenCatalogosMunicipiosFinalizado += delegate(List<DIR_CAT_DELG_MUNICIPIO> municipios)
                    {
                        var tipo = (municipios.GetType().GetGenericArguments()[0]).Name;
                        var lista = JsonConvert.SerializeObject(municipios);
                        AsyncManager.Parameters["lista"] = lista;
                        AsyncManager.Parameters["tipo"] = tipo;
                        AsyncManager.OutstandingOperations.Decrement();
                    };
                    AsyncManager.OutstandingOperations.Increment();
                    servicio.ObtenCatalogosMunicipios();
                    break;
            }
        }


        [SessionExpireFilter]
        public ActionResult ObtenCatalogoCompleted(string lista, string tipo)
        {
            Sesion();

            ViewBag.Lista = lista;
            ViewBag.Tipo = tipo;

            return View();
        }
    }
}