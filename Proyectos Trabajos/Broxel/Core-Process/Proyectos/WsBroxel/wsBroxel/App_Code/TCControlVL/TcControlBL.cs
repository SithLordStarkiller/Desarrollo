using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using wsBroxel.App_Code.TCControlVL.Model;
using wsBroxel.BussinessLogic;
using wsBroxel.wsTarjeta;

namespace wsBroxel.App_Code.TCControlVL
{
    public class TcControlBL
    {
        readonly CASA_SRTMX_TarjetaPortType _newTarjetaws = new CASA_SRTMX_TarjetaPortTypeClient("CASA_SRTMX_TarjetaHttpsSoap11Endpoint", "https://" + ConfigurationManager.AppSettings["CredencialHost"] + ":" + ConfigurationManager.AppSettings["CredencialPort"] + "/services/CASA_SRTMX_Tarjeta.CASA_SRTMX_TarjetaHttpsSoap11Endpoint");

        /// <summary>
        /// Consulta de estado para BroxelOnline (app y web)
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="tarjeta"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        public OperarTarjetaResponse ConsultaEstado(int idUser, string tarjeta, string cuenta)
        {
            return RealizaAccion(idUser, tarjeta, cuenta, 1);
        }

        public OperarTarjetaResponse CambiarEstado(int idUser, string tarjeta, string cuenta, string nuevoEstado)
        {
            return RealizaAccion(idUser, tarjeta, cuenta, 2, nuevoEstado);
        }
        public OperarTarjetaResponse BloquearEdoOp(int idUser,string tarjeta, string cuenta, string nuevoEstado)
        {
            if(nuevoEstado!=EstadosTarjetas.Bol3PinErr&&nuevoEstado!=EstadosTarjetas.BolBloqueoTemporal&&nuevoEstado!=EstadosTarjetas.BolCancelada&&nuevoEstado!=EstadosTarjetas.BolEnRep&&nuevoEstado!=EstadosTarjetas.BolExtravio&&nuevoEstado!=EstadosTarjetas.BolOtros&&nuevoEstado!=EstadosTarjetas.BolRobo)
                return new OperarTarjetaResponse{Success = 0, UserResponse = "Estado invalido"};
            return RealizaAccion(idUser, tarjeta, cuenta, 2, nuevoEstado);
        }

        #region Metodos privados

