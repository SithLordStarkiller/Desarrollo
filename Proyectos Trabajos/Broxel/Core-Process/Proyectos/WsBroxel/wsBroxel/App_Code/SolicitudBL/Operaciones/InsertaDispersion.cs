using System;
using wsBroxel.App_Code.SolicitudBL.Model;

namespace wsBroxel.App_Code.SolicitudBL.Operaciones
{
    public class InsertaDispersion:IOperacion
    {
        public DispResponse Execute(OperArguments oper, MySqlDataAccess mySql)
        {
            DispResponse res = null;
            var step = 20;
            try
            {
                switch (oper.TipoOper)
                {
                    case TiposOperacion.Dispersion:
                        if (!mySql.SpInsDispersionesFromWs(oper.Folio))
                        {
                            step = 21;
                            throw new Exception("Error al insertar en la tabla de dispersiones");
                        }
                        break;
                    case TiposOperacion.Pago:
                        if (!mySql.SpInsPagosFromWs(oper.Folio))
                        {
                            step = 22;
                            throw new Exception("Error al insertar en la tabla de pagos");
                        }
                        break;
                    default:
                        step = 22;
                        throw new Exception("Error al insertar en la tabla de pagos o dispersiones, no se definio el tipo de operacion.");
                }

            }
            catch (Exception e)
            {
                if (oper.Folio > 0)
                {
                    mySql.SetDispersionWsStatus(oper.Folio, step);
                    mySql.InsertDispersionErr(oper.Folio, e.Message);
                }
                res = new DispResponse { msg = oper.Locale==1?"01|General Error when inserting data to data base, please retry":"01", numTransac = 0 };
            }
            return res;
        }
    }
}