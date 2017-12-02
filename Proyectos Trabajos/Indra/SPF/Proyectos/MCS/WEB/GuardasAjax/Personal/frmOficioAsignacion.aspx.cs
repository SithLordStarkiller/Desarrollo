using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Personal_frmOficioAsignacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        MemoryStream mem = new MemoryStream();
        mem = (MemoryStream)Session["ftbMensaje"];

        try
        {
            //EJECUTAR EL REPORTE
            //mem = clsGeneraPDF.generaPDF(ftbMensaje.Xhtml);
            //mem = clsGeneraPDF.generaPDF(Request.QueryString["ftbMensaje"]);

            if (mem != null)
            {
                Response.Clear();
                Response.AppendHeader("Content-Disposition", "filename=test.pdf");
                Response.ContentType = "application/pdf";
                Response.OutputStream.Write(mem.GetBuffer(), 0, mem.GetBuffer().Length);
                Response.OutputStream.Flush();
                mem.Close();
                Response.End();
            }
        }
        catch (Exception ex)
        {

        }
    }
}