using System;
using SICOGUA.Seguridad;

namespace SICOGUA
{
    public partial class Generales_MasterPage : System.Web.UI.MasterPage
    {

        private void Page_Load(object sender, EventArgs e)
        {
            if (Session["objSession" + Session.SessionID] == null)
            {
                Response.Redirect("~/Default.aspx?strError=" + Server.UrlEncode("X500"));
            }
            if (!IsPostBack)
            {
                lblFecha.Text = DateTime.Now.ToString("D").ToUpper();

                if (!Equals(Session["objSession" + Session.SessionID], null))
                {
                    clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];
                    lblUsuario.Text = objSesion.usuario.UsuPaterno + " " + objSesion.usuario.UsuMaterno + " " +
                                      objSesion.usuario.UsuNombre + " [" + objSesion.usuario.Perfil.PerDescripcion + "]";

                    clsEntPermiso objPermiso = new clsEntPermiso();
                    objPermiso.IdPerfil = objSesion.usuario.Perfil.IdPerfil;
                    clsNegPermiso.aplicarPermisos(mnPrincipal, objPermiso, objSesion);

                    if (!Page.AppRelativeVirtualPath.Equals("~/frmInicio.aspx"))
                    {
                        if (!clsNegPermiso.permitirPagina(objPermiso, Page.AppRelativeVirtualPath, objSesion))
                        {
                            Response.Redirect("~/frmInicio.aspx");
                        }
                    }
                }
            }
        }

    }
}
