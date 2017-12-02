using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proNegocio;
using proEntidad;
using proSeguridad;

namespace mvcSICER.Controllers
{
    public class AccountController : Controller
    {
        public JsonResult Autenticacion(string usuario, string contrasena)
        {
            clsEntSesion objSession = new clsEntSesion();
            objSession.Usuario = new clsEntUsuario();
            objSession.Usuario.UsuLogin = usuario.Trim();
            objSession.Usuario.UsuContrasenia = clsSegRijndaelSimple.Encrypt(contrasena.Trim());
            return Json(clsNegUsuario.consultarPermisoAdmin(ref objSession), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);

        }

        public JsonResult revisarSesion()
        {
            string P = Convert.ToString(System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID]);
            return Json(System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID] == null ? true : false, "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutenticacionExamen(string usuario, string contrasena)
        {
            clsEntSesion objSession = new clsEntSesion();
            objSession.Usuario = new clsEntUsuario();
            objSession.Usuario.UsuLogin = usuario.Trim();
            objSession.Usuario.UsuContrasenia = clsSegRijndaelSimple.Encrypt(contrasena.Trim());
            
            return Json(clsNegUsuario.consultarPermisoUsuarioExamen(objSession), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);

        }


        public JsonResult mantieneSesion()
        {
            return Json(true, "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
    }
}
