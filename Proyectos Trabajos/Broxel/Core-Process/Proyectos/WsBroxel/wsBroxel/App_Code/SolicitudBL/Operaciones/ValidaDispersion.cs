using System;
using wsBroxel.App_Code.SolicitudBL.Model;

namespace wsBroxel.App_Code.SolicitudBL.Operaciones
{
    public class ValidaDispersion:IOperacion
    {
        public DispResponse Execute(OperArguments oper, MySqlDataAccess mySql)
        {
            DispResponse res = null;
            var step = 13;
            try
            {
                var val = mySql.ValidaDispersaPagoEnLinea(oper.NumeroCuenta);
                if (!(val.DisperDispersion || val.Pago))
                {
                    step = 14;
                    throw new Exception("La cuenta " + oper.NumeroCuenta + " no admite dispersion y/o pago en línea");
                }
                if(val.DisperDispersion)
                    oper.TipoOper = TiposOperacion.Dispersion;
                else if (val.Pago)
                    oper.TipoOper = TiposOperacion.Pago;
                else
                {
                    step = 14;
                    throw new Exception("La cuenta " + oper.NumeroCuenta + " no admite dispersion ni pago en línea");
                }
                oper.Producto = val.Producto;
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