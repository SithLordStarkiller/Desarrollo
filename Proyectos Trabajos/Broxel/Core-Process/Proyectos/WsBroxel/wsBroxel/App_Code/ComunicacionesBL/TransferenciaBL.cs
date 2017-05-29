using System;
using System.Configuration;
using System.Threading;
using wsBroxel.App_Code.GenericBL;
using wsBroxel.App_Code.SolicitudBL;
using wsBroxel.wsMovementsService;
using System.Diagnostics;
using wsBroxel.App_Code;
using wsBroxel.wsAutorizacion;
using System.Globalization;
using System.Net;
using System.Linq;

namespace wsBroxel.ComunicacionesBL
{
    public class TransferenciaBL
    {
        TransferenciaResponse response = new TransferenciaResponse();
		BroxelEntities ctx = new BroxelEntities();
		BroxelService b = new BroxelService();

		public TransferenciaResponse C2CPetrusPetrus(TransferenciaRequest request, wsBroxel.Movimiento m)
        {
            IMovementsService wsMovementsService = new wsMovementsService.MovementsServiceClient("BasicHttpBinding_IMovementsService", "http://" + ConfigurationManager.AppSettings["PetrusHost"] + ":" + ConfigurationManager.AppSettings["PetrusPort"] + "/IssuerCommercial.ClientWS.ServiceHost/Services/MovementsService.svc");
            var reqInt = new InspectorBehavior();
            var handler = (MovementsServiceClient)wsMovementsService;
            handler.Endpoint.Behaviors.Clear();
            handler.Endpoint.Behaviors.Add(reqInt);
            var dateTimeInicio = DateTime.Now;
            //Llama al servicio Transfer de Petrus para realizar transferencia C2C
            var transferencia = wsMovementsService.Transfer("103", new wsMovementsService.TransferDTO
            {
                Amount = request.Monto,
                CardNumberFrom = request.Tarjeta.NumeroTarjeta,
                CardNumberTo = request.TarjetaRecibe.NumeroTarjeta,
                Currency = 484,
            });
            var dateTimeFin = DateTime.Now;
            var reqXml = reqInt.LastRequestXml;
            var resXml = reqInt.LastResponseXml;
            response.UserResponse = "Transferencia PetrusAPetrus:" + transferencia.ReturnExecution.ToString() == "SUCCESS" ? transferencia.ReturnExecution.ToString() : transferencia.Errors[0].ErrorText;
            response.Success = transferencia.ReturnExecution.ToString() == "SUCCESS" ? 1 : 0;
            response.NumeroAutorizacion = transferencia.ReturnExecution.ToString() == "SUCCESS" ? "SUCCESS" : "ERROR";
            response.IdMovimiento = m.idMovimiento;
            response.CodigoRespuesta = transferencia.ReturnExecution.ToString() == "SUCCESS" ? -1 : 999;
            response.FechaCreacion = Convert.ToDateTime(m.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
            response.SaldoOrigenDespues = b.GetSaldosPorCuenta(request.Tarjeta.Cuenta, "", "TransferenciaDeCuentasOrigenDespues").Saldos.DisponibleCompras;
            response.SaldoDestinoDespues = b.GetSaldosPorCuenta(request.TarjetaRecibe.Cuenta, "", "TransferenciaDeCuentasDestinoDespues").Saldos.DisponibleCompras;

            try
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 17, 5,
                                    reqXml, resXml, m.idMovimiento);
                });
            }
            catch (Exception e)
            {
                Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
            }

            return response;
        }
        public TransferenciaResponse C2CCredencialCredencial(TransferenciaRequest request, wsBroxel.Movimiento mov, Comercio com, DateTime currentDate) 
        {
            AutorizacionPortType autorizadorWs = new AutorizacionPortTypeClient("AutorizacionHttpSoap11Endpoint",
			                                         "https://" + ConfigurationManager.AppSettings["CredencialHost"] + ":" + 
			                                          ConfigurationManager.AppSettings["CredencialPort"] + "/services/Autorizacion.AutorizacionHttpSoap11Endpoint");
			var autorizarReq = new AutorizarRequest
            {
                SecuenciaTransaccion = mov.idMovimiento.ToString(CultureInfo.InvariantCulture),
                Canal = "BroxelWeb",
                TipoTransaccion = "65",
                FechaTransaccion = currentDate.ToString("yyyyMMdd"),
                HoraTransaccion = currentDate.ToString("HHmmss"),
                Comercio = com.CodigoComercio,
                NombreComercio = com.Comercio1,
                Terminal = "PC",
                ModoIngreso = "1",
                Tarjeta = request.Tarjeta.NumeroTarjeta,
                FechaExpiracion = request.Tarjeta.FechaExpira,
                CodigoSeguridad = request.Tarjeta.Cvc2,
                Importe = request.Monto.ToString(CultureInfo.InvariantCulture),
                CodigoMoneda = "484",
                TarjetaDestino = request.TarjetaRecibe.NumeroTarjeta,
            };
            switch (request.TipoConceptoComision)
            {
                case 1:
                    autorizarReq.FeeTransaccion = request.Comision.ToString(CultureInfo.InvariantCulture);
                    break;
                case 2:
                    autorizarReq.FeeProcesamientoTransaccion = request.Comision.ToString(CultureInfo.InvariantCulture);
                    break;
            }
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var reqInt = new InspectorBehavior();
            var handler = (AutorizacionPortTypeClient)autorizadorWs;
            handler.Endpoint.Behaviors.Clear();
            handler.Endpoint.Behaviors.Add(reqInt);
            var dateTimeInicio = DateTime.Now;
            var autorizarResp = autorizadorWs.Autorizar(autorizarReq);
            var dateTimeFin = DateTime.Now;
            var reqXml = reqInt.LastRequestXml;
            var resXml = reqInt.LastResponseXml;

            mov.Autorizado = autorizarResp.CodigoRespuesta.Trim() == ("-1");
            mov.NoAutorizacion = autorizarResp.CodigoAutorizacion;

            int codresp = Convert.ToInt32(autorizarResp.CodigoRespuesta);
            CodigosRespuestaSQL cr = ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
            if (cr != null)
                response.UserResponse = cr.Descripcion;
            if (response.UserResponse.Trim().ToUpper() != autorizarResp.MensajeError.Trim().ToUpper() && autorizarResp.CodigoRespuesta.Trim() != ("-1"))
                response.UserResponse = autorizarResp.MensajeError.Trim().ToUpper();
            response.Success = mov.Autorizado == true ? 1 : 0;
            response.NumeroAutorizacion = autorizarResp.CodigoAutorizacion;
            response.IdMovimiento = mov.idMovimiento;
            response.CodigoRespuesta = Convert.ToInt32(autorizarResp.CodigoRespuesta);
            response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
            response.SaldoOrigenDespues = new BroxelService().GetSaldosPorCuenta(request.Tarjeta.Cuenta, "", "TransferenciaDeCuentasOrigenDespues").Saldos.DisponibleCompras;
            response.SaldoDestinoDespues = new BroxelService().GetSaldosPorCuenta(request.TarjetaRecibe.Cuenta, "", "TransferenciaDeCuentasDestinoDespues").Saldos.DisponibleCompras;
            try
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 17, 1,
                        reqXml, resXml, mov.idMovimiento);
                });
            }
            catch (Exception e)
            {
                Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
            }
            return response;
        }
		public TransferenciaResponse C2CPetrusCredencial(TransferenciaRequest request, wsBroxel.Movimiento mov)
		{
			response.SaldoOrigenAntes = b.GetSaldosPorCuenta(request.NumCuenta, "", "C2CPetrusCredencial").Saldos.DisponibleCompras;
			response.FechaCreacion = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
			var cargo = b.SetCargo(request.Monto, request.Tarjeta.NombreTarjeta, request.Tarjeta.NumeroTarjeta, request.Tarjeta.Cvc2, 
								   request.Tarjeta.FechaExpira, mov.NoReferencia, mov.NombreReferencia, request.UserID,request.idComercio);
			int idmovCargo = cargo.IdMovimiento;
			if (cargo.Success == 1)
			{
				response.SaldoOrigenDespues = b.GetSaldosPorCuenta(request.NumCuenta, "", "C2CPetrusCredencial").Saldos.DisponibleCompras;
				//Abono a cuenta destino
				response.SaldoDestinoAntes = b.GetSaldosPorCuenta(request.TarjetaRecibe.Cuenta, "", "C2CPetrusCredencial").Saldos.DisponibleCompras;
				var abono = b.EfectuarPago(request.Monto, request.TarjetaRecibe.NombreTarjeta, request.TarjetaRecibe.NumeroTarjeta, request.TarjetaRecibe.Cvc2,
										   request.TarjetaRecibe.FechaExpira, request.UserID, request.idComercio, request.TarjetaRecibe.Cuenta);
				if (abono.Success == 0)
				{
					//Aplicar Reverso cuando no es exitoso el abono
					var reverso = b.ReversoCargo(idmovCargo, request.Tarjeta.NumeroTarjeta, request.Tarjeta.Cvc2, request.Tarjeta.FechaExpira, request.UserID);
					response.Success = 0;
					response.UserResponse = "La transacción no se completo porque la cuenta destino no recibio el pago,"+(reverso.Success == 1? "el reverso del cargo de la cuenta origen fue exitoso." : "el reverso del cargo de la cuenta origen no se realizo correctamente:"+reverso.UserResponse);
					response.CodigoRespuesta = 999;
					response.NumeroAutorizacion = "0";
				}
				else
				{
					response.Success = abono.Success;
					response.UserResponse = abono.UserResponse;
					response.CodigoRespuesta = abono.CodigoRespuesta;
					response.NumeroAutorizacion = abono.NumeroAutorizacion;
				}
				response.SaldoDestinoDespues = b.GetSaldosPorCuenta(request.NumCuenta, "", "C2CPetrusCredencial").Saldos.DisponibleCompras;
			}
			else
			{
				response.SaldoOrigenDespues = b.GetSaldosPorCuenta(request.NumCuenta, "", "C2CPetrusCredencial").Saldos.DisponibleCompras;
				response.Success = 0;
				response.UserResponse = "El cargo no se completo correctamente, "+cargo.UserResponse;
				response.CodigoRespuesta = 999;
				response.NumeroAutorizacion = cargo.NumeroAutorizacion;
			}
			return response;
		}
    }
}