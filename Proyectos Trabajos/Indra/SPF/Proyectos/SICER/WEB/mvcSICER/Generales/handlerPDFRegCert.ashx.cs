using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using proEntidad;

namespace mvcSICER.Generales
{
    /// <summary>
    /// Summary description for handlerPDFRegCert
    /// </summary>
    public class handlerPDFRegCert : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            List<clsEntResponseImagen> listaPDFs = new List<clsEntResponseImagen>();
            //clsEntResponseImagen objRespImagen = new clsEntResponseImagen();
            // objRespImagen = clsNegImagen.obtienePDFExamen(Convert.ToString(context.Request.QueryString["nombreArchivo"]));
            // objRespImagen = clsNegImagen.obtieneImagenExamen(Convert.ToString(context.Request.QueryString["nombreArchivo"]));

            if (System.Web.HttpContext.Current.Session["lstPDFs" + System.Web.HttpContext.Current.Session.SessionID] != null)
            {   listaPDFs = (List<clsEntResponseImagen>)System.Web.HttpContext.Current.Session["lstPDFs" + System.Web.HttpContext.Current.Session.SessionID];

                context.Response.ContentType = "application/pdf";
                context.Response.BinaryWrite(listaPDFs.Find(x => x.identificador == context.Request.QueryString["identificador"]).byteImagen);
            
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Ocurrió un error al cargar el Documento");
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