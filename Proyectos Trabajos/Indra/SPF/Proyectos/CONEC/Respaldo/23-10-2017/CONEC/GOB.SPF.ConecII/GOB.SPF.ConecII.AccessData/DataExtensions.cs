using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.AccessData
{
    internal static class DataExtensions
    {
        internal static T GetValueFromDbObject<T>(this IDataReader reader, string columnName)
        {
            var dbObject = reader[columnName];
            if (dbObject == DBNull.Value)
                return default(T);

            var ordinal = reader.GetOrdinal(columnName);
            if (reader.GetFieldType(ordinal) != typeof(T))
                if (Nullable.GetUnderlyingType(typeof(T)) == null)
                {
                    return (T)Convert.ChangeType(dbObject, typeof(T));
                }
                else
                {
                    return (T)Convert.ChangeType(dbObject, Nullable.GetUnderlyingType(typeof(T)));
                }

            return (T) dbObject;

        }
    }
}
