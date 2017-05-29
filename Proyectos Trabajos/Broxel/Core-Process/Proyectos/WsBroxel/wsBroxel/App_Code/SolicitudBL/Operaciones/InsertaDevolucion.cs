using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsBroxel.App_Code.SolicitudBL.Model;

namespace wsBroxel.App_Code.SolicitudBL.Operaciones
{
    /// <summary>
    /// Inserta solicitud de devolucion
    /// </summary>
    public class InsertaDevolucion:IOperacion
    {
        public DispResponse Execute(OperArguments oper, MySqlDataAccess mySql)
        {
            DispResponse res = null;
            var step = 20;
            try
            {
                if (!mySql.spInsDevolucionesFromWS(oper.Folio))
                {
                    step = 21;
                    throw new Exception("Error al insertar en la tabla de devoluciones");
                }
            }
            catch (Exception e)
            {
                if (oper.Folio > 0)
                {
                    mySql.SetDispersionWsStatus(oper.Folio, step);
                    mySql.InsertDispersionErr(oper.Folio, e.Message);
                }
                res = new DispResponse { msg = oper.Locale == 1 ? "01|General Error when inserting data to data base, please retry" : "01", numTransac = 0 };
            }
            return res;
        }
    }
}