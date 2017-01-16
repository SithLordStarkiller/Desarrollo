using System;
using System.Web.Mvc;
using System.Web.Security;

namespace PubliPayments.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/
        [ChildActionOnly]
        public ActionResult Header()
        {
            var nombre = SessionUsuario.ObtenerDato(SessionUsuarioDato.NombreUsuario);
            ViewBag.NombreUsuario = String.Format("{0}",nombre.Length > 22 ? nombre.Substring(0, 22) + "..." : nombre);
            ViewBag.RolUsuario =  SessionUsuario.ObtenerDato(SessionUsuarioDato.NombreRol).Replace("London", (Config.AplicacionActual().idAplicacion==1?  "Infonavit":"General"));
            ViewBag.NombreDominio = SessionUsuario.ObtenerDato(SessionUsuarioDato.NombreDominio);
            ViewBag.idAplicacion = Config.AplicacionActual().idAplicacion;
            ViewBag.isOriginacion = Config.AplicacionActual().Nombre.Contains("OriginacionMovil");
            ViewBag.NombreAplicacion = Config.AplicacionActual().Nombre.ToUpper();
            ViewBag.EsCallCenter = Config.EsCallCenter || SessionUsuario.ObtenerDato(SessionUsuarioDato.EsCallCenterOut).ToLower()=="true";
            return PartialView("~/Views/Shared/_Header.cshtml");
        }

        public EmptyResult CerrarSesion()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
            return new EmptyResult();
        }

    }
}
