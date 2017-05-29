using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using wsBroxel.App_Code.SolicitudBL.Model;

namespace wsBroxel.App_Code.SolicitudBL.Operaciones
{
    /// <summary>
    /// Operacion que ejecuta las dispersiones de un pago
    /// </summary>
    public class EjecutaDispersion:IOperacion
    {
        /// <summary>
        /// Operacion principal de ejecución
        /// </summary>
        /// <param name="oper">Variable con argumentos necesarios para la ejecución</param>
        /// <param name="mySql">Clase de acceso a la base de datos mySql</param>
        /// <returns>Objeto de respuesta</returns>
        /// <exception cref="Exception"></exception>
        public DispResponse Execute(OperArguments oper, MySqlDataAccess mySql)
        {
            DispResponse res = null;
            var step = 30;
            try
            {
                var proxy = new BroxelService();

           
                var data = mySql.GetWsInvokeData(oper.Folio);
                switch (oper.TipoOper)
                {
                    case TiposOperacion.Dispersion:
                        if (data != null)
                        {
                            var ret = proxy.Incremento(data.IdSolicitud, data.Email);
                            if (ret)
                            {
                                mySql.SetDispersionWsStatus(oper.Folio, 0);
                                CargaMasivaFoliosComisiones(oper.Folio, mySql, proxy, oper.FolioSolicitudCargo);
                                return new DispResponse { msg = "00", numTransac = oper.Folio };
                            }
                            step = 32;
                            throw new Exception("Error al invocar al servicio, no se ejecuto la dispersión");
                        }
                        step = 31;
                        throw new Exception("No exiten datos para invocar al servicio de dispersion");
                    case TiposOperacion.Pago:
                        if (data != null)
                        {
                            var ret = proxy.Pago(data.IdSolicitud);
                            if (ret)
                            {
                                mySql.SetDispersionWsStatus(oper.Folio, 0);
                                CargaMasivaFoliosComisiones(oper.Folio, mySql, proxy, oper.FolioSolicitudCargo);
                                return new DispResponse { msg = "00", numTransac = oper.Folio };
                            }
                            step = 34;
                            throw new Exception("Error al invocar al servicio, no se ejecuto el pago");
                        }
                        step = 33;
                        throw new Exception("No exiten datos para invocar al servicio de pago");
                    default:
                        step = 35;
                        throw new Exception("Error al ejecutar pago o dispersion, no se definio el tipo de operacion.");
                }

                
            }
            catch (Exception e)
            {
                if (oper.Folio > 0)
                {
                    mySql.SetDispersionWsStatus(oper.Folio, step);
                    mySql.InsertDispersionErr(oper.Folio, e.Message);

                  //  mySql.SetBanderaDesbloqueoCuenta(oper.Folio, 0); regresa el estus de la bandera a 0 para cuando
                    //se repita el proceso esta pueda ser cambiando nuevamente...
                }
                var msg = oper.IdOperation == 1 || oper.IdOperation == 5 ? "00" : "01|Error caught when executing action, please check mail and retry";
                res = new DispResponse { msg = msg, numTransac = oper.Folio };
            }
            return res;
        }

        /// <summary>
        /// Realizamos las cargas masivas de los folios que se generaron las solicitudes.
        /// </summary>
        /// <param name="proxy">servicio de broxel en sql server</param>
        /// <param name="folios">lista de folios de las comisiones.</param>
        /// <param name="folioOperacion">Folio de la operacion</param>
        private void CargaMasivaFoliosComisiones(long folioOperacion,MySqlDataAccess mySql,  BroxelService proxy,List<string> folios)
        {
            if (folios != null && folios.Count > 0)
            {
                foreach (var folio in folios)
                {
                    Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["TiempoCobrarComision"]));
                    string folio1 = folio;
                    var taskCargaMasiva = new Task(() => CargaMasivaFoliosComisiones(folioOperacion, mySql,proxy, folio1));
                    taskCargaMasiva.Start();
                    taskCargaMasiva.Wait();
                }
            }
        }
        /// <summary>
        /// Realizamos las cargas masivas de los folios que se generaron las solicitudes.
        /// </summary>
        /// <param name="proxy">servicio de broxel en sql server</param>
        /// <param name="folio">folio de la comisión</param>
        private void CargaMasivaFoliosComisiones(long folioOperacion, MySqlDataAccess mySql, BroxelService proxy, string folio)
        {
            if (!proxy.CargoMasivo(folio))
            {
                mySql.InsertDispersionErr(folioOperacion, "Error al intentar realizar carga masiva. Folio de Cargo solicitud: " + folio);
            }
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