using System;
using AjaxControlToolkit;

public partial class Generales_wucMensaje : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMensaje.Text = "";
            divMensaje.Visible = false;
        }
    }

    public void Mensaje(string mensaje)
    {
        lblMensaje.Text = mensaje;
        divMensaje.Visible = true;
        timMensaje.Enabled = true;
    }

    protected void timMensaje_Tick(object sender, EventArgs e)
    {
        lblMensaje.Text = "";
        divMensaje.Visible = false;
        timMensaje.Enabled = false;
    }
}
