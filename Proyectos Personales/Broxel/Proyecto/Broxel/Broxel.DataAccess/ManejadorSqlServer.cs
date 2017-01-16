namespace Broxel.DataAccess
{
    using System.Configuration;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class ManejadorSqlServer
    {
        private readonly string _cadenaDeConexion = ConfigurationManager.ConnectionStrings["BroxelConnectionString"].ConnectionString;

        private SqlConnection AbrirConexion()
        {
            var conneccion = new SqlConnection(_cadenaDeConexion);
            conneccion.Open();
            return conneccion;
        }

        private static void CierraConexion(IDbConnection conn)
        {
            try
            {
                conn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable ExecuteDataTable(string dataBase, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            try
            {
                var conn = AbrirConexion();

                var cmd = new SqlCommand { Connection = conn, CommandType = commandType, CommandText = commandText };

                cmd.Parameters.AddRange(commandParameters);

                var dataTable = new DataTable();

                var dataAdapter = new SqlDataAdapter(cmd);

                dataAdapter.Fill(dataTable);

                CierraConexion(conn);

                return dataTable;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
