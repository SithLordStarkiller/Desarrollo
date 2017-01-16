using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PubliPayments.Entidades;
using PubliPayments.Negocios.WebServicePe;
using PubliPayments.Utiles;
using PubliPayments.Negocios.WebServiceFmk;
using System.Device.Location;

namespace PubliPayments.Negocios
{
    public class Orden
    {
        private readonly SistemasCobranzaEntities _ctx = new SistemasCobranzaEntities();
        private readonly int _idRol;

        private readonly bool _isProduction;
        // = ConfigurationManager.AppSettings["Produccion"] != null && ConfigurationManager.AppSettings["Produccion"].ToLower() == "true";

        private readonly string _idClient;
        // = IsProduction ? ConfigurationManager.AppSettings["IdClientProduccion"] : ConfigurationManager.AppSettings["IdClientTest"];

        private readonly string _idProduct;
        // = IsProduction ? ConfigurationManager.AppSettings["IdProductProduccion"] : ConfigurationManager.AppSettings["IdProductTest"];

        private readonly string _formulario;
        private readonly string _version;
        private static string _idAplicacion;
        private static readonly string VarPaedavi = ConfigurationManager.AppSettings["VarPAEDAVI"];

        public Orden(int idRol, bool isProduction, string idClient, string idProduct, string formulario, string version,
            string aplicacion)
        {
            _idRol = idRol;
            _isProduction = isProduction;
            _idClient = idClient;
            _idProduct = idProduct;
            _formulario = formulario;
            _version = version;
            _idAplicacion = aplicacion;
        }

        public Orden()
        {
        }

        public int Cancelar(List<int> ordenes, int idUsuarioLog)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden",
                "Cancelando " + ordenes.Count + " odenes");

            if (ordenes.Count < 0)
                return 0;

            var listaTexto = String.Join(",", ordenes.ToArray());

            Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden", listaTexto);

            int contadorOrdenesCanceladas;
            try
            {
                if (_idRol == 0 || _idRol == 2)
                {
                    //Cambio estatus de la orden a 2
                    var ent = new EntOrdenes();
                    //contadorOrdenesCanceladas = ent.ActualizarEstatusUsuarioOrdenes(listaTexto, 2, 0, true, false, idUsuarioLog);
                    contadorOrdenesCanceladas = ent.CancelarOrdenes(listaTexto, idUsuarioLog);
                    contadorOrdenesCanceladas = contadorOrdenesCanceladas < 0 ? 0 : contadorOrdenesCanceladas;
                }
                else
                {
                    //Cambio estatus de la orden a 12
                    //Cambio usuario a 0
                    //Mando a bitacora las ordenes
                    var ent = new EntOrdenes();
                    contadorOrdenesCanceladas = ent.ActualizarEstatusUsuarioOrdenes(listaTexto, 12, 0, true, false,
                        idUsuarioLog);
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error,
                    idUsuarioLog, "Orden",
                    "Error: al cancelar ordenes: " + ex.Message +
                    (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                contadorOrdenesCanceladas = 0;
            }

            return contadorOrdenesCanceladas;
        }

        /// <summary>
        /// Cancela las ordenes 
        /// </summary>
        /// <param name="ordenes">lista de ordenes </param>
        /// <param name="idUsuarioLog">usuario</param>
        /// <returns>número de cancelaciones realizadas</returns>
        public int CancelarCc(List<int> ordenes, int idUsuarioLog)
        {
            var result = new EntOrdenes().FiltrarEstatusUsuarioOrdenes(String.Join(",", ordenes));
            
            return string.IsNullOrEmpty(result)?0: Cancelar(ordenes, idUsuarioLog);
        }


        public int AsignarOrdenes(List<string> asignarCreditos, List<int> asignarOrdenes, bool afectaOrden,
            int idUsuario, int idUsuarioPadre, int idUsuarioAlta, int idDominio, int idUsuarioLog,
            int limiteOrdenesGestor)
        {

            Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden",
                "Asignando " + asignarOrdenes.Count + " ordenes - Asignando " + asignarCreditos.Count + " créditos");

            //var sb = new StringBuilder();

            var cnnSql = ConexionSql.Instance;
            int contadorOrdenesAsignadas = 0;
            //var asignadas = new List<int>();
            var asignadas = new ConcurrentQueue<int>();
            var limiteGestor = -1;
            var esGestor = false;

            //if (afectaOrden)
            //{
            //    //var limiteOrdenesGestor = ConfigurationManager.AppSettings["LimiteOrdenGestor"];
            //    limiteGestor = limiteOrdenesGestor;
            //    esGestor = idUsuario != 0 && cnnSql.ObtenerRol(idUsuario) == 4;
            //}

            //sb.Append("<Coleccion>");

            var exceptions = new ConcurrentQueue<Exception>();

            //for (int numeroOrden = 0; numeroOrden < asignarOrdenes.Count; numeroOrden++)
            Parallel.For(0, asignarOrdenes.Count, (numeroOrden,loopState) =>
            {
                var orden = asignarOrdenes.Count > 0 ? asignarOrdenes[numeroOrden] : 0;

                int idOrdenActual = 0;
                try
                {
                    DataSet dsOrdenXml = null;
                    int idOrden = 0;

                    //if (afectaOrden)
                    //{
                        //if (esGestor)
                        //{
                        //    int cuentaActual = cnnSql.ObtenerNumeroAsignaciones(idUsuario);
                        //    if (cuentaActual >= limiteGestor)
                        //    {
                        //        loopState.Stop();
                        //        return;
                        //    }
                        //}

                        dsOrdenXml = cnnSql.GeneraOrdenXml(asignarCreditos[numeroOrden], idUsuario, idUsuarioPadre,
                            idUsuarioAlta,
                            idDominio, orden);
                        idOrden = Convert.ToInt32(dsOrdenXml.Tables[0].Rows[0]["idOrden"]);
                        asignadas.Enqueue(idOrden);
                    //}
                    //else
                    //{
                    //    var context = new SistemasCobranzaEntities();

                    //    int orden1 = orden;
                    //    var ordenDatos = (from o in context.Ordenes
                    //                      where o.idOrden == orden1
                    //                      select new { o.idOrden, o.idUsuario, o.num_Cred }).FirstOrDefault();
                    //    if (ordenDatos != null)
                    //    {
                    //        dsOrdenXml = ObtieneOrdenXml(1, ordenDatos.num_Cred, ordenDatos.idOrden);

                    //        idOrden = ordenDatos.idOrden;
                    //    }

                    //    esGestor = true; //Si no afecta la orden siempre tiene que afectar el WS
                    //}
                    idOrdenActual = idOrden;

                    //if (esGestor)
                    //{
                    //    sb.Append(GeneraNewOrderXml(dsOrdenXml, idOrden, idUsuarioAlta));

                    //}

                    Interlocked.Increment(ref contadorOrdenesAsignadas);
                    //contadorOrdenesAsignadas++;


                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioAlta, "Orden",
                        "Error: al asignar orden al usuario: " + idUsuario + " - Orden: " + idOrdenActual + " - " +
                        ex.Message + (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));

                    exceptions.Enqueue(ex);

                    loopState.Stop();
                    return;
                    
                    //return 0;
                }
            }
                );

