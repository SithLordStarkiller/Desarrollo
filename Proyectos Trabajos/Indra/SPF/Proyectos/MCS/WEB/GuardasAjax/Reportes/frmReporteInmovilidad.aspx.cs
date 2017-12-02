using System;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using proUtilerias;
using System.Web.UI;

public partial class Reportes_frmReporteInmovilidad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clsCatalogos.llenarCatalogoZona(ddlZona, "catalogo.spLeerZona", "zonDescripcion", "idZona", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosAnexoTecnico", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
        }
    }
    protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlServicio.SelectedIndex > 0)
        {
            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacion, "catalogo.spLeerInstalacionesAnexoTecnico", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicio.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
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

        objReporte.Servicio.idServicio = ddlServicio.SelectedValue == "" ? Convert.ToInt32(0) : Convert.ToInt32(ddlServicio.SelectedValue); ;
        objReporte.Instalacion.IdInstalacion = ddlInstalacion.SelectedValue == "" ? Convert.ToInt32(0) : Convert.ToInt32(ddlInstalacion.SelectedValue);
        objReporte.ZonaEmpleadoInvolucrado.IdZona = ddlZona.SelectedValue == "" ? Convert.ToInt16(0) : Convert.ToInt16(ddlZona.SelectedValue);
        objReporte.NoEmpleadoAutor = txbNumero.Text == "" ? Convert.ToInt32(0) : Convert.ToInt32(txbNumero.Text);
        objReporte.NombreEmpleadoAutor = txbNombre.Text;
        objReporte.NombreEmpleadoInvolucrado = txbPaterno.Text;
        objReporte.NombreEmpleadoSuperior = txbMaterno.Text;
        objReporte.Sede = rblTipo.SelectedValue;

        Session["objReporteInmovilidad" + Session.SessionID] = objReporte;
        Response.Redirect("~/Reportes/frmRvReporteInmovilidad.aspx");
    }
}