using System;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using OracleConnectionODP.Entities;

namespace OracleConnectionODP.DataAccess
{
    public class ConnectionDbOracle
    {
        public string ConnectionOracle()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["OracleStringConnection"].ConnectionString; ;

            try
            {
                return connectionString;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public string LoginUserDa()
        {
            //string oradb = "User Id=C##OracleConnectionTest;Password=Aq141516182235;Data Source=127.0.0.1:1521/ORCL";
            OracleConnection conn = new OracleConnection(ConnectionOracle());
            conn.Open();
            var cmd = new OracleCommand
            {
                Connection = conn,
                CommandText = "SELECT UserName FROM USUSUARIO",
                CommandType = CommandType.Text
            };
            var dr = cmd.ExecuteReader();
            dr.Read();
            string user="";
            while (dr.Read())
            {
                user = dr.GetString(0);

            }
            conn.Dispose();
            return user;
        }
    }
}
