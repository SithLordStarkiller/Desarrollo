namespace Broxel.Helpers.Extencions
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Xml.Serialization;

    public static class Extensions
    {
        /// <summary>
        /// Convierte  un objeto DataTable en un objeto list de objeto que se especifique
        /// </summary>
        /// <typeparam name="T">DataTable</typeparam>
        /// <param name="dataTable">dataTable</param>
        /// <returns>List<T></T></returns>
        public static List<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            try
            {
                var dataList = new List<T>();
                const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

                var objFieldNames = typeof(T).GetProperties(flags).
                    Select(item => new
                    {
                        item.Name,
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
                            else if (propertyInfos.PropertyType == typeof(int?))
                            {
                                propertyInfos.SetValue
                                    (classObj, ConvertToIntNullable(dataRow[dtField.Name]), null);
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
                                propertyInfos.SetValue
                                    (classObj,
                                        dataRow[dtField.Name] is DateTime
                                            ? ConvertToDateString(dataRow[dtField.Name])
                                            : ConvertToString(dataRow[dtField.Name]), null);
                            }
                        }
                    }
                    dataList.Add(classObj);
                }
                return dataList;
            }
            catch (Exception e)
            {
                //Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "Extensions", "ToList: " + e.Message);
                throw;
            }
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
            return Convert.ToString(value.ReturnEmptyIfNull());
        }

        /// <summary>
        /// Convierte un objeto en int
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>int</returns>
        private static int ConvertToInt(object value)
        {
            return Convert.ToInt32(value.ReturnZeroIfNull());
        }

        /// <summary>
        /// Convierte objeto en un long
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>long</returns>
        private static long ConvertToLong(object value)
        {
            return Convert.ToInt64(value.ReturnZeroIfNull());
        }

        /// <summary>
        /// Convierte un objeto en un decimal
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>decimal</returns>
        private static decimal ConvertToDecimal(object value)
        {
            return Convert.ToDecimal(value.ReturnZeroIfNull());
        }

        /// <summary>
        /// Convierte objeto en DateTime
        /// </summary>
        /// <param name="date">date</param>
        /// <returns>DateTime</returns>
        private static DateTime ConvertToDateTime(object date)
        {
            return Convert.ToDateTime(date.ReturnDateTimeMinIfNull());
        }

        private static int? ConvertToIntNullable(object value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool GetResolvedConnecionIpAddress(string serverNameOrUrl, out string resolvedIpAddress)
        {
            var isResolved = false;
            IPAddress resolvIp = null;
            try
            {
                if (!IPAddress.TryParse(serverNameOrUrl, out resolvIp))
                {
                    var hostEntry = Dns.GetHostEntry(serverNameOrUrl);

                    if (hostEntry != null && hostEntry.AddressList != null
                        && hostEntry.AddressList.Length > 0)
                    {
                        if (hostEntry.AddressList.Length == 1)
                        {
                            resolvIp = hostEntry.AddressList[0];
                            isResolved = true;
                        }
                        else
                        {
                            foreach (var var in hostEntry.AddressList.Where(var => var.AddressFamily == AddressFamily.InterNetwork))
                            {
                                resolvIp = var;
                                isResolved = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    isResolved = true;
                }
            }
            catch (Exception)
            {
                isResolved = false;
                resolvIp = null;
            }
            finally
            {
                if (resolvIp != null) resolvedIpAddress = resolvIp.ToString();
            }

            resolvedIpAddress = null;
            return isResolved;
        }

        public static string SerializeObject<T>(T source)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var sw = new StringWriter())
            using (var writer = new XmlTextWriter(sw))
            {
                serializer.Serialize(writer, source);
                return sw.ToString();
            }
        }

        public static T DeSerializeObject<T>(string xml)
        {
            using (var sr = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(sr);
            }
        }

        public static object ReturnZeroIfNull(this object value)
        {
            if (value == DBNull.Value)
                return 0;
            if (value == null)
                return 0;
            return value;
        }

        public static object ReturnEmptyIfNull(this object value)
        {
            if (value == DBNull.Value)
                return string.Empty;
            if (value == null)
                return string.Empty;
            return value;
        }

        public static object ReturnFalseIfNull(this object value)
        {
            if (value == DBNull.Value)
                return false;
            if (value == null)
                return false;
            return value;
        }

        public static object ReturnDateTimeMinIfNull(this object value)
        {
            if (value == DBNull.Value)
                return DateTime.MinValue;
            if (value == null)
                return DateTime.MinValue;
            return value;
        }

        public static object ReturnNullIfDbNull(this object value)
        {
            if (value == DBNull.Value)
                return '\0';
            if (value == null)
                return '\0';
            return value;
        }

        public static string FormatUserDisplayName(string displayName = null, string defaultValue = "Users", bool returnNameIfExists = false, bool returnAddressPartIfExists = false)
        {
            if (!string.IsNullOrEmpty(displayName))
            {
                if (returnNameIfExists)
                    return Regex.Replace(displayName, @"\ \(\w{1,}\)", "");
                return (displayName.Split(' '))[0];
            }
            if (returnAddressPartIfExists)
            {
                var emailParts = defaultValue.Split('@');
                return emailParts[0];
            }
            return defaultValue;
        }

        public static string FormatUserTelephoneNumber(this string telephoneNumber)
        {
            var result = string.Empty;

            if (!string.IsNullOrEmpty(telephoneNumber))
            {
                result = telephoneNumber.ToLower().Trim().Replace("tel:", "");

                if (result.Contains(";"))
                {
                    if (!result.ToLower().Contains(";ext="))
                        result = result.Split(';')[0];
                }
            }

            return result;
        }

        public static bool IsValidEmail(this string emailAddress)
        {
            const string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            return Regex.IsMatch(emailAddress, pattern);
        }

        public static string ConvertDate(this DateTime datetTime, bool excludeHoursAndMinutes = false)
        {
            if (datetTime != DateTime.MinValue)
            {
                if (excludeHoursAndMinutes)
                    return datetTime.ToString("yyyy-MM-dd");
                return datetTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            return null;
        }
    }
}
