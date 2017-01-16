using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace PubliPayments.Reportes.ReporteRankingIndicador
{
    public partial class TablaGestor : DevExpress.XtraReports.UI.XtraReport
    {
        public TablaGestor()
        {
            InitializeComponent();
            cabeceraTabla.BackColor = Color.FromArgb(220, 220, 220);
        }

    }
}
