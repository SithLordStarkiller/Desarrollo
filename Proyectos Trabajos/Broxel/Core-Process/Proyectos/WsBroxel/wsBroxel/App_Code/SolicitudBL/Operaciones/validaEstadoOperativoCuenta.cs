using System;
using System.Net;
using System.Net.Sockets;
using wsBroxel.App_Code.SolicitudBL.Model;

namespace wsBroxel.App_Code.SolicitudBL.Operaciones
{
    /// <summary>
    /// Valida estado operativo de la cuenta y la activa en caso de que no sea operativa.
    /// </summary>
    public class ValidaEstadoOperativoCuenta : IOperacion
    {
        public DispResponse Execute(OperArguments oper, MySqlDataAccess mySql)
        {
            try
            {
                var proxy = new BroxelService();

                var infoSaldos = proxy.GetSaldosPorCuenta(oper.NumeroCuenta, "wsBroxel");

                //   !response.EstadoOperativo.);
                if (!infoSaldos.EstadoOperativo.ToLower().Equals("operativa"))
                {
                    var resultActivacion = proxy.ActivacionDeCuenta(oper.NumeroCuenta, "wsBroxel");

               
                    if (resultActivacion.Success.Equals(0))
                    {
                       
                        if (oper.Folio > 0)
                        {
                            mySql.InsertDispersionErr(oper.Folio, "Error: No se pudo realizar la activación de la cuenta");
                        }
                        /*
                        var msg = oper.IdOperation == 1 ? "00" : "01|Error caught when executing action, please check mail and retry";
                        res = new DispResponse { msg = msg, numTransac = oper.Folio };
                         */
                        return null;
                    }
                    mySql.SetBanderaDesbloqueoCuenta(oper.Folio,1);
                }


            }

            catch (Exception e)
            {
                if (oper.Folio > 0)
                {
                    mySql.InsertDispersionErr(oper.Folio, "Error: No se pudo realizar la activación de la cuenta: " + e);
                }
                /*
                var msg = oper.IdOperation == 1 ? "00" : "01|Error caught when executing action, please check mail and retry";
                res = new DispResponse { msg = msg, numTransac = oper.Folio };
                 */
            }
            return null;
        }



        private string GetLocalIp()
        {
            try
            {
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("10.0.2.4", 65530);
                    var endPoint = socket.LocalEndPoint as IPEndPoint;
                    if (endPoint == null)
                        return "";
                    var localIp = endPoint.Address.ToString();
                    return localIp;
                }
            }
            catch
            {
                return "";
            }
        }

    }
}