using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Universidad.Controlador.Login;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.WebAdministrativaPrueba
{
    public partial class Login : System.Web.UI.Page
    {
        private Sesion _sesion;
        protected void Page_Load(object sender, EventArgs e)
        {
            _sesion = new Sesion();
            _sesion.Conexion = (string)new AppSettingsReader().GetValue("RutaServidorInterno", typeof(string));
            HttpContext.Current.Session["Usuario"] = "Usuario";
            HttpContext.Current.Session["Sesion"] = "Sesion";
        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            try
            {
                var usuario = tbxUsuario.Text;
                var contrasena = tbxContrasena.Text;
                var login = new SVC_LoginAdministrativos(_sesion);

                login.LoginAdministrativo(usuario, contrasena);
                login.LoginAdministrativosFinalizado += Login_LoginAdministrativosFinalizado;
            }
            catch (Exception exception)
            {

            }
        }

        private void Login_LoginAdministrativosFinalizado(Entidades.US_USUARIOS usuario)
        {
            if (usuario != null)
            {
                Session["Usuario"] = usuario;
                Session["Sesion"] = _sesion;
                Response.Redirect("Default.aspx");
            }
        }
    }
}