using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using PubliPayments.Entidades;
using PubliPayments.Negocios;
using PubliPayments.Utiles;

namespace PubliPayments.Controllers
{
    public class BuscarCreditosController : Controller
    {
        public ActionResult Index()
        {
            Refrescar();
            ViewData["Rol"] = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol);
            return View();
        }

        public void Refrescar()
        {
            new BuscarOrdenes().Refrescar(Session);
        }

        public ActionResult GrdOrdenes(Busqueda modelo)
        {
            var instancia = new BuscarOrdenes();

            if (instancia.ValidarBusqueda(Session,ref modelo) == false)
                return PartialView("grdOrdenes");

            modelo.IdUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));

            var tabla = instancia.BuscaOrdenes(modelo);   
            return PartialView("grdOrdenes", tabla);
        }

        public ActionResult DetalleCredito(string numcredito, string snConvenio = "1")
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)), "BuscarCreditos", "DetalleCredito - Credito : " + numcredito);

            var datosOrden = new BuscarOrdenes().ObtenerOrden(numcredito);

            if (datosOrden["sit"] == "6")
                return Content("6");

            if (datosOrden ["sit"] == "1" || snConvenio == "0")
                return RedirectToAction("Gestionar", "CapturaWeb", new { credito = numcredito });

            return RedirectToAction("Ver", "Respuesta", new { idOrden = datosOrden ["ord"], proceso = 1 });
        }

        public ActionResult CboMunicipio(string delegacion)
        {
            delegacion = new BuscarOrdenes().ValidaMun(Session, ref delegacion);

            ViewData["Municipios"] = new BuscarOrdenes().ObtenerMunicipios(delegacion);
            return PartialView();
        }

        public ActionResult CboNombre()
        {
            ViewData["Nombres"] = new BuscarOrdenes().ObtenerNombres();
            return PartialView();
        }

        public ActionResult CboDelegacion()
        {
            ViewData["Nombres"] = new BuscarOrdenes().ObtenerDelegaciones();
            return PartialView();
        }

        public string CrearOrden(string numCredito)
        {
            var idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            var ordenCreada = new BuscarOrdenes().CrearOrden(numCredito, -1, -2, idUsuario, -1, 0);
            return ordenCreada.ToString(CultureInfo.InvariantCulture);
        }
    }
}
