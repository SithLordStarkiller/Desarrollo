using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Universidad.Controlador.Login;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.WebAdministrativaPrueba
{
    public partial class Login : System.Web.UI.Page
    {
        private Sesion _sesion;

        protected void Page_Load(object sender, EventArgs e)
        {
            _sesion = new Sesion
            {
                Conexion = (string) new AppSettingsReader().GetValue("RutaServidorInterno", typeof (string))
            };

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
                ScriptManager.RegisterStartupScript(udpPanel, typeof(Page), "Ingreso exitosos",
                           "alert('Hay un problema con el servidor o la conexion');",
                           true);
            }
        }

        private void Login_LoginAdministrativosFinalizado(US_USUARIOS usuario)
        {
            if (usuario != null)
            {
                Session["Usuario"] = usuario;
                Session["Sesion"] = _sesion;
                switch (usuario.ID_ESTATUS_USUARIOS)
                {
                    case 1:
                        {
                            Response.Redirect("Default.aspx");
                        }
                        break;

                    case 2:
                        {
                            ScriptManager.RegisterStartupScript(udpPanel, typeof(Page), "Ingreso exitosos",
                          "Mensage('El usuario se encuentra suspendido');",
                          true);
                        }
                        break;
                    
                    case 3:
                        {
                            ScriptManager.RegisterStartupScript(udpPanel, typeof(Page), "Ingreso exitosos",
                          "Mensage('El usuario se encuentra cancelado');",
                          true);
                        }
                        break;
                }


            }
            else
            {
                ScriptManager.RegisterStartupScript(udpPanel, typeof(Page), "Ingreso exitosos",
                           "ErrorLogin();",
                           true);
            }
        }
    }
}