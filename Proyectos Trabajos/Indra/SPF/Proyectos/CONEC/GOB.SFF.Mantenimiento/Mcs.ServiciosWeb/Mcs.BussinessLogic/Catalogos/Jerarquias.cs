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
    public class Jerarquias
    {
        public ResponseDto<List<BeJerarquia>> GetJerarquias()
        {

            ResponseDto<List<BeJerarquia>> result;
            try
            {
                var listDataResult = new JerarquiaRepository(null).GetJerarquias();

                //if (listDataResult == null) throw new Exception("Ocurrió un error al obte");
                result = new ResponseDto<List<BeJerarquia>>
                {
                    Success = true,
                    TotalRows = listDataResult?.Count ?? 0,
                    Value = (List<BeJerarquia>)listDataResult
                };
            }
            catch (Exception ex)
            {
                Logger.WriteLog("User01", "REP", "Jerarquias", ex);
                result = new ResponseDto<List<BeJerarquia>>
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
