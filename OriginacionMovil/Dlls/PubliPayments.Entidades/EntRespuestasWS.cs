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
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var ds = new DataSet();
            try
            {
                var sc = new SqlCommand("ObtenerRespuestasWS", cnn);
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@idOrden", SqlDbType.Int) {Value = idOrden};
                parametros[1] = new SqlParameter("@Ruta", SqlDbType.VarChar, 10) {Value = ruta};
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ObtenerRespuestasWS", ex.Message);
            }
            instancia.CierraConexion(cnn);
            return ds;
        }
    }
}
