using System;

namespace SICOGUA.Presentacion
{
    public partial class frmInicio : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["impresion" + Session.SessionID] = null;
            if (Session["objSession" + Session.SessionID] == null)
            {
                Response.Redirect("~/_Default.aspx?strError=" + Server.UrlEncode("X500"));
            }
        }
    }
}