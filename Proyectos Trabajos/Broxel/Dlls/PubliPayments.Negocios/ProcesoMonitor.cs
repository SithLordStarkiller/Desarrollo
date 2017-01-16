using System;
using System.Configuration;
using System.Data;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios
{
    public class ProcesoMonitor
    {
        EntMonitoreo monitor = new EntMonitoreo();
        /// <summary>
        /// Revisa que la base de datos esté disponible.
        /// </summary>
        /// <returns>DataSet con Ok </returns>
        public string MonitoreoBaseDatos()
        {
            try
            {
                var mensaje = string.Empty;
                var valor = new DataSet();
                valor = monitor.MonitoreoBaseDatos();
                if (valor.Tables.Count > 0 && valor.Tables[0].Rows.Count > 0)
                {
                    mensaje = valor.Tables[0].Rows[0]["OK"].ToString();
                }
                else
                {
                    mensaje = "Sin conexión";
                }

                return mensaje;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ProcesoMonitor", "Error en MonitoreoBaseDatos: " + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Regresa los minutos del ultimo envio en la tabla BitacoraEnvio.
        /// </summary>
        /// <returns></returns>
        public string MonitoreoBaseDatosUltima()
        {
            try
            {
                var mensaje = string.Empty;
                var valor = new DataSet();
                valor = monitor.MonitoreoBaseDatosUltima();
                if (valor.Tables.Count > 0 && valor.Tables[0].Rows.Count > 0)
                {
                    mensaje = "W:Tiempo elevado: " + valor.Tables[0].Rows[0]["Valor"].ToString();
                }
                else
                {
                    mensaje = "OK";
                }

                return mensaje;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ProcesoMonitor", "Error en MonitoreoBaseDatosUltima: " + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Regresa los minutos de la ultima fecha de recepcion en la tabla Ordenes.
        /// </summary>
        /// <returns></returns>
        public string MonitoreoBaseDatosRecibida()
        {
            try
            {
                var mensaje = string.Empty;
                var valor = new DataSet();
                valor = monitor.MonitoreoBaseDatosRecibida();
                if (valor.Tables.Count > 0 && valor.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(valor.Tables[0].Rows[0]["Valor"]) > 300)
                        mensaje = "W:Tiempo elevado: " + valor.Tables[0].Rows[0]["Valor"];
                    else
                        mensaje = "OK";
                }
                else
                    mensaje = "OK";

                return mensaje;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ProcesoMonitor", "Error en MonitoreoBaseDatosRecibida: " + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Ejecuta monitoreos de operacion, monitoreo de ultima act gestion movil.
        /// </summary>
        /// <returns></returns>
        public string MonitoreoUltimaActGestionMovil()
        {
            try
            {
                var mensaje = string.Empty;
                var valor = new DataSet();
                valor = monitor.MonitoreoUltimaActGestionMovil();
                if (valor.Tables.Count > 0 && valor.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(valor.Tables[0].Rows[0]["Valor"]) > 120)
                        mensaje = "W:Tiempo elevado: " + valor.Tables[0].Rows[0]["Valor"];
                    else
                        mensaje = "OK";
                }
                else
                    mensaje = "OK";

                return mensaje;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ProcesoMonitor", "Error en MonitoreoUltimaActGestionMovil: " + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Ejecuta monitoreos de operacion, monitoreo sincronizando.
        /// </summary>
        /// <returns></returns>
        public string MonitoreoSincronizando()
        {
            try
            {
                var porcError = Convert.ToInt32(ConfigurationManager.AppSettings["porcErrorSincronizando"]);
                var porcWarn = Convert.ToInt32(ConfigurationManager.AppSettings["porcWarnSincronizando"]);
                var mensaje = string.Empty;
                var valor = new DataSet();
                valor = monitor.MonitoreoSincronizando();
                if (valor.Tables.Count > 0 && valor.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(valor.Tables[0].Rows[0]["Valor"]) >= ((Convert.ToInt32(valor.Tables[0].Rows[0]["Cuenta"]) * porcError) / 100))
                        mensaje = "Fallo:Número elevado: " + valor.Tables[0].Rows[0]["Valor"];
                    else
                    {
                        if (Convert.ToInt32(valor.Tables[0].Rows[0]["Valor"]) >= ((Convert.ToInt32(valor.Tables[0].Rows[0]["Cuenta"]) * porcWarn) / 100))
                        {
                            mensaje = "W:Número elevado: " + valor.Tables[0].Rows[0]["Valor"];
                        }
                        else
                        {
                            mensaje = "OK";
                        }
                    }

                }
                else
                    mensaje = "OK";
                return mensaje;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ProcesoMonitor", "Error en MonitoreoSincronizando: " + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Ejecutars the stored procedure.
        /// </summary>
        /// <param name="sp">The sp.</param>
        /// <returns></returns>
        public int EjecutarStoredProcedure(string sp)
        {
            try
            {
                var monitor = new EntMonitoreo();
                var result = monitor.EjecutarStoredProcedure(sp);
                return result;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ProcesoMonitor", "Error en EjecutarStoredProcedure: " + ex.Message);
                return 0;
            }

        }
    }
}
