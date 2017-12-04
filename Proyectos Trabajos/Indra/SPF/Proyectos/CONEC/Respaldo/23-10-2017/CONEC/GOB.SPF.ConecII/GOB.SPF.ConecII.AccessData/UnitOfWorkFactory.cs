namespace GOB.SPF.ConecII.AccessData
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;

    public class UnitOfWorkFactory
    {
        private static string _connString;
        private static string ConnString
        {
            get
            {
                if (string.IsNullOrEmpty(_connString))
                    _connString = ConfigurationManager.ConnectionStrings["ConecTest"].ConnectionString;

                return _connString;
            }
        }

        public static IUnitOfWork Create()
        {

            var connection = new SqlConnection(ConnString);
            connection.Open();

            return new UnitOfWorkCatalog(connection, true);
        }

        public static IUnitOfWork CreateSecurityUnitOfWork(bool isTransaction)
        {
            var connection = new SqlConnection(ConnString);
            connection.Open();

            return new UnitOfWorkSecurity(connection, isTransaction);
        }
    }
}
