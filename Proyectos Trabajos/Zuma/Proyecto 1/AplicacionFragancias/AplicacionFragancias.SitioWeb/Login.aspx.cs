using System;
using AplicacionFragancias.LogicaNegocios.OperacionSistema;

namespace AplicacionFragancias.SitioWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            var usuario = tbxUsuario.Text;
            var contrasena = tbxContrasena.Text;

            var login = new OperacionSistema().ObtenUsuario(usuario, contrasena);

            if (login != null)
            {
                Session["Sesion"] = login;
                Response.Redirect("index.aspx");
            }

        }
    }
}