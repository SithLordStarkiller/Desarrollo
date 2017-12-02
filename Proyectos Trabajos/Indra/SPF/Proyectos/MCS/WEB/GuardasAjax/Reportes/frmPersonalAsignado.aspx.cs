using System;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using proUtilerias;

public partial class Reportes_frmPersonalAsignado : System.Web.UI.Page
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
        clsEntReporteListado objReporteListado = new clsEntReporteListado();
        objReporteListado.Servicio = new clsEntServicio();
        objReporteListado.Instalacion = new clsEntInstalacion();

        objReporteListado.FechaReporte = clsUtilerias.dtObtenerFecha(txbFecha.Text);
        objReporteListado.Servicio.idServicio = string.IsNullOrEmpty(ddlServicio.SelectedValue) ? 0 : Convert.ToInt32(ddlServicio.SelectedValue);
        objReporteListado.Instalacion.IdInstalacion = string.IsNullOrEmpty(ddlInstalacion.SelectedValue) ? 0 : Convert.ToInt32(ddlInstalacion.SelectedValue);

        Session["objPersonalAsignado" + Session.SessionID] = objReporteListado;
        Response.Redirect("~/Reportes/frmRvPersonalAsignado.aspx");
    }
}