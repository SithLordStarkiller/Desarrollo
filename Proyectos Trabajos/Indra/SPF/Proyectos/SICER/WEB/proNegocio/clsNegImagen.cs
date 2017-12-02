using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using proEntidad;
using proDatos;
using System.IO;

namespace proNegocio
{
    public class clsNegImagen
    {

        public static clsEntResponseImagen obtieneImagen(HttpRequestBase Request)
        {
            int maxLen = 100;
            string alerta = string.Empty;
            clsEntResponseImagen objResponse = new clsEntResponseImagen();
            byte[] foto;

            if (Request.Files[0].ContentLength > ((maxLen * 1024)))
            {
                alerta = "El archivo excede el tamaño límite de " + maxLen.ToString() + " Kb";
            }
            else
            {
                try
                {
                    using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                    {
                        foto = binaryReader.ReadBytes(Request.Files[0].ContentLength);

                    }

                    objResponse.strImagen = Convert.ToBase64String(foto);

                }
                catch (Exception ex)
                {
                    alerta = ex.Message;

                }
            }
            objResponse.response = alerta;

            return objResponse;

        }

        public static clsEntResponseImagen cargaImagenPDF(HttpRequestBase Request)
        {
            string alerta = string.Empty;
            clsEntResponseImagen objResponse = new clsEntResponseImagen();
            List<clsEntResponseImagen> listaPDFs = new List<clsEntResponseImagen>();

            byte[] imgDocByte;

            
                if (Request.Files[0].ContentType == "application/pdf")
                {
                    objResponse.tipo = "P";
                    if (Request.Files[0].ContentLength > ((2500 * 1024)))
                    {
                        alerta = "El PDF excede el tamaño límite de 2 Mb";
                    }
                }else{
                    objResponse.tipo = "I";
                    if (Request.Files[0].ContentLength > ((100 * 1024)))
                    {
                        alerta = "La imagen excede el tamaño límite de 100 Kb";
                    }
                }

                if(alerta==string.Empty)
                {
                        try
                        {
                            using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                            {
                                imgDocByte = binaryReader.ReadBytes(Request.Files[0].ContentLength);
                            }
                            
                            Guid nuevoid = Guid.NewGuid();

                            //Mejorar ya que los PDF o imagenes se van acumulando en la misma sesión
                            if (System.Web.HttpContext.Current.Session["lstPDFs" + System.Web.HttpContext.Current.Session.SessionID] != null)
                                listaPDFs = (List<clsEntResponseImagen>)System.Web.HttpContext.Current.Session["lstPDFs" + System.Web.HttpContext.Current.Session.SessionID];

                            listaPDFs.Add(new clsEntResponseImagen { identificador = nuevoid.ToString(), byteImagen = imgDocByte });
                            System.Web.HttpContext.Current.Session["lstPDFs" + System.Web.HttpContext.Current.Session.SessionID]=listaPDFs;
                            objResponse.identificador = nuevoid.ToString();

                            if (objResponse.tipo=="I") objResponse.strImagen = Convert.ToBase64String(imgDocByte);
                            
                        }
                        catch (Exception ex)
                        {
                            alerta = ex.Message;

                        }
                }
            
            objResponse.response = alerta;

            return objResponse;

        }

        public static clsEntResponseImagen obtieneImagenExamen(string resNombreArchivo)
        {
            string alerta = string.Empty;
            clsEntResponseImagen objResponse = new clsEntResponseImagen();
            byte[] bImagen;
            try
            {

                if (System.Web.HttpContext.Current.Session["rutaServidor"] == null)
                    System.Web.HttpContext.Current.Session["rutaServidor"] = clsDatCatalogos.catalogoRutaServidor()[0].rsDescripcion;

                string rutaImagen = (string)System.Web.HttpContext.Current.Session["rutaServidor"] + resNombreArchivo;

                FileStream fsImagen = new FileStream(rutaImagen, FileMode.Open);


                using (BinaryReader binaryReader = new BinaryReader(fsImagen))
                {
                    bImagen = binaryReader.ReadBytes(Convert.ToInt32(fsImagen.Length));

                }
                objResponse.strImagen = Convert.ToBase64String(bImagen);

            }
            catch (Exception ex)
            {

                alerta += ex.Message;

            }
            objResponse.response = alerta;

            return objResponse;

        }

        public static clsEntResponseImagen obtieneImagenCertificacion(string resNombreArchivo, string identificador)
        {
            string alerta = string.Empty;
            clsEntResponseImagen objResponse = new clsEntResponseImagen();
            byte[] bImagen;
            try
            {
                
                
                if (identificador == "")
                {
                    if (System.Web.HttpContext.Current.Session["rutaServidor"] == null)
                        System.Web.HttpContext.Current.Session["rutaServidor"] = clsDatCatalogos.catalogoRutaServidor()[0].rsDescripcion;
                    
                    string rutaImagen = (string)System.Web.HttpContext.Current.Session["rutaServidor"] + resNombreArchivo;

                    FileStream fsImagen = new FileStream(rutaImagen, FileMode.Open);
                    
                    using (BinaryReader binaryReader = new BinaryReader(fsImagen))
                    {
                        bImagen = binaryReader.ReadBytes(Convert.ToInt32(fsImagen.Length));
                    }
                }
                else {

                    bImagen = ((List<clsEntResponseImagen>)System.Web.HttpContext.Current.Session["lstPDFs" + System.Web.HttpContext.Current.Session.SessionID]).Find(x => x.identificador == identificador).byteImagen;

                }
                
                objResponse.strImagen =Convert.ToBase64String(bImagen);

            }
            catch (Exception ex)
            {
                
                alerta = ex.Message;

            }
            objResponse.response = alerta;

            return objResponse;

        }
        
