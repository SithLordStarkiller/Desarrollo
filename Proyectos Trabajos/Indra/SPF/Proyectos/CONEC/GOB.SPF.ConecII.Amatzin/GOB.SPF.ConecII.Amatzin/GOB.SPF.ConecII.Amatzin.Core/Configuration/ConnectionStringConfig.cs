using System.Configuration;
using GOB.SPF.ConecII.Amatzin.Core.Resources;
namespace GOB.SPF.ConecII.Amatzin.Core.Configuration
{
    public static class ConnectionStringConfig
    {
        public static string GetConnection(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        public static string GetConnection()
        {
            return GetConnection(ResourceConfiguration.ConnectionDbStore);
        }
    }
}
