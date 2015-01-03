using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplicacion.Entidades;
using Aplicacion.LogicaNegocio;
using Newtonsoft.Json;

namespace Aplicacion.Sitio
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Session["UserInfo"] != null)
            {
                Response.Redirect("index.aspx");
            }
        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            var ln = new LoginL();
            
            try
            {
                var empleado = ln.LoginPersonas(txbCorreoElectronico.Text, txbContrasena.Text);
                
                if (empleado != null)
                {
                    Session["UserInfo"] = empleado;
                    Response.Redirect("index.aspx");
                }
                else
                {
                    lblErrorLogin.Visible = true;
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page),
                            "Mensaje",
                            "alert('Error en el sistema');", true);
            }
        }
    }
}