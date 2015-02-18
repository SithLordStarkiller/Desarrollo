using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Universidad.Controlador;
using Universidad.Controlador.Login;
using Universidad.Controlador.Personas;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.WebAdministrativaPrueba
{
    public partial class SiteMaster : MasterPage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            Page.Load += Page_Load;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void lsLogOff_OnLoggingOut(object sender, LoginCancelEventArgs e)
        {
        }
    }

}