        /// <summary>
        /// Consulta o cambia estado de la tarjeta
        /// </summary>
        /// <param name="idUser">identificador de usuario broxel</param>
        /// <param name="tarjeta">Numero de tarjeta, en claro o mascarado</param>
        /// <param name="cuenta">Numero de cuenta, presente o vacio</param>
        /// <param name="idOper">1= Consulta, 2= Cambio de estado</param>
        /// <param name="nuevoEstado">Nuevo estado, ver EstadosTarjeta</param>
        /// <returns></returns>
        public OperarTarjetaResponse RealizaAccion(int idUser, string tarjeta, string cuenta, int idOper, string nuevoEstado = "")
        {
            var usuario = "";
            var tarjetaFromDb = "";
            //Bloque de validaciones 
            using (var ctx = new broxelco_rdgEntities())
            {
                // Peticion RDL, bloqueo y activacion de cualquier tarjeta no  conectada al usuario
                if (idUser == -500)
                {
                    usuario = "WebServiceSinUser";
                    tarjetaFromDb = tarjeta;
                }
                else
                {
                    var user = ctx.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == idUser);
                    if (user == null)
                        return new OperarTarjetaResponse { Success = 0, UserResponse = "Usuario inválido o no existe" };
                    usuario = user.Usuario;

                    if (string.IsNullOrEmpty(cuenta) && tarjeta.Contains("* *"))
                        return new OperarTarjetaResponse { Success = 0, UserResponse = "No se puede consultar el estado de la tarjeta mascarada sin número de cuenta" };
                    Tarjeta tar = null;
                    if (!string.IsNullOrEmpty(cuenta))
                    {
                        var tarjetaAdicional = ctx.TarjetasFisicasAdicionales.FirstOrDefault(t => t.NumCuenta == cuenta);
                        if (tarjetaAdicional != null)
                            tar = Helper.GetTarjetaFromCuentaAdicional(cuenta);
                        else
                            tar = Helper.GetTarjetaFromCuenta(cuenta);
                    }
                    else
                    {
                        cuenta = Helper.GetCuentaFromTarjeta(tarjeta);
                        var tarjetaAdicional = ctx.TarjetasFisicasAdicionales.FirstOrDefault(t => t.NumCuenta == cuenta);
                        if (tarjetaAdicional != null)
                            tar = Helper.GetTarjetaFromCuentaAdicional(cuenta);
                        else
                            tar = Helper.GetTarjetaFromCuenta(cuenta);

                    }

                    if (tar == null)
                        return new OperarTarjetaResponse { Success = 0, UserResponse = "No se pudieron recuperar los datos de la tarjeta desde la base de datos" };
                    tarjetaFromDb = tar.NumeroTarjeta;
                    if (tarjeta.Contains("* *")) // Validaciones de tarjeta ofuscada y cuenta
                    {
                        var numTarOf = tar.NumeroTarjeta.Substring(0, 6) + "** ****" + tar.NumeroTarjeta.Substring(12, 4);
                        if (tarjeta != numTarOf)
                            return new OperarTarjetaResponse { Success = 0, UserResponse = "La tarjeta enviada no corresponde a la tarjeta en base de datos" };
                        var ac = ctx.accessos_clientes.FirstOrDefault(y => y.IdUsuarioOnlineBroxel == idUser && y.cuenta == cuenta);
                        var family = new BroxelFamily();
                        var esHija = family.EsHijoDeLaCuentaPadreFamily(idUser, cuenta);

                        if (ac == null && !esHija)
                            return new OperarTarjetaResponse { Success = 0, UserResponse = "La tarjeta no pertenece al usuario" };
                    }
                    else //Validaciones de tarjeta en claro sin cuenta.
                    {
                        if (tarjeta != tar.NumeroTarjeta)
                            return new OperarTarjetaResponse { Success = 0, UserResponse = "La tarjeta enviada no corresponde a la tarjeta en base de datos" };
                        cuenta = tar.Cuenta;
                        var ac = ctx.accessos_clientes.FirstOrDefault(y => y.IdUsuarioOnlineBroxel == idUser && y.cuenta == cuenta);
                        var family = new BroxelFamily();
                        var esHija = family.EsHijoDeLaCuentaPadreFamily(idUser, cuenta);

                        if (ac == null && !esHija)
                            return new OperarTarjetaResponse { Success = 0, UserResponse = "La tarjeta no pertenece al usuario" };
                    }
                }
            }

            var req = new OperarTarjetaRequest
            {
                Tarjeta = new Tarjeta { NumeroTarjeta = tarjetaFromDb },
                NumCuenta = cuenta,
                Accion = DeterminaAccion(idOper,nuevoEstado),
                Solicitante = usuario,
                UserID = idUser
            };

            switch (idOper)
            {
                case 1:
                    return ConsultaExecute(req);
                case 2:
                    return CambiarEstadoExecute(req, nuevoEstado);
                default:
                    throw new Exception("Operación no definida");
            }
        }

        
        private string DeterminaAccion(int idOper, string infoAdd = "")
        {
            switch (idOper)
            {
                case 1:
                    return "ConsultaTarjeta" + infoAdd;
                case 2:
                    return "CambiaEstadoTarjeta|" + infoAdd;
                default:
                    throw new Exception("Operación no definida");
            }
        }

