namespace ExamenEdenred.SitioWeb.Controllers
{
    using System.Threading.Tasks;
    using Controller.Usuarios;
    using Entities.Models;
    using System.Web.Mvc;

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
    }
}