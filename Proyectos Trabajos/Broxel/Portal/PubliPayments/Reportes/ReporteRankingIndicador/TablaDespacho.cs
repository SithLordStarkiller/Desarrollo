using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraReports.UI;
using PubliPayments.Entidades;

namespace PubliPayments.Reportes.ReporteRankingIndicador
{
    public partial class TablaDespacho : XtraReport
    {
        private string _tipoDashboard;
        private string _indicador;
        private string _delegacion;
        private string _tipoFormulario;

        public TablaDespacho()
        {
            InitializeComponent();
            cabeceraTabla.BackColor = Color.FromArgb(160, 160, 160);
            cabeceraTabla.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                _tipoDashboard = tipoDashboardParam.Text;
                _indicador = indicadorParam.Text;
                _delegacion = delegacionParam.Text;
                _tipoFormulario = tipoFormularioParam.Text;
                string despacho = GetCurrentColumnValue("Identificador").ToString();
                int valor = Convert.ToInt32(GetCurrentColumnValue("Valor"));
                var lista = new EntRankingIndicadores().ObtenerTablaSupervisoresDelegacion(_tipoDashboard, _indicador,
                    despacho, _delegacion, valor,_tipoFormulario);
                ((XRSubreport) sender).ReportSource.FindControl("tipoDashboardParam", false).Text = _tipoDashboard;
                ((XRSubreport) sender).ReportSource.FindControl("indicadorParam", false).Text = _indicador;
                ((XRSubreport) sender).ReportSource.FindControl("despachoParam", false).Text = despacho;
                ((XRSubreport)sender).ReportSource.FindControl("tipoFormularioParam", false).Text = _tipoFormulario;
                ((XRSubreport) sender).ReportSource.FindControl("delegacionParam", false).Text = _delegacion;
                ((XRSubreport) sender).ReportSource.DataSource = lista;
            }
            catch (Exception)
            {
                var lista = new List<ResultadosUsuariosRankingModel>();
                ((XRSubreport)sender).ReportSource.DataSource = lista;
            }

        }

    }
}
