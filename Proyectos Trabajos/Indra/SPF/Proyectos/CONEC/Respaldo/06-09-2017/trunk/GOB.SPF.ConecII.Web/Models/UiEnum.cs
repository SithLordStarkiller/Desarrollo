using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiEnum
    {
        /// <summary>
        /// Constantes enumeradas para resultado de una operación
        /// </summary>
        public enum TransactionResult
        {
            Success = 1,
            Failed = 0,
            RecordExist = 2,
            Warning = 3,
            Unknown = -9
        }
    }
}