using System;
using proUtilerias;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;

public partial class Reportes_frmReporteIncidentes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clsCatalogos.llenarCatalogoZona(ddlZona, "catalogo.spLeerZona", "zonDescripcion", "idzona", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuario", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            //clsCatalogos.llenarCatalogoServicio(ddlServicio, "catalogo.spLeerServicio", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
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
        clsEntReporteIncidente objReporte = new clsEntReporteIncidente();
        objReporte.Servicio = new clsEntServicio();
        objReporte.Instalacion = new clsEntInstalacion();
        objReporte.ZonaEmpleadoInvolucrado = new clsEntZona();

        objReporte.Servicio.idServicio = Convert.ToInt32(ddlServicio.SelectedValue);
        objReporte.Instalacion.IdInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue);
        objReporte.ZonaEmpleadoInvolucrado.IdZona = Convert.ToInt16(ddlZona.SelectedValue);

        objReporte.RiFechaHora = clsUtilerias.dtObtenerFecha(txbFecha.Text);
        objReporte.Tarjeta = txbNumero.Text;
        objReporte.Sede = txbSede.Text;
        objReporte.Resumen = txbResumen.Text;

        Session["objReporteIncidente" + Session.SessionID] = objReporte;
        Response.Redirect("~/Reportes/frmRvIncidentes.aspx");
    }
}
