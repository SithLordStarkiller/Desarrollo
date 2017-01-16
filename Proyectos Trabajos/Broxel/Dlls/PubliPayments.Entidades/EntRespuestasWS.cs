using System;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntRespuestasWS
    {
        /// <summary>
        /// Obtiene las respuestas con la numeración necesaria para el envio al ws del cliente
        /// </summary>
        /// <param name="idOrden">Número de orden de la cual se quieren obtener las respuestas</param>
        /// <param name="ruta">Se requiere la ruta para realizar más rápido la consulta</param>
        /// <returns>Regresa un dataset que contiene los registros en formato "[id]|[valor]"</returns>
        public DataSet ObtenerRespuestasWS(int idOrden, string ruta)
        {
            try
            {
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@idOrden", SqlDbType.Int) {Value = idOrden};
                parametros[1] = new SqlParameter("@Ruta", SqlDbType.VarChar, 10) {Value = ruta};

                return ConnectionDB.Instancia.EjecutarDataSet("SqlDefault", "ObtenerRespuestasWS", parametros);
            }
            catch (Exception ex)
            {
                
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntRespuestasWS",
                    "Error en ObtenerRespuestasWS: " + ex.Message);
                return new DataSet();
            }
        }

        /// <summary>
        /// Obtiene datos de la gestion por medio del id de la orden
        /// </summary>
        /// <param name="idOrden">Id de la orden</param>
        /// <returns>DataSet con los datos de la gestion</returns>
        public DataSet ObtenerGestion(String idOrden)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@IDORDEN", SqlDbType.Int) {Value = idOrden};
                return ConnectionDB.Instancia.EjecutarDataSet("SqlDefault", "ObtenerGestiones", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntRespuestasWS",
                    "Error en ObtenerGestiones: " + ex.Message);
                return new DataSet();
            }
        }

        /// <summary>
        /// Selecciona los datos de error para procesar nuevamente en el ws
        /// </summary>
        /// <param name="idOrden">Id de la orden</param>
        /// <param name="idVisita">Id de la visita</param>
        /// <param name="origenEdoGestion">Estado de la gestion procesada</param>
        /// <returns>DataSet con la informacion de la orden</returns>
        public DataSet GetWsDatosErrorEnvio(int idOrden, int idVisita, string origenEdoGestion)
        {
            try
            {
                var parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("@ID_ORDEN", SqlDbType.Int) { Value = idOrden };
                parametros[1] = new SqlParameter("@ID_VISITA", SqlDbType.Int) { Value = idVisita };
                parametros[2] = new SqlParameter("@CV_ORIGEN", SqlDbType.VarChar, 3) { Value = origenEdoGestion };

                return ConnectionDB.Instancia.EjecutarDataSet("SqlDefault", "GetWSDatosErrorEnvio", parametros);
              
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntRespuestasWS",
                    "Error en GetWSDatosErrorEnvio: " + ex.Message);
                return new DataSet();
            }
        }

        /// <summary>
        /// Inserta/Actualiza los registros procesdos en el WS
        ///	Opcion 0: Error al procesar registro
        ///	Opcion 1: Respuesta correcta del ws
        /// </summary>
        /// <param name="opcion">Numero de opcion</param>
        /// <param name="idOrden">Id de la orden</param>
        /// <param name="idVisita">Id de la visita</param>
        /// <param name="numCredito">Numero de credito</param>
        /// <param name="idRuta">Ruta</param>
        /// <param name="fechaCaptura">Fecha de captura</param>
        /// <param name="despacho">Proveedor</param>
        /// <param name="usser">Despacho</param>
        /// <param name="alterUssr">Usuario de la aplicacion</param>
        /// <param name="estado">Estado de la gestion</param>
        /// <param name="movil">Dictamen</param>
        /// <param name="numMovimientos">Numero de respuestas de la gestion</param>
        /// <param name="aEnviar">Cadena con los registros</param>
        /// <param name="error">Error</param>
        /// <returns></returns>
        public DataSet InsUpdWsEnvio(int opcion, int idOrden, int idVisita, string numCredito, string idRuta, DateTime fechaCaptura, string despacho,
            string usser, string alterUssr, string estado, string movil, int numMovimientos, string aEnviar, string error)
        {
            try
            {
                var parametros = new SqlParameter[14];
                parametros[0] = new SqlParameter("@OPCION", SqlDbType.Int) { Value = opcion };
                parametros[1] = new SqlParameter("@ID_ORDEN", SqlDbType.Int) { Value = idOrden };
                parametros[2] = new SqlParameter("@ID_VISITA", SqlDbType.Int) { Value = idVisita };
                parametros[3] = new SqlParameter("@CV_CREDITO", SqlDbType.VarChar, 15) { Value = numCredito };
                parametros[4] = new SqlParameter("@FECHA_CAPTURA", SqlDbType.Date) { Value = fechaCaptura };
                parametros[5] = new SqlParameter("@CV_RUTA", SqlDbType.VarChar, 10) { Value = idRuta };
                parametros[6] = new SqlParameter("@CV_DESPACHO", SqlDbType.VarChar, 10) { Value = despacho };
                parametros[7] = new SqlParameter("@CV_USER", SqlDbType.NVarChar, 100) { Value = usser };
                parametros[8] = new SqlParameter("@CV_ALTER_USER", SqlDbType.VarChar, 50) { Value = alterUssr };
                parametros[9] = new SqlParameter("@CV_ORIGEN", SqlDbType.VarChar, 3) { Value = estado };
                parametros[10] = new SqlParameter("@CV_MOVIL", SqlDbType.VarChar, 50) { Value = movil };
                parametros[11] = new SqlParameter("@NUM_MOV", SqlDbType.Int) { Value = numMovimientos };
                parametros[12] = new SqlParameter("@CV_REGISTRO", SqlDbType.VarChar, 8000) { Value = aEnviar };
                parametros[13] = new SqlParameter("@ERROR", SqlDbType.VarChar, 1000) { Value = error };

                return ConnectionDB.Instancia.EjecutarDataSet("SqlDefault", "InsUpdWSEnvio", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntRespuestasWS",
                    "Error en InsUpdWSEnvio: numCredito_" + numCredito +" " + ex.Message);
                return new DataSet();
            }
        }


        /// <summary>
        /// Obtiene el resumen de los registros procesados en el ws, por día
        /// </summary>
        /// <returns>DataSet con la información</returns>
        public DataSet GetResumenProcesoWs()
        {
            try
            {
                return ConnectionDB.Instancia.EjecutarDataSet("SqlDefault", "GetResumenProcesoWS");
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntRespuestasWS",
                    "Error en GetResumenProcesoWS: " + ex.Message);
                return new DataSet();
            }
        }
    }
}
