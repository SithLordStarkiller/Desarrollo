using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PubliPayments.Utiles
{
    public class LimpiarCaracteres
    {
        private const string InvalidosQuerys = "\"'@%`[]";
        private static Regex numerosRegex = new Regex(@"[^\d]");

        /// <summary>
        /// Quita los caracteres de querys inválidos
        /// </summary>
        /// <param name="cadena">Cadena de caracteres</param>
        /// <returns>Recresa la cadena sin los caracteres de querys inválidos</returns>
        public static string QuitarInvalidosQuerys(string cadena)
        {
            return RemplazarInvalidosQuerys(cadena);
        }

        /// <summary>
        /// Remplaza los caracteres de querys inválidos por el caracter de remplazo
        /// </summary>
        /// <param name="cadena">Cadena de caracteres</param>
        /// <param name="caracterRemplazo"></param>
        /// <returns>Regresa la cadena de caracteres con los caracteres de querys inválidos remplazados por el caracter dado</returns>
        public static string RemplazarInvalidosQuerys(string cadena, char? caracterRemplazo = null)
        {
            if (cadena == null) return null;
            var quitar = new HashSet<char>(InvalidosQuerys);
            var result = new StringBuilder(cadena.Length);
            foreach (char c in cadena)
                if (quitar.Contains(c))
                {
                    if (caracterRemplazo != null)
                        result.Append(caracterRemplazo);
                }
                else
                    result.Append(c);

            return result.ToString();
        }

        /// <summary>
        /// Remplaza cualquier diacrítico por su caracter normalizado
        /// </summary>
        /// <param name="texto">Cadena de caracteres a procesar</param>
        /// <returns>Regresa una cadena de caracteres normalizada</returns>
        public static string RemplazarDiacriticos(string texto)
        {
            if (texto == null) return null;
            var normalizado = texto.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder(texto.Length);

            foreach (var c in normalizado)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Remplaza todos los caracteres que no sean números por la cadena de reemplazo, 
        /// si no se incluye el parámetro "cadenaRemplazo" solo se dejan los números
        /// </summary>
        /// <remarks>Dejar cadenaRemplazo en null si se quieren quitar todos los caracteres que no sean números</remarks>
        /// <param name="cadena">Cadena de caracteres a procesar</param>
        /// <param name="cadenaRemplazo">Cadena de caracteres por la cual se van a reemplazar caracteres que no sean números</param>
        /// <returns>Regresa la cadena de caracteres con los no números remplazados</returns>
        public static string DejarSoloNumeros(string cadena, string cadenaRemplazo = null)
        {
            if (cadenaRemplazo == null) cadenaRemplazo = "";
            if (cadena == null) return null;
            var result = new StringBuilder(cadena.Length);
            foreach (char c in cadena)
                if (c >= '0' && c <= '9')
                    result.Append(c);
                else
                {
                    result.Append(cadenaRemplazo);
                }
            return result.ToString();
        }
    }
}
