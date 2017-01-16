using System;
using PubliPayments.App_Code;

namespace PubliPayments.Reportes.ReporteGestionMovil
{
    public partial class ReporteGestionMovil : System.Web.UI.Page
    {
        private string _fechaCarga;
        private string _resFinal;
        private string _estFinal;
        private string _diaGestion;
        private string _horaGestion;
        private string _tipoDashboard;
        private string _despacho;
        private string _supervisor;
        private string _tipoReporte;
        private string _delegacion;
        private string _delegacionNombre;
        private string _estatusFinal;
        private string _tipoFormulario;
        private string _tipoFormularioNombre;

        protected void Page_Load(object sender, EventArgs e)
        {

            string tpDashActual = "";
            switch (Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol)))
            {
                case 0: tpDashActual = filtroDashBoard.Administrador.ToString(); break;
                case 1: tpDashActual = filtroDashBoard.Administrador.ToString(); break;
                case 2: tpDashActual = filtroDashBoard.Despacho.ToString(); break;
                case 3: tpDashActual = filtroDashBoard.Supervisor.ToString(); break;
                case 4: tpDashActual = filtroDashBoard.Gestor.ToString(); break;
                case 5: tpDashActual = filtroDashBoard.Delegacion.ToString(); break;
                case 6: tpDashActual = filtroDashBoard.Direccion.ToString(); break;
            }

            _fechaCarga = Request.QueryString["fCar"];
            _resFinal = Request.QueryString["resF"];
            _estFinal = Request.QueryString["estF"];
            _diaGestion = Request.QueryString["dGes"];
            _horaGestion = Request.QueryString["hGes"];
            _tipoDashboard = Request.QueryString["tD"];
            _despacho = Request.QueryString["desp"];
            _supervisor = Request.QueryString["spv"];
            _tipoReporte = Request.QueryString["mstr"];
            _delegacion = Request.QueryString["del"];
            _delegacionNombre = Request.QueryString["delegacion"];
            _estatusFinal = Request.QueryString["estatusFinal"];
            _tipoFormulario = Request.QueryString["tpFrm"];
            _tipoFormularioNombre = Request.QueryString["tipoFormulario"];


            
            if (tpDashActual != _tipoDashboard)
            {
                Response.Redirect("~/unauthorized.aspx");
            }
            

            switch (_tipoReporte)
            {
                case "GestionXHora":
                    ReportViewer1.Report = ObtenerGestionXHora();
                    break;
                case "GestionXDia":
                    ReportViewer1.Report = ObtenerGestionXDia();
                    break;
                case "Soluciones":
                    ReportViewer1.Report = ObtenerSoluciones();
                    break;
                case "GestionXSolucion":
                    ReportViewer1.Report = ObtenerGestionXSolucion();
                    break;
                default:
                    ClientScript.RegisterStartupScript(GetType(), "scriptCierre", "<script>alert('No se pudo cargar el reporte.');this.close();</script>", false);
                    break;
            }
        }

        GestionXHora ObtenerGestionXHora()
        {
            var report = new GestionXHora(_delegacion, _fechaCarga, _resFinal, _diaGestion, _despacho, _supervisor, _tipoFormulario);
            var filtros = "Tipo Formulario: " + _tipoFormularioNombre;
            if (_delegacion != "")
            {
                filtros += ("   Delegacion: " + _delegacionNombre);
            }
            if (_fechaCarga != "")
            {
                filtros += ("   Fecha de carga: " + _fechaCarga);
            }
            if (_resFinal != "")
            {
                filtros += ("   Respuesta final: " + _resFinal);
            }
            if (_diaGestion != "")
            {
                filtros += ("   Dia Gestion: " + (_diaGestion == "null" ? "Sin fecha de gestion" : _diaGestion));
            }
            report.FindControl("labelFiltros",false).Text = filtros;
            return report;
        }

        GestionXDia ObtenerGestionXDia()
        {
            var report = new GestionXDia(_delegacion, _fechaCarga, _resFinal, _horaGestion,_despacho,_supervisor, _tipoFormulario);
            var filtros = "Tipo Formulario: " + _tipoFormularioNombre;
            if (_delegacion != "")
            {
                filtros += ("   Delegacion: " + _delegacionNombre);
            }
            if (_fechaCarga != "")
            {
                filtros += ("   Fecha de carga: " + _fechaCarga);
            }
            if (_resFinal != "")
            {
                filtros += ("   Respuesta final: " + _resFinal);
            }
            if (_horaGestion != "")
            {
                filtros += ("   Hora Gestion: " + (_horaGestion == "null" ? "Sin fecha de gestion" : _horaGestion));
            }
            report.FindControl("labelFiltros", false).Text = filtros;
            return report;
        }

        Soluciones ObtenerSoluciones()
        {
            var report = new Soluciones(_delegacion, _fechaCarga, _despacho, _supervisor, _tipoFormulario);
            var filtros = "Tipo Formulario: " + _tipoFormularioNombre;
            if (_delegacion != "")
            {
                filtros += ("   Delegacion: " + _delegacionNombre);
            }
            if (_fechaCarga != "")
            {
                filtros += ("   Fecha de carga: " + _fechaCarga);
            }
            report.FindControl("labelFiltros", false).Text = filtros;
            return report;
        }

        GestionXSolucion ObtenerGestionXSolucion()
        {
            var report = new GestionXSolucion(_delegacion, _fechaCarga, _estFinal, _despacho, _supervisor, _tipoFormulario);
            var filtros = "Tipo Formulario: " + _tipoFormularioNombre;
            if (_delegacion != "")
            {
                filtros += ("   Delegacion: " + _delegacionNombre);
            }
            if (_fechaCarga != "")
            {
                filtros += ("   Fecha de carga: " + _fechaCarga);
            }
            if (_estFinal != "")
            {
                filtros += ("   Estatus Final: " + _estatusFinal);
            }
            report.FindControl("labelFiltros", false).Text = filtros;
            return report;
        }

        protected void ReportViewer1_Unload(object sender, EventArgs e)
        {
            ReportViewer1.Report = null;
        }
    }
}