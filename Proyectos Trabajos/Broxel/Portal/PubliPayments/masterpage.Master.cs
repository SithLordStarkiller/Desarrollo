using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using PubliPayments.Utiles;

namespace PubliPayments
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public string LogosDir = "", NombreAplicacion = "";
        public bool IsOriginacion = false;
        public int aplicacion = -1;
        public bool EsCallCenter = Config.EsCallCenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Application["Aplicacion"] == null)
                Response.Redirect("/Errores.html");

            Logger.WriteLine(Logger.TipoTraceLog.Trace,
                Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)),
                Path.GetFileName(Request.PhysicalPath),
                IsPostBack
                    ? "Trace PostBack"
                    : "Trace");
            LogosDir = Config.AplicacionActual().Nombre;
            aplicacion = Config.AplicacionActual().idAplicacion;
            IsOriginacion = Config.AplicacionActual().Nombre.Contains("OriginacionMovil");
            NombreAplicacion = Config.AplicacionActual().Nombre.ToUpper();
            try
            {
                var nombre = SessionUsuario.ObtenerDato(SessionUsuarioDato.NombreUsuario);
                lblNombre.Text = String.Format("{0} <br/> {1} ",
                    nombre.Length > 22 ? nombre.Substring(0, 22) + "..." : nombre
                    ,SessionUsuario.ObtenerDato(SessionUsuarioDato.NombreRol).Replace("London", (Config.AplicacionActual().idAplicacion==1)?"Infonavit":"General"));
                lblNombreDespacho.Text = SessionUsuario.ObtenerDato(SessionUsuarioDato.NombreDominio);
            }
            catch
            {
                lblNombre.Text = HttpContext.Current.User.Identity.Name;
            }
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            Response.Redirect("OrdenesAsignadas.aspx");
        }

        protected void menuPrincipal_MenuItemClick(object sender, MenuEventArgs e)
        {

        }

        protected void LoginLink_OnClick(object sender, EventArgs e)
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}