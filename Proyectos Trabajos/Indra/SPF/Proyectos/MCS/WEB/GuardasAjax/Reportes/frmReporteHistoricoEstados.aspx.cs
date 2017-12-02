using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using proUtilerias;

public partial class Reportes_frmReporteHistoricoEstados : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        clsEntReporteListado objReporteListado = new clsEntReporteListado();
        Session["objMes" + Session.SessionID] = ddlMes.SelectedItem.Value;
        Session["objAnio" + Session.SessionID] = ddlAnio.SelectedItem.Value;
        Response.Redirect("~/Reportes/frmRvReporteHistoricoEstados.aspx");
    }
}