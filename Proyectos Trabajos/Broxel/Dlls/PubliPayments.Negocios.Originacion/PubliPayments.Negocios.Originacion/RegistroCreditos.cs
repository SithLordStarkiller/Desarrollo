using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using PubliPayments.Entidades;
using PubliPayments.Entidades.Originacion;
using PubliPayments.Negocios.Originacion.WSRegistraSolicitudCreditoMejoravitApp11;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios.Originacion
{
    public static class RegistroCreditos 
    {
        public static Dictionary<string, string> RegistraCredito(Dictionary<String, String> respuestas)
        {
            var result = new Dictionary<string, string>();

            var datos = EntDatosProspecto.obtenerDatosRegistro(respuestas["Nss"], respuestas["Plazo"], respuestas["CentObre"]);

            var row = datos.Tables[0].Rows[0];

            //var nss = respuestas["Nss"];
            //var wsNombreTitular = row["wsNombreTitular"].ToString();
            //var Plazo = respuestas["Plazo"];
            //var contarias = row["contarias"].ToString();
            //var monto_credito = row["monto_credito"].ToString();
            //var pago_mensual = row["pago_mensual"].ToString();
            //var mano_obra = row["mano_obra"].ToString();
            //var wsGastosApertura = row["wsGastosApertura"].ToString();
            //var wsNumRegistroPatronal = row["wsNumRegistroPatronal"].ToString();
            //var wsPuntMin = row["wsPuntMin"].ToString();
            //var wsPuntTotal = row["wsPuntTotal"].ToString();
            //var wsCurp = row["wsCurp"].ToString();
            //var wsRFC = row["wsRFC"].ToString();
            //var ListaDocumentos = respuestas["ListaDocumentos"];
            //var NumIdentificacion = respuestas.ContainsKey("NumIdentificacionIFE") ? respuestas["NumIdentificacionIFE"] : respuestas["NumIdentificacionPas"];
            //var FechaValidezIdentificacion = respuestas["FechaValidezIdentificacion"];
            //var CentObre = respuestas["CentObre"];
            //var Calle = respuestas["Calle"];
            //var NumeroInt = respuestas["NumeroInt"];
            //var NumeroExt = respuestas["NumeroExt"];
            //var Estado = respuestas["Estado"];
            //var Delegacion = respuestas["Delegacion"];
            //var Colonia = respuestas["Colonia"];
            //var Cp = respuestas["Cp"];
            //var GeneroCB = respuestas["GeneroCB"];
            //var EdoCivil = respuestas["EdoCivil"];
            //var RegimenCony = respuestas.ContainsKey("RegimenCony") ? respuestas["RegimenCony"] : "";
            //var Telefono2Cte1 = respuestas["Telefono2Cte"].Substring(0, 3);
            //var Telefono2Cte = respuestas["Telefono2Cte"].Substring(3);
            //var Telefono1Cte11 = respuestas["Telefono1Cte"].Substring(0, 3);
            //var Telefono1Cte = respuestas["Telefono1Cte"].Substring(3);
            //var CorreoElectronico = respuestas["CorreoElectronico"];
            //var ApPaternoRef1 = respuestas["ApPaternoRef1"];
            //var ApMaternoRef1 = respuestas.ContainsKey("ApMaternoRef1") ? respuestas["ApMaternoRef1"] : "";
            //var NombreRef1 = respuestas["NombreRef1"];
            //var TipoTelR1 = respuestas["TipoTelR1"];
            //var Telefono1Ref11 = respuestas["Telefono1Ref1"].Substring(0, 3);
            //var Telefono1Ref1 = respuestas["Telefono1Ref1"].Substring(3);
            //var ApPaternoRef2 = respuestas["ApPaternoRef2"];
            //var ApMaternoRef2 = respuestas.ContainsKey("ApMaternoRef2") ? respuestas["ApMaternoRef2"] : "";
            //var NombreRef2 = respuestas["NombreRef2"];
            //var TipoTelR2 = respuestas["TipoTelR2"];
            //var Telefono1Ref21 = respuestas["Telefono1Ref2"].Substring(0, 3);
            //var Telefono1Ref2 = respuestas["Telefono1Ref2"].Substring(3);
            
            try
            {

                var logVariables = respuestas["Nss"]+", "+
                        respuestas["AssignedTo"]+", "+
                        row["wsNombreTitular"].ToString()+", "+
                        row["plazo"] + ", " +
                        row["contarias"].ToString()+", "+
                        row["monto_credito"].ToString()+", "+
                        row["pago_mensual"].ToString()+", "+
                        row["mano_obra"].ToString()+", "+ "0"+", "+
                        row["wsGastosApertura"].ToString()+", "+
                        row["wsNumRegistroPatronal"].ToString()+", "+
                        row["wsPuntMin"].ToString()+", "+
                        row["wsPuntTotal"].ToString()+", "+
                        row["wsCurp"].ToString()+", "+
                        row["wsRFC"].ToString()+", "+
                        row["WSNombreEmpresa"].ToString()+", "+
                        respuestas["ListaDocumentos"]+", "+
                        (respuestas.ContainsKey("NumIdentificacionIFE") ? respuestas["NumIdentificacionIFE"] : respuestas["NumIdentificacionPas"])+", "+
                        respuestas["FechaValidezIdentificacion"]+", "+
                        respuestas["CentObre"]+", "+
                        row["descripcionObrera"].ToString() + ", " + 
                        "0" + ", " + "0" + ", " + "0" + ", " +
                        respuestas["Calle"]+", "+
                        respuestas["NumeroInt"]+", "+
                        respuestas["NumeroExt"]+", "+ 
                        respuestas["EstadoCESI"]+", "+
                        respuestas["Estado"]+", "+ 
                        respuestas["DelegacionId"]+", "+
                        respuestas["Delegacion"]+", "+
                        respuestas["Colonia"]+", "+
                        (respuestas["Cp"].Length >= 5 ? respuestas["Cp"] : respuestas["Cp"].PadLeft(5, '0')) + ", " +
                        (respuestas["GeneroCB"] == "Masculino" ? "1" : "2")+", "+
                        respuestas["EdoCivil"]+", "+
                        (respuestas.ContainsKey("RegimenCony") ? respuestas["RegimenCony"] : "0")+", "+
                        respuestas["Lada"]+", "+
                        respuestas["Telefono2Cte"]+", "+ 
                        respuestas["LadaCelular"]+", "+
                        respuestas["Telefono1Cte"]+", "+
                        (respuestas.ContainsKey("CorreoElectronico") ? respuestas["CorreoElectronico"] : "") + ", " +
                        respuestas["ApPaternoRef1"]+", "+
                        (respuestas.ContainsKey("ApMaternoRef1") ? respuestas["ApMaternoRef1"] : "")+", "+
                        respuestas["NombreRef1"]+", "+
                        respuestas["TipoTelR1"]+", "+
                        respuestas["LadaR1"]+", "+
                        respuestas["Telefono1Ref1"]+", "+
                        respuestas["ApPaternoRef2"]+", "+
                        (respuestas.ContainsKey("ApMaternoRef2") ? respuestas["ApMaternoRef2"] : "")+", "+
                        respuestas["NombreRef2"]+", "+
                        respuestas["TipoTelR2"]+", "+
                        respuestas["LadaR2"]+", "+
                        respuestas["Telefono1Ref2"];
                
                Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "RegistroCredito - DatosServicio", logVariables);

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
                    row["mano_obra"].ToString(), "0",
                    row["wsGastosApertura"].ToString(),
                    row["wsNumRegistroPatronal"].ToString(),
                    row["wsPuntMin"].ToString(),
                    row["wsPuntTotal"].ToString(),
                    row["wsCurp"].ToString(),
                    row["wsRFC"].ToString(),
                    row["WSNombreEmpresa"].ToString(),
                    respuestas["ListaDocumentos"],
                    (respuestas.ContainsKey("NumIdentificacionIFE") ? respuestas["NumIdentificacionIFE"] : respuestas["NumIdentificacionPas"]),
                    respuestas["FechaValidezIdentificacion"],
                    respuestas["CentObre"],
                    row["descripcionObrera"].ToString(),
                    "0", "0", "0",
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
                    (respuestas.ContainsKey("RegimenCony") ? respuestas["RegimenCony"] : "0"),
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
                    respuestas["Telefono1Ref2"]);

                    result.Add("WSidMensaje", resp.WSidMensaje);
                    result.Add("Valor", resp.WSidMensaje == "0000" ? resp.WSCredito : resp.WSMensaje);
                    result.Add("FolioCredito", resp.WSidMensaje == "0000" ? resp.WSFolio : resp.WSMensaje);
                }
                else
                {
                    var rnd = new Random();
                    result.Add("WSidMensaje", "0000");
                    result.Add("Valor", "12345" + rnd.Next(10000, 99999));
                    result.Add("FolioCredito", "01");
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

        public static void ReenviarOrdenOci(Ordenes orden,string nss)
        {
            if (orden.idVisita == 1)
            {
                EntDatosProspecto.ReenviarAMovil(orden.idOrden.ToString(),nss,"");
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
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "RegistroCredito - ReenviarOrdenOci", "Error al generar DocAcuRecTarjeta " + ex.StackTrace.ToString() + (ex.InnerException!=null?ex.InnerException.StackTrace.ToString():""));
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocAcuRecTarjeta",
                                "http:\\\\imagenes.01800pagos.com\\" + orden.idOrden + "\\Formalizacion\\DocAcuRecTarjeta.pdf"
                                );
                        }

                        try
                        {
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocBuroCredito",
                                documentosOrden.GenerarDocumentos("DocBuroCredito"));
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "RegistroCredito - ReenviarOrdenOci", "Error al generar DocBuroCredito " + ex.StackTrace.ToString() + (ex.InnerException != null ? ex.InnerException.StackTrace.ToString() : ""));
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocBuroCredito",
                               "http:\\\\imagenes.01800pagos.com\\" + orden.idOrden + "\\Formalizacion\\DocBuroCredito.pdf"
                               );
                        }

                        try
                        {
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocSolCredito",
                                documentosOrden.GenerarDocumentos("DocSolCredito"));
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "RegistroCredito - ReenviarOrdenOci", "Error al generar DocSolCredito " + ex.StackTrace.ToString() + (ex.InnerException != null ? ex.InnerException.StackTrace.ToString() : ""));
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocSolCredito",
                               "http:\\\\imagenes.01800pagos.com\\" + orden.idOrden + "\\Formalizacion\\DocSolCredito.pdf"
                               );
                        }

                        try
                        {
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocCarContrato",
                                documentosOrden.GenerarDocumentos("DocCarContrato"));
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "RegistroCredito - ReenviarOrdenOci", "Error al generar DocCarContrato " + ex.StackTrace.ToString() + (ex.InnerException != null ? ex.InnerException.StackTrace.ToString() : ""));
                            new EntGestionadas().AgregarDocumentoOrden(orden.idOrden, "DocCarContrato",
                               "http:\\\\imagenes.01800pagos.com\\" + orden.idOrden + "\\Formalizacion\\DocCarContrato.pdf"
                               );
                        }


                        EntDatosProspecto.ReenviarAMovil(orden.idOrden.ToString(CultureInfo.InvariantCulture), nss,"");
                    }
                    catch (Exception ex)
                    {
                        var mensaje = "Originación - No se proceso el crédito - Error: " +
                              ex.Message;
                        Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsFormiik", mensaje);
                    }
                }
            }
        }

        public static void ReenviarOrdenOciIncompleto(Ordenes orden, string nss)
        {
            EntDatosProspecto.ReenviarAMovil(orden.idOrden.ToString(), nss, "I");
        }

        public static Dictionary<bool, string> RegistrarTc(string tarjetaEncriptada, string idOrden, string nss)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "RegistrarTc","idOrden: " + idOrden );
            var tarjeta = EncriptaTarjeta.DesencriptarTarjeta(tarjetaEncriptada);
            var entidad = new WsRespuestaRegTarjeta();

            if (ConfigurationManager.AppSettings["Produccion"] == "true")
            {
                try
                {
                
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var cliente = new WSRegistraSolicitudCreditoMejoravitAppSoapClient();
                entidad = cliente.RegistraSolicitudTarjeta(nss, "0", tarjeta);

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "RegistroCreditos",
                    "NSS: " + nss + ", idOrden: " + idOrden + ", idMensaje: " + entidad.WSidMensaje + ", Mensaje: " +
                    entidad.WSMensaje + ", EntFin: " + entidad.WSEntidadFinanciera);
                }
                catch (Exception ex)
                {
                    var mensaje = "Originación - No se registro la tarjeta - Error: " +
                              ex.Message;
                    Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsFormiik", mensaje);
                    return new Dictionary<bool, string> { { false, "Error al registrar la tarjeta" } };
                }
            }
            else
            {
                var entidad1=new WsRespuestaRegTarjeta();
                if (ConfigurationManager.AppSettings["TarjetaValida"] == "true")
                {
                    entidad1.WSEntidadFinanciera = "132";
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
}
