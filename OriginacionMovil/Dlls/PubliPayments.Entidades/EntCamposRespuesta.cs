using System;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntCamposRespuesta
    {
        /// <summary>
        /// Obtiene los campos respuesta
        /// </summary>
        /// <returns></returns>
        public DataSet ObtenerCamposRespuestaEtiqueta()
        {
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntCamposRespuesta", "ObtenerCamposRespuestaEtiqueta");

                var sc = new SqlCommand("ObtenerCamposRespuestaEtiqueta", cnn);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);
                return ds;
            }
            catch (Exception ex)
            {
                if(cnn != null) cnn.Close();
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntCamposRespuesta", "Error en ObtenerCamposRespuestaEtiqueta: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Actualiza tabla CamposRespuesta
        /// </summary>
        /// <param name="nombre">Nombre del campo</param>
        /// <param name="etiqueta">Mensaje a mostrar en UI</param>
        /// <param name="idCampo">Id del campo que se modificara</param>
        /// <returns></returns>
        public bool GuardarCamposRespuesta(string nombre, string etiqueta, int idCampo = -1)
        {
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntCamposRespuesta", "GuardarCamposRespuesta");

                var sc = new SqlCommand("GuardarCamposRespuesta", cnn);
                var parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("@idCampo", SqlDbType.Int) { Value = idCampo };
                parametros[1] = new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = nombre };
                parametros[2] = new SqlParameter("@Etiqueta", SqlDbType.VarChar) { Value = etiqueta};
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);
                return true;
            }
            catch (Exception ex)
            {
                if (cnn != null) cnn.Close();
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntCamposRespuesta", "GuardarCamposRespuesta - Error : " + ex.Message);
                return false;
            }
        }
    }
}
