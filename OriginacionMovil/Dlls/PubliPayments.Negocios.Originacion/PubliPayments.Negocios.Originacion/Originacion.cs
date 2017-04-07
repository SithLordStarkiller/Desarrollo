using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using PubliPayments.Entidades;
using PubliPayments.Entidades.Originacion;
using PubliPayments.Negocios.Originacion;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios.Originacion
{
    public class Originacion
    {
        private readonly Dictionary<string, string> _respuestas;
        private readonly Dictionary<string, string> _respuestas2;
        private int _idOrden=0;
        private string _ExternalType;
        private string _ExternalId;
        private BloqueoConcurrenciaModel _Bcmodel;
        private BloqueoConcurrencia _Bc;
        private string _fileXmlName;
        private string _AssignedTo;
        private bool _Productivo;

        public Originacion(Dictionary<string, string> respuestas, Dictionary<string, string> respuestas2, string externalType, BloqueoConcurrenciaModel bcmodel, BloqueoConcurrencia bc, string externalId, string fileXmlName, string assignedTo, bool productivo)
        {
            _respuestas = respuestas;
            _ExternalType = externalType;
            _Bcmodel = bcmodel;
            _Bc = bc;
            _ExternalId = externalId;
            _fileXmlName = fileXmlName;
            _AssignedTo = assignedTo;
            _Productivo = productivo;
            _respuestas2 = respuestas2;

            
        }

        public Originacion(Dictionary<string, string> respuestas, string externalType, string externalId, string fileXmlName,string assignedTo)
        {
            _respuestas = respuestas;
            _ExternalType = externalType;
            _ExternalId = externalId;
            _fileXmlName = fileXmlName;
            _AssignedTo = assignedTo;
        }

        public Originacion()
        {
            //throw new NotImplementedException();
        }

        public string GeneraOriginacion()
        {
            var orden = "";
           
            orden = _ExternalId;

            var validaciones = ValidacionesOCI.validarCampos(_respuestas);

            if (validaciones != "")
                return validaciones;

            _respuestas.Add("AssignedTo", _AssignedTo);

            var resultadoRegistro = RegistroCredito(orden);
            
            return resultadoRegistro;
            
        }

        public Stream CargarOriginacion()
        {
            string mensaje = "";
            string orden = "";
            int idOrden = -1;
            try
            {
                idOrden = Convert.ToInt32(_respuestas["ExternalId"]);
                orden = _respuestas["ExternalId"];
            }
            catch (Exception)
            {
                orden = _respuestas["ExternalId"];
            }

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "wsServices01800", "Recibiendo orden: " + orden);

            if (_ExternalType.ToUpper().TrimEnd().Contains("ORIGINAPP"))
            {
                //START Obtiene el OrdenId
                var nss = "";
                try
                {
                    nss = _respuestas["Nss"];
                }
                catch (Exception)
                {
                    nss = _respuestas2["Nss"];
                }

                if (nss == null)
                    throw new Exception("Cargar Originación: Número de seguro social no encontrado.");

                try
                {
                    var context = new SistemasCobranzaEntities();
                    idOrden = (from creditos in context.Creditos
                               join ordenes in context.Ordenes on creditos.CV_CREDITO equals ordenes.num_Cred
                               where creditos.CV_NSS.Equals(nss)
                               select ordenes.idOrden).FirstOrDefault();
                }
                catch (Exception e)
                {
                    throw new Exception("Cargar Originación: " + e.Message);
                }
                //END Obtiene el OrdenId
            }
            
            

            var resp = new Respuesta();
            mensaje = resp.GuardarRespuesta(idOrden, _respuestas, "wsFormiik", _AssignedTo, 1,
                _Productivo, "OriginacionMovil");

            // Todo revisar el false, beto lo tiene diferentes
            _Bcmodel.Resultado = mensaje == ""
                   ? "OK"
                   : mensaje;
            _Bc.ActualizarConcurrencia(_Bcmodel, 1);

            if (!string.IsNullOrEmpty(mensaje))
            {
                Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsServices01800", mensaje);
                return new MemoryStream(Encoding.UTF8.GetBytes(mensaje));
            }


            var ctxOri1 = new SistemasCobranzaEntities();
            var ordenCreada1 = ctxOri1.Ordenes.FirstOrDefault(x => x.idOrden == idOrden);

            if (ordenCreada1 == null)
            {
                mensaje = "Orden No encontrada FinOrig";

                _Bcmodel.Resultado = mensaje;
                _Bc.ActualizarConcurrencia(_Bcmodel, 1);
                Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                    "Error wsFormiik.SaveFull :aplicacion " + "OriginacionMovil",
                    "Id = " + _ExternalId + Environment.NewLine + "Archivo: " + _fileXmlName + Environment.NewLine +
                    "Error: " + mensaje + Environment.NewLine);
                Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsServices01800", "Id = " + _ExternalId + " - " + mensaje);
                return new MemoryStream(Encoding.UTF8.GetBytes(String.Empty));
            }

            RegistroCreditos.ReenviarOrdenOci(ordenCreada1, _respuestas.ContainsKey("Nss") ? _respuestas["Nss"] : _respuestas2["Nss"]);



            mensaje = "OK";
            Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
            return new MemoryStream(Encoding.UTF8.GetBytes(String.Empty));
        }

        public void RegistroCredito(string orden, ref int idOrden, ref bool resultadoRegistro)
        {
            var mensaje = "";
            //Si es originacion se genera el credito/orden y se continua con el flujo
            try
            {

                var numCred = RegistroCreditos.RegistraCredito(_respuestas);
                if (numCred["WSidMensaje"] == "0000")
                {
                    var registro = new EntCredito().InsertaCreditoOrden(numCred["Valor"],
                                                                        _respuestas["AssignedTo"], 485, 1, 64,
                                                                        "Formaliza"
                                                                        , (_ExternalType.ToUpper().TrimEnd().Contains("BIO") ? "B" : "O"), "", "",_respuestas["Nss"],_ExternalId);




                    if (!registro)
                    {
                        mensaje = "Originación - No se generó correctamente el crédito";
                        _Bcmodel.Resultado = mensaje;

                        _Bc.ActualizarConcurrencia(_Bcmodel, 1);

                        Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsServices01800", mensaje);
                        resultadoRegistro = false;
                    }

                    var numCreditoResult = numCred["Valor"];
                    var ctxOri = new SistemasCobranzaEntities();
                    var ordenCreada = ctxOri.Ordenes.FirstOrDefault(x => x.num_Cred == numCreditoResult);
                    if (ordenCreada != null)
                       idOrden = ordenCreada.idOrden;
                    else
                    {
                        throw new ApplicationException("No se pudo encontrar la orden " + orden);
                    }

                    _respuestas.Add("FolioCredito", numCred["FolioCredito"]);
                }
                else
                {
                    // actualizamos el resultado;

                    //_Bcmodel.Resultado = "Originación - No se registro el crédito";
                    _Bcmodel.Resultado = string.Format("Originación - No se registro el crédito - {0}, {1}, {2}", numCred["WSidMensaje"] ?? "WSidMensaje Nulo", numCred["Valor"] ?? "Valor Nulo", numCred["FolioCredito"] ?? "FolioCredito Nulo");
                    Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsServices01800", _Bcmodel.Resultado);

                    _Bc.ActualizarConcurrencia(_Bcmodel, 1);


                    mensaje = "Originación - No se registro el crédito - Error: " +
                          numCred["Valor"];
                    Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsServices01800", mensaje);
                    resultadoRegistro = false;
                }
            }
            catch (Exception errOrigina)
            {
                mensaje = "Originación - No se generó correctamente el crédito - Error: " +
                          errOrigina.Message;

                _Bcmodel.Resultado = mensaje;
                _Bc.ActualizarConcurrencia(_Bcmodel, 1);
                Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                    "Error wsFormiik.SaveFull :aplicacion " + "OriginacionMovil",
                    "Id = " + _ExternalId + Environment.NewLine + "Archivo: " + _fileXmlName + Environment.NewLine +
                    "Error: " + mensaje + Environment.NewLine + "Trace = " + errOrigina.StackTrace);

                Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsServices01800", mensaje);
                resultadoRegistro = false;
            }
        }

        public string RegistroCredito(string orden)
        {
            var mensaje = "";
            //Si es originacion se genera el credito/orden y se continua con el flujo
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "RegistroCredito", "Revisando credito existente NSS : " + _respuestas["Nss"]);
                var idOrden = new EntCredito().ObtenerCreditoPorNss(_respuestas["Nss"],orden);
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "RegistroCredito", "Credito existente NSS : " + _respuestas["Nss"] + " Res : " + (!String.IsNullOrEmpty(idOrden)).ToString());
                if (!String.IsNullOrEmpty(idOrden))
                {
                    return "Ya se ha registrado un credito para este NSS";
                }

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "RegistroCredito", "Revisando validaciones credito NSS : " + _respuestas["Nss"]);
                var bloqueo = new EntCredito().ValidarDatosCredito(_respuestas["numcue"], _respuestas["CorreoElectronico"], _respuestas["NumIdentificacionPas"] == "" ? _respuestas["NumIdentificacionIFE"] : _respuestas["NumIdentificacionPas"]);
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "RegistroCredito", "validaciones credito NSS : "+_respuestas["Nss"]+" Res : " + bloqueo);
                if (bloqueo!="OK")
                {
                    return bloqueo;
                }

                var numCred = RegistroCreditos.RegistraCredito(_respuestas);
                if (numCred["WSidMensaje"] == "0000")
                {
                    var registro = new EntCredito().InsertaCreditoOrden(numCred["Valor"],
                                                                        _respuestas["AssignedTo"], 485, 1, 64,
                                                                        "Formaliza"
                                                                        , (_ExternalType.ToUpper().TrimEnd().Contains("BIO") ? "B" : "O"), "", "",_respuestas["Nss"],orden);

                    if (!registro)
                    {
                        mensaje = "Originación - No se generó correctamente el crédito";
                        
                        Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsServices01800", mensaje);
                        return mensaje;
                    }

                    var numCreditoResult = numCred["Valor"];
                    var ctxOri = new SistemasCobranzaEntities();
                    var ordenCreada = ctxOri.Ordenes.FirstOrDefault(x => x.num_Cred == numCreditoResult);
                    if (ordenCreada == null)
                        throw new ApplicationException("No se pudo encontrar la orden " + orden);

                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "RegistroCredito", "IdOrden : " + ordenCreada.idOrden + " Ok ");

                    return "Registro OK";
                }
                else
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsServices01800", numCred["Valor"]);

                    mensaje = "Originación - No se registro el crédito - Error: " +
                          numCred["Valor"];
                    Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsServices01800", mensaje);
                    return numCred["Valor"];
                }
            }
            catch (Exception errOrigina)
            {
                mensaje = "Originación - No se generó correctamente el crédito - Error: " +
                          errOrigina.Message;

                Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                    "Error wsFormiik.SaveFull :aplicacion " + "OriginacionMovil",
                    "Id = " + _ExternalId + Environment.NewLine + "Archivo: " + _fileXmlName + Environment.NewLine +
                    "Error: " + mensaje + Environment.NewLine + "Trace = " + errOrigina.StackTrace);

                Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsServices01800", mensaje);
                return "Originación - No se generó correctamente el crédito";
            }
        }

        public DocsExpediente ObtenerDocumentosABorrar(Dictionary<string,bool> documentos)
        {
            try
            {
                var result = new DocsExpediente();

                //JObject jsonResponse = JObject.Parse(documentos);
                var docs = documentos.Where(documento => documento.Value).ToDictionary(documento => documento.Key, documento => documento.Value);
                //var jcredito=jsonResponse["credito"].ToString();
                //var jTelefonos = jsonResponse["documentos"].ToList();

                //foreach (var telefono in jTelefonos)
                //{
                //    var jp = (JProperty)telefono;
                //    var nombre = jp.Name;
                //    var va = jp.Value;

                //    docs.Add(nombre,Convert.ToBoolean(va));
                //}

                //result.credito = jcredito;
                result.documentos = docs;

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public string ReasignaionFlujosExcepcion(DocumentosOrigacion documentos)
        {
            try
            {
                var result = "OK";

                if (documentos == null)
                    throw new Exception("Modelo nulo");

                if (string.IsNullOrEmpty(documentos.Credito))
                    throw new Exception("Crédito no recibido");

                if (documentos.Documentos == null)
                    throw new Exception("Documentos no recibidos ");
                
                var fotosABorrar = ObtenerDocumentosABorrar(documentos.Documentos);

                if (fotosABorrar == null)
                    throw new Exception("Documentos no seleccionados");

                fotosABorrar.credito = documentos.Credito;

                var idOrden = new EntGestionadas().ObtenerOrdenCredito(fotosABorrar.credito);

                var fotos = new EntGestionadas().BorrarImagen(idOrden, ObtenerCadenaDocumentos(fotosABorrar.documentos));

                string directorioBitacoraImagenes = ConfigurationManager.AppSettings["CWDirectorioBitacoraImagenesOriginacion"];
                string directorioImagenes = ConfigurationManager.AppSettings["CWDirectorioImagenesOriginacion"];

                foreach (DataRow foto in fotos)
                {
                    try
                    {
                        var visita = foto["FaseProceso"].ToString();
                        var imagenFinal = foto["NombreCampo"].ToString();

                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, GetType().Name, "Imagen : " + imagenFinal);

                        var fecha = DateTime.Now.Year + DateTime.Now.Month.ToString(CultureInfo.InvariantCulture) + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute;

                        var fase = visita == "1" ? "Originacion" : (visita == "2" ? "Formalizacion" : "Preautorizacion");
                        var pathBitacora = directorioBitacoraImagenes + idOrden + @"\" + fase;
                        var path = directorioImagenes + idOrden + @"\" + fase;
                        var fullpathBitacora = pathBitacora + @"\" + fecha + imagenFinal + ".jpg";
                        var fullpath = path + @"\" + imagenFinal + ".jpg";

                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, GetType().Name, "Ruta Ori : " + fullpath + " Des : " + fullpathBitacora);

                        if (!Directory.Exists(pathBitacora))
                            Directory.CreateDirectory(pathBitacora);

                        if (File.Exists(fullpath))
                            File.Move(fullpath , fullpathBitacora);

                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, GetType().Name, "BorrarImagen Crédito : " + fotosABorrar.credito + " - OK : " + imagenFinal);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 0, GetType().Name, "BorrarImagen - Error:" + ex.Message + " Credito : " + fotosABorrar.credito);
                        return "Error: Ocurrió un error al intentar borrar el archivo. Intente nuevamente mas tarde. Si el problema continua intente recargar la página, disculpe las molestias que esto le pueda ocasionar.";
                    }
                }
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, GetType().Name, "Regreso Credito : " + fotosABorrar.credito + " Resultado : " + result);
                return result;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, GetType().Name, "ReasignaionFlujosExcepcion - Error:" + ex.Message);
                return "Error: " + ex.Message;
            }
        }

        //public void ReasignarOrden(string[][] ordenesList, string[] paginaActual)
        public string ReasignarOrden(string[][] ordenesList)
        {
            try
            {
                var guidTiempos = Tiempos.Iniciar();
                var ordenes = ordenesList.Select(x => long.Parse(x[0])).ToList();
                var aplicacion = ConfigurationManager.AppSettings["Aplicacion"];
                var idUsuario = 0;
                var idRol = 3;
                var entFormulario =
                    new EntFormulario().ObtenerListaFormularios("Formaliza").FirstOrDefault(x => x.Captura == 1);
                var respuesta = "";
                var totalCanceladas = 0;
                //string paginaOrigen = paginaActual[0];

                var datosApp = EntOriginacion.ObtenerDatosAplicacion();

                Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, GetType().Name, "ReasignarOrden num Ordenes : " + ordenes.Count);

                if (entFormulario != null)
                {
                    var orden = new Orden(idRol, Convert.ToBoolean(ConfigurationManager.AppSettings["Produccion"]),
                        datosApp.ClientId, datosApp.ProductId, entFormulario.Nombre, entFormulario.Version, null);

                    for (var i = 0; i < ordenes.Count; i += 100)
                    {
                        var restantes = ordenes.Count - i;
                        var valoresAProcesar = (restantes <= 100)
                            ? ordenes.GetRange(i, restantes)
                            : ordenes.GetRange(i, 100);

                        if (aplicacion.Contains("OriginacionMovil"))
                        {
                            totalCanceladas += orden.ReenviarIncompletasOriganacion(valoresAProcesar, idUsuario);
                        }
                        //else
                        //{
                        //    totalCanceladas += orden.CambiarEstatusOrdenes(valoresAProcesar, 3, true, false,0);
                        //}
                    }
                }
                if (respuesta == "")
                {
                    if (totalCanceladas == 0)
                    {
                        respuesta =
                            "Error: No se pudieron reenviar las órdenes seleccionadas, inténtelo nuevamente más tarde.|-1";
                    }
                    else if (totalCanceladas > 1)
                        respuesta = "Se reenviaron " + totalCanceladas + " órdenes.|-1";
                    else
                    {
                        respuesta = "Se reenvió " + totalCanceladas + " orden.|-1";
                    }

                }

                Tiempos.Terminar(guidTiempos);
                return respuesta;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, GetType().Name, "ReasignaionFlujosExcepcion - Error:" + ex.Message);
                return "Error: Error al reenviar la orden";
            }
        }

        private string ObtenerCadenaDocumentos(Dictionary<string, bool> docs)
        {
            var result = "";

            foreach (var dato in docs)
            {
                result += (",''"+dato.Key + "''");
            }

            return result.Substring(1);

        }

    }

    public class DocsExpediente
    {
        public string credito { get; set; }
        public Dictionary<string, bool> documentos { get; set; }
    }


}
