using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Universidad.Controlador.Login;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.WebAdministrativa.Account
{
    public partial class Login : Page
    {
        private Sesion _sesion;
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            var url = new AppSettingsReader().GetValue("ServidorInternoURL",typeof(string));
            _sesion = new Sesion() {Conexion = url.ToString()};
            var servicios = new SVC_LoginAdministrativos(_sesion);
            var usuario = lLogin.UserName;
            var contrasena = lLogin.Password;
            _sesion.Usuario = usuario;
            _sesion.Contrasena = contrasena;
            _sesion.Conexion = url.ToString();
            _sesion.RecordarContrasena = lLogin.DisplayRememberMe;
            servicios.LoginAdministrativo(usuario,contrasena);
            servicios.LoginAdministrativosFinalizado += servicios_LoginAdministrativosFinalizado;
        }

        void servicios_LoginAdministrativosFinalizado(US_USUARIOS usuario)
        {
            lLogin.UserName = "registrado";
            (HttpContext.Current.Session["Sesion"]) = _sesion;
        }
    }
}