        // TODO MLS Pasar este método a una clase de utilerias
        private void GuardarBitacora(Request request, Response response)
        {
            BroxelEntities _ctx = new BroxelEntities();
            StackFrame frame = new StackFrame(1);
            try
            {
                var numTarjeta = request.Tarjeta != null ? request.Tarjeta.NumeroTarjeta : "";
                if (!numTarjeta.Contains("*"))
                    numTarjeta = numTarjeta.Substring(0, 6) + "** ****" + numTarjeta.Substring(12, 4);
                LogTransaccionesSQL ltsq = new LogTransaccionesSQL
                {
                    FechaHora = Convert.ToDateTime(response.FechaCreacion ?? DateTime.Now.ToString("O")),
                    Mensaje = response.UserResponse,
                    MetodoEjecucion = frame.GetMethod().Name,
                    Accion = request.Accion ?? "",
                    NumCuenta = request.NumCuenta ?? "",
                    NumTarjeta = numTarjeta,
                    Usuario = request.Solicitante,
                    Resultado = response.Success.ToString(CultureInfo.InvariantCulture),
                    NumAutorizacion = response.NumeroAutorizacion,
                    WS = "BroxelService",
                };
                _ctx.LogTransaccionesSQL.Add(ltsq);
                _ctx.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Fallo en bitacora", e.ToString(), "67896789", "Broxel Online");
            }
            catch (Exception e)
            {
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Fallo en bitacora", e.ToString(), "67896789", "Broxel Online");
            }
        }
        /// <summary>
        /// Consulta datos de la tarjeta
        /// </summary>
        /// <param name="request">Objeto OperarCuentaRequest</param>
        /// <returns>Objeto OperarCuentaResponse</returns>
        private OperarTarjetaResponse ConsultaExecute(OperarTarjetaRequest request)
        {
            return ConsumirCrede(request, 1, "");
        }
        private OperarTarjetaResponse CambiarEstadoExecute(OperarTarjetaRequest request, string nuevoEstado)
        {

           OperarTarjetaResponse resp = ConsumirCrede(request, 2, nuevoEstado);
           //enviar Mail de estatus de tarjeta de forma asincrona
           var taskRenominacion = new Task(() => Mailing.EnviaCorreoTarjetaBloqueada(resp, request, nuevoEstado));
           taskRenominacion.Start();
           return resp;
        }

