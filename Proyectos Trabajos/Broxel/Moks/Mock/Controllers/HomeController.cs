namespace Mock.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Globalization;
    using Mock;

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
            var listaEstados = new List<VmEstado>
            {
                new VmEstado { IdEstado = 0, Descripcion = "Seleccionar Estado" },
                new VmEstado { IdEstado = 1, Descripcion = "Aguascalientes" },
                new VmEstado { IdEstado = 2, Descripcion = "Baja California" },
                new VmEstado { IdEstado = 3, Descripcion = "Baja California Sur" },
                new VmEstado { IdEstado = 4, Descripcion = "Campeche" },
                new VmEstado { IdEstado = 5, Descripcion = "Coahuila" },
                new VmEstado { IdEstado = 6, Descripcion = "Colima" },
                new VmEstado { IdEstado = 7, Descripcion = "Chiapas" },
                new VmEstado { IdEstado = 8, Descripcion = "Chihuahua" },
                new VmEstado { IdEstado = 9, Descripcion = "Distrito Federal" }
            };


            //listaEstados.Insert(0, new VmEstado { IdEstado = 0, Descripcion = "Seleccionar Estado" });

            var enumEstados = listaEstados.Select(c => new SelectListItem
            {
                Value = c.IdEstado.ToString(CultureInfo.InvariantCulture),
                Text = c.Descripcion
            }).ToArray();

            ViewBag.EnumEstados = enumEstados;


            var listaTrabajos = new List<VmTrabajo>
            {
                new VmTrabajo { IdTrabajo = 0, Descripcion = "Seleccionar trabajo" },
                new VmTrabajo { IdTrabajo = 1, Descripcion = "IMSS" },
                new VmTrabajo { IdTrabajo = 2, Descripcion = "CFE" },
                new VmTrabajo { IdTrabajo = 3, Descripcion = "PEMEX" },
                new VmTrabajo { IdTrabajo = 4, Descripcion = "INFONAVIT" }
            };



            //listaTrabajos.Insert(0, new VmTrabajo { IdTrabajo = 0, Descripcion = "Seleccionar trabajo" });

            var enumTrabajos = listaTrabajos.Select(c => new SelectListItem
            {
                Value = c.IdTrabajo.ToString(CultureInfo.InvariantCulture),
                Text = c.Descripcion
            }).ToArray();

            ViewBag.EnumTrabajos = enumTrabajos;

            var listaLugar = new List<VmLugar>
            {
                new VmLugar { IdLugar = 0, Descripcion = "Seleccionar Lugar" },
                new VmLugar { IdLugar = 1, Descripcion = "Centro" },
                new VmLugar { IdLugar = 2, Descripcion = "Sur" }
            };

            //listaLugar.Insert(0, new VmLugar { IdLugar = 0, Descripcion = "Seleccionar Lugar" });

            var enumLugares = listaLugar.Select(c => new SelectListItem
            {
                Value = c.IdLugar.ToString(CultureInfo.InvariantCulture),
                Text = c.Descripcion
            }).ToArray();

            ViewBag.EnumLugares = enumLugares;
            return View();
        }

        public bool ReenviaClaveSms(string telefono, string clave)
        {
            return true;
        }

        public bool ValidaClaveSms(string telefono, string clave)
        {
            return true;
        }

        public ActionResult ContinuarProceso()
        {
            return View();
        }

        public ActionResult Loggin()
        {
            return View();
        }

        public ActionResult Proceso(VmFormulario formulario)
        {
            return View();
        }
    }
}