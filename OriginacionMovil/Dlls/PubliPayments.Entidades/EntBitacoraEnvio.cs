using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntBitacoraEnvio
    {
        public DataSet ObtenerBitacoraEnvio(int tipoConsulta)
        {
            var guidTiempos = Tiempos.Iniciar();

            var sql = ConexionSql.Instance;
            var cnn = sql.IniciaConexion();
            var instancia = ConexionSql.Instance;
            var sc = new SqlCommand("ObtenerBitacoraEnvio", cnn) {CommandType = CommandType.StoredProcedure};
            var parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@TipoConsulta", SqlDbType.Int) { Value = tipoConsulta };
            sc.Parameters.AddRange(parametros);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            instancia.CierraConexion(cnn);

            Tiempos.Terminar(guidTiempos);

            return ds;
        }

        public int ActualizarResultado(string ids, string resultado)
        {
            var guidTiempos = Tiempos.Iniciar();
            
            var sql = ConexionSql.Instance;
            var cnn = sql.IniciaConexion();
            var instancia = ConexionSql.Instance;
            var sc = new SqlCommand("ActualizarResultadoBitacoraEnvio", cnn) { CommandType = CommandType.StoredProcedure };
            var parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@ids", SqlDbType.VarChar, 5000) { Value = ids };
            parametros[1] = new SqlParameter("@Resultado", SqlDbType.VarChar, 100) { Value = resultado };
            sc.Parameters.AddRange(parametros);
            var res = sc.ExecuteNonQuery();
            instancia.CierraConexion(cnn);

            Tiempos.Terminar(guidTiempos);

            return res;
        }

        public int CancelarOrdenXCW(string idOrden)
        {
            var guidTiempos = Tiempos.Iniciar();

            var sql = ConexionSql.Instance;
            var cnn = sql.IniciaConexion();
            var instancia = ConexionSql.Instance;
            var sc = new SqlCommand("CancelarOrdenCWBitacoraEnvio", cnn) { CommandType = CommandType.StoredProcedure };
            var parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@idOrdenTxt", SqlDbType.VarChar, 5000) { Value = idOrden };
            sc.Parameters.AddRange(parametros);
            var res = sc.ExecuteNonQuery();
            instancia.CierraConexion(cnn);

            Tiempos.Terminar(guidTiempos);

            return res;
        }
    }
}
