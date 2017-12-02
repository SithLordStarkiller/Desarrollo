using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using proUtilerias;

public partial class Reportes_frmEstadoFuerzaHomologado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
     
    }



    protected void btnGenerar_Click(object sender, EventArgs e)
    {

        
        Response.Redirect("~/Reportes/frmRvEstadoFuerzaHomologado.aspx");
    }
}