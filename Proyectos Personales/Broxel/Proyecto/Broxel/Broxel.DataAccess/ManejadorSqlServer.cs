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

        public DataTable ExecuteDataTable(string dataBase, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            try
            {
                var conn = AbrirConexion();

                var cmd = new SqlCommand { Connection = conn , CommandType = commandType, CommandText=commandText};

                foreach (var t in commandParameters)
                {
                    cmd.Parameters.Add(t);
                }

                var dataTable = new DataTable();

                var dataAdapter = new SqlDataAdapter(cmd);

                dataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception)
            {
                
                return null;
            }
        }
    }
}
