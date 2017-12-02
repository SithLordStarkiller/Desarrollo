using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SICOGUA.Negocio;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using System.Text;

public partial class Servicio_frmInventario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuarioAbierto", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
    }

    protected void grvInventario_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = grvInventario.Rows[e.RowIndex];
        TableCell id = row.Cells[0];
        TableCell idVig = row.Cells[3];
        Control objP = id.FindControl("idInventario");
        Control objV = idVig.FindControl("ieVigente");
        Label lbl = (Label)objP;
        Label lblV = (Label)objV;

        clsEntEquipoCatalogo objE = ((List<clsEntEquipoCatalogo>)ViewState["lstInventario"])[(Convert.ToInt32(lbl.Text) - 1)];
     
        clsEntInstalacion obj = new clsEntInstalacion
        {
            IdServicio = objE.idServicio
            ,IdInstalacion = objE.idInstalacion
            ,idInstalacionEquipo = objE.idInstalacionEquipo
        };

        txbFechaInicio.Text = Convert.ToDateTime(objE.ieFechaInicio).ToShortDateString();
        txbFechaFin.Text = objE.ieFechaFin!=string.Empty?Convert.ToDateTime(objE.ieFechaFin).ToShortDateString():string.Empty;
        ViewState["lstDetalle"] = clsNegArmamento.consultaInventarioDetallado(obj, (clsEntSesion)Session["objSession" + Session.SessionID]);
        grvArmamento.DataSource = ((List<clsEntEquipoCatalogo>)ViewState["lstDetalle"]).Count == 0 ? (List<clsEntEquipoCatalogo>)ViewState["lstInventarioEquipoAdd"] : (List<clsEntEquipoCatalogo>)ViewState["lstDetalle"];
        grvArmamento.DataBind();

        lblPop.Text = "Detalle del inventario";
        activo(((List<clsEntEquipoCatalogo>)ViewState["lstInventarioEquipoAdd"]) != null ? lblV.Text==string.Empty? true:false : false);
        popDetalle.Show();
    }

    protected void grvInventario_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        clsEntInstalacion objInstalacion = new clsEntInstalacion
        {
             IdServicio = Convert.ToInt32(ddlServicio.SelectedValue)
            ,IdInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue)
        };
        ViewState["lstInventario"] = clsNegArmamento.consultaInventario(objInstalacion, (clsEntSesion)Session["objSession" + Session.SessionID]);
        grvInventario.DataSource = (List<clsEntEquipoCatalogo>)ViewState["lstInventario"];
        grvInventario.DataBind();
        ViewState["lstInventarioEquipoAdd"] = null;
        btnAgregar.Enabled = true;
        btnGuardarFrm.Enabled = false;
    }

    protected void imgAdelante_Click(object sender, ImageClickEventArgs e)
    {
        grvInventario_PageIndexChanging(grvInventario, new GridViewPageEventArgs(grvInventario.PageIndex + 1));
    }
    protected void imgAtras_Click(object sender, ImageClickEventArgs e)
    {
        grvInventario_PageIndexChanging(grvInventario, new GridViewPageEventArgs(grvInventario.PageIndex - 1));
    }

    protected void grvInventario_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvInventario.PageIndex = ((List<clsEntEquipoCatalogo>)ViewState["lstInventario"]) != null ? e.NewPageIndex : 0;
        grvInventario.DataSource = ((List<clsEntEquipoCatalogo>)ViewState["lstInventario"]) != null ? ((List<clsEntEquipoCatalogo>)ViewState["lstInventario"]) : null;
        grvInventario.DataBind();

        imgAtras.Enabled = grvInventario.PageIndex == 0 ? false : true;
        imgAdelante.Enabled = grvInventario.PageIndex ==(grvInventario.Rows.Count)-1 ? false : true;

        if (grvInventario.PageIndex == 0)
        {
            grid_inventario();
        } 
    }

    protected void btnAgregarCancelar_Click(object sender, EventArgs e)
    {
        ViewState["lstDetalle"] =
        grvArmamento.DataSource = null;
        grvArmamento.DataBind();
        popDetalle.Hide();
        activo(true);
        divError.Visible = lblerror.Visible = false;
        lblerror.Text = string.Empty;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["lstDetalle"] = ViewState["lstInventarioEquipoAdd"] = ViewState["lstInventario"] =grvArmamento.DataSource = grvInventario.DataSource= null;
        grvInventario.DataBind(); grvArmamento.DataBind();
        btnGuardarFrm.Enabled = false;
        ddlInstalacion.Items.Clear();
        ddlInstalacion.Enabled = false;
        btnAgregar.Enabled = false;
        if (ddlServicio.SelectedIndex > 0)
        {
            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacion, "catalogo.spLeerInstalacionesPorUsuario", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicio.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlInstalacion.Enabled = true;
            return;
        }
    }
    protected void btnDetalleCancelar_Click(object sender, EventArgs e)
    {

    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        clsEntInstalacion obj = new clsEntInstalacion
        {
            IdServicio = Convert.ToInt32(ddlServicio.SelectedValue)
           ,IdInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue)
        };

        ViewState["lstInventarioEquipo"] = clsNegArmamento.consultaCatalogoEquipo(obj,(clsEntSesion)Session["objSession" + Session.SessionID]);
        grvArmamento.DataSource = (List<clsEntEquipoCatalogo>)ViewState["lstInventarioEquipo"];
        grvArmamento.DataBind();
        txbFechaInicio.Text = txbFechaFin.Text = string.Empty;
        lblPop.Text = "Agregar inventario";
        popDetalle.Show();
    }
    protected void btnAgregarPop_Click(object sender, EventArgs e)
    {
        //VALIDACIONES
        if (string.IsNullOrEmpty(txbFechaInicio.Text.Trim())) { divError.Visible = lblerror.Visible = true; lblerror.Text = "Es necesaria la fecha de inicio del inventario"; popDetalle.Show(); return; }
        int reg_ant = (((List<clsEntEquipoCatalogo>)ViewState["lstInventario"]).Count) - 2;
        if (reg_ant != -1 && reg_ant != -2)
        {
            DateTime fechaInicioAnt = Convert.ToDateTime(((List<clsEntEquipoCatalogo>)ViewState["lstInventario"])[0].ieFechaInicio);
            if (Convert.ToDateTime(txbFechaInicio.Text) < fechaInicioAnt) { divError.Visible = lblerror.Visible = true; lblerror.Text = "La fecha es menor al último inventario"; popDetalle.Show(); return; }
        }
        else
        {
            if (reg_ant == -1 && ((List<clsEntEquipoCatalogo>)ViewState["lstInventario"])[0].ieVigente!=null)
            {
                DateTime fechaInicioAnt = Convert.ToDateTime(((List<clsEntEquipoCatalogo>)ViewState["lstInventario"])[0].ieFechaInicio);
                if (Convert.ToDateTime(txbFechaInicio.Text) < fechaInicioAnt) { divError.Visible = lblerror.Visible = true; lblerror.Text = "La fecha es menor al último inventario"; popDetalle.Show(); return; }
            }
        }
        //FIN

        clsEntInstalacion objInstalacion = new clsEntInstalacion
        {
             IdServicio = Convert.ToInt32(ddlServicio.SelectedValue)
            ,IdInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue)
        };
        ViewState["lstInventarioEquipoAdd"] = null;
        ViewState["lstInventario"] = clsNegArmamento.consultaInventario(objInstalacion, (clsEntSesion)Session["objSession" + Session.SessionID]);
        grvInventario.DataSource = (List<clsEntEquipoCatalogo>)ViewState["lstInventario"];
        grvInventario.DataBind();

        clsEntEquipoCatalogo objInventario = new clsEntEquipoCatalogo
                {
                    idServicio = Convert.ToInt32(ddlServicio.SelectedValue)
                    ,idInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue)
                    ,ieFechaInicio = txbFechaInicio.Text
                    ,semaforo=1
                    ,ieFechaFin=string.Empty
                };

        if(ViewState["lstInventario"]!=null)
        {
            ((List<clsEntEquipoCatalogo>)ViewState["lstInventario"]).Insert(0,objInventario);
        }
        else
        {
            List<clsEntEquipoCatalogo> lstView = new List<clsEntEquipoCatalogo>();
            lstView.Add(objInventario);
            ViewState["lstInventario"] = lstView;
        }

        List<clsEntEquipoCatalogo> lst = new List<clsEntEquipoCatalogo>();
                foreach (GridViewRow row in grvArmamento.Rows)
                {
                    TableCell lblDes = row.Cells[1];
                    TableCell txb = row.Cells[2];
                    TableCell lbl = row.Cells[3];
                    Control objDes = lblDes.FindControl("lblDescripcion");
                    Control objTxb = txb.FindControl("txbNumero");
                    Control objLbl = lbl.FindControl("idEquipo");

                    Label lblDescripcion = (Label)objDes;   
                    TextBox txbNumero = (TextBox)objTxb;
                    Label lblIdEquipo = (Label)objLbl;

                    clsEntEquipoCatalogo obj = new clsEntEquipoCatalogo();
                    obj.idServicio =Convert.ToInt32(ddlServicio.SelectedValue);
                    obj.idInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue);
                    obj.idEquipo =Convert.ToInt32(lblIdEquipo.Text);
                    obj.ieCantidad = txbNumero.Text==string.Empty?0:Convert.ToInt32(txbNumero.Text.Trim());
                    obj.equDescripcion = lblDescripcion.Text.Trim();

                    lst.Add(obj);
                }
                ViewState["lstInventarioEquipoAdd"] = lst;
                mod_inventario();
                grvInventario.DataSource = ((List<clsEntEquipoCatalogo>)ViewState["lstInventario"]);
                grvInventario.DataBind();
                btnAgregar.Enabled = false;
                grid_inventario();
                popDetalle.Hide();
                btnGuardarFrm.Enabled = true;
                divError.Visible = lblerror.Visible = false;
                lblerror.Text = string.Empty;
    }

    private void activo(bool activo)
    {
        txbFechaInicio.Enabled =
        imbFechaInicio.Enabled =
        btnAgregarPop.Enabled =
        grvArmamento.Enabled = activo;
    }

    private void grid_inventario()
    {
        foreach (GridViewRow row in grvInventario.Rows)
        {
 
            TableCell lbl = row.Cells[3];
            TableCell imb = row.Cells[5];
            Control objLbl = lbl.FindControl("ieVigente");
            Control objImb = imb.FindControl("imbEliminar");
            Label lblVigente = (Label)objLbl;
            ImageButton img = (ImageButton)objImb;
            img.Visible = lblVigente.Text == string.Empty ? true : false;

        }
    }

    private void mod_inventario()
    {
        if (((List<clsEntEquipoCatalogo>)ViewState["lstInventario"]).Count < 2) { return; }
        //REVISAR FECHA DE INVENTARIO
        DateTime fechaInicio =Convert.ToDateTime(((List<clsEntEquipoCatalogo>)ViewState["lstInventario"])[0].ieFechaInicio);
        //FIN

        //CERRAR FECHA DE INVENTARIO ANTERIOR
        DateTime fechaInicioAnt = Convert.ToDateTime(((List<clsEntEquipoCatalogo>)ViewState["lstInventario"])[1].ieFechaInicio);

        if (fechaInicio == fechaInicioAnt)
        {
            ((List<clsEntEquipoCatalogo>)ViewState["lstInventario"])[1].ieFechaFin = fechaInicio.ToShortDateString();

        }
        else
        {
            ((List<clsEntEquipoCatalogo>)ViewState["lstInventario"])[1].ieFechaFin = fechaInicio.AddDays(-1).ToShortDateString();
        }
            ((List<clsEntEquipoCatalogo>)ViewState["lstInventario"])[1].semaforo = 2;
        //FIN
    }

    protected void ddlInstalacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["lstDetalle"] = ViewState["lstInventarioEquipoAdd"] = ViewState["lstInventario"] = grvArmamento.DataSource = grvInventario.DataSource = null;
        grvInventario.DataBind(); grvArmamento.DataBind();
       
        if (ddlInstalacion.SelectedIndex != 0)
        {
            btnAgregar.Enabled = clsNegArmamento.consultaPermisos(ddlServicio.SelectedValue, ddlInstalacion.SelectedValue, (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsEntInstalacion objInstalacion = new clsEntInstalacion
            {
                IdServicio = Convert.ToInt32(ddlServicio.SelectedValue)
                ,
                IdInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue)
            };

            ViewState["lstInventario"] = clsNegArmamento.consultaInventario(objInstalacion, (clsEntSesion)Session["objSession" + Session.SessionID]);
            grvInventario.DataSource = (List<clsEntEquipoCatalogo>)ViewState["lstInventario"];
            grvInventario.DataBind();
        }
        else
        {
            grvInventario.DataSource = null;
            grvInventario.DataBind();
        }

        btnGuardarFrm.Enabled = false;


            imgAtras.Visible = ((List<clsEntEquipoCatalogo>)ViewState["lstInventario"]).Count > 6?true:false;
            imgAdelante.Visible = ((List<clsEntEquipoCatalogo>)ViewState["lstInventario"]).Count > 6 ? true : false;
            imgAtras.Enabled = false;
            imgAdelante.Enabled = ((List<clsEntEquipoCatalogo>)ViewState["lstInventario"]).Count > 6 ? true : false;  




    }

    protected void btnCancelarFrm_Click(object sender, EventArgs e)
    {
        ViewState["lstDetalle"] = ViewState["lstInventarioEquipoAdd"] = ViewState["lstInventario"] = grvArmamento.DataSource = grvInventario.DataSource = null;
        grvInventario.DataBind(); grvArmamento.DataBind();
        Response.Redirect("~/frmInicio.aspx");
    }

    protected void btnGuardarFrm_Click(object sender, EventArgs e)
    {
        StringBuilder sbUrl = new StringBuilder();
        switch (clsNegArmamento.insertarInventario(((List<clsEntEquipoCatalogo>)ViewState["lstInventario"]), (List<clsEntEquipoCatalogo>)ViewState["lstInventarioEquipoAdd"], (clsEntSesion)Session["objSession" + Session.SessionID]))
        {
            case true:
                sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + Server.UrlEncode("Operación finalizada con éxito.") + "&hyper=frmIventario");
                break;
            case false:
                sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est101&strMensaje=" + Server.UrlEncode("Ha ocurrido un error durante la operación. Intentelo más tarde ó contacte a un Administrador."));
                break;
        }
     
        ViewState["lstDetalle"] = ViewState["lstInventarioEquipoAdd"] = ViewState["lstInventario"] = grvArmamento.DataSource = grvInventario.DataSource = null;
        btnGuardarFrm.Enabled = false;
        btnAgregar.Enabled = true;
        Response.Redirect(sbUrl.ToString());
    }
}