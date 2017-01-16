using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PubliPayments.App_Code;
using PubliPayments.Entidades;
using PubliPayments.Models;
using PubliPayments.Negocios;

namespace PubliPayments.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.LblTitulo_Dash = " Resumen de Actividad Plataforma de Gestión Móvil - ";

            var rol = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
            switch (rol)
            {
                case 0:
                    ViewBag.LblTitulo_Dash += "Administrador";
                    break;
                case 1:
                    ViewBag.LblTitulo_Dash += "Administrador";
                    break;
                case 2:
                    ViewBag.LblTitulo_Dash = "Despacho";
                    break;
                case 3:
                    ViewBag.LblTitulo_Dash = " Supervisor";
                    break;
                case 4:
                    ViewBag.LblTitulo_Dash = "Gestor";
                    break;
                case 5:
                    ViewBag.LblTitulo_Dash = "Delegación";
                    break;
                case 6:
                    ViewBag.LblTitulo_Dash = "Dirección";
                    break;
                default:
                    return Redirect("unauthorized.aspx");
            }
            return View();
        }

        public PartialViewResult ContenidoDashboard()
        {

            return PartialView();
        }

        public ActionResult IndicadoresSuperior()
        {
            var routeParams = new ValoresFiltros
            {
                Delegacion = "%",
                Despacho = "%",
                Supervisor = "%",
                Gestor = "%",
                TipoFormulario =
                    CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).IdFormulario.ToString()
            };

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

            ViewData["fecha"] = DateTime.Today.Date.ToShortDateString();

            var negocio = new NegocioDashboard();

            var datosInd =
                negocio.ObtenerIndiciadoresDashboard(
                    SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario), nomRol, 0,
                    routeParams.Delegacion, routeParams.Despacho, routeParams.Supervisor, routeParams.Gestor,
                    routeParams.TipoFormulario, Config.EsCallCenter.ToString(), rol);
            //var datosInd = EntDashboard.Dashboard(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario), nomRol, 0, routeParams.Delegacion, routeParams.Despacho, routeParams.Supervisor, routeParams.Gestor, routeParams.TipoFormulario, Config.EsCallCenter.ToString()).Tables[0];

            return PartialView(datosInd);
        }

        public ActionResult IndicadoresInferior()
        {
            var routeParams = new ValoresFiltros
            {
                Delegacion = "%",
                Despacho = "%",
                Supervisor = "%",
                Gestor = "%",
                TipoFormulario =
                    CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).IdFormulario.ToString()
            };

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

            var negocio = new NegocioDashboard();

            var datosInd1 =
                negocio.ObtenerIndiciadoresDashboard(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario), nomRol, 1,
                    routeParams.Delegacion, routeParams.Despacho, routeParams.Supervisor, routeParams.Gestor,
                    routeParams.TipoFormulario, Config.EsCallCenter.ToString(), rol);
            var datosInd2 =
                negocio.ObtenerIndiciadoresDashboard(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario), nomRol, 2,
                    routeParams.Delegacion, routeParams.Despacho, routeParams.Supervisor, routeParams.Gestor,
                    routeParams.TipoFormulario, Config.EsCallCenter.ToString(), rol);

            //var datosInd1 = EntDashboard.Dashboard(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario), nomRol, 1, routeParams.Delegacion, routeParams.Despacho, routeParams.Supervisor, routeParams.Gestor, routeParams.TipoFormulario, Config.EsCallCenter.ToString()).Tables[0];
            //var datosInd2 = EntDashboard.Dashboard(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario), nomRol, 2, routeParams.Delegacion, routeParams.Despacho, routeParams.Supervisor, routeParams.Gestor, routeParams.TipoFormulario, Config.EsCallCenter.ToString()).Tables[0];

            foreach (DataRow row in datosInd2.Rows)
            {
                var rowAux = datosInd1.NewRow();
                rowAux.ItemArray = row.ItemArray;
                datosInd1.Rows.Add(rowAux);
            }

            return PartialView(datosInd1);
        }

        public String ObtenerDatosDashboard(ValoresFiltros routeParams)
        {
            if (routeParams == null)
            {
                return "";
            }

            var rol = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
            var nomRol = "";
            switch (rol)
            {
                case 0: nomRol = filtroDashBoard.Administrador.ToString(); break;
                case 1: nomRol = filtroDashBoard.Administrador.ToString(); break;
                case 2: nomRol = filtroDashBoard.Despacho.ToString(); break;
                case 3: nomRol = filtroDashBoard.Supervisor.ToString(); break;
                case 4: nomRol = filtroDashBoard.Gestor.ToString(); break;
                case 5: nomRol = filtroDashBoard.Delegacion.ToString(); break;
                case 6: nomRol = filtroDashBoard.Direccion.ToString(); break;
                default:
                    break;
            }
            var datosInd1 =
                new EntDashboard().Dashboard(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario), nomRol, -1,
                    routeParams.Delegacion, routeParams.Despacho, routeParams.Supervisor, routeParams.Gestor,
                    routeParams.TipoFormulario, Config.EsCallCenter.ToString(), rol).Tables[0];

            var result = (from DataRow row in datosInd1.Rows
                          select new IndicadorDashboard
                          {
                              FcClave = row["Descripcion"].ToString(),
                              FiValue = Convert.ToInt32(row["Valor"]),
                              FiPorcentaje = row["Porcentaje"].ToString(),
                              FcDescripcion = row["NombreDisplay"].ToString(),
                              FiParte = Convert.ToInt32(row["Parte"])
                          }).ToList();

            var result2 = new JavaScriptSerializer().Serialize(result);
            return result2;
        }
    }
}
