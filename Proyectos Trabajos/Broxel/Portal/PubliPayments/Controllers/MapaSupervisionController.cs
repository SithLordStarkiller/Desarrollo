using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using PubliPayments.Entidades;

namespace PubliPayments.Controllers
{
    public class MapaSupervisionController : Controller
    {
        //
        // GET: /MapaSupervision/

        public ActionResult Index()
        {
            if (SessionUsuario.ObtenerDato(SessionUsuarioDato.Dominio) == "0" || "4".Contains(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol)))
            {
                Response.Redirect("unauthorized.aspx");
            }
            ViewData["AplicacionLogos"] = "/imagenes/Logos" + Config.AplicacionActual().Nombre + "/";
            ViewData["Aplicacion"] = Config.AplicacionActual().idAplicacion;
            return View();
        }

        public ActionResult ObtenerUsuarios()
        {
            var usuarios = new Models.UsuariosMapa();
            var lista = usuarios.ObtenerUsuariosMapa(Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)), Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio)));
            return PartialView(lista);
        }

        public ActionResult ObtenerRespuestasGps(List<string> usuarios, string fecha, string tipo)
        {
            var fechaSeleccionada = Convert.ToDateTime(fecha, new CultureInfo("es-MX"));
            var grupoMarcadores = new List<EntGrupoMarcadoresGps>();
            var contador = 0;
            Parallel.ForEach(usuarios, usuario =>
            {
                var marcadores = new EntRespuestas().ObtenerRespuestasMarcadores(Convert.ToInt32(usuario),
                    fechaSeleccionada, Convert.ToInt32(tipo),
                    CtrlComboFormulariosController.ObtenerModeloFormularioActivo(this.Session).Ruta);
                if (marcadores.Count > 0)
                {
                    var nombre = new EntUsuario().ObtenerUsuarioPorId(Convert.ToInt32(usuario)).Usuario;
                    var grupo = new EntGrupoMarcadoresGps
                    {
                        Color = contador,
                        ListaMarcadores = marcadores,
                        Cantidad = marcadores.Count,
                        Nombre = nombre
                    };
                    contador++;
                    grupoMarcadores.Add(grupo);
                }
            });
            return Json(grupoMarcadores);
        }
    }
}
