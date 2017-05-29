using System;
using wsBroxel.App_Code.SolicitudBL.Model;

namespace wsBroxel.App_Code.SolicitudBL.Operaciones
{
    public class ValidaHecho:IOperacion
    {
        public DispResponse Execute(OperArguments oper, MySqlDataAccess mySql)
        {
            DispResponse res = null;
            try
            {
                var data = mySql.GetFolio(oper.IdTransacFrom,oper.NumeroCuenta, oper.IdUser);
                if (data != null && data.Folio != 0)
                {
                    if (data.Status != 100)
                    {
                        //"mauricio.lopez@broxel.com, jesus.valdiviezo@broxel.com"
                        Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com",
                        "Error en la dispersión wsSPEI", "Se realizó una dispersión a la cuenta " +
                        oper.NumeroCuenta + " con el folio " + data.Folio + ", quedando en estatus "
                        + data.Status + ", favor de revisar.", "yMQ3E3ert6", "Broxel : Asignacion de Lineas");
                    }
                    res = new DispResponse { msg = "00", numTransac = data.Folio };    
                }
            }
            catch (Exception e)
            {
                if (oper.Folio > 0)
                {
                    mySql.SetDispersionWsStatus(oper.Folio, 1);
                    mySql.InsertDispersionErr(oper.Folio, e.Message);
                }
                res = new DispResponse { msg = "00", numTransac = oper.Folio };
            }
            return res;

        }
    }
}