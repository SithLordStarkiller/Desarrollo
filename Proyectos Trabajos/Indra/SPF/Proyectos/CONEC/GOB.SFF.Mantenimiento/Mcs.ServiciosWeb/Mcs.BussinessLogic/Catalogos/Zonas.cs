using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mcs.Core.Common;
using Mcs.DataAccess.Repositories;
using Mcs.Entities;

using Mcs.Core.Log4Net;

namespace Mcs.BussinessLogic.Catalogos
{
    /// <summary>
    /// 
    /// </summary>
    public class Zonas
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResponseDto<List<BeZona>> GetZonas()
        {
            ResponseDto<List<BeZona>> result;
            try
            {
                var listDataResult = new ZonasRepository(null).GetZonas();

                result = new ResponseDto<List<BeZona>>
                {
                    Success = true,
                    TotalRows = listDataResult?.Count ?? 0,
                    Value = (List<BeZona>)listDataResult
                };
            }
            catch (Exception ex)
            {
                Logger.WriteLog("User01", "MCS", "Zonas", ex);
                result = new ResponseDto<List<BeZona>>
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
