using System;
using System.Text;

namespace ExamenEdenred.SitioWeb.Controllers
{
    using System.Threading.Tasks;
    using Controller.Usuarios;
    using Entities.Models;
    using System.Web.Mvc;
    using System.IO;

    public class HomeController : AsyncController
    {
        public async Task<ActionResult> Index()
        {
            var session = new Session
            {
                Conexion = Properties.Settings.Default.PathService
            };

            Session["Session"] = Session;

            var servicioUsuarios = new ControllerUsuarios(session);

            var existe = await servicioUsuarios.ExisteUsuario();

            return View(existe);
        }

        public async Task<ActionResult> CargaArchivo()
        {
            if (Request.Files.Count <= 0) return View();

            var file = Request.Files[0];
            
            var sb = new StringBuilder();

            string texto;

            if (file == null || file.ContentLength <= 0) return View();

            var sr = new StreamReader(file.FileName);

            String line;

            while ((line = sr.ReadLine()) != null)
            {
                sb.AppendLine(line);
            }


            texto = sb.ToString();



            return View();
        }
    }
}