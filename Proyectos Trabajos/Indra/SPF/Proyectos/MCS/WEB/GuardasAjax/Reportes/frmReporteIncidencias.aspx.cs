using System;
using System.Globalization;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using proUtilerias;

public partial class Reportes_frmReporteIncidencias : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuario", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            //clsCatalogos.llenarCatalogoServicio(ddlServicio, "catalogo.spLeerServicio", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            covFecha.ValueToCompare = DateTime.Now.ToShortDateString();
        }
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
        clsEntReporteListado objReporteIncidencias = new clsEntReporteListado();
        objReporteIncidencias.Servicio = new clsEntServicio();
        objReporteIncidencias.Instalacion = new clsEntInstalacion();

        objReporteIncidencias.FechaReporte = clsUtilerias.dtObtenerFecha(txbFecha.Text);
        objReporteIncidencias.Servicio.idServicio = string.IsNullOrEmpty(ddlServicio.SelectedValue) ? 0 : Convert.ToInt32(ddlServicio.SelectedValue);
        objReporteIncidencias.Instalacion.IdInstalacion = string.IsNullOrEmpty(ddlInstalacion.SelectedValue) ? 0 : Convert.ToInt32(ddlInstalacion.SelectedValue);

        Session["objReporteIncidencias" + Session.SessionID] = objReporteIncidencias;
        Response.Redirect("~/Reportes/frmRvIncidencias.aspx");
    }

}
