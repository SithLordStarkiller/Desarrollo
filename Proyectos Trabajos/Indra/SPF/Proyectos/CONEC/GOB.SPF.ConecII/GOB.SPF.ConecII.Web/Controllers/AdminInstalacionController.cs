using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GOB.SPF.ConecII.Web.Models;
using GOB.SPF.ConecII.Web.Servicios;

namespace GOB.SPF.ConecII.Web.Controllers
{
    public class AdminInstalacionController : Controller
    {
        #region Administracion de instalacion

        #region Formulario principal
        public ActionResult ConsultarAdminInstalacion()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public PartialViewResult FactorLeyIngreso(UiFactorLeyIngreso model)
        {

            switch (model.Action)
            {
                case UiEnumEntity.New:
                    ViewBag.Title = "Crear Factor Ley Ingreso";
                    break;
                case UiEnumEntity.Edit:
                    ViewBag.Title = "Modificar Factor Ley Ingreso";
                    break;
                case UiEnumEntity.View:
                    ViewBag.Title = "Detalle del Factor Ley Ingreso";
                    break;
            }

            ServicesCatalog clientService = new ServicesCatalog();



            return PartialView(model);
        }

        #endregion

        #region Guardar 



        #endregion
        #endregion
    }
}