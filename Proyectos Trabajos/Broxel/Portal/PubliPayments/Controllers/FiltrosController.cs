using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using PubliPayments.App_Code;
using PubliPayments.Entidades;
using PubliPayments.Models;

namespace PubliPayments.Controllers
{
    public class FiltrosController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var routeParams=new ValoresFiltros
            {
                Delegacion = "%",
                Despacho = "%",
                Supervisor = "%",
                Gestor = "%",
                TipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).IdFormulario.ToString()
            };

            var parentActionViewContext = ControllerContext.ParentActionViewContext;
            while (parentActionViewContext.ParentActionViewContext != null)
            {
                parentActionViewContext = parentActionViewContext.ParentActionViewContext;
            }
            var rd = parentActionViewContext.RouteData;
            var currentAction = rd.Values["action"].ToString();
            var currentController = rd.Values["controller"].ToString();

            var tabla = new EntDashboard().ObtenerFiltros(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario), routeParams.Delegacion, routeParams.Despacho, routeParams.Supervisor, routeParams.Gestor,routeParams.TipoFormulario);

            var model = (from e in tabla.AsEnumerable() select e);

            var rol = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
            var nomRol = "";

            switch (rol)
            {
                case 0:
                    nomRol = filtroDashBoard.Administrador.ToString();
                    break;
                case 1:
                    nomRol = filtroDashBoard.Administrador.ToString();
                    break;
                case 2:
                    nomRol = filtroDashBoard.Despacho.ToString();
                    break;
                case 3:
                    nomRol = filtroDashBoard.Supervisor.ToString();
                    break;
                case 4:
                    nomRol = filtroDashBoard.Gestor.ToString();
                    break;
                case 5:
                    nomRol = filtroDashBoard.Delegacion.ToString();
                    break;
                case 6:
                    nomRol = filtroDashBoard.Direccion.ToString();
                    break;
                default:
                    break;
            }

            ViewBag.Delegacion = routeParams.Delegacion;
            ViewBag.Despacho = routeParams.Despacho;
            ViewBag.Supervisor = routeParams.Supervisor;
            ViewBag.Gestor = routeParams.Gestor;
            ViewBag.rol = nomRol;
            return PartialView(model);
        }


        [HttpPost]
        public ActionResult Index(ValoresFiltros routeParams)
        {
            if (routeParams == null)
            {
                return HttpNotFound();
            }

            if (ControllerContext.ParentActionViewContext!=null)
            {
                var parentActionViewContext = ControllerContext.ParentActionViewContext;
                while (parentActionViewContext.ParentActionViewContext != null)
                {
                    parentActionViewContext = parentActionViewContext.ParentActionViewContext;
                }
                var rd = parentActionViewContext.RouteData;
                var currentAction = rd.Values["action"].ToString();
                var currentController = rd.Values["controller"].ToString();
            }

            var tabla = new EntDashboard().ObtenerFiltros(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario), routeParams.Delegacion, routeParams.Despacho, routeParams.Supervisor, routeParams.Gestor, routeParams.TipoFormulario);

            var model =(from e in tabla.AsEnumerable() select e);

            var rol = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
            var nomRol = "";

            switch (rol)
            {
                case 0:
                    nomRol = filtroDashBoard.Administrador.ToString();
                    break;
                case 1:
                    nomRol = filtroDashBoard.Administrador.ToString();
                    break;
                case 2:
                    nomRol = filtroDashBoard.Despacho.ToString();
                    break;
                case 3:
                    nomRol = filtroDashBoard.Supervisor.ToString();
                    break;
                case 4:
                    nomRol = filtroDashBoard.Gestor.ToString();
                    break;
                case 5:
                    nomRol = filtroDashBoard.Delegacion.ToString();
                    break;
                case 6:
                    nomRol = filtroDashBoard.Direccion.ToString();
                    break;
                default:
                    break;
            }

            ViewBag.Delegacion = routeParams.Delegacion;
            ViewBag.Despacho = routeParams.Despacho;
            ViewBag.Supervisor = routeParams.Supervisor;
            ViewBag.Gestor = routeParams.Gestor;
            ViewBag.rol = nomRol;
            return PartialView(model);
        }

    }
}
