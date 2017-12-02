using System;
using System.Data;
using System.Web.UI.WebControls;
using SICOGUA.Entidades;
using proUtilerias;
using SICOGUA.Datos;
using SICOGUA.Negocio;
using SICOGUA.Seguridad;

public partial class Incidentes_frmBusquedaIncidentes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["impresion" + Session.SessionID] = null;
        if (!IsPostBack)
        {
            trResultados.Visible = false;
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuarioLimitado", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoZona(ddlZona, "catalogo.spLeerZona", "zonDescripcion", "idZona", (clsEntSesion)Session["objSession" + Session.SessionID]);
        }
    }

    protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlServicio.SelectedIndex > 0)
        {
            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacion, "catalogo.spLeerInstalacionesPorUsuarioAsignacionLimitada", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicio.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlInstalacion.Enabled = true;
            return;
        }
        ddlInstalacion.Items.Clear();
        ddlInstalacion.Enabled = false;
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        clsEntReporteIncidente objIncidente = new clsEntReporteIncidente();
        objIncidente.Servicio = new clsEntServicio();
        objIncidente.Instalacion = new clsEntInstalacion();
        objIncidente.ZonaEmpleadoInvolucrado = new clsEntZona();

        
        if (!string.IsNullOrEmpty(ddlServicio.SelectedValue))
        {
            objIncidente.Servicio.idServicio = Convert.ToInt32(ddlServicio.SelectedValue);
        }
        if (!string.IsNullOrEmpty(ddlInstalacion.SelectedValue))
        {
            objIncidente.Instalacion.IdInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue);
        }
        objIncidente.RiFechaHora = clsUtilerias.dtObtenerFecha(txbFecha.Text);
        objIncidente.NombreEmpleadoInvolucrado = txbPaterno.Text + " " + txbMaterno.Text + " " + txbNombre.Text;
        if (!string.IsNullOrEmpty(txbNumero.Text))
        {
            objIncidente.NoEmpleadoInvolucrado = Convert.ToInt32(txbNumero.Text);
        }
        if (!string.IsNullOrEmpty(ddlZona.SelectedValue))
        {
            objIncidente.ZonaEmpleadoInvolucrado.IdZona = Convert.ToInt16(ddlZona.SelectedValue);
        }

        DataTable dtBuscar = clsDatReporteIncidente.buscarReporteIncidente(objIncidente, (clsEntSesion)Session["objSession" + Session.SessionID]);

        double registros = dtBuscar.Rows.Count;
        double porPagina = grvBusqueda.PageSize;
        double paginas = registros / porPagina;
        int total = (int)Math.Ceiling(paginas);

        lblPaginas.Text = total.ToString();

        ViewState["totalPaginas" + Session.SessionID] = total;

        ViewState["dsBuscar" + Session.SessionID] = dtBuscar;
        lblCount.Text = dtBuscar.Rows.Count.ToString();

        ViewState["pagina" + Session.SessionID] = 0;

        lblPagina.Text = ((int)ViewState["pagina" + Session.SessionID] + 1).ToString();

        paginacion((int)ViewState["pagina" + Session.SessionID]);

        trResultados.Visible = true;
    }

    public void paginacion(int pagina)
    {
        if (pagina > -1)
        {
            lblPagina.Text = ((int)ViewState["pagina" + Session.SessionID] + 1).ToString();
            if (ViewState["dsBuscar" + Session.SessionID] != null)
            {
                DataTable dt = ViewState["dsBuscar" + Session.SessionID] as DataTable;
                if (dt != null)
                {
                    if (dt.Rows.Count != 0)
                    {
                        DataTable dp = dt.Clone();
                        int pageSize = grvBusqueda.PageSize;
                        int indice = pageSize * pagina;

                        for (int i = indice; i < indice + pageSize; i++)
                        {
                            if (i < dt.Rows.Count)
                            {
                                dp.ImportRow(dt.Rows[i]);
                            }
                        }
                        grvBusqueda.DataSource = dp;
                        grvBusqueda.DataBind();
                    }
                    else
                    {
                        grvBusqueda.DataSource = null;
                        grvBusqueda.DataBind();
                    }
                }
            }
        }
    }

    public int disminuirPagina()
    {
        int pagina = (int)ViewState["pagina" + Session.SessionID];
        if (pagina <= 0)
        {
            ViewState["pagina" + Session.SessionID] = 0;
            return 0;
        }
        ViewState["pagina" + Session.SessionID] = pagina - 1;
        return (pagina - 1);
    }

    public int aumentarPagina()
    {
        int pagina = (int)ViewState["pagina" + Session.SessionID];
        if (pagina >= (int)ViewState["totalPaginas" + Session.SessionID] - 1)
        {
            ViewState["pagina" + Session.SessionID] = (int)ViewState["totalPaginas" + Session.SessionID] - 1;
            return (int)ViewState["totalPaginas" + Session.SessionID] - 1;
        }
        ViewState["pagina" + Session.SessionID] = pagina + 1;
        return (pagina + 1);
    }

    protected void btnNuevaBusqueda_Click(object sender, EventArgs e)
    {
        txbFecha.Text = "";
        txbMaterno.Text = "";
        txbNombre.Text = "";
        txbNumero.Text = "";
        txbPaterno.Text = "";
        ddlServicio.SelectedIndex = -1;
        ddlServicio_SelectedIndexChanged(null, null);
        ddlInstalacion.SelectedIndex = -1;
        ddlZona.SelectedIndex = -1;
        trResultados.Visible = false;

 
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        ViewState["totalPaginas" + Session.SessionID] = null;
        ViewState["dsBuscar" + Session.SessionID] = null;
        ViewState["pagina" + Session.SessionID] = null;
        Response.Redirect("~/frmInicio.aspx");
    }

    protected void imgAtras_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        paginacion(disminuirPagina());
    }

    protected void imgAdelante_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        paginacion(aumentarPagina());
    }

    protected void grvBusqueda_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if(grvBusqueda.Rows.Count > 0)
        {
            clsEntReporteIncidente objIncidente = new clsEntReporteIncidente();
            Label lblIdIncidente = (Label) grvBusqueda.Rows[e.RowIndex].FindControl("lblIdIncidencia");
            objIncidente.IdIncidente = Convert.ToInt32(lblIdIncidente.Text);
            clsNegReporteIncidente.consultarReporteIncidente(ref objIncidente, (clsEntSesion)Session["objSession" + Session.SessionID]);

            Session["objIncidente" + Session.SessionID] = objIncidente;
            Response.Redirect("~/Incidentes/frmIncidentes.aspx");
        }
    }
}