        private OperarTarjetaResponse ConsumirCrede(OperarTarjetaRequest request, int idOper, string nuevoEstado = "")
        {
            var response = new OperarTarjetaResponse();
            try
            {
                var auth = new Autenticacion
                {
                    Password = Helper.CipherPassCREA("bRoXeL_1.2.3.4"),
                    Usuario = "broxel"
                };

                var solicitante = request.Solicitante.Length > 24
                    ? request.Solicitante.Substring(0, 24)
                    : request.Solicitante;

                var ori = new Originador
                {
                    Solicitante = solicitante,
                    ZonaHoraria = "America/Mexico_City"
                };

                var issuerCode = ConfigurationManager.AppSettings["issuerCode"];
                var disData = Helper.GetEngineByCard(request.Tarjeta.NumeroTarjeta);
                response.UserResponse = disData.Procesador == 0 ? "Error al consultar cuenta. Inexistente" : response.UserResponse;

                switch (idOper)
                {
                    case 1:

                        if (disData.Procesador == 1)
                        {
                            var consul = new ConsultarRequest
                            {
                                Autenticacion = auth,
                                Originador = ori,
                                Tarjeta = Helper.CipherPassCREA(request.Tarjeta.NumeroTarjeta)
                            };

                            var consul1 = new ConsultarRequest1 { ConsultarRequest = consul };

                            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                            var resConsul = _newTarjetaws.Consultar(consul1);
                            response.Success = resConsul.ConsultarResponse.Response.Codigo == "00" ? 1 : 0;
                            response.CodigoRespuesta = Convert.ToInt32(resConsul.ConsultarResponse.Response.Codigo);
                            response.NumeroAutorizacion = resConsul.ConsultarResponse.Response.TicketWS;
                            response.UserResponse = resConsul.ConsultarResponse.Response.Descripcion;
                            if (resConsul.ConsultarResponse.Tarjeta != null)
                            {
                                response.EstadoOperativo = resConsul.ConsultarResponse.Tarjeta.Estado.Descripcion;
                                response.CodigoEstadoOperativo = resConsul.ConsultarResponse.Tarjeta.Estado.Codigo;
                            }
                        }
                        else if (disData.Procesador == 2)
                        {
                            response.NumeroAutorizacion = "N/A";
                            response.FechaCreacion = DateTime.Now.ToString();

                            var petrusProxy = new GeneralAccountService.GeneralAccountServiceClient();
                            var petrusResponse = petrusProxy.GetCardAccount(issuerCode, new GeneralAccountService.GetCardAccountDTO() { CardNumber = request.Tarjeta.NumeroTarjeta });
                            if (petrusResponse.Errors.Length > 0)
                            {
                                response.Success = 0;
                                response.UserResponse = petrusResponse.Errors[0].ErrorText;
                                response.CodigoRespuesta = -1;
                            }
                            else
                            {
                                response.Success = petrusResponse.ReturnExecution == GeneralAccountService.ReturnExecution.SUCCESS ? 1 : 0;
                                response.CodigoRespuesta = response.Success = true ? 0 : -1;
                                response.UserResponse = "N/A";
                                if (petrusResponse.GetCardAccounts.Length > 0)
                                {
                                    response.EstadoOperativo = petrusResponse.GetCardAccounts[0].CustomersData[0].PaymentMediaList[0].StatusType == 1 ? "Activa" : "Bloqueada/Cancelada";
                                    response.CodigoEstadoOperativo = petrusResponse.GetCardAccounts[0].CustomersData[0].PaymentMediaList[0].StatusType.ToString();
                                }
                            }

                        }
                        
                        break;
                    case 2:
                        if (disData.Procesador == 1)
                        {
                            var cons = new CambiarEstadoRequest()
                            {
                                Autenticacion = auth,
                                Originador = ori,
                                Tarjeta = Helper.CipherPassCREA(request.Tarjeta.NumeroTarjeta),
                                NuevoEstado = nuevoEstado
                            };

                            var con1 = new CambiarEstadoRequest1 { CambiarEstadoRequest = cons };

                            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                            var res = _newTarjetaws.CambiarEstado(con1);
                            response.Success = res.CambiarEstadoResponse.Response.Codigo == "00" ? 1 : 0;
                            response.CodigoRespuesta = Convert.ToInt32(res.CambiarEstadoResponse.Response.Codigo);
                            response.NumeroAutorizacion = res.CambiarEstadoResponse.Response.TicketWS;
                            response.UserResponse = res.CambiarEstadoResponse.Response.Descripcion;
                        }
                        else if (disData.Procesador == 2)
                        {
                            var petrusProxy = new CardStatusService.CardStatusServiceClient();

							var cardStatusService = new CardStatusService.CardStatusMaintenanceDTO()
							{
								CardNumber = request.Tarjeta.NumeroTarjeta,
                                Observation = ori.Solicitante,
                                StatusType = (short)Helper.GetStatusTypeByStatusCode(nuevoEstado), //NEED TO CHECK
                                StatusCode = Helper.GetPetrusStatusCodeByCredentialStatusCode(nuevoEstado), //NEED TO CHECK
							};
							// set remition if status code is 6 or 7
							if (Int32.Parse(cardStatusService.StatusCode) == 6 || Int32.Parse(cardStatusService.StatusCode) == 7)
							{
								cardStatusService.RequestRemision = 1;
							}

							var res = petrusProxy.CardStatusMaintenance(issuerCode, cardStatusService);

                            if (res.Errors.Length > 0)
                            {
                                response.Success = 0;
                                response.UserResponse = res.Errors[0].ErrorText;
                                response.CodigoRespuesta = -1;
                            }
                            else
                            {
                                response.CodigoRespuesta = 0;
                                response.NumeroAutorizacion = res.ReferenceNumber.ToString();
                                response.Success = res.ReturnExecution == CardStatusService.ReturnExecution.SUCCESS ? 1 : 0;
                                response.UserResponse = "Operación realizada exitosamente";
                            }
                        }
                        break;
                    default:
                        throw new Exception("Operacion no definida");
                }
                GuardarBitacora(request, response);
            }
            catch (Exception e)
            {
                response.Success = 0;
                response.CodigoRespuesta = 999;
                response.NumeroAutorizacion = "";
                response.UserResponse = "Ocurrio un error al realizar la operacion : " + e.Message;
            }
            return response;
        }
        #endregion
    }
}