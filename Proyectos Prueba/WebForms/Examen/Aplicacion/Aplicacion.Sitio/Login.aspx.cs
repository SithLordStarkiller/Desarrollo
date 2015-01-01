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

        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            var ln = new LoginL();
            
            try
            {
                var empleado = ln.LoginPersonas(txbCorreoElectronico.Text, txbContrasena.Text);
                
                if (empleado != null)
                {
                    var jEmpleado = JsonConvert.SerializeObject(empleado);
                    Response.Redirect("index.aspx?Token=" + jEmpleado);
                }
                else
                {
                    lblErrorLogin.Visible = true;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}