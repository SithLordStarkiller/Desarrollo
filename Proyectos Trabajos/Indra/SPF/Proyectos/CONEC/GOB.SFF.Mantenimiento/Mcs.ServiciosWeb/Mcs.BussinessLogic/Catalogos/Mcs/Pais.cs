using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mcs.Entities.Mcs;
using Mcs.Core.Common;
using Mcs.DataAccess.Repositories.Mcs;
using Mcs.Core.Log4Net;
namespace Mcs.BussinessLogic.Catalogos.Mcs
{
    public class Pais
    {
        public ResponseDto<List<BePais>> GetAllPaises()
        {

            ResponseDto<List<BePais>> result;
            try
            {
                var listDataResult = new PaisRepository(null).GetAllPaises();

                result = new ResponseDto<List<BePais>>
                {
                    Success = true,
                    TotalRows = listDataResult?.Count ?? 0,
                    Value = (List<BePais>)listDataResult
                };
            }
            catch (Exception ex)
            {
                Logger.WriteLog("User01", "MCS", "Pais", ex);
                result = new ResponseDto<List<BePais>>
                {
                    Success = false,
                    Value = null,
                    Message = ex.Message
                };
            }

            return result;
        }


        public ResponseDto<BePais> GetPaisById(int id)
        {

            ResponseDto<BePais> result;
            try
            {
                var dataResult = new PaisRepository(null).GetPaisbyId(id);

                result = new ResponseDto<BePais>
                {
                    Success = true,
                    TotalRows = dataResult !=null?1: 0,
                    Value = (BePais)dataResult
                };
            }
            catch (Exception ex)
            {
                Logger.WriteLog("User01", "MCS", "Pais", ex);
                result = new ResponseDto<BePais>
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
