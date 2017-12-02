using System;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using proUtilerias;
using SICOGUA.Datos;


public partial class Reportes_frmPersonalAsignadoHistorico : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        ddlAnio.SelectedItem.Value = "0";
        ddlMes.SelectedItem.Value = "0";
    }

    

    protected void btnGenerar_Click(object sender, EventArgs e)
    {

        if (ddlMes.SelectedItem.Value == "0" )
        {
            lblMes.Text = "Es necesario seleccionar el mes";
            return;
        }
        if (ddlAnio.SelectedItem.Value == "0")
        {
            lblAnnio.Text = "Es necesario seleccionar el año";
            return;
        }
       

        Session["intMes" + Session.SessionID] = ddlMes.SelectedItem.Value;
        Session["intAnio" + Session.SessionID] = ddlAnio.SelectedItem.Value;
        Response.Redirect("~/Reportes/frmRvPersonalAsignadoHistorico.aspx");
    }
    protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMes.Text = "";
        if (lblAnnio.Text.Length == 0 && lblMes.Text.Length == 0)
        {
            lblFechaCorte.Text = clsDatCatalogos.consultarFechaCorte(Convert.ToInt32(ddlMes.SelectedItem.Value), Convert.ToInt32(ddlAnio.SelectedItem.Value), (clsEntSesion)Session["objSession" + Session.SessionID]);
        }
    }
    protected void ddlAnio_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblAnnio.Text = "";
    }
}