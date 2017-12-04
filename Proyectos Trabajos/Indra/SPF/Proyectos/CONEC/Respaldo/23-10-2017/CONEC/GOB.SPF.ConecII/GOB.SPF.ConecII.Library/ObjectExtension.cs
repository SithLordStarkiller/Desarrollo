using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GOB.SPF.ConecII.Library
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 16/10/2017
    /// Horacio Torres
    /// Creado
    /// </remarks>
    public static partial class Extensions
    {
        /// <summary>
        /// Converst any object to string. If object is null, then it uses default value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="strDefault"></param>
        /// <returns>String</returns>       
        public static string ToString(this Object obj, string strDefault)
        {
            string result = strDefault;
            if (obj != null)
            {
                result = obj.ToString();
            }
            return result;
        }

        /// <summary>        
        /// It checks if the objects represented through value-types, null, or strings are the same:
        /// for example if (string)"12/12/2010" == (datetime) 12 December 2010 12:00 AM 
        /// or null==null or (int) 0== (double) 0.0, or ' 5.45'==5.45 etc..        
        /// In other cases, it compares objects by object.Equals() method
        /// </summary>
        /// <param name="objA"></param>
        /// <param name="objB"></param>
        /// <returns></returns>       
        public static bool IsSameAs(this Object objA, Object objB)
        {
            bool blnResult = false;
            if ((objA == null || objA == DBNull.Value) && (objB == null || objB == DBNull.Value))
            { blnResult = true; }
            else
            {
                if ((objA.GetType().IsValueType) || (objA.GetType().IsValueType) || (objA is string) || (objB is string))
                {
                    string strA = objA.ToString().ToUpper().Trim();
                    string strB = objB.ToString().ToUpper().Trim();

                    decimal decA;
                    decimal decB;
                    if (decimal.TryParse(strA, out decA) && decimal.TryParse(strB, out decB))
                    {
                        if (decA == decB) { blnResult = true; }
                    }
                    else
                    {
                        double dblA;
                        double dblB;
                        if (double.TryParse(strA, out dblA) && double.TryParse(strB, out dblB))
                        {
                            if (dblA == dblB) { blnResult = true; }
                        }
                        else
                        {
                            DateTime dtmA;
                            DateTime dtmB;
                            if (DateTime.TryParse(strA, out dtmA) && DateTime.TryParse(strB, out dtmB))
                            {
                                if (dtmA == dtmB) { blnResult = true; }
                            }
                            else
                            {
                                bool blnA;
                                bool blnB;
                                if (bool.TryParse(strA, out blnA) && bool.TryParse(strB, out blnB))
                                {
                                    if (blnA == blnB) { blnResult = true; }
                                }
                                else
                                {
                                    if (objA.ToString() == objB.ToString())
                                    {
                                        blnResult = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    blnResult = object.Equals(objA, objB);
                }
            }
            return blnResult;
        }

        #region - Type Conversions / ReplaceNull equivs. -


        /// <summary>
        /// Get a value as the given Type of T from an object, but
        /// replaces DBNull, Null with the default value of T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>        
        public static T To<T>(this object source)
        {
            return To<T>(source, default(T));
        }

        /// <summary>
        /// Get a value as the given Type of T from an object, but
        /// replaces DBNull, Null, or the provided ComparisonValue 
        /// values with the given defaultValue.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>        
        public static T To<T>(this object source, T defaultValue)
        {

            //Make sure it isn't null, if so just return the default.
            if (source == null || source == DBNull.Value) return defaultValue;

            // Get the underlying source type.
            Type sourceType = source.GetType();

            // Get the underlying target type.
            Type targetType = typeof(T);

            // Make sure the conversion type is not the same as the original type
            if (targetType == sourceType)
            {
                // direct cast if the source and target are of same type
                return (T)source;
            }

            // Special case for strings.
            if (sourceType == typeof(string) && String.IsNullOrEmpty(source.ToString().Trim()))
            {
                // when string is null or empty then just return the default value passed in
                return defaultValue;
            }

            // NOTE: TryParse is much faster than ChangeType or ConvertFrom but you would have to add the TryParse for each know type
            //       or you could use reflection to do TryParse but then it would be much slower than ChangeType or ConvertFrom
            if (sourceType == typeof(string) && targetType.BaseType != null && targetType.BaseType == typeof(Enum))
            {
                return (T)Enum.Parse(targetType, source.ToString());
            }

            // Check for nullable types.
            // This is used to get the base type of the nullable and then call Convert.ChangeType which is faster than ConvertFrom
            if (IsNullable(targetType))
            {
                targetType = UnderlyingTypeOf(targetType);
            }

            // see if the source and target implement IConvertible
            if (typeof(System.IConvertible).IsAssignableFrom(sourceType) &&
                typeof(System.IConvertible).IsAssignableFrom(targetType))
            {
                // can convert any type that implements IConvertible
                return (T)Convert.ChangeType(source, targetType);
            }
            else
            {
                // this is a catch all but it will fail in some cases
                return (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(source.ToString());
            }
        }

        /// <summary>
        /// IsNullable
        /// </summary>
        /// <param name="testType"></param>
        /// /// <returns></returns>       
        private static bool IsNullable(Type testType)
        {
            if (!testType.IsGenericType) return false;

            return (testType.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// UnderlyingTypeOf
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>       
        private static Type UnderlyingTypeOf(Type testType)
        {

            System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(testType);
            return nullableConverter.UnderlyingType;
        }

        /// <summary>
        /// Get a DBNull.Value if the source value is Null.
        /// The second overload should be used with Strings if String.Empty
        /// is considered Null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>       
        public static object ToDBNull<T>(this T source)
        {
            T sourceNull = default(T);
            return ToDBNull<T>(source, sourceNull);
        }

        /// <summary>
        /// Get a DBNull.Value if the source value is equal to the given null comparison value
        /// or the value is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="nullComparisonValue"></param>
        /// <returns></returns>       
        public static object ToDBNull<T>(this T source, T nullComparisonValue)
        {
            T sourceNull = default(T);
            // If the target is equal to the given comparison value, return DBNull.
            if (source == null || (source.Equals(nullComparisonValue) == true
                || source.Equals(sourceNull) == true))
            {
                return DBNull.Value;
            }
            else
            {
                //Otherwise, return the source value.
                return source;
            }
        }
        #endregion


        /// <summary>
        /// Extension method to convert list of custom objects into a dataset.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>       
        public static DataSet ToDataSet<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable dtDestinationTable = new DataTable();
            ds.Tables.Add(dtDestinationTable);
            //add a column to table for each public property on T     
            foreach (var propInfo in elementType.GetProperties())
            {
                dtDestinationTable.Columns.Add(propInfo.Name, propInfo.PropertyType);
            }
            //go through each property on T and add each value to the table   
            foreach (T item in list)
            {
                DataRow drNewRow = dtDestinationTable.NewRow();
                foreach (var propInfo in elementType.GetProperties())
                {
                    drNewRow[propInfo.Name] = propInfo.GetValue(item, null);
                }
                dtDestinationTable.Rows.Add(drNewRow);
            }
            return ds;
        }
    }
}
