using System;using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SICOGUA.Entidades;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;
using System.Collections;
using SICOGUA.Seguridad;
using System.Data;
using SICOGUA.Datos;

namespace SICOGUA.Negocio
{
    public class clsNegOficioAsignacion
    {
        public enum eOrientacion { HORIZONTAL, VERTICAL };
        protected static iTextSharp.text.Font fuenteTitulos = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.BOLD);
        protected static iTextSharp.text.Font fuenteDatos = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8);
        public static MemoryStream generaPDFPersonalizado(List<clsEntAsignacionMasiva> lisIntegrantes, clsEntFirmanteOficioAsignacion empleadoFirmante, clsEntSesion objSesion, List<string> arrImagenes)
        {
            Document docPdf = null;
            docPdf = new Document(PageSize.LETTER);

            docPdf.SetMargins(50, 50, 10, 20);
            docPdf.SetMarginMirroring(false);
            MemoryStream memStream = new MemoryStream();

            Image imageHeader = Image.GetInstance(arrImagenes[0]);
            Image imageBackground = Image.GetInstance(arrImagenes[1]);

            imageHeader.Alignment = Image.ALIGN_CENTER;
            imageHeader.ScalePercent(42);
            imageBackground.SetAbsolutePosition(75, 100);
            imageBackground.ScalePercent(55f);

            DateTime time = DateTime.Now;

            string dia = " d ";
            string mes = "MMMM";
            string ano = "yyyy";
            string aux = "";

            
            try
            {

                List<string> lstContenido = new List<string>();

                lstContenido = consultaContenidoOficio(objSesion);

                Font fontgrisclaro = new Font(Font.TIMES_ROMAN, 11f, Font.ITALIC, new Color(150, 150, 150));
                Chunk chunkHeader = new Chunk(imageHeader, 0, -45);
                Chunk chunkFraseAño = new Chunk(lstContenido[1], fontgrisclaro);

                Phrase phraseHeader = new Phrase(chunkHeader);
                phraseHeader.Add("\n\n\n");
                phraseHeader.Add(chunkFraseAño);
                //phrase1.Add(chunk);
                HeaderFooter header = new HeaderFooter(phraseHeader, false);
                header.Alignment = Element.ALIGN_CENTER;
                header.Border = Rectangle.NO_BORDER;



                string strcontenidoOficio = lstContenido[0];



                String piePagina = lstContenido[2];
                piePagina = piePagina.Replace("\\n", "\n");

                Phrase phraseFinal = new Phrase(piePagina, new Font(Font.TIMES_ROMAN, 8f));

                
                HeaderFooter pie = new HeaderFooter(phraseFinal, false)
                {
                    Alignment = Element.ALIGN_CENTER,
                    BorderWidth = 0
                };



                PdfWriter.GetInstance(docPdf, memStream);
                
                if (lisIntegrantes.Count > 0)
                {
                    docPdf.Header = header;
                    docPdf.Footer = pie;
                    docPdf.Open();


                    List<string> lisDatosOficio = consultaDatosOficioAsignacion(lisIntegrantes[0].idInstalacion, lisIntegrantes[0].idServicio, objSesion);

                    Int16 idDia = 0;
                    switch (lisIntegrantes[0].fechaIngreso.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            idDia = 1;
                            break;

                        case DayOfWeek.Tuesday:
                            idDia = 2;
                            break;

                        case DayOfWeek.Wednesday:
                            idDia = 3;
                            break;

                        case DayOfWeek.Thursday:
                            idDia = 4;
                            break;

                        case DayOfWeek.Friday:
                            idDia = 5;
                            break;

                        case DayOfWeek.Saturday:
                            idDia = 6;
                            break;

                        case DayOfWeek.Sunday:
                            idDia = 7;
                            break;

                    }


                    strcontenidoOficio = strcontenidoOficio.Contains("«DirAdjunta»") == true ? strcontenidoOficio.Replace("«DirAdjunta»", lisDatosOficio[0]) : strcontenidoOficio;
                    strcontenidoOficio = strcontenidoOficio.Contains("«siglasDireccion»") == true ? strcontenidoOficio.Replace("«siglasDireccion»", lisDatosOficio[1]) : strcontenidoOficio;
                    strcontenidoOficio = strcontenidoOficio.Contains("«anio»") == true ? strcontenidoOficio.Replace("«anio»",time.ToString(ano)) : strcontenidoOficio;
                    strcontenidoOficio = strcontenidoOficio.Contains("«folio»") == true ? strcontenidoOficio.Replace("«folio»", "          ") : strcontenidoOficio;


                    strcontenidoOficio = strcontenidoOficio.Contains("«fechaHoy»") == true ? strcontenidoOficio.Replace("«fechaHoy»", time.ToString(dia) + "de " + time.ToString(mes) + " de " + time.ToString(ano)) : strcontenidoOficio;
                    strcontenidoOficio = strcontenidoOficio.Contains("«NombreServicio»") == true ? strcontenidoOficio.Replace("«NombreServicio»", lisDatosOficio[2]) : strcontenidoOficio;
                    strcontenidoOficio = strcontenidoOficio.Contains("«NombreInstalacion»") == true ? strcontenidoOficio.Replace("«NombreInstalacion»", lisDatosOficio[3]) : strcontenidoOficio;
                    strcontenidoOficio = strcontenidoOficio.Contains("«UbicacionServicio»") == true ? strcontenidoOficio.Replace("«UbicacionServicio»", lisDatosOficio[4]) : strcontenidoOficio;
                    
                    strcontenidoOficio = strcontenidoOficio.Contains("«DirGeneral»") == true ? strcontenidoOficio.Replace("«DirGeneral»", lisDatosOficio[5]) : strcontenidoOficio;
                    


                    strcontenidoOficio = strcontenidoOficio.Contains("«diaINICIO»") == true ? strcontenidoOficio.Replace("«diaINICIO»", lisIntegrantes[0].fechaIngreso.ToString(dia)) : strcontenidoOficio;
                    strcontenidoOficio = strcontenidoOficio.Contains("«mesINICIO»") == true ? strcontenidoOficio.Replace("«mesINICIO»", lisIntegrantes[0].fechaIngreso.ToString(mes)) : strcontenidoOficio;
                    strcontenidoOficio = strcontenidoOficio.Contains("«anoINICIO»") == true ? strcontenidoOficio.Replace("«anoINICIO»", lisIntegrantes[0].fechaIngreso.ToString(ano)) : strcontenidoOficio;

                    if (lisIntegrantes[0].idHorario <= 0)
                    {
                        strcontenidoOficio = strcontenidoOficio.Contains("«horaINICIO»") == true ? strcontenidoOficio.Replace("«horaINICIO»", "_____") : strcontenidoOficio;
                    }
                    else 
                    {
                        strcontenidoOficio = strcontenidoOficio.Contains("«horaINICIO»") == true ? strcontenidoOficio.Replace("«horaINICIO»", consultaHorarioDiaREA(idDia, lisIntegrantes[0].idHorario, objSesion)) : strcontenidoOficio;
                    }

                    strcontenidoOficio = strcontenidoOficio.Contains("«cargoFirmante»") == true ? strcontenidoOficio.Replace("«cargoFirmante»", empleadoFirmante.puestoDescripcion) : strcontenidoOficio;
                    strcontenidoOficio = strcontenidoOficio.Contains("«jerarquiaFirmante»") == true ? strcontenidoOficio.Replace("«jerarquiaFirmante»", empleadoFirmante.jerDescripcion) : strcontenidoOficio;
                    strcontenidoOficio = strcontenidoOficio.Contains("«nombreFirmante»") == true ? strcontenidoOficio.Replace("«nombreFirmante»", empleadoFirmante.empNombreCompleto) : strcontenidoOficio;
                    strcontenidoOficio = strcontenidoOficio.Contains("«citaAusencia»") == true ? strcontenidoOficio.Replace("«citaAusencia»", empleadoFirmante.citaAusencia) : strcontenidoOficio;

                    List<string> titularesEstacion = new List<string>();
                    titularesEstacion = consultaTitularEstacion(lisIntegrantes[0].idServicio, lisIntegrantes[0].idInstalacion, objSesion);


                    strcontenidoOficio = strcontenidoOficio.Contains("«nombreTitularEstacion»") == true ? strcontenidoOficio.Replace("«nombreTitularEstacion»", titularesEstacion[0]) : strcontenidoOficio;

                        aux = "";
                        if (strcontenidoOficio.Contains("«otrosTitularesEstacion»") == true)
                        {
                            for (int i=1; i < titularesEstacion.Count;i++)
                            {
                                aux += "<tr><td colspan=\"10\"><p style='line-height:40%;font-family:\"Arial\",\"sans-serif\";font-size:6pt;mso-fareast-language:ES-TRAD;margin-top:.05pt;margin-bottom:.05pt;text-align:left;'><b>Titular de la Estación" + titularesEstacion[i] + ".</b> Para su conocimiento. Presente.</p></td><td colspan=\"2\"><p style='line-height:40%;font-family:\"Arial\",\"sans-serif\";font-size:6pt;mso-fareast-language:ES-TRAD;margin-top:.05pt;margin-bottom:.05pt;text-align:right;'></p></td></tr>";
                                
                            }
                            strcontenidoOficio=strcontenidoOficio.Replace("«otrosTitularesEstacion»", aux);
                        }
                        
                    
                    List<string> jefesServicioDestino = new List<string>();
                    jefesServicioDestino = consultaJefeServicio(lisIntegrantes[0].idServicio, lisIntegrantes[0].idInstalacion, objSesion);

                    strcontenidoOficio = strcontenidoOficio.Contains("«nombreJefeServicioDestino»") == true ? strcontenidoOficio.Replace("«nombreJefeServicioDestino»",  jefesServicioDestino[0]) : strcontenidoOficio;

                    aux = "";
                        if (strcontenidoOficio.Contains("«otrosJefesServicioDestino»") == true)
                        {

                            for (int i = 1; i < jefesServicioDestino.Count; i++)
                            {
                                aux += "<tr><td colspan=\"10\"><p style='line-height:40%;font-family:\"Arial\",\"sans-serif\";font-size:6pt;mso-fareast-language:ES-TRAD;margin-top:.05pt;margin-bottom:.05pt;text-align:left;'><b>Jefe del Servicio de Destino" + jefesServicioDestino[i] + ".</b> Para su conocimiento. Presente.</p></td><td colspan=\"2\"><p style='line-height:40%;font-family:\"Arial\",\"sans-serif\";font-size:6pt;mso-fareast-language:ES-TRAD;margin-top:.05pt;margin-bottom:.05pt;text-align:right;'></p></td></tr>";
                            }
                            strcontenidoOficio=strcontenidoOficio.Replace("«otrosJefesServicioDestino»", aux);
                        }
                   

                    string strcontenidoAux;

                    List<clsEntEmpleado> lisDatosIntegrante = clsNegOficioAsignacion.consultaDatosIntegranteOficio(lisIntegrantes, objSesion);

                    foreach (clsEntEmpleado integrante in lisDatosIntegrante)
                    {
                        List<string> jefesServicioOrigen = new List<string>();
                        jefesServicioOrigen = consultaJefeServicio(integrante.EmpleadoAsignacion2.Servicio.idServicio, integrante.EmpleadoAsignacion2.Instalacion.IdInstalacion, objSesion);


                        strcontenidoAux = strcontenidoOficio;
                        strcontenidoAux = strcontenidoAux.Contains("«jerarquiaIntegrante»") == true ? strcontenidoAux.Replace("«jerarquiaIntegrante»", integrante.jerDescripcion) : strcontenidoAux;
                        strcontenidoAux = strcontenidoAux.Contains("«nombreIntegrante»") == true ? strcontenidoAux.Replace("«nombreIntegrante»", integrante.EmpPaterno + " " + integrante.EmpMaterno + " " + integrante.EmpNombre) : strcontenidoAux;
                        strcontenidoAux = strcontenidoAux.Contains("«numeroEmpIntegrante»") == true ? strcontenidoAux.Replace("«numeroEmpIntegrante»", integrante.EmpNumero == 0 ? "" : integrante.EmpNumero.ToString()) : strcontenidoAux;

                        strcontenidoAux = strcontenidoAux.Contains("«nombreJefeServicioOrigen»") == true ? strcontenidoAux.Replace("«nombreJefeServicioOrigen»", jefesServicioOrigen[0]) : strcontenidoAux;

                        aux = "";
                            if (strcontenidoAux.Contains("«otrosJefesServicioOrigen»") == true)
                            {

                                for (int i = 1; i < jefesServicioOrigen.Count; i++)
                                {
                                    aux += "<tr><td colspan=\"10\"><p style='line-height:40%;font-family:\"Arial\",\"sans-serif\";font-size:6pt;mso-fareast-language:ES-TRAD;margin-top:.05pt;margin-bottom:.05pt;text-align:left;'><b>Jefe del Servicio de Origen al que pertenece" + jefesServicioOrigen[i] + ".</b> Para su conocimiento. Presente.</p></td><td colspan=\"2\"><p style='line-height:40%;font-family:\"Arial\",\"sans-serif\";font-size:6pt;mso-fareast-language:ES-TRAD;margin-top:.05pt;margin-bottom:.05pt;text-align:right;'></p></td></tr>";
                                }
                                strcontenidoAux = strcontenidoAux.Replace("«otrosJefesServicioOrigen»", aux);
                            }
                           



                        docPdf.Add(imageBackground);

                        ArrayList htmlarraylist = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(strcontenidoAux), null);

                        foreach (IElement elemento in htmlarraylist)
                        {
                            docPdf.Add(elemento);
                        }


                        docPdf.NewPage();
                      
                    }

                    if (docPdf != null) docPdf.Close();

                }
                return memStream;
            }

            catch (Exception ex)
            {
                return null;
            }



        }
        /*
        public static List<string> consultaDatosIntegranteOficio(Guid idEmpleado, clsEntSesion objSesion)
        {
            return clsDatOficioAsignacion.consultaIntegranteOficioAsignacion(idEmpleado, objSesion);

        }
         * */
        public static List<clsEntEmpleado> consultaDatosIntegranteOficio(List<clsEntAsignacionMasiva> lisIntegrantes, clsEntSesion objSesion)
        {
            return clsDatOficioAsignacion.consultaIntegranteOficioAsignacion(lisIntegrantes, objSesion);
        }


        public static List<string> consultaDatosOficioAsignacion(int idInstalacion, int idServicio, clsEntSesion objSesion)
        {
            return clsDatOficioAsignacion.consultaDatosOficioAsignacion(idInstalacion, idServicio, objSesion);
        }


        public static bool insertarOficioAsignacion(clsEntOficioAsignacion objOficio, clsEntSesion objSesion)
        {
            if (clsDatOficioAsignacion.insertarOficioAsignacion(objOficio, objSesion))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public static clsEntOficioAsignacion obtieneOficioAsignacion(Guid idEmpleado, Int16 idEmpleadoAsignacion, clsEntSesion objSesion)
        {
            DataTable dtOficioAsignacion = clsDatOficioAsignacion.obtieneOficioAsignacion(idEmpleado, idEmpleadoAsignacion, objSesion);

            if (dtOficioAsignacion.Rows.Count == 0)
            {
                return new clsEntOficioAsignacion();
            }

            DataRow drOficioAsignacion = dtOficioAsignacion.Rows[0];

            return new clsEntOficioAsignacion
            {
                idEmpleado = idEmpleado,
                idEmpleadoAsignacion = idEmpleadoAsignacion,
                oficioAsignacion = (Byte[])drOficioAsignacion["eoImagen"],
                fechaCarga = (DateTime)drOficioAsignacion["eoFecha"]
            };
        }





        public static string consultaHorarioDiaREA(Int16 idDia, int idHorario, clsEntSesion objSesion)
        {
            return clsDatOficioAsignacion.consultaHorarioDiaREA(idDia, idHorario, objSesion);
        }

        public static List<string> consultaContenidoOficio( clsEntSesion objSesion)
        {
            return clsDatOficioAsignacion.consultaContenidoOficio( objSesion);

        }
        /*
        public static string consultaContenidoOficio(int intTipo ,clsEntSesion objSesion)
        {
            return clsDatOficioAsignacion.consultaContenidoOficio(intTipo,objSesion);

        }*/

        public static List<string> consultaTitularEstacion(int idServicio, int idInstalacion, clsEntSesion objSesion)
        {
            return clsDatOficioAsignacion.consultaTitularEstacion(idServicio, idInstalacion, objSesion);
        }

        public static List<string> consultaJefeServicio(int idServicio, int idInstalacion, clsEntSesion objSesion)
        {
            return clsDatOficioAsignacion.consultaJefeServicio(idServicio, idInstalacion, objSesion);
        }

        public static Boolean verificaZona(int idServicio, int idInstalacion, clsEntSesion objSesion)
        {
            return clsDatOficioAsignacion.verificaZona(idServicio, idInstalacion, objSesion);
        }

        public static string LowerCaseWords(string value)
        {
            char[] array = value.ToCharArray();

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] != ' ')
                {
                    if (char.IsUpper(array[i]))
                    {
                        array[i] = char.ToLower(array[i]);
                    }
                }
            }
            return new string(array);
        }



    }
}
