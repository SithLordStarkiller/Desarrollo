
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SICOGUA.Datos;
using SICOGUA.Entidades;
using SICOGUA.Negocio;
using System.Text;
using SICOGUA.Seguridad;
using System.Globalization;
using System.Web.UI;
using System.Data;
using SICOGUA.proNegocio;

public partial class PaseLista_frmInconsistencias : System.Web.UI.Page
{
    List<clsEntIncosistencia> lisInconsistencias = new List<clsEntIncosistencia>();
    public DataTable dtFuncion = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["objEmpleado" + Session.SessionID] = Session["empleadoFoto" + Session.SessionID] = null;
            clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];
            clsCatalogos.llenarCatalogo(ddlFuncion, "catalogo.spLeerFuncionAsignacion", "faDescripcion", "idFuncionAsignacion", (clsEntSesion)Session["objSession" + Session.SessionID]);
            Session["dtFuncion"] =(DataTable)  ddlFuncion.DataSource;
          
            lblFecha.Text = DateTime.Now.ToShortDateString();
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuarioAsignacionLimitada", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
        }
    }

    protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        grvAsistencia.DataSource = "";
        grvAsistencia.DataBind();
        if (ddlServicio.SelectedIndex > 0)
        {
            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacion, "catalogo.spLeerInstalacionesPorUsuarioAsignacionLimitada", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicio.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlInstalacion.Enabled = true;
            tOperaciones.Visible = false;
            return;
        }
        ddlInstalacion.Items.Clear();
        ddlInstalacion.Enabled = false;
        tOperaciones.Visible = false;
       
    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        StringBuilder sbUrl = new StringBuilder();
       lisInconsistencias= clsDatInconsistencias.listaInconsistencia( (clsEntSesion)Session["objSession" + Session.SessionID], Convert.ToInt32(ddlServicio.SelectedItem.Value), Convert.ToInt32(ddlInstalacion.SelectedItem.Value), DateTime.Parse(lblFecha.Text));
       grvAsistencia.DataSource = lisInconsistencias;
       grvAsistencia.DataBind();
       Session["lisInconsistencias"] = lisInconsistencias;
       if (lisInconsistencias == null || lisInconsistencias.Count == 0)
       {
           sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est101&strMensaje=" + Server.UrlEncode("No existen inconsistencias para la instalación seleccionada"));
           Response.Redirect(sbUrl.ToString());
       }
       else
       {
           tOperaciones.Visible = true;
       }

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lisInconsistencias = (List<clsEntIncosistencia>)Session["lisInconsistencias"];
        string strRegresa = "";
        lisInconsistencias = lisInconsistencias.FindAll(delegate(clsEntIncosistencia p) { return p.cambiar != 0; });

        if (lisInconsistencias != null && lisInconsistencias.Count != 0)
        {

            strRegresa = clsDatInconsistencias.cambiaInconsistencias(lisInconsistencias, (clsEntSesion)Session["objSession" + Session.SessionID]);
            if (strRegresa.Length == 0)
            {
                string script = "alert('Se almacenó la información!.');";
                StringBuilder sbUrl = new StringBuilder();

                sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + Server.UrlEncode("Operación finalizada con éxito.") + "&hyper=frmReemplazo"); ClientScript.RegisterClientScriptBlock(GetType(), "Mensaje", script, true);
                Response.Redirect(sbUrl.ToString());
            }
            else
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est101&strMensaje=" + Server.UrlEncode("Existe un problema, consultalo con el Administrador"));
                Response.Redirect(sbUrl.ToString());
            }




        }
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {

        ddlServicio.SelectedIndex = 0;
        ddlServicio_SelectedIndexChanged(null, null);
        ddlInstalacion.ClearSelection();
        ddlInstalacion.Items.Clear();
        ddlInstalacion.Enabled = false;
      
        lblEntradaSalida.Text = "Inconsitencias";



        grvAsistencia.DataSource = null;
        grvAsistencia.DataBind();

        tOperaciones.Visible = false;

        Session["dtInstante" + Session.SessionID] = null;
        Session["objAsistencia" + Session.SessionID] = null;
    }

    protected void grvAsistencia_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlInstalacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        grvAsistencia.DataSource = "";
        grvAsistencia.DataBind();
        if (ddlInstalacion.SelectedIndex > 0)
      
        tOperaciones.Visible = false;
    }

    protected void ddlHorario_SelectedIndexChanged(object sender, EventArgs e)
    {
        tOperaciones.Visible = false;
        grvAsistencia.DataSource = "";
        grvAsistencia.DataBind();
        
    }

    protected void grvAsistencia_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }
   
  
    protected void grvAsistencia_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            clsEntIncosistencia objInconsistencia = (clsEntIncosistencia)e.Row.DataItem;
            DropDownList ddlList = (DropDownList)e.Row.Cells[4].FindControl("ddlFuncionTabla");
            ddlList.Items.Add(new ListItem("", ""));
            ddlList.AppendDataBoundItems = true;
            ddlList.DataSource =(DataTable)Session["dtFuncion"];
            ddlList.DataValueField = "idFuncionAsignacion";
            ddlList.DataTextField = "faDescripcion";
            ddlList.DataBind();
            ddlList.CssClass = "texto";
            ddlList.SelectedIndex = ddlList.Items.IndexOf(ddlList.Items.FindByValue(objInconsistencia.idFuncionAsignacion.ToString()));
            if (objInconsistencia.permisoCambiar == false)
            {
                CheckBox chkValidar = (CheckBox)e.Row.Cells[4].FindControl("chkCambiar");
                chkValidar.Enabled = false;
                ddlFuncion.Enabled = false;
                e.Row.ForeColor = System.Drawing.Color.Red;
            }

        }
    }
    protected void seleccionaCambiar(object sender, EventArgs e)
    {
        lisInconsistencias = (List<clsEntIncosistencia>)Session["lisInconsistencias"];
        CheckBox chkValida = (CheckBox)sender;
        DataControlFieldCell drRenglon = (DataControlFieldCell)chkValida.Parent;
        GridViewRow gvrRenglon = (GridViewRow)drRenglon.Parent;
        if (chkValida.Checked == true)
        {
            lisInconsistencias[gvrRenglon.RowIndex].cambiar = 1;
        }
        else
        {
            lisInconsistencias[gvrRenglon.RowIndex].cambiar = 0;
        }
        Session["lisInconsistencias"] = lisInconsistencias;
    }
    protected void seleccionaFuncion(object sender, EventArgs e)
    {
      
        DropDownList ddlList = (DropDownList)sender;
        DataControlFieldCell drRenglon = (DataControlFieldCell)ddlList.Parent;
        GridViewRow gvrRenglon = (GridViewRow)drRenglon.Parent;
        lisInconsistencias = (List<clsEntIncosistencia>)Session["lisInconsistencias"];
        lisInconsistencias[gvrRenglon.RowIndex].idFuncionAsignacion = Convert.ToInt32(ddlList.SelectedItem.Value);
        Session["lisInconsistencias"] = lisInconsistencias;
    }
}