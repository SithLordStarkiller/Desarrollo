using System;
using System.Data;
using System.Data.SqlClient;

namespace PubliPayments.Entidades.MYO
{
    public class EntRespuestasMYO
    {
        public static void InsertarRespuesta(int idOrden, string nombreCampo, string valor)
        {
            string sql = "exec [InsertarRespuesta] " +
                            "@idOrden = "+idOrden+"," +
                            "@NombreCampo = N'"+nombreCampo+"'," +
                            "@Valor =N'"+valor+"'," +
                            "@idFormulario = - 1";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

        }

        public static void BorrarRespuestasOrden(int idOrden, string nombreCampo)
        {
            var sql = "exec BorrarRespuestasOrden " +
                      "@idOrden = " + idOrden + ", " +
                      "@nombreCampo = N'" + nombreCampo + "'";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
        }

        /// <summary>
        /// valida los resultados esperados para la situacion en la cual se encuentra la orden para Myo
        /// </summary>
        /// <param name="idOrden">idorden a procesar</param>
        /// <returns>resultado esperado</returns>
        public DataSet ProcesarRespuestasMyo(int idOrden)
        {

            var instancia = ConnectionDB.Instancia;

            var parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@idorden", SqlDbType.VarChar, 50) { Value = idOrden };

            var ds = instancia.EjecutarDataSet("SqlDefault", "ProcesarRespuestasMyo", parametros);

            return ds;
        }
    }
}
