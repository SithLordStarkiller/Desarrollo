using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsBroxel.App_Code.SolicitudBL.Model;

namespace wsBroxel.App_Code.SolicitudBL.Operaciones
{
    /// <summary>
    /// Ejecuta devolución programada
    /// </summary>
    public class EjecutaDevolucion:IOperacion
    {
        /// <summary>
        /// Método principal de ejecución de la implementadora
        /// </summary>
        /// <param name="oper">Argumentos</param>
        /// <param name="mySql">Objeto de acceso a bd MySql</param>
        /// <returns></returns>
        public DispResponse Execute(OperArguments oper, MySqlDataAccess mySql)
        {
            DispResponse res = null;
            var step = 30;
            try
            {
                var proxy = new BroxelService();
                var data = mySql.GetWsInvokeData(oper.Folio);
                if (data != null)
                {
                    var ret = proxy.Devolucion(data.IdSolicitud);
                    if (ret)
                    {
                        mySql.SetDispersionWsStatus(oper.Folio, 0);
                        return new DispResponse { msg = "00", numTransac = oper.Folio };
                    }
                    step = 32;
                        throw new Exception("Error al invocar al servicio, no se ejecuto la devolución");
                }
                step = 31;
                throw new Exception("No exiten datos para invocar al servicio de dispersion");
            }
            catch (Exception e)
            {
                if (oper.Folio > 0)
                {
                    mySql.SetDispersionWsStatus(oper.Folio, step);
                    mySql.InsertDispersionErr(oper.Folio, e.Message);
                }
                var msg = oper.IdOperation == 1 ? "00" : "01|Error caught when executing action, please check mail and retry";
                res = new DispResponse { msg = msg, numTransac = oper.Folio };
            }
            return res;

        }
    }
}