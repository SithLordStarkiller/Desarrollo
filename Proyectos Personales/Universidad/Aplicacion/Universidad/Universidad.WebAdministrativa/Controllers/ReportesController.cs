using System.Web.Mvc;

namespace Universidad.WebAdministrativa.Controllers
{
    public class ReportesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult Reportes()
        {
            return Redirect("../VisorReportes.aspx");
        }

        [SessionExpireFilter]
        public ActionResult ReportesDinamico(string reporte)
        {
            return Redirect("../VisorReportes.aspx?Reporte=" + reporte);
        }
    }
}