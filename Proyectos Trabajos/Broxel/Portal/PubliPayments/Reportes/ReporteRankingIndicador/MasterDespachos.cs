using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.XtraReports.UI;
using PubliPayments.Entidades;

namespace PubliPayments.Reportes.ReporteRankingIndicador
{
    public partial class MasterDespachos : XtraReport
    {
        private string _tipoDashboard;
        private string _indicador;
        private string _tipoFormulario;

        public MasterDespachos()
        {
            InitializeComponent();
            xrPanel1.BackColor = Color.FromArgb(65,51,57);
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
                    _tipoFormulario = tipoFormularioParam.Text;
                    var lista =
                        new EntRankingIndicadores().ObtenerTablaDelegaciones(_tipoDashboard, _indicador,
                            GetCurrentColumnValue("Identificador").ToString(),
                            Convert.ToInt32(GetCurrentColumnValue("Valor")),_tipoFormulario).Where(dato => dato.Valor != 0);
                    ((XRSubreport) sender).ReportSource.FindControl("tipoDashboardParam", false).Text = _tipoDashboard;
                    ((XRSubreport) sender).ReportSource.FindControl("indicadorParam", false).Text = _indicador;
                    ((XRSubreport)sender).ReportSource.FindControl("tipoFormularioParam", false).Text = _tipoFormulario;
                    ((XRSubreport) sender).ReportSource.FindControl("despachoParam", false).Text =
                        GetCurrentColumnValue("Identificador").ToString();
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
