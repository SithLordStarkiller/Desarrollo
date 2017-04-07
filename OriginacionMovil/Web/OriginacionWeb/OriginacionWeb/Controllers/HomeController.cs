namespace OriginacionWeb.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Globalization;
    using System.Linq;
    using PubliPayments.Negocios.Originacion;
    using OriginacionMovil.Models;
    using System.Web;
    using System.Web.Security;
    using PubliPayments.Entidades;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registro()
        {
            var listaEstados = new List<CatEstados> { new CatEstados { Id = 0, EstadoDescripcion = "Selecciona tu localidad" } };

            listaEstados.AddRange(new RegistrarUsuarioWeb().ObtenerEstados());

            var enumEstados = listaEstados.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(CultureInfo.InvariantCulture),
                Text = c.EstadoDescripcion,
                Selected = c.Id == 0
            }).ToArray();

            ViewBag.EnumEstados = enumEstados;


            var listaTrabajos = new List<CatEmpresa> { new CatEmpresa { Id = 0, Nombre = "Selecciona tu empresa" } };

            var enumTrabajos = listaTrabajos.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(CultureInfo.InvariantCulture),
                Text = c.Nombre
            }).ToArray();

            ViewBag.EnumTrabajos = enumTrabajos;


            var listaLugar = new List<CatLugar>();

            listaLugar.Insert(0, new CatLugar { Id = 0, Nombre = "Selecciona el lugar de entrega de documentacion" });

            var enumLugares = listaLugar.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(CultureInfo.InvariantCulture),
                Text = c.Nombre
            }).ToArray();

            ViewBag.EnumLugares = enumLugares;

            return View();
        }

        [HttpPost]
        public PartialViewResult sliEmpresas(string idPadre)
        {
            try
            {
                var lista = new List<CatEmpresa> { new CatEmpresa { Id = 0, Nombre = "Seleccionar tu empresa" } };

                var listaEmpresas = new RegistrarUsuarioWeb().ObtenerEmpresas().Where(x => x.IdPadre == idPadre).ToList();

                lista.AddRange(listaEmpresas);

                var sliEmpresas = lista.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(CultureInfo.InvariantCulture),
                    Text = c.Nombre,
                    Selected = c.Id == 0
                }).ToArray();

                ViewBag.IdDropDown = "idTrabajo";

                return PartialView("_SelectListItem", sliEmpresas);
            }
            catch (Exception)
            {
                return PartialView("_SelectListItem", null);
            }
        }

        [HttpPost]
        public PartialViewResult sliLugar(string idExterno)
        {
            try
            {
                var lista = new List<CatLugar> { new CatLugar { Id = 0, Nombre = "Selecciona el lugar de entrega de documentacion" } };

                var listaLugares = new RegistrarUsuarioWeb().ObtenerLugares().Where(x => x.IdExterno == idExterno).ToList();

                lista.AddRange(listaLugares);

                var sliLugares = lista.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(CultureInfo.InvariantCulture),
                    Text = c.Nombre,
                    Selected = c.Id == 0
                }).ToArray();

                ViewBag.IdDropDown = "idLugares";

                return PartialView("_SelectListItem", sliLugares);
            }
            catch (Exception)
            {
                return PartialView("_SelectListItem", null);
            }
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

        private void Crearcookie(UsuarioModel modelUsuario, bool cbRecordarme)
        {

            string userData = modelUsuario.IdUsuario + "," +
                              modelUsuario.IdRol + "," +
                              modelUsuario.Usuario + "," +
                              modelUsuario.Estatus + "," +
                              //modelUsuario.Perfil + "," +
                              modelUsuario.Nombre;

            // Create a new ticket used for authentication
            var ticket = new FormsAuthenticationTicket(
                1, // Ticket version
                modelUsuario.IdUsuario.ToString(CultureInfo.InvariantCulture), // Username associated with ticket
                DateTime.Now, // Date/time issued
                cbRecordarme ? DateTime.Now.AddDays(2) : DateTime.Now.AddMinutes(30), // Date/time to expire
                true, // "true" for a persistent user cookie
                userData, // User-data, in this case the roles
                FormsAuthentication.FormsCookiePath); // Path cookie valid for

            // Encrypt the cookie using the machine key for secure transport
            var hash = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(
                FormsAuthentication.FormsCookieName, // Name of auth cookie
                hash) { Expires = ticket.Expiration }; // Hashed ticket

            // Set the cookie's expiration time to the tickets expiration time
            //if (ticket.IsPersistent)

            // Add the cookie to the list for outgoing response
            Response.Cookies.Add(cookie);
            System.Web.HttpContext.Current.Session["SessionUsuario"] = userData.Split(',');
        }
    }
}