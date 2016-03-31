namespace SunCorp.WebSiteAdministrator.Controller
{
    using System.Web.Mvc;

    public class HomeController : AsyncController
    {
        // GET: Home
        public ActionResult Site()
        {
            return View();
        }
    }
}