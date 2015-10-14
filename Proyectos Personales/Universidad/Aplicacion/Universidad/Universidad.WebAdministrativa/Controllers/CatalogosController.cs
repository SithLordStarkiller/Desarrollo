using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Universidad.Controlador.GestionCatalogos;
using Universidad.Entidades;
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
        public async Task<ActionResult> CatalogosAsync()
        {
            var sesion = (Sesion)Session["sesion"];
            var servicioCatalogos = new SvcGestionCatalogos(sesion);

            var lista = await servicioCatalogos.ObtenTablasCatalogos();

            Sesion();

            ViewBag.ListaTablasCatalogos = lista.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(CultureInfo.InvariantCulture),
                Text = c.Nombre
            }).ToArray();

            return View();
        }

        [SessionExpireFilter]
        public async Task<ActionResult> ObtenCatalogoAsync(string tabla)
        {
            var sesion = (Sesion)Session["Sesion"];
            var servicio = new SvcGestionCatalogos(sesion);
            string lista, tipo;

            switch (tabla)
            {
                case "DIR_CAT_COLONIAS":
                    var colonias = await servicio.ObtenCatalogosColonias();
                    tipo = (colonias.GetType().GetGenericArguments()[0]).Name;
                    lista = JsonConvert.SerializeObject(colonias);
                    Sesion();
                    ViewBag.Lista = lista;
                    ViewBag.Tipo = tipo;
                    break;

                case "DIR_CAT_DELG_MUNICIPIO":
                    var municipios = await servicio.ObtenCatalogosMunicipios();
                    tipo = (municipios.GetType().GetGenericArguments()[0]).Name;
                    lista = JsonConvert.SerializeObject(municipios);
                    ViewBag.Lista = lista;
                    ViewBag.Tipo = tipo;
                    break;
            }

            return View();
        }
    }
}