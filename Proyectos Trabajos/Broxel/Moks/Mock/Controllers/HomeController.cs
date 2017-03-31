using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mock.Models;
using System.Globalization;

namespace Mock.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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

        public ActionResult Mock()
        {
            var listaEstados = new List<Estado>();

            listaEstados.Insert(0, new Estado { IdEstado = 0, Descripcion = "Seleccionar Estado" });

            var enumEstados = listaEstados.Select(c => new SelectListItem
            {
                Value = c.IdEstado.ToString(CultureInfo.InvariantCulture),
                Text = c.Descripcion
            }).ToArray();

            ViewBag.EnumEstados = enumEstados;


            var listaTrabajos = new List<Trabajo>();

            listaTrabajos.Insert(0, new Trabajo { IdTrabajo = 0, Descripcion = "Seleccionar trabajo" });

            var enumTrabajos = listaTrabajos.Select(c => new SelectListItem
            {
                Value = c.IdTrabajo.ToString(CultureInfo.InvariantCulture),
                Text = c.Descripcion
            }).ToArray();

            ViewBag.EnumTrabajos = enumTrabajos;

            var listaLugar = new List<Lugar>();

            listaLugar.Insert(0, new Lugar { IdLugar = 0, Descripcion = "Seleccionar Lugar" });

            var enumLugares = listaLugar.Select(c => new SelectListItem
            {
                Value = c.IdLugar.ToString(CultureInfo.InvariantCulture),
                Text = c.Descripcion
            }).ToArray();

            ViewBag.EnumLugares = enumLugares;
            return View();
        }
    }
}