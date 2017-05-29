using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using MySql.Data.MySqlClient;
using wsBroxel.App_Code.RequestResponses;
using wsBroxel.App_Code.SolicitudBL;
using wsBroxel.App_Code.VCBL;
using wsBroxel.App_Code.VCBL.Models;
using wsBroxel.GeneralAccountService;
using wsBroxel.wsMovementsService;
using wsBroxel.wsSMS;

namespace wsBroxel.App_Code.GenericBL
{
	public class PagoServiciosBL 
	{
		private wsBroxel.BroxelService wsBroxel = new BroxelService();
		/// <summary>
		/// Realiza el pago de servicios tanto Credencial como Petrus.
		/// </summary>
		/// <param name="request">Numero de tarjeta</param>
		/// <param name="TransaccionWS">Numero de transaccion del WS</param>
		/// <param name="isTA">Si es tiempo aire</param>
		public PagoServicioResp PagoServicios(PagoServiciosTarjeta request, long TransaccionWS, bool isTA)
		{
			try
			{
				using (var ctxBroxelCo = new broxelco_rdgEntities())
				{
					var pagoServicioCtrl = new PagoServiciosCtrl
					{
						cuenta = request.cuenta,
						monto = request.monto,
						comision = request.comision,
						idWSPagoServicios = request.idWSPagoServicios,
						idOperComision = 0,
						idOperMonto = 0,
						idPagoSerRev = 0,
						FechaHora = DateTime.Now,
						isTA = (sbyte)(isTA ? 1 : 0),
					};
					try
					{
						ctxBroxelCo.PagoServiciosCtrl.Add(pagoServicioCtrl);
						ctxBroxelCo.SaveChanges();
					}
					catch (Exception ex) {
						return new PagoServicioResp { IdTransac = 0, Message = "No es posible afectar la base de datos de pago de servicios"+ ex.Message };
					}
					// Obtiene el comercio para la tx de tiempo aire
					var idComercioTA = "";
					if (isTA)
					{
						idComercioTA = new SQLDataAccess().GetTiempAireIdComercio(TransaccionWS);
						if (idComercioTA == "")
							return new PagoServicioResp { IdTransac = 0, Message = "No es posible determinar la comision para la operacion de Tiempo Aire" };
					}

					// Valida los datos enviados
					Tarjeta t;

					if (!string.IsNullOrEmpty(request.numTarjeta) && !string.IsNullOrEmpty(request.fechaVencimiento) && !string.IsNullOrEmpty(request.cvc))
					{
						t = new Tarjeta
						{
							Cuenta = request.cuenta,
							Cvc2 = request.cvc,
							FechaExpira = request.fechaVencimiento,
							NombreTarjeta = " ",
							NumeroTarjeta = request.numTarjeta,
							Procesador = 2,
						};
						var json = new VCBL.VCardBL().GetCardDataPagoServicio(t, pagoServicioCtrl.id);
						if (json.IdTran == 0) { return new PagoServicioResp { IdTransac = 0, Message = "Error al realizar la transacción" }; }
						var cuenta = Helper.GetCuentaFromTarjeta(t.NumeroTarjeta);
						t.Cuenta = cuenta;
					}
					else
					{
						t = Helper.GetTarjetaFromCuenta(request.cuenta);

					}

					if (t == null)
						return new PagoServicioResp { IdTransac = 0, Message = "Cuenta inexistente" };

					if (string.IsNullOrEmpty(t.NumeroTarjeta))
						return new PagoServicioResp { IdTransac = 0, Message = "Tarjeta inexistente." };
					if (request.monto <= 0)
						return new PagoServicioResp { IdTransac = 0, Message = "El monto a pagar no puede ser igual a cero" };

					decimal saldos = 0.0m;
					if (t.Procesador == 1)
					{
						var saldoCredencial = wsBroxel.GetSaldosPorCuenta(t.Cuenta, "2", "RealizaDisposicion1");
						saldos = saldoCredencial.Saldos.DisponibleCompras;
					}
					else if(t.Procesador == 2)
					{
						IGeneralAccountService _cuentaWsPetrus = new GeneralAccountServiceClient("BasicHttpBinding_IGeneralAccountService", "http://" + ConfigurationManager.AppSettings["PetrusHost"] + ":" + ConfigurationManager.AppSettings["PetrusPort"] + "/IssuerCustomer.ClientWS.ServiceHost/Services/GeneralAccountService.svc");
						var balance = new GetBalanceDTO { CardNumber = t.NumeroTarjeta };
						GetBalanceResponseDTO cr = _cuentaWsPetrus.GetBalance("103", balance);
						saldos = Convert.ToInt32(cr.AccountBalance);
					}
					if (saldos >= request.monto + request.comision)
					{
						// Realiza cargo del monto
						var idComercio = isTA ? Convert.ToInt32(idComercioTA) : 2115;
						var cargo1 = wsBroxel.SetCargo(Convert.ToDecimal(request.monto), t.NombreTarjeta, t.NumeroTarjeta, t.Cvc2, t.FechaExpira, TransaccionWS.ToString(CultureInfo.InvariantCulture), "PAGOSERV", 99999, idComercio);
						if (cargo1.Success == 1)
						{

							pagoServicioCtrl.idOperMonto = cargo1.IdMovimiento;
							ctxBroxelCo.SaveChanges();
							// Realiza cargo de la comision si existe comisión 
							if (request.comision > 0 && !isTA)
							{
								var cargo2 = wsBroxel.SetCargo(Convert.ToDecimal(request.comision), t.NombreTarjeta, t.NumeroTarjeta, t.Cvc2, t.FechaExpira, TransaccionWS.ToString(CultureInfo.InvariantCulture), "PAGOCONSERV", 99999, 2116);
								if (cargo2.Success == 1)
								{
									pagoServicioCtrl.idOperComision = cargo2.IdMovimiento;
									ctxBroxelCo.SaveChanges();
									return new PagoServicioResp { IdTransac = pagoServicioCtrl.id, Message = "Operación Exitosa" };
								}
								else
									throw new Exception("No fue posible realizar el cargo por la comision del pago de servicio");
							}
							else
							{
								return new PagoServicioResp { IdTransac = pagoServicioCtrl.id, Message = "Operación Exitosa" };
							}
						}
						else
							throw new Exception("No fue posible realizar el cargo por el pago de servicio");
					}
					else
						return new PagoServicioResp { IdTransac = 0, Message = "Saldo insuficiente para el pago de servicios" };
				}
			}
			catch (Exception ex)
			{
				return new PagoServicioResp { IdTransac = 0, Message = "Ocurrio un error al realizar el pago: " + ex.Message };
			}
			
		}

