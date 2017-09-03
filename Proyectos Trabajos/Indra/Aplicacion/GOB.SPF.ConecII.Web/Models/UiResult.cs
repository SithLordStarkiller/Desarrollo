using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiResult<T>
    {
        /// <summary>
        /// Tipo de valor para el resultado
        /// </summary>
        public T ObjectResult { get; set; }
        /// <summary>
        /// Lista de resultados de tipo generics
        /// </summary>
        public List<T> List { get; set; }

        /// <summary>
        /// Resultado de texto
        /// </summary>
        public string TextResult { get; set; }
        /// <summary>
        /// Resultado de texto para respuesta satisfactoria
        /// </summary>
        public string SuccessfulTextResult { get; set; }
        /// <summary>
        /// Resultado de texto para respuesta fallida
        /// </summary>
        public string ErrorTextResult { get; set; }
        /// <summary>
        /// Mensage para resultado satisfactorio o fallido
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Dirección 
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Codigo del error
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Tipo de resultado, Erroneo, Satisfactorio, aDvertencia, etc.
        /// </summary>
        public UiEnum.TransactionResult Result { get; set; }
    }
}