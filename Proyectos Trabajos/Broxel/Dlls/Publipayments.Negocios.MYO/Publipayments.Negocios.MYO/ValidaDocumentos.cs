using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubliPayments.Entidades.MYO;
using PubliPayments.Utiles;

namespace Publipayments.Negocios.MYO
{
    public class ValidaDocumentos
    {
        public static ResultModel DocumentosValidas(List<DocumentosModel> documentos, string documentosRechazo, string documentosNoObligatorios)
        {
            var result = new ResultModel();
            result.Dictamen = "Documentos OK";
            result.Valido = true;
            var docsRechazo = documentosRechazo.Split('|');
            var docsNoObligatorios = documentosNoObligatorios.Split('|');

            foreach (var doc in documentos)
            {
                if (!docsNoObligatorios.Contains(doc.Nombre))
                {
                    if (doc.Valor == "No especificado")
                    {
                        result.Valido = false;
                        result.Dictamen = "Debe llenar todos los campos obligatorios.";
                    }
                    else
                    {
                        if (doc.Valor == "No")
                        {
                            if (docsRechazo.Contains(doc.Nombre))
                            {
                                result.Dictamen = "Rechazado Completo por Documentacion";
                            }
                            else
                            {
                                result.Dictamen = result.Dictamen == "Documentos OK"
                                    ? "Rechazado por Documentacion"
                                    : result.Dictamen;
                            }
                            result.Valido = false;
                        }
                    }
                }
                else
                {
                    if (doc.Valor == "No")
                    {
                        if (docsRechazo.Contains(doc.Nombre))
                        {
                            result.Dictamen = "Rechazado Completo por Documentacion";
                        }
                        else
                        {
                            result.Dictamen = result.Dictamen == "Documentos OK"
                                ? "Rechazado por Documentacion"
                                : result.Dictamen;
                        }
                        result.Valido = false;
                    }
                }
            }


            return result;
        }

        public static bool GuardaDocumentos(int idOrden, List<DocumentosModel> documentos)
        {
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ValidaDocumentos", "GuardaDocumentos - guardando documentos - Orden: " + idOrden);
                foreach (var documento in documentos)
                {
                    EntRespuestasMYO.InsertarRespuesta(idOrden, documento.Nombre + "Result", documento.Valor);
                    if (documento.Comentario != "")
                    {
                        EntRespuestasMYO.InsertarRespuesta(idOrden, documento.Nombre + "Comentario", documento.Comentario);
                    }
                    if (documento.Tipo == "2")
                    {
                        EntRespuestasMYO.InsertarRespuesta(idOrden, documento.Nombre + "Url", documento.Url);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ValidaDocumentos", "GuardaDocumentos - error al guardar documentos - Orden: " + idOrden +" _Error:"+ex.Message);
                return false;
            }

            return true;
        }

        public static bool BorrarDocumentos(int idOrden, List<DocumentosModel> documentos)
        {
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ValidaDocumentos", "BorrarDocumentos - borrando documentos - Orden: " + idOrden);
                var datosOrden = EntOrdenesMyo.ObtenerInfoOrdenMyo(idOrden);
                foreach (var documento in documentos)
                {
                    if (documento.Valor == "No")
                    {
                        EntRespuestasMYO.BorrarRespuestasOrden(idOrden, documento.Nombre);
                        if (documento.Tipo == "1")
                        {
                            var entLoan = new EntLoan();
                            entLoan.ActualizarDatosRefImg(documento.Comentario, Convert.ToInt32(documento.IdDocumento),
                                datosOrden.Etiqueta.ToUpper() == "ACREDITADO"
                                    ? "IMGACR"
                                    : datosOrden.Etiqueta.ToUpper() == "INVERSIONISTA_1" ? "IMGII" : "IMGIB", -1);
                        }
                    }
                }
                CorreosMyo.DocumentosIncorrectos(documentos, idOrden);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ValidaDocumentos", "BorrarDocumentos - error al borrar documentos - Orden: " + idOrden + " _Error:" + ex.Message);
                return false;
            }

            return true;
        }
    }
}
