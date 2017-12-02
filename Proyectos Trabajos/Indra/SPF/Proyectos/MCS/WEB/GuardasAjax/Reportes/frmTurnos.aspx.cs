using System;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using proUtilerias;

public partial class Reportes_frmTurnos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (IsPostBack) return;
        clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuario", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
    }

    protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlServicio.SelectedIndex > 0)
        {
            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacion, "catalogo.spLeerInstalacionesPorUsuario", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicio.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlInstalacion.Enabled = true;
            return;
        }
        ddlInstalacion.Items.Clear();
        ddlInstalacion.Enabled = false;
    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {

        clsEntReporteTurno objReporteTurno = new clsEntReporteTurno();
        objReporteTurno.idServicio = string.IsNullOrEmpty(ddlServicio.SelectedValue) ? 0 : Convert.ToInt32(ddlServicio.SelectedValue);
        objReporteTurno.idInstalacion = string.IsNullOrEmpty(ddlInstalacion.SelectedValue) ? 0 : Convert.ToInt32(ddlInstalacion.SelectedValue);
        objReporteTurno.intMes = string.IsNullOrEmpty(ddlMes.SelectedValue) ? 0 : Convert.ToInt32(ddlMes.SelectedValue);
        objReporteTurno.intAnio = string.IsNullOrEmpty(ddlAnio.SelectedValue) ? 0 : Convert.ToInt32(ddlAnio.SelectedValue);
        objReporteTurno.strContratante = txbContratante.Text;
        objReporteTurno.strSPF = txbSPF.Text;
        objReporteTurno.strObservaciones = txbObservaciones.Text;
       

        Session["objReporteTurno" + Session.SessionID] = objReporteTurno;
        Response.Redirect("~/Reportes/frmRvTurnos.aspx");
    }
}