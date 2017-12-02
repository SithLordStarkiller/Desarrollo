using System;
using System.Collections.Generic;
using Mcs.Core.Common;
using Mcs.Entities;
using Mcs.Core.Log4Net;
using Mcs.DataAccess.Repositories;

namespace Mcs.BussinessLogic.Catalogos
{
    public class Areas
    {
        public ResponseDto<List<BeArea>> GetAreas()
        {

            ResponseDto<List<BeArea>> result;
            try
            {
                var listDataResult = new AreasRepository(null).GetAreas();

                //if (listDataResult == null) throw new Exception("Ocurrió un error al obte");
                result = new ResponseDto<List<BeArea>>
                {
                    Success = true,
                    TotalRows = listDataResult?.Count ?? 0,
                    Value = (List<BeArea>)listDataResult
                };
            }
            catch (Exception ex)
            {
                Logger.WriteLog("User01", "REP", "Areas", ex);
                result = new ResponseDto<List<BeArea>>
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
