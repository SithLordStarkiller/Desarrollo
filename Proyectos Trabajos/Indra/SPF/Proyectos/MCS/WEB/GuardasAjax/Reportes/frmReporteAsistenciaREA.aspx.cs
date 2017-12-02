using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SICOGUA.Entidades;
using proUtilerias;
using SICOGUA.Negocio;
using SICOGUA.Seguridad;
using System.Configuration;
//using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;

using Microsoft.Reporting.WebForms;
public partial class Reportes_frmReporteAsistenciaREA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            covFecha.ValueToCompare = DateTime.Now.ToShortDateString();
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuario", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoEstatusREA(ddlEstatusAsistencia, "catalogo.spConsultarEstatusREA", "estDescripcion", "idEstatus", (clsEntSesion)Session["objSession" + Session.SessionID]);
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
        clsEntReporteListado objReporteListado = new clsEntReporteListado();
        objReporteListado.Servicio = new clsEntServicio();
        objReporteListado.Instalacion = new clsEntInstalacion();

        objReporteListado.FechaReporte = clsUtilerias.dtObtenerFecha(txbFecha.Text);
        objReporteListado.Servicio.idServicio = string.IsNullOrEmpty(ddlServicio.SelectedValue) ? 0 : Convert.ToInt32(ddlServicio.SelectedValue);
        objReporteListado.Instalacion.IdInstalacion = string.IsNullOrEmpty(ddlInstalacion.SelectedValue) ? 0 : Convert.ToInt32(ddlInstalacion.SelectedValue);
        objReporteListado.idEstatus = ddlEstatusAsistencia.SelectedValue == "" ? 0 :Convert.ToInt16(ddlEstatusAsistencia.SelectedValue);
        Session["objReporteListado" + Session.SessionID] = objReporteListado;
        Response.Redirect("~/Reportes/frmRvAsistenciaREA.aspx?hyper=Reporte");
      
    }
    }