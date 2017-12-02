using System;
using System.Collections.Generic;
using Mcs.Core.Common;
using Mcs.Core.Log4Net;
using Mcs.DataAccess.Repositories;
using Mcs.Entities;

namespace Mcs.BussinessLogic.Catalogos
{
    public class Municipios
    {
        public ResponseDto<List<BeMunicipio>> GetMunicipios(int idEstado)
        {
            ResponseDto<List<BeMunicipio>> result;
            try
            {
                if (idEstado <= 0) throw new Exception("El id de la entidad federativa no puede ser nulo o 0.");

                var listDataResult = new MunicipiosRepository(null).GetMunicipios(idEstado);
                result = new ResponseDto<List<BeMunicipio>>
                {
                    Success = true,
                    TotalRows = listDataResult?.Count ?? 0,
                    Value = (List<BeMunicipio>)listDataResult
                };
            }
            catch (Exception ex)
            {
                Logger.WriteLog("User01", "MSC", "Municipios", ex);
                result = new ResponseDto<List<BeMunicipio>>
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