        public static clsEntResponseImagen almacenaImagenPersona(string imagen, string nombre)
        {
            clsEntResponseImagen objResponse = new clsEntResponseImagen();
            string alerta = string.Empty;
            string ruta;
            //string ruta = "D:\\tmp\\" + nombre + ".png";
            //string ruta = "\\Fileserver\\INSTALACION BIOMETRICOS\\" + nombre + ".jpg";

            //spuConsultarRutaServidor_Result RutaServidor = new spuConsultarRutaServidor_Result();


            try
            {

                if (HttpContext.Current.Session["rutaServidor"] == null)
                    HttpContext.Current.Session["rutaServidor"] = clsDatCatalogos.catalogoRutaServidor()[0].rsDescripcion;

                //RutaServidor = clsDatCatalogos.catalogoRutaServidor()[0];
                ruta = (string)HttpContext.Current.Session["rutaServidor"] + "\\personas\\" + nombre + ".png";
                objResponse.strImagen = "\\personas\\" + nombre + ".png";
                // Convert Base64 String to byte[]
                byte[] imageBytes = Convert.FromBase64String(imagen);
                //objResponse.byteImagen = imageBytes;
                var fs = new BinaryWriter(new FileStream(ruta, FileMode.Create, FileAccess.Write));
                fs.Write(imageBytes);
                fs.Close();
            }
            catch (Exception ex)
            {
                alerta = ex.Message;

            }

            objResponse.response = alerta;

            return objResponse;
        
        }

        public static clsEntResponseImagen guardaPDF(MemoryStream pdf, int idCertificacionRegistro)
        {
            clsEntResponseImagen objResponse = new clsEntResponseImagen();
            string alerta = string.Empty;
            string ruta;
            string nombre;

            try
            {
                nombre = clsDatImagen.nombreArchPDF(idCertificacionRegistro)[0].crNombreCertificado;
                
                //RutaServidor
                if (HttpContext.Current.Session["rutaServidor"] == null)
                    HttpContext.Current.Session["rutaServidor"] = clsDatCatalogos.catalogoRutaServidor()[0].rsDescripcion;

                ruta = (string)HttpContext.Current.Session["rutaServidor"] + nombre;
                objResponse.strImagen = nombre;

                // Local
                //ruta = "C:/Users/SPF/Documents/" + nombre + ".pdf";
                //objResponse.strImagen = "C:/Users/SPF/Documents/" + nombre + ".pdf";

                // Convert Base64 String to byte[]
                byte[] imageBytes = pdf.ToArray();
                //objResponse.byteImagen = imageBytes;
                var fs = new BinaryWriter(new FileStream(ruta, FileMode.Create, FileAccess.Write));
                fs.Write(imageBytes);
                fs.Close();

              //  clsNegCertificacionPDF.enviaCorreo(imageBytes, "certificado.pdf", "marcos.cortes@cns.gob.mx", "spfservicios.sql@cns.gob.mx","Envío de certificado SPF","Se le hace llegar a usted el certificado.");

            }
            catch (Exception ex)
            {
                alerta = ex.Message;

            }

            objResponse.response = alerta;

            return objResponse;
        }


        public static clsEntResponseImagen obtieneImagenPersona(string imagen)
        {
            clsEntResponseImagen objResponse = new clsEntResponseImagen();
            string alerta = string.Empty;
            string ruta;
            byte[] bImagen;
            try
            {
                if (HttpContext.Current.Session["rutaServidor"] == null)
                    HttpContext.Current.Session["rutaServidor"] = clsDatCatalogos.catalogoRutaServidor()[0].rsDescripcion;

                //RutaServidor = clsDatCatalogos.catalogoRutaServidor()[0];
                ruta = (string)HttpContext.Current.Session["rutaServidor"] + imagen;

                FileStream fsImagen = new FileStream(ruta, FileMode.Open);

                using (BinaryReader binaryReader = new BinaryReader(fsImagen))
                {
                    bImagen = binaryReader.ReadBytes(Convert.ToInt32(fsImagen.Length));

                }
                objResponse.strImagen = Convert.ToBase64String(bImagen);

            }
            catch (Exception ex)
            {

                alerta = ex.Message;

            }
            objResponse.response = alerta;

            return objResponse;
        }

        public static clsEntResponseImagen obtienePDFExamen(string resNombreArchivo)
        {
            string alerta = string.Empty;
            clsEntResponseImagen objResponse = new clsEntResponseImagen();
            byte[] bImagen;
            try
            {

                if (HttpContext.Current.Session["rutaServidor"] == null)
                    HttpContext.Current.Session["rutaServidor"] = clsDatCatalogos.catalogoRutaServidor()[0].rsDescripcion;

                string rutaImagen = (string)System.Web.HttpContext.Current.Session["rutaServidor"] + resNombreArchivo;
              //  string rutaImagen = clsDatCatalogos.catalogoRutaServidor()[0].rsDescripcion + resNombreArchivo;
 
                FileStream fsImagen = new FileStream(rutaImagen, FileMode.Open);


                using (BinaryReader binaryReader = new BinaryReader(fsImagen))
                {
                    bImagen = binaryReader.ReadBytes(Convert.ToInt32(fsImagen.Length));
                }

                //objResponse.strImagen = Convert.ToBase64String(bImagen);
                objResponse.byteImagen = bImagen;

            }
            catch (Exception ex)
            {

                alerta = ex.Message;

            }
            objResponse.response = alerta;

            return objResponse;

        }
    }
}
