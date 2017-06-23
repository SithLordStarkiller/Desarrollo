namespace Suncorp.WebSiteCorporativo.Controllers
{
    using Suncorp.Models;
    using Suncorp.ServiceController;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        SessionSecurityWcf session = new SessionSecurityWcf { UrlServer = Properties.Settings.Default.Url };

        public ActionResult Index()
        {
            var clientUsuarios = new UsuariosClient(session);
            var a = clientUsuarios.ObtenerUsuarioLogin("", "").Result;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}