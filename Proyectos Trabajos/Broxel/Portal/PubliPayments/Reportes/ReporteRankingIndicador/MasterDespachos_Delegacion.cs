using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraReports.UI;
using PubliPayments.Entidades;

namespace PubliPayments.Reportes.ReporteRankingIndicador
{
    public partial class MasterDespachos_Delegacion : XtraReport
    {
        private string _tipoDashboard;
        private string _indicador;
        private string _delegacion;
        private string _tipoFormulario;

        public MasterDespachos_Delegacion()
        {
            InitializeComponent();
            xrPanel1.BackColor = Color.FromArgb(65, 51, 57);
            cabeceraTabla.BackColor = Color.FromArgb(130, 130, 130);
            cabeceraTabla.ForeColor = Color.FromArgb(255, 255, 255);
            switch (Config.AplicacionActual().idAplicacion)
            {
                case 1:
                    xrLabel5.Text = @"Instituto del Fondo Nacional de la Vivienda para los Trabajadores (Infonavit)";
                    xrLabel2.Text = @"  ©2013  -  ";
                    xrPictureBox3.Visible = true;
                    break;
                default:
                    xrLabel5.Text = "";
                    xrLabel2.Text = "";
                    xrPictureBox3.Visible = false;
                    break;
            }
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
                    despacho, _delegacion, valor,_tipoFormulario); //.Where(dato => dato.Valor != 0);
                ((XRSubreport) sender).ReportSource.FindControl("tipoDashboardParam", false).Text = _tipoDashboard;
                ((XRSubreport) sender).ReportSource.FindControl("indicadorParam", false).Text = _indicador;
                ((XRSubreport)sender).ReportSource.FindControl("tipoFormularioParam", false).Text = _tipoFormulario;
                ((XRSubreport) sender).ReportSource.FindControl("despachoParam", false).Text =
                    GetCurrentColumnValue("Identificador").ToString();
                ((XRSubreport) sender).ReportSource.FindControl("delegacionParam", false).Text = _delegacion;
                ((XRSubreport) sender).ReportSource.DataSource = lista;
            }
            catch (Exception)
            {
                var lista = new List<ResultadosUsuariosRankingModel>();
                ((XRSubreport) sender).ReportSource.DataSource = lista;
            }
        }

    }
}
