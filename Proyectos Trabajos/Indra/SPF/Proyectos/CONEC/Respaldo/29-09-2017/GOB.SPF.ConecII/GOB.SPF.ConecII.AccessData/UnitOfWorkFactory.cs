namespace GOB.SPF.ConecII.AccessData
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;

    public class UnitOfWorkFactory
    {
        public static IUnitOfWork Create()
        {
          string connString = connString = ConfigurationManager.ConnectionStrings["ConecTest"].ConnectionString;

          var connection = new SqlConnection(connString);
          connection.Open();
          
          return new UnitOfWorkCatalog(connection, true);
        }
    }
}
