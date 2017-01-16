using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using PubliPayments.Entidades;
using PubliPayments.Negocios;

namespace PubliPayments.Controllers
{
    public class AdminFormularioController : Controller
    {
        //
        // GET: /AdminFormulario/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult grdConfiguracion()
        {
            var models = new CamposRespuestas().ObtenerDatos();
            return PartialView(models);
        }

        public ActionResult EditarEtiqueta(int idCampo)
        {
            var datos = new CamposRespuestas().ObtenerDatos().Where(m => m.IdCampo == idCampo).ToArray();
            var modelo = new CamposRespuestaModel();
            if (datos.Length > 0)
            {
                modelo = new CamposRespuestaModel
                {
                    IdCampo = int.Parse(datos[0].IdCampo.ToString(CultureInfo.InvariantCulture)),
                    Etiqueta = datos[0].Etiqueta,
                    Nombre = datos[0].Nombre
                };
            }
            return PartialView(modelo);
        }

        public ActionResult GuardarEtiqueta(CamposRespuestaModel modelo)
        {
            var res = new CamposRespuestas().GuardarRespuestas(modelo);
            var resultado = "";
            if (res) resultado = "OK";
            return Content(resultado);
        }
    }
}
