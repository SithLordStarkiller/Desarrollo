using System;
using System.Globalization;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using proUtilerias;


public partial class Reportes_frmEstadoFuerzaHomologado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        clsCatalogos.llenarCatalogoZona(ddlZona, "catalogo.spLeerZona", "zonDescripcion", "idZona", (clsEntSesion)Session["objSession" + Session.SessionID]);
    }



    protected void btnGenerar_Click(object sender, EventArgs e)
    {

        if (ddlZona.SelectedItem.Value.Length == 0)
        {
            Session["objZona" + Session.SessionID] = "0";
        }
        else
        {
            Session["objZona" + Session.SessionID] = ddlZona.SelectedItem.Value;
        }
        Response.Redirect("~/Reportes/frmRvEstadoFuerzaHomologado.aspx");
    }
}