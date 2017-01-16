using System;
using System.Linq;
using System.Web.UI;
using PubliPayments.App_Code;
using PubliPayments.Entidades;

namespace PubliPayments.Reportes.ReporteRankingIndicador
{
    public partial class ReporteRanking : Page
    {
        private string _tipoDashboard;
        private string _indicador;
        private string _tipoReporte;
        private string _supervisor;
        private string _despacho;
        private string _tituloReporte;
        private string _delegacion;
        private string _tipoFormulario;
        private string _tipoFormularioNombre;

        protected void Page_Load(object sender, EventArgs e)
        {
            string tpDashActual="";
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
            _indicador = Request.QueryString["ind"];
            _despacho = Request.QueryString["desp"];
            _supervisor = Request.QueryString["spv"];
            _tipoReporte = Request.QueryString["mstr"];
            _tituloReporte = Request.QueryString["ttl"];
            _delegacion = Request.QueryString["del"];
            _tipoFormulario=Request.QueryString["tpFrm"];
            _tipoFormularioNombre=Request.QueryString["tipoFormulario"];
            

            if (tpDashActual != _tipoDashboard)
            {
                Response.Redirect("~/unauthorized.aspx");
            }

            switch (_tipoReporte)
            {
                case "MasterDespachos":
                    if (_tipoDashboard != "Administrador")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "scriptCierre", "<script>alert('No se pudo cargar el reporte.');this.close();</script>", false);
                    }
                    ReportViewer1.Report = ObtenerMasterDespachos();
                break;
                case "MasterDelegacion":
                    if (_tipoDashboard != "Administrador")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "scriptCierre", "<script>alert('No se pudo cargar el reporte.');this.close();</script>", false);
                    }
                    ReportViewer1.Report = ObtenerMasterDelegacion();
                break;
                case "MasterDelegacionDespacho":
                    if (_tipoDashboard != "Despacho")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "scriptCierre", "<script>alert('No se pudo cargar el reporte.');this.close();</script>", false);
                    }
                    ReportViewer1.Report = ObtenerMasterDelegacionDespacho();
                break;
                case "MasterGestor":
                    if (_tipoDashboard != "Supervisor")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "scriptCierre", "<script>alert('No se pudo cargar el reporte.');this.close();</script>", false);
                    }
                    ReportViewer1.Report = ObtenerMasterGestor();
                break;
                case "MasterDespachos_Delegacion":
                    if (_tipoDashboard != "Delegacion")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "scriptCierre", "<script>alert('No se pudo cargar el reporte.');this.close();</script>", false);
                    }
                    ReportViewer1.Report = ObtenerMasterDespachos_Delegacion();
                    break;
                default:
                    ClientScript.RegisterStartupScript(GetType(),"scriptCierre","<script>alert('No se pudo cargar el reporte.');this.close();</script>",false);
                    break;
            }

        }

        MasterDespachos ObtenerMasterDespachos()
        {
            var report = new MasterDespachos();
            var lista = new EntRankingIndicadores().ObtenerTablaDespachos(_tipoDashboard, _indicador,_tipoFormulario);
            report.FindControl("tipoDashboardParam", false).Text = _tipoDashboard;
            report.FindControl("indicadorParam", false).Text = _indicador;
            report.FindControl("tipoFormularioParam", false).Text = _tipoFormulario;
            var indicadorTitulo = "";
            indicadorTitulo += "Indicador: "+_tituloReporte;
            indicadorTitulo += "       Tipo Formulario: " + _tipoFormularioNombre;
            report.FindControl("LabelIndicadorTitulo", false).Text = indicadorTitulo;
            report.DataSource = lista;
            return report;
        }

        MasterDelegacion ObtenerMasterDelegacion()
        {
            var report = new MasterDelegacion();
            var lista = new EntRankingIndicadores().ObtenerDelegaciones(_tipoDashboard, _indicador,_tipoFormulario);
            report.FindControl("tipoDashboardParam", false).Text = _tipoDashboard;
            report.FindControl("indicadorParam", false).Text = _indicador;
            report.FindControl("tipoFormularioParam", false).Text = _tipoFormulario;
            var indicadorTitulo = "";
            indicadorTitulo += "Indicador: " + _tituloReporte;
            indicadorTitulo += "       Tipo Formulario: " + _tipoFormularioNombre;
            report.FindControl("LabelIndicadorTitulo", false).Text = indicadorTitulo;
            report.DataSource = lista;
            return report;
        }

        MasterDelegacionDespacho ObtenerMasterDelegacionDespacho()
        {
            var report = new MasterDelegacionDespacho();
            var lista = new EntRankingIndicadores().ObtenerTablaDelegaciones(_tipoDashboard, _indicador, _despacho,100,_tipoFormulario).Where(e => e.Valor != 0);
            report.FindControl("tipoDashboardParam", false).Text = _tipoDashboard;
            report.FindControl("indicadorParam", false).Text = _indicador;
            report.FindControl("despachoParam", false).Text = _despacho;
            report.FindControl("tipoFormularioParam", false).Text = _tipoFormulario;
            var indicadorTitulo = "";
            indicadorTitulo += "Indicador: " + _tituloReporte;
            indicadorTitulo += "       Tipo Formulario: " + _tipoFormularioNombre;
            report.FindControl("LabelIndicadorTitulo", false).Text = indicadorTitulo;
            report.DataSource = lista;
            return report;
        }

        MasterGestor ObtenerMasterGestor()
        {
            var report = new MasterGestor();
            var lista = new EntRankingIndicadores().ObtenerTablaSupervisorValor(_tipoDashboard, _indicador, _despacho, _supervisor,100,_tipoFormulario);
            report.FindControl("despachoParam", false).Text = _despacho;
            report.FindControl("tipoDashboardParam", false).Text = _tipoDashboard;
            report.FindControl("indicadorParam", false).Text = _indicador;
            report.FindControl("tipoFormularioParam", false).Text = _tipoFormulario;
            var indicadorTitulo = "";
            indicadorTitulo += "Indicador: " + _tituloReporte;
            indicadorTitulo += "       Tipo Formulario: " + _tipoFormularioNombre;
            report.FindControl("LabelIndicadorTitulo", false).Text = indicadorTitulo;
            report.DataSource = lista;
            return report;
        }

        MasterDespachos_Delegacion ObtenerMasterDespachos_Delegacion()
        {
            var report = new MasterDespachos_Delegacion();
            var lista = new EntRankingIndicadores().ObtenerTablaDespachosDelegacion(_tipoDashboard, _indicador, _delegacion,100,_tipoFormulario).Where(e => e.Valor != 0);
            report.FindControl("tipoDashboardParam", false).Text = _tipoDashboard;
            report.FindControl("indicadorParam", false).Text = _indicador;
            report.FindControl("delegacionParam", false).Text = _delegacion;
            report.FindControl("tipoFormularioParam", false).Text = _tipoFormulario;
            var indicadorTitulo = "";
            indicadorTitulo += "Indicador: " + _tituloReporte;
            indicadorTitulo += "       Tipo Formulario: " + _tipoFormularioNombre;
            report.FindControl("LabelIndicadorTitulo", false).Text = indicadorTitulo;
            report.DataSource = lista;
            return report;
        }

        protected void ReportViewer1_Unload(object sender, EventArgs e)
        {
            ReportViewer1.Report = null;
        }
    }
}