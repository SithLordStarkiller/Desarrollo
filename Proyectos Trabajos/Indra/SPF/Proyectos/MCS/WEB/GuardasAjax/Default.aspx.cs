using System;
using System.Text;

public partial class init : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["objSession" + Session.SessionID] = null;
        Session["objEmpleado" + Session.SessionID] = null;
        Session["lisHorarios"] = null;
        lblFecha.Text = DateTime.Now.ToString("D").ToUpper();
        const string strParametros = "toolbar=no,location=no, directories=no, status=no, menubar=no ,scrollbars=yes, resizable=no, fullscreen=yes";
        StringBuilder sbScript = new StringBuilder();
        sbScript.Append("window.open('_Default.aspx','window','" + strParametros + "');");
        ClientScript.RegisterClientScriptBlock(GetType(), "Inicio", sbScript.ToString(), true);
    }
    protected void lnkIniciar_Click(object sender, EventArgs e)
    {
        Session["objSession" + Session.SessionID] = null;
        Session["objEmpleado" + Session.SessionID] = null;
        Session["lisHorarios"] = null;
        const string strParametros = "toolbar=no,location=no, directories=no, status=no, menubar=no ,scrollbars=yes, resizable=no, fullscreen=yes";
        StringBuilder sbScript = new StringBuilder();
        sbScript.Append("window.open('_Default.aspx','window','" + strParametros + "');");
        ClientScript.RegisterClientScriptBlock(GetType(), "Inicio", sbScript.ToString(), true);
    }
    protected void imgIniciar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Session["objSession" + Session.SessionID] = null;
        Session["objEmpleado" + Session.SessionID] = null;
        Session["lisHorarios"] = null;
        const string strParametros = "toolbar=no,location=no, directories=no, status=no, menubar=no ,scrollbars=yes, resizable=no, fullscreen=yes";
        StringBuilder sbScript = new StringBuilder();
        sbScript.Append("window.open('_Default.aspx','window','" + strParametros + "');");
        ClientScript.RegisterClientScriptBlock(GetType(), "Inicio", sbScript.ToString(), true);
    }
}
