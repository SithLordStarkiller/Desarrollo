using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PubliPayments.App_Code;
using PubliPayments.Entidades;
using PubliPayments.Reportes.ReporteAuditoriaImagenes;
using PubliPayments.Reportes.ReporteRankingIndicador;

namespace PubliPayments.Reportes.ReporteTelefonosRepetidos
{
    public partial class ReporteTelefonosRepetidos : System.Web.UI.Page
    {
        private string _tipoDashboard;
        private string _tipoReporte;
        private string _supervisor;
        private string _despacho;
        private string _delegacion;
        private string _nomDelegacion;
        private string _nomDespacho;
        private string _nomSupervisor;
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


            _tipoDashboard = Request.QueryString["tD"];
            _delegacion = Request.QueryString["del"];
            _despacho = Request.QueryString["desp"];
            _supervisor = Request.QueryString["spv"];
            _tipoReporte = Request.QueryString["mstr"];
            _nomDelegacion = Request.QueryString["nomDelegacion"];
            _nomDespacho = Request.QueryString["nomDespacho"];
            _nomSupervisor = Request.QueryString["nomSupervisor"];
            _tipoFormulario = Request.QueryString["tpFrm"];
            _tipoFormularioNombre = Request.QueryString["tipoFormulario"];
            
            if (tpDashActual != _tipoDashboard)
            {
                Response.Redirect("~/unauthorized.aspx");
            }

            switch (_tipoReporte)
            {
                case "MasterTelRepetido":
                    if (_tipoDashboard != "Administrador" && _tipoDashboard != "Supervisor")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "scriptCierre", "<script>alert('No se pudo cargar el reporte.');this.close();</script>", false);
                    }
                    ReportViewer1.Report = ObtenerMasterTelRepetido();
                    break;
                default:
                    ClientScript.RegisterStartupScript(GetType(), "scriptCierre", "<script>alert('No se pudo cargar el reporte.');this.close();</script>", false);
                    break;
            }
        }

        TelefonosRepetidos ObtenerMasterTelRepetido()
        {
            var report = new TelefonosRepetidos(_supervisor, _despacho,  _delegacion, _tipoFormulario);

            var filtros = "Tipo Formulario: " + _tipoFormularioNombre;
            if (_delegacion != "" && _delegacion != "9999")
            {
                filtros += ("   Delegacion: " + _nomDelegacion);
            }
            if (_despacho != "" && _despacho != "9999")
            {
                filtros += ("   Despacho: " + _nomDespacho);
            }
            if (_supervisor != "" && _supervisor != "9999")
            {
                filtros += ("   Supervisor: " + _nomSupervisor);
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