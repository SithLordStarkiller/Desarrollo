using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using wsBroxel.newWSAutorizacion;
using wsBroxel.PaymentService;

namespace wsBroxel.App_Code.GenericBL
{
	public class ReversoBL
	{
		
		/// <summary>
		/// Reversa asignaciones de línea
		/// </summary>
		/// <param name="request"></param>
		/// <param name="mov"></param>
		/// <returns></returns>
		public LimiteResponse ReversoLimite(LimiteRequest request, wsBroxel.Movimiento mov)
		{
     		IPaymentService paymentPetrus = new PaymentServiceClient("BasicHttpBinding_IPaymentService", "http://" + ConfigurationManager.AppSettings["PetrusHost"] + ":" + ConfigurationManager.AppSettings["PetrusPort"] + "/IssuerCommercial.ClientWS.ServiceHost/Services/PaymentService.svc");
		    AutorizacionPortType newAutorizadorWs = new AutorizacionPortTypeClient("AutorizacionHttpsSoap11Endpoint1", "https://" + ConfigurationManager.AppSettings["CredencialNewHost"] + ":" + ConfigurationManager.AppSettings["CredencialNewPort"] + "/Autorizacion");

			var bProxy = new BroxelService();
			var response = new LimiteResponse();
			var currentDate = DateTime.Now;
			
			using (var ctx = new BroxelEntities())
			{
				var anul = new AnulacionN
				{
					TipoTransaccion = 33,
					SubTipoTransaccion = Convert.ToInt32(request.Tipo.ToString(CultureInfo.InvariantCulture)),
					Fecha = currentDate,
					idTransaccion = mov.idMovimiento,
					idUsuario = request.UserID,
				};
				ctx.AnulacionN.Add(anul);
				ctx.SaveChanges();
				try
				{
					request.Tarjeta.Procesador = Helper.GetTarjetaFromCuenta(request.NumCuenta).Procesador;
					response.SaldoAntes = bProxy.GetSaldosPorCuenta(request.Tarjeta.Cuenta, "", "ReversoBL").Saldos.DisponibleCompras;
					if (request.Tarjeta.Procesador == 1)
					{
						var autorizarReq = new AutorizarRequest
						{
							Canal = "BroxelWeb",
							TipoTransaccion = "33",
							SubTipoTransaccion = request.Tipo.ToString(CultureInfo.InvariantCulture),
							SecuenciaTransaccion = mov.idMovimiento.ToString(CultureInfo.InvariantCulture),
							Comercio = "999999999000003",
							FechaExpiracion = request.Tarjeta.FechaExpira,
							Importe = request.Limite.ToString(CultureInfo.InvariantCulture),
							FechaTransaccion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyyMMdd"),
							HoraTransaccion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("HHmmss"),
							Terminal = "PC",
							CodigoSeguridad = request.Tarjeta.Cvc2,
							ModoIngreso = "1",
							Tarjeta = request.Tarjeta.NumeroTarjeta,
							CodigoMoneda = "484",
						};
						ServicePointManager.ServerCertificateValidationCallback +=
							(sender, cert, chain, sslPolicyErrors) => true;

						var reqInt = new InspectorBehavior();
						var handler = (AutorizacionPortTypeClient)newAutorizadorWs;
						handler.Endpoint.Behaviors.Clear();
						handler.Endpoint.Behaviors.Add(reqInt);
						var dateTimeInicio = DateTime.Now;
						var autorizarResp = newAutorizadorWs.Autorizar(autorizarReq);
						var dateTimeFin = DateTime.Now;
						var reqXml = reqInt.LastRequestXml;
						var resXml = reqInt.LastResponseXml;

						try
						{
							ThreadPool.QueueUserWorkItem(delegate
							{
								new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 13, 2,
									reqXml, resXml, mov.idMovimiento, anul.idAnulacion);
							});
						}
						catch (Exception e)
						{
							Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
						}

						int codresp = Convert.ToInt32(autorizarResp.CodigoRespuesta);
						CodigosRespuestaSQL cr = ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
						if (cr != null)
							response.UserResponse = cr.Descripcion;
						anul.NumAutorizacion = Convert.ToInt32(autorizarResp.CodigoAutorizacion);
						anul.MensajeRespuesta = cr.Descripcion;
						anul.Autorizado = autorizarResp.CodigoRespuesta.Trim() == ("-1");
						response.Success = anul.Autorizado == true ? 1 : 0;
						response.NumeroAutorizacion = autorizarResp.CodigoAutorizacion;
						response.CodigoRespuesta = Convert.ToInt32(autorizarResp.CodigoRespuesta);
						response.FechaCreacion = Convert.ToDateTime(anul.Fecha).ToString("yyyy-MM-dd HH:mm:ss");
						response.IdMovimiento = anul.idAnulacion;
						ctx.SaveChanges();
					}
					else if (request.Tarjeta.Procesador == 2)
					{
						var reverso = new UndoPaymentDTO
						{
							Amount = request.Limite,
							CurrencyCode = 484,
							GeneralAccountNumber = Convert.ToInt64(request.Tarjeta.Cuenta),
							PaymentCode = mov.NoAutorizacion,
							PaymentDate = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss"),
							UndoReason = "Reverso por webservice",
						};
						var reqInt = new InspectorBehavior();
						var handler = (PaymentServiceClient)paymentPetrus;
						handler.Endpoint.Behaviors.Clear();
						handler.Endpoint.Behaviors.Add(reqInt);
						var dateTimeInicio = DateTime.Now;
						var rev = paymentPetrus.UndoPayment(reverso);
						var dateTimeFin = DateTime.Now;
						var reqXml = reqInt.LastRequestXml;
						var resXml = reqInt.LastResponseXml;

						try
						{
							ThreadPool.QueueUserWorkItem(delegate
							{
								new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 3, 2,
									reqXml, resXml, mov.idMovimiento);
							});
						}
						catch (Exception e)
						{
							Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
						}

						anul.Autorizado = rev.paymentInfo.PaymentCode.Trim() == ("-1");
						anul.NumAutorizacion = rev.ReturnExecution == PaymentService.ReturnExecution.SUCCESS ? 1 : 0;
						anul.MensajeRespuesta = rev.ReturnExecution == PaymentService.ReturnExecution.SUCCESS
							? "Exito"
							: rev.Errors[0].ErrorText;
						response.Success = anul.Autorizado == true ? 1 : 0;
						response.NumeroAutorizacion = rev.paymentInfo.PaymentCode;
						response.CodigoRespuesta = anul.Autorizado == true ? 1 : 999;
						response.FechaCreacion = Convert.ToDateTime(anul.Fecha).ToString("ddMMyyyy HH:mm:ss");
						response.IdMovimiento = anul.idAnulacion;
						response.UserResponse = anul.MensajeRespuesta;
						ctx.SaveChanges();
					}
					response.SaldoDespues = bProxy.GetSaldosPorCuenta(request.Tarjeta.Cuenta, "", "ReversoBL").Saldos.DisponibleCompras;
				}
				catch (Exception ex)
				{
					anul.NumAutorizacion = -1;
					anul.Autorizado = false;
					anul.MensajeRespuesta = "Problema en WebService : " + ex;
					ctx.SaveChanges();
				}
			}
			return response;
		}

		/// <summary>
		/// Reversa cargos
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public CargoDeleteResponse EliminarPago(CargoEditRequest request)
		{
            IPaymentService paymentPetrus = new PaymentServiceClient("BasicHttpBinding_IPaymentService", "http://" + ConfigurationManager.AppSettings["PetrusHost"] + ":" + ConfigurationManager.AppSettings["PetrusPort"] + "/IssuerCommercial.ClientWS.ServiceHost/Services/PaymentService.svc");
            AutorizacionPortType newAutorizadorWs = new AutorizacionPortTypeClient("AutorizacionHttpsSoap11Endpoint1", "https://" + ConfigurationManager.AppSettings["CredencialNewHost"] + ":" + ConfigurationManager.AppSettings["CredencialNewPort"] + "/Autorizacion");
			CargoDeleteResponse response = new CargoDeleteResponse();

			using (var ctx = new BroxelEntities())
			{
			 	wsBroxel.Movimiento cargo = ctx.Movimiento.FirstOrDefault(m => m.idMovimiento == request.IdCargo);
				if (cargo != null)
				{
					DateTime currentTime = DateTime.Now;
					AnulacionN anul = new AnulacionN
					{
						Fecha = DateTime.Now,
						idTransaccion = request.IdCargo,
						idUsuario = request.UserID,
						TipoTransaccion = 1043,
					};
					ctx.AnulacionN.Add(anul);
					ctx.SaveChanges();
					try
					{
						cargo.idComercio = Convert.ToInt32(cargo.idComercio);
						wsBroxel.Comercio com = (from x in ctx.Comercio where x.idComercio == cargo.idComercio select x).First();
						request.Tarjeta.Procesador = Helper.GetTarjetaFromCuenta(request.NumCuenta).Procesador;

						if (request.Tarjeta.Procesador == 1)
						{
							var autorizarReq = new AutorizarRequest
							{
								Canal = "BroxelWeb",
								TipoTransaccion = "1043",
								SecuenciaTransaccion = request.IdCargo.ToString(CultureInfo.InvariantCulture),
								FechaTransaccion = Convert.ToDateTime(cargo.FechaHoraCreacion).ToString("yyyyMMdd"),
								HoraTransaccion = Convert.ToDateTime(cargo.FechaHoraCreacion).ToString("HHmmss"),
								Comercio = com.CodigoComercio,
								Terminal = "PC",
								ModoIngreso = "1",
								Tarjeta = request.Tarjeta.NumeroTarjeta,
								FechaExpiracion = request.Tarjeta.FechaExpira,
								Importe = cargo.Monto.ToString(),
								CodigoSeguridad = request.Tarjeta.Cvc2,
								NombreComercio = com.Comercio1,
								CodigoMoneda = "484",
							};
							ServicePointManager.ServerCertificateValidationCallback +=
							(sender, cert, chain, sslPolicyErrors) => true;

							var reqInt = new InspectorBehavior();
							var handler = (AutorizacionPortTypeClient)newAutorizadorWs;
							handler.Endpoint.Behaviors.Clear();
							handler.Endpoint.Behaviors.Add(reqInt);
							var dateTimeInicio = DateTime.Now;
							var autorizarResp = newAutorizadorWs.Autorizar(autorizarReq);
							var dateTimeFin = DateTime.Now;
							var reqXml = reqInt.LastRequestXml;
							var resXml = reqInt.LastResponseXml;

							try
							{
								ThreadPool.QueueUserWorkItem(delegate
								{
									new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 10, 1,
										reqXml, resXml, 0, anul.idAnulacion);
								});
							}
							catch (Exception e)
							{
								Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
							}

							anul.Autorizado = autorizarResp.CodigoRespuesta.Trim() == ("-1");
							anul.NumAutorizacion = Convert.ToInt32(autorizarResp.CodigoAutorizacion);
							ctx.SaveChanges();
							int codresp = Convert.ToInt32(autorizarResp.CodigoRespuesta);
							CodigosRespuestaSQL cr = ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
							if (cr != null)
								response.UserResponse = cr.Descripcion;
							response.Success = anul.Autorizado == true ? 1 : 0;
							response.NumeroAutorizacion = autorizarResp.CodigoAutorizacion;
							response.IdAnulacion = anul.idAnulacion;
							response.CodigoRespuesta = Convert.ToInt32(autorizarResp.CodigoRespuesta);
							response.FechaCreacion = Convert.ToDateTime(anul.Fecha).ToString("yyyy-MM-dd HH:mm:ss");
						}
						else if (request.Tarjeta.Procesador == 2)
						{
							var reverso = new UndoPaymentDTO
							{
								Amount = cargo.Monto,
								CurrencyCode = 484,
								GeneralAccountNumber = Convert.ToInt64(request.Tarjeta.Cuenta),
								PaymentCode = Convert.ToString(request.IdCargo),
								PaymentDate = Convert.ToDateTime(cargo.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss"),
								UndoReason = "Reverso por webservice",
							};
							var reqInt = new InspectorBehavior();
							var handler = (PaymentServiceClient)paymentPetrus;
							handler.Endpoint.Behaviors.Clear();
							handler.Endpoint.Behaviors.Add(reqInt);
							var dateTimeInicio = DateTime.Now;
							var rev = paymentPetrus.UndoPayment(reverso);
							var dateTimeFin = DateTime.Now;
							var reqXml = reqInt.LastRequestXml;
							var resXml = reqInt.LastResponseXml;

							try
							{
								ThreadPool.QueueUserWorkItem(delegate
								{
									new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 3, 2,
										reqXml, resXml, anul.idAnulacion);
								});
							}
							catch (Exception e)
							{
								Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
							}

							anul.Autorizado = rev.paymentInfo.PaymentCode.Trim() == ("-1");
							anul.NumAutorizacion = rev.ReturnExecution == PaymentService.ReturnExecution.SUCCESS ? 1 : 0;
							anul.MensajeRespuesta = rev.ReturnExecution == PaymentService.ReturnExecution.SUCCESS
								? "Exito"
								: rev.Errors[0].ErrorText;
							response.Success = anul.Autorizado == true ? 1 : 0;
							response.NumeroAutorizacion = rev.paymentInfo.PaymentCode;
							response.CodigoRespuesta = anul.Autorizado == true ? 1 : 999;
							response.FechaCreacion = Convert.ToDateTime(anul.Fecha).ToString("ddMMyyyy HH:mm:ss");
							response.IdAnulacion = anul.idAnulacion;
							response.UserResponse = anul.MensajeRespuesta;
							ctx.SaveChanges();
						}
					}
					catch (Exception ex)
					{
						anul.Autorizado = false;
						anul.NumAutorizacion = 0;
						anul.MensajeRespuesta = "Error al eliminar el reverso : " + ex.Message;
						response.Success = 0;
						response.CodigoRespuesta = 999;
						response.IdAnulacion = anul.idAnulacion;
						response.UserResponse = anul.MensajeRespuesta;
						ctx.SaveChanges();
					}
				}
			}
			return response;
		} 
	}
}