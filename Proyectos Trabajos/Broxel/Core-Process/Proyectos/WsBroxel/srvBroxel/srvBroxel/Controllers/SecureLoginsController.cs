using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace srvBroxel.Controllers
{
    public class SecureLoginsController : ApiController
    {
        private MySqlConnection _conn;

        /// <summary>
        /// Obtiene IdUserSecure
        /// </summary>
        /// <param name="idUser">idUser</param>
        /// <returns></returns>
        public Int64 GetIdUserSecure(Int64 idUser)
        {
            long resultado;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();
                string sql = "call spInsIdSecureUser(" + idUser.ToString(CultureInfo.InvariantCulture) + ");";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };
                var dr = cmd.ExecuteReader();
                var tableResumen = new DataTable();
                tableResumen.Load(dr);
                if (tableResumen.Rows.Count > 0)
                {
                    var row = tableResumen.Rows[0];
                    var res = row[0].ToString();
                    if (res.Contains("Error"))
                        resultado = 0;
                    resultado = Convert.ToInt64(res);
                }
                else
                {
                    resultado = 0;
                }
            }
            catch (MySqlException ex)
            {
                //Console.WriteLine(ex.Message);
                ErrorHandling.EscribeError(ex);
                resultado = 0;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return resultado;
        }

        /// <summary>
        /// Valida el idUserSecure
        /// </summary>
        /// <param name="idUserSecure">idUserSecure</param>
        /// <returns>idUser</returns>
        public Int64 GetIdUserValid(long idUserSecure)
        {
            long resultado;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();
                string sql = "call spValidaIdUserSecure(" + idUserSecure.ToString(CultureInfo.InvariantCulture) + ");";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };
                var dr = cmd.ExecuteReader();
                var tableResumen = new DataTable();
                tableResumen.Load(dr);
                if (tableResumen.Rows.Count > 0)
                {
                    var row = tableResumen.Rows[0];
                    var res = row[0].ToString();
                    if (res.Contains("Error"))
                        resultado = 0;
                    resultado = Convert.ToInt64(res);
                }
                else
                {
                    resultado = 0;
                }
            }
            catch (MySqlException ex)
            {
                //Console.WriteLine(ex.Message);
                ErrorHandling.EscribeError(ex);
                resultado = 0;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return resultado;
        }

    }
    
}
