using System;
using wsBroxel.App_Code.SolicitudBL.Model;

namespace wsBroxel.App_Code.SolicitudBL.Operaciones
{
    public class AfectaLcCliente:IOperacion
    {
        public DispResponse Execute(OperArguments oper, MySqlDataAccess mySql)
        {
            DispResponse res = null;
            var step = 13;
            try
            {
                if (mySql.SetDispersionWsUpdLc(oper.Folio, 1))
                {
                    if (Helper.ActualizaLineaCreditoCliente(oper.ClabeCliente, 5, oper.Monto) == 1)
                    {
                        mySql.SetDispersionWsUpdLc(oper.Folio, mySql.GetIdLc(oper.Monto,oper.ClabeCliente));
                    }
                    else
                    {
                        step = 15;
                        throw new Exception("Error al intentar actualizar linea de credito(2)");
                    }
                }
                else
                {
                    step = 14;
                    throw new Exception("Error al intentar actualizar id linea de credito(1)");
                }
            }
            catch (Exception e)
            {
                if (oper.Folio > 0)
                {
                    mySql.SetDispersionWsStatus(oper.Folio, step);
                    mySql.InsertDispersionErr(oper.Folio, e.Message);
                }
                res = new DispResponse { msg = "01", numTransac = 0 };
            }
            return res;
        }
    }
}