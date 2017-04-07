using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PubliPayments.Utiles
{
    public static class Extensions
    {
        /// <summary>
        /// Convierte  un objeto DataTable en un objeto list de objeto que se especifique
        /// </summary>
        /// <typeparam name="T">DataTable</typeparam>
        /// <param name="dataTable">dataTable</param>
        /// <returns>List<T></returns>
        public static List<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            var dataList = new List<T>();
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                    var field = objFieldNames.Find(x => x.Name == dtField.Name);

                    if (field != null)
                    {

                        if (propertyInfos.PropertyType == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToDateTime(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(long))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToLong(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToDateString(dataRow[dtField.Name]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToString(dataRow[dtField.Name]), null);
                            }
                        }
                    }
                }
                dataList.Add(classObj);
            }
            return dataList;
        }

        /// <summary>
        /// Convierte un objeto en sttring
        /// </summary>
        /// <param name="date">date</param>
        /// <returns>string</returns>
        private static string ConvertToDateString(object date)
        {
            return date == null ? string.Empty : Convert.ToDateTime(date).ConvertDate();
        }

        /// <summary>
        /// Convierte objeto en un string
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>string</returns>
        private static string ConvertToString(object value)
        {
            return Convert.ToString(HelperFunctions.ReturnEmptyIfNull(value));
        }

        /// <summary>
        /// Convierte un objeto en int
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>int</returns>
        private static int ConvertToInt(object value)
        {
            return Convert.ToInt32(HelperFunctions.ReturnZeroIfNull(value));
        }

        /// <summary>
        /// Convierte objeto en un long
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>long</returns>
        private static long ConvertToLong(object value)
        {
            return Convert.ToInt64(HelperFunctions.ReturnZeroIfNull(value));
        }

        /// <summary>
        /// Convierte un objeto en un decimal
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>decimal</returns>
        private static decimal ConvertToDecimal(object value)
        {
            return Convert.ToDecimal(HelperFunctions.ReturnZeroIfNull(value));
        }

        /// <summary>
        /// Convierte objeto en DateTime
        /// </summary>
        /// <param name="date">date</param>
        /// <returns>DateTime</returns>
        private static DateTime ConvertToDateTime(object date)
        {
            return Convert.ToDateTime(HelperFunctions.ReturnDateTimeMinIfNull(date));
        }
    }
}
