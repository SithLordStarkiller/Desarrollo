using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunCorp.WebSiteAdministrator.Controller
{
    public class HomeController : AsyncController
    {
        // GET: Home
        public ActionResult Site()
        {
            return View();
        }
    }
}