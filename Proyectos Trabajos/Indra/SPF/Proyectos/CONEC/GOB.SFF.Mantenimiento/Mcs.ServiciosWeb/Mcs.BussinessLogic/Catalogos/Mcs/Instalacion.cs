using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mcs.Core.Common;
using Mcs.DataAccess.Repositories.Mcs;
using Mcs.Core.Log4Net;
using Mcs.DataAccess.Repositories;
using Mcs.Entities;
using Mcs.Entities.Mcs;


namespace Mcs.BussinessLogic.Catalogos.Mcs
{
    public class Instalacion
    {
        public ResponseDto<List<BeInstalacion>> GetInstalaciones()
        {

            ResponseDto<List<BeInstalacion>> result;
            try
            {
                var listDataResult = new InstalacionRepository(null).GetInstalaciones();
                result = new ResponseDto<List<BeInstalacion>>
                {
                    Success = true,
                    TotalRows = listDataResult?.Count ?? 0,
                    Value = (List<BeInstalacion>)listDataResult
                };
            }
            catch (Exception ex)
            {
                Logger.WriteLog("User01", "MCS", "Instalaciones", ex);
                result = new ResponseDto<List<BeInstalacion>>
                {
                    Success = false,
                    Value = null,
                    Message = ex.Message +  "  " + ex.StackTrace
                };
            }


            return result;
        }
    }
}