            //Si ocurrió un error regresa 0
            if (exceptions.Count > 0)
            {
                if (afectaOrden)
                    DesasginarOrdenes(asignadas, idUsuarioAlta);
                return 0;
            }
                
            //sb.Append("</Coleccion>");

            //if (!esGestor && (_idRol == 0 || _idRol == 2))
            //{
                Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuarioAlta, "Orden",
                    contadorOrdenesAsignadas + " Ordenes generadas - UsuarioPadre " + idUsuarioPadre);
            //}
            //else
            //{
            //    try
            //    {
            //        var bkclient = new BackEndClient("BasicHttpBinding_IBackEnd");
            //        Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioAlta, "Orden",
            //            "Enviando XML: " + sb);
            //        var resultado = bkclient.AddWorkOrdersXMLId(_idClient, _idProduct, sb.ToString());

            //        Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioAlta, "Orden",
            //            "Resultado de envío id:" + resultado);

            //        if (afectaOrden)
            //        {
            //            foreach (var id in asignadas)
            //            {
            //                int id1 = id;
            //                var orden = _ctx.Ordenes.FirstOrDefault(x => x.idOrden.Equals(id1));
            //                if (orden != null) orden.FechaEnvio = DateTime.Now;
            //            } 
            //            _ctx.SaveChanges();
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        string ordenes = string.Join(",", asignarOrdenes.ToArray());
            //        Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioAlta, "Orden",
            //            "Error: al enviar al WS, idUsuario: " + idUsuario + " - Ordenes " + ordenes + " - " + ex.Message +
            //            (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));

            //        if (afectaOrden)
            //            DesasginarOrdenes(asignadas, idUsuarioAlta);

            //        return 0;
            //    }
            //}

            return contadorOrdenesAsignadas;
        }

        private string GeneraNewOrderXml(DataSet dsOrdenXml, int idOrden, int idUsuarioAlta)
        {
            var sb = new StringBuilder();

            if (dsOrdenXml != null && dsOrdenXml.Tables.Count > 0)
            {
                DataTable dsCamposXml = dsOrdenXml.Tables[1];
                bool enviarMovil = false;
                if (dsOrdenXml.Tables[0].Rows.Count > 0 && dsCamposXml.Rows.Count > 0)
                {
                    try
                    {
                        enviarMovil = Convert.ToBoolean(dsOrdenXml.Tables[0].Rows[0]["EnviarMovil"]);
                    }
                    catch (Exception)
                    {

                        enviarMovil = true;
                    }

                    if (!enviarMovil)
                    {
                        return "";
                    }


                    sb.Append("<NewOrder id=\"");
                    sb.Append(idOrden);
                    sb.Append(String.Format(("\" type=\"{0}\" version=\"{1}\" userName=\""),
                        dsOrdenXml.Tables[0].Rows[0]["tipoFormulario"], dsOrdenXml.Tables[0].Rows[0]["Version"]));
                    if (dsOrdenXml.Tables[0].Rows[0]["Usuario"].ToString().Trim().ToLower() != "no asignado")
                        sb.Append(dsOrdenXml.Tables[0].Rows[0]["Usuario"]);
                    else
                    {
                        throw new ApplicationException(
                            "No se encontró el usuario no se puede enviar al WS, posible cancelación antes de envío");
                    }
                    sb.Append("\" priority=\"1\" expirationDate=\"");
                    sb.Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                    sb.Append("\" cancellationDate=\"");
                    sb.Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                    sb.Append("\">");



                    sb.Append("<Parametros>");
                    for (int i = 0; i < dsCamposXml.Rows.Count; i++)
                    {
                        sb.Append("<parametro llave=\"");
                        sb.Append(dsCamposXml.Rows[i]["Nombre"]);
                        sb.Append("\" valor=\"");
                        string valorCampo = dsCamposXml.Rows[i]["Valor"].ToString();

                        valorCampo = ProcesarValorXml(valorCampo, dsOrdenXml, idUsuarioAlta);

                        sb.Append(valorCampo);

                        sb.Append("\" />");
                    }

                    if (dsOrdenXml.Tables[0].Rows[0]["CV_RUTA"].ToString().ToUpper().Contains("CSD") || dsOrdenXml.Tables[0].Rows[0]["CV_RUTA"].ToString().ToUpper().Contains("RDST"))
                    {
                        var parametros = AgregarControlesXml(dsOrdenXml);
                        foreach (var parametro in parametros)
                        {
                            sb.Append("<parametro llave=\"");
                            sb.Append(parametro.Key);
                            sb.Append("\" valor=\"");
                            sb.Append(parametro.Value);
                            sb.Append("\"/>");
                        }
                    }
                    sb.Append("</Parametros></NewOrder>");
                }
            }

            return sb.ToString();
        }

        private void DesasginarOrdenes(IEnumerable<int> asignadas, int idUsuarioAlta)
        {
            var enumerable = asignadas as IList<int> ?? asignadas.ToList();
            Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioAlta, "Orden",
                "DesasginarOrdenes " + enumerable.Count + " ordenes");

            foreach (var id in enumerable)
            {
                int id1 = id;
                var orden = _ctx.Ordenes.FirstOrDefault(x => x.idOrden.Equals(id1));
                if (orden != null)
                {
                    orden.idUsuario = 0;
                    orden.FechaEnvio = null;
                }
                else
                    Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioAlta, "Orden",
                        "No se encontró la orden a des-asignar: " + id);
            }

            try
            {
                _ctx.SaveChanges();
            }
            catch (Exception exDesAsignar)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioAlta, "Orden",
                    "Error: al des-asignar ordenes: " + exDesAsignar.Message +
                    (exDesAsignar.InnerException != null ? " - Inner: " + exDesAsignar.InnerException.Message : ""));
            }
        }

        public static Dictionary<string, string> AgregarControlesXml(DataSet dsOrdenXml)
        {
            var solucion = dsOrdenXml.Tables[0].Rows[0]["TX_SOLUCIONES"].ToString();
            bool mostrarMb = (dsOrdenXml.Tables[0].Rows[0]["TX_SCRIPT"].ToString() != "");
            int idVisita = Convert.ToInt32(dsOrdenXml.Tables[0].Rows[0]["idVisita"]);
            string IN_OPCION_JUDICIAL = Convert.ToString(dsOrdenXml.Tables[0].Rows[0]["IN_OPCION_JUDICIAL"]);
            
            string tipo = "";
            var celularSMSRecibido = "";
            var dicGestAnt = "";
            var solCel = "No";

            try
            {
                tipo = dsOrdenXml.Tables[0].Rows[0]["Tipo"].ToString().ToUpper();
                celularSMSRecibido = dsOrdenXml.Tables[0].Rows[0]["CelularSMS_Recibido"].ToString().ToUpper();
                dicGestAnt = dsOrdenXml.Tables[0].Rows[0]["DicGest_Ant"].ToString().ToUpper();
                solCel = dsOrdenXml.Tables[0].Rows[0]["IN_SOLICITAR_CEL"].ToString().ToUpper() == "1" ? "Si" : "No";

            }
            catch (Exception e)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Orden",
                    "AgregarControlesXml " + e.Message);
            }


            var dic = new Dictionary<string, string> { { "DicPagoLiquid", "Si" } };

            if (solucion.Contains("IN_FPP"))
                dic.Add("DicMantenimientoFPP_PPAR", "2");
            else if (solucion.Contains("IN_PPAR"))
                dic.Add("DicMantenimientoFPP_PPAR", "3");
            else
                dic.Add("DicMantenimientoFPP_PPAR", "1");

            dic.Add("DicAplicaFPP", solucion.Contains("FPP")
                ? "Si"
                : "No");

            dic.Add("DicCalificaBCN", solucion.Contains("BCN")
                ? "Si"
                : "No");

            dic.Add("DicAplicaConvenio", solucion.Contains("TPP")
                ? "Si"
                : "No");

            dic.Add("DicCalificaDCP", solucion.Contains("DCP")
                ? ((idVisita == 1 && "0,1".Contains(IN_OPCION_JUDICIAL))?"No":"Si")
                : "No");

            
            dic.Add("DicCalificaPR5050", solucion.Contains("5050")
                ? "Si"
                : "No");

            dic.Add("DicCalificaProrroga", solucion.Contains("CON")
                ? "Si"
                : "No");

            dic.Add("DicAplicaSTM", solucion.Contains("STM")
                ? "Si"
                : "No");

            dic.Add("DicAplicaVSMP", solucion.Contains("VSMP")
                ? "Si"
                : "No");
            
            dic.Add("MostrarMensaje", mostrarMb
                ? "Si"
                : "No");

            if (tipo.Contains("S"))
            {
                dic.Add("DicAplicaSMS", "Si");
            }
            else
            {
                dic.Add("DicAplicaSMS", "No");
                dic.Add("RecMenSMS", "No");
                dic.Add("CorroboroCelularSMS", "Si");
            }
            dic.Add("CelularSMS_Recibido", celularSMSRecibido);
            dic.Add("DicGest_Ant", dicGestAnt);
            dic.Add("GestDesAnt", "No");
            dic.Add("IN_SOLICITAR_CEL", solCel);

            return dic;
        }

        public ResultadoLista EnviarOrdenesWs(List<OrdenModel> asignarOrdenes, bool esReasignacion, int idUsuarioLog)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden",
                "Enviando " + (esReasignacion ? " resignación de " : "") + asignarOrdenes.Count +
                " órdenes al WebService");

            var listaOrdenes = asignarOrdenes.Select(x => x.IdOrden).ToArray();

            var listaTexto = String.Join(",", listaOrdenes);
            while (listaTexto != "")
            {
                if (listaTexto.Length > 500)
                {
                    var lista = listaTexto.Substring(0, 500);
                    Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden",
                        lista);
                    listaTexto = listaTexto.Substring(500);
                }
                else
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden",
                        listaTexto);
                    listaTexto = "";
                }
            }

            var sb = new StringBuilder();
            var cnnSql = ConexionSql.Instance;

            var lockObject = new object();

            var resultado = new ResultadoLista();

            lock (lockObject)
            {
                sb.Append("<Coleccion>");
            }

            var stack = new ConcurrentStack<int>();
            Parallel.ForEach(asignarOrdenes, orden =>
            {
                try
                {
                    DataSet dsOrdenXml = ObtieneOrdenXml(1, orden.NumCred, orden.IdOrden);
                    string ordenXml = GeneraNewOrderXml(dsOrdenXml, orden.IdOrden, idUsuarioLog);
                    lock (lockObject)
                    {
                        sb.Append(ordenXml);
                    }
                }
                catch (Exception ex)
                {
                    stack.Push(orden.IdOrden);
                    Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "Orden",
                        "Error: al generar el XML de la orden: " + orden.IdOrden + " - ..." + ex.Message +
                        (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                }
            });

            lock (lockObject)
            {
                sb.Append("</Coleccion>");
            }

            resultado.Lista = stack.ToList();

            try
            {

                Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden",
                    "Enviando XML");
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") +
                                (esReasignacion ? "re-asignación" : "asignación") + " -Orden-Enviado XML: " + sb);
                if (sb.ToString().IndexOf("NewOrder", System.StringComparison.Ordinal)<=0)
                {
                    resultado.Resultado = "Sin asignar a movil";
                }
                else
                    if (_idAplicacion.ToUpper().Contains("SIRA"))
                {
                    var ordenesArr = new ParseXmlAsignacion().Sira(sb);
                    if (ordenesArr != null)
                    {
                        var servicePe = new Service1SoapClient();
                        var resp = servicePe.SolicitudSira(_idClient, _idProduct, ordenesArr);

                        if (resp.ordenesError.Length > 0)
                        {
                            resultado.Resultado = "Error:";
                            foreach (OrdenError t in resp.ordenesError)
                            {
                                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "Orden",
                                    "asignación idOrden:" + t.IdOrden + ", Mensaje:" + t.Mensaje);
                                resultado.Resultado += "," + t.IdOrden;
                            }
                        }
                        else
                        {
                            resultado.Resultado = resp.Mensaje.ToLower();
                        }
                    }
                }
                else
                {
                    var bkclient = new BackEndClient("BasicHttpBinding_IBackEnd");
                    if (esReasignacion)
                    {
                        bkclient.ReassignWorkOrdersXMLIdSync(_idClient, _idProduct, sb.ToString());
                        resultado.Resultado = "ReasignacionOK";
                    }
                    else
                    {
                        resultado.Resultado = bkclient.AddWorkOrdersXMLId(_idClient, _idProduct, sb.ToString());
                    }
                }

                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") +
                                (esReasignacion ? "re-asignación" : "asignación") + " -Orden-Resultado: " +
                                resultado.Resultado);
                Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden",
                    "Resultado de la " + (esReasignacion ? "re-asignación" : "asignación") + " id:" + resultado.Resultado);
            }
            catch (Exception ex)
            {
                string ordenes = String.Join(",", listaOrdenes);
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "Orden",
                    "Error: al enviar XML de " + (esReasignacion ? "re-asignación" : "asignación") + " al WS, Ordenes: " +
                    ordenes + " - ...");
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "Orden",
                    "Error: al enviar al WS, Error: " + ex.Message +
                    (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                resultado.Resultado = "Error: al enviar al WS";
            }

            return resultado;
        }

        public string EnviarCancelacionWs(List<OrdenModel> ordenes, int idUsuarioLog)
        {
            string resultado = "";
            Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden",
                "EnviarCancelacionWs " + ordenes.Count);

            var listaTexto = String.Join(",", ordenes.Select(x => x.IdOrden).ToArray());
            while (listaTexto != "")
            {
                if (listaTexto.Length > 500)
                {
                    var lista = listaTexto.Substring(0, 500);
                    Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden",
                        lista);
                    listaTexto = listaTexto.Substring(500);
                }
                else
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden",
                        listaTexto);
                    listaTexto = "";
                }
            }

            try
            {

                if (ordenes.Count > 0)
                {
                    var lockObject = new object();

                    var sb = new StringBuilder();


                    lock (lockObject)
                    {
                        sb.Append("<Coleccion>");
                    }

                    int contadorCanceladas = 0;
                    Parallel.ForEach(ordenes, orden =>
                    {
                        lock (lockObject)
                        {
                            if (orden.EnviarMovil)
                            {
                                sb.Append(string.Format("<CancelOrder id=\"{0}\" username=\"{1}\"/>", orden.IdOrden,
                                orden.UsuarioAnterior));
                            }
                            
                        }
                        Interlocked.Increment(ref contadorCanceladas);
                    });
                    lock (lockObject)
                    {
                        sb.Append("</Coleccion>");
                    }

                    if (contadorCanceladas > 0)
                    {
                        try
                        {
                            var bkclient = new BackEndClient("BasicHttpBinding_IBackEnd");
                            Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden",
                                "Enviando XML Cancelación");
                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " -Cancelar-Enviado XML: " + sb);

                            if (sb.ToString().IndexOf("CancelOrder", StringComparison.Ordinal) <= 0)
                            {
                                resultado = "Sin asignar a movil";
                            }
                            else if (_idAplicacion.ToUpper().Contains("SIRA"))
                            {

                                var servicePe = new Service1SoapClient();
                                int[] OrdenCancelar = ordenes.Select(x => x.IdOrden).ToArray();
                                var resp = servicePe.cancelaSolicitud(_idClient, _idProduct, OrdenCancelar);
                                if (resp.ordenesError.Length > 0)
                                {
                                    foreach (OrdenError t in resp.ordenesError)
                                    {
                                        Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "Orden",
                                            "Error: al enviar cancelación al WS, Ordenes:" + t.IdOrden + ", Mensaje:" +
                                            t.Mensaje);
                                        resultado += "," + t.IdOrden;
                                        return "Error:" + resultado;
                                    }
                                }
                                else
                                {
                                    resultado = resp.Mensaje.ToLower();
                                }
                            }
                            else
                            {
                                resultado = bkclient.CancelWorkOrdersXMLId(_idClient, _idProduct, sb.ToString());
                            }

                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " -Cancelar-Resultado: " +
                                            resultado);
                            Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden",
                                "Resultado de envío de cancelación - id:" + resultado);
                        }
                        catch (Exception exWs)
                        {
                            string ordenesCancelar = String.Join(",", ordenes.Select(x => x.IdOrden).ToArray());
                            Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "Orden",
                                "Error: al enviar cancelación al WS, Ordenes: " + ordenesCancelar + " - " + exWs.Message +
                                (exWs.InnerException != null ? " - Inner: " + exWs.InnerException.Message : ""));
                            return "Error: al enviar cancelación al WS";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error,
                    idUsuarioLog, "Orden",
                    "Error: en EnviarCancelacionWs: " + ex.Message +
                    (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));

                resultado = "Error: en EnviarCancelacionWs";
            }
            return resultado;
        }

        public static string ProcesarValorXml(string valorCampo, DataSet dsOrdenXml, int idUsuarioAlta)
        {
            if (valorCampo.StartsWith("[Campo]"))
            {
                valorCampo = valorCampo.Replace("[Campo]", "");
                if (dsOrdenXml.Tables[0].Columns.Contains(valorCampo))
                {
                    try
                    {
                        if (valorCampo.StartsWith("CV_CANAL"))
                        {
                            valorCampo = dsOrdenXml.Tables[0].Rows[0][valorCampo].ToString() == "T"
                                ? "TELEFONICA"
                                : "DOMICILIARIA";
                        }
                        else if (valorCampo.StartsWith("FH_"))
                            valorCampo =
                                Convert.ToDateTime(dsOrdenXml.Tables[0].Rows[0][valorCampo])
                                    .ToString("dd-MM-yyyy");
                        else if (valorCampo.StartsWith("IM_"))
                        {
                            valorCampo =
                                Convert.ToDouble(dsOrdenXml.Tables[0].Rows[0][valorCampo], CultureInfo.InvariantCulture)
                                    .ToString("###########.00", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            valorCampo = dsOrdenXml.Tables[0].Rows[0][valorCampo].ToString();
                        }
                    }
                    catch (Exception)
                    {
                        valorCampo = dsOrdenXml.Tables[0].Rows[0][valorCampo].ToString();
                    }
                }
                else
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioAlta, "Orden",
                        "Error: Campo no encontrado: " + valorCampo);
                }
            }
            if (valorCampo.Contains(Convert.ToChar(26)))
                valorCampo = valorCampo.Replace(Convert.ToChar(26), ' ');
            if (valorCampo.Contains(Convert.ToChar(29)))
                valorCampo = valorCampo.Replace(Convert.ToChar(29), ' ');
            if (_idAplicacion != null)
            {
                if (!_idAplicacion.Contains("OriginacionMovil"))
                {
                    if (valorCampo.Contains("&"))
                        valorCampo = valorCampo.Replace("&", "&amp;");
                }

            }
            var valor = valorCampo.Trim();
            return valor;
        }

        /// <summary>
        /// Cambia el estatus de las órdenes sin modificar las respuestas
        /// </summary>
        /// <param name="ordenes">Listado de las órdenes separadas por coma</param>
        /// <param name="estatus">estatus al que se van a cambiar las órdenes</param>
        /// <param name="actualizarFecha">Indica si se va actualizar la fecha de las órdenes que se cambien</param>
        /// <param name="esReverso">Si es true, dispara actualizaciones de reversos</param>
        /// <param name="idUsuarioLog">id del usuario al que se le va a generar los registros de log</param>
        /// <returns>Regresa la cantidad de registros que se modificaron</returns>
        public int CambiarEstatusOrdenes(string ordenes, int estatus, bool actualizarFecha, bool esReverso, int idUsuarioLog)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden",
                "CambiarEstatusOrdenes - Estatus: " + estatus + " - Ordenes " + ordenes);
            var ent = new EntOrdenes();
            int contadorOrdenes = 0;
            try
            {
                contadorOrdenes = ent.ActualizaEstatusOrdenes(ordenes, estatus, actualizarFecha, esReverso);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "Orden",
                    "CambiarEstatusOrdenes - Error: " + ex.Message);
            }
            Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden",
                "CambiarEstatusOrdenes - Ordenes cambiadas " + contadorOrdenes);
            return contadorOrdenes;
        }

        /// <summary>
        /// Cambia el estatus de las órdenes sin modificar las respuestas
        /// </summary>
        /// <param name="ordenes">Listado de las órdenes</param>
        /// <param name="estatus">estatus al que se van a cambiar las órdenes</param>
        /// <param name="actualizarFecha">Indica si se va actualizar la fecha de las órdenes que se cambien</param>
        /// <param name="esReverso">Si es true, dispara actualizaciones de reversos</param>
        /// <param name="idUsuarioLog">id del usuario al que se le va a generar los registros de log</param>
        /// <returns>Regresa la cantidad de registros que se modificaron</returns>
        public int CambiarEstatusOrdenes(List<int> ordenes, int estatus, bool actualizarFecha, bool esReverso, int idUsuarioLog)
        {
            var listaTexto = String.Join(",", ordenes.ToArray());
            return CambiarEstatusOrdenes(listaTexto, estatus, actualizarFecha, esReverso, idUsuarioLog);
        }

        /// <summary>
        /// Cambia el estatus de las órdenes sin modificar las respuestas
        /// </summary>
        /// <param name="ordenes">Listado de las órdenes separadas por coma</param>
        /// <param name="idUsuarioLog">id del usuario al que se le va a generar los registros de log</param>
        /// <returns>Regresa la cantidad de registros que se modificaron</returns>
        public int ReenviarIncompletas(string ordenes, int idUsuarioLog)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden",
                "ReenviarIncompletas - Ordenes " + ordenes);
            var ent = new EntOrdenes();
            int contadorOrdenes = 0;
            try
            {
                contadorOrdenes = ent.ReenviaIncompletosOriginacion(ordenes);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "Orden",
                    "ReenviarIncompletas - Error: " + ex.Message);
            }
            Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioLog, "Orden",
                "ReenviarIncompletas - Ordenes cambiadas " + contadorOrdenes);
            return contadorOrdenes;
        }

        /// <summary>
        /// Cambia el estatus de las órdenes sin modificar las respuestas
        /// </summary>
        /// <param name="ordenes">Listado de las órdenes</param>
        /// <param name="idUsuarioLog">id del usuario al que se le va a generar los registros de log</param>
        /// <returns>Regresa la cantidad de registros que se modificaron</returns>
        public int ReenviarIncompletas(List<int> ordenes, int idUsuarioLog)
        {
            var listaTexto = String.Join(",", ordenes.ToArray());
            return ReenviarIncompletas(listaTexto, idUsuarioLog);
        }

        /// <summary>
        /// Método que recupera las ordenes con estatus 4 y Tipo null o vacío, para validar con el WS si ya están dispersadas.
        /// </summary>
        /// <param name="estatus">Estatus a consultar (4)</param>
        /// <param name="idUsuarioLog">Identificador del usuario para el log</param>
        /// <returns>Listado de las ordenes listas para validar con el WS</returns>
        public List<ModeloOrdesConsultaEstatus> ObtenerOrdenesPorEstatusTipo(int estatus, int idUsuarioLog)
        {

            EntOrdenes recuperaOrdenes = new EntOrdenes();

            return recuperaOrdenes.ObtenerOrdenesPorEstatusTipo(estatus, idUsuarioLog);

        }

        /// <summary>
        /// Método que permite actualziar el campo Tipo de una orden con estatus 4
        /// </summary>
        /// <param name="idOrden">Identificador de la orden</param>
        /// <param name="tipo">Identificador para la marcar que la orden fue dispersada</param>
        /// <param name="estatus">Estatus el cual se realizara busqueda</param>
        /// <param name="idUsuarioLog">Isuario para el LOG</param>
        /// <returns>1 = se actulizo de manera correcta, 0 = no se actulizó</returns>
        public int ActualizarTipoOrden(int idOrden, string tipo, int estatus, int idUsuarioLog)
        {
            EntOrdenes actTipoOrden = new EntOrdenes();
            int retorno = 0;
            retorno = actTipoOrden.ActualizarTipoOrden(idOrden, tipo, estatus, idUsuarioLog);
            return retorno;
        }

        /// <summary>
        /// Método que permite recuperar el correo de una orden
        /// </summary>
        /// <param name="idOrden">Identificador de la orden</param>
        /// <param name="idUsuarioLog">Usuario para le LOG</param>
        /// <returns>Objeto con la infformación del correo y el id de la orden</returns>
        public ModeloOrdesConsultaEstatus ObtenerOrdenCorreo(int idOrden, int idUsuarioLog)
        {
            EntOrdenes actTipoOrden = new EntOrdenes();

            ModeloOrdesConsultaEstatus retorno = actTipoOrden.ObtenerOrdenCorreo(idOrden, idUsuarioLog);

            return retorno;
        }

        /// <summary>
        /// en caso de que no se tenga una orden asociada mandar -1 
        /// </summary>
        /// <param name="pool">NA</param>
        /// <param name="credito">crédito</param>
        /// <param name="orden">orden asociada</param>
        /// <returns>dataset necesario para generar el XML para gestionar</returns>
        /// JARO
        public DataSet ObtieneOrdenXml(int pool, String credito, int orden)
        {
            var cnnSql = ConexionSql.Instance;
            return orden < 0 ? cnnSql.ObtieneOrdenXCreditoXml(credito) : cnnSql.ObtieneOrdenXml(1, credito, orden);
        }


        public OrdenModel ObtenerOrdenxCredito(string credito)
        {
            return new EntOrdenes().ObtenerOrdenxCredito(credito);
        }

        /// <summary>
        /// Inserta registro en tabla BitacoraEstadosOrdenes
        /// </summary>
        /// <param name="textoOrdenes">formato de proceso "15:Visita:2015-11-14 12:13:34.200" para diferenciar ordenes separados por "," </param>
        /// <returns>string vacío si no hay algún error</returns>
        /// JARO
        public string InsertaBitacoraEstadosOrdenes(string textoOrdenes)
        {
            string result = "";
            try
            {
                result = new EntOrdenes().InsertaBitacoraEstadosOrdenes(textoOrdenes);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "Orden", "InsertaBitacoraEstadosOrdenes:" + ex.Message);
                result = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Guarda temporalmente en una tabla lo relacionado a la gestión realizada por CC para que posterior mente sea procesada
        /// </summary>
        /// <param name="idOrden">Orden a procesar</param>
        /// <param name="idUsuario">id usuario que esta realizando la gestión</param>
        /// <param name="dicRespuesta"> Diccionario de las respuestas que obtiene de CW</param>
        /// <returns></returns>
        public string InsertarRespuestasPendientes(int idOrden, int idUsuario, StringBuilder dicRespuesta)
        {
            string resultado = "";
            try
            {
                resultado += new EntOrdenes().InsertarRespuestasPendientes(idOrden, idUsuario, dicRespuesta);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Orden-InsertarRespuestasPendientes", resultado);
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene una lista de idOrden las cuales están preparadas para poder procesar
        /// </summary>
        /// <returns>string idOrden,idorden </returns>
        public static string ObtenerRespuestasPendientes()
        {
            return new EntOrdenes().ObtenerRespuestasPendientes();
        }

        /// <summary>
        /// se obtiene com resultado el diccionario construido por las respuestas almacenada
        /// </summary>
        /// <param name="idOrden">idOrden a procesar</param>
        /// <param name="idusuario">parámetro de salida del id usuario que realizo la gestión</param>
        /// <returns>diccionario de respuestas y idUsuario</returns>
        public static Dictionary<string, string> ObtenerDatosRespuestasPendientes(int idOrden,ref string idusuario)
        {
            var result = new Dictionary<string, string>();
            try
            {
                var ent = new EntOrdenes();
                var ds = ent.ObtenerDatosRespuestasPendientes(idOrden);
                idusuario=null ;
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result.Add(Convert.ToString(d["NombreCampo"]), Convert.ToString(d["valor"]));
                    idusuario = idusuario??Convert.ToString(d["idUsuario"]);
                }

            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "Orden", "ObtenerDatosRespuestasPendientes:" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Borra de tabla las respuestas que estaban pendientes por procesar
        /// </summary>
        /// <param name="idOrden">idOrden a procesar</param>
        /// <returns>true si se ha realizado el borrado</returns>
        public static bool BorrarRespuestasPendientes(int idOrden)
        {
            return new EntOrdenes().BorrarRespuestasPendientes(idOrden);
        }

        public int ActualizarEstatusUsuarioOrdenes(string ordenes, int estatus, int actualizaUsuario,bool actualizaFecha, bool actualizaSiEstatusIgual, int idUsuarioLog)
        {
            return new EntOrdenes().ActualizarEstatusUsuarioOrdenes(ordenes, estatus, actualizaUsuario, actualizaFecha, actualizaSiEstatusIgual, idUsuarioLog);                                                    
        }
    }
}