		/// <summary>
		/// Realiza el reverso del pago de servicio, utilizando el id de la transaccion con el cual se hizo el pago.
		/// </summary>
		/// <param name="IdTransac">Númeror identifica el pago de servicio</param>
		public PagoServicioResp PagoReverso(long IdTransac)
		{
			try
			{
				using (var ctxBroxelCo = new broxelco_rdgEntities())
				{
					var pagoServicioOri = ctxBroxelCo.PagoServiciosCtrl.FirstOrDefault(x => x.id == IdTransac);
					if (pagoServicioOri == null)
						return new PagoServicioResp { IdTransac = 0, Message = "No se encuentra la transacción " + IdTransac.ToString(CultureInfo.InvariantCulture) };
					var pagoSerCtrl = new PagoServiciosCtrl
					{
						comision = pagoServicioOri.comision,
						cuenta = pagoServicioOri.cuenta,
						idWSPagoServicios = pagoServicioOri.idWSPagoServicios,
						idPagoSerRev = pagoServicioOri.id,
						idOperComision = 0,
						idOperMonto = 0,
						FechaHora = DateTime.Now,
						tipoOper = "Reverso"
					};

					try
					{
						ctxBroxelCo.PagoServiciosCtrl.Add(pagoSerCtrl);
						ctxBroxelCo.SaveChanges();
					}
					catch (Exception)
					{
						return new PagoServicioResp { IdTransac = 0, Message = "No es posible afectar la base de datos de pago de servicios" };
					}
					//Comprobar si la tarjeta esta en la tabla PscDatosAd
					Tarjeta t;
					var obtenerT = new MySqlDataAccess().ObtenerTarjetaPscDatosAd(pagoServicioOri.id);
					if (obtenerT == null)
					{
						t = Helper.GetTarjetaFromCuenta(pagoSerCtrl.cuenta);
					}
					else
					{
						JVCData des;
						des = new VCardBL().GetDeserealizePagoServicio(obtenerT, pagoServicioOri.id);

						t = new Tarjeta
						{
							Cuenta = des.Clabe,
							Cvc2 = des.Cvv,
							FechaExpira = des.FechaVencimiento,
							NombreTarjeta = des.Nombre,
							NumeroTarjeta = des.Tarjeta,
							Procesador = 2,
						};
					}
					// reversa comision
					if (pagoServicioOri.idOperComision > 0)
					{
						try
						{
							var c = wsBroxel.ReversoCargo(Convert.ToInt32(pagoServicioOri.idOperComision), t.NumeroTarjeta, t.Cvc2, t.FechaExpira, 99999);
							if (c.Success == 1)
							{
								ThreadPool.QueueUserWorkItem(delegate
								{
									MensajeCelularReverso(t.NumeroTarjeta, pagoSerCtrl.cuenta);
								});

								pagoSerCtrl.idOperComision = c.IdAnulacion;
								ctxBroxelCo.SaveChanges();
							}
						}
						catch (Exception x)
						{
							pagoSerCtrl.idOperComision = -1;
							ctxBroxelCo.SaveChanges();
						}
					}
					// reversa monto
					if (pagoServicioOri.idOperMonto > 0)
					{
						try
						{
							var c = wsBroxel.ReversoCargo(Convert.ToInt32(pagoServicioOri.idOperMonto), t.NumeroTarjeta, t.Cvc2, t.FechaExpira, 99999);
							if (c.Success == 1)
							{
								ThreadPool.QueueUserWorkItem(delegate
								{
									MensajeCelularReverso(t.NumeroTarjeta, pagoSerCtrl.cuenta);
								});
								pagoSerCtrl.idOperMonto = c.IdAnulacion;
								ctxBroxelCo.SaveChanges();
							}
						}
						catch (Exception ex)
						{
							pagoSerCtrl.idOperMonto = -1;
							ctxBroxelCo.SaveChanges();
						}
					}
					return new PagoServicioResp { IdTransac = pagoSerCtrl.id, Message = "Operación Exitosa" };
				}
			}
			catch (Exception e)
			{
				return new PagoServicioResp { IdTransac = 0, Message = "Ocurrio un error: " + e.Message };
			}
		}

