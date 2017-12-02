using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using proEntidad;
using proNegocio;
using System.Web.SessionState;


namespace mvcSICER.Generales
{
    /// <summary>
    /// Summary description for handlerPDF
    /// </summary>
    public class handlerPDF : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            clsEntResponseImagen objRespImagen = new clsEntResponseImagen();
            objRespImagen = clsNegImagen.obtienePDFExamen(Convert.ToString(context.Request.QueryString["nombreArchivo"]));
            // objRespImagen = clsNegImagen.obtieneImagenExamen(Convert.ToString(context.Request.QueryString["nombreArchivo"]));

            if (objRespImagen.response == "")
            {
                context.Response.ContentType = "application/pdf";
                context.Response.BinaryWrite(objRespImagen.byteImagen);
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(objRespImagen.response);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}