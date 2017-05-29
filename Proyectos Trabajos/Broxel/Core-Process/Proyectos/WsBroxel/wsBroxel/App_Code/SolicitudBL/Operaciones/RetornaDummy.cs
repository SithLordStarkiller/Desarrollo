using System;
using wsBroxel.App_Code.SolicitudBL.Model;

namespace wsBroxel.App_Code.SolicitudBL.Operaciones
{
    public class RetornaDummy:IOperacion
    {
        public DispResponse Execute(OperArguments oper, MySqlDataAccess mySql)
        {
            DispResponse res = null;
            try
            {
                res = new DispResponse { msg = "00", numTransac = oper.Folio };
            }
            catch (Exception e)
            {
                if (oper.Folio > 0)
                {
                    mySql.SetDispersionWsStatus(oper.Folio, 40);
                    mySql.InsertDispersionErr(oper.Folio, e.Message);
                }
                res = new DispResponse { msg = "00", numTransac = oper.Folio };
            }
            return res;
            
        }
    }
}