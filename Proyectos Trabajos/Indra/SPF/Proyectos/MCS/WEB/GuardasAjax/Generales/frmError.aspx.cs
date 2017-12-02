using System;

public partial class Generales_frmError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["objSession" + Session.SessionID] == null)
        {
            Response.Redirect("~/_Default.aspx?strError=" + Server.UrlEncode("X500"));
        }
    }
}
