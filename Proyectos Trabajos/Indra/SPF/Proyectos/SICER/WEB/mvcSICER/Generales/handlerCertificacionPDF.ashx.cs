using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using proEntidad;
using proNegocio;
using System.Web.SessionState;
using System.IO;

using System.Web.UI.WebControls;

namespace mvcSICER.Generales
{
    /// <summary>
    /// Summary description for handlerCertificacionPDF
    /// </summary>
    public class handlerCertificacionPDF : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            List<string> arrImagenes = new List<string>();

            arrImagenes.Add(HttpContext.Current.Server.MapPath("~/Imagenes/CabeceraPlecaSmall.png"));
            arrImagenes.Add(HttpContext.Current.Server.MapPath("~/Imagenes/BackgroundCertificacionPDF.png"));
            arrImagenes.Add(HttpContext.Current.Server.MapPath("~/Imagenes/grayBackground.png"));
            arrImagenes.Add(HttpContext.Current.Server.MapPath("~/Imagenes/blackBackground.png"));

            List<string> arrFirma = new List<string>();
            List<spuConsultarFirmasCertificado_Result> firma = clsNegCatalogos.consultarFirmasCertificado();

            arrFirma.Add(firma[0].fcJerarquia);  // Jerarquia
            arrFirma.Add(firma[0].fcNombre);  // Nombre
            arrFirma.Add(firma[0].fcCargo);  // Cargo

            string datosPDF = HttpContext.Current.Request.QueryString["strDatosPersona"];
            //int tamaño = datosPDF.Length;
            //for (int i = 0; i < tamaño; i++)
            {
                datosPDF = datosPDF.Replace('~', ' '); //El simbolo ¬ no lo reconoce y en el documento aparecen todas las palabras juntas
            }

            // Tomar CURP y idCertificacionRegistro para guardar Archivo PDF

            string[] datos = datosPDF.Split('|');
            //CAMBIA para que esta ruta no este directamente en códoigo
            string certificadoPDF = "/certificaciones/" + datos[9] + "_" + datos[3] + ".pdf";
            int idCertificacionRegistro = Convert.ToInt32(datos[9]);

            
            // Dentro de if. Poner parametro de comprobacion en el metodo para el nombre del archivo. Revisar Document docPdf

            clsEntResponseImagen pdf = clsNegImagen.obtienePDFExamen(certificadoPDF);

            if (pdf.response == "" || pdf.response == null)
            {
                context.Response.ContentType = "application/pdf";
                context.Response.BinaryWrite(pdf.byteImagen);
            }
            else
            {

                //MemoryStream mem = new MemoryStream();
                //mem = clsNegCertificacionPDF.generaPDFPersonalizado(arrImagenes, arrFirma, datosPDF, certificadoPDF, idCertificacionRegistro);

                clsEntResponseCertificado objResCertificado = new clsEntResponseCertificado();
                objResCertificado = clsNegCertificacionPDF.generaPDFPersonalizado(arrImagenes, arrFirma, datosPDF, certificadoPDF, idCertificacionRegistro);

                try
                {
                    if (objResCertificado.alerta == string.Empty)
                    {
                        context.Response.Clear();
                        context.Response.AppendHeader("Content-Disposition", "filename=test.pdf");
                        context.Response.ContentType = "application/pdf";
                        context.Response.OutputStream.Write(objResCertificado.memCertificado.GetBuffer(), 0, objResCertificado.memCertificado.GetBuffer().Length);
                        context.Response.OutputStream.Flush();
                        objResCertificado.memCertificado.Close();
                        context.Response.End();
                    }
                    else {
                        context.Response.ContentType = "text/plain";
                        context.Response.Write(objResCertificado.alerta);
                    }
                }
                catch (Exception ex)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Ocurrió un error al cargar el Documento: " + ex.Message);
                }
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