using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntMonitoreo
    {
        private int _idAplicacion; 
        public EntMonitoreo(int idAplicacion )
        {
            _idAplicacion = idAplicacion;
        }

        public EntMonitoreo()
        {
            
        }

        public List<Monitoreo> ObtenerMonitoreos()
        {
            using (var context = new SistemasCobranzaEntities())
            {
                var monitoreo = from m in context.Monitoreo
                    where m.idAplicacion == _idAplicacion
                    select m;

                return monitoreo.ToList();
            }
        }

        /// <summary>
        /// Revisa que la base de datos esté disponible.
        /// </summary>
        /// <returns>DataSet con Ok </returns>
        public DataSet MonitoreoBaseDatos()
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[0];
                return instancia.EjecutarDataSet("SqlDefault", "MonitoreoBaseDatos", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntMonitoreo", "Error en MonitoreoBaseDatos: " + ex.Message);
                return new DataSet();
            }
        }

        /// <summary>
        /// Regresa los minutos del ultimo envio en la tabla BitacoraEnvio.
        /// </summary>
        /// <returns>DataSet con Ok </returns>
        public DataSet MonitoreoBaseDatosUltima()
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[0];
                return instancia.EjecutarDataSet("SqlDefault", "MonitoreoBaseDatosUltima", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntMonitoreo", "Error en MonitoreoBaseDatosUltima: " + ex.Message);
                return new DataSet();
            }
        }

        /// <summary>
        /// Regresa los minutos de la ultima fecha de recepcion en la tabla Ordenes.
        /// </summary>
        /// <returns></returns>
        public DataSet MonitoreoBaseDatosRecibida()
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[0];
                return instancia.EjecutarDataSet("SqlDefault", "MonitoreoBaseDatosRecibida", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntMonitoreo", "Error en MonitoreoBaseDatosRecibida: " + ex.Message);
                return new DataSet();
            }
        }

        /// <summary>
        /// Ejecuta monitoreos de operacion, monitoreo de ultima act gestion movil.
        /// </summary>
        /// <returns></returns>
        public DataSet MonitoreoUltimaActGestionMovil()
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@TipoMonitoreo", SqlDbType.VarChar) { Value = "UltimaActGestionMovil" };

                return instancia.EjecutarDataSet("SqlDefault", "MonitoreoOperacion", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntMonitoreo", "Error en MonitoreoUltimaActGestionMovil: " + ex.Message);
                return new DataSet();
            }
        }

        /// <summary>
        /// Ejecuta monitoreos de operacion, monitoreo sincronizando.
        /// </summary>
        /// <returns></returns>
        public DataSet MonitoreoSincronizando()
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@TipoMonitoreo", SqlDbType.VarChar) { Value = "NumeroSincronizando" };

                return instancia.EjecutarDataSet("SqlDefault", "MonitoreoOperacion", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntMonitoreo", "Error en MonitoreoSincronizando: " + ex.Message);
                return new DataSet();
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
                var instancia = ConnectionDB.Instancia;
                var resultado = instancia.EjecutarEscalar("SqlDefault", sp);
                var resulta = Convert.ToInt32(resultado);
                return resulta;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntMonitoreo", "Error en EjecutarStoredProcedure: " + ex.Message);
                return 0;
            }
        }
    }
}
