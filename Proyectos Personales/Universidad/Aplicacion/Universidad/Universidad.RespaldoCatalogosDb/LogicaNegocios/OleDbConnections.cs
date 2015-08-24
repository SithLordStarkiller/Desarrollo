namespace Universidad.RespaldoCatalogosDb.LogicaNegocios
{
    using System.Data.OleDb;
    using System.Data.SqlClient;

    public class OleDbConnections
    {
        private readonly SqlConnectionStringBuilder _connectionString;

        public OleDbConnections(SqlConnectionStringBuilder connectionString)
        {
            _connectionString = connectionString;
        }

        public OleDbConnection Connect()
        {
            var url = "Provider=SQLOLEDB.1;Data Source = " + _connectionString.DataSource + ";User Id =" + _connectionString.UserID + ";password=" + _connectionString.Password + ";Initial Catalog=" + _connectionString.InitialCatalog;
            var myConnection = new OleDbConnection
            {
                ConnectionString = url
            };

            myConnection.Open();

            return myConnection;
        }

        public void CloseConnection(OleDbConnection oleDbConnection)
        {
            oleDbConnection.Close();
        }
    }
}
