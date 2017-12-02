using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mcs.Core.Common;
using Mcs.Core.Log4Net;
using Mcs.DataAccess.Repositories;
using Mcs.Entities;

namespace Mcs.BussinessLogic.Catalogos
{
    /// <summary>
    /// Clase de negocio de Entidades Federativas.
    /// </summary>
    public class EntidadesFederativas
    {
        /// <summary>
        /// Obtiene las todas las Entidades Federativas.
        /// </summary>
        /// <returns>Devuelve un objeto Response generico que contiene una lista de las Entidades Federativas.</returns>
        public ResponseDto<List<BeEntidadFederativa>> GetEntidadesFederativas()
        {
            ResponseDto<List<BeEntidadFederativa>> result;
            try
            {
                var listDataResult = new EntidadesFederativasRepository(null).GetEntidadesFederativas();
                result = new ResponseDto<List<BeEntidadFederativa>>
                {
                    Success = true,
                    TotalRows = listDataResult?.Count ?? 0,
                    Value = (List<BeEntidadFederativa>)listDataResult
                };
            }
            catch (Exception ex)
            {
                Logger.WriteLog("User01", "MSC", "Entidades Federativas", ex);
                result = new ResponseDto<List<BeEntidadFederativa>>
                {
                    Success = false,
                    TotalRows = 0,
                    Value = null,
                    Message = ex.Message
                };
            }

            return result;
        }


    }
}
