namespace CrudMySql.AccesoDatos
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Data;

    public class MySqlDbController
    {
        private static readonly MySqlDbController Instance = new MySqlDbController();

        private static readonly string connectionString = "server=127.0.0.1;user id=Local;password=A@141516182235;port=3306;database=broxel";

        public MySqlConnection OpenConnection()
        {
            try
            {
                var con = new MySqlConnection(connectionString);
                con.Open();
                return con;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void CloseConnection(MySqlConnection con)
        {
            try
            {
                con.Close();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable EjecutarDataSet(string command, MySqlParameter[] parametros, CommandType tipoComando)
        {
            var instancia = new MySqlDbController();
            MySqlConnection cnn = null;
            DataTable ds;
            try
            {
                cnn = instancia.OpenConnection();
                var sc = new MySqlCommand(command, cnn)
                {
                    CommandType = tipoComando
                };
                if (parametros != null)
                    sc.Parameters.AddRange(parametros);
                var dataReader = sc.ExecuteReader();
                ds = new DataTable();
                ds.Load(dataReader);
                instancia.CloseConnection(cnn);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (cnn != null)
                    instancia.CloseConnection(cnn);
            }

            return ds;
        }
    }
}
