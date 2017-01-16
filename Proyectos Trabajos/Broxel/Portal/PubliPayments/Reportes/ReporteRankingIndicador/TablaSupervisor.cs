using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraReports.UI;
using PubliPayments.Entidades;

namespace PubliPayments.Reportes.ReporteRankingIndicador
{
    public partial class TablaSupervisor : XtraReport
    {
        private string _tipoDashboard;
        private string _indicador;
        private string _despacho;
        private string _delegacion;
        private string _tipoFormulario;

        public TablaSupervisor()
        {
            InitializeComponent();
            cabeceraTabla.BackColor = Color.FromArgb(190, 190, 190);
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                _tipoDashboard = tipoDashboardParam.Text;
                _indicador = indicadorParam.Text;
                _despacho = despachoParam.Text;
                _delegacion = delegacionParam.Text;
                _tipoFormulario = tipoFormularioParam.Text;
                int valor = Convert.ToInt32(GetCurrentColumnValue("Valor"));
                var lista = new EntRankingIndicadores().ObtenerTablaGestoresDelegacion(_tipoDashboard, _indicador,
                    _despacho, GetCurrentColumnValue("Identificador").ToString(), _delegacion, valor,_tipoFormulario);

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
