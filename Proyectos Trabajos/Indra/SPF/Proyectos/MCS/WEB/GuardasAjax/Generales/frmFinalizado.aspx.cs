using System;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Generales_frmFinalizado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["objSession" + Session.SessionID] == null)
        //{
        //    Response.Redirect("~/_Default.aspx?strError=" + Server.UrlEncode("X500"));
        //}
        if (!IsPostBack)
        {
            if (Request.QueryString.Count == 3)
            {
                if (Request.QueryString["hyper"] == "frmAdignacionesMasivas")
                {
                    Button1.Visible = true;
                }
                else
                {
                    Button1.Visible = false;
                }
            }
            else
            {
                Button1.Visible = false;
            }



            if (Request.QueryString["strEstatus"] != null)
            {
                switch (Request.QueryString["strEstatus"])
                {
                    // Finalizado Correctamente
                    case "est100":
                        imgIcon.ImageUrl = "~/Imagenes/Sign-Select-icon.png";
                        break;
                    // Finalizado Incorrectamente
                    case "est101":
                        imgIcon.ImageUrl = "~/Imagenes/Sign-Error-icon.png";
                        break;
                    // Advertencia
                    case "est102":
                        imgIcon.ImageUrl = "~/Imagenes/Symbol-Exclamation.png";
                        break;
                    // Ningun Caso
                    default:
                        Response.Redirect("~/frmInicio.aspx");
                        break;
                }
            }
            if (Request.QueryString["strMensaje"] != null)
            {
                lblMensaje.Text = Server.UrlDecode(Request.QueryString["strMensaje"]);
                return;
            }
            Response.Redirect("~/frmInicio.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "impresion", "ini()", true);
    }
    protected void lnkContinuar_Click(object sender, EventArgs e)
    {
        Session["impresion" + Session.SessionID] = null;
        Response.Redirect("~/frmInicio.aspx");
    }
}
