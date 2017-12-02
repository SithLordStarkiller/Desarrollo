using System;
using SICOGUA.Seguridad;
using SICOGUA.Datos;
using System.Data;
using System.Web.UI.WebControls;
using SICOGUA.Entidades;
using System.Text;

public partial class Catalogos_frmCatalogos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["impresion" + Session.SessionID] = null;
        if (!IsPostBack)
        {


        }
    }

    protected void mnuCatalogos_MenuItemClick(object sender, System.Web.UI.WebControls.MenuEventArgs e)
    {
        DataTable dt = new DataTable();
        if (e != null)
        {
            lblCatalogo.Text = e.Item.Target;
            hfCatalogo.Value = e.Item.Value;
        }
        trDescripcion.Visible = true;
        txbDescripcion.Focus();
        txbDescripcion.Text = "";

        switch (hfCatalogo.Value)
        {
            case "1":
                clsCatalogos.llenarCatalogo(ddlRaiz, "empleado.spConsultarCatalogoEmpleado", "empEmpleado", "idEmpleado", (clsEntSesion)Session["objSession" + Session.SessionID]);
                dt = clsDatCatalogos.consultaCatalogo("catalogo.spLeerServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
                grvCatalogo.Columns[2].HeaderText = "Responsable del Servicio";
                grvCatalogo.Columns[3].HeaderText = "Servicio";
                dt.Columns[2].ColumnName = "catDescripcion";
                lblRaíz.Text = "Responsable del Servicio:";
                grvCatalogo.Columns[2].Visible = true;
                lblDescripcion.Text = "Servicio:";
                txbDescripcion.MaxLength = 50;
                trRaiz.Visible = true;
                break;
            case "2":
                clsCatalogos.llenarCatalogo(ddlRaiz, "catalogo.spLeerServicio", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
                dt = clsDatCatalogos.consultaCatalogoInstalacion("catalogo.spLeerInstalacion", 0, (clsEntSesion)Session["objSession" + Session.SessionID]);
                grvCatalogo.Columns[3].HeaderText = "Instalación";
                grvCatalogo.Columns[2].HeaderText = "Servicio";
                dt.Columns[2].ColumnName = "catDescripcion";
                grvCatalogo.Columns[2].Visible = true;
                lblDescripcion.Text = "Instalación:";
                txbDescripcion.MaxLength = 100;
                lblRaíz.Text = "Servicio:";
                trRaiz.Visible = true;
                ddlRaiz.Focus();
                break;
            case "3":
                dt = clsDatCatalogos.consultaCatalogo("catalogo.spLeerTipoIncidencia", (clsEntSesion)Session["objSession" + Session.SessionID]);
                grvCatalogo.Columns[3].HeaderText = "Tipo de Incidencia";
                lblDescripcion.Text = "Tipo de Incidencia:";
                dt.Columns[2].ColumnName = "catDescripcion";
                grvCatalogo.Columns[2].Visible = false;
                txbDescripcion.MaxLength = 80;
                trRaiz.Visible = false;
                break;
        }
        dt.Columns[0].ColumnName = "idCatalogo";

    trResultados.Visible = true;
        trGrid.Visible = true;

        double registros = dt.Rows.Count;
        double porPagina = grvCatalogo.PageSize;
        double paginas = registros / porPagina;
        int total = (int)Math.Ceiling(paginas);

        lblPaginas.Text = total.ToString();

        ViewState["totalPaginas"] = total;

        ViewState["dsBuscar"] = dt;
        lblCount.Text = dt.Rows.Count.ToString();

        ViewState["pagina"] = 0;

        lblPagina.Text = ((int)ViewState["pagina"] + 1).ToString();

        paginacion((int)ViewState["pagina"]);

        if (lblCount.Text.Trim() == "0")
        {
            trGrid.Visible = false;
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        StringBuilder sbUrl = new StringBuilder();

        if (!string.IsNullOrEmpty(hfCatalogo.Value) && string.IsNullOrEmpty(hfIdCatalogo.Value))
        {
            switch (hfCatalogo.Value)
            {
                case "1":
                    clsEntServicio objServicio = new clsEntServicio();
                    objServicio.idEmpleado = new Guid(ddlRaiz.SelectedValue);
                    objServicio.serDescripcion = txbDescripcion.Text;
                    if (clsDatServicio.insertarServicio(objServicio, (clsEntSesion)Session["objSession" + Session.SessionID]))
                    {
                        mnuCatalogos_MenuItemClick(null, null);
                        return;
                    }
                    break;
                case "2":
                    clsEntInstalacion objInstalacion = new clsEntInstalacion();
                    objInstalacion.IdServicio = Convert.ToInt32(ddlRaiz.SelectedValue);
                    objInstalacion.InsNombre = txbDescripcion.Text;
                    if (clsDatInstalacion.insertarInstalacion(objInstalacion, (clsEntSesion)Session["objSession" + Session.SessionID]))
                    {
                        mnuCatalogos_MenuItemClick(null, null);
                        return;
                    }
                    break;
                case "3":
                    clsEntTipoIncidencia objIncidencia = new clsEntTipoIncidencia();
                    objIncidencia.TiDescripcion = txbDescripcion.Text;
                    if (clsDatTipoIncidencia.insertarTipoIncidencia(objIncidencia, (clsEntSesion)Session["objSession" + Session.SessionID]))
                    {
                        mnuCatalogos_MenuItemClick(null, null);
                        return;
                    }
                    break;
            }

        }
        else if (!string.IsNullOrEmpty(hfCatalogo.Value) && !string.IsNullOrEmpty(hfIdCatalogo.Value))
        {
            switch (hfCatalogo.Value)
            {
                case "1":
                    clsEntServicio objServicio = new clsEntServicio();
                    objServicio.idServicio = Convert.ToInt32(hfIdCatalogo.Value);
                    objServicio.idEmpleado = new Guid(ddlRaiz.SelectedValue);
                    objServicio.serDescripcion = txbDescripcion.Text;
                    if (clsDatServicio.actualizarServicio(objServicio, (clsEntSesion)Session["objSession" + Session.SessionID]))
                    {
                        mnuCatalogos_MenuItemClick(null, null);
                        return;
                    }
                    break;
                case "2":
                    clsEntInstalacion objInstalacion = new clsEntInstalacion();
                    objInstalacion.IdInstalacion = Convert.ToInt32(hfIdCatalogo.Value);
                    objInstalacion.IdServicioAntes = Convert.ToInt32(hfIdServicio.Value);
                    objInstalacion.IdServicio = Convert.ToInt32(ddlRaiz.SelectedValue);
                    objInstalacion.InsNombre = txbDescripcion.Text;
                    if (clsDatInstalacion.actualizarInstalacion(objInstalacion, (clsEntSesion)Session["objSession" + Session.SessionID]))
                    {
                        mnuCatalogos_MenuItemClick(null, null);
                        return;
                    }
                    break;
                case "3":
                    clsEntTipoIncidencia objIncidencia = new clsEntTipoIncidencia();
                    objIncidencia.IdTipoIncidencia = Convert.ToInt16(hfIdCatalogo.Value);
                    objIncidencia.TiDescripcion = txbDescripcion.Text;
                    if (clsDatTipoIncidencia.actualizarTipoIncidencia(objIncidencia, (clsEntSesion)Session["objSession" + Session.SessionID]))
                    {
                        mnuCatalogos_MenuItemClick(null, null);
                        return;
                    }
                    break;
            }

            hfIdCatalogo.Value = null;
            
        }

        sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est101&strMensaje=" + Server.UrlEncode("Ha ocurrido un error durante la operación. Intentelo más tarde ó contacte a un Administrador."));
        Response.Redirect(sbUrl.ToString());
    }

    protected void grvCatalogo_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in grvCatalogo.Rows)
        {
            Label lbl = (Label)gvr.FindControl("lblIndice");
            lbl.Text = Convert.ToString(gvr.RowIndex + 1);
        }
    }

    protected void grvCatalogo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (!string.IsNullOrEmpty(hfCatalogo.Value))
        {
            GridViewRow gvr = grvCatalogo.Rows[e.RowIndex];
            StringBuilder sbUrl = new StringBuilder();

            switch (hfCatalogo.Value)
            {
                case "1":
                    clsEntServicio objServicio = new clsEntServicio();
                    objServicio.idServicio = Convert.ToInt32(((Label)gvr.FindControl("lblIdCatalogo")).Text);
                    if (clsDatServicio.eliminarServicio(objServicio, (clsEntSesion)Session["objSession" + Session.SessionID]))
                    {
                        mnuCatalogos_MenuItemClick(null, null);
                        return;
                    }
                    break;
                case "2":
                    clsEntInstalacion objInstalacion = new clsEntInstalacion();
                    clsEntServicio objServicio2 = new clsEntServicio();
                    objServicio2.serDescripcion = ((Label)gvr.FindControl("lblRaiz")).Text;
                    objInstalacion.IdInstalacion = Convert.ToInt32(((Label)gvr.FindControl("lblIdCatalogo")).Text);
                    if (clsDatInstalacion.eliminarInstalacion(objInstalacion, objServicio2, (clsEntSesion)Session["objSession" + Session.SessionID]))
                    {
                        mnuCatalogos_MenuItemClick(null, null);
                        return;
                    }
                    break;
                case "3":
                    clsEntTipoIncidencia objIncidencia = new clsEntTipoIncidencia();
                    objIncidencia.IdTipoIncidencia = Convert.ToInt16(((Label)gvr.FindControl("lblIdCatalogo")).Text);
                    if (clsDatTipoIncidencia.eliminarTipoIncidencia(objIncidencia, (clsEntSesion)Session["objSession" + Session.SessionID]))
                    {
                        mnuCatalogos_MenuItemClick(null, null);
                        return;
                    }
                    break;
            }

            sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est101&strMensaje=" + Server.UrlEncode("Ha ocurrido un error durante la operación. Intentelo más tarde ó contacte a un Administrador."));
            Response.Redirect(sbUrl.ToString());
        }
    }

    protected void grvCatalogo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (!string.IsNullOrEmpty(hfCatalogo.Value))
        {
            GridViewRow gvr = grvCatalogo.Rows[e.RowIndex];
            hfIdCatalogo.Value = ((Label)gvr.FindControl("lblIdCatalogo")).Text;
            txbDescripcion.Text = ((Label)gvr.FindControl("lblDescripcion")).Text;
            ddlRaiz.ClearSelection();

            switch (hfCatalogo.Value)
            {
                case "1":
                    txbDescripcion.MaxLength = 50;                    
                    ddlRaiz.Items.FindByText(((Label)gvr.FindControl("lblRaiz")).Text).Selected = true;
                    ddlRaiz.Focus();
                    trRaiz.Visible = true;
                    break;
                case "2":
                    txbDescripcion.MaxLength = 100;
                    ddlRaiz.Items.FindByText(((Label)gvr.FindControl("lblRaiz")).Text).Selected = true;
                    hfIdServicio.Value = ddlRaiz.SelectedValue;
                    ddlRaiz.Focus();
                    trRaiz.Visible = true;
                    break;
                case "3":
                    txbDescripcion.MaxLength = 80;
                    txbDescripcion.Focus();
                    trRaiz.Visible = false;                    
                    break;
            }
        }
    }

    public void paginacion(int pagina)
    {
        if (pagina > -1)
        {
            lblPagina.Text = ((int)ViewState["pagina"] + 1).ToString();
            if (ViewState["dsBuscar"] != null)
            {
                DataTable dt = ViewState["dsBuscar"] as DataTable;
                if (dt != null)
                {
                    if (dt.Rows.Count != 0)
                    {
                        DataTable dp = dt.Clone();
                        int pageSize = grvCatalogo.PageSize;
                        int indice = pageSize * pagina;

                        for (int i = indice; i < indice + pageSize; i++)
                        {
                            if (i < dt.Rows.Count)
                            {
                                dp.ImportRow(dt.Rows[i]);
                            }
                        }
                        grvCatalogo.DataSource = dp;
                        grvCatalogo.DataBind();
                    }
                }
            }
        }
    }

    public int disminuirPagina()
    {
        int pagina = (int)ViewState["pagina"];
        if (pagina <= 0)
        {
            ViewState["pagina"] = 0;
            return 0;
        }
        ViewState["pagina"] = pagina - 1;
        return (pagina - 1);
    }

    public int aumentarPagina()
    {
        int pagina = (int)ViewState["pagina"];
        if (pagina >= (int)ViewState["totalPaginas"] - 1)
        {
            ViewState["pagina"] = (int)ViewState["totalPaginas"] - 1;
            return (int)ViewState["totalPaginas"] - 1;
        }
        ViewState["pagina"] = pagina + 1;
        return (pagina + 1);
    }

    protected void imgAtras_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        paginacion(disminuirPagina());
    }

    protected void imgAdelante_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        paginacion(aumentarPagina());
    }
}
