using System;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntSMS
    {
        /// <summary>
        /// Inserta en tabla AutorizacionSMS las ordenes y teléfonos para el envío de SMS
        /// </summary>
        /// <returns> </returns>
        public void InsertaAutorizaciones()
        {
            try
            {
                var sql = ConexionSql.Instance;
                var cnn = sql.IniciaConexion();
                var sc = new SqlCommand("InsertaAutorizaciones", cnn) { CommandType = CommandType.StoredProcedure };
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                sql.CierraConexion(cnn);
            }                           
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "InsertaAutorizaciones",
                   " error:" + ex.Message);
            }                     
        }

        public DataSet ObtenerTelefonosEnvioSMS()
        {
            var sql = ConexionSql.Instance;
            var cnn = sql.IniciaConexion();
            var sc = new SqlCommand("ObtenerTelefonosEnvioSMS", cnn) {CommandType = CommandType.StoredProcedure};
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            sql.CierraConexion(cnn);
            return ds;
        }
        /// <summary>
        /// Actualiza el registro de envio del mensaje de SMS
        /// </summary>
        /// <param name="idOrden">idOrden para actualizar registro</param>
        /// <param name="logId">respuesta del proveedor del servicio de envio de SMS</param>
        public void ActualizacionEnvioSMS(int idOrden, int logId)
        {

            var sql = ConexionSql.Instance;
            var cnn = sql.IniciaConexion();
            var sc = new SqlCommand("ActualizacionEnvioSMS", cnn) {CommandType = CommandType.StoredProcedure};
            var parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@idOrden", SqlDbType.Int) {Value = idOrden};
            parametros[1] = new SqlParameter("@LogId", SqlDbType.Int) {Value = logId};
            sc.Parameters.AddRange(parametros);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            try
            {
                sda.Fill(ds);
                sql.CierraConexion(cnn);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntSMS", "ActualizacionEnvioSMS: " + ex.Message);
                sql.CierraConexion(cnn);
            }
        }
        /// <summary>
        /// Borra de tabla AutorizacionSMS el teléfono asociado y manda el registro  a tabla de BitacoraAutorizacionSMS
        /// </summary>
        /// <param name="credito">Crédito para borrar registro de numero</param>
        /// <returns>entero 1 si el proceso se realizo exitosamente</returns>
        public int InsertaBitacoraAutorizaciones(string credito)
        {
            SqlConnection cnn = null;
            var ds = new DataSet();
            try
            {
                cnn = ConexionSql.Instance.IniciaConexion();
                var sc = new SqlCommand("InsertaBitacoraAutorizaciones", cnn) { CommandType = CommandType.StoredProcedure };
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@Credito", SqlDbType.VarChar, 15) { Value = credito };
                sc.Parameters.AddRange(parametros);
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
                ConexionSql.Instance.CierraConexion(cnn);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["exito"]) < 0)
                    {
                        return -1;
                    }
                }

            }
            catch (Exception ex)
            {
                if (cnn != null)
                {
                    if (cnn.State != ConnectionState.Closed)
                    {
                        ConexionSql.Instance.CierraConexion(cnn);
                    }
                }
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntSMS", "InsertaBitacoraAutorizaciones:" + ex.Message);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// Obtiene un DataSet con los SMS que no fueron procesados
        /// </summary>
        /// <returns>Regresa un DataSet con los SMS que no fueron procesados</returns>
        public DataSet ObtenerSMSNoProcesados()
        {
            //Utilizo la base de datos de broxelSMS
            var cnn = ConnectionDB.Instancia.IniciaConexion("BroxelSMSs");
            var sc = new SqlCommand("ObtenerSMSPorProcesar", cnn) {CommandType = CommandType.StoredProcedure};
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            ConnectionDB.Instancia.CierraConexion(cnn);
            return ds;
        }

        /// <summary>
        /// Actualiza a procesados los telefonos o id 
        /// </summary>
        /// <param name="id">Si es -1 se actualizan todos los telefonos dados</param>
        /// <param name="telefono">Número de teléfono a actualizar</param>
        /// <returns>Cantidad de registros afectados</returns>
        public int ActualizaTelefonosProcesados(int id, string telefono)
        {
            //Utilizo la base de datos de broxelSMS
            var cnn = ConnectionDB.Instancia.IniciaConexion("BroxelSMSs");
            var sc = new SqlCommand("ActualizaTelefonosProcesados", cnn) {CommandType = CommandType.StoredProcedure};
            var parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@id", SqlDbType.Int) {Value = id};
            parametros[1] = new SqlParameter("@telefono", SqlDbType.Char, 10) {Value = telefono};
            sc.Parameters.AddRange(parametros);
            return sc.ExecuteNonQuery();
        }

        /// <summary>
        /// Valida la autorización sms
        /// </summary>
        /// <param name="telefono">Teléfono al que se quiere validar</param>
        /// <param name="autorizacion">Clave de la autorizacion proporcionada por SMS</param>
        /// <returns>Regresa un dataset con un registro de tipo Resultado/Mensaje</returns>
        public DataSet ValidaAutorizacionSMS(string telefono, string autorizacion)
        {
            SqlConnection cnn = null;
            var ds = new DataSet();
            try
            {
                cnn = ConnectionDB.Instancia.IniciaConexion("SqlDefault");
                var sc = new SqlCommand("ValidaAutorizacionSMS", cnn) { CommandType = CommandType.StoredProcedure };
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@telefono", SqlDbType.Char, 10) {Value = telefono};
                parametros[1] = new SqlParameter("@autorizacion", SqlDbType.Char, 8) {Value = autorizacion};
                sc.Parameters.AddRange(parametros);
                var sda = new SqlDataAdapter(sc);

                sda.Fill(ds);
                ConnectionDB.Instancia.CierraConexion(cnn);
                return ds;
            }

            catch (Exception ex)
            {
                if (cnn != null)
                {
                    if (cnn.State != ConnectionState.Closed)
                    {
                        ConnectionDB.Instancia.CierraConexion(cnn);        
                    }
                }
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ValidaAutorizacionSMS",
                    "telefono: " + telefono + "- autorizacion: " + autorizacion + " error:" + ex.Message);
            }
            return ds;
        }

        /// <summary>
        /// Rescupera la respuesta almecenada en Bitacora
        /// </summary>
        /// <param name="idOrden">Orden de la cual se restauran las respuestas</param>
        /// <param name="autorizar">bandera que indica si se autorizara la gestion despues de recuperar las respuestas</param>

        public void RestaurarRespuesta(int idOrden, int autorizar)
        {

            SqlConnection cnn=null;
            try
            {
                cnn = ConnectionDB.Instancia.IniciaConexion("SqlDefault");
                var sc = new SqlCommand("AutorizaSMS", cnn) {CommandType = CommandType.StoredProcedure};
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@idOrden", SqlDbType.Int) {Value = idOrden};
                parametros[1] = new SqlParameter("@Autorizar", SqlDbType.Int) {Value = autorizar};
                sc.Parameters.AddRange(parametros);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                ConnectionDB.Instancia.CierraConexion(cnn);
            }
            catch (Exception ex)
            {
                if (cnn != null)
                {
                    if (cnn.State != ConnectionState.Closed)
                    {
                        ConnectionDB.Instancia.CierraConexion(cnn);
                    }
                }
                
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "RestaurarRespuesta",
                    "idOrden: " + idOrden + "- Autorizar: " + autorizar + " error:" + ex.Message);
            }

        }

        /// <summary>
        /// Otiene el telefono que esta relacionado a una orden en los envios de SMS
        /// </summary>
        /// <param name="idOrden">orden a buscar</param>
        /// <returns>Telefono</returns>
        public string ObtenerTelefonoXOrden(int idOrden)
        {
            SqlConnection cnn = null;
            var ds = new DataSet();
            try
            {
                cnn = ConnectionDB.Instancia.IniciaConexion("SqlDefault");
                var sc = new SqlCommand("ObtenerTelefonoXOrden", cnn) { CommandType = CommandType.StoredProcedure };
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@idOrden", SqlDbType.Int) { Value = idOrden };
                sc.Parameters.AddRange(parametros);
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
                ConnectionDB.Instancia.CierraConexion(cnn);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["Telefono"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (cnn != null)
                {
                    if (cnn.State != ConnectionState.Closed)
                    {
                        ConnectionDB.Instancia.CierraConexion(cnn);
                    }
                }
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntSMS", " ObtenerTelefonoXOrden:" + ex.Message);
                return String.Empty;
            }
            return String.Empty;
        }

        /// <summary>
        /// Valida si un numero de telefono ya esta procesado para un envio de SMS
        /// </summary>
        /// <param name="telefono">Telefono a valida</param>
        /// /// <param name="credito">Credito perteneciente al numero</param>
        /// <returns>booleano si este se puede utilizar</returns>
        public bool ValidarTelefonoSMS(string telefono,string credito)
        {
            SqlConnection cnn = null;

            try
            {
                cnn = ConnectionDB.Instancia.IniciaConexion("SqlDefault");
                var sc = new SqlCommand("ValidarTelefonoSMS", cnn) { CommandType = CommandType.StoredProcedure };
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@Telefono", SqlDbType.Char, 10) { Value = telefono };
                parametros[1] = new SqlParameter("@Credito", SqlDbType.VarChar, 15) { Value = credito };
                sc.Parameters.AddRange(parametros);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                ConnectionDB.Instancia.CierraConexion(cnn);

                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["invalido"].ToString() == "0";
                }
            }
            catch (Exception ex)
            {
                if (cnn != null)
                {
                    if (cnn.State != ConnectionState.Closed)
                    {
                        ConnectionDB.Instancia.CierraConexion(cnn);
                    }
                }
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntSMS", " ValidarTelefonoSMS:" + ex.Message);
            }
            return true;
        }


        public bool ValidaEnvioDeSMS(string telefono)
        {
            SqlConnection cnn = null;

            try
            {
                cnn = ConnectionDB.Instancia.IniciaConexion("SqlDefault");
                var sc = new SqlCommand("ValidarOrdenEnvioSMS", cnn) { CommandType = CommandType.StoredProcedure };
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@Telefono", SqlDbType.Char, 10) { Value = telefono };
                sc.Parameters.AddRange(parametros);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                ConnectionDB.Instancia.CierraConexion(cnn);

                if (ds.Tables.Count > 0)
                    return ds.Tables[0].Rows[0]["enviar"].ToString() == "1";
                
                return false;
            }
            catch (Exception ex)
            {
                if (cnn != null)
                {
                    if (cnn.State != ConnectionState.Closed)
                    {
                        ConnectionDB.Instancia.CierraConexion(cnn);
                    }
                }
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntSMS", " ValidaEnvioDeSMS:" + ex.Message);
                return false;
            }
        }
    }
}
 
