using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;
using System.Collections;
using System.Data;
using proEntidad;
using proDatos;
using System.Net.Mail;

namespace proNegocio
{
    public class clsNegCertificacionPDF
    {

        public enum eOrientacion { HORIZONTAL, VERTICAL };
        protected static iTextSharp.text.Font fuenteTitulos = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.BOLD);
        protected static iTextSharp.text.Font fuenteDatos = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8);
        public static clsEntResponseCertificado generaPDFPersonalizado(List<string> arrImagenes, List<string> arrFirma, string strDatosPersona, string certificadoPDF, int idCertificacionRegistro)
        {
           
                Document docPdf = null;
                docPdf = new Document(PageSize.LETTER);

                docPdf.SetMargins(50, 50, 10, 20);
                docPdf.SetMarginMirroring(false);
                MemoryStream memStream = new MemoryStream();
                clsEntResponseCertificado objResCertificado = new clsEntResponseCertificado();
                objResCertificado.alerta = string.Empty;
               
                try
                {
                docPdf.AddTitle(certificadoPDF);

                Image imageHeader = Image.GetInstance(arrImagenes[0]);
                Image imageBackground = Image.GetInstance(arrImagenes[1]);
                Image imageGrayBackground = Image.GetInstance(arrImagenes[2]);
                Image imageBlackBackground = Image.GetInstance(arrImagenes[3]);
                Image imagenPersona;

                BaseFont soberanaTexto = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("SoberanaTexto-Regular.otf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                BaseFont soberanaTitular = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("SoberanaTitular-Regular.otf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                //BaseFont sTitular = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("SoberanaTitular-Regular.otf"), BaseFont.CP1250, BaseFont.EMBEDDED);

                FontFactory.FontImp.DefaultEmbedding = true;

                FontFactory.Register(HttpContext.Current.Server.MapPath("SoberanaTexto-Regular.otf"), "soberanaTexto");
                FontFactory.Register(HttpContext.Current.Server.MapPath("SoberanaTitular-Regular.otf"), "soberanaTitular");
                //FontFactory.Register(HttpContext.Current.Server.MapPath("SoberanaTitular-Regular.otf"), "sTitular");

                //iTextSharp.text.html.simpleparser.StyleSheet ssPDf = new iTextSharp.text.html.simpleparser.StyleSheet();

                //ssPDf.LoadTagStyle("body", "face", "SoberanaTitular-Regular");
                //ssPDf.LoadTagStyle("body", "encoding", BaseFont.IDENTITY_H);

                soberanaTitular.Subset = false;
                soberanaTexto.Subset = false;

                imageHeader.Alignment = Image.ALIGN_CENTER;
                imageHeader.ScalePercent(42);
                imageBackground.SetAbsolutePosition(125, 315);
                imageBackground.ScalePercent(45f);

                imageGrayBackground.ScalePercent(65);
                imageBlackBackground.ScalePercent(65);

                int primerValidez;
                int renovacionValidez;

                DateTime time = DateTime.Now;
                DateTime tiempo;
                DateTime vigencia;


                string dia = " d ";
                string mes = "MMMM";
                string ano = "yyyy";
                string aux = "";
                string aux1 = "";
                bool espacios = false;
                bool nombreLargo = false;


                    List<string> lstContenido = new List<string>();

                    lstContenido.Add(clsNegCatalogos.consultaContenidoCertificado());
                    lstContenido.Add("EL SERVICIO DE PROTECCIÓN FEDERAL");

                    Font fontGris = new Font(soberanaTitular, 18f, Font.BOLD, new Color(120, 120, 120));
                    Font fontgrisclaro = new Font(soberanaTexto, 15f, Font.BOLD, new Color(150, 150, 150));
                    Font negro = new Font(soberanaTitular, 18f, Font.BOLD, new Color(0, 0, 0));
                    Chunk chunkHeader = new Chunk(imageHeader, 0, -45);
                    Chunk chunkTitulo = new Chunk(lstContenido[1], fontGris);

                    Phrase phraseHeader = new Phrase(chunkHeader);
                    phraseHeader.Add("\n\n\n");
                    phraseHeader.Add("\n");
                    phraseHeader.Add(chunkTitulo);


                    HeaderFooter header = new HeaderFooter(phraseHeader, false);
                    header.Alignment = Element.ALIGN_CENTER;
                    header.Border = Rectangle.NO_BORDER;

                    string strcontenidoPDF = lstContenido[0];

                    PdfWriter.GetInstance(docPdf, memStream);

                    docPdf.Header = header;
                    docPdf.Open();

                    List<string> lisDatosCertificacion = new List<string>();

                    string[] datos = strDatosPersona.Split('|');

                    if (datos[0] != "")
                    {
                        lisDatosCertificacion.Add(datos[2]);    //0  Nombre
                        lisDatosCertificacion.Add(datos[0]);    //1  Ap Paterno
                        lisDatosCertificacion.Add(datos[1]);    //2  Ap Materno
                        lisDatosCertificacion.Add(datos[3]);    //3  CURP
                        lisDatosCertificacion.Add(datos[4]);    //4  CUIP
                        lisDatosCertificacion.Add(datos[5]);    //5  Fecha Realizo Examen
                        lisDatosCertificacion.Add(datos[7]);    //6  Folio 
                        lisDatosCertificacion.Add(datos[8]);    //7  Num Empleado   
                        lisDatosCertificacion.Add(datos[9]);    //8  idCertificacionRegistro 
                        lisDatosCertificacion.Add(datos[10]);   //9  cerNombre
                        lisDatosCertificacion.Add(datos[11]);   //10 cerDescripcion
                        lisDatosCertificacion.Add(datos[12]);   //11 Primer Validez
                        lisDatosCertificacion.Add(datos[13]);   //12 Renovacion Validez

                        // entidades evaluadora y certificadora
                        lisDatosCertificacion.Add(datos[14]);   //13
                        lisDatosCertificacion.Add(datos[15]);   //14

                        // Imagen persona
                        lisDatosCertificacion.Add(datos[6]);    //15
                    }

                    // Fecha que realizo el examen
                    tiempo = DateTime.ParseExact(lisDatosCertificacion[5], "d", null);

                    primerValidez = Convert.ToInt32(lisDatosCertificacion[11]);
                    renovacionValidez = Convert.ToInt32(lisDatosCertificacion[12]);

                    vigencia = DateTime.ParseExact(lisDatosCertificacion[5], "d", null).AddYears(primerValidez);

                    if (System.Web.HttpContext.Current.Session["rutaServidor"] == null)
                        System.Web.HttpContext.Current.Session["rutaServidor"] = clsDatCatalogos.catalogoRutaServidor()[0].rsDescripcion;

                    string rutaImagen = (string)System.Web.HttpContext.Current.Session["rutaServidor"] + lisDatosCertificacion[15];

                    // Imagen Persona
                    imagenPersona = Image.GetInstance(rutaImagen);
                    imagenPersona.SetAbsolutePosition(470, 543);
                    imagenPersona.ScalePercent(40f);
                    imagenPersona.ScaleAbsolute(110, 110);

                    lisDatosCertificacion.Add(arrFirma[0]);     //16
                    lisDatosCertificacion.Add(arrFirma[1]);     //17
                    lisDatosCertificacion.Add(arrFirma[2]);     //18


                    string nombreIntegrante = lisDatosCertificacion[0] + " " + lisDatosCertificacion[1] + " " + lisDatosCertificacion[2];

                    aux = "<!--";
                    aux1 = "-->";

                    if (nombreIntegrante.Length >= 26)
                    {
                        if (nombreIntegrante.Length >= 28)
                        {
                            strcontenidoPDF = strcontenidoPDF.Contains("«nombreIntegrante»") == true ? strcontenidoPDF.Replace("«nombreIntegrante»", lisDatosCertificacion[0] + " " + lisDatosCertificacion[1] + "<br /><br />" + lisDatosCertificacion[2]) : strcontenidoPDF;
                            espacios = true;
                        }
                        else
                        {
                            strcontenidoPDF = strcontenidoPDF.Contains("«nombreIntegrante»") == true ? strcontenidoPDF.Replace("«nombreIntegrante»", lisDatosCertificacion[0] + " " + lisDatosCertificacion[1] + " " + lisDatosCertificacion[2]) : strcontenidoPDF; // llevaba "<br />" al final
                        }
                        strcontenidoPDF = strcontenidoPDF.Contains("«nombreLargo»") == true ? strcontenidoPDF.Replace("«nombreLargo»", "") : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«nombreLargo1»") == true ? strcontenidoPDF.Replace("«nombreLargo1»", "") : strcontenidoPDF;

                        strcontenidoPDF = strcontenidoPDF.Contains("«nombreNormal»") == true ? strcontenidoPDF.Replace("«nombreNormal»", aux) : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«nombreNormal1»") == true ? strcontenidoPDF.Replace("«nombreNormal1»", aux1) : strcontenidoPDF;
                        nombreLargo = true;
                    }
                    else
                    {
                        strcontenidoPDF = strcontenidoPDF.Contains("«nombreIntegrante»") == true ? strcontenidoPDF.Replace("«nombreIntegrante»", nombreIntegrante) : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«nombreLargo»") == true ? strcontenidoPDF.Replace("«nombreLargo»", aux) : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«nombreLargo1»") == true ? strcontenidoPDF.Replace("«nombreLargo1»", aux1) : strcontenidoPDF;

                        strcontenidoPDF = strcontenidoPDF.Contains("«nombreNormal»") == true ? strcontenidoPDF.Replace("«nombreNormal»", "") : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«nombreNormal1»") == true ? strcontenidoPDF.Replace("«nombreNormal1»", "") : strcontenidoPDF;
                    }

                    //strcontenidoPDF = strcontenidoPDF.Contains("«nombreIntegrante»") == true ? strcontenidoPDF.Replace("«nombreIntegrante»", lisDatosCertificacion[0] + " " + lisDatosCertificacion[1] + " " + lisDatosCertificacion[2]) : strcontenidoPDF;
                    strcontenidoPDF = strcontenidoPDF.Contains("«integranteCURP»") == true ? strcontenidoPDF.Replace("«integranteCURP»", lisDatosCertificacion[3]) : strcontenidoPDF;

                    // Si la persona no tiene CUIP

                    if (lisDatosCertificacion[4] != "")
                    {
                        strcontenidoPDF = strcontenidoPDF.Contains("«integranteCUIP»") == true ? strcontenidoPDF.Replace("«integranteCUIP»", lisDatosCertificacion[4]) : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«sinCUIP»") == true ? strcontenidoPDF.Replace("«sinCUIP»", "") : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«sinCUIP1»") == true ? strcontenidoPDF.Replace("«sinCUIP1»", "") : strcontenidoPDF;

                    }
                    else
                    {
                        aux = "<!--";
                        aux1 = "-->";
                        strcontenidoPDF = strcontenidoPDF.Contains("«sinCUIP»") == true ? strcontenidoPDF.Replace("«sinCUIP»", aux) : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«sinCUIP1»") == true ? strcontenidoPDF.Replace("«sinCUIP1»", aux1) : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«integranteCUIP»") == true ? strcontenidoPDF.Replace("«integranteCUIP»", "<br />") : strcontenidoPDF;
                    }


                    strcontenidoPDF = strcontenidoPDF.Contains("«fechaVigencia»") == true ? strcontenidoPDF.Replace("«fechaVigencia»", vigencia.ToString(dia) + "de " + vigencia.ToString(mes) + " de " + vigencia.ToString(ano)) : strcontenidoPDF;
                    strcontenidoPDF = strcontenidoPDF.Contains("«fechaRealizoExamen»") == true ? strcontenidoPDF.Replace("«fechaRealizoExamen»", tiempo.ToString(dia) + "de " + tiempo.ToString(mes) + " de " + tiempo.ToString(ano)) : strcontenidoPDF;

                    if (lisDatosCertificacion[9].Length >= 95)   // Nombre Certificación Largo
                    {
                        if (lisDatosCertificacion[14].Length >= 31)  // entidad certificadora Larga
                        {
                            if (nombreLargo)
                            {
                                if (nombreIntegrante.Length >= 28)
                                {
                                    aux = "<br />";
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", "") : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", "") : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF; // "<br />"
                                    espacios = false;
                                }
                                else //Nombre Largo un renglon
                                {
                                    aux = "<br />";
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", "") : strcontenidoPDF; // "<br />"
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", aux) : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF; // ""
                                    espacios = false;
                                }
                            }
                            else // Nombre Corto
                            {
                                aux = "<br />";
                                strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", "") : strcontenidoPDF;  // aux
                                strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", aux) : strcontenidoPDF;
                                strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF;
                                strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF;  // ""
                                espacios = false;
                            }
                        }
                        else  // entidad certificadora corta
                        {
                            if (nombreLargo)
                            {
                                if (nombreIntegrante.Length >= 28)
                                {
                                    aux = "<br />";
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", aux) : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", "") : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF;  // "<br />"
                                    espacios = false;
                                }
                                else //Nombre Largo un renglon
                                {
                                    aux = "<br />";
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", aux) : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", aux) : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF; // ""
                                    espacios = false;
                                }
                            }
                            else // Nombre Corto
                            {
                                aux = "<br />";
                                strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", aux) : strcontenidoPDF;
                                strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", aux) : strcontenidoPDF;
                                strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF;
                                strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF;  // ""
                                espacios = false;
                            }
                        }
                    }
                    else
                    {
                        if (lisDatosCertificacion[9].Length >= 40)   // Nombre Certificación Medio
                        {
                            if (lisDatosCertificacion[14].Length >= 31)  // entidad certificadora Larga
                            {
                                if (nombreLargo)
                                {
                                    if (nombreIntegrante.Length >= 28)
                                    {
                                        aux = "<br />";
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", aux) : strcontenidoPDF; //""
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF; //""
                                        espacios = true;
                                    }
                                    else  //Nombre Largo un renglon
                                    {
                                        aux = "<br />";
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", aux) : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", aux) : strcontenidoPDF; // ""
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF;
                                        espacios = true;
                                    }
                                }
                                else // Nombre Corto
                                {
                                    aux = "<br />";
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", "") : strcontenidoPDF; //aux
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", aux) : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", aux) : strcontenidoPDF; //""
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF; //""
                                    espacios = true;
                                }
                            }
                            else // entidad certificadora corta
                            {
                                if (nombreLargo)
                                {
                                    if (nombreIntegrante.Length >= 28)
                                    {
                                        aux = "<br />";
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", aux) : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF;
                                        espacios = true;
                                    }
                                    else  //Nombre Largo un renglon
                                    {
                                        aux = "<br />";
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", aux) : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", aux) : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF;
                                        espacios = true;
                                    }
                                }
                                else // Nombre Corto
                                {
                                    aux = "<br />";
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", aux) : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF; //aux
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF; //""
                                    espacios = true;
                                }
                            }

                        }
                        espacios = false;

                        if (nombreLargo)
                        {
                            if (lisDatosCertificacion[14].Length >= 31)  // entidad certificadora Larga
                            {
                                if (nombreIntegrante.Length >= 28)
                                {
                                    if (espacios)
                                    {
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", "") : strcontenidoPDF;
                                        espacios = false;
                                    }
                                    else
                                    {
                                        aux = "<br />";
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", aux) : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF; // aux
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF;
                                        espacios = true;
                                    }
                                }
                                else //Nombre Largo un renglon
                                {
                                    if (espacios)
                                    {
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", "") : strcontenidoPDF;
                                        espacios = false;
                                    }
                                    else
                                    {
                                        aux = "<br />";
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", aux) : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", aux) : strcontenidoPDF; // ""
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF; // aux
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF; // "<br /><br />"
                                        espacios = true;
                                    }
                                }
                            }
                            else  // entidad certificadora corta
                            {
                                if (nombreIntegrante.Length >= 28)
                                {
                                    if (espacios)
                                    {
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", "") : strcontenidoPDF;
                                        espacios = false;
                                    }
                                    else
                                    {
                                        aux = "<br />";
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", aux) : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", aux) : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF;
                                        espacios = true;
                                    }
                                }
                                else //Nombre Largo un renglon
                                {
                                    if (espacios)
                                    {
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", "") : strcontenidoPDF;
                                        espacios = false;
                                    }
                                    else
                                    {
                                        aux = "<br />";
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", aux) : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", aux) : strcontenidoPDF; // ""
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", aux) : strcontenidoPDF;
                                        strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF; // "<br /><br />"
                                        espacios = true;
                                    }
                                }
                            }
                        }
                        else // Nombre Corto
                        {
                            if (lisDatosCertificacion[14].Length >= 31)  // entidad certificadora Larga
                            {
                                if (espacios)
                                {
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", "") : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", "") : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", "") : strcontenidoPDF;
                                    espacios = false;
                                }
                                else
                                {
                                    aux = "<br />";
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", aux) : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", aux) : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF;  // "<br /><br />"
                                    espacios = true;
                                }
                            }
                            else  // entidad certificadora
                            {
                                if (espacios)
                                {
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", "") : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", "") : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", "") : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", "") : strcontenidoPDF;
                                    espacios = false;
                                }
                                else
                                {
                                    aux = "<br />";
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin»") == true ? strcontenidoPDF.Replace("«saltLin»", aux) : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLinAlt»") == true ? strcontenidoPDF.Replace("«saltLinAlt»", aux) : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin1»") == true ? strcontenidoPDF.Replace("«saltLin1»", aux) : strcontenidoPDF;
                                    strcontenidoPDF = strcontenidoPDF.Contains("«saltLin2»") == true ? strcontenidoPDF.Replace("«saltLin2»", aux) : strcontenidoPDF;  // "<br /><br />"
                                    espacios = true;
                                }
                            }

                        }
                    }

                    strcontenidoPDF = strcontenidoPDF.Contains("«nombreCertificacion»") == true ? strcontenidoPDF.Replace("«nombreCertificacion»", lisDatosCertificacion[9]) : strcontenidoPDF;
                    strcontenidoPDF = strcontenidoPDF.Contains("«respondiente»") == true ? strcontenidoPDF.Replace("«respondiente»", lisDatosCertificacion[10]) : strcontenidoPDF;


                    strcontenidoPDF = strcontenidoPDF.Contains("«numEmpleado»") == true ? strcontenidoPDF.Replace("«numEmpleado»", lisDatosCertificacion[7]) : strcontenidoPDF;

                    // filtro para las entidades evaluaduras y certificadoras

                    aux = "";
                    aux1 = "";

                    if (lisDatosCertificacion[13] == lisDatosCertificacion[14])
                    {
                        imageGrayBackground.SetAbsolutePosition(45, 20);
                        imageBlackBackground.SetAbsolutePosition(42, 40);

                        aux = "<!--";
                        aux1 = "-->";

                        strcontenidoPDF = strcontenidoPDF.Contains("«sinEntidadCertEval»") == true ? strcontenidoPDF.Replace("«sinEntidadCertEval»", "") : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«sinEntidadCertEval1»") == true ? strcontenidoPDF.Replace("«sinEntidadCertEval1»", "") : strcontenidoPDF;

                        strcontenidoPDF = strcontenidoPDF.Contains("«entidadCertEvalNombre»") == true ? strcontenidoPDF.Replace("«entidadCertEvalNombre»", lisDatosCertificacion[13]) : strcontenidoPDF;

                        strcontenidoPDF = strcontenidoPDF.Contains("«sinEntidadCert»") == true ? strcontenidoPDF.Replace("«sinEntidadCert»", aux) : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«sinEntidadCert1»") == true ? strcontenidoPDF.Replace("«sinEntidadCert1»", aux1) : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«entidadCertNombre»") == true ? strcontenidoPDF.Replace("«entidadCertNombre»", "") : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«sinEntidadEval»") == true ? strcontenidoPDF.Replace("«sinEntidadEval»", aux) : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«sinEntidadEval1»") == true ? strcontenidoPDF.Replace("«sinEntidadEval1»", aux1) : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«entidadEvalNombre»") == true ? strcontenidoPDF.Replace("«entidadEvalNombre»", "<br />") : strcontenidoPDF;

                    }
                    else
                    {
                        imageGrayBackground.SetAbsolutePosition(0, 20);
                        imageBlackBackground.SetAbsolutePosition(-5, 20);

                        aux = "<!--";
                        aux1 = "-->";

                        strcontenidoPDF = strcontenidoPDF.Contains("«sinEntidadCert»") == true ? strcontenidoPDF.Replace("«sinEntidadCert»", "") : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«sinEntidadCert1»") == true ? strcontenidoPDF.Replace("«sinEntidadCert1»", "") : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«sinEntidadEval»") == true ? strcontenidoPDF.Replace("«sinEntidadEval»", "") : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«sinEntidadEval1»") == true ? strcontenidoPDF.Replace("«sinEntidadEval1»", "") : strcontenidoPDF;

                        strcontenidoPDF = strcontenidoPDF.Contains("«sinEntidadCertEval»") == true ? strcontenidoPDF.Replace("«sinEntidadCertEval»", aux) : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«sinEntidadCertEval1»") == true ? strcontenidoPDF.Replace("«sinEntidadCertEval1»", aux1) : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«entidadCertEvalNombre»") == true ? strcontenidoPDF.Replace("«entidadCertEvalNombre»", "") : strcontenidoPDF;

                        strcontenidoPDF = strcontenidoPDF.Contains("«entidadCertNombre»") == true ? strcontenidoPDF.Replace("«entidadCertNombre»", lisDatosCertificacion[14]) : strcontenidoPDF;
                        strcontenidoPDF = strcontenidoPDF.Contains("«entidadEvalNombre»") == true ? strcontenidoPDF.Replace("«entidadEvalNombre»", lisDatosCertificacion[13]) : strcontenidoPDF;
                    }

                    strcontenidoPDF = strcontenidoPDF.Contains("«fechaHoy»") == true ? strcontenidoPDF.Replace("«fechaHoy»", tiempo.ToString(dia) + "de " + tiempo.ToString(mes) + " de " + tiempo.ToString(ano)) : strcontenidoPDF;
                    strcontenidoPDF = strcontenidoPDF.Contains("«gradoDesc»") == true ? strcontenidoPDF.Replace("«gradoDesc»", lisDatosCertificacion[16]) : strcontenidoPDF;
                    strcontenidoPDF = strcontenidoPDF.Contains("«nombreEmp»") == true ? strcontenidoPDF.Replace("«nombreEmp»", lisDatosCertificacion[17]) : strcontenidoPDF;
                    strcontenidoPDF = strcontenidoPDF.Contains("«cargoDesc»") == true ? strcontenidoPDF.Replace("«cargoDesc»", lisDatosCertificacion[18]) : strcontenidoPDF;
                    strcontenidoPDF = strcontenidoPDF.Contains("«folio»") == true ? strcontenidoPDF.Replace("«folio»", lisDatosCertificacion[6]) : strcontenidoPDF;

                    docPdf.Add(imageBackground);
                    docPdf.Add(imagenPersona);
                    docPdf.Add(imageGrayBackground);
                    docPdf.Add(imageBlackBackground);

                    ArrayList htmlarraylist = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(strcontenidoPDF), null);

                    foreach (IElement elemento in htmlarraylist)
                    {
                        docPdf.Add(elemento);
                    }

                    if (docPdf != null)
                    {
                        docPdf.Close();
                    }

                    clsNegImagen.guardaPDF(memStream, idCertificacionRegistro);
                    objResCertificado.memCertificado = memStream;


                    return objResCertificado;
            

                }
                catch (Exception ex)
                {
                    objResCertificado.alerta = ex.Message;

                    return objResCertificado;
                }
        }


        public static string enviaCorreo(string pdfName, string nameAttach, string receptor, string sender, string subject, string text)
        {

            byte[] attachment = clsNegImagen.obtienePDFExamen(pdfName).byteImagen;

            const string smtpServer = "10.237.48.5";
            SmtpClient smtpMail = new SmtpClient(smtpServer);
            string strError = "";
            MailMessage mmCorreo = new MailMessage(sender, receptor, subject, text);
            MemoryStream s = new MemoryStream(attachment);
            Attachment data = new Attachment(s, nameAttach);

            mmCorreo.IsBodyHtml = false;
            mmCorreo.Attachments.Add(data);
            smtpMail.UseDefaultCredentials = true;
            smtpMail.Host = smtpServer;
            smtpMail.Port = 25;
            try
            {
                smtpMail.Timeout = 50000;
                smtpMail.Send(mmCorreo);
                return strError = "";
            }
            catch (Exception ex)
            {
                //     ViewState["error"] = ex;
                return strError + "Error:  " + ex.Message + " a " + mmCorreo.To;
            }
        }
    }
}
