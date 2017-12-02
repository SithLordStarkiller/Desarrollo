using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using SICOGUA.Datos;
using SICOGUA.Negocio;
using REA.Negocio;

public partial class Servicio_frmBuscarInstalacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Parametros
        if (Request.QueryString.Count > 0)
        {
            Session["pantalla"] = Request.QueryString.Get("pantalla");
        }
        #endregion
        if (!IsPostBack)
        {
            Session["lista"] = null;
            Session["pagina"]=null;
            Session["totalPaginas"] = null;
            Session["impresion" + Session.SessionID] = null;
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosAnexoTecnico", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
        }
    }
    protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlServicio.SelectedIndex > 0)
        {
            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacion, "catalogo.spLeerInstalacionesAnexoTecnico", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicio.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlInstalacion.Enabled = true;
            
        }


    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        List<clsEntInstalacion> lsInstalacion = new List<clsEntInstalacion>();
        int idServicio=0;
        int intInstalacion=0;
        char charVigente='P';
        if (rbtVigente.Checked){ charVigente='V';}
        if(rbtNoVigente.Checked){charVigente='N';}
        if(rbtAmbos.Checked){charVigente='A';}


        if (ddlServicio.SelectedIndex > 0) { idServicio = Convert.ToInt32(ddlServicio.SelectedValue); } else { idServicio = 0; }
        if (ddlInstalacion.SelectedIndex <= 0) { intInstalacion = 0; } else { intInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue); }

       lsInstalacion = clsNegHorarioREA.consultarInstalacion(idServicio, intInstalacion, charVigente, (clsEntSesion)Session["objSession" + Session.SessionID]);
       double registro = lsInstalacion.Count();
       double porPagina = grvBusqueda.PageSize;
       double paginas = registro / porPagina;
       int total = (int)Math.Ceiling(paginas);

      // lblPaginas.Text = total.ToString();
       string valor = Convert.ToString(lsInstalacion.Count());
       lblCount.Text = valor;
       //Session["pagina"] = 0;
       //lblPagina.Text = ((int)Session["pagina"] + 1).ToString();
       //Session["totalPaginas"] = total;
       // Session["lista"] = lsInstalacion;
       // paginacion((int)Session["pagina"]);

       // if (valor == "0")
       // {
       //     trGrid.Visible = false;
       // }
       // else
       // {
       //     trGrid.Visible = true;
       // }
       trGrid.Visible = true;
       grvBusqueda.DataSource = lsInstalacion;
       grvBusqueda.DataBind();

      
    }
    protected void btnNueva_Click(object sender, EventArgs e)
    {
        limpiarforma();
    }
    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        limpiarforma();
        Response.Redirect("~/frmInicio.aspx");
    }

    public void limpiarforma()
    {
        ddlServicio.SelectedIndex = 0;
        ddlInstalacion.SelectedIndex = 0;
        ddlInstalacion.Enabled = false;
        rbtVigente.Checked = true;
        rbtAmbos.Checked = false;
        rbtNoVigente.Checked = false;
        List<clsEntInstalacion> lsInstalacion = new List<clsEntInstalacion>();
        grvBusqueda.DataSource = lsInstalacion;
        grvBusqueda.DataBind();
        trGrid.Visible = false;
        lblCount.Text = "";
        
    }
    //protected void imgAtras_Click(object sender, ImageClickEventArgs e)
    //{
    //    disminuirPagina();
    //}
    //protected void imgAdelante_Click(object sender, ImageClickEventArgs e)
    //{
    //    aumentarPagina();
    //}

    protected void grvBusqueda_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Session["servicio"]=Convert.ToInt32(((Label)(grvBusqueda.Rows[e.RowIndex].Cells[0].FindControl("lblServicio"))).Text);
        Session["instalacion"] = Convert.ToInt32(((Label)(grvBusqueda.Rows[e.RowIndex].Cells[0].FindControl("lblInstalacion"))).Text);
        Session["vigencia"] = ((Label)(grvBusqueda.Rows[e.RowIndex].Cells[0].FindControl("lblVigente"))).Text;
        Session["lista"] = null;
         Response.Redirect(Session["pantalla"].ToString());
    }

    //public void paginacion(int pagina)
    //{
    //    if (pagina > -1)
    //    {
    //        lblPagina.Text = ((int)Session["pagina"] + 1).ToString();
    //        if (Session["lista"] != null)
    //        {
    //            List<clsEntInstalacion> lsInstalacion = new List<clsEntInstalacion>();
    //            List<clsEntInstalacion> lsSerIns = new List<clsEntInstalacion>();
    //            lsInstalacion = (List<clsEntInstalacion>)Session["lista"];
    //            if (lsInstalacion.Count != 0)
    //            {
    //                int pageSize = grvBusqueda.PageSize;
    //                int indice = pageSize * pagina;
    //                for (int i = indice; i < indice + pageSize; i++)
    //                {
    //                    if (i < lsInstalacion.Count())
    //                    {
    //                        lsSerIns.Add(lsInstalacion[i]);
    //                    }
    //                }
    //                grvBusqueda.DataSource = lsSerIns;
    //                grvBusqueda.DataBind();
    //            }
    //        }
    //    }
    //}
    //public int disminuirPagina()
    //{
    //    int pagina = (int)ViewState["pagina"];
    //    if (pagina <= 0)
    //    {
    //        ViewState["pagina"] = 0;
    //        return 0;
    //    }
    //    ViewState["pagina"] = pagina - 1;
    //    return (pagina - 1);
    //}
    //public int aumentarPagina()
    //{
    //    int pagina = (int)Session["pagina"];
    //    if (pagina >= (int)Session["totalPaginas"] - 1)
    //    {
    //        Session["pagina"] = (int)Session["totalPaginas"] - 1;
    //        return (int)Session["totalPaginas"] - 1;
    //    }
    //    Session["pagina"] = pagina + 1;
    //    return (pagina + 1);
    //}
}