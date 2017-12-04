using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOB.SPF.ConecII.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult Forbidden()
        {
            return View();
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult InternalServerError()
        {
            return View();
        }
    }
}