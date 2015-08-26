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
            string url;
            
            if (_connectionString.IntegratedSecurity)
            {
                //Provider=SQLOLEDB.1;
                url = "Data Source = " + _connectionString.DataSource +
                      ";Initial Catalog=" + _connectionString.InitialCatalog + ";Trusted_Connection=yes";
            }
            else
            {
                url = "Data Source = " + _connectionString.DataSource + ";User Id =" +
                          _connectionString.UserID + ";password=" + _connectionString.Password + ";Initial Catalog=" +
                          _connectionString.InitialCatalog;
            }

            var myConnection = new OleDbConnection
            {
                ConnectionString = url
            };

            myConnection.Open();

            return myConnection;
        }

        public void ExecuteQuery(OleDbConnection connetion,string query)
        {
            connetion.Open();

            var command = new OleDbCommand(query, connetion);

            command.ExecuteNonQuery();
        }

        public void CloseConnection(OleDbConnection oleDbConnection)
        {
            oleDbConnection.Close();
        }
    }
}
