using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
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

        public void LogInAsync()
        {
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

        public ActionResult LogInCompleted(US_USUARIOS usuario,Sesion sesion)
        {
            TempData["usuario"] = usuario;
            TempData["sesion"] = sesion;
            return RedirectToAction("Default", "Home");
        }
    }
}