using System;
using System.Data.Common;
using SICOGUA.Seguridad;

namespace SICOGUA.Presentacion
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblFecha.Text = DateTime.Now.ToString("D").ToUpper();
                txtUsuario.Focus();
                ViewState["numIntentos"] = 0;
                Session.Clear();

                if (Request.QueryString["mensaje"] != null)
                {
                    string mensaje = clsSegRijndaelSimple.Decrypt(Request.QueryString["mensaje"]);
                    txtUsuario.Text = mensaje.Split('|')[0];
                    txtPassword.Text = mensaje.Split('|')[1];
                    btnEntrar_Click(null, null);
                    return;
                }

                if (Request.QueryString["strError"] != null)
                {
                    if (Request.QueryString["strError"] == "X500")
                    {
                        mensaje("&nbsp;Antes de Ingresar, Debes Iniciar Sesión.&nbsp;", true);
                    }
                    else
                    {
                        lblError.Text = "";
                        divMensajes.Visible = false;
                    }
                }
                else
                {
                    lblError.Text = "";
                    divMensajes.Visible = false;
                }
            }
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            mensaje("", false);

            clsEntSesion objSession = new clsEntSesion();
            objSession.usuario = new clsEntUsuario();

            if (!string.IsNullOrEmpty(txtUsuario.Text.Trim()) && !string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                objSession.usuario.UsuLogin = txtUsuario.Text.Trim();
                objSession.usuario.UsuContrasenia = clsSegRijndaelSimple.Encrypt(txtPassword.Text.Trim());
            }
            else if (txtUsuario.Text.Trim() == "" && txtPassword.Text.Trim() != "")
            {
                mensaje("&nbsp;Debe Ingresar Nombre de Usuario.&nbsp;", true);
                txtUsuario.Focus();
                return;
            }
            else if (txtUsuario.Text.Trim() != "" && txtPassword.Text.Trim() == "")
            {
                mensaje("&nbsp;Debe Ingresar Contraseña.&nbsp;", true);
                txtPassword.Focus();
                return;
            }
            else
            {
                mensaje("&nbsp;Debe Ingresar Nombre de Usuario y Contraseña.&nbsp;", true);
                txtUsuario.Focus();
                return;
            }

            try
            {
                clsNegUsuario.consultarPermiso(ref objSession);
            }
            catch (DbException sqlEx)
            {
                if(sqlEx.GetType() == typeof(System.Data.SqlClient.SqlException))
                {
                    if(((System.Data.SqlClient.SqlException)sqlEx).Number == 18456)
                    {
                        ViewState["numIntentos"] = (int)ViewState["numIntentos"] + 1;
                        mensaje("&nbsp;Nombre de Usuario y/o Contraseña Incorrecto.&nbsp;", true);
                        txtPassword.Focus();
                        return;
                    }
                }
                else
                {
                    mensaje("&nbsp;Ha ocurrido un error, inténtelo más tarde.&nbsp;", true);
                }
            }
            catch (Exception ex)
            {
                mensaje("&nbsp;Ha ocurrido un error, inténtelo más tarde.&nbsp;", true);
            }
            

            if (objSession.usuario.IdUsuario != 0)
            {
                objSession.inicio = DateTime.Now;
                objSession.ip = Request.UserHostAddress;
                objSession.sessionId = Session.SessionID;
                objSession.intentos = (int)ViewState["numIntentos"];
                objSession.estatus = clsEntSesion.tipoEstatus.Activa;
               

                Session["objSession" + Session.SessionID] = objSession;
                int iResultado = 0;

                try
                {
                    iResultado = clsDatSesion.iniciarSesion(objSession);
                }
                catch (Exception ex)
                {
                    mensaje("&nbsp;Ha ocurrido un error, inténtelo más tarde.&nbsp;", true);
                }

                switch (iResultado)
                {
                    case 0:
                        mensaje("&nbsp;El usuario ya ha iniciado sesión.&nbsp;", true);
                        break;
                    case 1:
                        Response.Redirect("~/frmInicio.aspx");
                        break;
                }
            }
            else
            {
                ViewState["numIntentos"] = (int)ViewState["numIntentos"] + 1;
                mensaje("&nbsp;Tu cuenta ha sido bloqueada.&nbsp;", true);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            txtPassword.Text = "";
            lblError.Text = "";
        }

        protected void timLogon_Tick(object sender, EventArgs e)
        {
            mensaje("", false);
        }

        private void mensaje(string mensaje, bool visible)
        {
            lblError.Text = mensaje;
            divMensajes.Visible = visible;
            timLogon.Enabled = visible;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session.Clear();
            ClientScript.RegisterClientScriptBlock(GetType(), "Salir", "window.close();", true);
        }
}
}