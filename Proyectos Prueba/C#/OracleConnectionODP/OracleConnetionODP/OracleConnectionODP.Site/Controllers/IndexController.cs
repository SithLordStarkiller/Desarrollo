using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OracleConnectionODP.BusinessLogic;

namespace OracleConnectionODP.Site.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            var a = new EntitiesBl().LoginUserBl();
            return View("Index");
        }
    }
}