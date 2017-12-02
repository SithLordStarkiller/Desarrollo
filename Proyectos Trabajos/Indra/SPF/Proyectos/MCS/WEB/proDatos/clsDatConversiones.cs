using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Datos
{
    public class conversiones
    {
        public static int? entero(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(valor);
            }

        }


        // Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        // <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        // Regresa el valor entero (int) si el parámetro es diferente de DBNull.Value, de otra forma regresa 0
        public static int enteroNoNulo(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(valor);
            }


        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor entero (short?) si el parámetro es diferente de DBNull.Value, de otra forma regresa null</returns>
        public static short? enteroCorto(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToInt16(valor);
            }


        }



        // Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        //Objeto que contiene el valor para comparar y convertir
        //Regresa el valor entero (short) si el parámetro es diferente de DBNull.Value, de otra forma regresa 0
        public static short enteroCortoNoNulo(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt16(valor);
            }

        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor cadena (string) si el parámetro es diferente de DBNull.Value, de otra forma regresa String.Empty ("")</returns>
        public static string cadena(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return String.Empty;
            }
            else
            {
                return valor.ToString();
            }

        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor caracter (char?) si el parámetro es diferente de DBNull.Value, de otra forma regresa null</returns>
        public static char? caracter(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToChar(valor);
            }


        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor caracter (char) si el parámetro es diferente de DBNull.Value, de otra forma new char()</returns>
        public static char caracterNoNulo(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return new char();
            }
            else
            {
                return Convert.ToChar(valor);
            }

        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor booleano (bool?) si el parámetro es diferente de DBNull.Value, de otra forma regresa null</returns>
        public static bool? booleano(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToBoolean(valor);
            }

        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor booleano (bool) si el parámetro es diferente de DBNull.Value, de otra forma regresa false</returns>
        public static bool boleanoNoNulo(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(valor);
            }

        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor Fecha (Datetime?) si el parámetro es diferente de DBNull.Value, de otra forma regresa null</returns>
        public static DateTime? fecha(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToDateTime(valor);
            }

        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor Fecha (Datetime) si el parámetro es diferente de DBNull.Value, de otra forma regresa new DateTime()</returns>
        public static DateTime fechaNoNulo(object valor)
        {


            if (valor == DBNull.Value || valor == null)
            {
                return DateTime.MinValue;
            }
            else
            {
                return Convert.ToDateTime(valor);
            }

        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor del arreglo de bytes (Byte[]) si el parámetro es diferente de DBNull.Value, de otra forma regresa null</returns>
        public static Byte[] arregloBytes(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                Byte[] arreglo = null;
                return arreglo;
            }
            else
            {
                return (Byte[])valor;
            }

        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor del identificador único (Guid?) si el parámetro es diferente de DBNull.Value, de otra forma regresa null</returns>
        public static Guid? identificadorUnico(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return null;
            }
            else
            {
                return (Guid?)valor;
            }


        }



        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor del identificador único (Guid) si el parámetro es diferente de DBNull.Value, de otra forma regresa Guid.Empy</returns>
        public static Guid identificadorUnicoNoNulo(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return Guid.Empty;
            }
            else
            {
                return (Guid)valor;
            }

        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor en objeto byte? si el parámetro es diferente de DBNull.Value, de otra forma regresa null</returns>
        public static Byte? objetoByte(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToByte(valor);
            }

        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor en objeto byte si el parámetro es diferente de DBNull.Value, de otra forma regresa new byte()</returns>
        public static Byte objetoByteNoNulo(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return new byte();
            }
            else
            {
                return Convert.ToByte(valor);
            }

        }



        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor entero (int64?) si el parámetro es diferente de DBNull.Value, de otra forma regresa null</returns>
        public static Int64? enteroLargo(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToInt64(valor);
            }

        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor entero (int64) si el parámetro es diferente de DBNull.Value, de otra forma regresa 0</returns>
        public static Int64 enteroLargoNoNulo(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(valor);
            }

        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor entero (Decimal?) si el parámetro es diferente de DBNull.Value, de otra forma regresa null</returns>
        public static Decimal? objetoDecimal(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToDecimal(valor);
            }

        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor entero (Decimal) si el parámetro es diferente de DBNull.Value, de otra forma regresa 0</returns>
        public static Decimal objetoDecimalNoNulo(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(valor);
            }

        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor entero (float?) si el parámetro es diferente de DBNull.Value, de otra forma regresa null</returns>
        public static float? flotante(object valor)
        {

            if (valor == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToSingle(valor);
            }


        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor entero (Decimal) si el parámetro es diferente de DBNull.Value, de otra forma regresa 0</returns>
        public static float flotanteNoNulo(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToSingle(valor);
            }

        }


        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor entero (Double?) si el parámetro es diferente de DBNull.Value, de otra forma regresa null</returns>
        public static Double? doble(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToDouble(valor);
            }

        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor entero (Double) si el parámetro es diferente de DBNull.Value, de otra forma regresa 0</returns>
        public static Double dobleNoNulo(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(valor);
            }

        }


        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor entero (Single?) si el parámetro es diferente de DBNull.Value, de otra forma regresa null</returns>
        public static Single? simple(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToSingle(valor);
            }

        }

        /// <summary>
        /// Realiza la comparación del objeto pasado como parámetro con DBNull.value y hace la conversión al tipo de dato correspondiente
        /// </summary>
        /// <param name="valor">Objeto que contiene el valor para comparar y convertir</param>
        /// <returns>Regresa el valor entero (Single) si el parámetro es diferente de DBNull.Value, de otra forma regresa 0</returns>
        public static Single simpleNoNulo(object valor)
        {

            if (valor == null || valor == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToSingle(valor);
            }

        }

        //public static DateTime tiempo(object valor)
        //{
        //    if (valor == null || valor == DBNull.Value)
        //    {
        //        return DateTime.MinValue;
        //    }
        //    else
        //    {
        //        Convert.ToDateTime(DateTime.MinValue + valor);
        //    }
        //}

    }
}


