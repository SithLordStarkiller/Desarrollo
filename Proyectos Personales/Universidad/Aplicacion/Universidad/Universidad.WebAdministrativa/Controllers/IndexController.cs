using System.Web.Mvc;
using Universidad.Controlador.Login;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.WebAdministrativa.Controllers
{
    public class IndexController : AsyncController
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void LogInAsync(Sesion sesion)
        {
            sesion.Conexion = Properties.Settings.Default.RutaServidorInterno;
            AsyncManager.Parameters["sesion"] = sesion;
            var servidorLogin = new SVC_LoginAdministrativos(sesion);
            servidorLogin.LoginAdministrativosFinalizado += delegate(US_USUARIOS usuarios)
            {
                AsyncManager.Parameters["usuario"] = usuarios;
                AsyncManager.OutstandingOperations.Decrement();
            };
            AsyncManager.OutstandingOperations.Increment();
            servidorLogin.LoginAdministrativo(sesion.Usuario, sesion.Contrasena);
        }

        public ActionResult LogInCompleted(US_USUARIOS usuario, Sesion sesion)
        {
            System.Web.HttpContext.Current.Session["Usuario"] = "Usuario";
            System.Web.HttpContext.Current.Session["Sesion"] = "Sesion";
            System.Web.HttpContext.Current.Session["Persona"] = "Persona";

            Session["Sesion"] = sesion;
            Session["Usuario"] = usuario;

            return RedirectToAction("Default", "Home");
        }

        public ActionResult CerrarSesion()
        {
            Session["Sesion"] = null;
            Session["Usuario"] = null;
            Session["Persona"] = null;
            Session["TipoPersona"] = null;
            return View("Index");
        }
    }
}