using System;
using System.Web.UI.WebControls;
using proUtilerias;
using SICOGUA.Seguridad;
using SICOGUA.Datos;
using System.Text;
using SICOGUA.Entidades;

public partial class Usuarios_frmUsuarios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clsCatalogos.llenarCatalogo(ddlPerfil, "seguridad.spConsultarPerfiles", "perDescripcion", "idPerfil", (clsEntSesion)Session["objSession" + Session.SessionID]);
            grvUsuario.DataSource = clsDatCatalogos.consultaCatalogo("seguridad.spConsultarUsuarios", (clsEntSesion)Session["objSession" + Session.SessionID]);
            grvUsuario.DataBind();

            if (Session["objEmpleado" + Session.SessionID] != null)
            {
                clsEntEmpleado objEmpleado = (clsEntEmpleado)Session["objEmpleado" + Session.SessionID];

                clsUtilerias.llenarLabel(lblPaterno, objEmpleado.EmpPaterno, "-");
                clsUtilerias.llenarLabel(lblMaterno, objEmpleado.EmpMaterno, "-");
                clsUtilerias.llenarLabel(lblNombre, objEmpleado.EmpNombre, "-");
                clsUtilerias.llenarLabel(lblSexo, objEmpleado.EmpSexo.ToString(), "-");

                clsUtilerias.llenarTextBox(txbUsuario, objEmpleado.EmpNombre.ToLower() + "." + objEmpleado.EmpPaterno.ToLower());

                trDatos.Visible = true;
                btnGuardar.Visible = true;
            }
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session["objEmpleado" + Session.SessionID] = null;
        Response.Redirect("~/frmInicio.aspx");
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfIdUsuario.Value))
        {
            if (Session["objEmpleado" + Session.SessionID] != null)
            {
                clsEntUsuario objUsuario = new clsEntUsuario();
                objUsuario.Perfil = new clsEntPerfil();

                objUsuario.UsuLogin = txbUsuario.Text.Trim();
                objUsuario.UsuContrasenia = clsSegRijndaelSimple.Encrypt(txbContrasenia.Text.Trim());
                objUsuario.IdEmpleado = ((clsEntEmpleado)Session["objEmpleado" + Session.SessionID]).IdEmpleado;
                objUsuario.Perfil.IdPerfil = Convert.ToInt16(ddlPerfil.SelectedValue);
                if (ddlPerfil.SelectedItem.Text == "ADMINISTRADOR")
                {
                    objUsuario.Administrador = 1;
                }

                string mensaje = clsNegUsuario.crearUsuario(objUsuario,
                                                            (clsEntSesion)Session["objSession" + Session.SessionID]);

                switch (mensaje)
                {
                    case "Operación finalizada correctamente.":
                        grvUsuario.DataSource = clsDatCatalogos.consultaCatalogo("seguridad.spConsultarUsuarios",
                                                                                 (clsEntSesion)
                                                                                 Session["objSession" + Session.SessionID]);
                        grvUsuario.DataBind();
                        limpiarCampos();
                        divOk.Visible = true;
                        divError.Visible = false;
                        timOk.Enabled = true;
                        lblOk.Text = mensaje;
                        break;
                    case "La Contraseña no cumple el nivel de seguridad requerido.":
                        divOk.Visible = false;
                        divError.Visible = true;
                        timError.Enabled = true;
                        lblError.Text = mensaje;
                        break;
                    case "El nombre de usuario ya existe.":
                        divOk.Visible = false;
                        divError.Visible = true;
                        timError.Enabled = true;
                        lblError.Text = mensaje;
                        break;
                    case "Ha ocurrido un error, inténtelo nuevamente.":
                        divOk.Visible = false;
                        divError.Visible = true;
                        timError.Enabled = true;
                        lblError.Text = mensaje;
                        break;
                }
            }
            else
            {
                string busqueda = "Generales/frmBusqueda.aspx?Redirect=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "") + "&Cancel=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "");
                string script = "if(confirm('¡Para crear un Usuario es necesario seleccionar un Empleado!.')) location.href='./../" + busqueda + "';";
                ClientScript.RegisterClientScriptBlock(GetType(), "Mensaje", script, true);
            }
        }
        else if (!string.IsNullOrEmpty(hfIdUsuario.Value))
        {
            clsEntUsuario objUsuario = new clsEntUsuario();
            objUsuario.Perfil = new clsEntPerfil();

            objUsuario.IdUsuario = Convert.ToInt16(hfIdUsuario.Value);
            objUsuario.Perfil.IdPerfil = Convert.ToInt16(ddlPerfil.SelectedValue);

            if (clsDatUsuario.actualizarUsuario(objUsuario, (clsEntSesion)Session["objSession" + Session.SessionID]))
            {
                divOk.Visible = true;
                divError.Visible = false;
                timOk.Enabled = true;
                lblOk.Text = "Operación finalizada correctamente.";
                grvUsuario.DataSource = clsDatCatalogos.consultaCatalogo("seguridad.spConsultarUsuarios", (clsEntSesion)Session["objSession" + Session.SessionID]);
                grvUsuario.DataBind();
            }
            else
            {
                divOk.Visible = false;
                divError.Visible = true;
                timError.Enabled = true;
                lblError.Text = "Ha ocurrido un error, inténtelo nuevamente.";
            }
        }
        limpiarCampos();
    }

    private void limpiarCampos()
    {
        Session["objEmpleado" + Session.SessionID] = null;

        lblPaterno.Text = "";
        lblMaterno.Text = "";
        lblNombre.Text = "";
        lblSexo.Text = "";
        txbUsuario.Text = "";
        txbContrasenia.Text = "";
        txbConfirmacion.Text = "";
        ddlPerfil.ClearSelection();

        txbUsuario.Enabled = true;
        trdContrasenia.Visible = true;
        triContrasenia.Visible = true;
        trdConfirmacion.Visible = true;
        triConfirmacion.Visible = true;

        trDatos.Visible = false;
        btnGuardar.Visible = false;
    }

    protected void grvUsuario_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in grvUsuario.Rows)
        {
            Label lbl = (Label)gvr.FindControl("lblIndice");
            lbl.Text = Convert.ToString(gvr.RowIndex + 1);
        }
    }

    protected void grvUsuario_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow gvr = grvUsuario.Rows[e.RowIndex];
        StringBuilder sbUrl = new StringBuilder();

        if (!string.IsNullOrEmpty(((Label)gvr.FindControl("lblIdUsuario")).Text))
        {
            clsEntUsuario objUsuario = new clsEntUsuario();
            objUsuario.IdUsuario = Convert.ToInt16(((Label)gvr.FindControl("lblIdUsuario")).Text);

            if (clsDatUsuario.eliminarUsuario(objUsuario, (clsEntSesion)Session["objSession" + Session.SessionID]))
            {
                grvUsuario.DataSource = clsDatCatalogos.consultaCatalogo("seguridad.spConsultarUsuarios", (clsEntSesion)Session["objSession" + Session.SessionID]);
                grvUsuario.DataBind();
                return;
            }

            sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est101&strMensaje=" + Server.UrlEncode("Ha ocurrido un error durante la operación. Intentelo más tarde ó contacte a un Administrador."));
            Response.Redirect(sbUrl.ToString());
        }
    }

    protected void grvUsuario_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow gvr = grvUsuario.Rows[e.RowIndex];
        trDatos.Visible = true;
        btnGuardar.Visible = true;

        if (!string.IsNullOrEmpty(((Label)gvr.FindControl("lblIdUsuario")).Text))
        {
            hfIdUsuario.Value = ((Label)gvr.FindControl("lblIdUsuario")).Text;
        }
        clsUtilerias.llenarLabel(lblPaterno, ((Label)gvr.FindControl("lblPaterno")).Text, "-");
        clsUtilerias.llenarLabel(lblMaterno, ((Label)gvr.FindControl("lblMaterno")).Text, "-");
        clsUtilerias.llenarLabel(lblNombre, ((Label)gvr.FindControl("lblNombre")).Text, "-");
        clsUtilerias.llenarLabel(lblSexo, ((Label)gvr.FindControl("lblSexo")).Text.Trim(), "-");
        clsUtilerias.llenarTextBox(txbUsuario, ((Label)gvr.FindControl("lblUsuario")).Text);
        txbUsuario.Enabled = false;
        trdContrasenia.Visible = false;
        triContrasenia.Visible = false;
        trdConfirmacion.Visible = false;
        triConfirmacion.Visible = false;
        clsUtilerias.seleccionarDropDownList(ddlPerfil, ((Label)gvr.FindControl("lblIdPerfil")).Text);
    }

    protected void timOk_Tick(object sender, EventArgs e)
    {
        divOk.Visible = false;
        timOk.Enabled = false;
    }

    protected void timError_Tick(object sender, EventArgs e)
    {
        divError.Visible = false;
        timError.Enabled = false;
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        string busqueda = "Generales/frmBusqueda.aspx?Redirect=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "") + "&Cancel=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "");
        Response.Redirect("~/" + busqueda);
    }
    protected void btnAgregarServicioInstalacion_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hfIdUsuario.Value))
        {
            Response.Redirect(string.Format("~/Usuarios/frmUsuarioInstalacion.aspx?IdUsuario={0}", hfIdUsuario.Value));
        }
    }
}
