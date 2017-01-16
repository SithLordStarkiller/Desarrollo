using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ionic.Zip;
using PubliPayments.Entidades;
using Publipayments.Negocios.MYO;
using PubliPayments.Utiles;
using Renci.SshNet;
using PubliPayments.Negocios;
using PubliPayments.Negocios.Originacion;



namespace PubliPayments
{
    internal class Program
    {
        private static bool ejecutandose = true;
        private static object Lock = new object();
        private static int _procesando;
        private static string[] _archivoProcesar;
        //private const string IdClient = "DBD4DCBD-27F8-42D6-91CF-0902F5B96421";
        //private const string IdProduct = "26CB50A8-ED3A-47C9-8A18-87CA7B616224";
        private static readonly string ArchivoGestiones = ConfigurationManager.AppSettings["ArchivoGestiones"];
        private static readonly string ArchivoGestionesP = ConfigurationManager.AppSettings["ArchivoGestionesP"];
        private static readonly string ArchivoCedulaInspeccion = ConfigurationManager.AppSettings["ArchivoCedulaInspeccion"];
        private static readonly string ArchivoFotos = ConfigurationManager.AppSettings["ArchivoFotos"];
        private static readonly string ArchivoReferencias = ConfigurationManager.AppSettings["ArchivoReferencias"];
        private static readonly string ArchivoRUnificado = ConfigurationManager.AppSettings["ArchivoRUnificado"];
        private static readonly string ArchivoRegistroAsesores = ConfigurationManager.AppSettings["ArchivoRegistroAsesores"];

        private static readonly string DirectorioSftp = ConfigurationManager.AppSettings["DirectorioSFTP"];
        private static readonly int TiempoMonitoreoSMS = Convert.ToInt32(ConfigurationManager.AppSettings["TiempoMonitoreoSMS"]);

        private static readonly int TiempoEnvioSMS = Convert.ToInt32(ConfigurationManager.AppSettings["TiempoEnvioSMS"]);
        private static readonly string[] FiltroArchivo = ConfigurationManager.AppSettings["FiltroArchivo"].Split(',');
        private static readonly string urlImagenesFormiik = ConfigurationManager.AppSettings["urlImagenesFormiik"];

        // variables para la consultar el WS de estatus del crédito
        private static readonly int TiempoMonitoreoConsultaCreditoWS = Convert.ToInt32(ConfigurationManager.AppSettings["TiempoMonitoreoConsultaCreditoWS"]);

        private static readonly int TiempoEspera = Convert.ToInt32(ConfigurationManager.AppSettings["TiempoEspera"]);

        private static readonly string NotificacionesEmailDispersion = ConfigurationManager.AppSettings["NotificacionesEmailDispersion"];

        private static readonly string NotificacionesEmailDesarrollo = ConfigurationManager.AppSettings["NotificacionesEmailDesarrollo"];

        private static readonly int EstatusABuscar = Convert.ToInt32(ConfigurationManager.AppSettings["EstatusABuscar"]);
        private static readonly string SubjectDispersion = ConfigurationManager.AppSettings["SubjectDispersion"];
        private static readonly string SubjectDesarrollo = ConfigurationManager.AppSettings["SubjectDesarrollo"];
        private static readonly string CorreoDesarrollo = ConfigurationManager.AppSettings["CorreoDesarrollo"];
        private static readonly string ReglaAsignacion = ConfigurationManager.AppSettings["ReglaPGM_ASIGNACION_INI"];
        private static readonly string ReglaProcesoArchivo = ConfigurationManager.AppSettings["ReglaProcesoArchivo"];
        private static readonly string ArchivoPgmCifras = ConfigurationManager.AppSettings["PgmCifras"];
        
        private static readonly string direccion = ConfigurationManager.AppSettings["CorreoResumenRegistroWs"];

        private static DataSet catalogoEstatusPago = new DataSet();

        public static void Main()
        {
            Run();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Run()
        {
            ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            ConnectionDB.EstalecerConnectionString("SqlDefault",
                ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            ConnectionDB.EstalecerConnectionString("BroxelSMSs",
                ConfigurationManager.ConnectionStrings["BroxelSMSs"].ConnectionString);
            ConnectionDB.EstalecerConnectionString("FlockMYO",
                ConfigurationManager.ConnectionStrings["FlockMYO"].ConnectionString);
            Inicializa.Inicializar(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);

            Config.AplicacionActual();
            var serviciosDatos = new EntUsuariosServicios().ObtenerUsuariosServicios(1, 1);
            var dicServicios = serviciosDatos.ToDictionary(x => x.Usuario, x => x.Password);
            Inicializa.InicializarEmail(dicServicios);
            
            #region ProcesarFlock

            if (ConfigurationManager.AppSettings["TareaProcesarFlock"] != null &&
                ConfigurationManager.AppSettings["TareaProcesarFlock"].ToLower() == "true")
            {
                var tProcesarFc = new Task(TareaProcesarFlock);
                tProcesarFc.Start();
            }

            #endregion ProcesarFlock

            #region EnviarOrdenesWS

            if (ConfigurationManager.AppSettings["TareaEnviarOrdenesWS"] != null &&
                ConfigurationManager.AppSettings["TareaEnviarOrdenesWS"].ToLower() == "true")
            {
                var tEnviarOrdenesWs = new Task(TareaEnviarOrdenesWs);
                tEnviarOrdenesWs.Start();
            }

            #endregion EnviarOrdenesWS

            #region ReporteGestionMovil

            if (ConfigurationManager.AppSettings["TareaReporteGestionMovil"] != null &&
                ConfigurationManager.AppSettings["TareaReporteGestionMovil"].ToLower() == "true")
            {
                var tReporteGestionMovil = new Task(TareaReporteGestionMovil);
                tReporteGestionMovil.Start();
            }

            #endregion ReporteGestionMovil

            #region MonitoreoSMS

            if (ConfigurationManager.AppSettings["TareaMonitoreoSMS"] != null &&
                ConfigurationManager.AppSettings["TareaMonitoreoSMS"].ToLower() == "true")
            {
                var tMonitoreoSMS = new Task(TareaMonitoreoSMS);
                tMonitoreoSMS.Start();
            }

            #endregion MonitoreoSMS

            #region EnvioSMS

            if (ConfigurationManager.AppSettings["TareaEnvioSMS"] != null &&
                ConfigurationManager.AppSettings["TareaEnvioSMS"].ToLower() == "true")
            {
                var tEnvioSMS = new Task(TareaEnvioSMS);
                tEnvioSMS.Start();
            }

            #endregion EnvioSMS

            #region ProcesarSMS

            if (ConfigurationManager.AppSettings["TareaProcesarSMS"] != null &&
                ConfigurationManager.AppSettings["TareaProcesarSMS"].ToLower() == "true")
            {
                var tProcesarSMS = new Task(TareaProcesarSMS);
                tProcesarSMS.Start();
            }

            #endregion ProcesarSMS

            #region ProcesarReporteGeneral

            if (ConfigurationManager.AppSettings["TareaProcesarReporteGeneral"] != null &&
                ConfigurationManager.AppSettings["TareaProcesarReporteGeneral"].ToLower() == "true")
            {
                var tProcesarRg = new Task(ProcesarReporteGeneral);
                tProcesarRg.Start();
            }

            #endregion ProcesarReporteGeneral

            #region ConsultarEstatusCreditoWS

            //Tarea para consultar las ordenes cone status 4 que
            if (ConfigurationManager.AppSettings["TareaConsultarEstatusCreditoWS"] != null &&
                ConfigurationManager.AppSettings["TareaConsultarEstatusCreditoWS"].ToLower() == "true" &&
                Config.AplicacionActual().Nombre.Contains("OriginacionMovil")
                )
            {
                var tProcesarOt = new Task(TareaProcesaOrdenesTipo);
                tProcesarOt.Start();
            }

            #endregion ConsultarEstatusCreditoWS

            #region ProcesarOCR

            //Tarea para procesar las identificaciones por OCR
            if (ConfigurationManager.AppSettings["TareaProcesarOCR"] != null &&
                ConfigurationManager.AppSettings["TareaProcesarOCR"].ToLower() == "true")
            {
                var tProcesarOcr = new Task(TareaProcesaOcr);
                tProcesarOcr.Start();
            }

            #endregion ProcesarOCR

            #region ProcesarRespuestasPendientes

            if (ConfigurationManager.AppSettings["TareaProcesarRespuestasPendientes"] != null &&
                ConfigurationManager.AppSettings["TareaProcesarRespuestasPendientes"].ToLower() == "true")
            {
                var tpRespuestasPendientes = new Task(TareaProcesarRespuestasPendientes);
                tpRespuestasPendientes.Start();
            }

            #endregion ProcesarRespuestasPendientes

            #region ProcesarCarteraPreventiva

            if (ConfigurationManager.AppSettings["ProcesarCarteraPreventiva"] != null &&
                ConfigurationManager.AppSettings["ProcesarCarteraPreventiva"].ToLower() == "true")
            {
                var procesarCarteraPreventiva = new Task(ProcesarCarteraPreventiva);
                procesarCarteraPreventiva.Start();
            }

            #endregion ProcesarCarteraPreventiva

            #region ProcesarCarteraVencida

            if (ConfigurationManager.AppSettings["ProcesarCarteraVencida"] != null &&
                ConfigurationManager.AppSettings["ProcesarCarteraVencida"].ToLower() == "true")
            {
                var procesarCarteraVencida = new Task(ProcesarCarteraVencida);
                procesarCarteraVencida.Start();
            }

            #endregion ProcesarCarteraVencida

            #region EnviarAlertasMyo

            if (ConfigurationManager.AppSettings["EnviarAlertasMyo"] != null &&
              ConfigurationManager.AppSettings["EnviarAlertasMyo"].ToLower() == "true")
            {
                var enviarAlertasMyo = new Task(EnviarAlertasMyo);
                enviarAlertasMyo.Start();
            }

            #endregion EnviarAlertasMyo

            #region ObtenerDatosCobranzaPreventiva

            if (ConfigurationManager.AppSettings["ObtenerDatosCobranzaPreventiva"] != null &&
                ConfigurationManager.AppSettings["ObtenerDatosCobranzaPreventiva"].ToLower() == "true")
            {
                var obtenerDatosCobranzaPreventiva = new Task(ObtenerDatosCobranzaPreventiva);
                obtenerDatosCobranzaPreventiva.Start();
            }

            #endregion ObtenerDatosCobranzaPreventiva

            #region EnviarAlertasPreventivas

            if (ConfigurationManager.AppSettings["EnviarAlertasPreventivas"] != null &&
              ConfigurationManager.AppSettings["EnviarAlertasPreventivas"].ToLower() == "true")
            {
                var enviarAlertasPreventivas = new Task(EnviarAlertasPreventivas);
                enviarAlertasPreventivas.Start();
            }

            #endregion EnviarAlertasPreventivas

            #region ObtenerSAFCarteraVencida

            if (ConfigurationManager.AppSettings["ObtenerSAFCarteraVencida"] != null &&
                ConfigurationManager.AppSettings["ObtenerSAFCarteraVencida"].ToLower() == "true")
            {
                var obtenerSafCarteraVencida = new Task(ObtenerSAFCarteraVencida);
                obtenerSafCarteraVencida.Start();
            }

            #endregion ObtenerSAFCarteraVencida

            #region DescargaImagenesFormiik

            if (ConfigurationManager.AppSettings["TareaDescargaImagenesFormiik"] != null &&
                ConfigurationManager.AppSettings["TareaDescargaImagenesFormiik"].ToLower() == "true" &&
                Config.AplicacionActual().Nombre.Contains("OriginacionMovil"))
            {
                var tDescargaImagenesFormiik = new Task(TareaDescargaImagenesFormiik);
                tDescargaImagenesFormiik.Start();
            }

            #endregion DescargaImagenesFormiik

            #region DescargaImagenesFlock

            if (ConfigurationManager.AppSettings["TareaDescargaImagenesFlock"] != null &&
                ConfigurationManager.AppSettings["TareaDescargaImagenesFlock"].ToLower() == "true" &&
                Config.AplicacionActual().Nombre.Contains("MYO"))
            {
                var tDescargaImagenesFlock = new Task(TareaDescargaImagenesFlock);
                tDescargaImagenesFlock.Start();
            }

            #endregion DescargaImagenesFlock

            #region RecibirArchivos

            if (ConfigurationManager.AppSettings["TareaRecibirArchivos"] != null &&
                ConfigurationManager.AppSettings["TareaRecibirArchivos"].ToLower() == "true")
            {
                var tRecibirArchivos = new Task(TareaRecibirArchivos);
                tRecibirArchivos.Start();
            }

            #endregion RecibirArchivos

            #region EnviarArchivos

            if (ConfigurationManager.AppSettings["TareaEnviarArchivos"] != null &&
                ConfigurationManager.AppSettings["TareaEnviarArchivos"].ToLower() == "true")
            {
                var tEnviarArchivos = new Task(TareaEnviarArchivos);
                tEnviarArchivos.Start();
            }

            #endregion EnviarArchivos

            #region EnviaResumenProcesoRegistroWS

            if (ConfigurationManager.AppSettings["TareaEnviaResumenProcesoRegistroWs"] != null &&
                ConfigurationManager.AppSettings["TareaEnviaResumenProcesoRegistroWs"].ToLower() == "true")
            {
                var tEnviarResumenProcesoRegistroWs = new Task(TareaEnviaResumenProcesoRegistroWs);
                tEnviarResumenProcesoRegistroWs.Start();
            }

            #endregion EnviaResumenProcesoRegistroWS

            while (ejecutandose)
            {
                Thread.Sleep(30000);
            }
        }

        #region TareaProcesarFlock

        private static void TareaProcesarFlock()
        {
            while (ejecutandose)
            {
                new Loan().GenerarXml();
                Thread.Sleep(60000);
            }
        }

        #endregion TareaProcesarFlock

        #region TareaEnviarOrdenesWs

        private static void TareaEnviarOrdenesWs()
        {
            var enviarOrdenesWs = new EnviarOrdenesWs();
            while (ejecutandose)
            {
                int cantidad = 0;
                try
                {
                    cantidad = enviarOrdenesWs.Ejecutar();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Tareas-TareaEnviarOrdenesWs",
                        "Error en enviarOrdenesWs.Ejecutar:"
                        + ex.Message);
                }

                if (cantidad <= 0)
                    Thread.Sleep(60000); //Espero 1 minuto
            }
        }

        #endregion TareaEnviarOrdenesWs

        #region TareaReporteGestionMovil

        private static void TareaReporteGestionMovil()
        {
            var msUpGestion = Convert.ToInt32(ConfigurationManager.AppSettings["msReporteGestionMovil"]);
            while (ejecutandose)
            {
                var horaFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    msUpGestion, 0);
                var hora1 = DateTime.Now;
                var hora2 = DateTime.Now.AddSeconds(40);
                int result = DateTime.Compare(horaFinal, hora1);
                int result2 = DateTime.Compare(horaFinal, hora2);
                if (result > 0 && result2 < 0)
                {
                    try
                    {
                        var reporteGestionMovilSp = new ReporteGestionMovilSp();
                        reporteGestionMovilSp.Ejecutar();
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "TareaReporteGestionMovil", "OK");
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "TareaReporteGestionMovil",
                            "Error: " + ex.Message +
                            (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                    }
                }
                Thread.Sleep(30000);
            }
        }

        #endregion TareaReporteGestionMovil

        #region TareaMonitoreoSMS

        private static void TareaMonitoreoSMS()
        {
            var entAutorizacionSMS = new EntSMS();
            while (ejecutandose)
            {
                try
                {
                    entAutorizacionSMS.InsertaAutorizaciones();

                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Tareas-TareaMonitoreoSMS",
                        "Error en entAutorizacionSMS.InsertaAutorizaciones:"
                        + ex.Message);
                }

                Thread.Sleep(TiempoMonitoreoSMS);
            }
        }

