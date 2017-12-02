using System;
using System.Data;
using System.Web.UI.WebControls;
using SICOGUA.Entidades;
using SICOGUA.Datos;
using SICOGUA.Negocio;
using SICOGUA.Seguridad;
using System.Text;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Collections.Specialized;

public partial class tipoAsignacion_frmTipoAsignacion : System.Web.UI.Page
{
    static private List<clsEntEmpleadoTipoAsignacion> lisEmpleadoTipoAsignacion = null;
    static private DataTable dtTipoAsignacion = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblFecha.Text = DateTime.Today.ToShortDateString();
        if (!IsPostBack)
        {
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuario", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoTipoAsignacion(ddlTipoAsignacion, "catalogo.spuLeerTipoAsignacion", "tiaDescripcion", "idTipoAsignacion", (clsEntSesion)Session["objSession" + Session.SessionID]);
            dtTipoAsignacion = (DataTable)ddlTipoAsignacion.DataSource;
            Session["tipoAsignacion"] = dtTipoAsignacion;
            btnGuardar.Enabled = false;
        }


    }

    protected void btnCerrar_Click(object sender, EventArgs e)
    {

        popDetalle.Hide();
    }


    public void paginacion(int pagina)
    {
        if (pagina > -1)
        {
            lblPagina.Text = ((int)ViewState["pagina"] + 1).ToString();
            if (ViewState["dsBuscar"] != null)
            {
                DataSet ds = ViewState["dsBuscar"] as DataSet;
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        DataTable dp = dt.Clone();
                        int pageSize = grvEmpleadoTipoAsignacion.PageSize;
                        int indice = pageSize * pagina;

                        for (int i = indice; i < indice + pageSize; i++)
                        {
                            if (i < dt.Rows.Count)
                            {
                                dp.ImportRow(dt.Rows[i]);
                            }
                        }
                        grvEmpleadoTipoAsignacion.DataSource = dp;
                        grvEmpleadoTipoAsignacion.DataBind();
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


    protected void gvReemplazos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int intRenglon = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName.ToUpper() == "CMDDETALLE")
        {
            popDetalle.Show();
            lblNombre.Text = lisEmpleadoTipoAsignacion[intRenglon].empCompleto;
            lblNumeroEmpleado.Text = lisEmpleadoTipoAsignacion[intRenglon].empNumero.ToString();
            lblJerarquia.Text = lisEmpleadoTipoAsignacion[intRenglon].jerDescripcion;
            lblLoc.Text = lisEmpleadoTipoAsignacion[intRenglon].empLoc;
            lblSexo.Text = lisEmpleadoTipoAsignacion[intRenglon].empSexo;
            lblZona.Text = lisEmpleadoTipoAsignacion[intRenglon].zonDescripcion;
            lblEstacion.Text = lisEmpleadoTipoAsignacion[intRenglon].estacion;
            lblServicio.Text = lisEmpleadoTipoAsignacion[intRenglon].serDescripcion;
            lblInstalacion.Text = lisEmpleadoTipoAsignacion[intRenglon].insNombre;
            lblEstado.Text = lisEmpleadoTipoAsignacion[intRenglon].estDescripcion;
            lblFuncion.Text = lisEmpleadoTipoAsignacion[intRenglon].faDescripcion;
            lblInicioAsignacion.Text = lisEmpleadoTipoAsignacion[intRenglon].inicioAsignacion.Substring(0, 10);
            lblFinAsignacion.Text = lisEmpleadoTipoAsignacion[intRenglon].finAsignacion.Length > 0 ? lisEmpleadoTipoAsignacion[intRenglon].finAsignacion.Substring(0, 10) : "";
            lblObservaciones.Text = lisEmpleadoTipoAsignacion[intRenglon].incidencia;
            lblInicioIncidencia.Text = lisEmpleadoTipoAsignacion[intRenglon].inicioIncidencia.Length > 0 ? lisEmpleadoTipoAsignacion[intRenglon].inicioIncidencia.Substring(0, 10) : "";
            lblFinInicidencia.Text = lisEmpleadoTipoAsignacion[intRenglon].finIncidencia.Length > 0 ? lisEmpleadoTipoAsignacion[intRenglon].finIncidencia.Substring(0, 10) : "";
            lblIncidenciaRevisada.Text = lisEmpleadoTipoAsignacion[intRenglon].incidenciaRevisada;
            lblTipoAsignacion.Text = lisEmpleadoTipoAsignacion[intRenglon].tiaDescripcion;
            lblFechaHora.Text = lisEmpleadoTipoAsignacion[intRenglon].fecha.Length >0 ?lisEmpleadoTipoAsignacion[intRenglon].fecha.Substring(0, 16):"";
            lblUsuario.Text = lisEmpleadoTipoAsignacion[intRenglon].usuario;
        }

    }
    protected void btnCerrarPeriodo_Click(object sender, EventArgs e)
    {
        //  popPeriodo.Hide();

    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string strRegresa = "";
        lisEmpleadoTipoAsignacion = lisEmpleadoTipoAsignacion.FindAll(delegate(clsEntEmpleadoTipoAsignacion p) { return p.idTipoAsignacion != 0; });

        if (lisEmpleadoTipoAsignacion != null || lisEmpleadoTipoAsignacion.Count != 0)
        {
            strRegresa = clsNegEmpleadoTipoAsignacion.insertaEmpleadoTipoAsignacion(lisEmpleadoTipoAsignacion, (clsEntSesion)Session["objSession" + Session.SessionID]);
            if (strRegresa.Length > 0)
            {
                //lblError.Visible = true;
                //divErrorReemplazo.Visible = true;
                //lblError.Text = strRegresa;
                return;
            }
            else
            {

                string script = "alert('Se almacenó la información!.');";
                StringBuilder sbUrl = new StringBuilder();

                sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + Server.UrlEncode("Operación finalizada con éxito.") + "&hyper=frmReemplazo"); ClientScript.RegisterClientScriptBlock(GetType(), "Mensaje", script, true);
                Response.Redirect(sbUrl.ToString());





            }
        }
        else
        {
            //lblError.Visible = true;
            //divErrorReemplazo.Visible = true;
            //lblError.Text = "Es necesario asignar periodo a todos los reemplazos";
            return;
        }

    }
    protected void btnNuevaBusquedaReemplazar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/tipoAsignacion/frmTipoAsignacion.aspx");
    }
    protected void btnBuscarReemplazar_Click(object sender, EventArgs e)
    {

        int idTipoAsignacion = 0, idServicio = 0, idInstalacion = 0, empNumero = 0;

        if (ddlTipoAsignacion.SelectedIndex > 0)
        {
            idTipoAsignacion = Convert.ToInt32(ddlTipoAsignacion.SelectedItem.Value);
        }
        if (ddlServicio.SelectedIndex > 0)
        {
            idServicio = Convert.ToInt32(ddlServicio.SelectedItem.Value);
        }
        if (ddlInstalacion.SelectedIndex > 0)
        {
            idInstalacion = Convert.ToInt32(ddlInstalacion.SelectedItem.Value);
        }
        if (txbNumero.Text.Length > 0)
        {
            empNumero = Convert.ToInt32(txbNumero.Text);
        }


        lisEmpleadoTipoAsignacion = clsNegEmpleadoTipoAsignacion.lisEmpleadoTipoAsignacion((clsEntSesion)Session["objSession" + Session.SessionID], idTipoAsignacion, idServicio, idInstalacion, empNumero, txbPaterno.Text, txbMaterno.Text, txbNombre.Text, txbRFC.Text, Convert.ToInt32(rblTipoAsignacion.SelectedValue));
        grvEmpleadoTipoAsignacion.DataSource = lisEmpleadoTipoAsignacion;
        grvEmpleadoTipoAsignacion.DataBind();

        Session["lisEmpleadoTipoAsignacion"] = lisEmpleadoTipoAsignacion;
        lblTotal.Text = lisEmpleadoTipoAsignacion.Count().ToString();
        lblPagina.Text = (grvEmpleadoTipoAsignacion.PageIndex + 1).ToString();
        lblPaginas.Text = grvEmpleadoTipoAsignacion.PageCount.ToString();


        if (lisEmpleadoTipoAsignacion.Count > 0)
        {
            btnGuardar.Enabled = true;
        }
        else
        {
            btnGuardar.Enabled = false;
        }


    }

    protected void gvReemplazos_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        lisEmpleadoTipoAsignacion = (List<clsEntEmpleadoTipoAsignacion>)Session["lisEmpleadoTipoAsignacion"];
        if (lisEmpleadoTipoAsignacion != null)
        {

            grvEmpleadoTipoAsignacion.PageIndex = e.NewPageIndex;
            grvEmpleadoTipoAsignacion.DataSource = lisEmpleadoTipoAsignacion;
            grvEmpleadoTipoAsignacion.DataBind();
            lblPagina.Text = (grvEmpleadoTipoAsignacion.PageIndex + 1).ToString();
            lblPaginas.Text = grvEmpleadoTipoAsignacion.PageCount.ToString();

        }
    }
    protected void imgAtrasReemplazar_Click(object sender, ImageClickEventArgs e)
    {
        if (grvEmpleadoTipoAsignacion.PageIndex > 0)
        {
            gvReemplazos_PageIndexChanging(grvEmpleadoTipoAsignacion, new GridViewPageEventArgs(grvEmpleadoTipoAsignacion.PageIndex - 1));
            lblPagina.Text = (grvEmpleadoTipoAsignacion.PageIndex + 1).ToString();
            lblPaginas.Text = grvEmpleadoTipoAsignacion.PageCount.ToString();
        }
    }
    protected void imgAdelanteReemplazar_Click(object sender, ImageClickEventArgs e)
    {
        if (grvEmpleadoTipoAsignacion.PageIndex < grvEmpleadoTipoAsignacion.PageCount)
        {
            gvReemplazos_PageIndexChanging(grvEmpleadoTipoAsignacion, new GridViewPageEventArgs(grvEmpleadoTipoAsignacion.PageIndex + 1));
            lblPagina.Text = (grvEmpleadoTipoAsignacion.PageIndex + 1).ToString();
            lblPaginas.Text = grvEmpleadoTipoAsignacion.PageCount.ToString();
        }
    }
    protected void grvEmpleadoTipoAsignacion_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            clsEntEmpleadoTipoAsignacion objEmpleadoTipoAsignacion = (clsEntEmpleadoTipoAsignacion)e.Row.DataItem;
            DropDownList ddlList = (DropDownList)e.Row.Cells[3].FindControl("ddlAsignacionTabla");
            dtTipoAsignacion = (DataTable)Session["tipoAsignacion"];
            ddlList.Items.Add(new ListItem("", "0"));
            ddlList.AppendDataBoundItems = true;
            ddlList.DataSource = dtTipoAsignacion;
            ddlList.DataValueField = "idTipoAsignacion";
            ddlList.DataTextField = "tiaDescripcion";
            ddlList.DataBind();
            ddlList.CssClass = "texto";
            ddlList.SelectedIndex = ddlList.Items.IndexOf(ddlList.Items.FindByValue(objEmpleadoTipoAsignacion.idTipoAsignacion.ToString()));


        }


    }
    protected void seleccionaTipoAsignacion(object sender, EventArgs e)
    {

        DropDownList ddlList = (DropDownList)sender;
        DataControlFieldCell drRenglon = (DataControlFieldCell)ddlList.Parent;
        GridViewRow gvrRenglon = (GridViewRow)drRenglon.Parent;
        lisEmpleadoTipoAsignacion[((Convert.ToInt32(lblPagina.Text) - 1) * 20) + gvrRenglon.RowIndex].idTipoAsignacion = Convert.ToInt32(ddlList.SelectedItem.Value);
    }
    protected void grvEmpleadoTipoAsignacion_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        int a = 0;
    }
    protected void btnReporte_Click(object sender, EventArgs e)
    {

        clsEntEmpleadoTipoAsignacion objEmpleadoTipoAsignacionReporte = new clsEntEmpleadoTipoAsignacion();

        objEmpleadoTipoAsignacionReporte.idServicio = ddlServicio.SelectedValue == "" ? 0 : Convert.ToInt32(ddlServicio.SelectedValue);
        objEmpleadoTipoAsignacionReporte.idInstalacion = ddlInstalacion.SelectedValue == "" ? 0 : Convert.ToInt32(ddlInstalacion.SelectedValue);
        objEmpleadoTipoAsignacionReporte.idTipoAsignacion = ddlTipoAsignacion.SelectedValue == "" ? 0 : Convert.ToInt32(ddlTipoAsignacion.SelectedValue);
        objEmpleadoTipoAsignacionReporte.empNumero = txbNumero.Text.Length <= 0 ? 0 : Convert.ToInt32(txbNumero.Text);
        objEmpleadoTipoAsignacionReporte.empPaterno = txbPaterno.Text.Length <= 0 ? "" : txbPaterno.Text;
        objEmpleadoTipoAsignacionReporte.empMaterno = txbMaterno.Text.Length <= 0 ? "" : txbMaterno.Text;
        objEmpleadoTipoAsignacionReporte.empNombre = txbNombre.Text.Length <= 0 ? "" : txbNombre.Text;
        objEmpleadoTipoAsignacionReporte.empRFC = txbRFC.Text.Length <= 0 ? "" : txbRFC.Text;
        objEmpleadoTipoAsignacionReporte.idEmpleadoAsignacion = Convert.ToInt32(rblTipoAsignacion.SelectedValue);


        Session["objEmpleadoTipoAsignacionReporte" + Session.SessionID] = objEmpleadoTipoAsignacionReporte;
        Response.Redirect("~/Reportes/frmRvTipoAsignacion.aspx?hyper=Reporte");
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/frmInicio.aspx");
    }
}