using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PubliPayments.App_Code;
using PubliPayments.Entidades;
using PubliPayments.Reportes.ReporteRankingIndicador;

namespace PubliPayments.Reportes.ReporteAuditoriaImagenes
{
    public partial class Auditoria : System.Web.UI.Page
    {
        private string _tipoDashboard;
        private string _gestor;
        private string _tipoReporte;
        private string _supervisor;
        private string _despacho;
        private string _dictamen;
        private string _status;
        private string _delegacion;
        private string _nomDelegacion;
        private string _nomDespacho;
        private string _nomSupervisor;
        private string _nomGestor;
        private string _nomDictamen;
        private string _tipoFormulario;
        private string _tipoFormularioNombre;
        private string _autorizacion;
        private string _autorizacionNombre;
        private string _conexionBd;
        private string _valorOcr;
        

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
            _gestor = Request.QueryString["gst"];
            _dictamen = Request.QueryString["dct"];
            _status = Request.QueryString["stt"];
            _tipoReporte = Request.QueryString["mstr"];
            _nomDelegacion = Request.QueryString["nomDelegacion"];
            _nomDespacho = Request.QueryString["nomDespacho"];
            _nomSupervisor = Request.QueryString["nomSupervisor"];
            _nomGestor = Request.QueryString["nomGestor"];
            _nomDictamen = Request.QueryString["nomDictamen"];
            _tipoFormulario = Request.QueryString["tpFrm"];
            _tipoFormularioNombre = Request.QueryString["tipoFormulario"];
            _autorizacion = Request.QueryString["aut"];
            _autorizacionNombre = Request.QueryString["nomAutorizacion"];
            _conexionBd = Request.QueryString["conexionBd"];
            _valorOcr = Request.QueryString["valorOcr"];
            

            if (tpDashActual != _tipoDashboard)
            {
                Response.Redirect("~/unauthorized.aspx");
            }

            switch (_tipoReporte)
            {
                case "MasterAuditoria":
                    if (_tipoDashboard != "Administrador" && _tipoDashboard != "Delegacion")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "scriptCierre", "<script>alert('No se pudo cargar el reporte.');this.close();</script>", false);
                    }
                    ReportViewer1.Report = ObtenerMasterAuditoria();
                    break;
               default:
                    ClientScript.RegisterStartupScript(GetType(), "scriptCierre", "<script>alert('No se pudo cargar el reporte.');this.close();</script>", false);
                    break;
            }
        }

        AuditoriaImagenes ObtenerMasterAuditoria()
        {
            var report = new AuditoriaImagenes(_gestor, _supervisor, _despacho, _dictamen, _status, _delegacion, _tipoFormulario, _autorizacion, _valorOcr, _conexionBd);
            
            var filtros = "Tipo Formulario: "+_tipoFormularioNombre;
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
            if (_gestor != "" && _gestor != "9999")
            {
                filtros += ("   Gestor: " + _nomGestor);
            }
            if (_dictamen != "" && _dictamen != "9999")
            {
                filtros += ("   Dictamen: " + _nomDictamen);
            }
            if (_autorizacion != "" && _autorizacion != "9999")
            {
                filtros += ("   Autorizacion: " + _autorizacionNombre);
            }
            if (_status != "" && _status != "9999")
            {
                filtros += ("   Status: " + (_status == "0" ? "No Autorizado" : _status == "1" ? "Autorizado" : "Sin Auditar"));
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