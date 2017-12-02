using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mcs.Core.Common;
using Mcs.Entities;
using Mcs.Core.Log4Net;
using Mcs.DataAccess.Repositories;

namespace Mcs.BussinessLogic.Catalogos
{
    public class Estaciones
    {
        public ResponseDto<List<BeEstacion>> GetEstaciones()
        {

            ResponseDto<List<BeEstacion>> result;
            try
            {
                var listDataResult = new EstacionesRepository(null).GetEstaciones();

                result = new ResponseDto<List<BeEstacion>>
                {
                    Success = true,
                    TotalRows = listDataResult?.Count ?? 0,
                    Value = (List<BeEstacion>)listDataResult
                };
            }
            catch (Exception ex)
            {
                Logger.WriteLog("User01", "MCS", "Estaciones", ex);
                result = new ResponseDto<List<BeEstacion>>
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
