/*
 * Las clases de utiles que utilicen entidad se tienen que colocar en esta misma dll para evitar un referencia ciclica
 */

using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace PubliPayments.Utiles
{
    public sealed class EntTraceLog
    {
        public static string ConnectionString = ""; // Se llena en inicializa

        public bool GuardaTraceLog(int idUsuario, int tipoTraceLog, string origen, string texto)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                try
                {
                    cnn.Open();
                    var sc = new SqlCommand("InsTraceLog", cnn);
                    var parametros = new SqlParameter[4];
                    parametros[0] = new SqlParameter("@idUsuario", SqlDbType.Int) {Value = idUsuario};
                    parametros[1] = new SqlParameter("@idTipoLog", SqlDbType.Int) {Value = tipoTraceLog};
                    parametros[2] = new SqlParameter("@Origen", SqlDbType.NVarChar, 250) {Value = origen};
                    parametros[3] = new SqlParameter("@Texto", SqlDbType.NVarChar, 500) {Value = texto};
                    sc.Parameters.AddRange(parametros);
                    sc.CommandType = CommandType.StoredProcedure;
                    var insert = sc.ExecuteNonQuery();
                    cnn.Close();
                    if (insert != 1)
                    {
                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " - " + texto +
                                        " - Error al insertar el registro");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " - " + texto +
                                    " - Error al insertar el registro: " + ex.Message);
                    return false;
                }
                finally
                {
                    cnn.Close();
                }
            }
            return true;
        }
    }
}