        #endregion TareaMonitoreoSMS

        #region TareaEnvioSMS

        private static void TareaEnvioSMS()
        {
            var envioSMS = new EnvioSMS();
            while (ejecutandose)
            {
                try
                {
                    envioSMS.EnviarSMSGestion();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Tareas-TareaEnvioSMS",
                        "Error en envioSMS.EnviarSMSGestion:"
                        + ex.Message);
                }
                Thread.Sleep(TiempoEnvioSMS);
            }
        }

        #endregion TareaEnvioSMS

        #region TareaProcesarSMS

        private static void TareaProcesarSMS()
        {
            while (ejecutandose)
            {
                try
                {
                    var sms = new ProcesarSMS();
                    sms.Procesar();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "TareaProcesarSMS", "Error: " + ex.Message);
                }

                Thread.Sleep(60000); //Espero 1 minuto
            }
        }

        #endregion TareaProcesarSMS

        #region TareaProcesarReporteGeneral

        private static void ProcesarReporteGeneral()
        {
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Tareas",
                    "ProcesarReporteGeneral - Iniciando...");

                var rg = new ReporteGeneral();
                rg.GeneraReporteSupervisores();

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Tareas",
                    "ProcesarReporteGeneral - Finalizando...");
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "Tareas",
                    "ProcesarReporteGeneral - Error:" + ex.Message);
            }
        }

        #endregion TareaProcesarReporteGeneral

        #region TareaConsultarEstatusCreditoWS

        /// <summary>
        ///  Tarea para generara un aviso por correo a los créditos que mejoravit disperso,
        ///  así como marcar el la orden para que ya no se genere de nuevo el aviso
        /// </summary>
        private static void TareaProcesaOrdenesTipo()
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Tareas-ProcesaOrdenesTipo",
            "TareaProcesaOrdenesTipo : Ejecutando tarea ");
            while (ejecutandose)
            {

                var consultaOrdenes = new Orden(0, false, null, null, null, null, null);

                ConsultaEstatusMejoravit consultaestatus = new ConsultaEstatusMejoravit();
                Orden actualizaOrden = new Orden(0, false, null, null, null, null, null);

                try
                {
                    //Obtenemos las ordenes con estatus 4 y tipo ''
                    List<ModeloOrdesConsultaEstatus> consultarOrdenesTipo;
                    consultarOrdenesTipo = new List<ModeloOrdesConsultaEstatus>();
                    consultarOrdenesTipo = consultaOrdenes.ObtenerOrdenesPorEstatusTipo(EstatusABuscar, 0);

                    if (consultarOrdenesTipo.Count > 0)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Tareas-ProcesaOrdenesTipo",
                            "TareaProcesaOrdenesTipo : total de ordenes a procesar: " +
                            consultarOrdenesTipo.Count);
                    }

                    foreach (ModeloOrdesConsultaEstatus item in consultarOrdenesTipo)
                    {
                        //Consultamos por orden con el NSS si ya esta dispersado el crédito
                        RespuestaEstatus respEst = consultaestatus.ConsultaEstatus(item.Nss);
                        if (respEst.IdMensaje.Equals("0000") && respEst.Vuelta == 3)
                        {
                            #region consulta estatus

                            //enviar mail 
                            ModeloOrdesConsultaEstatus modeloCorreo = null;
                            modeloCorreo = consultaOrdenes.ObtenerOrdenCorreo(item.IdOrden, 0);

                            if (modeloCorreo.Correo.Trim() != string.Empty)
                            {

                                //Enviar primero el correo
                                bool seEnvio = Email.EnviarEmail(modeloCorreo.Correo, SubjectDispersion,
                                    string.Format(NotificacionesEmailDispersion, modeloCorreo.Nombre,
                                        modeloCorreo.NumeroCredito));

                                // sí se envio el correo  se actualiza el campo tipo de la tabla ordenes
                                if (seEnvio)
                                {
                                    if (actualizaOrden.ActualizarTipoOrden(item.IdOrden, "D", 4, 0) == 0)
                                    // no se pudo actualizar el campo Ordenes.Tipo
                                    {
                                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Tareas-ProcesaOrdenesTipo",
                                            "TareaProcesaOrdenesTipo: no se pudo actualizar el estatus");

                                        bool seEnvioError = Email.EnviarEmail(CorreoDesarrollo, SubjectDesarrollo,
                                            string.Format(NotificacionesEmailDesarrollo,
                                                "No se pudo actualizar el estatus('D') de la orden: " +
                                                item.IdOrden + ", NSS: " + item.Nss));

                                        if (!seEnvioError)
                                        {
                                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Tareas-ProcesaOrdenesTipo",
                                                "TareaProcesaOrdenesTipo: no se pudo enviar el correo a desarrollo");
                                        }
                                    }
                                    else
                                    {
                                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Tareas-ProcesaOrdenesTipo",
                                            "TareaProcesaOrdenesTipo: actualización correcta Tipo (D): " +
                                            item.IdOrden);
                                    }
                                }
                                else
                                {
                                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Tareas-ProcesaOrdenesTipo",
                                        "TareaProcesaOrdenesTipo: no se pudo enviar el correo al beneficiario: " +
                                        item.IdOrden);
                                }
                            }
                            else
                            {
                                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Tareas-ProcesaOrdenesTipo",
                                    "TareaProcesaOrdenesTipo no se tiene cuenta de correo, idOrden: " +
                                    item.IdOrden + ", NSS: " + item.Nss);

                                bool seEnvioError = Email.EnviarEmail(CorreoDesarrollo, SubjectDesarrollo,
                                    string.Format(NotificacionesEmailDesarrollo,
                                        "El beneficiario de la orden: " +
                                        item.IdOrden + ", NSS: " + item.Nss +
                                        " no tiene capturado su correo."));

                                if (!seEnvioError)
                                {
                                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Tareas-ProcesaOrdenesTipo",
                                        "TareaProcesaOrdenesTipo: no se pudo enviar el correo a desarrollo - sin cuenta de correo el beneficiario.");
                                }
                            }

                            #endregion consulta estatus
                        }
                    }


                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "Tareas-ProcesaOrdenesTipo",
                        "TareaProcesaOrdenesTipo - Error:" + ex.Message);
                }

                Thread.Sleep(TiempoMonitoreoConsultaCreditoWS); // continua ejecutando despues de un tiempo defindo en la configuración
            } // fin while
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Tareas-ProcesaOrdenesTipo",
                  "TareaProcesaOrdenesTipo   finalizando");
        }

        #endregion TareaConsultarEstatusCreditoWS

        #region TareaProcesaOcr

        /// <summary>
        /// Tarea que se encarga de pasar las identificaiones por OCR para validar el acreditado 
        /// </summary>

        private static void TareaProcesaOcr()
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Tareas-TareaProcesaOcr",
            "TareaProcesaOcr - Ejecutando tarea ");
            while (ejecutandose)
            {
                //Primero se marcan los que hayan sido gestionados por el conyuge
                //Obtener ordenes a marcar
                var negocio = new ProcesarOcr();
                try
                {
                    using (var dsFamilia = negocio.ObtenerOrdenesPorAceptacionConvenio(true))
                    {

                        if (dsFamilia != null && dsFamilia.Tables.Count > 0 && dsFamilia.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in dsFamilia.Tables[0].Rows)
                            {
                                //Inserto la respuesta
                                negocio.InsertarRespuestaOcr(Convert.ToInt32(row["idOrden"]),
                                    EtiquetaOcr.AceptadoFamiliar);
                            }
                        }


                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Tareas-TareaProcesaOcr",
                        "TareaProcesaOcr - ObtenerOrdenesPorAceptacionConvenio - Error:" + ex.Message);
                }
                try
                {
                    var listaImagenesOcr = negocio.ObtenerImagenesOcr();
                    foreach (ProcesoOcrModel item in listaImagenesOcr)
                    {
                        //Envio al OCR la solicitud
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Tareas-TareaProcesaOcr",
                            "TareaProcesaOcr - EnviarImagenOcr - Url: " + item.Url);
                        item.Guid = negocio.EnviarImagenOcr(item.Url);
                    }

                    int indice = 0;
                    var fechaEspera = DateTime.Now.AddMinutes(5.0);
                    //Espero a que termine el OCR - Maximo el tiempo de espera
                    while (listaImagenesOcr.Count > 0 && DateTime.Now < fechaEspera)
                    {
                        var respuesta = negocio.ConsultarImagenOcr(listaImagenesOcr[indice].Guid);
                        if (respuesta == null) //Si ocurrió un error lo borra
                            listaImagenesOcr.Remove(listaImagenesOcr[indice]);
                        else if (respuesta == "vacio")
                        {
                            negocio.InsertarRespuestaOcr(listaImagenesOcr[indice].IdOrden, EtiquetaOcr.NoCoincideNoEsIdentificacion);

                            listaImagenesOcr.Remove(listaImagenesOcr[indice]);
                        }
                        else if (respuesta != "")
                        {
                            //Obtiene el resultado del Ocr
                            var etiqueta = negocio.ObtenerEtiquetaOcr(respuesta,
                                listaImagenesOcr[indice].NombreAcreditado);

                            negocio.InsertarRespuestaOcr(listaImagenesOcr[indice].IdOrden, etiqueta);

                            listaImagenesOcr.Remove(listaImagenesOcr[indice]);
                        }

                        //Si la respuesta esta vacia continua con el siguiente 
                        indice++;
                        //Todo: Recorrer al revez para que borre los ultimos primero
                        if (indice >= listaImagenesOcr.Count)
                        {
                            //Si llego al final vuelve a empezar
                            indice = 0;
                            Thread.Sleep(10000); //10 Segundos
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Tareas-TareaProcesaOcr",
                        "TareaProcesaOcr - ObtenerImagenesOcr - Error:" + ex.Message);
                }

                Thread.Sleep(5 * 60 * 1000); //Espera 5 minutos
            } // fin while
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Tareas-TareaProcesaOcr",
                  "TareaProcesaOcr - Finalizando");
        }

        #endregion TareaProcesaOcr

        #region TareaProcesarRespuestasPendientes

        /// <summary>
        /// se encarga de procesar las respuestas que estén pendientes y las manda para guardar la gestión.
        /// </summary>
        private static void TareaProcesarRespuestasPendientes()
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Tareas-TareaProcesarRespuestasPendientes",
            "TareaProcesarRespuestasPendientes - Ejecutando tarea ");
            while (ejecutandose)
            {
                try
                {
                    var ordenesP = Orden.ObtenerRespuestasPendientes();

                    if (ordenesP != "")
                    {
                        foreach (var o in ordenesP.Split(','))
                        {
                            string idusuario = null;

                            var diccionary = Orden.ObtenerDatosRespuestasPendientes(Convert.ToInt32(o), ref idusuario);

                            var resultado = new Respuesta().GuardarRespuesta(Convert.ToInt32(o), diccionary, idusuario == "-111" ? "CapturaWeb" : "MobilePendiente", idusuario, idusuario != null ? Convert.ToInt32(idusuario) : 0, Config.AplicacionActual().Productivo, Config.AplicacionActual().Nombre);

                            if (string.IsNullOrEmpty(resultado))
                            {
                                if (!Orden.BorrarRespuestasPendientes(Convert.ToInt32(o)))
                                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1,
                                        "Tareas-TareaProcesarRespuestasPendientes",
                                        "No se pudo borrar los registros de la respuesta pendiente");
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Tareas-TareaProcesarRespuestasPendientes", ex.Message);
                }


                Thread.Sleep(1 * 60 * 1000); //Espera 3 minutos
            } // fin while
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Tareas-TareaProcesarRespuestasPendientes",
                  "TareaProcesarRespuestasPendientes - Finalizando");
        }

        #endregion TareaProcesarRespuestasPendientes

        #region TareaProcesarCarteraPreventiva

        private static void ProcesarCarteraPreventiva()
        {
            while (ejecutandose)
            {
                try
                {
                    new Cobranza().ProcesarCarteraPreventiva();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ProcesarCarteraPreventiva", "Error: " + ex.Message);
                }

                Thread.Sleep(60000); //Espero 1 minuto
            }
        }

        #endregion TareaProcesarCarteraPreventiva

        #region TareaProcesarCarteraVencida

        private static void ProcesarCarteraVencida()
        {
            while (ejecutandose)
            {
                try
                {
                    new Cobranza().ProcesarCarteraVencida();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ProcesarCarteraVencida", "Error: " + ex.Message);
                }

                Thread.Sleep(60000); //Espero 1 minuto
            }
        }

        #endregion TareaProcesarCarteraVencida

        #region TareaEnviarAlertasMyo

        private static void EnviarAlertasMyo()
        {
            while (ejecutandose)
            {
                try
                {
                    new Cobranza().EnviarAlertas();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EnviarAlertasMyo", "Error: " + ex.Message);
                }

                Thread.Sleep(60000); //Espero 1 minuto
            }
        }

        #endregion TareaEnviarAlertasMyo

        #region TareaObtenerDatosCobranzaPreventiva

        private static void ObtenerDatosCobranzaPreventiva()
        {
            int hrsCobPrev = Convert.ToInt32(ConfigurationManager.AppSettings["hrsCobPrev"]);
            int msCobPrev = Convert.ToInt32(ConfigurationManager.AppSettings["msCobPrev"]);
            while (ejecutandose)
            {

                var horaFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hrsCobPrev, msCobPrev, 0);
                var hora1 = DateTime.Now;
                var hora2 = DateTime.Now.AddSeconds(20);
                int result = DateTime.Compare(horaFinal, hora1);
                int result2 = DateTime.Compare(horaFinal, hora2);
                if (result > 0 && result2 < 0)
                {
                    try
                    {
                        new Cobranza().ObtenerDatosCobranzaPreventiva();
                        Thread.Sleep(30000); //Espera 30 segundos
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "obtenerDatosCobranzaPreventiva", "Error: " + ex.Message);
                    }

                }
                Thread.Sleep(10000); //Espera 10 segundos
            }
        }

        #endregion TareaObtenerDatosCobranzaPreventiva

        #region TareaEnviarAlertasPreventivas

        private static void EnviarAlertasPreventivas()
        {
            while (ejecutandose)
            {
                try
                {
                    new Cobranza().EnviarAlertasPreventivas();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EnviarAlertasPreventivas", "Error: " + ex.Message);
                }

                Thread.Sleep(60000); //Espero 1 minuto
            }
        }

        #endregion TareaEnviarAlertasPreventivas

        #region TareaObtenerSAFCarteraVencida

        private static void ObtenerSAFCarteraVencida()
        {
            int hrsUpfoto = Convert.ToInt32(ConfigurationManager.AppSettings["hrsSAFCV"]);
            int msUpFoto = Convert.ToInt32(ConfigurationManager.AppSettings["msSAFCV"]);
            while (ejecutandose)
            {

                var horaFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hrsUpfoto,
                    msUpFoto, 0);
                var hora1 = DateTime.Now;
                var hora2 = DateTime.Now.AddSeconds(20);
                int result = DateTime.Compare(horaFinal, hora1);
                int result2 = DateTime.Compare(horaFinal, hora2);
                if (result > 0 && result2 < 0)
                {
                    try
                    {
                        new Cobranza().ObtenerSAFCarteraVencida();
                        Thread.Sleep(30000); //Espera 30 segundos
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ObtenerSAFCarteraVencida", "Error: " + ex.Message);
                    }

                }
                Thread.Sleep(10000); //Espera 10 segundos
            }
        }

        #endregion TareaObtenerSAFCarteraVencida

        #region TareaProcesarReasignaciones

        private static void TareaProcesarReasignaciones()
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Tareas-TareaProcesarReasignaciones", "TareaProcesarReasignaciones - Ejecutando tarea ");
            try
            {
                var catalogoGeneral = new CatalogoGeneral();
                var CGmodelo = new CatalogoGeneralModel() { Llave = "BloqueoReasignaciones", Valor = "1" };
                Trace.WriteLine("se procede a bloquear reporte");
                catalogoGeneral.InsUpdCatalogoGeneral(CGmodelo);
                List<Task> tasks = new List<Task>(2);
                Task t = new Task(ProcesarReasignacionesMovil);
                tasks.Add(t);
                t.Start();
                Task t2 = new Task(ProcesarReasignacionesCreditos);
                tasks.Add(t2);
                t2.Start();
                Task.WaitAll(tasks.ToArray());
                Trace.WriteLine("se procede desbloquear reporte");
                CGmodelo.Valor = "0";
                catalogoGeneral.InsUpdCatalogoGeneral(CGmodelo);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Tareas-TareaProcesarReasignaciones", ex.Message);
            }
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Tareas-TareaProcesarReasignaciones", "TareaProcesarReasignaciones - Finaliza tarea ");
        }

        private static void ProcesarReasignacionesMovil()
        {
            Trace.WriteLine(String.Format("{0} -Inicio: ProcesarReasignacionesMovil", DateTime.Now));
            var cnnSql = ConexionSql.Instance;
            DataSet dsOrdenes = null;
            try
            {

                dsOrdenes = cnnSql.ObtenerOrdenesAMover();
                if (dsOrdenes != null && dsOrdenes.Tables.Count > 0)
                {
                    var idordenArr = dsOrdenes.Tables[0].Rows.Cast<DataRow>().Select(rw => rw["idorden"]).ToList();
                    List<Task> tasks = new List<Task>();

                    for (int i = 0; i < idordenArr.Count; i += 100)
                    {
                        int restantes = idordenArr.Count - i;

                        var valoresAProcesar = (restantes <= 100)
                            ? idordenArr.GetRange(i, restantes)
                            : idordenArr.GetRange(i, 100);


                        var ordenes = string.Join(",", valoresAProcesar);

                        Task t = new Task(
                       () =>
                       {
                           try
                           {
                               Trace.WriteLine(
                                   String.Format("{0} -ProcesarReasignacionesMovil.OrdenesAMover: {1} ",
                                       DateTime.Now, ordenes));
                               cnnSql.OrdenesAMover(ordenes);

                           }
                           catch (Exception e)
                           {
                               Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Tareas",
                                   "Exception: ProcesarReasignacionesMovil.OrdenesAMover- " + e.Message);
                               Trace.WriteLine(
                                   String.Format(
                                       "{0} -Error:ProcesarReasignacionesMovil.OrdenesAMover: {1} Exception:{2} ",
                                       DateTime.Now, ordenes, e.Message));
                           }

                       });
                        tasks.Add(t);
                        t.Start();

                    }
                    Task.WaitAll(tasks.ToArray());
                }
                Trace.WriteLine(String.Format("{0} -Fin: ProcesarReasignacionesMovil", DateTime.Now));
            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "Exception: ProcesarReasignacionesMovil- " + e.Message);
            }
        }

        private static void ProcesarReasignacionesCreditos()
        {
            Trace.WriteLine(String.Format("{0} -Inicio: ProcesarReasignacionesCreditos", DateTime.Now));
            var cnnSql = ConexionSql.Instance;
            DataSet dsCreditos = null;
            try
            {
                dsCreditos = cnnSql.ObtenerCreditosAMover();
                if (dsCreditos != null && dsCreditos.Tables.Count > 0)
                {
                    var numCredArr = dsCreditos.Tables[0].Rows.Cast<DataRow>().Select(rw => rw["num_Cred"]).ToList();
                    List<Task> tasks = new List<Task>();

                    for (int i = 0; i < numCredArr.Count; i += 100)
                    {
                        int restantes = numCredArr.Count - i;

                        var valoresAProcesar = (restantes <= 100)
                            ? numCredArr.GetRange(i, restantes)
                            : numCredArr.GetRange(i, 100);


                        var creditos = string.Join(",", valoresAProcesar);

                        Task t = new Task(
                            () =>
                            {
                                try
                                {
                                    Trace.WriteLine(
                                        String.Format("{0} -ProcesarReasignacionesCreditos.CreditosAMover: {1} ",
                                            DateTime.Now, creditos));
                                    cnnSql.CreditosAMover(creditos);

                                }
                                catch (Exception e)
                                {
                                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Tareas",
                                        "Exception: ProcesarReasignacionesCreditos.CreditosAMover- " + e.Message);
                                    Trace.WriteLine(
                                        String.Format(
                                            "{0} -Error:ProcesarReasignacionesCreditos.CreditosAMover: {1} Exception:{2} ",
                                            DateTime.Now, creditos, e.Message));
                                }

                            });
                        tasks.Add(t);
                        t.Start();

                    }
                }
                Trace.WriteLine(String.Format("{0} -Fin: ProcesarReasignacionesCreditos", DateTime.Now));
            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Tareas",
                    "Exception: ProcesarReasignacionesCreditos- " + e.Message);
            }
        }

        #endregion TareaProcesarReasignaciones

        #region TareaDescargaImagenesFormiik

        private static void TareaDescargaImagenesFormiik()
        {
            while (ejecutandose)
            {
                var listaTareas = new List<Task>();
                const string sql = "exec ObtenerImagenesSinDescargar";
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                foreach (var row in ds.Tables[0].AsEnumerable())
                {
                    var imagen = row["Valor"].ToString();
                    var orden = row["idOrden"].ToString();
                    var nombre = row["Titulo"].ToString();
                    var visita = row["visitaCorresp"].ToString();
                    var campo = row["idCampo"].ToString();
                    var tarea = new Task(() => DescargaImagenOrden(orden, imagen, nombre, visita, campo));
                    tarea.Start();
                    listaTareas.Add(tarea);
                    Thread.Sleep(1);
                }
                Task.WaitAll(listaTareas.ToArray());
                Thread.Sleep(120000);
            }
        }

        private static void DescargaImagenOrden(string orden, string url, string nombre, string visita, string campo)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Tareas",
                    "DescargaImagenOrden - " + orden + " - " + url);
            bool disponible;
            string directorio = ConfigurationManager.AppSettings["URLFormiik"] ?? "";
            using (var client = new MyClient())
            {
                client.HeadOnly = true;
                try
                {
                    client.DownloadString(directorio + url);
                    disponible = true;
                }
                catch (Exception)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Tareas",
                     "DescargaImagenOrden - No Disponible:  " + url);
                    disponible = false;
                }
            }

            if (!disponible) return;

            var directorioImagenes = ConfigurationManager.AppSettings["DirectorioImagenesFormiik"] ?? "";

            try
            {
                var uri = new Uri(directorio + url);

                var filename = Path.GetFileName(uri.LocalPath);
                var fase = visita == "1" ? "Originacion" : (visita == "2" ? "Formalizacion" : "Preautorizacion");
                var path = directorioImagenes + orden + @"\" + fase;
                var url1 = urlImagenesFormiik + orden + @"/" + fase;

                using (var client = new WebClient())
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (!File.Exists(path + @"\" + filename))
                    {
                        client.DownloadFile(directorio + url, path + @"\" + filename);
                        Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "Tareas",
                            "DescargaImagenOrden - Imagen Descargada - " + orden + " - " + url);
                        File.Move(path + @"\" + filename,
                            path + @"\" + nombre + Path.GetExtension(path + @"\" + filename));
                        new EntGestionadas().ActualizaRutaImagenes(orden, campo,
                            (url1 + @"/" + nombre + Path.GetExtension(url1 + @"/" + filename)));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "Tareas",
                    "DescargaImagenOrden - Error:" + ex.Message);
            }
        }

        #endregion TareaDescargaImagenesFormiik

        #region TareaDescargaImagenesFlock

        private static void TareaDescargaImagenesFlock()
        {
            while (ejecutandose)
            {
                var listaTareas = new List<Task>();
                const string sql = "exec ObtenerImagenesSinDescargarFlock";
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                foreach (var row in ds.Tables[0].AsEnumerable())
                {
                    var imagen = row["Valor"].ToString();
                    var orden = row["idOrden"].ToString();
                    var nombre = row["Titulo"].ToString();
                    var visita = row["visitaCorresp"].ToString();
                    var campo = row["idCampo"].ToString();
                    var idFlock = row["idFlock"].ToString();
                    var tarea = new Task(() => DescargaImagenOrdenFlock(orden, imagen, nombre, visita, campo, idFlock));
                    tarea.Start();
                    listaTareas.Add(tarea);
                    Thread.Sleep(1);
                }
                Task.WaitAll(listaTareas.ToArray());
                Thread.Sleep(120000);
            }
        }

        private static void DescargaImagenOrdenFlock(string orden, string url, string nombre, string visita, string campo, string idFlock)
        {
            bool disponible;
            string directorio = ConfigurationManager.AppSettings["URLFlock"] ?? "";
            using (var client = new MyClient())
            {
                client.HeadOnly = true;
                try
                {
                    client.DownloadString(directorio + url);
                    disponible = true;
                }
                catch (Exception)
                {
                    disponible = false;
                }
            }

            if (!disponible) return;

            var directorioImagenes = ConfigurationManager.AppSettings["DirectorioImagenesFlock"] ?? "";

            try
            {
                var uri = new Uri(directorio + url);

                var filename = Path.GetFileName(uri.LocalPath);
                const string fase = ""; //visita == "1" ? "MesaControl" : (visita == "2" ? "CallCenter" : "Legal");
                var path = directorioImagenes + orden + @"\" + fase;
                var url1 = urlImagenesFormiik + orden + @"/" + fase;

                using (var client = new WebClient())
                {
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    if (!File.Exists(path + @"\" + filename))
                    {
                        client.DownloadFile(directorio + url, path + @"\" + filename);
                        Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "Tareas",
                            "DescargaImagenOrden - Imagen Descargada - " + orden + " - " + url);
                        File.Move(path + @"\" + filename,
                            path + @"\" + nombre + Path.GetExtension(path + @"\" + filename));
                        new EntGestionadas().ActualizaRutaImagenes(orden, campo,
                            (url1 + @"/" + nombre + Path.GetExtension(url1 + @"/" + filename) + "*" + (idFlock ?? "0")));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "Tareas",
                    "DescargaImagenOrden - Error:" + ex.Message);
            }
        }

        #endregion TareaDescargaImagenesFlock

        #region TareaRecibirArchivos

        private static void TareaRecibirArchivos()
        {
            //Se comenta Archivo pagos,REasignaciones , sigue en pruebas
            _archivoProcesar = (ConfigurationManager.AppSettings["ArchivoAsignacion"]
                                + ";" + ConfigurationManager.AppSettings["ArchivoComplementaria"]
                                + ";" + ConfigurationManager.AppSettings["ArchivoActualizacion"]
                                + ";" + ConfigurationManager.AppSettings["ArchivoAsignacionIni"]
                                + ";" + ConfigurationManager.AppSettings["ArchivoPagos"]).Split(';');

            //// Create a new FileSystemWatcher and set its properties.
            var watcher = new FileSystemWatcher
            {
                Path = DirectorioSftp,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                               | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = FiltroArchivo[0]
            };
            // to Export archives

            /* Watch for changes in LastAccess and LastWrite times, and
            the renaming of files or directories. */
            //Only watch text files.

            //// Add event handlers.
            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnRenamed;

            //// Begin watching.
            watcher.EnableRaisingEvents = true;


            while (ejecutandose)
            {
                if (ContarProcesos() <= 0)
                {
                    var revisaArchivos =
                        new Task(() => BuscarArchivosSftp(DirectorioSftp));
                    revisaArchivos.Start();
                    revisaArchivos.Wait();

                    if (ContarProcesos() <= 0)
                    {
                        //Revisa si existe algun archivo por importar
                        var tarea = new Task(ImportaArchivos);
                        tarea.Start();
                    }
                }
                Thread.Sleep(30000); //Espera 30 segundos

                try
                {
                    var catalogoGeneral = new CatalogoGeneral();
                    var CGmodelo =
                        catalogoGeneral.ObtenerDatosCatalogoGeneral(new CatalogoGeneralModel()
                        {
                            Llave = "BloqueoReasignaciones",
                            FechaCreacion = DateTime.Now
                        });
                    bool correoEnviado = false;
                    while (CGmodelo.Valor == "1")
                    {
                        Trace.WriteLine(String.Format("{0} - BloqueoReasignaciones se encuentra activo desde: {1}",
                            DateTime.Now, CGmodelo.FechaCreacion));
                        if (DateTime.Compare(DateTime.Now.AddHours(-1), CGmodelo.FechaCreacion) > 0)
                        {
                            if (!correoEnviado)
                            {
                                NotificacionesEmail("Proceso de reasignaciones - alerta ",
                                    (String.Format(
                                        "{0} - BloqueoReasignaciones se encuentra activo desde: {1} y excede 1 hora",
                                        DateTime.Now, CGmodelo.FechaCreacion)), true);
                                correoEnviado = true;
                            }
                            Trace.WriteLine(
                                String.Format(
                                    "{0} - BloqueoReasignaciones se encuentra activo desde: {1} con tiempo mayor a 1 hora ",
                                    DateTime.Now, CGmodelo.FechaCreacion));
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Tareas",
                                String.Format("BloqueoReasignaciones:Bloqueado desde: {0} con tiempo mayor a 1 hora ",
                                    CGmodelo.FechaCreacion));
                        }
                        if (DateTime.Compare(DateTime.Now.AddHours(-2), CGmodelo.FechaCreacion) > 0)
                        {
                            CGmodelo.Valor = "0";
                            catalogoGeneral.InsUpdCatalogoGeneral(CGmodelo);
                            Trace.WriteLine(
                                String.Format(
                                    "{0} - BloqueoReasignaciones se procede a desactivar por exceder 2 horas ",
                                    DateTime.Now));
                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Tareas",
                                "BloqueoReasignaciones se procede a desactivar por exceder 2 horas ");
                        }
                        Thread.Sleep(5 * 60 * 1000); //Espera 5 minutos
                        catalogoGeneral.ObtenerDatosCatalogoGeneral(CGmodelo);
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Tareas", "Error - TareaRecibirArchivos: " + ex.Message);
                    Thread.Sleep(5 * 60 * 1000); //Espera 5 minutos
                }
            }
        }

        private static void BuscarArchivosSftp(string pathLocal)
        {
            try
            {
                Console.WriteLine("Buscando archivos en el sFTP ... " + DateTime.Now);
                Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos", "Buscando archivos en el sFTP ... " + DateTime.Now);
                string host = ConfigurationManager.AppSettings["sFTPhost"]; // "201.134.132.136";
                string username = ConfigurationManager.AppSettings["sFTPusername"]; // "UserPPMCSI";
                string password = ConfigurationManager.AppSettings["sFTPpassword"]; // "Us3rPPMC$1";
                string remoteDirectory = ConfigurationManager.AppSettings["sFTPremoteDEntrega"];
                int port = Convert.ToInt32(ConfigurationManager.AppSettings["sFTPPort"]);

                //"./PPM/ENTREGA"; // . always refers to the current directory.

                using (var sftp = new SftpClient(host, port, username, password))
                {
                    sftp.Connect();
                    var files = sftp.ListDirectory(remoteDirectory);

                    foreach (var file in files)
                    {
                        Console.WriteLine(file.FullName);
                        Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos", "BuscarArchivosSftp:" + file.FullName);

                        if (file.IsDirectory)
                        {
                            continue;
                        }

                        try
                        {
                            using (var localFile = File.OpenWrite(pathLocal + file.Name))
                            {
                                sftp.DownloadFile(file.FullName, localFile);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos", "BuscarArchivosSftp: Fallo al intentar descargar los archivos del sFTP: " + file.FullName + "Error: " + ex.Message);
                            NotificacionesEmail("Error al descargar archivo del sFTP: " + DateTime.Now, "Fallo al intentar descargar los archivos del sFTP: " + file.FullName + "Error: " + ex.Message + " inner: " + ex.InnerException.Message, true);
                            sftp.Disconnect();
                            throw;
                        }
                        Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos", "BuscarArchivosSftp: Archivo descargado " + file.FullName);
                        sftp.DeleteFile(file.FullName);
                        Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos", "BuscarArchivosSftp: Archivo borrado" + file.FullName);
                    }
                    sftp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fallo al intentar obtener los archivos del sFTP : " + ex.Message);
                Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos", "BuscarArchivosSftp: Fallo al intentar obtener los archivos del sFTP : " + ex.Message);
            }

        }

        private static void ImportaArchivos()
        {
            Procesando();
            var context = new SistemasCobranzaEntities();
            var archivos = Directory.GetFiles(ConfigurationManager.AppSettings["DirectorioArchivosDescomprimidos"],
                FiltroArchivo[1]);
            foreach (var archivo in archivos)
            {
                bool valido = false;
                foreach (string s in _archivoProcesar)
                {
                    valido = (valido ||
                              archivo.ToUpper() ==
                              (ConfigurationManager.AppSettings["DirectorioArchivosDescomprimidos"] + "\\" + s).ToUpper());


                }
                valido = ProcesarReglasAsignacion(archivo);
                if (valido)
                {
                    try
                    {
                        var fechaABuscar = DateTime.Now.AddHours(-12);
                        string archivo1 = archivo;
                        var arch = from a in context.Archivos
                                   where a.Archivo == archivo1
                                         && a.Tipo == "txt"
                                         && a.FechaAlta > fechaABuscar
                                   orderby a.Fecha descending
                                   select a;
                        if (arch.Any())
                        {
                            var ar = arch.First();

                            if (ar.Estatus == "En uso" ||
                                (ar.Estatus == "Procesando" && DateTime.Now > ar.Fecha.AddHours(2)))
                            //Si esta en uso hace un reintento, si tiene mas de 2 hs en procesando lo intenta tomar de nuevo
                            {
                                Thread.Sleep(5000);
                                string archivo2 = archivo;
                                var tarea = new Task(() => ImportarArchivo(archivo2, ar));
                                tarea.Start();
                            }
                            else
                            {
                                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos",
                                    "El archivo: " + ar.Archivo + " esta en estatus: " + ar.Estatus);
                                if (ar.Estatus == "Procesado")
                                {
                                    string archivo2 = archivo;
                                    var tarea = new Task(() => ImportarArchivo(archivo2, null));
                                    tarea.Start();
                                }
                                else
                                {
                                    MoverArchivoProcesado(ar.Archivo, ar.Fecha, "Importados");
                                }
                            }
                        }
                        else
                        {
                            string archivo2 = archivo;
                            var tarea = new Task(() => ImportarArchivo(archivo2, null));
                            tarea.Start();
                        }
                    }
                    catch (Exception)
                    {
                        string archivo2 = archivo;
                        var tarea = new Task(() => ImportarArchivo(archivo2, null));
                        tarea.Start();
                    }

                }
            }
            Terminado();
        }

        private static void MoverArchivoProcesado(string origen, DateTime fechaAlta, string carpeta)
        {
            string pathDestino = Path.GetDirectoryName(origen) + "\\" + carpeta + "\\" +
                                 fechaAlta.ToString("yyyyMMddHHmmss") + " - " + Path.GetFileName(origen);
            MoverArchivo(origen, pathDestino);
        }

        private static bool ProcesarReglasAsignacion(string nombreArchivo)
        {

            try
            {
                var reglaArch = ReglaAsignacion.Split('|');
                if (nombreArchivo.Split('\\').LastOrDefault().ToUpper() == reglaArch.FirstOrDefault().ToUpper())
                {
                    return DateTime.Today.Day.ToString() == reglaArch.LastOrDefault();
                }
            }
            catch (Exception)
            {
            }
            return true;
        }
        private static bool ProcesarReglasArchivo(string nombreArchivo)
        {

            try
            {
                var reglaArch = ReglaProcesoArchivo.Split(',');
                foreach (var archTemp in reglaArch)
                {
                    var arch = archTemp.Split('|');
                    if (nombreArchivo.Split('\\').LastOrDefault().ToUpper() == arch.FirstOrDefault().ToUpper())
                    {
                         if(DateTime.Today.Day.ToString() == arch.LastOrDefault())
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        private static void MoverArchivo(string origen, string destino)
        {
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos",
                    "Moviendo archivo " + origen + " a " + destino);
                if (File.Exists(destino))
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos",
                        "El archivo " + destino + " ya existe. Se va a reemplazar.");
                    File.Delete(destino);
                }
                File.Move(origen, destino);
            }
            catch (Exception errMover)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "MoverArchivo:No se pudo mover el archivo: " + origen + " - Error: " + errMover.Message);
            }
        }

        #endregion TareaRecibirArchivos

        #region TareaEnviarArchivos

        private static void TareaEnviarArchivos()
        {
            var watcherE = new FileSystemWatcher
            {
                Path = DirectorioSftp,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                               | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = "*.TXT"

            };
            // Add event handlers.
            watcherE.Created += OnChangedE;

            // Begin watching.
            watcherE.EnableRaisingEvents = true;
            var taskGestion = new Task(GestionTxtTask);
            taskGestion.Start();

            var taskGestionP = new Task(GestionTxtTaskP);
            taskGestionP.Start();

            var taskCedulainspeccion = new Task(CedulaInspeccionTxtTask);
            taskCedulainspeccion.Start();

            var taskFotos = new Task(FotosTxtTask);
            taskFotos.Start();

            var taskReferencias = new Task(ReferenciasTxtTask);
            taskReferencias.Start();

            var taskRUnificado = new Task(RUnificadoTxtTask);
            taskRUnificado.Start();

            var taskRegistroAsesores = new Task(RegistroAsesoresTxtTask);
            taskRegistroAsesores.Start();

        }

        private static void GestionTxtTask()
        {
            int hrsUpGestion = Convert.ToInt32(ConfigurationManager.AppSettings["hrsUpGestion"]);
            int msUpGestion = Convert.ToInt32(ConfigurationManager.AppSettings["msUpGestion"]);
            while (ejecutandose)
            {
                var horaFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hrsUpGestion,
                    msUpGestion, 0);
                var hora1 = DateTime.Now;
                var hora2 = DateTime.Now.AddSeconds(20);
                int result = DateTime.Compare(horaFinal, hora1);
                int result2 = DateTime.Compare(horaFinal, hora2);
                if (result > 0 && result2 < 0)
                {
                    GestionTxt(1);
                    Thread.Sleep(30000); //Espera 30 segundos
                }
                Thread.Sleep(10000); //Espera 10 segundos
            }
        }

        private static void GestionTxtTaskP()
        {
            int hrsUpGestion = Convert.ToInt32(ConfigurationManager.AppSettings["hrsUpGestionP"]);
            int msUpGestion = Convert.ToInt32(ConfigurationManager.AppSettings["msUpGestionP"]);
            while (ejecutandose)
            {
                var horaFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hrsUpGestion,
                    msUpGestion, 0);
                var hora1 = DateTime.Now;
                var hora2 = DateTime.Now.AddSeconds(20);
                int result = DateTime.Compare(horaFinal, hora1);
                int result2 = DateTime.Compare(horaFinal, hora2);
                if (result > 0 && result2 < 0)
                {
                    GestionTxt(2);
                    Thread.Sleep(30000); //Espera 30 segundos
                }
                Thread.Sleep(10000); //Espera 10 segundos
            }
        }

        private static void CedulaInspeccionTxtTask()
        {
            int hrsUpGestion = Convert.ToInt32(ConfigurationManager.AppSettings["hrsUpCedulaInspeccion"]);
            int msUpGestion = Convert.ToInt32(ConfigurationManager.AppSettings["msUpCedulaInspeccion"]);
            while (ejecutandose)
            {
                var horaFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hrsUpGestion,
                    msUpGestion, 0);
                var hora1 = DateTime.Now;
                var hora2 = DateTime.Now.AddSeconds(20);
                int result = DateTime.Compare(horaFinal, hora1);
                int result2 = DateTime.Compare(horaFinal, hora2);
                if (result > 0 && result2 < 0)
                {
                    GestionTxt(3);
                    Thread.Sleep(30000); //Espera 30 segundos
                }
                Thread.Sleep(10000); //Espera 10 segundos
            }
        }

        private static void FotosTxtTask()
        {
            int hrsUpfoto = Convert.ToInt32(ConfigurationManager.AppSettings["hrsUpFoto"]);
            int msUpFoto = Convert.ToInt32(ConfigurationManager.AppSettings["msUpFoto"]);
            while (ejecutandose)
            {

                var horaFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hrsUpfoto,
                    msUpFoto, 0);
                var hora1 = DateTime.Now;
                var hora2 = DateTime.Now.AddSeconds(20);
                int result = DateTime.Compare(horaFinal, hora1);
                int result2 = DateTime.Compare(horaFinal, hora2);
                if (result > 0 && result2 < 0)
                {
                    FotosTxt();
                    Thread.Sleep(30000); //Espera 30 segundos
                }
                Thread.Sleep(10000); //Espera 10 segundos
            }
        }

        private static void ReferenciasTxtTask()
        {
            int hrsUpReferencias = Convert.ToInt32(ConfigurationManager.AppSettings["hrsUpReferencias"]);
            int msUpReferencias = Convert.ToInt32(ConfigurationManager.AppSettings["msUpReferencias"]);
            while (ejecutandose)
            {

                var horaFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hrsUpReferencias,
                    msUpReferencias, 0);
                var hora1 = DateTime.Now;
                var hora2 = DateTime.Now.AddSeconds(20);
                int result = DateTime.Compare(horaFinal, hora1);
                int result2 = DateTime.Compare(horaFinal, hora2);
                if (result > 0 && result2 < 0)
                {
                    InsertaHistoricoDiario();
                    Thread.Sleep(10000);
                   if(ProcesarReglasArchivo(ArchivoReferencias))
                       ReferenciasTxt();
                    Thread.Sleep(30000); //Espera 30 segundos
                }
                Thread.Sleep(10000); //Espera 10 segundos
            }
        }

        private static void RUnificadoTxtTask()
        {
            int hrsUpGestion = Convert.ToInt32(ConfigurationManager.AppSettings["hrsUpRUnificado"]);
            int msUpGestion = Convert.ToInt32(ConfigurationManager.AppSettings["msUpRUnificado"]);
            while (ejecutandose)
            {
                var horaFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hrsUpGestion,
                    msUpGestion, 0);
                var hora1 = DateTime.Now;
                var hora2 = DateTime.Now.AddSeconds(20);
                int result = DateTime.Compare(horaFinal, hora1);
                int result2 = DateTime.Compare(horaFinal, hora2);
                if (result > 0 && result2 < 0)
                {
                    RUnificadoTxt();
                    Thread.Sleep(30000); //Espera 30 segundos
                }
                Thread.Sleep(10000); //Espera 10 segundos
            }
        }

        private static void RegistroAsesoresTxtTask()
        {
            int hrsUpRegistroAsesores = Convert.ToInt32(ConfigurationManager.AppSettings["hrsUpRegistroAsesores"]);
            int msUpRegistroAsesores = Convert.ToInt32(ConfigurationManager.AppSettings["msUpRegistroAsesores"]);
            while (ejecutandose)
            {
                var horaFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hrsUpRegistroAsesores, msUpRegistroAsesores, 0);
                var hora1 = DateTime.Now;
                var hora2 = DateTime.Now.AddSeconds(20);
                int result = DateTime.Compare(horaFinal, hora1);
                int result2 = DateTime.Compare(horaFinal, hora2);
                if (result > 0 && result2 < 0)
                {
                    RegistroAsesoresTxt();
                    Thread.Sleep(30000); //Espera 30 segundos
                }
                Thread.Sleep(10000); //Espera 10 segundos
            }
        }

        private static void GestionTxt(int tipoArchivo)
        {
            var inicioProceso = DateTime.Now;
            var archivoG = GeneraGestionBuilder(tipoArchivo);
            try
            {
                if (archivoG.Registros > 0)
                {
                    var sw = new StreamWriter(DirectorioSftp + archivoG.Nombre);
                    using (sw)
                    {
                        sw.WriteLine(archivoG.Contenido);
                        sw.Close();
                    }
                    var tiempo = DateTime.Now.Subtract(inicioProceso);
                    var arch = new Archivos
                    {
                        Fecha = DateTime.Now,
                        Archivo = DirectorioSftp + archivoG.Nombre,
                        Estatus = "Creado",
                        Tiempo = Convert.ToInt32(tiempo.TotalSeconds),
                        Tipo = "txt",
                        FechaAlta = DateTime.Now,
                        Registros = archivoG.Registros
                    };
                    using (var context = new SistemasCobranzaEntities())
                    {
                        context.Archivos.Add(arch);
                        context.SaveChanges();
                    }

                    var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("ArchivoCreado");
                    mensajeServ.Titulo = string.Format(mensajeServ.Titulo, archivoG.Nombre);
                    mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, archivoG.Nombre, archivoG.Registros.ToString(CultureInfo.InvariantCulture), DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    NotificacionesEmail(mensajeServ.Titulo, mensajeServ.Mensaje, false, mensajeServ.EsHtml);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos", "GestionTxt -Generar Archivo " + e.Message);
            }
        }

        private static void FotosTxt()
        {
            var inicioProceso = DateTime.Now;
            var archivoF = GeneraFotosBuilder();
            try
            {
                if (archivoF.Registros > 0)
                {
                    var sw = new StreamWriter(DirectorioSftp + archivoF.Nombre);
                    using (sw)
                    {
                        sw.WriteLine(archivoF.Contenido);
                        sw.Close();
                    }
                    var tiempo = DateTime.Now.Subtract(inicioProceso);
                    var arch = new Archivos
                    {
                        Fecha = DateTime.Now,
                        Archivo = DirectorioSftp + archivoF.Nombre,
                        Estatus = "Creado",
                        Tiempo = Convert.ToInt32(tiempo.TotalSeconds),
                        Tipo = "txt",
                        FechaAlta = DateTime.Now,
                        Registros = archivoF.Registros
                    };
                    using (var context = new SistemasCobranzaEntities())
                    {
                        context.Archivos.Add(arch);
                        context.SaveChanges();
                    }

                    var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("ArchivoCreado");
                    mensajeServ.Titulo = string.Format(mensajeServ.Titulo, archivoF.Nombre);
                    mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, archivoF.Nombre, archivoF.Registros.ToString(CultureInfo.InvariantCulture), DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    NotificacionesEmail(mensajeServ.Titulo, mensajeServ.Mensaje, false, mensajeServ.EsHtml);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos", "FotosTxt-crear archivo " + e.Message);
            }
        }

        private static void ReferenciasTxt()
        {
            var inicioProceso = DateTime.Now;
            var archivoR = GeneraReferenciasBuilder();
            try
            {
                if (archivoR.Registros > 0)
                {
                    var sw = new StreamWriter(DirectorioSftp + archivoR.Nombre);
                    using (sw)
                    {
                        sw.WriteLine(archivoR.Contenido);
                        sw.Close();
                    }
                    var tiempo = DateTime.Now.Subtract(inicioProceso);
                    var arch = new Archivos
                    {
                        Fecha = DateTime.Now,
                        Archivo = DirectorioSftp + archivoR.Nombre,
                        Estatus = "Creado",
                        Tiempo = Convert.ToInt32(tiempo.TotalSeconds),
                        Tipo = "txt",
                        FechaAlta = DateTime.Now,
                        Registros = archivoR.Registros
                    };
                    using (var context = new SistemasCobranzaEntities())
                    {
                        context.Archivos.Add(arch);
                        context.SaveChanges();
                    }
                    ArchCifras(archivoR);
                    var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("ArchivoCreado");
                    mensajeServ.Titulo = string.Format(mensajeServ.Titulo, archivoR.Nombre);
                    mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, archivoR.Nombre, archivoR.Registros.ToString(CultureInfo.InvariantCulture), DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    NotificacionesEmail(mensajeServ.Titulo, mensajeServ.Mensaje, false, mensajeServ.EsHtml);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "ReferenciasTxt -CreacionArchivo " + e.Message);
            }
        }

        private static void ArchCifras( ArchivoExportado arch)
        {
            try
            {
                var inicioProceso = DateTime.Now;
                var sw = new StreamWriter(DirectorioSftp + ArchivoPgmCifras);
                using (sw)
                {
                    sw.WriteLine("{0}|{1}|{2}|{3}|{4}", "PPM", "ACR", "K", DateTime.Now.ToString("yyyyMMdd"), arch.Registros);
                    sw.Close();
                }
                var tiempo = DateTime.Now.Subtract(inicioProceso);
                var archtemp = new Archivos
                {
                    Fecha = DateTime.Now,
                    Archivo = DirectorioSftp + ArchivoPgmCifras,
                    Estatus = "Creado",
                    Tiempo = Convert.ToInt32(tiempo.TotalSeconds),
                    Tipo = "txt",
                    FechaAlta = DateTime.Now,
                    Registros = 1
                };
                using (var context = new SistemasCobranzaEntities())
                {
                    context.Archivos.Add(archtemp);
                    context.SaveChanges();
                }

                var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("ArchivoCreado");
                mensajeServ.Titulo = string.Format(mensajeServ.Titulo, ArchivoPgmCifras);
                mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, ArchivoPgmCifras, 1.ToString(CultureInfo.InvariantCulture), DateTime.Now.ToString(CultureInfo.InvariantCulture));
                NotificacionesEmail(mensajeServ.Titulo, mensajeServ.Mensaje, false, mensajeServ.EsHtml);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                   "ArchCifras_ " + ex.Message);
                
            }

        }

        private static void RUnificadoTxt()
        {
            var inicioProceso = DateTime.Now;
            var archivoRu = GeneraRUnificadoBuilder();
            try
            {
                if (archivoRu.Registros > 0)
                {
                    var sw = new StreamWriter(DirectorioSftp + archivoRu.Nombre);
                    using (sw)
                    {
                        sw.WriteLine(archivoRu.Contenido);
                        sw.Close();
                    }
                    var tiempo = DateTime.Now.Subtract(inicioProceso);
                    var arch = new Archivos
                    {
                        Fecha = DateTime.Now,
                        Archivo = DirectorioSftp + archivoRu.Nombre,
                        Estatus = "Creado",
                        Tiempo = Convert.ToInt32(tiempo.TotalSeconds),
                        Tipo = "txt",
                        FechaAlta = DateTime.Now,
                        Registros = archivoRu.Registros
                    };
                    using (var context = new SistemasCobranzaEntities())
                    {
                        context.Archivos.Add(arch);
                        context.SaveChanges();
                    }

                    var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("ArchivoCreado");
                    mensajeServ.Titulo = string.Format(mensajeServ.Titulo, archivoRu.Nombre);
                    mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, archivoRu.Nombre, archivoRu.Registros.ToString(CultureInfo.InvariantCulture), DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    NotificacionesEmail(mensajeServ.Titulo, mensajeServ.Mensaje, false, mensajeServ.EsHtml);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos", "RUnificadoTxt - CrearArchivo " + e.Message);
            }
        }

        private static void RegistroAsesoresTxt()
        {
            var inicioProceso = DateTime.Now;
            var archivoRa = GeneraRegistroAsesoresBuilder();
            try
            {
                if (archivoRa.Registros > 0)
                {
                    var sw = new StreamWriter(DirectorioSftp + archivoRa.Nombre);
                    using (sw)
                    {
                        sw.WriteLine(archivoRa.Contenido);
                        sw.Close();
                    }
                    var tiempo = DateTime.Now.Subtract(inicioProceso);
                    var arch = new Archivos
                    {
                        Fecha = DateTime.Now,
                        Archivo = DirectorioSftp + archivoRa.Nombre,
                        Estatus = "Creado",
                        Tiempo = Convert.ToInt32(tiempo.TotalSeconds),
                        Tipo = "txt",
                        FechaAlta = DateTime.Now,
                        Registros = archivoRa.Registros
                    };
                    using (var context = new SistemasCobranzaEntities())
                    {
                        context.Archivos.Add(arch);
                        context.SaveChanges();
                    }

                    var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("ArchivoCreado");
                    mensajeServ.Titulo = string.Format(mensajeServ.Titulo, archivoRa.Nombre);
                    mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, archivoRa.Nombre, archivoRa.Registros.ToString(CultureInfo.InvariantCulture), DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    NotificacionesEmail(mensajeServ.Titulo, mensajeServ.Mensaje, false, mensajeServ.EsHtml);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos", "RegistroAsesoresTxt - CrearArchivo " + e.Message);
            }
        }

        private static ArchivoExportado GeneraGestionBuilder(int tipoArchivo)
        {
            ConexionSql cnnSql = ConexionSql.Instance;
            DataSet ds = null;
            StringBuilder cabecero = new StringBuilder();
            StringBuilder registros = new StringBuilder();
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos", " Inicia: GeneraGestionBuilder");
                ds = cnnSql.ObtenerRespuestasGestionadas(0, 1, tipoArchivo);
            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "Exception: GeneraGestionBuilder- ObtenerRespuestasGestionadas " + e.Message);
            }

            var archivoExportado = new ArchivoExportado
            {
                Nombre = ((tipoArchivo == 1) ? ArchivoGestiones
                            : (tipoArchivo == 2) ? ArchivoGestionesP
                                : (tipoArchivo == 3) ? ArchivoCedulaInspeccion : ArchivoGestiones),
                Contenido = null,
                Registros = 0
            };
            int cantidadRegistros = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var camposRespuesta =
                    (from x in new SistemasCobranzaEntities().CamposRespuesta where x.Referencia != "" select x).ToList();

                IEnumerable<string> columnNames =
                    ds.Tables[0].Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                cabecero.Append(string.Join("|", columnNames));
                registros.AppendLine(cabecero.ToString());
                foreach (DataRow res in ds.Tables[0].Rows)
                {
                    var campos = new StringBuilder();
                    var registro = new StringBuilder();
                    cantidadRegistros++;

                    var cred = "";
                    Creditos credito = null;

                    foreach (var columna in ds.Tables[0].Columns)
                    {
                        var campo = ((DataColumn)columna).ColumnName.ToLower();

                        if (campo != "id_carga")
                        {
                            if (campo == "num_cred")
                            {
                                campos.Append("CV_CREDITO");
                                cred = res[campo].ToString();
                                registro.Append(cred);
                                campos.Append("|");
                                registro.Append("|");
                            }
                            else
                            {
                                if (credito == null)
                                {
                                    if (cred != "")
                                    {
                                        credito =
                                            new SistemasCobranzaEntities().Creditos.FirstOrDefault(
                                                x => x.CV_CREDITO == cred);
                                    }
                                }

                                if (credito != null)
                                {

                                    var referencia = camposRespuesta.FirstOrDefault(x => x.Nombre == campo);
                                    if (referencia != null && referencia.Referencia != null)
                                        campo = referencia.Referencia.ToLower();

                                    if (campo != "no_enviar") //encontrado == null
                                    {
                                        if (res[campo] != null)
                                        {
                                            registro.Append(NoEspeciales(res[campo].ToString()));
                                            registro.Append("|");
                                        }
                                    }
                                    else
                                    {
                                        Trace.WriteLine(campo);
                                    }
                                }
                            }
                        }
                    }
                    registros.AppendLine(registro.ToString().Substring(0, registro.ToString().Length - 1));
                }
            }

            if (cantidadRegistros > 0)
            {
                archivoExportado.Registros = cantidadRegistros;
                archivoExportado.Contenido = registros.ToString();
            }

            return archivoExportado;
        }

        private static ArchivoExportado GeneraFotosBuilder()
        {
            var respuesta = new StringBuilder();
            ConexionSql cnnSql = ConexionSql.Instance;
            DataSet ds = null;
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos", "Inicia: GeneraFotosBuilder");
                ds = cnnSql.ObtenerRespuestasGestionadas(0, 1, 6);
            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "Exception:ObtenerRespuestasGestionadas- " + e.Message);
            }

            var listaFotos = new List<Fotos>();
            var archivoExportado = new ArchivoExportado { Nombre = ArchivoFotos, Contenido = null, Registros = 0 };
            int cantidadRegistros = 0;
            try
            {
                if (ds != null)
                    foreach (DataRow res in ds.Tables[0].Rows)
                    {
                        var cred = "";
                        var responseDate = "";
                        var listaFotosTemp = new List<Fotos>();
                        foreach (var columna in ds.Tables[0].Columns)
                        {
                            var campo = ((DataColumn)columna).ColumnName.ToLower();
                            if (campo == "num_cred")
                            {
                                cred = res[campo].ToString();
                            }
                            else if (campo == "responsedate")
                            {
                                responseDate =
                                    DateTime.ParseExact(res[campo].ToString(), "yyyyMMdd HH:mm:ss",
                                        CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
                            }
                            else if (res[campo].ToString().ToLower().Contains(".jpg"))
                            {
                                listaFotosTemp.Add(new Fotos
                                {
                                    Fecha = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                    Credito = cred,
                                    Tipo = campo,
                                    Url = res[campo].ToString()
                                });
                            }
                        }
                        if (responseDate != "")
                        {
                            foreach (Fotos t in listaFotosTemp)
                            {
                                listaFotos.Add(new Fotos
                                {
                                    Fecha = responseDate,
                                    Credito = t.Credito,
                                    Tipo = t.Tipo,
                                    Url = t.Url
                                });
                            }
                        }
                        listaFotosTemp = null;
                    }
                respuesta.AppendLine("Fecha|Credito|Tipo|Url");
                foreach (var lF in listaFotos)
                {
                    cantidadRegistros++;
                    respuesta.AppendLine(lF.Fecha + "|" + lF.Credito + "|" + lF.Tipo + "|" + lF.Url);
                }

            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "GeneraFotosBuilder-Parseo de datos " + e.Message);
            }
            archivoExportado.Registros = cantidadRegistros;
            archivoExportado.Contenido = respuesta.ToString();
            return archivoExportado;
        }

        private static ArchivoExportado GeneraReferenciasBuilder()
        {
            var respuesta = new StringBuilder();
            ConexionSql cnnSql = ConexionSql.Instance;
            DataSet dsCamposReferencias = null;
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos", "Inicia: GeneraReferenciasBuilder");
                dsCamposReferencias = cnnSql.ObtenerCamposReferencias(2, 1);
            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "Exception:ObtenerCamposReferencias- " + e.Message);
            }

            DataSet dsCredito = null;
            var archivoExportado = new ArchivoExportado { Nombre = ArchivoReferencias, Contenido = null, Registros = 0 };
            int cantidadRegistros = 0;
            try
            {
                using (dsCamposReferencias)
                {
                    if (dsCamposReferencias != null)
                    {
                        respuesta.AppendLine(
                            (string.Join("|", dsCamposReferencias.Tables[0].Columns.Cast<DataColumn>().ToList()))
                                .ToUpper().Replace("IDORDEN|", ""));
                        foreach (DataRow cRef in dsCamposReferencias.Tables[0].Rows)
                        {
                            var cred = "";
                            var linea = new StringBuilder();
                            foreach (var columna in dsCamposReferencias.Tables[0].Columns)
                            {
                                var campoRef = ((DataColumn)columna).ColumnName.ToUpper();
                                var campo = campoRef.Replace("_ACT", "");
                                if (campo == "CV_CREDITO")
                                {
                                    cred = cRef[campo].ToString();
                                    dsCredito = cnnSql.ObtenerCredito(cred);
                                    linea.Append(cred);
                                }
                                else if (campo != "IDORDEN")
                                {
                                    linea.Append("|");
                                    var valor = NoEspeciales(cRef[campoRef].ToString());
                                    try
                                    {

                                        linea.Append(dsCredito != null &&
                                                     cRef[campoRef].ToString().Trim().ToUpper() !=
                                                     dsCredito.Tables[0].Rows[0][campo].ToString().Trim().ToUpper()
                                            ? valor
                                            : "");
                                    }
                                    catch (Exception)
                                    {
                                        linea.Append(valor);
                                    }
                                }
                            }
                            if (linea.ToString().Replace(cred, "").Replace("|", "").Trim() != "")
                            {
                                cantidadRegistros++;
                                respuesta.AppendLine(linea.ToString());

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "Exception:using (dsCamposReferencias)- " + e.Message);
            }
            archivoExportado.Registros = cantidadRegistros;
            archivoExportado.Contenido = respuesta.ToString();
            return archivoExportado;
        }

        private static ArchivoExportado GeneraRUnificadoBuilder()
        {
            var cnnSql = ConexionSql.Instance;
            DataSet ds = null;
            var cabecero = new StringBuilder();
            var registros = new StringBuilder();
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos", " Inicia: GeneraRUnificadoBuilder");
                ds = cnnSql.ObtenerReporteUnificado();
            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "Exception: GeneraRUnificadoBuilder- ObtenerReporteUnificado " + e.Message);
            }
            var archivoExportado = new ArchivoExportado { Nombre = ArchivoRUnificado, Contenido = null, Registros = 0 };
            var cantidadRegistros = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                IEnumerable<string> columnNames =
                    ds.Tables[0].Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                cabecero.Append(string.Join("|", columnNames));
                //var CFinal = cabecero.ToString().Substring(0, cabecero.ToString().IndexOf("Procesar", System.StringComparison.Ordinal)); //+"CT_FACTOR_REA_CONV|IM_MONTO_MENSUALIDAD_PESOS|CV_PRODUCTO_CONVENIO|CV_TIPO_CONVENIO|NU_VISITA";
                registros.AppendLine(cabecero.ToString());
                foreach (DataRow res in ds.Tables[0].Rows)
                {
                    var campos = new StringBuilder();
                    var registro = new StringBuilder();
                    cantidadRegistros++;

                    var cred = "";
                    foreach (var columna in ds.Tables[0].Columns)
                    {
                        var campo = ((DataColumn)columna).ColumnName;


                        if (campo == "CV_CREDITO")
                        {
                            campos.Append("CV_CREDITO");
                            cred = res[campo].ToString();
                            registro.Append(cred);
                            campos.Append("|");
                            registro.Append("|");
                        }
                        else
                        {
                            if (cred != "")
                            {
                                if (res[campo] != null)
                                {
                                    registro.Append(NoEspeciales(res[campo].ToString()));
                                    registro.Append("|");
                                }
                            }
                        }
                    }
                    registros.AppendLine(registro.ToString().Substring(0, registro.ToString().Length - 1));
                }

            }
            if (cantidadRegistros > 0)
            {
                archivoExportado.Registros = cantidadRegistros;
                archivoExportado.Contenido = registros.ToString();
            }

            return archivoExportado;
        }

        private static ArchivoExportado GeneraRegistroAsesoresBuilder()
        {
            var cnnSql = ConexionSql.Instance;
            DataSet ds = null;
            var cabecero = new StringBuilder();
            var registros = new StringBuilder();
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos", " Inicia: GeneraRegistroAsesoresBuilder");
                ds = cnnSql.ObtenerReporteRegistroAsesores();
            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "Exception: GeneraRegistroAsesoresBuilder- ObtenerReporteRegistroAsesores " + e.Message);
            }
            var archivoExportado = new ArchivoExportado { Nombre = ArchivoRegistroAsesores, Contenido = null, Registros = 0 };
            var cantidadRegistros = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                IEnumerable<string> columnNames =
                    ds.Tables[0].Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                cabecero.Append(string.Join("|", columnNames));
                registros.AppendLine(cabecero.ToString());
                foreach (DataRow res in ds.Tables[0].Rows)
                {
                    var registro = new StringBuilder();
                    cantidadRegistros++;

                    foreach (var columna in ds.Tables[0].Columns)
                    {
                        var campo = ((DataColumn)columna).ColumnName;

                        if (res[campo] != null)
                        {
                            registro.Append(NoEspeciales(res[campo].ToString()));
                            registro.Append("|");
                        }
                    }
                    registros.AppendLine(registro.ToString());
                }

            }
            if (cantidadRegistros > 0)
            {
                archivoExportado.Registros = cantidadRegistros;
                archivoExportado.Contenido = registros.ToString();
            }

            return archivoExportado;
        }

        private static void InsertaHistoricoDiario()
        {
            try
            {
                var cnnSql = ConexionSql.Instance;
                cnnSql.InsertaHistoricoOrdenes(false);
            }
            catch (Exception e)
            {
                NotificacionesEmail("Error al insertar historicos" + DateTime.Now,
                    e.Message + " inner: " + e.InnerException.Message, true);
            }

        }

        private static string NoEspeciales(string texto)
        {
            string respuesta = texto.Replace("ñ", "?")
                .Replace("á", "a")
                .Replace("é", "e")
                .Replace("í", "i")
                .Replace("ó", "o")
                .Replace("ú", "u")
                .Replace("Á", "A")
                .Replace("É", "E")
                .Replace("Í", "I")
                .Replace("Ó", "O")
                .Replace("Ú", "U");
            return respuesta.ToUpper();
        }

        #endregion TareaEnviarArchivos

        private static void TareaEnviaResumenProcesoRegistroWs()
        {
            int hrsResumenProcesoWs = Convert.ToInt32(ConfigurationManager.AppSettings["hrsResumenProcesoWs"]);
            int msResumenProcesoWs = Convert.ToInt32(ConfigurationManager.AppSettings["msResumenProcesoWs"]);
            string cv_ruta = string.Empty;
            string edo_gestion = string.Empty;
            int totalEnviado = 0;
            int totalmovimientos = 0;
            DateTime fechaCaptura = DateTime.Now;
            string lCuerpoCorreo = string.Empty;

            while (ejecutandose)
            {
                var horaFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hrsResumenProcesoWs, msResumenProcesoWs, 0);
                var hora1 = DateTime.Now;
                var hora2 = DateTime.Now.AddSeconds(20);
                int result = DateTime.Compare(horaFinal, hora1);
                int result2 = DateTime.Compare(horaFinal, hora2);
                if (result > 0 && result2 < 0)
                {
                    try
                    {
                        
                        lCuerpoCorreo += @"<table class=""tablaMensaje"" cellspacing=""1"" border=""1"" align=""center"" style=""width:100%"">";
                        lCuerpoCorreo += @"<tr>
                                                <td class=""celdaFondo"" align=""center"" style=""width:20%"">Ruta</td>
                                                <td class=""celdaFondo"" align=""center"" style=""width:10%"">Estado gestión</td>                                                
                                                <td class=""celdaFondo"" align=""center"" style=""width:20%"">Total enviados</td>
                                                <td class=""celdaFondo"" align=""center"" style=""width:20%"">Total movimientos</td>
                                                <td class=""celdaFondo"" align=""center"" style=""width:30%"">Fecha</td>
                                                
                                            </tr>";

                        var datos = new DataSet();
                        var reporteResumenProcesoWs = new EntRespuestasWS();

                        datos = reporteResumenProcesoWs.GetResumenProcesoWs();

                        if (datos != null && datos.Tables.Count > 0)
                        {
                            DataTable dt = datos.Tables[0];

                            foreach (DataRow row in dt.Rows)
                            {
                                cv_ruta = row["CV_RUTA"].ToString();
                                edo_gestion = row["EDO_GESTION"].ToString();
                                totalEnviado = int.Parse(row["TOTAL_ENVIADO"].ToString());
                                totalmovimientos = int.Parse(row["TOTAL_MOVIMIENTOS"].ToString());
                                fechaCaptura = DateTime.Parse(row["FECHA"].ToString()); ;

                                lCuerpoCorreo += string.Format(@"<tr><td class=""celdaTexto"" align=""left"" style=""width:20%"">{0}</td>
                                                                    <td class=""celdaTexto"" align=""center"" style=""width:20%"">{1}</td>
                                                                    <td class=""celdaTexto"" align=""center"" style=""width:20%"">{2}</td>
                                                                    <td class=""celdaTexto"" align=""center"" style=""width:20%"">{3}</td>
                                                                    <td class=""celdaTexto"" align=""right"" style=""width:20%"">{4}</td><tr>", 
                                                                cv_ruta, edo_gestion, totalEnviado, totalmovimientos, fechaCaptura.ToString("yyyy-MM-dd"));;
                            }

                            lCuerpoCorreo += @"</table>";
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "TareaEnviaResumenProcesoRegistroWs",
                            "Error: " + ex.Message +
                            (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                    }
                    
                    Email.EnviarEmail(direccion, "Registros procesados Web Service", lCuerpoCorreo, true);
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "TareaEnviaResumenProcesoRegistroWs", "OK");
                }
                Thread.Sleep(10000); //Espera 10 segundos
            }
        }

        #region EventHandlers
        // Define the event handlers. 
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("Archivo: " + e.FullPath + " " + e.ChangeType);
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                var tempArchivo = e.FullPath;
                var task = new Task(() => DescomprimeArchivo(tempArchivo, null));
                task.Start();
            }

            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                var tempArchivo = e.FullPath;
                var task = new Task(() => ValidaArchivo(tempArchivo));
                task.Start();
            }
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            Console.WriteLine("Archivo: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }

        private static void OnChangedE(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("Archivo: " + e.FullPath + " " + e.ChangeType);
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                var tempArchivo = e.FullPath;
                var task = new Task(() => ComprimeArchivo(tempArchivo, null));
                task.Start();
            }

            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                var tempArchivo = e.FullPath;
                var task = new Task(() => ValidaArchivoE(tempArchivo));
                task.Start();
            }
        }

        #endregion EventHandlers

        #region VidaProceso

        private static void Procesando()
        {
            lock (Lock)
            {
                _procesando++;
            }
        }

        private static void Terminado()
        {
            lock (Lock)
            {
                _procesando--;
            }
        }

        private static int ContarProcesos()
        {
            int cantidad;
            lock (Lock)
            {
                cantidad = _procesando;
            }
            return cantidad;
        }

        #endregion VidaProceso

        #region Clases

        internal class CampoDato
        {
            public string Campo { get; set; }
            public string Dato { get; set; }
        }

        internal class Fotos
        {
            public string Fecha { get; set; }
            public string Credito { get; set; }
            public string Tipo { get; set; }
            public string Url { get; set; }
        }

        internal class ArchivoExportado
        {
            public string Nombre { get; set; }
            public string Contenido { get; set; }
            public int Registros { get; set; }
        }

        private class MyClient : WebClient
        {
            public bool HeadOnly { get; set; }

            protected override WebRequest GetWebRequest(Uri address)
            {
                var req = base.GetWebRequest(address);
                if (req != null && (HeadOnly && req.Method == "GET"))
                {
                    req.Method = "HEAD";
                }
                return req;
            }
        }

        #endregion Clases

        #region Archivos

        private static void ValidaArchivo(string path)
        {
            Thread.Sleep(5000);
            var context = new SistemasCobranzaEntities();
            var fechaABuscar = DateTime.Now.AddHours(-12);
            var archivos = from a in context.Archivos
                           where a.Archivo == path
                                 && a.FechaAlta > fechaABuscar
                           orderby a.Fecha descending
                           select a;

            if (archivos.Any())
            {
                var archivo = archivos.First();

                if (archivo.Estatus == "Fallo Descomprimir")
                {
                    //Si fallo el archvo o no se pudo descomprimir se vuelve a intentar descomprimir
                    DescomprimeArchivo(path, archivo);
                }
                else
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos",
                        "El archivo: " + archivo.Archivo + " esta en estatus: " + archivo.Estatus);
                }
            }
            else
                DescomprimeArchivo(path, null);
        }

        private static void ValidaArchivoE(string path)
        {
            Thread.Sleep(5000);
            var context = new SistemasCobranzaEntities();
            var fechaABuscar = DateTime.Now.AddHours(-12);
            var archivos = from a in context.Archivos
                           where a.Archivo == path
                                 && a.FechaAlta > fechaABuscar
                           orderby a.Fecha descending
                           select a;

            if (archivos.Any())
            {
                var archivo = archivos.First();

                if (archivo.Estatus == "Fallo Comprimir")
                {
                    //Si fallo el archvo o no se pudo comprimir se vuelve a intentar comprimir
                    ComprimeArchivo(path, archivo);
                }
                else
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos",
                        "El archivo: " + archivo.Archivo + " esta en estatus: " + archivo.Estatus);
                }
            }
            else
                ComprimeArchivo(path, null);
        }

        private static void DescomprimeArchivo(string path, Archivos archivo)
        {
            Procesando();
            var context = new SistemasCobranzaEntities();
            long anterior = -1;
            var sinCambios = false;
            int intentos = 0;

            Archivos arch;
            if (archivo == null)
            {
                arch = new Archivos
                {
                    Fecha = DateTime.Now,
                    Archivo = path,
                    Estatus = "Creado",
                    Tiempo = 0,
                    Tipo = "zip",
                    FechaAlta = DateTime.Now
                };

                arch = context.Archivos.Add(arch);
            }
            else
            {
                arch = archivo;
                arch.Estatus = "Actualizado";
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "DescomprimeArchivo: No se pudo registrar el archivo en la base de datos: " + ex.Message);
                Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                    "DescomprimeArchivo: No se pudo registrar el archivo en la base de datos: " + ex.Message);
                Terminado();
                return;
            }

            while (!sinCambios)
            {
                try
                {
                    // Primero valido por longitud del archivo
                    var f = new FileInfo(path);
                    sinCambios = anterior == f.Length;
                    anterior = f.Length;
                    if (!sinCambios)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos",
                            "Archivo: " + path + " Length: " + Convert.ToInt64(f.Length / 1024) + " Kb");
                        Thread.Sleep(5000);
                    }
                    else
                    {
                        //Valido si tengo acceso al archivo
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos",
                            "Archivo: " + path + " Revisa si esta bloqueado");
                        var file = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                        file.Close();
                    }
                }
                catch (Exception)
                {
                    //intento 20 veces sino lo termino
                    sinCambios = false;
                    Thread.Sleep(5000);
                    intentos++;
                    if (intentos > 20)
                        sinCambios = true;
                }
            }
            if (intentos <= 20) //si no ocurrió un error descomprimo el archivo
            {
                arch.Fecha = DateTime.Now;
                arch.Estatus = "Descomprimiendo";
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                        "DescomprimeArchivo:No se pudo actualizar la información del archivo en la base de datos, se continua procesando el archivo..." +
                        ex.Message);
                    Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                        "DescomprimeArchivo:No se pudo actualizar la nformación del archivo en la base de datos, se continua procesando el archivo..." +
                        ex.Message);
                }
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos", "Archivo: " + path + " Descomprimiendo...");

                try
                {
                    using (ZipFile zip = ZipFile.Read(path))
                    {
                        foreach (ZipEntry f in zip)
                            f.Extract(ConfigurationManager.AppSettings["DirectorioArchivosDescomprimidos"] + "\\",
                                ExtractExistingFileAction.OverwriteSilently);
                    }
                    arch.Estatus = "Descomprimido";
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos",
                        "Archivo: " + path + " extraido correctamente");
                }
                catch (Exception ex)
                {

                    context.ArchivosError.Add(new ArchivosError
                    {
                        id_archivo = arch.id,
                        Error = ex.Message
                    });
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception errEx)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                            "DescomprimeArchivo:No se pudo guardar el registro de errores." + errEx.Message);
                        Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                            "DescomprimeArchivo:No se pudo guardar el registro de errores." + errEx.Message);
                    }

                    arch.Estatus = "Fallo Descomprimir";
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                        "Archivo: Ocurrió un error al intentar extraer el archivo:" + ex.Message);
                }

                var tiempo = DateTime.Now.Subtract(arch.Fecha);
                arch.Tiempo = Convert.ToInt32(tiempo.TotalSeconds);
                arch.Fecha = DateTime.Now;
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                        "DescomprimeArchivo:No se pudo actualizar la información del archivo en la base de datos, se continua procesando el archivo..." +
                        ex.Message);
                    Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                        "DescomprimeArchivo:No se pudo actualizar la información del archivo en la base de datos, se continua procesando el archivo..." +
                        ex.Message);
                }

                MoverArchivoProcesado(arch.Archivo, arch.FechaAlta, "Descomprimido");

                var tarea = new Task(ImportaArchivos);
                tarea.Start();
            }
            else
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "Archivo: " + path + " No se pudo descomprimir");
                arch.Fecha = DateTime.Now;
                arch.Estatus = "Fallo Descomprimir";
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                        "DescomprimeArchivo:No se pudo actualizar la información del archivo en la base de datos, se continua procesando el archivo..." +
                        ex.Message);
                    Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                        "DescomprimeArchivo:No se pudo actualizar la información del archivo en la base de datos, se continua procesando el archivo..." +
                        ex.Message);
                }
            }
            Terminado();
        }

        private static void ComprimeArchivo(string path, Archivos archivo)
        {
            var context = new SistemasCobranzaEntities();
            long anterior = -1;
            var sinCambios = false;
            int intentos = 0;


            Archivos arch;
            if (archivo == null)
            {
                arch = new Archivos
                {
                    Fecha = DateTime.Now,
                    Archivo = path.Replace("TXT", "zip"),
                    Estatus = "Creado",
                    Tiempo = 0,
                    Tipo = "zip",
                    FechaAlta = DateTime.Now
                };

                arch = context.Archivos.Add(arch);
            }
            else
            {
                arch = archivo;
                arch.Estatus = "Actualizado";
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "ComprimeArchivo:No se pudo registrar el archivo en la base de datos: " + ex.Message);
                Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                    "ComprimeArchivo:No se pudo registrar el archivo en la base de datos: " + ex.Message);
                return;
            }

            while (!sinCambios)
            {
                try
                {
                    // Primero valido por longitud del archivo
                    var f = new FileInfo(path);
                    sinCambios = anterior == f.Length;
                    anterior = f.Length;
                    if (!sinCambios)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos",
                            "Archivo: " + path + " Length: " + Convert.ToInt64(f.Length / 1024) + " Kb");
                        Thread.Sleep(5000);
                    }
                    else
                    {
                        //Valido si tengo acceso al archivo
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos",
                            "Archivo: " + path + " Revisa si esta bloqueado");
                        var file = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                        file.Close();
                    }
                }
                catch (Exception)
                {
                    //intento 20 veces sino lo termino
                    sinCambios = false;
                    Thread.Sleep(5000);
                    intentos++;
                    if (intentos > 20)
                        sinCambios = true;
                }
            }
            if (intentos <= 20) //si no ocurrió un error comprimo el archivo
            {
                arch.Fecha = DateTime.Now;
                arch.Estatus = "Comprimiendo";
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                        "ComprimeArchivo:No se pudo actualizar la información del archivo en la base de datos, se continua procesando el archivo..." +
                        ex.Message);
                    Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                        "ComprimeArchivo:No se pudo actualizar la información del archivo en la base de datos, se continua procesando el archivo..." +
                        ex.Message);
                }

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos", "Archivo: " + path + " Comprimiendo...");
                int tipoArchivo = 0;

                if (path.Contains(ArchivoGestiones))
                {
                    tipoArchivo = 1;
                }
                else if (path.Contains(ArchivoGestionesP))
                {
                    tipoArchivo = 2;
                }
                else if (path.Contains(ArchivoCedulaInspeccion))
                {
                    tipoArchivo = 3;
                }
                else if (path.Contains(ArchivoFotos))
                {
                    tipoArchivo = 7;
                }
                else if (path.Contains(ArchivoReferencias))
                {
                    tipoArchivo = 8;
                }
                else if (path.Contains(ArchivoRUnificado))
                {
                    tipoArchivo = 9;
                }

                else if (path.Contains(ArchivoRegistroAsesores))
                {
                    tipoArchivo = 11;
                }
                else if (path.Contains(ArchivoPgmCifras))
                {
                    tipoArchivo = 12;
                }
                


                try
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddFile(path, "");
                        zip.Save(ConfigurationManager.AppSettings["DirectorioArchivosComprimidos"] + "\\" +
                                 ((tipoArchivo == 1)
                                 ? ArchivoGestiones
                                    : (tipoArchivo == 2)
                                    ? ArchivoGestionesP
                                        : (tipoArchivo == 3)
                                        ? ArchivoCedulaInspeccion
                                            : (tipoArchivo == 7)
                                            ? ArchivoFotos
                                                : (tipoArchivo == 8)
                                                ? ArchivoReferencias
                                                    : (tipoArchivo == 9)
                                                    ? ArchivoRUnificado
                                                        : (tipoArchivo == 11)
                                                        ? ArchivoRegistroAsesores
                                                        : (tipoArchivo == 12)
                                                        ? ArchivoPgmCifras
                                                        : "NONE.TXT").Replace("TXT", "zip"));
                    }
                    arch.Estatus = "Comprimido";
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos",
                        "Archivo: " + path + " procesado correctamente");
                }
                catch (Exception ex)
                {
                    context.ArchivosError.Add(new ArchivosError
                    {
                        id_archivo = arch.id,
                        Error = ex.Message
                    });
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception errEx)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                            "ComprimeArchivo:No se pudo guardar el registro de errores." + errEx.Message);
                        Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                            "ComprimeArchivo:No se pudo guardar el registro de errores." + errEx.Message);
                    }

                    arch.Estatus = "Fallo Comprimir";
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                        "Archivo: Ocurrió un error al intentar comprimir el archivo:" + ex.Message);
                }

                var tiempo = DateTime.Now.Subtract(arch.Fecha);
                arch.Tiempo = Convert.ToInt32(tiempo.TotalSeconds);
                arch.Fecha = DateTime.Now;
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                        "ComprimeArchivo:No se pudo actualizar la información del archivo en la base de datos, se continua procesando el archivo..." +
                        ex.Message);
                    Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                        "ComprimeArchivo:No se pudo actualizar la información del archivo en la base de datos, se continua procesando el archivo..." +
                        ex.Message);
                }

                MoverArchivoProcesado(arch.Archivo.Replace("zip", "TXT"), arch.FechaAlta, "Comprimido");

                var tarea = new Task(() => ExportarArchivos(arch));
                tarea.Start();
            }
            else
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos", "Archivo: " + path + " No se pudo Comprimir");
                arch.Fecha = DateTime.Now;
                arch.Estatus = "Fallo Comprimir";
                context.ArchivosError.Add(new ArchivosError
                {
                    id_archivo = arch.id,
                    Error = "Fallo Comprimir sobrepasaron 20 intentos"
                });
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                        "ComprimeArchivo:No se pudo actualizar la información del archivo en la base de datos, se continua procesando el archivo..." +
                        ex.Message);
                }
            }
        }

        private static void ExportarArchivos(Archivos arch)
        {
            var context = new SistemasCobranzaEntities();
            DateTime initialTime;
            var finalTime = DateTime.Now.AddHours(2);
            bool succes;
            string error;
            string nomCortoArchivo;
            do
            {
                initialTime = DateTime.Now;
                error = SubeArchivosSftp(arch.Archivo);
                succes = error == "";
                if (!succes)
                {
                    Thread.Sleep(300000); //Espera 5 minutos    
                }
            } while (!succes && (DateTime.Compare(initialTime, finalTime) < 0));
            try
            {
                var nomCortoArchivoArr = arch.Archivo.Split('\\');
                var tamanioArchivo = nomCortoArchivoArr.Length;
                nomCortoArchivo = nomCortoArchivoArr[tamanioArchivo - 1];
            }
            catch (Exception)
            {
                nomCortoArchivo = arch.Archivo;
            }


            if (!succes)
            {
                context.ArchivosError.Add(new ArchivosError
                {
                    id_archivo = arch.id,
                    Error = error
                });
                try
                {
                    context.SaveChanges();
                    NotificacionesEmail("Error al enviar archivo " + nomCortoArchivo + ", " + DateTime.Now, error, true);
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                        "Exception:ExportarArchivos,Error al enviar archivo-- " + ex.Message);
                    Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                        "Exception:ExportarArchivos,Error al enviar archivo-- " + ex.Message);
                }
            }
            else
            {
                try
                {
                    var arch2 = context.Archivos.FirstOrDefault(x => x.id == arch.id);
                    if (arch2 != null)
                    {
                        arch2.Fecha = DateTime.Now;
                        arch2.Estatus = "Enviado";
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos", "arch2 != null,SaveChanges" + ex.Message);
                    Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                        "arch2 != null,SaveChanges" + ex.Message);
                }

            }

            int tipoArchivo = 0;

            if (arch.Archivo.Contains(ArchivoGestiones.Replace("TXT", "zip")))
            {
                tipoArchivo = 1;
            }
            else if (arch.Archivo.Contains(ArchivoGestionesP.Replace("TXT", "zip")))
            {
                tipoArchivo = 2;
            }
            else if (arch.Archivo.Contains(ArchivoCedulaInspeccion.Replace("TXT", "zip")))
            {
                tipoArchivo = 3;
            }
            else if (arch.Archivo.Contains(ArchivoFotos.Replace("TXT", "zip")))
            {
                tipoArchivo = 7;
            }
            else if (arch.Archivo.Contains(ArchivoReferencias.Replace("TXT", "zip")))
            {
                tipoArchivo = 8;
            }
            else if (arch.Archivo.Contains(ArchivoRUnificado.Replace("TXT", "zip")))
            {
                tipoArchivo = 9;
            }

            else if (arch.Archivo.Contains(ArchivoRegistroAsesores.Replace("TXT", "zip")))
            {
                tipoArchivo = 11;
            }
            else if (arch.Archivo.Contains(ArchivoPgmCifras.Replace("TXT", "zip")))
            {
                tipoArchivo = 12;
            }


            MoverArchivoProcesado(
                ConfigurationManager.AppSettings["DirectorioArchivosComprimidos"] + " \\" +
                ((tipoArchivo == 1)
                  ? ArchivoGestiones
                    : (tipoArchivo == 2)
                    ? ArchivoGestionesP
                        : (tipoArchivo == 3)
                        ? ArchivoCedulaInspeccion
                            : (tipoArchivo == 7)
                            ? ArchivoFotos
                                : (tipoArchivo == 8)
                                ? ArchivoReferencias
                                    : (tipoArchivo == 9)
                                    ? ArchivoRUnificado
                                        : (tipoArchivo == 11) ? ArchivoRegistroAsesores 
                                            : (tipoArchivo == 12) ? ArchivoPgmCifras : "NONE.TXT").Replace("TXT", "zip"),
                arch.Fecha, "Exportados");
        }

        private static void ImportarArchivo(string archivo, Archivos ar)
        {
            Procesando();
            var context = new SistemasCobranzaEntities();
            var ent = new EntArchivos();

            if (ar == null)
            {
                var arch = new Archivos
                {
                    Fecha = DateTime.Now,
                    Archivo = archivo,
                    Estatus = "Procesando",
                    Tiempo = 0,
                    Registros = 0,
                    Tipo = "txt",
                    FechaAlta = DateTime.Now
                };

                ar = context.Archivos.Add(arch);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                        "ImportarArchivo:No se pudo registrar el archivo en la base de datos: " + ex.Message);
                    Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                        "ImportarArchivo:No se pudo registrar el archivo en la base de datos: " + ex.Message);
                    Terminado();
                    return;
                }
            }
            else
            {
                try
                {
                    ar = context.Archivos.FirstOrDefault(x => x.id == ar.id);
                    if (ar != null)
                    {
                        ar.Fecha = DateTime.Now;
                        ar.Estatus = "Procesando";
                        ar.Tiempo = 0;
                        ar.Registros = 0;
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                        "ImportarArchivo:No se pudo registrar el archivo en la base de datos: " + ex.Message);
                    Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                        "ImportarArchivo:No se pudo registrar el archivo en la base de datos: " + ex.Message);
                    Terminado();
                    return;
                }
            }

            if (ar == null)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "ImportarArchivo:No se encontró el archivo o no se pudo generar en la base de datos");
                Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                    "ImportarArchivo:No se encontró el archivo o no se pudo generar en la base de datos");
                Terminado();
                return;
            }

            try
            {
                //Valido si tengo acceso al archivo
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos",
                    "Archivo: " + archivo + " Revisa si esta bloqueado");
                var file = new FileStream(archivo, FileMode.Open, FileAccess.ReadWrite);
                file.Close();

                int totalRegistros = 0;
                var lineasConError = new List<string>();

                using (
                    var sr =
                        new StreamReader(Config.AplicacionActual().Nombre.Contains("SIRA")
                            ? (ProcessArchive.CargarExcel(archivo,
                                ConfigurationManager.AppSettings["DirectorioArchivosDescomprimidos"] + "\\Importados\\"))
                            : (archivo)))
                {
                    int tipoArchivo = 0;

                    if (ar.Archivo.ToUpper().Contains(ConfigurationManager.AppSettings["ArchivoPagos"].ToUpper()))
                    //asignacion 1,complementaria 2, actualizacion 3,reasignaciones 4, pagos 5
                    {
                        tipoArchivo = 5;
                        catalogoEstatusPago = ent.ObtieneEstatusPago();
                    }

                    var header = sr.ReadLine();
                    if (header != null)
                    {
                        header = header.Replace(" ", "");
                        if (Config.AplicacionActual().Nombre.Contains("SIRA"))
                        {
                            header = header.Replace("Rfc", "CV_CREDITO").Replace("RFC", "CV_CREDITO");
                        }
                        var cabecero = header.Split('|');
                        var tipo = header.Split('|');
                        var tamaño = new int[cabecero.GetUpperBound(0) + 1];

                        string campos = "";
                        var tabla = string.Empty;

                        if (tipoArchivo == 5)
                        {
                            tabla = "Pagos";
                        }
                        else
                        {
                            tabla = "Creditos";
                        }

                        //Valida cabecero

                        var camposTabla = context.ObtenerCamposTabla(tabla).ToList();

                        for (var i = 0; i < cabecero.GetUpperBound(0) + 1; i++)
                        {
                            var campo = camposTabla.FirstOrDefault(x => x.Columna == cabecero[i]);

                            if (tipoArchivo == 5)
                            {
                                if (campo != null)
                                {
                                    campos += cabecero[i] + ",";
                                    tamaño[i] = campo.Largo == null ? -1 : campo.Largo.Value;
                                }
                                else
                                {
                                    string error = "El campo " + cabecero[i] +
                                                   " no pertenece al contrato. Se intenta continuar con los demas campos.";
                                    lineasConError.Add("Cabecero" + " <- " + error);
                                    cabecero[i] = "Error";
                                    tipo[i] = "E";
                                    
                                }
                            }
                            else
                            {
                                if (campo != null)
                                {
                                    campos += cabecero[i] + ",";
                                    if (campo.Tipo.ToUpper().IndexOf("CHAR", StringComparison.Ordinal) >= 0)
                                        tipo[i] = "C";
                                    else if (campo.Tipo.ToUpper().IndexOf("DATE", StringComparison.Ordinal) >= 0)
                                        tipo[i] = "D";
                                    else
                                        tipo[i] = "";
                                    tamaño[i] = campo.Largo == null ? -1 : campo.Largo.Value;
                                }
                                else
                                {
                                    string error = "El campo " + cabecero[i] +
                                                   " no pertenece al contrato. Se intenta continuar con los demas campos.";
                                    lineasConError.Add("Cabecero" + " <- " + error);
                                    cabecero[i] = "Error";
                                    tipo[i] = "E";
                                }
                            }
                        }

                        campos = campos.Length > 1 ? campos.Substring(0, campos.Length - 1) : campos;

                        var limiteTareas = Convert.ToInt32(ConfigurationManager.AppSettings["TareasProceso"]);

                        var tareas = new Task<string>[limiteTareas];
                        bool tareasCreadas = false;
                        var cancelaciones = new ConcurrentQueue<int>();
                        while (sr.Peek() > -1)
                        {
                            string line = sr.ReadLine();
                            if (string.IsNullOrEmpty(line))
                                break;

                            totalRegistros++;

                            if (!tareasCreadas)
                            {
                                bool tareaLanzada = false;

                                for (int i = 0; i < tareas.GetUpperBound(0) + 1; i++)
                                {
                                    if (tareas[i] == null)
                                    {
                                        string line1 = line;
                                        if (Config.AplicacionActual().Nombre.Contains("SIRA"))
                                        {
                                            line1 = line1.Length > 5 ? line1.Replace("|yes", "|si") : line1;
                                        }
                                        tareas[i] =
                                            Task<string>.Factory.StartNew(
                                                () =>
                                                    ProcesarLinea(ar.id, line1, campos, cabecero, tipo, tamaño,
                                                        tipoArchivo, cancelaciones));
                                        tareaLanzada = true;
                                        break;
                                    }
                                }

                                if (tareaLanzada)
                                {
                                    continue;
                                }
                                tareasCreadas = true;
                            }

                            // ReSharper disable once CoVariantArrayConversion
                            int indice = Task.WaitAny(tareas);
                            if (tareas[indice].Result != "")
                                lineasConError.Add(tareas[indice].Result);
                            tareas[indice].Dispose();

                            string line2 = line;
                            tareas[indice] =
                                Task<string>.Factory.StartNew(
                                    () =>
                                        ProcesarLinea(ar.id, line2, campos, cabecero, tipo, tamaño, tipoArchivo,
                                            cancelaciones));
                            if (totalRegistros % 1000 == 0)
                            {
                                ar.Registros = totalRegistros;
                                context.SaveChanges();
                            }

                        }

                        if (limiteTareas > totalRegistros)
                        {
                            bool salir;
                            do
                            {
                                salir = true;
                                foreach (var tarea in tareas)
                                {
                                    if (tarea != null)
                                    {
                                        if (tarea.Status == TaskStatus.Running)
                                            salir = false;
                                    }
                                }
                                if (!salir)
                                {
                                    Thread.Sleep(2000); //espera 2 segundos     
                                }

                            } while (!salir);
                        }
                        else
                        {
                            // ReSharper disable once CoVariantArrayConversion
                            Task.WaitAll(tareas);
                        }

                        ar.Registros = totalRegistros;
                        context.SaveChanges();


                        foreach (var tarea in tareas)
                        {
                            if (tarea == null) continue;
                            if (tarea.Result != "")
                                lineasConError.Add(tarea.Result);
                        }
                        var cancelarOrdenList = new List<int>();
                        if (cancelaciones.Count > 0)
                        {
                            int idCancelar;
                            while (cancelaciones.TryDequeue(out idCancelar))
                            {
                                cancelarOrdenList.Add(idCancelar);
                            }
                        }
                    }
                    else
                    {
                        lineasConError.Add("No se encontró el cabecero en la primer línea");
                    }
                }
                string nomCortoArchivo;
                try
                {
                    var nomCortoArchivoArr = ar.Archivo.Split('\\');
                    var tamanioArchivo = nomCortoArchivoArr.Length;
                    nomCortoArchivo = nomCortoArchivoArr[tamanioArchivo - 1];
                }
                catch (Exception)
                {
                    nomCortoArchivo = ar.Archivo;
                }


                if (lineasConError.Count > 0)
                {
                    var sbErrores = new StringBuilder();
                    foreach (var l in lineasConError)
                    {
                        sbErrores.Append(l);
                        sbErrores.Append(Environment.NewLine);
                    }

                    var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("ArchivoProcesado");
                    mensajeServ.Titulo = string.Format(mensajeServ.Titulo, nomCortoArchivo);
                    mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, totalRegistros, lineasConError.Count, nomCortoArchivo, DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    NotificacionesEmail(mensajeServ.Titulo, mensajeServ.Mensaje, false, mensajeServ.EsHtml);

                    context.ArchivosError.Add(new ArchivosError
                    {
                        id_archivo = ar.id,
                        Error = sbErrores.ToString()
                    });
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception errEx)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                            "ImportarArchivo:No se pudo guardar el registro de errores." + errEx.Message);
                        Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                            "ImportarArchivo:No se pudo guardar el registro de errores." + errEx.Message);
                    }
                }
                else
                {
                    var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("ArchivoProcesado");
                    mensajeServ.Titulo = string.Format(mensajeServ.Titulo, nomCortoArchivo);
                    mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, totalRegistros, lineasConError.Count, nomCortoArchivo, DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    NotificacionesEmail(mensajeServ.Titulo, mensajeServ.Mensaje, false, mensajeServ.EsHtml);
                }

                try
                {
                    ar.Tiempo = Convert.ToInt32((DateTime.Now - ar.Fecha).TotalSeconds);
                    ar.Fecha = DateTime.Now;
                    ar.Estatus = "Procesado";
                    ar.Registros = totalRegistros;

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                        "ImportarArchivo:No se pudo actualizar la información del archivo en la base de datos" + ex.Message);
                    Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                        "ImportarArchivo:No se pudo actualizar la información del archivo en la base de datos" + ex.Message);
                }
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos",
                    ar.Archivo + " Se procesaron " + totalRegistros.ToString(CultureInfo.InvariantCulture) +
                    " registros.");
                Console.WriteLine(ar.Archivo + " Se procesaron " + totalRegistros.ToString(CultureInfo.InvariantCulture) +
                                  " registros.");
            }
            catch (Exception errorEnUso)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos", errorEnUso.Message);
                ar.Fecha = DateTime.Now;
                ar.Estatus = "En uso";
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                        "ImportarArchivo:No se pudo actualizar la información del archivo en la base de datos, se continua procesando el archivo..." +
                        ex.Message);
                    Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                        "ImportarArchivo:No se pudo actualizar la información del archivo en la base de datos, se continua procesando el archivo..." +
                        ex.Message);
                }
            }
            MoverArchivoProcesado(ar.Archivo, ar.Fecha, "Importados");
            Terminado();
            TareaProcesarReasignaciones();  /// se manda a ejecutar el proceso de las reasignaciones
        }

        private static string ProcesarLinea(int idArchivo, string line, string campos, string[] cabecero, string[] tipo,
            int[] tamaño, int tipoArchivo, ConcurrentQueue<int> cancelaciones)
        {
            //asignacion 1,complementaria 2, actualizacion 3,reasignaciones 4, pagos 5
            var errorSalida = new StringBuilder();
            string[] linea = line.Split('|');
            string credito = string.Empty;
            string identificadorSucursal = "000";
            var insertaDatos = new EntArchivos();

            if (linea.GetUpperBound(0) != cabecero.GetUpperBound(0))
            //Revisa que la cantidad de campos sea la misma que la que bienen en el cabecero
            {
                return line.Substring(0, 20) +
                       "... <- Error en formato, la cantidad de campos no coincide con el cabecero";
            }

            var values = new StringBuilder();
            for (var i = 0; i < cabecero.GetUpperBound(0) + 1; i++)
            {
                if (cabecero[i] == "Error") continue;
                //Limpia los campos
                if (linea[i].IndexOf('\'') > 0)
                    linea[i] = linea[i].Replace('\'', ' ');
                if (linea[i].IndexOf('\"') > 0)
                    linea[i] = linea[i].Replace('\"', ' ');
                if (linea[i].IndexOf(',') > 0)
                    linea[i] = linea[i].Replace(',', ' ');

                linea[i] = linea[i].Trim();

                if (cabecero[i].ToUpper().Equals("CV_CREDITO"))
                {
                    credito = linea[i];
                }

                if (Config.AplicacionActual().Nombre.Contains("SIRA") && cabecero[i].ToUpper().Equals("IDENTIFICADORSUCURSAL"))
                {
                    identificadorSucursal = linea[i] != null ? linea[i].PadLeft(4, '0') : "0000";
                }
                if (tipo[i] == "C" && linea[i].ToUpper() != "NULL")
                {
                    values.Append("\'");
                    if (linea[i].Length > tamaño[i])
                    {
                        errorSalida.Append("|'" + credito + "' Campo:" + cabecero[i] + " <- " +
                                           "Error: Tamaño campo " + tamaño[i] + " ");
                        values.Append(linea[i].Substring(0, tamaño[i]));
                    }
                    else
                    {
                        if (cabecero[i].ToUpper().Equals("TX_NOMBRE_DESPACHO"))
                        {
                            if (linea[i].Trim() == "")
                            {
                                errorSalida.Append("|'" + credito + "| " + line.Substring(0, 20) + "... <- Campo:" + cabecero[i] +
                                                   " No puede estar vacio.");
                                return errorSalida.ToString();
                            }
                        }
                        values.Append(linea[i]);
                    }

                    values.Append("\'");
                }
                else if (tipo[i] == "D")
                {
                    values.Append("\'");

                    try
                    {
                        if (linea[i] != "NULL")
                        {
                            if (linea[i].IndexOf(" ", StringComparison.Ordinal) >= 0)
                            {
                                errorSalida.Append("|'" + credito + "' <- " + cabecero[i] +
                                                   " - Error: espacios campo fecha.");
                            }
                            else
                            {
                                if (linea[i].IndexOf("/", StringComparison.Ordinal) >= 0)
                                {
                                    IFormatProvider cultura = new CultureInfo("es-MX");
                                    DateTime fecha = DateTime.Parse(linea[i], cultura, DateTimeStyles.AssumeLocal);
                                    values.Append(fecha.ToString("yyyy-MM-dd"));
                                }
                                else if (linea[i].Trim() != "")
                                    values.Append(linea[i].Substring(0, 4) + "-" + linea[i].Substring(4, 2) + "-" +
                                                  linea[i].Substring(6, 2));
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        errorSalida.Append("|'" + credito + "' <- " + cabecero[i] + " - Error:" + err.Message);
                    }

                    values.Append("\'");
                }
                else
                {
                    if (linea[i] == "")
                        linea[i] = "0";
                    values.Append(linea[i]);
                }
                values.Append(",");
            }

            try
            {
                var valores = values.ToString();
                valores = valores.Length > 1 ? valores.Substring(0, valores.Length - 1) : valores;

                var resultado = new DataSet();
                

                if (Config.AplicacionActual().Nombre.Contains("SIRA"))
                {
                    valores = valores.Replace(",'" + credito + "',", ",'" + credito + identificadorSucursal + "',");
                    credito = credito + identificadorSucursal;
                }

                if (tipoArchivo == 5)
                {
                    errorSalida.Append(ValidaRegistrosPagosLondon(idArchivo, credito, valores));
                }
                else
                {
                    resultado = insertaDatos.InsUpdLondon(idArchivo, credito, campos, valores, tipoArchivo);
                    
                    if (resultado.Tables.Count > 0 && resultado.Tables[0].Rows.Count > 0)
                    {
                        string error = "Error: ";
                        if (Convert.ToInt16(resultado.Tables[0].Rows[0]["Codigo"]) != 0)
                        {
                            error += resultado.Tables[0].Rows[0]["Descripcion"];
                        }

                        if (error != "Error: ")
                        {
                            return credito + " <- " + error;
                        }
                    }
                    else
                    {
                        return credito + " <- " + "Error:  La insersion no regreso ningun resultado";
                    }
                }
            }
            catch (Exception ex)
            {
                return credito + " <- Error:  " + ex.Message + "-- Inner " + ex.InnerException;
            }

            return errorSalida.ToString();
        }

        public static string ValidaRegistrosPagosLondon(int idArchivo, string credito, string valores)
        {
            try
            {
                var errorSalida = new StringBuilder();
                string[] linea = valores.Split(',');
                var insertaDatos = new EntArchivos();
                int idResultado = 0;
                int noPago = int.Parse(linea[2]);
                decimal montoPago = decimal.Parse(linea[3]);
                string txPago = linea[4];
                DataSet resultado = new DataSet();

                if (catalogoEstatusPago != null)
                {
                    if (catalogoEstatusPago.Tables.Count > 0 && catalogoEstatusPago.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow res in catalogoEstatusPago.Tables[0].Rows)
                        {
                            try
                            {
                                var Resultado = res["CV_ESTATUS_PAGO"].ToString();

                                if (Resultado.Equals(linea[1].ToUpper()))
                                {
                                    int.TryParse(res["ID_ESTATUS"].ToString(), out idResultado);
                                }

                            }
                            catch
                                (Exception err)
                            {
                                errorSalida.Append(err.Message);
                            }
                        }

                        if (idResultado == 0)
                        {
                            errorSalida.Append("Error: " + credito + ", el estatus del pago es incorrecto --> " +
                                               linea[1] + " <--. ");
                        }

                        if (noPago <= 0)
                        {
                            errorSalida.Append("Error: " + credito + ", el numero de pago no es valido --> " + noPago + " <--. ");
                        }

                        if (montoPago <= 0)
                        {
                            errorSalida.Append("Error: " + credito + ", el monto del pago no es valido -->  $ " + Double.Parse(montoPago.ToString()) + " <--. ");
                        }

                        if (txPago.ToUpper() == "NINGUNO" || txPago == "0")
                        {
                            if (txPago == "0")
                            {
                                errorSalida.Append("Error: " + credito + ", la descripcion del pago no debe ir vacia. ");
                            }
                            else
                            {
                                errorSalida.Append("Error: " + credito + ", la descripcion del pago no es valida --> " + txPago + " <--. ");

                            }
                        }

                        if (idResultado != 0 && noPago > 0 && montoPago > 0 && txPago.ToUpper() != "NINGUNO" && txPago != "0")
                        {

                            resultado = insertaDatos.InsUpdPagosLondon(idArchivo, credito, idResultado, noPago, montoPago, txPago);

                            if (resultado.Tables.Count > 0 && resultado.Tables[0].Rows.Count > 0)
                            {
                                string error = "Error: ";
                                if (Convert.ToInt16(resultado.Tables[0].Rows[0]["Codigo"]) != 0)
                                {
                                    error += resultado.Tables[0].Rows[0]["Descripcion"];
                                }

                                if (error != "Error: ")
                                {
                                    return credito + " <- " + error;
                                }
                            }
                            else
                            {
                                return credito + " <- " + "Error:  La insersion no regreso ningun resultado";
                            }
                        }
                    }
                }

                return errorSalida.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntArchivos", "Error en ValidaRegistrosPagosLondon: " + ex.Message);
                return credito + " <- " + "Error:  La insersion no regreso ningun resultado";
            }
        }

        private static string SubeArchivosSftp(string nombreArchivo)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos", "Subiendo archivos en el sFTP ");
            Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos", "Subiendo archivos en el sFTP " + DateTime.Now);

            string host = ConfigurationManager.AppSettings["sFTPhost"]; // "201.134.132.136";
            string username = ConfigurationManager.AppSettings["sFTPusername"]; // "UserPPMCSI";
            string password = ConfigurationManager.AppSettings["sFTPpassword"]; // "Us3rPPMC$1";
            string remoteDirectory = ConfigurationManager.AppSettings["sFTPremoteDRespuesta"];
            DateTime fechaAlta = DateTime.Now.AddDays(-1);
            string renombrarTxt = "";
            bool renombrar = false;
            // "./PPM/RESPUESTA"; // . always refers to the current directory.

            int tipoArchivo =
                nombreArchivo.Contains(ArchivoGestiones.Replace("TXT", "zip")) ? 1
                    : nombreArchivo.Contains(ArchivoGestionesP.Replace("TXT", "zip")) ? 2
                        : nombreArchivo.Contains(ArchivoCedulaInspeccion.Replace("TXT", "zip")) ? 3
                            : nombreArchivo.Contains(ArchivoFotos.Replace("TXT", "zip")) ? 7
                                : nombreArchivo.Contains(ArchivoReferencias.Replace("TXT", "zip")) ? 8
                                    : nombreArchivo.Contains(ArchivoRUnificado.Replace("TXT", "zip")) ? 9
                                        : nombreArchivo.Contains(ArchivoRegistroAsesores.Replace("TXT", "zip")) ? 11
                                            : nombreArchivo.Contains(ArchivoPgmCifras.Replace("TXT", "zip")) ? 12 : 0;


            string nArchivoZip =
                ((tipoArchivo == 1) ? ArchivoGestiones
                    : (tipoArchivo == 2) ? ArchivoGestionesP
                        : (tipoArchivo == 3) ? ArchivoCedulaInspeccion
                            : (tipoArchivo == 7) ? ArchivoFotos
                                : (tipoArchivo == 8) ? ArchivoReferencias
                                    : (tipoArchivo == 9) ? ArchivoRUnificado
                                        : (tipoArchivo == 11) ? ArchivoRegistroAsesores
                                            : (tipoArchivo == 12) ? ArchivoPgmCifras : "NONE.TXT").Replace("TXT", "zip");

            string response = "";
            try
            {
                using (var sftp = new SftpClient(host, 22, username, password))
                {
                    sftp.Connect();
                    var files = sftp.ListDirectory(remoteDirectory);
                    var archivos = Directory.GetFiles(
                        ConfigurationManager.AppSettings["DirectorioArchivosComprimidos"], nArchivoZip);
                    sftp.ChangeDirectory(remoteDirectory);
                    foreach (var file in files)
                    {
                        if (file.Name == nArchivoZip)
                        {
                            renombrar = true;
                            renombrarTxt = file.Name;
                        }

                    }
                    if (renombrar)
                    {
                        sftp.RenameFile(renombrarTxt,
                            renombrarTxt.Replace(".zip", "") + "-" + fechaAlta.ToString("yyyyMMdd") + ".zip");
                        sftp.Disconnect();
                        Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos", "Info:Se renombra el archivo " + renombrarTxt);
                        return "Info:Se renombra el archivo " + renombrarTxt;
                    }
                    foreach (var archivo in archivos)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos", archivo);

                        if (archivo.Contains(nArchivoZip))
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos", "Subiendo  archivo: " + archivo);

                            try
                            {
                                using (var fileStream = new FileStream(archivo, FileMode.Open))
                                {
                                    sftp.BufferSize = 4 * 1024; // bypass Payload error large files
                                    sftp.UploadFile(fileStream, Path.GetFileName(archivo), true);
                                }
                            }
                            catch (Exception ex)
                            {

                                Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos", "Eror de transferencia de archivo: " + archivo + " Error:" + ex.Message);
                                throw;
                            }

                            Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos", "Tranferencia exitosa de archivo: " + archivo);
                        }
                    }
                    sftp.Disconnect();
                    sftp.Connect();
                    bool enviado = false;
                    files = sftp.ListDirectory(remoteDirectory);
                    foreach (var file in files)
                    {
                        if (file.Name == nArchivoZip)
                            enviado = true;
                    }

                    if (!enviado)
                        response += "Error:No se envió el archivo " + nombreArchivo + " a " + remoteDirectory;
                    sftp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                response += "Error en envio : " + ex.Message;
            }
            Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos", response);
            return response;
        }

        #endregion Archivos

        #region NotificacionesEmail

        private static void NotificacionesEmail(string subject, string body, bool privado, bool esHtml = false)
        {
            try
            {
                var entUsuario = new EntUsuario();
                var usuarios = (privado) ? entUsuario.ObtenerUsuariosPorDominio("1") : entUsuario.ObtenerUsuariosPorDominio("1,2").Where(x => x.IdRol == 1 || x.IdRol == 0).ToList();
                var direccion = usuarios.GroupBy(usr => usr.Email).Select(usuario => usuario.Key).ToList();
                Email.EnviarEmail(direccion, subject, body, esHtml);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Tareas", "NotificacionesEmail - " + ex.Message);
            }
        }

        #endregion NotificacionesEmail

    } //fin class
}

