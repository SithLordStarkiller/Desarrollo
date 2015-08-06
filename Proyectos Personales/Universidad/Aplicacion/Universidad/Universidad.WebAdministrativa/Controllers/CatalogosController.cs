using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Universidad.Controlador.GestionCatalogos;
using Universidad.Entidades;
using Universidad.Entidades.Catalogos;
using Universidad.Entidades.ControlUsuario;

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
            
            if (tabla == "DIR_CAT_COLONIAS")
            {
                servicio.ObtenCatalogosColoniasFinalizado += delegate(List<DIR_CAT_COLONIAS> colonias)
                {
                    AsyncManager.Parameters["listaColonias"] = colonias;
                    AsyncManager.OutstandingOperations.Decrement();
                };
                
                AsyncManager.OutstandingOperations.Increment();
                servicio.ObtenCatalogosColonias();
            }
        }

        [SessionExpireFilter]
        public ActionResult ObtenCatalogoCompleted(List<DIR_CAT_COLONIAS> listaColonias)
        {
            Sesion();

            //ViewBag.ListaColonias = listaColonias;
            return View();
        }
    }
}