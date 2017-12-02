using System;
using System.Globalization;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using proUtilerias;

public partial class Reportes_frmReporteAntiguedad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clsCatalogos.llenarCatalogoZona(ddlZona, "catalogo.spLeerZona", "zonDescripcion", "idZona", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuario", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);

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
        clsEntReporteListado objReporteListado = new clsEntReporteListado();
        objReporteListado.Zona = new clsEntZona();
        objReporteListado.Servicio = new clsEntServicio();
        objReporteListado.Instalacion = new clsEntInstalacion();

        if (ddlZona.SelectedItem.Value == "")
        {
            divError.Visible = true;
            lblerror.Text = "Debe ingresar por lo menos la ZONA";
            return;
        }
        else
        {
            objReporteListado.Zona.IdZona = string.IsNullOrEmpty(ddlZona.SelectedValue) ? Convert.ToInt16(0) : Convert.ToInt16(ddlZona.SelectedValue);
            objReporteListado.Servicio.idServicio = string.IsNullOrEmpty(ddlServicio.SelectedValue) ? 0 : Convert.ToInt32(ddlServicio.SelectedValue);
            objReporteListado.Instalacion.IdInstalacion = string.IsNullOrEmpty(ddlInstalacion.SelectedValue) ? 0 : Convert.ToInt32(ddlInstalacion.SelectedValue);

            Session["objReporteListado" + Session.SessionID] = objReporteListado;
            Response.Redirect("~/Reportes/frmRvAntiguedad.aspx");
        }
    }

    protected void ddlZona_SelectedIndexChanged(object sender, EventArgs e)
    {
        divError.Visible = false;
        lblerror.Text = "";
    }
}