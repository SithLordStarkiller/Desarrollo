using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Device.Location;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using PubliPayments.Entidades;
using PubliPayments.Utiles;



namespace PubliPayments.Negocios
{
    public class Respuesta
    {
        private static readonly string CampoCelularSms = "CelularSMS_";
        private static readonly string CampoCelularSMSAct = "CelularSMS_Actualizado";
        private static readonly string CamposRespSMS = "AssignedTo,FinalDate,CelularSMS_Actualizado";
        private static readonly string CamposRespIdentif = "AssignedTo,FinalDate,CelularSMS_Actualizado";
        private static readonly string CampoCelularSMSAnt = "CelularSMS_Ant";
        private static readonly string CampoAutorizacionRespSMS = "ResultadoClaveSMS";
        private static readonly string CodigoSMS = "CodSMS";
        private static readonly string VarVivRecuperacion = "Abandonada,deshabitada,Invadida,Vandalizada";
        private static readonly int _mtsRecuperacion = 100;

        private static readonly string IgnorarIncFormalizacion =
            "ExternalType,AssignedTo,InitialDate,FinalDate,ResponseDate,InitialLatitude,FinalLatitude,InitialLongitude,FinalLongitude,FormiikResponseSource";
        


        public string GuardarRespuesta( int idOrden, Dictionary<string, string> dicRespuesta, string origen, string usuario, int idUsuarioLog, bool isProduction, string aplicacion)
        {
            try
            {
                try
                {//Se guarda la información que llega.
                    var valoresRespuesta = new StringBuilder();
                    valoresRespuesta.Append(String.Join("|", dicRespuesta.Select(x => x.Key + "," + x.Value)));
                    Trace.WriteLine(string.Format("{0} - Respuesta.GuardarRespuesta - idOrden:{1}, dicRespuesta:{2}, origen:{3}, usuario:{4}, idUsuarioLog:{5}, isProduction:{6}, aplicación:{7})",
                                         DateTime.Now,idOrden, valoresRespuesta, origen, usuario, idUsuarioLog, isProduction, aplicacion));
                }
                catch (Exception ex){    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, origen + " - " + aplicacion, "No se pudo guardar información de la respuesta "+ex.Message);}


                dicRespuesta = dicRespuesta.ToDictionary(s => s.Key.ToUpper().StartsWith("NUMIDENTIFICACION") ? "NumIdentificacion" : s.Key, s => s.Value);
                dicRespuesta = dicRespuesta.ToDictionary(s => s.Key, s => s.Key.ToUpper().Equals("EXTERNALTYPE") && s.Value.ToUpper().Contains("ORIGINAPP") ? "Formalizacion" : s.Value);
        
                string mensaje;
                bool esCapturaWeb = origen.ToUpper() == "CAPTURAWEB";
                var context = new SistemasCobranzaEntities();
                Dictionary<string, string> dicRespuestaFinal;
                bool esCallCenter = false;
                bool esCallCenterOut = false;
                
                var Entusuario = new EntUsuario();

                try
                {
                    if (idOrden < 0)
                    {
                        var numCredito = dicRespuesta.FirstOrDefault(x => x.Key.ToUpper().Equals("EXTERNALID")).Value;
                        var modelOrden = new Orden().ObtenerOrdenxCredito(numCredito);

                        var usuarioModel = Entusuario.ObtenerUsuarioXUsuario(usuario);
                        esCallCenterOut = usuarioModel.EsCallCenterOut;
                        if (modelOrden == null)
                        {
                            var usuarioPadreModel = Entusuario.ObtenerUsuarioPorId(usuarioModel.IdPadre);   
                            var ordenCreada = esCallCenterOut ? new BuscarOrdenes().CrearOrden(numCredito, usuarioModel.IdUsuario, usuarioModel.IdPadre, usuarioPadreModel.idPadre, usuarioModel.IdDominio, 0) : new BuscarOrdenes().CrearOrden(numCredito, -111, -110, idUsuarioLog, -1, 0);
                            if (ordenCreada < 0)
                            {
                                mensaje = "No se puede generar la orden para el crédito " + numCredito;
                                Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, origen + " - " + aplicacion, mensaje);
                                Email.EnviarEmail(
                                    "sistemasdesarrollo@publipayments.com",
                                    "Error " + origen + ":aplicación " + aplicacion,
                                    string.Format("{0} - {1}", DateTime.Now, mensaje));
                                return esCapturaWeb ? mensaje : string.Empty;
                            }
                            idOrden = ordenCreada;
                            dicRespuesta = redireccionImagenes(dicRespuesta, idOrden);
                        }
                        else
                        {
                            idOrden = modelOrden.IdOrden;
                        }
                    }
                    else
                    {
                        var usuarioModel = Entusuario.ObtenerUsuarioPorId(Convert.ToString(idUsuarioLog));
                        esCallCenterOut = usuarioModel.EsCallCenterOut;
                    }
                    
                }
                catch (Exception ex)
                {

                    string exceptionInner = ex.InnerException != null ? ex.InnerException.Message : "";
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Respuesta", "Error: GuardarRespuesta.CrearOrden_" + origen + " - " + aplicacion + ex.Message + " inner " + exceptionInner);
                    
                    throw;
                }
                

