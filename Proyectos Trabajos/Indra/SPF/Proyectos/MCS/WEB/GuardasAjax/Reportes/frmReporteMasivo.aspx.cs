using System;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using proUtilerias;

public partial class Reportes_frmReporteMasivo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
            clsEntAsignacionMasiva objReporte = new clsEntAsignacionMasiva();
            objReporte.fechaIngreso = Convert.ToDateTime(txbFechaInicio.Text);
            objReporte.fechaBaja = Convert.ToDateTime(txbFechaFin.Text);

            Session["objReporteRevision" + Session.SessionID] = objReporte;
            Response.Redirect("~/Reportes/frmRvReporteMasivo.aspx");
    }
}