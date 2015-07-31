using System.Web.Mvc;
using Microsoft.Reporting.WebForms;

namespace Universidad.WebAdministrativa.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Reportes()
        {
            return Redirect("../Views/Reportes/VisorReportes.aspx");
        }
    }
}