		/// <summary>
		/// Envio de mensaje SMS para notificar la aplicacion de reversos.
		/// </summary>
		/// <param name="tarjeta">Numero de tarjeta</param>
		/// <param name="cuenta">Numero de cuenta</param>
		public void MensajeCelularReverso(string tarjeta, string cuenta)
		{
			using (var ctx = new broxelco_rdgEntities())
			{
				var ultDig = tarjeta.Substring(tarjeta.Length - 4, 4);
				var ac = ctx.accessos_clientes.FirstOrDefault(a => a.cuenta == cuenta);
				if (ac == null)
				{
					Trace.WriteLine(DateTime.Now.ToString("O") + " EnviaPagoServicioReversoMsg: No se encontró registro en accessos_clientes para la cuenta: " + cuenta + ", imposible enviar mensaje");
					return;
				}
				var msg = ConfigurationManager.AppSettings["RPSSMS"];
				var proxySms = new ServicioSMSClient();
				var host = ConfigurationManager.AppSettings["OtpHostSMS"];
				var cred = new Credenciales
				{
					Username = "br0x3l",
					Password = "bTEax3l",
					Host = !string.IsNullOrEmpty(host) ? host : "http://api.c3ntrosms.com:8585/Api/rec.php"
				};
				var sms = new SMS
				{
					Mensaje = msg.Replace("{0}", ultDig),
					Telefono = ac.celular
				};
			    proxySms.EnviarSMSC3ntro(sms, cred);
			}
		}

		
	}
}