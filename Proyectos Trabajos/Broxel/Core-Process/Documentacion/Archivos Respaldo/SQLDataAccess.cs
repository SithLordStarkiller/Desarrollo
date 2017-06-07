namespace wsBroxel.App_Code.SolicitudBL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Clase para acceso a SQL
    /// </summary>
    public class SQLDataAccess
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idTranWs"></param>
        /// <returns></returns>
        public string GetTiempAireIdComercio(long idTranWs)
        {
            var res = "";
            try
            {
                var cs = ConfigurationManager.ConnectionStrings["SQLPS"].ToString();

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@idTranWs",SqlDbType.BigInt){ Value = idTranWs}
                };

                var cmd = "select a.idComercio " +
                          "from CatProvedor a join Catalogo_Producto b on a.IdProvedor = b.IdProvedor " +
                          "where b.SKU = (" +
                          " select SUBSTRING(xmlSolicitud, charindex('<idParametro>21</idParametro><value>',xmlSolicitud)+LEN('<idParametro>21</idParametro><value>'), (CHARINDEX('</value>',xmlSolicitud,charindex('<idParametro>21</idParametro><value>',xmlSolicitud)+LEN('<idParametro>21</idParametro><value>'))) - (charindex('<idParametro>21</idParametro><value>',xmlSolicitud)+LEN('<idParametro>21</idParametro><value>'))) as SKU " +
                          " from PagoTransac  with (nolock) " +
                          " where idTransac = @idTranWs)";
                var dt = SQLSelect(cmd, cs,parameters);
                if (dt != null)
                {
                    res = dt.Rows[0]["idComercio"].ToString();
                }
            }
            catch (Exception)
            {
                res = "";
            }
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <returns></returns>
        public string GetLineaMyCard(string numCuenta)
        {
            var linea = string.Empty;
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@numCuenta",SqlDbType.VarChar){ Value = numCuenta}
                };

                var cs = ConfigurationManager.ConnectionStrings["SQLBI"].ToString();
                var cmd = "select line from FTPReportLog where cuenta = @numCuenta order by fechaProceso desc";
                var dt = SQLSelect(cmd, cs,parameters);
                if (dt != null)
                {
                    linea = dt.Rows[0]["line"].ToString();
                }
            }
            catch (Exception)
            {
                linea = string.Empty;
            }
            return linea;
        }


        private DataTable SQLSelect(string command, string conectionString, SqlParameter[] parameters)
        {
            var conn = new SqlConnection(conectionString);
            try
            {
                conn.Open();
                var cmd = new SqlCommand
                {
                    CommandText = command,
                    Connection = conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var resTable = new DataTable();
                resTable.Load(dr);
                conn.Close();
                return resTable.Rows.Count > 0 ? resTable : null;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
    }
}