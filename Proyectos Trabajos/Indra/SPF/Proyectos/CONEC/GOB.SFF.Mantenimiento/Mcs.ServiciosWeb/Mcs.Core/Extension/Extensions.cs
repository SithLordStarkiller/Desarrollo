using System;
using System.Collections;
using System.Collections.Generic;


namespace Mcs.Core.Extension
{
    public static class Extensions
    {

        /// <summary>
        /// Evalua si el valor de la cadena es un valor numerico.
        /// </summary>
        /// <returns></returns>
        public static bool IsNumeric(this string evaluateString)
        {
            float output;
            return float.TryParse(evaluateString, out output);
        }

        /// <summary>
        /// Evalua el tipo de objeto y realiza la conversión al tipo boolean.
        /// </summary>
        /// <returns></returns>
        public static bool ToBoolean(this object valueToEval)
        {
            if (valueToEval == null) return false;
            if (valueToEval is bool) return (bool)valueToEval;

            var booleanPossibleValues = new Dictionary<string, bool>
            {
                {"false", false},
                {"true", true},
                {"verdadero", true},
                {"falso", false},
                {"si", true},
                {"no", false},
                {"on", true},
                {"off", false},
                {"t", true},
                {"f", false},
                {"s", true},
                {"n", false},
                {"v", true}
            };

            if (valueToEval is string || valueToEval is char)
            {
                bool booleanValue;

                var particle = Convert.ToString(valueToEval).ToLowerInvariant();
                var success = bool.TryParse(valueToEval.ToString(), out booleanValue);

                if (!success)
                {
                    if (particle == string.Empty || particle == GetSpaces(particle.Length)) return booleanValue;

                    if (particle.IsNumeric())
                    {
                        return float.Parse(particle) > 0;
                    }

                    foreach (var element in booleanPossibleValues)
                    {
                        if (particle == element.Key)
                        {
                            return element.Value;
                        }
                    }
                    return true;
                }
                return booleanValue;
            }

            var isDateTime = valueToEval is DateTime;
            var isArray = valueToEval is Array;
            var isNumeric = valueToEval.ToString().IsNumeric();

            if (isDateTime) return true;
            if (isArray && ((Array)valueToEval).Length > 0) return true;
            if (isNumeric && Convert.ToDouble(valueToEval) > 0) return true;

            var isCollection = valueToEval is ICollection;

            if (isCollection)
            {
                var collection = (ICollection)valueToEval;
                if (collection.Count > 0) return true;

                return false;
            }

            return !isDateTime && !isArray && !isNumeric && !isCollection;
        }
        /// <summary>
        /// Converte el objeto en el tipo especificado, including all type objects to boolean.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="objectValue"></param>
        /// <returns></returns>
        public static TResult ToValue<TResult>(this object objectValue)
        {
            var value = objectValue;

            if (value == DBNull.Value || value == null)
                return default(TResult);

            var typeResult = typeof(TResult);
            var typeValue = value.GetType();

            if (!typeResult.IsEnum)
            {
                value = typeof(TResult) == typeof(bool) ? value.ToBoolean() : value;

                if (typeValue == typeof(String) && String.IsNullOrWhiteSpace(value.ToString()))
                    return default(TResult);

                return (TResult)Convert.ChangeType(value, typeof(TResult));
            }

            if (typeValue != typeof(String)) return (TResult)Enum.ToObject(typeResult, value);

            if (value == DBNull.Value)
                return default(TResult);

            value = typeof(TResult) == typeof(bool) ? value.ToBoolean() : value;

            return (TResult)Convert.ChangeType(value, typeof(TResult));
        }

        public static string GetSpaces(int size)
        {
            return string.Empty.PadLeft(size);
        }
    }
}
