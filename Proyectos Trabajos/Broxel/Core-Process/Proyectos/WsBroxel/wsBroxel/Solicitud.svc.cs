using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using wsBroxel.App_Code;
using wsBroxel.App_Code.Online;
using wsBroxel.App_Code.PreAutorizador;
using wsBroxel.App_Code.RequestResponses;
using wsBroxel.App_Code.SolicitudBL;
using wsBroxel.App_Code.SolicitudBL.Model;
using wsBroxel.App_Code.GenericBL;
using wsBroxel.App_Code.TCControlVL;
using wsBroxel.App_Code.TCControlVL.Model;
using wsBroxel.App_Code.Utils;
using wsBroxel.App_Code.VCBL;
using wsBroxel.App_Code.VCBL.Models;
using System.Diagnostics;

namespace wsBroxel
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Solicitud" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Solicitud.svc or Solicitud.svc.cs at the Solution Explorer and start debugging.
    public class Solicitud : ISolicitud
    {

        /// <summary>
        /// Metodo para dispersion a cuenta wsSPEI, por ser deprecado
        /// </summary>
        /// <param name="numeroCuenta">Numero de cuenta</param>
        /// <param name="monto">Monto a dispersar</param>
        /// <param name="idTransac">Identificador univoco de la transaccion generadora</param>
        /// <returns></returns>
        public DispResponse DispersionACuenta(string numeroCuenta, decimal monto, int idTransac)
        {
            return DispersionACuentaUsr(numeroCuenta, monto, idTransac, "wsSPEI");
        }        

        /// <summary>
        /// Metodo para dispersion a cuenta wsSPEI y wsRedDePagos
        /// </summary>
        /// <param name="numeroCuenta">Numero de cuenta</param>
        /// <param name="monto">Monto a dispersar</param>
        /// <param name="idTransac">Identificador univoco de la transaccion generadora</param>
        /// <param name="idUser">Usuario que invoca, wsSPEI o wsRedPagos</param>
        /// <returns></returns>                                                                 
        public DispResponse DispersionACuentaUsr(string numeroCuenta, decimal monto, int idTransac, string idUser)
        {
            return DispersionACuentaUsrRef(numeroCuenta, monto, idTransac, idUser,"NA");
        }
        /// <summary>
        /// Metodo para dispersion a cuenta wsSPEI y wsRedDePagos
        /// </summary>
        /// <param name="numeroCuenta">Numero de cuenta</param>
        /// <param name="monto">Monto a dispersar</param>
        /// <param name="idTransac">Identificador univoco de la transaccion generadora</param>
        /// <param name="idUser">Usuario que invoca, wsSPEI o wsRedPagos</param>
        /// <param name="referencia">Referencia de pago</param>
        /// <returns></returns>
        public DispResponse DispersionACuentaUsrRef(string numeroCuenta, decimal monto, int idTransac, string idUser, string referencia)
        {
            string ip = "Unknown";
            var props = OperationContext.Current.IncomingMessageProperties;
            var endpointProperty = props[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            if (endpointProperty != null)
            {
                ip = endpointProperty.Address;
            }

            var idOp = idUser == "wsLiqComercios" ? 5 : 1;

            //var msg = idUser == "wsLiqComercios" ? "00":"01";

            var msg = "01";

            if (idUser == "wsLiqComercios" || idUser == "wsSPEI")
                msg = "00";

            var a = new OperArguments
            {
                Folio = 0,
                IdUser = idUser,
                Monto = monto,
                NumeroCuenta = numeroCuenta,
                NumeroTarjeta = "0000000000",
                Password = "NA",
                Token = "NA",
                IpFrom = ip,
                IdTransacFrom = idTransac,
                IdOperation = idOp,
                Referencia = referencia
            };
            return new MainSolicitudBL().Execute(a, idOp, msg,false);
            //return new DispResponse { msg = "En Construcción, petición recibida de: " + ip, numTransac = 0 };   
        }
        ///// <summary>
        ///// Metodo para dispersion a cuenta wsSPEI y wsRedDePagos con el canal que los esta invocando.
        ///// </summary>
        ///// <param name="numeroCuenta"></param>
        ///// <param name="monto"></param>
        ///// <param name="idTransac"></param>
        ///// <param name="idUser"></param>
        ///// <param name="referencia"></param>
        ///// <param name="canal"></param>
        ///// <returns></returns>
        //public DispResponse DispersionAcuentaConCanal(string numeroCuenta, decimal monto, int idTransac, string idUser,
        //    string referencia, string canal)
        //{
        //    string ip = "Unknown";
        //    var props = OperationContext.Current.IncomingMessageProperties;
        //    var endpointProperty = props[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
        //    if (endpointProperty != null)
        //    {
        //        ip = endpointProperty.Address;
        //    }
        //    var a = new OperArguments
        //    {
        //        Folio = 0,
        //        IdUser = idUser,
        //        Monto = monto,
        //        NumeroCuenta = numeroCuenta,
        //        NumeroTarjeta = "0000000000",
        //        Password = "NA",
        //        Token = "NA",
        //        IpFrom = ip,
        //        IdTransacFrom = idTransac,
        //        IdOperation = 1,
        //        Referencia = referencia,
        //        Canal = canal 
        //    };
        //    //return new MainSolicitudBL().Execute(a, 1, "01");
        //   return new DispResponse { msg = "En Construcción, petición recibida de: " + ip};   
        //}

        /// <summary>
        /// Dispersion a cuentas generica
        /// </summary>
        /// <param name="monto">monto a dispersar</param>
        /// <param name="cuentas">listado de cuentas a dispersar</param>
        /// <param name="idUsuario">Usuario de entrada</param>
        /// <returns></returns>
        public DispResponse DispersionCuentasGenerica(decimal monto, List<string> cuentas, string idUsuario)
        {
            string ip = "Unknown";
            var props = OperationContext.Current.IncomingMessageProperties;
            var endpointProperty = props[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            if (endpointProperty != null)
            {
                ip = endpointProperty.Address;
            }
            var a = new OperArguments
            {
                Folio = 0,
                IdUser = idUsuario,
                Monto = monto,
                NumeroCuenta = cuentas.Count.ToString(CultureInfo.InvariantCulture),
                NumeroTarjeta = "0000000000",
                Password = "NA",
                Token = "NA",
                IpFrom = ip,
                Cuentas = cuentas,
                Locale = 1,
                IdOperation = 2,
                Referencia = "NA"
            };
            return new MainSolicitudBL().Execute(a, 4, "01");
          
        }

        /// <summary>
        /// Dispersion conferma
        /// </summary>
        /// <param name="monto">monto a dispersar</param>
        /// <param name="cuentas">listado de cuentas a dispersar</param>
        /// <returns></returns>
        public DispResponse DispersionConferma(decimal monto, List<string> cuentas)
        {
            string ip = "Unknown";
            var props = OperationContext.Current.IncomingMessageProperties;
            var endpointProperty = props[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            if (endpointProperty != null)
            {
                ip = endpointProperty.Address;
            }
            var a = new OperArguments
            {
                Folio = 0,
                IdUser = "wsConferma",
                Monto = monto,
                NumeroCuenta = cuentas.Count.ToString(CultureInfo.InvariantCulture),
                NumeroTarjeta = "0000000000",
                Password = "NA",
                Token = "NA",
                IpFrom = ip,
                Cuentas = cuentas,
                Locale = 1,
                IdOperation = 2,
                Referencia = "NA"
            };
            return new MainSolicitudBL().Execute(a, 2, "01");

           
        }
        /// <summary>
        /// Devolucion conferma
        /// </summary>
        /// <param name="cuentas">listado de cuentas a realizar devolucion</param>
        /// <returns></returns>
        public DispResponse DevolucionConferma(List<string> cuentas)
        {
            string ip = "Unknown";
            var props = OperationContext.Current.IncomingMessageProperties;
            var endpointProperty = props[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            if (endpointProperty != null)
            {
                ip = endpointProperty.Address;
            }
            var a = new OperArguments
            {
                Folio = 0,
                IdUser = "wsConferma",
                Monto = 0,
                NumeroCuenta = cuentas.Count.ToString(CultureInfo.InvariantCulture),
                NumeroTarjeta = "0000000000",
                Password = "NA",
                Token = "NA",
                IpFrom = ip,
                Cuentas = cuentas,
                Locale = 1,
                IdOperation = 3,
                Referencia = "NA"
            };
            return new MainSolicitudBL().Execute(a, 3, "01");
        }

        /// <summary>
        /// Realizar un cargo 
        /// </summary>
        /// <param name="numeroTarjeta"></param>
        /// <param name="cvc2"></param>
        /// <param name="numeroCaja"></param>
        /// <param name="numeroTienda"></param>
        /// <param name="fechaOperacion"></param>
        /// <param name="horaOperacion"></param>
        /// <param name="transaccionPos"></param>
        /// <param name="monto"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public CargoWSResponse CargoWs(string numeroTarjeta, string cvc2, string numeroCaja, string numeroTienda,
            string fechaOperacion, string horaOperacion, string transaccionPos, decimal monto, string idUser)
        {
            string ip = "Unknown";
            var props = OperationContext.Current.IncomingMessageProperties;
            var endpointProperty = props[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            if (endpointProperty != null)
            {
                ip = endpointProperty.Address;
            }

            return new CargoWSResponse
            {
                FechaHoraOperacion = DateTime.Now,
                NoAutorizacion = new Random().Next(100000, 999999).ToString(CultureInfo.InvariantCulture),
                msg="",
                numTransac = new Random().Next(100000, 999999)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroTarjeta"></param>
        /// <param name="cvc2"></param>
        /// <param name="numeroCaja"></param>
        /// <param name="numeroTienda"></param>
        /// <param name="fechaOperacion"></param>
        /// <param name="horaOperacion"></param>
        /// <param name="transaccionPos"></param>
        /// <param name="monto"></param>
        /// <param name="idUser"></param>
        /// <param name="noAutorizacionOri"></param>
        /// <returns></returns>
        public CargoWSResponse ReversoCargoWs(string numeroTarjeta, string cvc2, string numeroCaja, string numeroTienda,
            string fechaOperacion, string horaOperacion, string transaccionPos, decimal monto, string idUser, string noAutorizacionOri)
        {
            string ip = "Unknown";
            var props = OperationContext.Current.IncomingMessageProperties;
            var endpointProperty = props[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            if (endpointProperty != null)
            {
                ip = endpointProperty.Address;
            }

            return new CargoWSResponse
            {
                FechaHoraOperacion = DateTime.Now,
                NoAutorizacion = new Random().Next(100000, 999999).ToString(CultureInfo.InvariantCulture),
                msg = "",
                numTransac = new Random().Next(100000, 999999)
            };
        }
        /// <summary>
        /// Metodo para creación de sesión de intercambio seguro entre .NET y php
        /// </summary>
        /// <param name="idUser">Usuario de UsuariosBroxelOnline</param>
        /// <param name="idAplication">Identificador de aplicación</param>
        /// <returns></returns>
        public string CrearSessionDash(int idUser, int idAplication)
        {
            string dash = string.Empty;
            try
            {
                var controllerDash = new App_Code.SessionDashMyo();
                dash = controllerDash.CrearSessionDash(idUser,idAplication);
                Trace.WriteLine("dash:" + dash);
                
            }
            catch(Exception e)
            {
                return null;
            }
            return dash;
        }

        //public SessionDashMyoResponse CrearSessionDash(string user)
        //{
        //    SessionDashMyoRequest res = new SessionDashMyoRequest();

        //    return res.crearSessionDash(user);
            
        //}


        /// <summary>
        /// Metodo para dispersion a cuenta wsC2GO
        /// </summary>
        /// <param name="numeroCuenta">Numero de cuenta</param>
        /// <param name="monto">Monto a dispersar</param>
        /// <param name="idTransac">Identificador univoco de la transaccion generadora</param>
        /// <returns></returns>                                             
        
        public DispResponse DispersionACuentaC2GO(string numeroCuenta, decimal monto, int idTransac)
        {
            string ip = "Unknown";
            var props = OperationContext.Current.IncomingMessageProperties;
            var endpointProperty = props[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            if (endpointProperty != null)
            {
                ip = endpointProperty.Address;
            }
            var a = new OperArguments
            {
                Folio = 0,
                IdUser = "wsC2GO",
                Monto = monto,
                NumeroCuenta = numeroCuenta,
                NumeroTarjeta = "0000000000",
                Password = "NA",
                Token = "NA",
                IpFrom = ip,
                IdTransacFrom = idTransac,
                IdOperation = 1,
                Referencia = "NA"
            };
            return new MainSolicitudBL().Execute(a, 1, "01");            
        }

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
      
    }
}
