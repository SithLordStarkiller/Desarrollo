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
    public class TipoInstalacion
    {
        public ResponseDto<List<BeTipoInstalacion>> GetTiposInstalacion()
        {

            ResponseDto<List<BeTipoInstalacion>> result;
            try
            {
                var listDataResult = new TipoInstalacionRepository(null).GetTiposInstalaciones();

                //if (listDataResult == null) throw new Exception("Ocurrió un error al obte");
                result = new ResponseDto<List<BeTipoInstalacion>>
                {
                    Success = true,
                    TotalRows = listDataResult?.Count ?? 0,
                    Value = (List<BeTipoInstalacion>)listDataResult
                };
            }
            catch (Exception ex)
            {
                Logger.WriteLog("User01", "MCS", "Tipos de Instalaciones", ex);
                result = new ResponseDto<List<BeTipoInstalacion>>
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
