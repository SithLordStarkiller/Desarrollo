using System;
using System.Globalization;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using proUtilerias;

public partial class Reportes_frmIncidenciasPaseLista : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            covFecha.ValueToCompare = DateTime.Now.ToShortDateString();
        }
    }



    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        clsEntEstadoFuerza objEstadoFuerza = new clsEntEstadoFuerza();
        objEstadoFuerza.FechaReporte = clsUtilerias.dtObtenerFecha(txbFecha.Text);


        Session["objEstadoFuerza" + Session.SessionID] = objEstadoFuerza;
        Response.Redirect("~/Reportes/frmRvIncidenciasPaseLista.aspx");
    }
}