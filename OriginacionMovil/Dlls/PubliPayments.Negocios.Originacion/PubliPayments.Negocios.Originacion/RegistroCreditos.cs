using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PubliPayments.Entidades;
using PubliPayments.Entidades.Originacion;
using PubliPayments.Negocios.Originacion.RegistraSolicitud2;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios.Originacion
{
    public static class RegistroCreditos 
    {
        public static Dictionary<string, string> RegistraCredito(Dictionary<String, String> respuestas)
        {
            var result = new Dictionary<string, string>();

            var datos = EntDatosProspecto.obtenerDatosRegistro(respuestas["Nss"], respuestas["Plazo"], respuestas["CentObre"],1);

            var row = datos.Tables[0].Rows[0];

            var Nss =  respuestas["Nss"].ToString();
            var AssignedTo = respuestas["AssignedTo"].ToString();
            var wsNombreTitular = row["wsNombreTitular"].ToString();
            var plazo = row["plazo"].ToString();
            var contarias = row["contarias"].ToString();
            var monto_credito = row["monto_credito"].ToString();
            var pago_mensual = row["pago_mensual"].ToString();
            var mano_obra = row["mano_obra"].ToString();
            var PensionAlimenticia = "0";
            var wsGastosApertura = row["wsGastosApertura"].ToString();
            var wsNumRegistroPatronal = row["wsNumRegistroPatronal"].ToString();
            var wsPuntMin = row["wsPuntMin"].ToString();
            var wsPuntTotal = row["wsPuntTotal"].ToString();
            var wsCurp = row["wsCurp"].ToString();
            var wsRFC = row["wsRFC"].ToString();
            var WSNombreEmpresa = row["WSNombreEmpresa"].ToString();
            var ListaDocumentos = respuestas["ListaDocumentos"].ToString();
            var NumIdentificacion = (respuestas.ContainsKey("NumIdentificacionIFE") ? respuestas["NumIdentificacionIFE"].ToString() : respuestas["NumIdentificacionPas"].ToString());
            var FechaValidezIdentificacion = respuestas["FechaValidezIdentificacion"].ToString();
            var CentObre = respuestas["CentObre"].ToString();
            var descripcionObrera = row["descripcionObrera"].ToString();
            var LadaEmpresa = respuestas["LadaEmpresa"].ToString();
            var TelEmpresa = respuestas["TelEmpresa"].ToString();
            var ExtEmpresa = respuestas["ExtEmpresa"].ToString();
            var Calle = respuestas["Calle"].ToString();
            var NumeroInt = respuestas["NumeroInt"].ToString();
            var NumeroExt = respuestas["NumeroExt"].ToString();
            var EstadoCESI = respuestas["EstadoCESI"].ToString();
            var Estado = respuestas["Estado"].ToString();
            var DelegacionId = respuestas["DelegacionId"].ToString();
            var Delegacion = respuestas["Delegacion"].ToString();
            var Colonia = respuestas["Colonia"].ToString();
            var Cp = (respuestas["Cp"].Length >= 5 ? respuestas["Cp"].ToString() : respuestas["Cp"].PadLeft(5, '0').ToString());
            var GeneroCB = (respuestas["GeneroCB"] == "Masculino" ? "1" : "2");
            var EdoCivil = respuestas["EdoCivil"].ToString();
            var RegimenCony = (respuestas.ContainsKey("RegimenCony") ? respuestas["RegimenCony"].ToString() : "0");
            var Lada = respuestas["Lada"].ToString();
            var Telefono2Cte = respuestas["Telefono2Cte"].ToString();
            var LadaCelular = respuestas["LadaCelular"].ToString();
            var Telefono1Cte = respuestas["Telefono1Cte"].ToString();
            var CorreoElectronico = (respuestas.ContainsKey("CorreoElectronico") ? respuestas["CorreoElectronico"].ToString() : "");
            var ApPaternoRef1 = respuestas["ApPaternoRef1"].ToString();
            var ApMaternoRef1 = (respuestas.ContainsKey("ApMaternoRef1") ? respuestas["ApMaternoRef1"].ToString() : "");
            var NombreRef1 = respuestas["NombreRef1"].ToString();
            var TipoTelR1 = respuestas["TipoTelR1"].ToString();
            var LadaR1 = respuestas["LadaR1"].ToString();
            var Telefono1Ref1 = respuestas["Telefono1Ref1"].ToString();
            var ApPaternoRef2 = respuestas["ApPaternoRef2"].ToString();
            var ApMaternoRef2 = (respuestas.ContainsKey("ApMaternoRef2") ? respuestas["ApMaternoRef2"].ToString() : "");
            var NombreRef2 = respuestas["NombreRef2"].ToString();
            var TipoTelR2 = respuestas["TipoTelR2"].ToString();
            var LadaR2 = respuestas["LadaR2"].ToString();
            var Telefono1Ref2 = respuestas["Telefono1Ref2"].ToString();
            var ApPaternoBen = respuestas["ApPaternoBen"].ToString();
            var ApMaternoBen = respuestas["ApMaternoBen"].ToString();
            var NombreBeneficiario = respuestas["NombreBeneficiario"].ToString();
            var numcue = respuestas["numcue"].ToString();
            
            try
            {
                var logVariablesObject = new VariablesLog
                {
                    Nss =  respuestas["Nss"].ToString(),
                    AssignedTo = respuestas["AssignedTo"].ToString(),
                    wsNombreTitular = row["wsNombreTitular"].ToString(),
                    plazo = row["plazo"].ToString(),
                    contarias = row["contarias"].ToString(),
                    monto_credito = row["monto_credito"].ToString(),
                    pago_mensual = row["pago_mensual"].ToString(),
                    mano_obra = row["mano_obra"].ToString(),
                    PensionAlimenticia = "0",
                    wsGastosApertura = row["wsGastosApertura"].ToString(),
                    wsNumRegistroPatronal = row["wsNumRegistroPatronal"].ToString(),
                    wsPuntMin = row["wsPuntMin"].ToString(),
                    wsPuntTotal = row["wsPuntTotal"].ToString(),
                    wsCurp = row["wsCurp"].ToString(),
                    wsRFC = row["wsRFC"].ToString(),
                    WSNombreEmpresa = row["WSNombreEmpresa"].ToString(),
                    ListaDocumentos = respuestas["ListaDocumentos"].ToString(),
                    NumIdentificacion = (respuestas["NumIdentificacionIFE"] != "" ? respuestas["NumIdentificacionIFE"].ToString() : respuestas["NumIdentificacionPas"].ToString()),
                    FechaValidezIdentificacion = respuestas["FechaValidezIdentificacion"].ToString(),
                    CentObre = respuestas["CentObre"].ToString(),
                    descripcionObrera = row["descripcionObrera"].ToString(),
                    LadaEmpresa = respuestas["LadaEmpresa"].ToString(),
                    TelEmpresa = respuestas["TelEmpresa"].ToString(),
                    ExtEmpresa = respuestas["ExtEmpresa"].ToString(),
                    Calle = respuestas["Calle"].ToString(),
                    NumeroInt = respuestas["NumeroInt"].ToString(),
                    NumeroExt = respuestas["NumeroExt"].ToString(),
                    EstadoCESI = respuestas["EstadoCESI"].ToString(),
                    Estado = respuestas["Estado"].ToString(),
                    DelegacionId = respuestas["DelegacionId"].ToString(),
                    Delegacion = respuestas["Delegacion"].ToString(),
                    Colonia = respuestas["Colonia"].ToString(),
                    Cp = (respuestas["Cp"].Length >= 5 ? respuestas["Cp"].ToString() : respuestas["Cp"].PadLeft(5, '0').ToString()),
                    GeneroCB = (respuestas["GeneroCB"] == "Masculino" ? "1" : "2"),
                    EdoCivil = respuestas["EdoCivil"].ToString(),
                    RegimenCony = (respuestas["RegimenCony"]!="" ? respuestas["RegimenCony"].ToString() : "0"),
                    Lada = respuestas["Lada"].ToString(),
                    Telefono2Cte = respuestas["Telefono2Cte"].ToString(),
                    LadaCelular = respuestas["LadaCelular"].ToString(),
                    Telefono1Cte = respuestas["Telefono1Cte"].ToString(),
                    CorreoElectronico = (respuestas.ContainsKey("CorreoElectronico") ? respuestas["CorreoElectronico"].ToString() : ""),
                    ApPaternoRef1 = respuestas["ApPaternoRef1"].ToString(),
                    ApMaternoRef1 = (respuestas.ContainsKey("ApMaternoRef1") ? respuestas["ApMaternoRef1"].ToString() : ""),
                    NombreRef1 = respuestas["NombreRef1"].ToString(),
                    TipoTelR1 = respuestas["TipoTelR1"].ToString(),
                    LadaR1 = respuestas["LadaR1"].ToString(),
                    Telefono1Ref1 = respuestas["Telefono1Ref1"].ToString(),
                    ApPaternoRef2 = respuestas["ApPaternoRef2"].ToString(),
                    ApMaternoRef2 = (respuestas.ContainsKey("ApMaternoRef2") ? respuestas["ApMaternoRef2"].ToString() : ""),
                    NombreRef2 = respuestas["NombreRef2"].ToString(),
                    TipoTelR2 = respuestas["TipoTelR2"].ToString(),
                    LadaR2 = respuestas["LadaR2"].ToString(),
                    Telefono1Ref2 = respuestas["Telefono1Ref2"].ToString(),
                    ApPaternoBen = respuestas["ApPaternoBen"].ToString(),
                    ApMaternoBen = respuestas["ApMaternoBen"].ToString(),
                    NombreBeneficiario = respuestas["NombreBeneficiario"].ToString(),
                    numcue = respuestas["numcue"].ToString()
                };

                //var logVariables = respuestas["Nss"]+", "+
                //        respuestas["AssignedTo"]+", "+
                //        row["wsNombreTitular"].ToString()+", "+
                //        row["plazo"] + ", " +
                //        row["contarias"].ToString()+", "+
                //        row["monto_credito"].ToString()+", "+
                //        row["pago_mensual"].ToString()+", "+
                //        row["mano_obra"].ToString()+", "+ "0"+", "+
                //        row["wsGastosApertura"].ToString()+", "+
                //        row["wsNumRegistroPatronal"].ToString()+", "+
                //        row["wsPuntMin"].ToString()+", "+
                //        row["wsPuntTotal"].ToString()+", "+
                //        row["wsCurp"].ToString()+", "+
                //        row["wsRFC"].ToString()+", "+
                //        row["WSNombreEmpresa"].ToString()+", "+
                //        respuestas["ListaDocumentos"]+", "+
                //        (respuestas.ContainsKey("NumIdentificacionIFE") ? respuestas["NumIdentificacionIFE"] : respuestas["NumIdentificacionPas"])+", "+
                //        respuestas["FechaValidezIdentificacion"]+", "+
                //        respuestas["CentObre"]+", "+
                //        row["descripcionObrera"].ToString() + ", " + 
                //        respuestas["LadaEmpresa"]+
                //    respuestas["TelEmpresa"]+
                //    respuestas["ExtEmpresa"]+", " +
                //        respuestas["Calle"]+", "+
                //        respuestas["NumeroInt"]+", "+
                //        respuestas["NumeroExt"]+", "+ 
                //        respuestas["EstadoCESI"]+", "+
                //        respuestas["Estado"]+", "+ 
                //        respuestas["DelegacionId"]+", "+
                //        respuestas["Delegacion"]+", "+
                //        respuestas["Colonia"]+", "+
                //        (respuestas["Cp"].Length >= 5 ? respuestas["Cp"] : respuestas["Cp"].PadLeft(5, '0')) + ", " +
                //        (respuestas["GeneroCB"] == "Masculino" ? "1" : "2")+", "+
                //        respuestas["EdoCivil"]+", "+
                //        (respuestas.ContainsKey("RegimenCony") ? respuestas["RegimenCony"] : "0")+", "+
                //        respuestas["Lada"]+", "+
                //        respuestas["Telefono2Cte"]+", "+ 
                //        respuestas["LadaCelular"]+", "+
                //        respuestas["Telefono1Cte"]+", "+
                //        (respuestas.ContainsKey("CorreoElectronico") ? respuestas["CorreoElectronico"] : "") + ", " +
                //        respuestas["ApPaternoRef1"]+", "+
                //        (respuestas.ContainsKey("ApMaternoRef1") ? respuestas["ApMaternoRef1"] : "")+", "+
                //        respuestas["NombreRef1"]+", "+
                //        respuestas["TipoTelR1"]+", "+
                //        respuestas["LadaR1"]+", "+
                //        respuestas["Telefono1Ref1"]+", "+
                //        respuestas["ApPaternoRef2"]+", "+
                //        (respuestas.ContainsKey("ApMaternoRef2") ? respuestas["ApMaternoRef2"] : "")+", "+
                //        respuestas["NombreRef2"]+", "+
                //        respuestas["TipoTelR2"]+", "+
                //        respuestas["LadaR2"]+", "+
                //        respuestas["Telefono1Ref2"]+", "+
                //        respuestas["ApPaternoBen"] + ", " +
                //        respuestas["ApMaternoBen"] + ", " +
                //        respuestas["NombreBeneficiario"] + ", " +
                //        respuestas["numcue"];

                var LogRequest = SerializeXML.SerializeObject(logVariablesObject);

                Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "TraceServiciosYY", "RequestRegistroCredito - " + LogRequest);

                if (ConfigurationManager.AppSettings["Produccion"] == "true")
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    var cliente = new WSRegistraSolicitudCreditoMejoravitAppSoapClient();
                    var resp = cliente.RegistraSolicitud(
                    respuestas["Nss"],
                    respuestas["AssignedTo"],
                    row["wsNombreTitular"].ToString(),
                    row["plazo"].ToString(),
                    row["contarias"].ToString(),
                    row["monto_credito"].ToString(),
                    row["pago_mensual"].ToString(),
                    row["mano_obra"].ToString(),
                    "0",
                    row["wsGastosApertura"].ToString(),
                    row["wsNumRegistroPatronal"].ToString(),
                    row["wsPuntMin"].ToString(),
                    row["wsPuntTotal"].ToString(),
                    row["wsCurp"].ToString(),
                    row["wsRFC"].ToString(),
                    row["WSNombreEmpresa"].ToString(),
                    respuestas["ListaDocumentos"],
                    (respuestas["NumIdentificacionIFE"] != "" ? respuestas["NumIdentificacionIFE"] : respuestas["NumIdentificacionPas"]),
                    respuestas["FechaValidezIdentificacion"],
                    respuestas["CentObre"],
                    row["descripcionObrera"].ToString(),
                    respuestas["LadaEmpresa"],
                    respuestas["TelEmpresa"],
                    respuestas["ExtEmpresa"],
                    respuestas["Calle"],
                    respuestas["NumeroInt"],
                    respuestas["NumeroExt"],
                    respuestas["EstadoCESI"],
                    respuestas["Estado"],
                    respuestas["DelegacionId"],
                    respuestas["Delegacion"],
                    respuestas["Colonia"],
                    (respuestas["Cp"].Length >= 5 ? respuestas["Cp"] : respuestas["Cp"].PadLeft(5, '0')), 
                    (respuestas["GeneroCB"] == "Masculino" ? "1" : "2"),
                    respuestas["EdoCivil"],
                    (respuestas["RegimenCony"] != "" ? respuestas["RegimenCony"] : "0"),
                    respuestas["Lada"],
                    respuestas["Telefono2Cte"],
                    respuestas["LadaCelular"],
                    respuestas["Telefono1Cte"],
                    (respuestas.ContainsKey("CorreoElectronico") ? respuestas["CorreoElectronico"] : ""),
                    respuestas["ApPaternoRef1"],
                    (respuestas.ContainsKey("ApMaternoRef1") ? respuestas["ApMaternoRef1"] : ""),
                    respuestas["NombreRef1"],
                    respuestas["TipoTelR1"],
                    respuestas["LadaR1"],
                    respuestas["Telefono1Ref1"],
                    respuestas["ApPaternoRef2"],
                    (respuestas.ContainsKey("ApMaternoRef2") ? respuestas["ApMaternoRef2"] : ""),
                    respuestas["NombreRef2"],
                    respuestas["TipoTelR2"],
                    respuestas["LadaR2"],
                    respuestas["Telefono1Ref2"],
                    respuestas["ApPaternoBen"],
                    respuestas["ApMaternoBen"],
                    respuestas["NombreBeneficiario"],
                    respuestas["numcue"]);

                    var LogResultado = SerializeXML.SerializeObject(result);

                    Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "TraceServiciosYY", "ResultadoRegistroCredito - " + LogResultado);

                    Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "RegistraCredito", "ResultadoRegistroCredito Msj : " + resp.WSMensaje + " Cred : " + resp.WSCredito);

                    result.Add("WSidMensaje", resp.WSidMensaje);
                    result.Add("Valor", resp.WSidMensaje == "0000" ? resp.WSCredito : resp.WSMensaje);
                    result.Add("FolioCredito", resp.WSidMensaje == "0000" ? resp.WSFolio : resp.WSMensaje);
                    
                }
                else
                {
                    var tipoResultado = ConfigurationManager.AppSettings["PasarOriginacion"];
                    if (tipoResultado.Contains("false"))
                    {
                        result.Add("WSidMensaje", "0003");
                        result.Add("Valor", "Ocurrio un error al guardar");
                        result.Add("FolioCredito", "01");
                    }
                    else
                    {
                        var rnd = new Random();
                        result.Add("WSidMensaje", "0000");
                        result.Add("Valor", "12345" + rnd.Next(10000, 99999));
                        result.Add("FolioCredito", "01");
                    }
                }
            }
            catch (Exception errOrigina)
            {
                var mensaje = "Originación - No se registro el crédito - Error: " +
                              errOrigina.Message;
                Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsFormiik", mensaje);
            }


            return result;
        }

        /// <summary>
        /// Obtiene el código del estado
        /// </summary>
        /// <returns>Código del estado</returns>
        public static string ObtenerCesiXEstado(string estado)
        {
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "ObtenerCesiXEstado", "Obtener código estado : " + estado);
                var entCesi = new EntEstadisCesi().ObtenerEstadisCesi();

                var estadoCodigo = entCesi["estado"];

                if (estadoCodigo == null)
                    return null;

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "ObtenerCesiXEstado", "Obtener código obtenido : " + estadoCodigo);

                return estadoCodigo;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ObtenerCesiXEstado", "Error al obtener código : " + ex.Message);
                return null;
            }
        }

        public static void ReenviarOrdenOci(Ordenes orden,string nss)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ReenviarOrdenOci", "Orden : " + orden.idOrden + " Visita : " + orden.idVisita);

            if (orden.idVisita == 1)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ReenviarOrdenOci", "Orden : " + orden.idOrden + " Act Visita 2");
                EntDatosProspecto.ReenviarAMovil(orden.idOrden.ToString(),nss,"");
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ReenviarOrdenOci", "Orden : " + orden.idOrden + " Act Visita 2 OK");
            }
            else
            {
                if (orden.idVisita == 2)
                {
                    try
                    {
                        var documentosOrden = new DocumentosOrden(orden.idOrden.ToString(CultureInfo.InvariantCulture));
                        documentosOrden.DocumentosCompletos(2);

                        try
                        {
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocAcuRecTarjeta",
                                documentosOrden.GenerarDocumentos("DocAcuRecTarjeta"));
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ReenviarOrdenOci",
                                "Error al generar DocAcuRecTarjeta " + ex.StackTrace.ToString() +
                                (ex.InnerException != null ? ex.InnerException.StackTrace.ToString() : ""));
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocAcuRecTarjeta",
                                "http:\\\\imagenes.01800pagos.com\\" + orden.idOrden +
                                "\\Formalizacion\\DocAcuRecTarjeta.pdf"
                                );
                        }

                        try
                        {
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocBuroCredito",
                                documentosOrden.GenerarDocumentos("DocBuroCredito"));
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ReenviarOrdenOci",
                                "Error al generar DocBuroCredito " + ex.StackTrace.ToString() +
                                (ex.InnerException != null ? ex.InnerException.StackTrace.ToString() : ""));
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocBuroCredito",
                                "http:\\\\imagenes.01800pagos.com\\" + orden.idOrden +
                                "\\Formalizacion\\DocBuroCredito.pdf"
                                );
                        }

                        try
                        {
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocPreventivo",
                                documentosOrden.GenerarDocumentos("DocPreventivo"));
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ReenviarOrdenOci",
                                "Error al generar DocPreventivo " + ex.StackTrace.ToString() +
                                (ex.InnerException != null ? ex.InnerException.StackTrace.ToString() : ""));
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocPreventivo",
                                "http:\\\\imagenes.01800pagos.com\\" + orden.idOrden +
                                "\\Formalizacion\\DocPreventivo.pdf"
                                );
                        }

                        try
                        {
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocCartaSessionIrr",
                                documentosOrden.GenerarDocumentos("DocCartaSessionIrr"));
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ReenviarOrdenOci",
                                "Error al generar DocCartaSessionIrr " + ex.StackTrace.ToString() +
                                (ex.InnerException != null ? ex.InnerException.StackTrace.ToString() : ""));
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocCartaSessionIrr",
                                "http:\\\\imagenes.01800pagos.com\\" + orden.idOrden +
                                "\\Formalizacion\\DocCartaSessionIrr.pdf"
                                );
                        }

                        try
                        {
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocSolCredito",
                                documentosOrden.GenerarDocumentos("DocSolCredito"));
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ReenviarOrdenOci",
                                "Error al generar DocSolCredito " + ex.StackTrace.ToString() +
                                (ex.InnerException != null ? ex.InnerException.StackTrace.ToString() : ""));
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocSolCredito",
                                "http:\\\\imagenes.01800pagos.com\\" + orden.idOrden +
                                "\\Formalizacion\\DocSolCredito.pdf"
                                );
                        }

                        try
                        {
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocCarContrato",
                                documentosOrden.GenerarDocumentos("DocCarContrato"));
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ReenviarOrdenOci",
                                "Error al generar DocCarContrato " + ex.StackTrace.ToString() +
                                (ex.InnerException != null ? ex.InnerException.StackTrace.ToString() : ""));
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocCarContrato",
                                "http:\\\\imagenes.01800pagos.com\\" + orden.idOrden +
                                "\\Formalizacion\\DocCarContrato.pdf"
                                );
                        }

                        try
                        {
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocUnificado",
                                documentosOrden.GenerarDocumentos("DocUnificado"));
                        }
                        catch (Exception ex)
                        {
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocUnificado",
                                "http:\\\\imagenes.01800pagos.com\\" + orden.idOrden +
                                "\\Formalizacion\\DocUnificado.pdf"
                                );

                            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ReenviarOrdenOci",
                                "Error al generar DocUnificado " + ex.StackTrace +
                                (ex.InnerException != null ? ex.InnerException.StackTrace : ""));
                        }
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ReenviarOrdenOci", "Orden : " + orden.idOrden + " Act Visita 3");
                        EntDatosProspecto.ReenviarAMovil(orden.idOrden.ToString(CultureInfo.InvariantCulture), nss, "");
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ReenviarOrdenOci", "Orden : " + orden.idOrden + " Act Visita 3 OK");
                    }
                    catch (Exception ex)
                    {
                        var mensaje = "Originación - No se proceso el crédito - Error: " +
                                      ex.Message;
                        Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ReenviarOrdenOci", mensaje);
                    }
                }
            }
        }

        public static void ReenviarOrdenOciIncompleto(Ordenes orden, string nss)
        {
            EntDatosProspecto.ReenviarAMovil(orden.idOrden.ToString(), nss, "I");
        }
        
        public static Dictionary<bool, string> RegistrarTc(string tarjetaEncriptada, string idOrden, string nss,string usuario)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "RegistrarTc", "idOrden : " + idOrden + "TarjetaEnc : " + tarjetaEncriptada + " NSS : " + nss);

            var bloqueo = EntCredito.ValidacionCuenta(tarjetaEncriptada);

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "RegistrarTc", "Resultado Validacion orden "+idOrden+": " + bloqueo);

            if (bloqueo!="OK")
                return new Dictionary<bool, string> { { false, "Ya existe un credito con esta cuenta. Favor de verificar." } };

            var tarjeta = EncriptaTarjeta.DesencriptarTarjeta(tarjetaEncriptada);
            var entidad = new WsRespuestaRegTarjeta();

            var tarjetaExiste = EntDatosProspecto.ObtenerEntidadPorIdOrden(idOrden);

            if (!String.IsNullOrEmpty(tarjetaExiste))
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "RegistrarTc", "idOrden - " + idOrden + " , Crédito ya tiene una cuenta registrada");
                return new Dictionary<bool, string> { { true, "Crédito ya tiene una cuenta registrada. Si ya había registrado la cuenta, oprima guardar para continuar el proceso." } };
            }

            if (ConfigurationManager.AppSettings["Produccion"] == "true")
            {
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    var cliente = new WSRegistraSolicitudCreditoMejoravitAppSoapClient();

                    var modeloRequest = new ModeloRegistroTC {nss = nss, tarjeta = tarjeta, usuario = usuario};

                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "RegistrarTc", "RequestRegistroTC - " + modeloRequest.nss);

                    entidad = cliente.RegistraSolicitudTarjeta(nss, usuario, tarjeta);
                    
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "RegistrarTc", "NSS : " + nss + " idOrden : " + idOrden + " idMensaje : " + entidad.WSidMensaje + " Mensaje : " + entidad.WSMensaje + " EntFin : " + entidad.WSEntidadFinanciera);
                }
                catch (Exception ex)
                {
                    var mensaje = "Originación - No se registro la tarjeta - Error: " + ex.Message;
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "RegistrarTc", mensaje);
                    return new Dictionary<bool, string> { { false, "Error al registrar la tarjeta" } };
                }
            }
            else
            {
                var entidad1=new WsRespuestaRegTarjeta();
                if (ConfigurationManager.AppSettings["TarjetaValida"] == "true")
                {
                    entidad1.WSEntidadFinanciera = "152";
                    entidad1.WSMensaje = "Ok";
                    entidad1.WSidMensaje = "0000";
                }
                else
                {
                    entidad1.WSEntidadFinanciera = "0";
                    entidad1.WSMensaje = "Tarjeta No Valida";
                    entidad1.WSidMensaje = "0001";
                }
                entidad = entidad1;
            }

            if (entidad.WSidMensaje == "0000")
            {
                EntDatosProspecto.RegistrarEntidadFinanciera(entidad.WSEntidadFinanciera, idOrden);
                return new Dictionary<bool, string>{{true,"Tarjeta Registrada"}};
            }
            else
            {
                return new Dictionary<bool, string> { { false, entidad.WSMensaje } };
            }
        }
    }

    public class VariablesLog 
    {
        public string Nss { get; set; }
        public string AssignedTo { get; set; }
        public string wsNombreTitular { get; set; }
        public string plazo { get; set; }
        public string contarias { get; set; }
        public string monto_credito { get; set; }
        public string pago_mensual { get; set; }
        public string mano_obra { get; set; }
        public string PensionAlimenticia { get; set; }
        public string wsGastosApertura { get; set; }
        public string wsNumRegistroPatronal { get; set; }
        public string wsPuntMin { get; set; }
        public string wsPuntTotal { get; set; }
        public string wsCurp { get; set; }
        public string wsRFC { get; set; }
        public string WSNombreEmpresa { get; set; }
        public string ListaDocumentos { get; set; }
        public string NumIdentificacion { get; set; }
        public string FechaValidezIdentificacion { get; set; }
        public string CentObre { get; set; } 
        public string descripcionObrera { get; set; }
        public string LadaEmpresa { get; set; }
        public string TelEmpresa { get; set; }
        public string ExtEmpresa { get; set; }
        public string Calle { get; set; }
        public string NumeroInt { get; set; }
        public string NumeroExt { get; set; }
        public string EstadoCESI { get; set; }
        public string Estado { get; set; }
        public string DelegacionId { get; set; }
        public string Delegacion { get; set; }
        public string Colonia { get; set; }
        public string Cp { get; set; }
        public string GeneroCB { get; set; }
        public string EdoCivil { get; set; }
        public string RegimenCony { get; set; }
        public string Lada { get; set; }
        public string Telefono2Cte { get; set; }
        public string LadaCelular { get; set; }
        public string Telefono1Cte { get; set; }
        public string CorreoElectronico { get; set; }
        public string ApPaternoRef1 { get; set; }
        public string ApMaternoRef1 { get; set; }
        public string NombreRef1 { get; set; }
        public string TipoTelR1 { get; set; }
        public string LadaR1 { get; set; }
        public string Telefono1Ref1 { get; set; }
        public string ApPaternoRef2 { get; set; }
        public string ApMaternoRef2 { get; set; }
        public string NombreRef2 { get; set; }
        public string TipoTelR2 { get; set; }
        public string LadaR2 { get; set; }
        public string Telefono1Ref2 { get; set; }
        public string ApPaternoBen { get; set; }
        public string ApMaternoBen { get; set; }
        public string NombreBeneficiario { get; set; }
        public string numcue { get; set; }
    }

    public class ModeloRegistroTC
    {
        public string nss { get; set; }
        public string usuario { get; set; }
        public string tarjeta { get; set; }
        
    }
}
