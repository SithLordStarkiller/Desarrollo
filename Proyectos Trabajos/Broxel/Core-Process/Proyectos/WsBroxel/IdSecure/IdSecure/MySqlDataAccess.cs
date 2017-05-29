using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace IdSecure
{
    class MySqlDataAccess
    {
        /// <summary>
        /// Obtiene IdUserSecure
        /// </summary>
        /// <param name="idUser">idUser</param>
        /// <returns></returns>
        public int GetIdUserSecure(int idUser)
        {
            int resultado;
            var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
            try
            {
                
                conn.Open();
                string sql = "call spInsIdSecureUser(" + idUser.ToString(CultureInfo.InvariantCulture) + ");";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = conn,
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
                    {
                        conn.Close();
                        return GetIdUserSecure(idUser);
                    }
                    resultado = Convert.ToInt32(res);
                }
                else
                {
                    resultado = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                conn.Close();
                resultado = GetIdUserSecure(idUser);
            }
            finally
            {
                conn.Close();
            }

            return resultado;
        }

        /// <summary>
        /// Valida el idUserSecure
        /// </summary>
        /// <param name="idUserSecure">idUserSecure</param>
        /// <returns>idUser</returns>
        public int GetIdUserValid(int idUserSecure)
        {
            int resultado;
            var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
            try
            {

                conn.Open();
                string sql = "call spValidaIdUserSecure(" + idUserSecure.ToString(CultureInfo.InvariantCulture) + ");";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = conn,
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
                        return 0;
                    resultado = Convert.ToInt32(res);
                }
                else
                {
                    resultado = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultado = 0;
            }
            finally
            {
                conn.Close();
            }

            return resultado;
        }
    }
}
