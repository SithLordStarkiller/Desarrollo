using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace ComCredencial.SolicitudBL
{
    public class MySqlDataAccess
    {
        private MySqlConnection _conn;
        #region Generales
        /// <summary>
        /// Valida si una cuenta esta en cuarentena de niveles de cuenta
        /// </summary>
        /// <param name="numCuenta">numero de cuenta</param>
        /// <returns>True si la cuenta se encuentra en cuarentena, False si no</returns>
        public bool ValidaCuentaEnCuarentena(string numCuenta)
        {
            var res = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();
                var cmd = new MySqlCommand
                {
                    CommandText = "select id " +
                                  "from CuarentenaCuentas " +
                                  "where numCuenta = '" + numCuenta + "' and fechaSalida is null",
                    CommandType = CommandType.Text,
                    Connection = _conn,
                    CommandTimeout = 1200
                };
                var dr = cmd.ExecuteReader();
                var cuarentenaTable = new DataTable();
                cuarentenaTable.Load(dr);
                if (cuarentenaTable.Rows.Count > 0)
                {
                    res = true;
                }

            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al validar cuenta en cuarentena: " + e);
                res = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }

        #endregion

    }
}
