using System;
using System.Collections.Generic;
using Mcs.Core.Common;
using Mcs.Entities;
using Mcs.Core.Log4Net;
using Mcs.DataAccess.Repositories;

namespace Mcs.BussinessLogic.Rep
{
    public class Integrantes
    {
        public ResponseDto<List<BeIntegrantes>> GetIntegrantes()
        {

            ResponseDto<List<BeIntegrantes>> result;
            try
            {
                var listDataResult = new IntegrantesRepository(null).GetIntegrantes();

                //if (listDataResult == null) throw new Exception("Ocurrió un error al obte");
                result = new ResponseDto<List<BeIntegrantes>>
                {
                    Success = true,
                    TotalRows = listDataResult?.Count ?? 0,
                    Value = (List<BeIntegrantes>)listDataResult
                };
            }
            catch (Exception ex)
            {
                Logger.WriteLog("User01", "REP", "Integrantes", ex);
                result = new ResponseDto<List<BeIntegrantes>>
                {
                    Success = false,
                    Value = null,
                    Message = ex.Message
                };
            }


            return result;
        }
    }
}
