using System;
using System.Data;
using System.Data.Common;
using Mcs.DataAccess.Interfaces;

using Mcs.Core.Configuration;
using Mcs.Core.Security;
using Mcs.Core.Exceptions;

namespace Mcs.DataAccess
{
    public class DbConnectionFactory : IConnectionFactory
    {
        private readonly DbProviderFactory _provider;
        private readonly string _connectionString;
        private readonly string _name;

        public DbConnectionFactory(string connectionName)
        {
            if (connectionName == null) throw new ArgumentNullException(Messages.NullParameterException);

            var conStr = Encriptacion.Decrypt(AppSettingsConfig.GetValue(connectionName));
            var userConn = Encriptacion.Decrypt(AppSettingsConfig.GetValue("userConnec"));
            var pwdConn = Encriptacion.Decrypt(AppSettingsConfig.GetValue("pwdConnec"));
            var provider = Encriptacion.Decrypt(AppSettingsConfig.GetValue("provider"));

            conStr += "UID=" + userConn + ";PWD=" + pwdConn;
            

            //var conStr = ConfigurationManager.ConnectionStrings[connectionName];
            if (conStr == null)
                throw new Exception(string.Format(Messages.ConfigurationExceptionFind, connectionName));

            _name = provider;
            _provider = DbProviderFactories.GetFactory(provider);
            _connectionString = conStr;

        }

        public IDbConnection Create()
        {
            var connection = _provider.CreateConnection();
            if (connection == null)
                throw new Exception(string.Format(Messages.ConfigurationExceptionCreate, _name));

            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }
    }
}
