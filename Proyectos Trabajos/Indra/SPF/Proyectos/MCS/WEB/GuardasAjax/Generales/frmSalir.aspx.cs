using System;
using System.Text;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;

public partial class Generales_frmSalir : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            const string strParametros = "status:no;edge:sunken;dialogHide:true;help:no;dialogWidth:500px;dialogHeight:200px;scroll:no";
            StringBuilder sbScript = new StringBuilder();
            try
            {
                if (Session["objSession" + Session.SessionID] == null)
                {
                    Response.Redirect("~/_Default.aspx?strError=" + Server.UrlEncode("X500"));
                }

                lblFecha.Text = DateTime.Now.ToString("D").ToUpper();
                clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];
                objSesion.estatus = clsEntSesion.tipoEstatus.Finalizada;
                Session["objSession" + Session.SessionID] = null;
                clsDatSesion.finalizarSesion(objSesion);
            }
            catch (Exception)
            {
                sbScript.Append("showModelessDialog('./../Generales/frmMensaje.aspx?strMensaje=" + Server.UrlEncode("Ha ocurrido un error, inténtelo más tarde") +
                                                "&strTipo=Error',window,'" + strParametros + "');");
                ClientScript.RegisterClientScriptBlock(GetType(), "MensajeError", sbScript.ToString(), true);
            }
        }
    }

    protected void imgIniciar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Session.Clear();
        Response.Redirect("~/_Default.aspx");
    }

    protected void imgSalir_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Session.Clear();
        ClientScript.RegisterClientScriptBlock(GetType(), "Salir", "window.close();", true);
    }
}