                Respuestas resp = context.Respuestas.FirstOrDefault(x => x.idOrden == idOrden);
                var entSms = new EntSMS();

                int tipoFormulario;
                string nombreFormulario = "";
                var gpsDictionary = new Dictionary<string, string>();
                if (!aplicacion.ToUpper().Contains("SIRA"))
                {
                    var formularioRes = dicRespuesta.FirstOrDefault(x => x.Key.ToUpper().Equals("FORMIIKRESPONSESOURCE"));
                    tipoFormulario = (formularioRes.Value.ToUpper() == "MOBILE" ? 1 : 2);
                    nombreFormulario = dicRespuesta.FirstOrDefault(x => x.Key.ToUpper().Equals("EXTERNALTYPE")).Value;
                    esCallCenter = nombreFormulario.ToUpper().Contains("CALLCENTER");

                    try
                    {
                        if (nombreFormulario.ToUpper().Contains("COBSOCIAL"))   //Solo en caso de que sea cobranza se llena el diccionario
                        {
                            gpsDictionary.Add("Initial", dicRespuesta.FirstOrDefault(x => x.Key.ToUpper().Equals("INITIALLATITUDE")).Value + " " + dicRespuesta.FirstOrDefault(x => x.Key.ToUpper().Equals("INITIALLONGITUDE")).Value);
                            gpsDictionary.Add("Final", dicRespuesta.FirstOrDefault(x => x.Key.ToUpper().Equals("FINALLATITUDE")).Value + " " + dicRespuesta.FirstOrDefault(x => x.Key.ToUpper().Equals("FINALLONGITUDE")).Value);
                            gpsDictionary.Add("gps_automatico", dicRespuesta.FirstOrDefault(x => x.Key.ToUpper().Equals("GPS_AUTOMATICO")).Value.Replace("Lat:", "").Replace("Lon:", " ").Replace("Sat:", " "));
                            gpsDictionary.Add("gpsFin", dicRespuesta.FirstOrDefault(x => x.Key.ToUpper().Equals("GPSFIN")).Value.Replace("Lat:", "").Replace("Lon:", " ").Replace("Sat:", " "));
                        }
                    }
                    catch{}
                }
                else
                {
                    tipoFormulario = 1;
                }
              
                
                Ordenes orden = context.Ordenes.FirstOrDefault(x => x.idOrden == idOrden);
                 if (orden != null)
                {
                    if (resp == null || aplicacion.ToUpper().Contains("ORIGINACIONMOVIL") || aplicacion.ToUpper().Contains("MYO"))
                    {
                        if (orden.Estatus > 2 && aplicacion.ToUpper().Contains("ORIGINACIONMOVIL"))
                        {
                            return string.Empty;
                        }
                        Creditos credito = nombreFormulario.ToUpper().Contains("REGISTROUSUARIO") ? (new Creditos{ CV_RUTA = "RA" }): context.Creditos.FirstOrDefault(x => x.CV_CREDITO == orden.num_Cred);

                        if (credito != null)
                        {
                            if (origen.ToUpper() == "MOBILEPENDIENTE" )
                            {
                                orden.idUsuario = idUsuarioLog;
                            }

                            if (orden.idUsuario != 0 || esCallCenter || esCallCenterOut)
                            {

                                FormularioModel formulario;
                                int idcampo = 0;
                                bool actualizaCelSMS;
                                bool codigoCelSMS;
                                try
                                {
                                    orden.Estatus = 3;
                                    orden.FechaModificacion = DateTime.Now;
                                    orden.FechaRecepcion = DateTime.Now;
                                    idcampo = context.CamposRespuesta.Max(x => x.idCampo);

                                    if (tipoFormulario == 2 && orden.Estatus == 6)
                                    {
                                        return "Error -7: Orden Sincronizando ";
                                    }
                                    formulario = !aplicacion.ToUpper().Contains("SIRA") ?
                                       new EntFormulario().ObtenerListaFormularios(credito.CV_RUTA).FirstOrDefault(x => x.Captura == tipoFormulario && x.Nombre.ToUpper() == nombreFormulario.ToUpper()) :
                                       new EntFormulario().ObtenerListaFormularios(credito.CV_RUTA).FirstOrDefault(x => x.Captura == tipoFormulario);


                                     actualizaCelSMS = dicRespuesta.ContainsKey(CampoCelularSMSAct);
                                     codigoCelSMS = dicRespuesta.ContainsKey(CodigoSMS);
                                    if (aplicacion.ToUpper().Contains("ORIGINACIONMOVIL"))
                                    {
                                        if (orden.Tipo == "I")
                                        {
                                            dicRespuesta = FormalizacionCamposDuplicadosCorrecciones(dicRespuesta);
                                        }
                                        else if (orden.idVisita > 1)
                                        {
                                            dicRespuesta = FormalizacionCamposDuplicados(dicRespuesta);
                                        }
                                    }

                                    if (aplicacion.ToUpper().Contains("MYO"))
                                        if (orden.Tipo == " " && orden.Estatus == 3 && (orden.idVisita == 1 || orden.idVisita == 2))
                                        {
                                            if (dicRespuesta.ContainsKey("FormiikResponseSource")) dicRespuesta.Remove("FormiikResponseSource");
                                            if (dicRespuesta.ContainsKey("ExternalType")) dicRespuesta.Remove("ExternalType");
                                        }

                                }
                                catch (Exception ex)
                                {
                                    string exceptionInner = ex.InnerException != null ? ex.InnerException.Message : "";
                                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1,"Respuesta","Error: GuardarRespuesta.ObtenerFormulario_"+ origen + " - " + aplicacion + ex.Message + " inner " + exceptionInner);
                                    throw;
                                }
                               

                                bool tieneSMS = false;

                                if (formulario != null)
                                {
                                    dicRespuestaFinal = (actualizaCelSMS)
                                        ? DiccionarioSMS(dicRespuesta, idOrden)
                                        : dicRespuesta;
                                    if (dicRespuestaFinal != null)
                                    {

                                        try
                                        {
                                            foreach (KeyValuePair<string, string> respuestaTemp in dicRespuestaFinal)
                                            {
                                                KeyValuePair<string, string> respuesta = respuestaTemp;
                                                if (respuesta.Key.ToUpper() == "PRODUCTID" ||
                                                    respuesta.Key.ToUpper() == "EXTERNALID" ||
                                                    respuesta.Key.ToUpper() == "CELULARSMS_RECIBIDO" ||
                                                    respuesta.Key.ToUpper() == "DICGEST_ANT" ||
                                                    respuesta.Key.ToUpper() == "DICAPLICASMS") continue;

                                                var valorRespuesta = CambioValRecuperacion(credito, respuesta,
                                                    gpsDictionary);
                                                respuesta = valorRespuesta;

                                                CamposRespuesta campo =
                                                    context.CamposRespuesta.FirstOrDefault(
                                                        x =>
                                                            x.Nombre == respuesta.Key &&
                                                            x.idFormulario == formulario.IdFormulario);
                                                if (campo == null)
                                                {
                                                    //si no se encontró el campo lo inserta
                                                    idcampo++;

                                                    campo = new CamposRespuesta
                                                    {
                                                        idCampo = idcampo,
                                                        Nombre = respuesta.Key,
                                                        Tipo = respuesta.Key.EndsWith("_ACT") ? 2 : 1,
                                                        idFormulario = formulario.IdFormulario
                                                    };
                                                    context.CamposRespuesta.Add(campo);
                                                }

                                                if (respuesta.Key.ToUpper().StartsWith(CampoCelularSms.ToUpper()) && respuesta.Value.Trim() != "" && respuesta.Value.Trim().Length == 10)
                                                {
                                                    tieneSMS = true;
                                                    orden.Tipo = "S";
                                                    var bitacoraSMS = entSms.InsertaBitacoraAutorizaciones(orden.num_Cred);
                                                    if (bitacoraSMS < 1)
                                                    {
                                                        Email.EnviarEmail(
                                                            "sistemasdesarrollo@publipayments.com",
                                                            "Error " + origen + ":aplicación " + aplicacion,
                                                            "Error al intentar mandar a bitácora orden SMS " +
                                                            orden.idOrden);
                                                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1,
                                                            origen + " - " + aplicacion,
                                                            "Error al intentar mandar a bitácora orden SMS " +
                                                            orden.idOrden);
                                                    }
                                                }

                                                context.Respuestas.Add(new Respuestas
                                                {
                                                    idCampo = campo.idCampo,
                                                    idOrden = idOrden,
                                                    Valor = (aplicacion.ToUpper().Contains("ORIGINACIONMOVIL")
                                                        ? limpiaNoPermitidos(respuesta.Value.Length > 350
                                                            ? respuesta.Value.Substring(0, 350)
                                                            : respuesta.Value).ToUpper()
                                                        : limpiaNoPermitidos(respuesta.Value.Length > 350
                                                            ? respuesta.Value.Substring(0, 350)
                                                            : respuesta.Value)),
                                                    idDominio = orden.idDominio,
                                                    idUsuarioPadre = orden.idUsuarioPadre,
                                                    idFormulario = formulario.IdFormulario
                                                });
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Respuesta", "Error: GuardarRespuesta.Procesar CamposRespuesta_" + origen + " - " + aplicacion + ex.Message);
                                            throw;
                                        }
                                    }


                                    try
                                    {
                                        orden.Tipo = !tieneSMS && !codigoCelSMS ? (esCallCenter || esCallCenterOut ? "C" : " ") : (esCallCenter || esCallCenterOut ? ("C" + orden.Tipo).Trim() : orden.Tipo);
                                        orden.idUsuario = esCallCenter && orden.idUsuario == 0 ? -111 : orden.idUsuario;
                                        context.SaveChanges();
                                        new LlamadasSinExitos().BorrarLlamadaSinExito(orden.num_Cred);
                                      
                                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, origen + " - " + aplicacion,
                                            "La respuesta de la orden: " + idOrden + " se guardó correctamente");
                                        if (codigoCelSMS)
                                        {
                                            string codigo = dicRespuesta[CodigoSMS];
                                            if (ProcesarCodigoSMS(idOrden, codigo) == 1)
                                            {
                                                if (isProduction && aplicacion.ToUpper() == "LONDON")
                                                    Gestiones.EnviarGestion(idOrden, usuario, "V");     
                                            }
                                            else
                                            {
                                                if (isProduction && aplicacion.ToUpper() == "LONDON" )
                                                    Gestiones.EnviarGestion(idOrden, usuario, "P");     
                                            }
                                        }
                                        else if (actualizaCelSMS)
                                        {
                                            entSms.RestaurarRespuesta(idOrden, 0);
                                        }
                                        if (tieneSMS || actualizaCelSMS)
                                        {
                                            new ProcesarTelefonos().InsertarTipoTelefono(orden.idOrden);
                                        }
                                        if (esCapturaWeb && orden.idUsuario > 0 && !(esCallCenter || esCallCenterOut))
                                        {
                                            //Se cancela La orden en el dispositivo 
                                            try
                                            {
                                                    new EntBitacoraEnvio().CancelarOrdenXCW(idOrden.ToString(CultureInfo.InvariantCulture));
                                            }
                                            catch (Exception ex)
                                            {

                                                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "Respuesta", "Error: GuardarRespuesta.CancelarOrdenXCW_" + origen + " - " + aplicacion + ex.Message);
                                                Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                                                    "Error " + "CapturaWeb - CancelarOrden",
                                                    "Error al intentar insertar la cancelación de Ordenes: " + idOrden + " en BitacoraEnvio: " +
                                                    ex.Message);
                                            }
                                        }
                                        /*Enviar gestión*/
                                        if (isProduction && aplicacion.ToUpper() == "LONDON" && !codigoCelSMS)
                                            Gestiones.EnviarGestion(idOrden, usuario, "P");     ////// se manda a llamar el Ws de London

                                        // Actualizar dictamen de la orden
                                        if (!new EntOrdenes().ActualizarDictamenOrden(idOrden))
                                        {
                                            Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "GuardarRespuesta",
                                                "Fallo al intentar actualizar el dictamen de la orden " + idOrden);
                                        }

                                    }
                                    catch (DbEntityValidationException exEntidad)
                                    {


                                        Trace.WriteLine("M-GuardarRespuesta-DbEntityValidationException-" +
                                                        exEntidad.Message);

                                        string err = string.Format("{0} - {1} \n", DateTime.Now, "idOrden = " + idOrden);
                                        foreach (var errores in exEntidad.EntityValidationErrors)
                                        {
                                            foreach (var error in errores.ValidationErrors)
                                            {
                                                err += error.ErrorMessage + "\n";
                                            }
                                        }
                                        string exceptionInner = exEntidad.InnerException.Message ?? "";
                                        exceptionInner += "___" + exEntidad.InnerException.InnerException.Message ?? "";
                                        Email.EnviarEmail(
                                            "sistemasdesarrollo@publipayments.com",
                                            "Error " + origen + ":aplicación " + aplicacion,
                                            "Error al intentar guardar en la base de datos: " + exEntidad.Message +
                                            " inner " + exceptionInner + "\n" +
                                            err);
                                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, origen + " - " + aplicacion,
                                            "Error al intentar guardar en la base de datos: " + exEntidad.Message +
                                            " inner " + exceptionInner + "\n" +
                                            err);
                                        throw new ApplicationException(
                                            "Error al intentar guardar en la base de datos: " +
                                            exEntidad.Message + " inner " + exceptionInner);
                                    }
                                    catch (Exception exGenericaEntidad)
                                    {
                                        string exceptionInner = exGenericaEntidad.InnerException.Message ??
                                                                exGenericaEntidad.InnerException.Message;
                                        exceptionInner += "___" +
                                                          exGenericaEntidad.InnerException.InnerException.Message ??
                                                          exGenericaEntidad.InnerException.InnerException.Message;
                                        Trace.WriteLine("M-GuardarRespuesta-exGenericaEntidad" +
                                                        exGenericaEntidad.Message + " inner " + exceptionInner);
                                        Email.EnviarEmail(
                                            "sistemasdesarrollo@publipayments.com",
                                            "Error " + origen + ":aplicación " + aplicacion,
                                            "Error al intentar guardar en la base de datos: " +
                                            exGenericaEntidad.Message +
                                            "\n" + string.Format("{0} - {1} \n", DateTime.Now, "idOrden = " + idOrden));
                                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, origen + " - " + aplicacion,
                                            "Error al intentar guardar en la base de datos: " +
                                            exGenericaEntidad.Message + " inner " + exceptionInner +
                                            "\n" +
                                            string.Format("{0} - {1} \n", DateTime.Now, "idOrden = " + idOrden));
                                        throw new ApplicationException(
                                            "Error al intentar guardar en la base de datos: " +
                                            exceptionInner);
                                    }

                                  

                                }
                                else
                                {
                                    mensaje = "No se encontró el formulario para la ruta : " + credito.CV_RUTA;
                                    Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, origen + " - " + aplicacion, mensaje);
                                    Email.EnviarEmail(
                                        "sistemasdesarrollo@publipayments.com",
                                        "Error " + origen + ":aplicación " + aplicacion,
                                        string.Format("{0} - {1}", DateTime.Now, mensaje));
                                    return esCapturaWeb ? mensaje : string.Empty;
                                }
                            }
                            else
                            {
                                mensaje = "No se puede guardar la respuesta para la orden: " + idOrden +
                                          " porque no está  asignada.";
                                Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, origen + " - " + aplicacion, mensaje);
                                Email.EnviarEmail(
                                    "sistemasdesarrollo@publipayments.com",
                                    "Error " + origen + ":aplicación " + aplicacion,
                                    string.Format("{0} - {1}", DateTime.Now, mensaje));
                                return esCapturaWeb ? mensaje : string.Empty;
                            }
                        }
                        else
                        {
                            mensaje = "No se encontró el crédito asociado : " + orden.num_Cred;
                            Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, origen + " - " + aplicacion, mensaje);
                            Email.EnviarEmail(
                                "sistemasdesarrollo@publipayments.com",
                                "Error " + origen + ":aplicación " + aplicacion,
                                string.Format("{0} - {1}", DateTime.Now, mensaje));
                            return esCapturaWeb ? mensaje : string.Empty;
                        }
                    }
                    else if (esCallCenter || orden.Tipo.ToUpper().Contains("C") || esCallCenterOut) /* se aplica proceso para que las respuestas se almacenen provisionalmente  en otra tabla y se mande la cancelación */
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, origen + " - " + aplicacion, "Se inicia proceso de recuperación respuesta automática, credito: " + orden.num_Cred);

                             mensaje = "Se procede a cancelar, orden con respuesta anterior " + orden.num_Cred;
                            var valoresRespuesta = new StringBuilder();
                            valoresRespuesta.Append(String.Join("|", dicRespuesta.Select(x => x.Key + "," + x.Value)));
                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Respuesta-RespuestasPendientes", valoresRespuesta.ToString());

                            var ordenNegocio = new Orden(3, isProduction, null, null, "", "", aplicacion);

                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, origen + " - " + aplicacion, mensaje);
                            if (orden.Estatus == 4)
                            {
                                var bloqueo = new EntFormulario().ObtenerBloqueoReverso(orden.idOrden);
                                if (!Convert.ToBoolean(bloqueo))
                                {
                                    mensaje = "El crédito no puede ser re-versado " + orden.num_Cred;
                                    Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                                      "Error " + origen + ":aplicación " + aplicacion,
                                      string.Format("{0} - {1}", DateTime.Now, mensaje));
                                    return esCapturaWeb ? mensaje : string.Empty;
                                }
                                var reversos = ordenNegocio.CambiarEstatusOrdenes(orden.idOrden.ToString(CultureInfo.InvariantCulture), 3, true, true, idUsuarioLog);

                                if (reversos == 0)
                                {
                                    mensaje = "El crédito no puede ser re-versado " + orden.num_Cred;
                                    Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                                      "Error " + origen + ":aplicación " + aplicacion,
                                      string.Format("{0} - {1}", DateTime.Now, "CambiarEstatusOrdenes :" + mensaje));
                                    return esCapturaWeb ? mensaje : string.Empty;
                                }
                            }
                            int totalCanceladas = ordenNegocio.CancelarCc(new List<int> { orden.idOrden }, idUsuarioLog);
                            if (totalCanceladas > 0)
                            {
                                var respPendientes = ordenNegocio.InsertarRespuestasPendientes(orden.idOrden, esCallCenter ? -111 : esCallCenterOut ? Convert.ToInt32(idUsuarioLog) : orden.idUsuarioAnterior, valoresRespuesta);
                                if (respPendientes.Trim() != "")
                                {
                                    mensaje = "No se pudo guardar los registros de las respuestas de la orden gestionada por CC " + orden.num_Cred;
                                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, origen + " - " + aplicacion, mensaje + " " + respPendientes);
                                    Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                                        "Error " + origen + ":aplicación " + aplicacion,
                                        string.Format("{0} - {1}", DateTime.Now, respPendientes));
                                }
                            }
                            else
                            {
                                mensaje = "No se pudo cancelar la orden gestionada por CC " + orden.num_Cred;
                                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, origen + " - " + aplicacion, mensaje);
                                Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                                    "Error " + origen + ":aplicación " + aplicacion,
                                    string.Format("{0} - {1}", DateTime.Now, mensaje));
                            }
                        
                        return string.Empty;
                   }
                    else
                    {
                        mensaje = "Ya existe una respuesta para esta ExternalID : " + idOrden;
                        Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, origen + " - " + aplicacion, mensaje);
                        Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                            "Error " + origen + ":aplicación " + aplicacion,
                            string.Format("{0} - {1}", DateTime.Now, mensaje));
                        return esCapturaWeb ? mensaje : string.Empty;
                    }
                }
                   else
                    {
                        mensaje = "No se encontró la orden : " + idOrden;
                        Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, origen + " - " + aplicacion, mensaje);
                        Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                            "Error " + origen + ":aplicación " + aplicacion,
                            string.Format("{0} - {1}", DateTime.Now, mensaje));
                        return esCapturaWeb ? mensaje : string.Empty;
                   }
            }
            catch (Exception ex)
            {
                string exceptionInner = ex.InnerException != null ? ex.InnerException.Message : "";
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, origen + " - " + aplicacion,
                    "GuardarRespuesta - Error:" + ex.Message + " inner " + exceptionInner);
                return "Error -6: " + ex.Message;
            }
            return string.Empty;
        }
            
        private string limpiaNoPermitidos(string valor)
        {
            valor = valor.Replace("\"", " ");
            valor = valor.Replace("'", "");
            valor = valor.Replace("|", " ");
            valor = valor.Replace('\r', ' ');
            valor = valor.Replace('\n', ' ');
            return valor;
        }

        /// <summary>
        /// Obtiene las respuestas de la orden 
        /// </summary>
        /// <param name="tipo"> Tipo de operación a realizar</param>
        /// <param name="idOrden">id orden a buscar</param>
        /// <param name="reporte">indica si es para reporte</param>
        /// <param name="idUsuarioPadre">id usuario supervisor</param>
        /// <param name="restriccion">Bandera que indica si se necesita aplicar restricción para la búsqueda de la respuesta</param>
        /// <param name="conexionBd">Base de datos a conectarse</param>
        /// <returns></returns>
        public DataSet ObtenerRespuestas(int tipo, string idOrden, int reporte, int idUsuarioPadre,bool restriccion, string conexionBd)
        {
            return new EntRespuestas().ObtenerRespuestas(tipo,idOrden, reporte, idUsuarioPadre, restriccion, conexionBd);
        }

        #region SMS

        /// <summary>
        /// Crea un diccionario personalizado a partir de otro diccionario raíz
        /// </summary>
        /// <param name="diccionario">Diccionario raíz del cual se van a sacar los campos</param>
        /// <param name="idOrden">Id de la orden a insertar en TraceLog</param>
        /// <returns>Diccionario con campos específicos</returns>
        private Dictionary<string, string> DiccionarioSMS(Dictionary<string, string> diccionario, int idOrden)
        {
            try
            {
                var camposSMSArr = CamposRespSMS.Split(',');
                var result = camposSMSArr.ToDictionary(s => s == CampoCelularSMSAct ? CampoCelularSMSAnt : s + "_SMS",s => diccionario[s]);
                return result;
            }
            catch (Exception)
            {
                Email.EnviarEmail(
                    "sistemasdesarrollo@publipayments.com", "Error al validar SMS",
                    "Error crear diccionario respuesta SMS en Orden " + idOrden);
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "DiccionarioSMS",
                    "Error crear diccionario respuesta SMS en Orden " + idOrden);
            }

            return null;
        }

        /// <summary>
        /// Crea un diccionario personalizado a partir de otro diccionario raíz
        /// </summary>
        /// <param name="diccionario">Diccionario raíz del cual se van a sacar los campos</param>
        /// <param name="idOrden">Id de la orden a insertar en TraceLog</param>
        /// <returns>Diccionario con campos específicos</returns>
        private Dictionary<string, string> DiccionarioNumIdentificacion(Dictionary<string, string> diccionario, int idOrden)
        {
            try
            {
                var camposIdentifArr = CamposRespIdentif.Split(',');
                var result = camposIdentifArr.ToDictionary(s => s == CampoCelularSMSAct ? CampoCelularSMSAnt : "NumIdentificacion", s => diccionario[s]);
                return result;
            }
            catch (Exception)
            {
                Email.EnviarEmail(
                    "sistemasdesarrollo@publipayments.com", "Error al validar SMS",
                    "Error crear diccionario respuesta SMS en Orden " + idOrden);
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "DiccionarioSMS",
                    "Error crear diccionario respuesta SMS en Orden " + idOrden);
            }

            return null;
        }
        

        /// <summary>
        /// Valida si el código que fue capturado por el gestor es valido 
        /// </summary>
        /// <param name="idOrden">orden a validar</param>
        /// <param name="codigo">Código recuperado</param>
        private int ProcesarCodigoSMS(int idOrden, string codigo)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Respuesta", " ProcesarCodigoSMS idorden:" + idOrden);
            var entSms = new EntSMS();
            var telefono = entSms.ObtenerTelefonoXOrden(idOrden);

            try
            {
                var resultado = entSms.ValidaAutorizacionSMS(telefono, codigo);
                if (resultado.Tables.Count > 0 && resultado.Tables[0].Rows.Count > 0)
                {
                    var result = Convert.ToInt32(resultado.Tables[0].Rows[0]["Resultado"]);
                    var texto = resultado.Tables[0].Rows[0]["Mensaje"];

                    switch (result)
                    {
                        case -1:
                            entSms.RestaurarRespuesta(idOrden, 0);
                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "ProcesarCodigoSMS",
                                string.Format(
                                    "Error al validar SMS, La clave de confirmación no es correcta Código: {0} - Teléfono: {1} - Orden: {2} - Mensaje: {3}",
                                    codigo, telefono, idOrden, texto));
                            break;
                        case 1:
                            break;
                        default:
                            entSms.RestaurarRespuesta(idOrden, 0);
                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "ProcesarCodigoSMS",
                                string.Format(
                                    "Error al validar SMS Código: {0} - Teléfono: {1} - Orden: {2} - Mensaje: {3}", codigo,
                                    telefono, idOrden, texto));
                            break;
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                string exceptionInner = ex.InnerException != null ? ex.InnerException.Message : "";
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Respuesta", "Error: GuardarRespuesta.ProcesarCodigoSMS_" + ex.Message + " inner " + exceptionInner);
            }
            Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "ProcesarCodigoSMS",
                string.Format("No se pudo validar el SMS - Teléfono: {0} - Orden: {1}", telefono,
                    idOrden));

            return -1;
        }

        #endregion

        #region Originacion

        private Dictionary<string, string> FormalizacionCamposDuplicadosCorrecciones(
            Dictionary<string, string> diccionario)
        {
            return diccionario.Where(dic => dic.Key.Contains("_2"))
                .ToDictionary(dic => dic.Key.Replace("_2", ""), dic => dic.Value);
        }

        private Dictionary<string, string> FormalizacionCamposDuplicados(Dictionary<string, string> diccionario)
        {
            var ignorarIncFormalizacion = IgnorarIncFormalizacion.Split(',');
            foreach (var s in ignorarIncFormalizacion)
            {
                diccionario.Remove(s);
            }
            return diccionario;
        }

        #endregion

        #region imagenesCW

        private Dictionary<string, string> redireccionImagenes(Dictionary<string, string> dicRespuesta, int idorden)
        {
            var dicTemp=new Dictionary<string, string>();
            foreach (var xx in dicRespuesta)
            {
                
                if (xx.Value.StartsWith("https"))
                {
                    dicTemp[xx.Key] = xx.Value.Replace("temp/", idorden.ToString(CultureInfo.InvariantCulture) + "/");
                }
                else
                {
                    dicTemp[xx.Key] = xx.Value;
                }

            }

            string directorioImagenes = ConfigurationManager.AppSettings["CWDirectorioImagenes"] ?? @"C:\Publipayments\ImagenesCW\";
            var path = directorioImagenes + idorden;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var imagenes = dicTemp.Where(dic => dic.Value.StartsWith("https"));
            foreach (var i in imagenes)
            {
                var xx=i.Value.Split('/');
                MoverArchivo(directorioImagenes + "temp" + "\\" + xx[xx.Length - 1], path+ "\\"  +xx[xx.Length - 1]);
            }
            return dicTemp;

        }

        private static void MoverArchivo(string origen, string destino)
        {
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Respuesta",
                    "Moviendo archivo " + origen + " a " + destino);
                if (File.Exists(destino))
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Respuesta",
                        "El archivo " + destino + " ya existe. Se va a reemplazar.");
                    File.Delete(destino);
                }
                File.Move(origen, destino);
            }
            catch (Exception errMover)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "No se pudo mover el archivo: " + origen + " - Error: " + errMover.Message);

                throw;
            }
        }

        #endregion

        /// <summary>
        /// Cambia el valor de la respuesta cuando este cumple con las condiciones para ser Recuperación de Vivienda
        /// </summary>
        /// <param name="creditoModel"></param>
        /// <param name="respuesta">elemento que se manda a evaluar</param>
        /// <param name="gpsDictionary">Diccionario de las coordenadas GPS que tenemos en la respuesta</param>
        /// <returns>Valor que se deberá de insertar en la respuesta</returns>
        private KeyValuePair<string, string> CambioValRecuperacion(Creditos creditoModel, KeyValuePair<string, string> respuesta, Dictionary<string, string> gpsDictionary)
        {
            try
            {
                if (respuesta.Value != null && gpsDictionary.Count > 0 && respuesta.Key.StartsWith("Dictamen"))
                {
                    var dicRecuperacion = VarVivRecuperacion.Split(',');
                    if (dicRecuperacion.Any(dr => respuesta.Value.ToUpper().Contains(dr.ToUpper())))
                    {
                        if (VivRecuperacion(creditoModel, gpsDictionary))
                            return new KeyValuePair<string, string>("DictamenRecViv", "Recuperación de Vivienda");
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Respuesta","CambioValRecuperacion_" + creditoModel.CV_CREDITO + " - ..." + ex.Message + (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                  return new KeyValuePair<string, string>(respuesta.Key,  respuesta.Value??""); 
            }

            return new KeyValuePair<string, string>(respuesta.Key, respuesta.Value ?? ""); 
        }

        /// <summary>
        /// valida si el crédito cumple con las condiciones para recuperar la vivienda
        /// </summary>
        /// <param name="creditoModel"></param>
        /// <param name="gpsDictionary"></param>
        /// <returns></returns>
        private bool VivRecuperacion(Creditos creditoModel, Dictionary<string, string> gpsDictionary)
        {
            try
            {
                var gestionesAnt = new List<string[]>
                {
                    creditoModel.TX_ULTIMA_GESTION_1MES.Split('/'),
                    creditoModel.TX_ULTIMA_GESTION_2MESES.Split('/'),
                    creditoModel.TX_ULTIMA_GESTION_3MESES.Split('/'),
                    creditoModel.TX_ULTIMA_GESTION_4MESES.Split('/')
                };


                foreach (var gAnt in gestionesAnt)
                {

                    var despachoActual = creditoModel.TX_NOMBRE_DESPACHO.ToUpper();
                    var despachoUltimo = gAnt.Length > 0 ? gAnt[0].ToUpper() : "";

                    var dicRecuperacion = VarVivRecuperacion.Split(',');

                    if (despachoActual.ToUpper() != despachoUltimo.ToUpper() && dicRecuperacion.Any(dr => gAnt[1].ToUpper().Contains(dr.ToUpper())))
                    {
                        if (!string.IsNullOrEmpty(gAnt[3]))
                        if (gpsDictionary.Select(gps => CalculoDistanciasGps(gps.Value, gAnt[3])).Any(distancia => distancia <=_mtsRecuperacion ))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Respuesta",
                    "VivRecuperacion_" + creditoModel.CV_CREDITO + " - ..." +
                    ex.Message + (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));

                return false;
            }
        }
        /// <summary>
        /// Se encarga de calcular la distancia que existe entre dos puntos
        /// </summary>
        /// <returns>cantidad de metros de diferencia</returns>
        private  double CalculoDistanciasGps(string origen, string destino)
        {
            try
            {
                if (string.IsNullOrEmpty(origen) || string.IsNullOrEmpty(destino))
                    return 100;
                var origenArr = origen.Split(' ');
                var destinoArr = destino.Split(' ');

                if (string.IsNullOrEmpty(origenArr[0]) || string.IsNullOrEmpty(origenArr[1]) ||
                    string.IsNullOrEmpty(destinoArr[0]) || string.IsNullOrEmpty(destinoArr[1]))
                    return 100;
                return CalculoDistanciasGps(Convert.ToDouble(origenArr[0], CultureInfo.InvariantCulture), Convert.ToDouble(origenArr[1], CultureInfo.InvariantCulture),
                                       Convert.ToDouble(destinoArr[0], CultureInfo.InvariantCulture), Convert.ToDouble(destinoArr[1], CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Respuesta", string.Format("CalculoDistanciasGps_ origen:{0} destino:{1}_ {2}", origen, destino, ex.Message));
                return 100;
            }
        }

        /// <summary>
        /// Se encarga de calcular la distancia que existe entre dos puntos
        /// </summary>
        /// <returns>cantidad de metros de diferencia</returns>
        private  double CalculoDistanciasGps(double latitudOrigen, double longitudOrigen, double latitudDestino, double longitudDestino)
        {
            try
            {
                var geo = new GeoCoordinate(latitudOrigen, longitudOrigen);
                return geo.GetDistanceTo(new GeoCoordinate(latitudDestino, longitudDestino));
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Respuesta", string.Format("CalculoDistanciasGps_ latitudOrigen:{0} longitudOrigen:{1} latitudDestino:{2} latitudDestino:{3}_ {4}", latitudOrigen, longitudOrigen, latitudDestino, longitudDestino, ex.Message));
                return 100;
            }

        }

        /// <summary>
        /// Realiza procesos necesarios para recibir la respuesta, en caso de que sea necesario
        /// </summary>
        /// <param name="externalId">identificador donde si no es un entero, seguro pertenece a una originacion </param>
        /// <param name="externalType"> nombre del formulario que se esta procesando</param>
        /// <param name="assignedTo">Usuario que esta mandando la información</param>
        /// <returns>id Orden perteneciente al registro del usuario</returns>
        public int PrepararOrden(string externalId, string externalType, string assignedTo=null)
        {
           int idorden = 0;
          
           try
           {
               idorden=Convert.ToInt32(externalId);
               return idorden;
           }
           catch (Exception)
           {

               if (string.IsNullOrEmpty(externalType))
               {
                   Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Respuesta", "PrepararOrden_ el campo externalType es null");
                   return idorden;
               }               
               if (externalType.ToUpper().Contains("REGISTROUSUARIO")) //el registro pertenece a un registro de un asesor
                   {
                       return new RegistroUsuario().RegistroUsuarioPPM(externalId, externalType, assignedTo);
                   }

                   return idorden;
           }

        }
    }
}

