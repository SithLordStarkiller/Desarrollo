using Mcs.DataAccess.Interfaces;
using  Mcs.DataAccess.Enumerators;

namespace Mcs.DataAccess
{
    public class ConnectionHelper
    {
        public static IConnectionFactory GetConnection(string key)
        {
            return new DbConnectionFactory(key);
        }

        public static IConnectionFactory GetConnection(DataBaseType dbType)
        {
            switch (dbType)
            {
                    case DataBaseType.Sicogua:
                        return new DbConnectionFactory("base");
                    case DataBaseType.Rep:
                        return new DbConnectionFactory("bdRep");
                    case DataBaseType.Rea:
                        return new DbConnectionFactory("bdRea");
                    case DataBaseType.Cove:
                        return new DbConnectionFactory("bdCove");
                default:
                    return new DbConnectionFactory("base");
            }
            
        }

    }
}
