using System;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntDictamen
    {
        public string ObtenerDictamenWS(int idOrden, string ruta)
        {
            string resultado = "";
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var ds = new DataSet();
            try
            {
                var sc = new SqlCommand("ObtenerDictamenWS", cnn);
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
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ObtenerDictamenWS", ex.Message);
            }

            instancia.CierraConexion(cnn);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                resultado = ds.Tables[0].Rows[0]["Clave"].ToString();
            }
            return resultado;
        }
    }
}
