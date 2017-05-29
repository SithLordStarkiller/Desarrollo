using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.Objects;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using IdSecure;
using Microsoft.SqlServer.Server;
using wsBroxel.App_Code;
using wsBroxel.App_Code.Online;
using wsBroxel.App_Code.SolicitudBL;
using wsBroxel.App_Code.TokenBL;
using wsBroxel.App_Code.Utils;
using wsBroxel.wsOperaciones;
using wsBroxel.wsSMS;
using wsBroxel.wsSPEI;
using wsBroxel.App_Code.RequestResponses;
using wsBroxel.App_Code.GenericBL;
using wsBroxel.App_Code.CargosBL;
using wsBroxel.App_Code.VCBL.Models;
using wsBroxel.App_Code.VCBL;
using MySql.Data.MySqlClient;

namespace wsBroxel
{
    /// <summary>
    /// Summary description for Disposiciones
    /// </summary>
    [WebService(Namespace = "DisposicionesBroxel")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsDisposiciones : System.Web.Services.WebService
    {
        #region Atributos
        wsSPEI.IwsSPEIClient wsSpei = new IwsSPEIClient();
        private wsBroxel.BroxelService wsBroxel = new BroxelService();
        private Decimal? Comision;
        private Decimal ComisionIVA;
        #endregion

        #region MetodosPrivados

        /// <summary>
        /// Envia semilla por SMS
        /// </summary>
        /// <param name="seed">semilla</param>
        /// <param name="celular">telefono celular</param>
        /// <returns>true si pudo enviar SMS, false en caso de error</returns>
        private bool MandaSeedSMS(string seed, string celular)
        {
            var msg = ConfigurationManager.AppSettings["DispSMS"];
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
                Mensaje = msg.Replace("{0}", seed),
                Telefono = celular
            };
            var res = proxySms.EnviarSMSC3ntro(sms, cred);
            if (!res.Enviado)
                Trace.WriteLine(DateTime.Now.ToString("O") + " MandaSeedSMS: Ocurrio un error al enviar el SMS: " + res.Resultado);
            return res.Enviado;
        }

        private bool ValidaCLABE(string clabe)
        {
            int suma = 0;
            int[] ponderacion = { 3, 7, 1 };
            if (Regex.IsMatch(clabe, "[0-9]{18}"))
            {
                for (int i = 0; i < 17; i++)
                {
                    suma += (ponderacion[(i + 3) % 3] * clabe[i]) % 10;
                }
                suma = 10 - (suma % 10);
                suma = suma == 10 ? 0 : suma;
                if (suma.ToString(CultureInfo.InvariantCulture) == clabe[17].ToString())
                    return true;
            }
            return false;
        }

        private Decimal? CalculaComision(String NumCuenta, Decimal Monto)
        {
            broxelco_rdgEntities _broxelcoRdgEntities = new broxelco_rdgEntities();
            var maq = _broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == NumCuenta);
            if (maq != null)
            {
                if (CheckGeneraComision(maq) == 0)
                    return 0.0M;
                var detalleCliente = Entities.Instance.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == maq.clave_cliente && x.Producto == maq.producto);
                if (detalleCliente == null)
                    return CalculaComisionPorProducto(maq.producto, Monto);
                switch (detalleCliente.TipoComisionDisposicion)
                {
                    case 0: // NO PUEDE HACER --  NUNCA DEBERIA DE LLEGAR
                        return null;
                    case 1: // PORCENTAJE
                        return Monto * Convert.ToDecimal(detalleCliente.ComisionDisposicion);
                    case 2: // MONTO FIJO
                        return Convert.ToDecimal(detalleCliente.ComisionDisposicion);
                    default:
                        return CalculaComisionPorProducto(detalleCliente.Producto, Monto);
                }
            }
            return null;
        }

        private Decimal? CalculaComisionPorProducto(String Producto, Decimal Monto)
        {
            var producto = Entities.Instance.ProductosBroxel.FirstOrDefault(x => x.codigo == Producto);
            if (producto != null && (producto.DisposicionEfectivo == 3 || producto.DisposicionEfectivo == 4))
            {
                if (producto.DisposicionEfectivo == 3)
                    return Monto * Convert.ToDecimal(producto.ComisionDisposicion);
                if (producto.DisposicionEfectivo == 4)
                    return Convert.ToDecimal(producto.ComisionDisposicion);
            }
            return null;
        }
        /* MLS Vieja Funcionalidad de reverso de cargos , se queda*/
        public void ReversaCargos(long IdDisposicion)
        {
            try
            {
                broxelco_rdgEntities _broxelcoRdgEntities = new broxelco_rdgEntities();
                //var transferEnviada = _broxelcoRdgEntities.Transferencias.Where(x=>x.)
                var cargos = _broxelcoRdgEntities.CargosDisposicionesEfectivo.Where(x => x.IdDisposicion == IdDisposicion && x.Autorizado == 1).ToList();
                Tarjeta t = Helper.GetTarjetaFromCuenta(cargos[0].NumCuenta);
                foreach (var cargo in cargos)
                {
                    CargoDeleteResponse c = wsBroxel.ReversoCargo(Convert.ToInt32(cargo.IdMovimiento), t.NumeroTarjeta, t.Cvc2, t.FechaExpira, 795);
                    cargo.IdReverso = c.IdAnulacion;
                    cargo.FechaHoraReverso = Convert.ToDateTime(c.FechaCreacion);
                    cargo.AutorizadoReverso = c.Success;
                    cargo.NumAutorizacionReverso = c.NumeroAutorizacion;
                }
                _broxelcoRdgEntities.SaveChanges();
            }
            catch (Exception e)
            {
                Helper.SendMail("mauricio.lopez@broxel.com", "mauricio.lopez@broxel.com", "Error al reversar cargos en disposion", "Disp: ." + IdDisposicion + ". No pudo reversar los cargos y tenia que hacerlo " + e, "9419Ivyc");
            }
        }

        private string PrintCurrency(Decimal value)
        {
            try
            {
                return (Convert.ToDecimal(value).ToString("C", CultureInfo.CurrentCulture)).Insert(1, " ");
            }
            catch (Exception)
            {
                return "ERR";
            }
        }

        private void NotificaCLABEAgregada()
        {
            //TODO: Enviar notificacion de CLABE ... validar tiempo de espera
            //Helper.SendMail("broxelonline@broxe.com", "marisela.sanchez@broxel.com, omar.gonzalez@broxel.com, javier.lopez@broxel.com, juan.pastrana@broxel.com, aldo.garcia@broxel.com", |"Disposicion", p, "67896789");
        }

        [WebMethod]

        public void NotificaEnvio(string beneficiario, string clabe, Decimal monto, string claveCliente)
        {
            NotificaEnvioPorParteDeBroxel(beneficiario, clabe, monto, claveCliente);
        }

        private void NotificaEnvioPorParteDeBroxel(string beneficiario, string clabe, Decimal monto, string claveCliente)
        {
            try
            {
                String body = String.Format("Existe un disposición de crédito del cliente {3} para el beneficiario {0} a la cuenta clabe {1} por un monto de {2} favor de fondear la cuenta No. 646180110300000000", beneficiario, clabe, PrintCurrency(monto), claveCliente);
                Helper.SendMail("broxelonline@broxel.com", "marisela.sanchez@broxel.com, omar.gonzalez@broxel.com, javier.lopez@broxel.com, juan.pastrana@broxel.com, mauricio.lopez@broxel.com, maximiliano.silva@broxel.com, rodrigo.diazdeleon@broxel.com, d.galindo@broxel.com", "Fondeo por disposición", body, "67896789");
            }
            catch (Exception e)
            {
                //TODO: Guardar en bitacora
            }
        }

        private DisposicionResponse ValidaDisposicion(string numCuenta, Decimal monto)
        {
            var dr = new DisposicionResponse();

            var maxTransferAmount = Convert.ToDecimal(ConfigurationManager.AppSettings["MaxTransferAmount"]);

            if(monto>maxTransferAmount)
            {
                var culture = new CultureInfo("es-MX");
                dr.Valida = false;
                dr.UserResponse = "El monto máximo para esta operación es de " + maxTransferAmount.ToString("C2", culture) + ", intente con un monto menor.";
                return dr;
            }

            if (new MySqlDataAccess().ValidaNivelCuarentenaDisposicion(numCuenta))
            {
                dr.Valida = false;
                dr.UserResponse = "El nivel de seguridad de tu cuenta impide realizar transferencias SPEI, por favor completa tus datos para subir de nivel.";
                return dr;
            }

            var minTransfer = Convert.ToInt32(ConfigurationManager.AppSettings["MinTransfer"]);
            var maxTransfer = Convert.ToInt32(ConfigurationManager.AppSettings["MaxTransfer"]);
            if ((DateTime.Now.Hour < minTransfer || DateTime.Now.Hour > maxTransfer) && (DateTime.Now.DayOfWeek == DayOfWeek.Sunday || DateTime.Now.DayOfWeek == DayOfWeek.Saturday))
            {
                dr.Valida = false;
                dr.UserResponse = "Módulo abierto de " + minTransfer.ToString("D2") + ":00 a " + maxTransfer.ToString("D2") + ":00 hrs. de Lunes a Viernes";
                return dr;
            }
            if (monto < 0)
            {
                dr.Valida = false;
                dr.UserResponse = "No se pueden transferir cantidades negativas";
                return dr;
            }
            //Modulo de validación de transferencia a mejoravit.

            dr = ValidaDisposicionMejoravit(numCuenta, monto);
            if (!dr.Valida)
                return dr;
            dr.Valida = true;
            return dr;
        }

        private DisposicionResponse ValidaDisposicionMejoravit(string numCuenta, Decimal monto)
        {
            try
            {
                var culture = new CultureInfo("es-MX");
                var mySql = new MySqlDataAccess();
                var porcentaje = mySql.MontoPorcentaje(numCuenta);
                if (porcentaje == 0)
                    return new DisposicionResponse { UserResponse = "No aplica porcentaje Multiva", Valida = true };
                if (mySql.ValidaDisposicionesEfectivo(numCuenta))
                    return new DisposicionResponse { UserResponse = "No es posible realizar otra transferencia, esta cuenta ya ha efectuado una transferencia. Por favor revise sus movimientos.", Valida = false };

                var saldos = new BroxelService().GetSaldosPorCuenta(numCuenta, "WebMejoravit",
                    "ValidaDisposicionMejoravit");
                var montoPermitido = saldos.Saldos.LimiteCompra * porcentaje;
                if (monto > montoPermitido)
                    return new DisposicionResponse
                    {
                        UserResponse = "El monto solicitado " + monto.ToString("C2", culture) +
                        " es superior al monto permitido para esta operación: " +
                        montoPermitido.ToString("C2", culture) + ", por favor modifique el monto solicitado.",
                        Valida = false
                    };
                return new DisposicionResponse { UserResponse = "Ok", Valida = true };
            }
            catch (Exception e)
            {
                return new DisposicionResponse { UserResponse = "Surgió un error al validar la disposición: " + e.Message, Valida = false };
            }

        }



        #endregion

        #region MetodosPublicos

        #region PagoServicios

        /// <summary>
        /// Reversa un pago de servicio basado en el id de transacción original
        /// </summary>
        /// <param name="IdTransac"></param>
        /// <returns></returns>
        [WebMethod]
        public PagoServicioResp PagoServicioReverso(long IdTransac)
        {
            /*
            var r = new Random();
            return new PagoServicioResp { IdTransac = r.Next(55555, 99999), Message = "Operación exitosa" };
            */
            var ctxBroxelCo = new broxelco_rdgEntities();
            var pagoServicioOri = ctxBroxelCo.PagoServiciosCtrl.FirstOrDefault(x => x.id == IdTransac);
            try
            {
                if(pagoServicioOri==null)
                    return new PagoServicioResp{IdTransac = 0,Message = "No se encuentra la transacción " + IdTransac.ToString(CultureInfo.InvariantCulture)};
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

                var t = Helper.GetTarjetaFromCuenta(pagoSerCtrl.cuenta);
                if(t==null)
                    return new PagoServicioResp{IdTransac = 0, Message = "No se encontraron datos para la cuenta " + pagoSerCtrl.cuenta};
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
                                EnviaPagoServicioReversoMsg(t.NumeroTarjeta, pagoSerCtrl.cuenta);
                            });

                            pagoSerCtrl.idOperComision = c.IdAnulacion;
                            ctxBroxelCo.SaveChanges();
                        }
                    }
                    catch (Exception)
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
                                EnviaPagoServicioReversoMsg(t.NumeroTarjeta, pagoSerCtrl.cuenta);
                            });
                            pagoSerCtrl.idOperMonto = c.IdAnulacion;
                            ctxBroxelCo.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        pagoSerCtrl.idOperMonto = -1;
                        ctxBroxelCo.SaveChanges();
                    }
                }
                return new PagoServicioResp{IdTransac = pagoSerCtrl.id, Message = "Operación Exitosa"};
            }
            catch (Exception e)
            {
                return new PagoServicioResp { IdTransac = 0, Message = "Ocurrio un error: " + e.Message };
            }
        }
		/// <summary>
		/// Reversa un pago de servicio basado en el id de transacción original
		/// </summary>
		/// <param name="IdTransac"></param>
		/// <returns></returns>
		[WebMethod]
		public PagoServicioResp PagoServicioReversoNew(long IdTransac)
		{
			try
			{
				var reverso = new PagoServiciosBL().PagoReverso(IdTransac);
				return new PagoServicioResp { IdTransac = reverso.IdTransac, Message = reverso.Message };
			}
			catch (Exception e)
			{
				return new PagoServicioResp { IdTransac = 0, Message = "Ocurrio un error: " + e.Message };
			}
		}
		/// <summary>
		/// Metodo para pago de servicio, genera un movimiento por el pago del servicio y otro por la comisión.
		/// </summary>
		/// <param name="NumCuenta"></param>
		/// <param name="Monto"></param>
		/// <param name="MontoComision"></param>
		/// <returns></returns>
		[WebMethod]
        public PagoServicioResp PagoServicio(string NumCuenta, Decimal Monto, Decimal MontoComision, long TransaccionWS, bool isTA)
        {
            //var r = new Random();
            //return new PagoServicioResp { IdTransac = r.Next(55555, 99999), Message = "Operación exitosa" };
            
            // Crea entrada en la tabla de log.
            var ctxBroxelCo = new broxelco_rdgEntities();
            var pagoServicioCtrl = new PagoServiciosCtrl
            {
                cuenta = NumCuenta,
                monto = Monto,
                comision = MontoComision,
                idWSPagoServicios = TransaccionWS.ToString(CultureInfo.InvariantCulture),
                idOperComision = 0,
                idOperMonto = 0,
                idPagoSerRev = 0,
                FechaHora = DateTime.Now,
                isTA = (sbyte)(isTA?1:0)
            };

            try
            {
                ctxBroxelCo.PagoServiciosCtrl.Add(pagoServicioCtrl);
                ctxBroxelCo.SaveChanges();
            }
            catch (Exception)
            {
                return new PagoServicioResp { IdTransac = 0, Message = "No es posible afectar la base de datos de pago de servicios"};
            }

            try
            {
                // Obtiene el comercio para la tx de tiempo aire
                var idComercioTA = "";
                if (isTA)
                {
                    idComercioTA = new SQLDataAccess().GetTiempAireIdComercio(TransaccionWS);
                    if(idComercioTA=="")
                        return new PagoServicioResp { IdTransac = 0, Message = "No es posible determinar la comision para la operacion de Tiempo Aire" };
                }
                
                // Valida los datos enviados
                var t = Helper.GetTarjetaFromCuenta(NumCuenta);

                if (t != null)
                {
                    if(string.IsNullOrEmpty(t.NumeroTarjeta))
                        return new PagoServicioResp { IdTransac = 0, Message = "Tarjeta inexistente." };
                    if(Monto<=0)
                        return new PagoServicioResp { IdTransac = 0, Message = "El monto a pagar no puede ser igual a cero" };
                    /*
                     * MLS Comisiones podrian venir de cero
                    if(!isTA)
                        if (Monto <= 0)
                            return new PagoServicioResp { IdTransac = 0, Message = "La comision no puede ser igual a cero" };
                    */
                    // Valida el saldo total del pago de servicio

                    var saldos = wsBroxel.GetSaldosPorCuenta(NumCuenta, "2", "RealizaDisposicion1");
                    if (saldos.Saldos.DisponibleCompras >= Monto + MontoComision)
                    {
                        // Realiza cargo del monto
                        var idComercio = isTA ? Convert.ToInt32(idComercioTA) : 2115;
                        var cargo1 = wsBroxel.SetCargo(Convert.ToDecimal(Monto), t.NombreTarjeta, t.NumeroTarjeta, t.Cvc2, t.FechaExpira, TransaccionWS.ToString(CultureInfo.InvariantCulture), "PAGOSERV", 99999, idComercio);
                        if (cargo1.Success == 1)
                        {
                            pagoServicioCtrl.idOperMonto = cargo1.IdMovimiento;
                            ctxBroxelCo.SaveChanges();
                            // Realiza cargo de la comision si existe comisión 
                            if (MontoComision > 0 && !isTA)
                            {
                                var cargo2 = wsBroxel.SetCargo(Convert.ToDecimal(MontoComision), t.NombreTarjeta, t.NumeroTarjeta, t.Cvc2, t.FechaExpira, TransaccionWS.ToString(CultureInfo.InvariantCulture), "PAGOCONSERV", 99999, 2116);
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
                else
                {
                    return new PagoServicioResp{ IdTransac = 0, Message = "Cuenta inexistente"};
                }

            }
            catch (Exception e)
            {
                if (pagoServicioCtrl.id > 0)
                    PagoServicioReverso(pagoServicioCtrl.id);
                return new PagoServicioResp { IdTransac = 0, Message = "Ocurrio un error al realizar el pago: " + e.Message };
            }
        }

		/// <summary>
		/// Metodo para pago de servicio, genera un movimiento por el pago del servicio y otro por la comisión.
		/// </summary>
		/// <param name="Tarjeta"></param>
		/// <param name="FechaVencimiento"></param>
		/// <param name="CVC"></param>
		/// <param name="NumCuenta"></param>
		/// <param name="Monto"></param>
		/// <param name="MontoComision"></param>
		/// <returns></returns>
		[WebMethod]
		public PagoServicioResp PagoServicioPetrusCredito(string Tarjeta, string FechaVencimiento, string CVC, string NumCuenta, Decimal Monto, Decimal MontoComision, long TransaccionWS, bool isTA)
		{
			var pago = new PagoServicioResp();
			try
			{
				var p = new PagoServiciosTarjeta
				{
					comision = MontoComision,
					cuenta = NumCuenta,
					cvc = CVC,
					FechaHora = DateTime.Now,
					fechaVencimiento = FechaVencimiento,
					idWSPagoServicios = TransaccionWS.ToString(CultureInfo.InvariantCulture),
					idOperComision = 0,
					idOperMonto = 0,
					idPagoSerRev = 0,
					monto = Monto,
					numTarjeta = Tarjeta,
				};
				return pago = new PagoServiciosBL().PagoServicios(p, TransaccionWS, isTA);
			}
			catch (Exception e)
			{
				if (pago.IdTransac > 0)
					PagoServicioReverso(pago.IdTransac);
				return new PagoServicioResp { IdTransac = 0, Message = "Ocurrio un error al realizar el pago: " + e.Message };
			}
		}
        /*
		/// <summary>
		/// Metodo para pago de servicio, genera un movimiento por el pago del servicio y otro por la comisión.
		/// </summary>
		/// <param name="tarjeta"></param>
		/// <param name="fecha"></param>
		/// <param name="cvc"></param>
		/// <returns></returns>
		[WebMethod]
		public VCardData pruebaServicio(string tarjeta, string fecha, string cvc)
		{
			try
			{
				var t = new Tarjeta { NumeroTarjeta = tarjeta, Cvc2 = cvc, FechaExpira = fecha };
				var a = new VCardData{ };//.GetCardDataPagoServicio(t, 1);
				string cs = "Server=10.210.66.59;Database=broxelco_rdg;User Id=appnetuserdes;Password=2sZcuiawnWkb;";

				MySqlConnection conn = null;
				MySqlDataReader rdr = null;
				try
				{
					conn = new MySqlConnection(cs);
					conn.Open();

					string query = @"select IdPsc,strAd from PscDatosAd where `IdPsc` = 1";

					MySqlCommand cmd = new MySqlCommand(query, conn);
					rdr = cmd.ExecuteReader();

					while (rdr.Read())
					{
						var id = rdr.GetString(0);
						var str = rdr.GetString(1);
						var des = new VCardBL().GetDeserealizePagoServicio(str, Convert.ToInt32(id));
					}
					conn.Close();
				}
				catch (MySqlException ex)
				{
					Console.WriteLine("Error: {0}", ex.ToString());

				}
				
				return a;
			}
			catch (Exception e)
			{
				return new VCardData { IdTran = 0, Track1 = "Ocurrio un error: " + e.Message, Track2 = "prueba" };
			}
		}
        */
		private void EnviaPagoServicioReversoMsg(string tarjeta, string cuenta)
        {
            try
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
            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + " EnviaPagoServicioReversoMsg: Ocurrio un error la procesar el mensaje para : " + cuenta + ": " + e);
            }
        }

		private void EnviaPagoServicioReversoMsgNew(string tarjeta, string cuenta)
		{
			try
			{
				new PagoServiciosBL().MensajeCelularReverso(tarjeta, cuenta);
			}
			catch (Exception e)
			{
				Trace.WriteLine(DateTime.Now.ToString("O") + " EnviaPagoServicioReversoMsg: Ocurrio un error la procesar el mensaje para : " + cuenta + ": " + e);
			}
		}

		#endregion

		#region Disposiciones

		/// <summary>
		/// Genera código autenticación para transferencias (OTP).
		/// </summary>
		/// <param name="idUser">IdSecure del usuario que invoca el método.</param>
		/// <param name="numCuenta">Número de cuenta de cargo.</param>
		/// <returns></returns>
		[WebMethod]
        public bool GenerarOtpDisposicion(int idUser, string numCuenta)
        {
            //MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return false;
            }
            idUser = id;

            var mySql = new MySqlDataAccess();
            var broxelcoRdgEntities = new broxelco_rdgEntities();
            var acceso = broxelcoRdgEntities.accessos_clientes.FirstOrDefault(x => x.cuenta == numCuenta && x.IdUsuarioOnlineBroxel == idUser);
            if (acceso == null) return false;
            const string appId = "DispBroxelUsr";
            var password = mySql.GetTokenUsuarioApp(appId);
            if (password == null) return false;
            var pwd = AesEncrypterToken.Encrypt(password + '|' + DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:sszzz"));
            var seed = new TokenService().GetTokenSeed(numCuenta, appId, pwd, acceso.celular);
            var otp = TokenBroxel.GeneraNumero(seed, DateTime.Now);
            var vigencia = mySql.GetTokenAppTolerance(appId);
            var updToken = mySql.UpdOtpToken(otp, numCuenta, appId);
            var sms = MandaSeedSMS(otp, acceso.celular);
            if (ConfigurationManager.AppSettings["EnviarCorreoOtp"] == "1")
            {
                var mail = GenericBL.EnviarMailOtp(acceso.Email, otp, vigencia);
                return updToken && (sms || mail);
            }
            else
                return updToken && sms;
        }

        /// <summary>
        /// Genera código autenticación (OTP) para Telecom.
        /// </summary>
        /// <param name="numCuenta">Número de cuenta del usuario que invoca el método.</param>
        /// <returns></returns>
        [WebMethod]
        public bool GenerarOtpTelecom(string numCuenta)
        {
            var mySql = new MySqlDataAccess();
            var broxelcoRdgEntities = new broxelco_rdgEntities();
            var maquila = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == numCuenta);
            if (maquila == null) return false;
            const string appId = "OtpTelecomUsr";
            var password = mySql.GetTokenUsuarioApp(appId);
            if (password == null) return false;
            var pwd = AesEncrypterToken.Encrypt(password + '|' + DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:sszzz"));
            var seed = new TokenService().GetTokenSeed(numCuenta, appId, pwd, maquila.Telefono);
            var otp = TokenBroxel.GeneraNumero(seed, DateTime.Now);
            var vigencia = mySql.GetTokenAppTolerance(appId);
            var updToken = mySql.UpdOtpToken(otp, numCuenta, appId);
            var sms = MandaSeedSMS(otp, maquila.Telefono);
            if (ConfigurationManager.AppSettings["EnviarCorreoOtp"] != "1") return updToken && sms;
            var mail = GenericBL.EnviarMailOtp(maquila.email, otp, vigencia);
            return updToken && (sms || mail);
        }

        /// <summary>
        /// Valida el código de autenticación (OTP) para Telecom.
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        public DisposicionResponse ValidaOtpTelecom(string numCuenta, string otp)
        {
            if (string.IsNullOrEmpty(otp))
                return new DisposicionResponse
                {
                    UserResponse = "Número de autenticación Inválido.",
                    Valida = false
                };
            if (!new TokenServiceBL().ValidaOtpTemp(numCuenta, "OtpTelecomUsr", otp))
                return new DisposicionResponse
                {
                    UserResponse = "El número de autenticación no es válido.",
                    Valida = false
                };
            return new DisposicionResponse
            {
                NumeroAutorizacion = otp,
                UserResponse = "Otp Validado con éxito.",
                Valida = true
            };
        }

        /// <summary>
        /// Genera la semilla (seed).
        /// </summary>
        /// <param name="idUser">IdSecure del usuario que invoca el método.</param>
        /// <param name="numCuenta">Número de cuenta al que se le asigna la semilla (seed).</param>
        /// <returns></returns>
        [WebMethod]
        public string GenerarSeed(int idUser, string numCuenta)
        {
            var seed = string.Empty;
            try
            {
                //MLS idUserSecure
                var id = new IdSecureComp().GetIdUserValid(idUser);
                if (id == 0)
                {
                    return string.Empty; ;
                }
                idUser = id;

                var mySql = new MySqlDataAccess();
                var broxelcoRdgEntities = new broxelco_rdgEntities();
                var acceso = broxelcoRdgEntities.accessos_clientes.FirstOrDefault(x => x.cuenta == numCuenta && x.IdUsuarioOnlineBroxel == idUser);
                if (acceso == null) return string.Empty;
                const string appId = "DispBroxelUsr";
                var password = mySql.GetTokenUsuarioApp(appId);
                if (password == null) return string.Empty;
                var pwd = AesEncrypterToken.Encrypt(password + '|' + DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:sszzz"));
                seed = new TokenService().GetTokenSeed(numCuenta, appId, pwd, acceso.celular);
            }
            catch (Exception)
            {
                seed = string.Empty;
            }
            return seed;
        }


        // TODO MLS Validar con Max
        /// <summary>
        /// Realiza disposiciones (Transferencias SPEI)
        /// </summary>
        /// <param name="NumCuenta">Número de cuenta de cargo</param>
        /// <param name="Monto">Monto de la disposición</param>
        /// <param name="IdCLABE">Id de la Clabe interbancaria asociada al usuario </param>
        /// <param name="otp">OTP</param>
        /// <param name="Usuario">Usuario que invoca al método</param>
        /// <returns></returns>
        [WebMethod]
        public DisposicionResponse RealizaDisposicion(string NumCuenta, decimal Monto, long IdCLABE, string Usuario = "", string otp = "", string concepto = "", string referenciaNumerica = "")
        {
            var dr = new DisposicionResponse();
            var broxelcoRdgEntities = new broxelco_rdgEntities();
            var numeroAutorizacion = "0";
            var numeroTarjeta = "";
            var broxelSQL = new BroxelEntities();
            var refNumerica = 0;
            var referenciaTransferencia = "";  // variable para asignar la referencia que regresa el servicio de transferencia.

            try
            {
                if(!string.IsNullOrEmpty(referenciaNumerica))
                    refNumerica = Convert.ToInt32(referenciaNumerica);
            }
            catch (Exception e)
            {
                return new DisposicionResponse { UserResponse = "La referencia numérica contiene caracteres no válidos", Valida = false };
            }

            //Valida para saber si el comercio esta en black-list
            var valida = new MySqlDataAccess();
            if (valida.ValidaComercioBlackList(NumCuenta))
            {
                return new DisposicionResponse
                {
                    UserResponse = "No puedes realizar la disposición."
                };
            }

            //Valida que hayan pasado mas 30 minutos desde la fecha de creación de la clabe.
            var clabeUsuario = broxelcoRdgEntities.UsuariosOnlineCLABE.FirstOrDefault(x => x.Id == IdCLABE);
            DateTime fechaAltaClabe;
            DateTime fechaActual = DateTime.Now;
            int minutosEspera = 30;
            fechaAltaClabe = clabeUsuario.FechaAlta.AddMinutes(minutosEspera);

            if (fechaAltaClabe > fechaActual) {
                 return new DisposicionResponse { UserResponse = "No puedes utilizar esta cuenta CLABE por el momento, debes esperar 30 minutos a partir de que la diste de alta.", Valida = false };
            }

            if (!string.IsNullOrEmpty(otp))
                if (! new TokenServiceBL().ValidaOtpTemp(NumCuenta, "DispBroxelUsr", otp))
                    return new DisposicionResponse {UserResponse = "El número de autenticación no es válido", Valida = false};

            dr = ValidaDisposicion(NumCuenta, Monto);

            if (!dr.Valida)
                return dr;

            Comision = CalculaComision(NumCuenta, Monto);
            if (Comision != null)
            {
                ComisionIVA = Convert.ToDecimal(Comision * 1.16m);
                var maq = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == NumCuenta);
                if (maq != null)
                {
                    long dispEfectivoId = 0;
                    try
                    {
                        var generaComision = CheckGeneraComision(maq);
                        var t = Helper.GetTarjetaFromCuenta(NumCuenta);
                        if (!String.IsNullOrEmpty(t.NumeroTarjeta))
                        {
                            numeroTarjeta = t.NumeroTarjeta;
                            var tarEnc = t.NumeroTarjeta.Substring(0, 6) + "** ****" + t.NumeroTarjeta.Substring(12);

                            var saldos = wsBroxel.GetSaldosPorCuenta(NumCuenta, "2", "RealizaDisposicion1");
                            if (saldos.Saldos.DisponibleCompras >= Monto + Convert.ToDecimal(ComisionIVA))
                            {
                                var dispEfectivo = new DisposicionesEfectivo
                                {
                                    Monto = (float?)(Monto),
                                    NumCuenta = NumCuenta,
                                    NumTarjeta = tarEnc,
                                    DispPOSAntes = (float?)saldos.Saldos.DisponibleCompras,
                                    FechaHoraCreacion = DateTime.Now,
                                    ClabeDestino = IdCLABE.ToString(CultureInfo.InvariantCulture),
                                    GeneraComision = generaComision,
                                };
                                try
                                {
                                    broxelcoRdgEntities.DisposicionesEfectivo.Add(dispEfectivo);
                                    broxelcoRdgEntities.SaveChanges();
                                }
                                catch (DbEntityValidationException e)
                                {
                                    Trace.WriteLine("Errores de validacion de entidad en RealizaDisposicion:");
                                    foreach (var eve in e.EntityValidationErrors)
                                    {
                                        Trace.WriteLine("Entity of type \""+ eve.Entry.Entity.GetType().Name + "\" in state \"" + eve.Entry.State + "\" has the following validation errors:");
                                        foreach (var ve in eve.ValidationErrors)
                                        {
                                            Trace.WriteLine("- Property: \"" + ve.PropertyName + "\", Error: \"" + ve.ErrorMessage + "\"");
                                        }
                                    }

                                    broxelSQL.LogTransaccionesSQL.Add(new LogTransaccionesSQL
                                    {
                                        Usuario = Usuario,
                                        WS = "wsDisposiciones",
                                        Resultado = e.Message,
                                        Mensaje = e.Message,
                                        NumTarjeta = numeroTarjeta,
                                        NumCuenta = NumCuenta,
                                        NumAutorizacion = numeroAutorizacion,
                                        MetodoEjecucion = "",
                                        FechaHora = DateTime.Now,
                                        Accion = "RealizaDisposición",
                                    });
                                }
                                dispEfectivoId = dispEfectivo.Id;
                                var cargo1 = wsBroxel.SetCargo(Convert.ToDecimal(Monto), t.NombreTarjeta, t.NumeroTarjeta,
                                    t.Cvc2, t.FechaExpira, "0101", "DISPEFE", 795, 312);
                                numeroAutorizacion = cargo1.NumeroAutorizacion;
                                dr.NumeroAutorizacion = cargo1.NumeroAutorizacion;
                                var c1 = new CargosDisposicionesEfectivo
                                {
                                    Autorizado = cargo1.Success,
                                    IdDisposicion = (int)dispEfectivo.Id,
                                    IdMovimiento = cargo1.IdMovimiento,
                                    Monto = (float?)(Monto),
                                    NumCuenta = NumCuenta,
                                    NumTarjeta = tarEnc,
                                    FechaHoraCreacion = Convert.ToDateTime(cargo1.FechaCreacion),
                                    NumAutorizacion = cargo1.NumeroAutorizacion
                                };
                                broxelcoRdgEntities.CargosDisposicionesEfectivo.Add(c1);
                                broxelcoRdgEntities.SaveChanges();
                                if (cargo1.Success == 1)
                                {
                                    CargoResponse cargo2;
                                    if (ComisionIVA > 0)
                                        cargo2 = wsBroxel.SetCargo(ComisionIVA, t.NombreTarjeta, t.NumeroTarjeta, t.Cvc2, t.FechaExpira, "0102", "COMDISPEFE", 796, 313);
                                    else
                                        cargo2 = new CargoResponse
                                        {
                                            CodigoRespuesta = -1,
                                            FechaCreacion = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                            IdMovimiento = 0,
                                            NumeroAutorizacion = "0",
                                            Success = 1,
                                            UserResponse = "Sin Cargo"
                                        };
                                    var c2 = new CargosDisposicionesEfectivo
                                    {
                                        Autorizado = cargo2.Success,
                                        IdDisposicion = (int)dispEfectivo.Id,
                                        IdMovimiento = cargo2.IdMovimiento,
                                        Monto = (float?)(ComisionIVA),
                                        NumCuenta = NumCuenta,
                                        NumTarjeta = tarEnc,
                                        FechaHoraCreacion = Convert.ToDateTime(cargo2.FechaCreacion),
                                        NumAutorizacion = cargo2.NumeroAutorizacion
                                    };
                                    var saldos2 = wsBroxel.GetSaldosPorCuenta(NumCuenta, "2", "RealizaDisposicion2");
                                    if (saldos2 != null)
                                    {
                                        dispEfectivo.DispPOSDespues = (float?)(saldos2.Saldos.DisponibleCompras);
                                        broxelcoRdgEntities.CargosDisposicionesEfectivo.Add(c2);
                                        broxelcoRdgEntities.SaveChanges();
                                        if (cargo2.Success == 1)
                                        {
                                            var clabe = broxelcoRdgEntities.UsuariosOnlineCLABE.FirstOrDefault(x => x.Id == IdCLABE);
                                            if(clabe==null)
                                                throw new Exception("La cuenta CLABE no existe en base de datos.");
                                            RespuestaTransferencia respTransfer = null;
                                            try
                                            {
                                                respTransfer = wsSpei.RegistraTranferenciaConcepto(new TransferenciaConcepto
                                                {
                                                    Clabe = clabe.CLABE,
                                                    Email = clabe.EmailAviso,
                                                    Monto = Convert.ToDecimal(Monto),
                                                    NombreBeneficiario = clabe.Nombre,
                                                    NumCuentaBroxel = maq.num_cuenta,
                                                    RFC = clabe.RFC,
                                                    IdDisposicion = Convert.ToInt32(dispEfectivo.Id), 
                                                    ReferenciaNumerica = refNumerica,
                                                    ConceptoPago = string.IsNullOrEmpty(concepto)?"DISPOSICION CREDITO " + DateTime.Now.ToString("dd-MM-yy"):concepto,
                                                });
                                            }
                                            catch (Exception e)
                                            {
                                                ReversaCargos(dispEfectivo.Id);
                                                dr.Valida = false;
                                                dr.NumeroAutorizacion = "";
                                                dr.UserResponse = "No se ha podido realizar el envío de la transferencia:" + e.Message;
                                                Helper.SendMail("broxelonline@broxel.com", "maximiliano.silva@broxel.com, mauricio.lopez@broxel.com", "Fallo Transfer en RealizaDisposicion WS de Aldo",
                                                    dispEfectivo.Monto + " - " + dispEfectivo.NumTarjeta + " - " + dispEfectivo.NumCuenta + " - " + dispEfectivo.NumTarjeta + " - " + dispEfectivo.ClabeDestino + " - " + e, "67896790");
                                            }
                                            try
                                            {
                                                if (respTransfer != null)
                                                {
                                                    dispEfectivo.ClaveRastreo = respTransfer.ClaveRastreo;
                                                    dispEfectivo.CuentaSTP = respTransfer.CuentaSTP;
                                                    dispEfectivo.EnvioPago = respTransfer.EnvioPago;
                                                    dispEfectivo.FechaPago = respTransfer.FechaPago;
                                                    dispEfectivo.IdTransferencia = respTransfer.IdTransferencia;
                                                    dispEfectivo.Motivo = respTransfer.Descripcion.Length > 95 ? respTransfer.Descripcion.Substring(0, 95) : respTransfer.Descripcion;
                                                    dispEfectivo.ReferenciaPago = respTransfer.ReferenciaPago.ToString();
                                                    dispEfectivo.StatusPago = respTransfer.Codigo;
                                                    referenciaTransferencia = respTransfer.ReferenciaPago.ToString();
                                                    try
                                                    {
                                                        broxelcoRdgEntities.SaveChanges();
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        Helper.SendMail("broxelonline@broxel.com",
                                                        "maximiliano.silva@broxel.com, mauricio.lopez@broxel.com",
                                                        "Fallo Transfer despues de recibirla e intentar guardarla en WS de Aldo",
                                                        dispEfectivo.Id + " - " + dispEfectivo.ClaveRastreo + " - " + dispEfectivo.CuentaSTP + " - " + dispEfectivo.EnvioPago + " - " + dispEfectivo.FechaPago + " - " + dispEfectivo.IdTransferencia + " - " + dispEfectivo.Motivo + " - "
                                                        + dispEfectivo.ReferenciaPago + " - " + dispEfectivo.StatusPago + " - " + e, "67896790");
                                                    }
                                                    if (dispEfectivo.StatusPago == "1")
                                                    {
                                                        NotificaEnvioPorParteDeBroxel(clabe.Nombre, clabe.CLABE, Monto,
                                                            maq.clave_cliente);
                                                        dr.Valida = true;
                                                        dr.UserResponse =
                                                            "El cargo se ha realizado de manera exitosa y la transferencia se encuentra en proceso de envío.";
                                                        
                                                    }
                                                    else
                                                    {
                                                        ReversaCargos(dispEfectivo.Id);
                                                        dr.Valida = false;
                                                        dr.UserResponse =
                                                            "No se ha podido realizar el envío de la transferencia.";
                                                        dr.NumeroAutorizacion = "";
                                                    }
                                                }
                                                else
                                                {
                                                    dr.Valida = false;
                                                    dr.UserResponse = "No se ha podido realizar el envío de la transferencia.";
                                                    dr.NumeroAutorizacion = "";
                                                    broxelcoRdgEntities.SaveChanges();
                                                }
                                            }
                                            catch (Exception e)
                                            {
                         
                                                ReversaCargos(dispEfectivo.Id);
                                                dr.Valida = false;
                                                dr.NumeroAutorizacion = "";
                                                dr.UserResponse =
                                                    "No se ha podido realizar el envío de la transferencia.";
                                                Helper.SendMail("broxelonline@broxel.com", "maximiliano.silva@broxel.com, mauricio.lopez@broxel.com", "Fallo Transfer en RealizaDisposicion WS de Aldo",
                                                dispEfectivo.Monto + " - " + dispEfectivo.NumTarjeta + " - " + dispEfectivo.NumCuenta + " - " + dispEfectivo.NumTarjeta + " - " + dispEfectivo.ClabeDestino + " - " + e, "67896790");
                                            }
                                        }
                                        else
                                        {
                                            broxelcoRdgEntities.SaveChanges();
                                            ReversaCargos(dispEfectivo.Id);
                                            dr.Valida = false;
                                            dr.NumeroAutorizacion = "";
                                            dr.UserResponse = cargo2.UserResponse +
                                                          " -  Error aplicando comisión, se reversará la operación completa.";
                                            Helper.SendMail("broxelonline@broxel.com", "maximiliano.silva@broxel.com, mauricio.lopez@broxel.com", "Fallo Transfer en RealizaDisposicion WS de Aldo",
                                            dispEfectivo.Monto + " - " + dispEfectivo.NumTarjeta + " - " + dispEfectivo.NumCuenta + " - " + dispEfectivo.NumTarjeta + " - " + dispEfectivo.ClabeDestino + " - " + dr.UserResponse, "67896790");
                                        }
                                    }
                                    else
                                    {
                                        ReversaCargos(dispEfectivo.Id);
                                        broxelcoRdgEntities.SaveChanges();
                                        dr.Valida = false;
                                        dr.UserResponse = "Error obteniendo el saldo de la cuenta.";
                                        dr.NumeroAutorizacion = "";
                                        Helper.SendMail("broxelonline@broxel.com", "maximiliano.silva@broxel.com, mauricio.lopez@broxel.com", "Fallo Transfer en RealizaDisposicion WS de Aldo",
                                        dispEfectivo.Monto + " - " + dispEfectivo.NumTarjeta + " - " + dispEfectivo.NumCuenta + " - " + dispEfectivo.NumTarjeta + " - " + dispEfectivo.ClabeDestino + " - " + dr.UserResponse, "67896790");
                                    }
                                }
                                else
                                {
                                    broxelcoRdgEntities.SaveChanges();
                                    dr.Valida = false;
                                    dr.UserResponse = cargo1.UserResponse;
                                    dr.NumeroAutorizacion = "";
                                }
                            }
                            else
                            {
                                broxelcoRdgEntities.SaveChanges();
                                dr.Valida = false;
                                dr.UserResponse = "No se cuenta con el saldo mínimo para realizar esta operación.";
                                dr.NumeroAutorizacion = "";
                            }
                        }
                        else
                        {
                            broxelcoRdgEntities.SaveChanges();
                            dr.Valida = false;
                            dr.UserResponse = "No se encontró la tarjeta";
                            dr.NumeroAutorizacion = "";
                        }
                    }
                    catch (Exception e)
                    {
                        if(dispEfectivoId>0)
                            ReversaCargos(dispEfectivoId);
                        if (e.InnerException.Message == "MalaConfiguracion")
                        {
                            dr.Valida = false;
                            dr.UserResponse = e.Message;
                            dr.NumeroAutorizacion = "";
                        }
                        else
                        {
                            Helper.SendMail("broxelonline@broxel.com",
                                                    "maximiliano.silva@broxel.com, mauricio.lopez@broxel.com",
                                                    "Fallo Transfer despues de revisar maquila en WS de Aldo",
                                                    Monto + " - " + " - " + NumCuenta + " - " + e, "67896790");
                        }
                    }
                }
                else
                {
                    dr.Valida = false;
                    dr.UserResponse = "No se encontró la cuenta";
                    dr.NumeroAutorizacion = "";
                }
            }
            else
            {
                dr.Valida = false;
                dr.UserResponse = "Esta tarjeta no permite realizar disposiciones de efectivo.";
                dr.NumeroAutorizacion = "";
            }
            broxelcoRdgEntities.SaveChanges();

            try
            {
                broxelSQL.LogTransaccionesSQL.Add(new LogTransaccionesSQL
                {
                    Usuario = Usuario,
                    WS = "wsDisposiciones",
                    Resultado = dr.Valida.ToString(),
                    Mensaje = dr.UserResponse,
                    NumTarjeta = numeroTarjeta,
                    NumCuenta = NumCuenta,
                    NumAutorizacion = numeroAutorizacion,
                    MetodoEjecucion = "",
                    FechaHora = DateTime.Now,
                    Accion = "RealizaDisposición",
                });
                broxelSQL.SaveChanges();
            }
            catch (Exception e )
            {
                Trace.WriteLine(string.Format("{0:dd/MM/yyyy HH:mm:ss.fff} error al enviar correo de  transferencias: {1}", DateTime.Now, e.Message));
            }
            // Envía correo de transferencias.
            try
            {
                if(!dr.Valida)
                    return dr;
                var usuario = broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.Usuario == Usuario);
                //String emailUsuario = usuario.CorreoElectronico;

                var datosCorreo = new DatosEmailTransferencias
                {
                    Fecha = DateTime.Now,
                    Monto = Convert.ToDouble(Monto),
                    Usuario = usuario.NombreCompleto,
                    CorreoUsuario = usuario.CorreoElectronico,
                    IdCLABE = IdCLABE,
                    NumeroTarjeta = numeroTarjeta.Substring(numeroTarjeta.Length - 4, 4),
                    Referencia = referenciaNumerica,
                    Concepto = string.IsNullOrEmpty(concepto) ? "DISPOSICION CREDITO " : concepto,
                    NumeroAutorizacion = numeroAutorizacion,
                    Comision = Convert.ToDouble(ComisionIVA),
                    NumeroCuenta = NumCuenta
                };

                new GenericBL().ComposeTranferenciasMail(datosCorreo, TipoCorreo.SPEI);
                //new GenericBL().ComposeTranferenciasMail(DateTime.Now, Convert.ToDouble(Monto), IdCLABE.ToString(), numeroAutorizacion, emailUsuario, "", "", NumCuenta, numeroTarjeta);
            }
            catch (Exception ex)
            {
                Helper.SendMail("broxelonline@broxel.com", "luis.huerta@broxel.com", "ERROR  al enviar correo de transferencia en  RealizaDisposicion", ex.InnerException.Message, "67896789", "Broxel Fintech");
               Trace.WriteLine(string.Format("{0:dd/MM/yyyy HH:mm:ss.fff} error al enviar correo de  transferencias: {1}", DateTime.Now, ex.Message));
            }
          
            return dr;
        }
        // TODO MLS Validar con Max
        /* MLS Cambio en congeladora, no estaba en la version productiva
        private int ComisionesSinCobrar(maquila maq)
        {
            var DetalleClienteBroxel = Entities.Instance.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == maq.clave_cliente && x.Producto == maq.producto);
            if (DetalleClienteBroxel != null)
            {
                return DetalleClienteBroxel.DisposicionesSinCobro;
            }
            throw new Exception("El producto no cuenta con disposición de efectivo", new Exception("MalaConfiguracion"));
        }*/

        private int CheckGeneraComision(maquila maq)
        {
            //TODO: Revisar como se si esta enviada o aplicada OK
            broxelco_rdgEntities _broxelcoRdgEntities = new broxelco_rdgEntities();
            var DetalleClienteBroxel = Entities.Instance.DetalleClientesBroxel.FirstOrDefault(
                    x => x.ClaveCliente == maq.clave_cliente && x.Producto == maq.producto);
            if (DetalleClienteBroxel != null)
            {
                var count = _broxelcoRdgEntities.DisposicionesEfectivo.Count(x => x.NumCuenta == maq.num_cuenta);
                return count >= DetalleClienteBroxel.DisposicionesSinCobro ? 1 : 0;
            }
            throw new Exception("El producto no cuenta con disposición de efectivo", new Exception("MalaConfiguracion"));
        }
        // TODO MLS Validar con Max
        
        [WebMethod]
        public void ReversaCargosProb(int id)
        {
            ReversaCargos(id);
        }
        
        [WebMethod]
        public string ActualizaTransferencia(String claveRastreo, String status, String motivo)
        {
            String Response = String.Empty;
            broxelco_rdgEntities _broxelcoRdgEntities = new broxelco_rdgEntities();
            DisposicionesEfectivo disposicion = null;
            try
            {
                /* MLS Cambio en congeladora, no estaba en la version productiva
                var transfer = _broxelcoRdgEntities.Transferencias.FirstOrDefault(x => x.ClaveRastreo == claveRastreo);
                disposicion = _broxelcoRdgEntities.DisposicionesEfectivo.FirstOrDefault(x => x.Id == transfer.IdDisposicion);
                disposicion.StatusPago = status;
                disposicion.Motivo = motivo;
                _broxelcoRdgEntities.SaveChanges();
                if(status=="2")
                    RevisaComisiones(disposicion);
                Response = "OK";
                 */
                disposicion = _broxelcoRdgEntities.DisposicionesEfectivo.FirstOrDefault(x => x.ClaveRastreo == claveRastreo);
                if (disposicion == null)
                {
                    Response = "NO OK";
                    Helper.SendMail("broxelonline@broxel.com", "marisela.sanchez@broxel.com, omar.gonzalez@broxel.com, javier.lopez@broxel.com, juan.pastrana@broxel.com, mauricio.lopez@broxel.com, maximiliano.silva@broxel.com",
                        "Error en fondeo por disposición", " Fallo al actualizartransferencia claveRastro " + claveRastreo + ", no se encontr esa claveRastreo en DisposicionesEfectivo", "67896789");
                    return Response;
                }
                disposicion.StatusPago = status;
                disposicion.Motivo = motivo;
                _broxelcoRdgEntities.SaveChanges();
                Response = "OK";
            }
            catch (Exception e)
            {
                Response = "NO OK";
                Helper.SendMail("broxelonline@broxel.com", "marisela.sanchez@broxel.com, omar.gonzalez@broxel.com, javier.lopez@broxel.com, juan.pastrana@broxel.com, mauricio.lopez@broxel.com, maximiliano.silva@broxel.com",
                    "Error en fondeo por disposición", " Fallo al actualizartransferencia claveRastro " + claveRastreo + e, "67896789");
            }
            finally
            {
                if (status == "3" || status == "E")
                {
                    if (disposicion != null)
                    {
                        ReversaCargos(disposicion.Id);
                        String body = String.Format("La transferencia con clave de rastreo {0} CLABE Destino : {1} no fue exitosa. Favor de revisarlo <br><br>", disposicion.ClaveRastreo, disposicion.ClabeDestino);
                        Helper.SendMail("broxelonline@broxel.com", "marisela.sanchez@broxel.com, omar.gonzalez@broxel.com, javier.lopez@broxel.com, juan.pastrana@broxel.com, mauricio.lopez@broxel.com, maximiliano.silva@broxel.com", "Fondeo por disposición", body, "67896789");
                    }
                }
            }
            return Response;
        }
        // TODO MLS Validar con Max

        /* MLS Cambio en congeladora, no estaba en la version productiva
        private void RevisaComisiones(DisposicionesEfectivo disposicion)
        {
            broxelco_rdgEntities _broxelcoRdgEntities = new broxelco_rdgEntities();
            var maq = _broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == disposicion.NumCuenta);
            var DetalleClienteBroxel = Entities.Instance.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == maq.clave_cliente && x.Producto == maq.producto);
            int count;
            if (DetalleClienteBroxel != null)
            {
                count = _broxelcoRdgEntities.DisposicionesEfectivo.Count(x => x.NumCuenta == maq.num_cuenta && x.StatusPago == "2" && x.GeneraComision == 0);
                var faltan = ComisionesSinCobrar(maq)-count;
                if (count < ComisionesSinCobrar(maq))
                {
                    //Si hay alguna previa a mi de la cual no tenga respuesta no hago nada.
                    var disposicionesSinNotificacionCreadasAntes = _broxelcoRdgEntities.DisposicionesEfectivo.Count(x => x.FechaHoraCreacion < disposicion.FechaHoraCreacion && disposicion.StatusPago == "1");
                    if (disposicionesSinNotificacionCreadasAntes == 0)
                    {
                        //Ahora reviso todas las que ya tengan respuesta OK, identifico las N (max count) que hagan falta y les reverso la comision.
                        var DisposicionesConRespuestaOkMayores = _broxelcoRdgEntities.DisposicionesEfectivo.Where(x => x.FechaHoraCreacion > disposicion.FechaHoraCreacion && disposicion.StatusPago == "2")
                            .ToList().Take(faltan);
                        foreach (var dispo in DisposicionesConRespuestaOkMayores)
                        {
                            var comision = _broxelcoRdgEntities.CargosDisposicionesEfectivo.Where(x => x.IdDisposicion == dispo.Id && x.Autorizado == 1 && x.Tipo == "COMISION").ToList()[0];
                            Tarjeta t = Helper.GetTarjetaFromCuenta(comision.NumCuenta);
                            CargoDeleteResponse c = wsBroxel.ReversoCargo(Convert.ToInt32(comision.IdMovimiento), t.NumeroTarjeta, t.Cvc2, t.FechaExpira, 795);
                            comision.IdReverso = c.IdAnulacion;
                            comision.FechaHoraReverso = Convert.ToDateTime(c.FechaCreacion);
                            comision.AutorizadoReverso = c.Success;
                            comision.NumAutorizacionReverso = c.NumeroAutorizacion;         
                        }

                    }
                }
            }
            throw new Exception("El producto no cuenta con disposición de efectivo", new Exception("MalaConfiguracion"));
        }*/


        [WebMethod]
        public DisposicionResponse ValidaDisposicion(String NumCuenta, Decimal Monto, String IdCLABE = "", String Usuario = "")
        {
            var dr = new DisposicionResponse();
            try
            {
                dr = ValidaDisposicion(NumCuenta, Monto);
                if (!dr.Valida)
                    return dr;
                dr.Valida = false;
                var clabe = "";
                var broxelcoRdgEntities = new broxelco_rdgEntities();
                if (!string.IsNullOrEmpty(IdCLABE))
                {
                    var clab = Convert.ToInt32(IdCLABE);
                    var usuariosOnlineClabe = broxelcoRdgEntities.UsuariosOnlineCLABE.FirstOrDefault(x => x.Id == clab);
                    if (usuariosOnlineClabe != null)
                        clabe = " con terminación (" + usuariosOnlineClabe.CLABE.Substring(13) + ") ";
                }
                Comision = CalculaComision(NumCuenta, Monto);
                if (Comision != null)
                {
                    ComisionIVA = Convert.ToDecimal(Comision * 1.16m);
                    var maq = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == NumCuenta);
                    if (maq != null)
                    {
                        Tarjeta t = Helper.GetTarjetaFromCuenta(NumCuenta);
                        if (t != null)
                        {
                            var saldos = wsBroxel.GetSaldosPorCuenta(NumCuenta, "2", "ValidaDisposicion");
                            if (saldos.Saldos.DisponibleCompras >= Monto + Comision + Comision * .16m)
                            {
                                dr.Valida = true;
                                dr.UserResponse =
                                    "Se hará un cargo a tu tarjeta de crédito con terminación (" +
                                    t.NumeroTarjeta.Substring(12) + ") " +
                                    "y se abonará a tu cuenta " + clabe + ". El cargo por la disposición de tu línea de crédito será por "
                                    + PrintCurrency(Monto) + " más una comisión de " + ComisionIVA +
                                    ". La comisión ya incluye I.V.A.";
                                return dr;
                            }
                            dr.UserResponse = "No se cuenta con la cantidad mínima para poder disponer de dicha cantidad";
                        }
                        else
                        dr.UserResponse = "No se encontro la tarjeta";
                    }
                    else
                        dr.UserResponse = "No se encontro la cuenta";
                }
                else
                    dr.UserResponse = "Esta tarjeta no permite disposición de efectivo";
            }
            catch (Exception e)
            {
                dr.Valida = false;
                dr.UserResponse = "Ocurrió un error al validar la operación: " + e.Message;
            }
            return dr;
        }

        [WebMethod]
        public List<UsuariosOnlineCLABE> ConsultaCLABEs(int IdUsuarioOnline)
        {
            //MLS idUserSecure
            var idUser = new IdSecureComp().GetIdUserValid(IdUsuarioOnline);
            if (idUser == 0)
            {
                return new List<UsuariosOnlineCLABE>();
            }
            IdUsuarioOnline = idUser;

            broxelco_rdgEntities _broxelcoRdgEntities = new broxelco_rdgEntities();
            
            var clabes = _broxelcoRdgEntities.UsuariosOnlineCLABE.Where(x => x.IdUsuario == IdUsuarioOnline && x.Activa == 1).ToList();
            DateTime fechaAltaClabe;
            DateTime fechaActual = DateTime.Now;
            List<UsuariosOnlineCLABE> listaClabes = new List<UsuariosOnlineCLABE>();
            int minutosEspera = 30;


            foreach (var clabe in clabes)
            {
                //Fecha de alta se añade los 30 minutos de espera para poder utilizarla.
                fechaAltaClabe = clabe.FechaAlta.AddMinutes(minutosEspera);
                clabe.Banco = Entities.Instance.BancosStp.FirstOrDefault(x => x.id == clabe.IdBanco).nombre;

                if (fechaAltaClabe < fechaActual)
                {
                    listaClabes.Add(clabe);
                }
                else
                {
                    clabe.CLABE = "**************" + clabe.CLABE.Substring(14);
                    clabe.Nombre = clabe.Nombre + " - Disponible en: " + (fechaAltaClabe - fechaActual).Minutes + " min.";
                    listaClabes.Add(clabe);
                }
            }

            return listaClabes;
        }


        [WebMethod]
        public string  ConsultaClabePorCuenta(string numeroCuenta)
        {
            var clabe=string.Empty ;
            try
            { 
                 broxelco_rdgEntities _broxelcoRdgEntities = new broxelco_rdgEntities();

                 clabe = _broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == numeroCuenta).CLABE;
          
                if(clabe==null)
                {
                    clabe = string.Empty;
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ConsultaClabePorCuenta - No regreso registros la cuenta");
                }
             }
            catch(Exception ex)
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ConsultaClabePorCuenta -Error: " + ex.Message);
            }

            return clabe;
        }

        [WebMethod]
        public Boolean EliminaCLABE(int IdUsuarioOnline, int idCLABE,  string host= "")
        {
            try
            {
                //MLS idUserSecure
                var idUser = new IdSecureComp().GetIdUserValid(IdUsuarioOnline);
                if (idUser == 0)
                {
                    return false;
                }
                IdUsuarioOnline = idUser;
                broxelco_rdgEntities _broxelcoRdgEntities = new broxelco_rdgEntities();
                var uoc =
                    _broxelcoRdgEntities.UsuariosOnlineCLABE.FirstOrDefault(
                        x => x.IdUsuario == IdUsuarioOnline && x.Id == idCLABE);
                if (uoc != null)
                {
                    uoc.Activa = 0;
                    _broxelcoRdgEntities.Entry(uoc).State = EntityState.Modified;
                    _broxelcoRdgEntities.SaveChanges();

                    try {
                        if (!(String.IsNullOrEmpty(host)) && ConfigurationManager.AppSettings["EnviarCorreoAviso"] == "1")
                        {
                            var usuario = _broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == IdUsuarioOnline);
                            string clabeMask = "************** " + uoc.CLABE.Substring(14);
                            //Mailing.EnviaCorreoAvisoMovimiento("Cuenta CLABE", 0, "cuenta CLABE", clabeMask, usuario.NombreCompleto, host, usuario.CorreoElectronico);
                        }
                       return true;
                    } catch (Exception ex)
                    {
                        Helper.SendMail("broxelonline@broxel.com", "luis.huerta@broxel.com", "EnviaCorreoAvisoMovimiento", "Se intento enviar el correo de notificación de que se dio de baja una cuenta clabe " + uoc.Id + " con la excpeción " + ex, "67896789");
                    }
                    return true;
                 
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public Boolean AgregaCLABE(int IdUsuarioOnline, String CLABE, String EmailAviso, String Nombre, String RFC, string identifier = "", string host = "")
        {
            IFormatProvider culture = new CultureInfo("es-MX", true);
            var fecha = DateTime.Now;

           //MLS idUserSecure
                var idUser = new IdSecureComp().GetIdUserValid(IdUsuarioOnline);
            if (idUser == 0)
            {
                return false;
            }
            IdUsuarioOnline = idUser;

           broxelco_rdgEntities _broxelcoRdgEntities = new broxelco_rdgEntities();
            var usuario = _broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == IdUsuarioOnline);

            if (identifier != ConfigurationManager.AppSettings["MejoravitBatchId"])
            {
                var cuentas = _broxelcoRdgEntities.accessos_clientes.Where(x => x.IdUsuarioOnlineBroxel == IdUsuarioOnline).Select(x => x.cuenta).ToList();
                var valida = new MySqlDataAccess();
     
                foreach (var numCuenta in cuentas)
                {
                    if (valida.validarMerchant(numCuenta))
                    {
                        return false;
                    }
                }

                foreach (var numCuenta in cuentas)
                {
                    if (valida.ValidaProductoMejoravit(numCuenta))
                    {
                        return false;
                    }
                }
            }
            //........................................................................
            
            if (ValidaCLABE(CLABE))
            {
                var banco = _broxelcoRdgEntities.bancos_stp.FirstOrDefault(x => x.codigoBanco.StartsWith(CLABE.Substring(0, 3)));
                if (banco == null)
                    return false;

                var uoc =
                    _broxelcoRdgEntities.UsuariosOnlineCLABE.FirstOrDefault(
                        x => x.IdUsuario == IdUsuarioOnline && x.CLABE == CLABE);
                if (uoc != null)
                {
                    try
                    {
                        uoc.Activa = 1;
                        uoc.EmailAviso = EmailAviso;
                        uoc.Nombre = Nombre;
                        uoc.RFC = RFC;
                        _broxelcoRdgEntities.Entry(uoc).State = EntityState.Modified;
                        _broxelcoRdgEntities.SaveChanges();

                        try
                        {
                            //->LAHA
                            if (ConfigurationManager.AppSettings["EnviarCorreoAviso"] == "1")
                            {
                                string clabeMask =  CLABE.Substring(CLABE.Length -4,4);   
                                var listaEtiquetas = new List<MailReplacer>
                                {
                                    new MailReplacer {Tag = "{FECHA}", Value = fecha.ToString("dd MMMM yyyy",culture)},
                                    new MailReplacer {Tag = "{CLABE}", Value = clabeMask}
                                };
                                Mailing.EnviaCorreoAvisoMovimiento(3, 7, usuario.CorreoElectronico, listaEtiquetas); // 3,7 es configuración de alta de CLABE, verificar en la tabla MailingConfig.
                            }
                            //<-

                        }
                        catch (Exception ex)
                        {
                            Helper.SendMail("broxelonline@broxel.com", "luis.huerta@broxel.com", "EnviaCorreoAvisoMovimiento", "Se intento enviar el correo de notificacion de que se agrego cuenta clabe " + uoc.Id + " con la excpeción " + ex, "67896789");
                        }

                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine("Error al actualizar UsuariosOnlineCLABE: " + e);
                       Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "AgregarCLABE", "Se intento agregar la clabe existente " + uoc.Id + " con la excpeción " + e , "67896789");
                    }
                }
                else
                {
                    try
                    {
                        _broxelcoRdgEntities.UsuariosOnlineCLABE.Add(new UsuariosOnlineCLABE
                        {
                            Activa = 1,
                            CLABE = CLABE,
                            EmailAviso = EmailAviso,
                            FechaAlta = DateTime.Now,
                            IdBanco = banco.id,
                            IdUsuario = IdUsuarioOnline,
                            Nombre = Nombre,
                            RFC = RFC
                        });
                        _broxelcoRdgEntities.SaveChanges();

                        try
                        {
                            //LAHA
                            if (ConfigurationManager.AppSettings["EnviarCorreoAviso"] == "1")
                            {
                                string clabeMask = CLABE.Substring(CLABE.Length - 4, 4);
                                var listaEtiquetas = new List<MailReplacer>
                                {
                                    new MailReplacer {Tag = "{FECHA}", Value = fecha.ToString("dd MMMM yyyy",culture)},
                                    new MailReplacer {Tag = "{CLABE}", Value = clabeMask}
                                };
                                Mailing.EnviaCorreoAvisoMovimiento(3, 7, usuario.CorreoElectronico, listaEtiquetas); // 3,7 es configuración de alta de CLABE, verificar en la tabla MailingConfig.
                            }
                            //
                        }
                        catch (Exception ex)
                        {
                            Helper.SendMail("broxelonline@broxel.com", "luis.huerta@broxel.com", "EnviaCorreoAvisoMovimiento", "Se intento enviar el correo de notificacion de que se agrego cuenta clabe " + uoc.Id + " con la excpeción " + ex, "67896789");
                        }

                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine("Error al insertar UsuariosOnlineCLABE: " + e);
                        StringBuilder exception = new StringBuilder();
                        foreach (var eve in ((DbEntityValidationException)e).EntityValidationErrors)
                        {
                            exception.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                exception.AppendFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                            }
                        }

                        Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "AgregarCLABE", "Se intento agregar la clabe con la excpeción " + exception, "67896789");
                    }
                    
                }
                return true;
            }
            return false;
        }
        #endregion

        #region RecepcionTransferenciasSPEI
        // TODO MLS Validar con Max
        /* MLS Cambio en congeladora, no estaba en la version productiva
        [WebMethod]
        public int AbonaACLABE(String CLABE, double Monto)
        {
            BroxelService wsBroxelService = new BroxelService();
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            var dC = broxelcoRdgEntities.DetalleClientesBroxel.FirstOrDefault(x => x.CLABE == CLABE);

            //NO es F&F
            if (dC == null || (dC.Producto != "K165" && dC.Producto != "D152"))
                return Helper.ActualizaLineaCreditoCliente(CLABE, 1, Monto);

            dC = broxelcoRdgEntities.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == dC.ClaveCliente && x.Producto == "K165");
            if (dC == null)
                return 0;


            //SE ENVIA PAGO A EXPENSES PRIMERO
            double debeProdK = dC.LineaCreditoOriginal - dC.LineaCreditoActual;
            if (Monto < debeProdK)
            {
                return Helper.ActualizaLineaCreditoCliente(dC.CLABE, 1, Monto);
            }
            int res = Helper.ActualizaLineaCreditoCliente(dC.CLABE, 1, debeProdK);

            var cliente = broxelcoRdgEntities.clientesBroxel.FirstOrDefault(x => x.claveCliente == dC.ClaveCliente);

            //DEL RESTANTE SE ENVIA A CREDITO - AHORA TO-DO SE ENVIA A CREDITO
            var dC2 = broxelcoRdgEntities.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == dC.ClaveCliente && x.Producto == "D152");
            int res2 = Helper.ActualizaLineaCreditoCliente(dC2.CLABE, 1, Monto - debeProdK);
            //SI SE DEBE ENVIAR SOLO A CREDITO
            //int res2 = Helper.ActualizaLineaCreditoCliente(dC2.CLABE, 1, Monto);

            //Genera pago y ejecuta pagoWS 
            var query = "SELECT COALESCE(substr(max(`folio`),6,6)+1,1,substr(max(`folio`),6,6)+1) AS folio FROM pagosSolicitudes WHERE SUBSTRING(`folio`,1,5)='" + DateTime.Now.ToString("yyMM") + "P'";
            var folio = DateTime.Now.ToString("yyMM") + "P" + broxelcoRdgEntities.Database.SqlQuery<String>(query).FirstOrDefault().PadLeft(6).Replace(' ', '0');

            var newPagoSolicitud = new pagosSolicitudes
            {
                areaSolicitante = "TransferCliente",
                claveCliente = dC.ClaveCliente,
                cliente = cliente.razonSocial,
                cuentas_repetidas = 0,
                email = "",
                estado = "NUEVO",
                fechaCreacion = DateTime.Now,
                usuarioCreacion = "WebServicePagoF&F",
                montoPrincipal = Monto - debeProdK,
                //montoPrincipal = Monto,  // - debeProdK
                producto = "D152",
                rfc = cliente.rfc,
                tipo = "PAGO",
                solicitante = "WebServicePagoF&F",
                total_cuentas = 1,
                valor_estimado = Convert.ToDecimal(Monto -debeProdK),
                //valor_estimado = Convert.ToDecimal(Monto), // - debeProdK
                folio = folio,
                fechaVerificacion = DateTime.Now,
                usuarioVerificacion = "WebService",
            };

            broxelcoRdgEntities.pagosSolicitudes.Add(newPagoSolicitud);
            broxelcoRdgEntities.SaveChanges();
            var maquila = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.producto == "D152" && x.clave_cliente == dC.ClaveCliente);
            broxelcoRdgEntities.pagosInternos.Add(new pagosInternos
            {
                claveCliente = cliente.claveCliente,
                producto = "D152",
                idSolicitud = folio,
                cuenta = maquila == null ? "" : maquila.num_cuenta,
                pago = Monto - debeProdK,
                //pago = Monto, // - debeProdK
            });
            try
            {
                broxelcoRdgEntities.SaveChanges();
                newPagoSolicitud.estado = "PENDIENTE";
                broxelcoRdgEntities.SaveChanges();
                newPagoSolicitud.estado = "WebService";
                broxelcoRdgEntities.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return ((res==1) && (res2 == 1) && wsBroxelService.Pago(folio)) ? 1 : 0;
            //return ((res2 == 1) && wsBroxelService.Pago(folio)) ? 1 : 0;
        }*/
        [WebMethod]
        public int AbonaACLABE(String CLABE, double Monto)
        {
            BroxelService wsBroxelService = new BroxelService();
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            var dC = broxelcoRdgEntities.DetalleClientesBroxel.FirstOrDefault(x => x.CLABE == CLABE);
            if (CLABE.StartsWith("6461801148") && dC != null && (dC.Producto != "K165" && dC.Producto != "D152"))
            {

            }
            if (dC == null || (dC.Producto != "K165" && dC.Producto != "D152"))
                return Helper.ActualizaLineaCreditoCliente(CLABE, 1, Convert.ToDecimal(Monto));
            /* MLS Cambio para que solo se afecte a la linea de credito del credito
            dC = broxelcoRdgEntities.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == dC.ClaveCliente && x.Producto == "K165");
            if (dC == null)
                return 0;

            double debeProdK = dC.LineaCreditoOriginal - dC.LineaCreditoActual;
            if (Monto < debeProdK)
            {
                return Helper.ActualizaLineaCreditoCliente(dC.CLABE, 1, Monto);
            } */
            var cliente = broxelcoRdgEntities.clientesBroxel.FirstOrDefault(x => x.claveCliente == dC.ClaveCliente);
            /*
            int res = Helper.ActualizaLineaCreditoCliente(dC.CLABE, 1, debeProdK);
            */
            var dC2 = broxelcoRdgEntities.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == dC.ClaveCliente && x.Producto == "D152");
            if (dC2 == null)
                return 0;

            var res2 = Helper.ActualizaLineaCreditoCliente(dC2.CLABE, 1, Convert.ToDecimal(Monto) /*- debeProdK*/);

            //Genera pago y ejecuta pagoWS 
            var query = "SELECT COALESCE(substr(max(`folio`),6,6)+1,1,substr(max(`folio`),6,6)+1) AS folio FROM pagosSolicitudes WHERE SUBSTRING(`folio`,1,5)='" + DateTime.Now.ToString("yyMM") + "P'";
            var folio = DateTime.Now.ToString("yyMM") + "P" + broxelcoRdgEntities.Database.SqlQuery<String>(query).FirstOrDefault().PadLeft(6).Replace(' ', '0');

            var newPagoSolicitud = new pagosSolicitudes
            {
                areaSolicitante = "TransferCliente",
                claveCliente = dC.ClaveCliente,
                cliente = cliente.razonSocial,
                cuentas_repetidas = 0,
                email = "",
                estado = "NUEVO",
                fechaCreacion = DateTime.Now,
                usuarioCreacion = "WebServicePagoF&F",
                montoPrincipal = Monto /*- debeProdK*/,
                producto = "D152",
                rfc = cliente.rfc,
                tipo = "PAGO",
                solicitante = "WebServicePagoF&F",
                total_cuentas = 1,
                valor_estimado = Convert.ToDecimal(Monto /*- debeProdK*/),
                folio = folio,
                fechaVerificacion = DateTime.Now,
                usuarioVerificacion = "WebService",
            };

            broxelcoRdgEntities.pagosSolicitudes.Add(newPagoSolicitud);
            broxelcoRdgEntities.SaveChanges();
            var maquila = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.producto == "D152" && x.clave_cliente == dC.ClaveCliente);
            broxelcoRdgEntities.pagosInternos.Add(new pagosInternos
            {
                claveCliente = cliente.claveCliente,
                producto = "D152",
                idSolicitud = folio,
                cuenta = maquila == null ? "" : maquila.num_cuenta,
                pago = Monto /*- debeProdK*/,
            });
            try
            {
                broxelcoRdgEntities.SaveChanges();
                newPagoSolicitud.estado = "PENDIENTE";
                broxelcoRdgEntities.SaveChanges();
                newPagoSolicitud.estado = "WebService";
                broxelcoRdgEntities.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return (/*(res == 1) &&*/ ((res2 == 1)) && wsBroxelService.Pago(folio)) ? 1 : 0;
        }

        #endregion

        #region CargosGenericosSinAsignar
        /// <summary>
        /// Realiza cargos a una cuenta con multiples montos a pagar.
        /// </summary>
        /// <param name="numCuenta">numero de cuenta</param>
        /// <param name="producto">producto</param>
        /// <param name="idComercio">id del comercio</param>
        /// <param name="montos">lista de montos</param>
        /// <returns></returns>
        [WebMethod]
        public RealizarCargoMontosResp RealizarCargoMultiplesMontos(string claveCliente, int idComercio,string producto, List<DetalleMonto> detalleCuenta, bool realizarCargo)
        {
            return new CargoBL().RealizaCargosMejoravit(claveCliente, idComercio, producto, detalleCuenta, realizarCargo);
        }
        #endregion
        
        #endregion
    }
}
