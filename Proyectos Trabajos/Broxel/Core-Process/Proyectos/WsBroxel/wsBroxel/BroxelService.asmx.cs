using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using IdSecure;
using MySql.Data.MySqlClient.Memcached;
using wsBroxel.App_Code;
using wsBroxel.App_Code.GenericBL;
using wsBroxel.App_Code.Online;
using wsBroxel.App_Code.RequestResponses;
using wsBroxel.App_Code.TCControlVL;
using wsBroxel.App_Code.TCControlVL.Model;
using wsBroxel.App_Code.VCBL;
using wsBroxel.GeneralAccountService;
using wsBroxel.PaymentService;
using wsBroxel.wsAutorizacion;
using wsBroxel.wsCuenta;
using wsBroxel.wsMovementsService;
using wsBroxel.wsOperaciones;
using wsBroxel.App_Code.Utils;
using wsBroxel.App_Code.SolicitudBL.Model;

using Autenticacion = wsBroxel.wsCuenta.Autenticacion;
using ConsultarRequest = wsBroxel.wsCuenta.ConsultarRequest;
using ConsultarRequest1 = wsBroxel.wsCuenta.ConsultarRequest1;
using ConsultarResponse = wsBroxel.wsCuenta.ConsultarResponse;
using Originador = wsBroxel.wsCuenta.Originador;
using Tarjeta = wsBroxel.App_Code.Tarjeta;
using newAutorizacion = wsBroxel.newWSAutorizacion;
using wsBroxel.App_Code.SolicitudBL;
using ErrorDTO = wsBroxel.GeneralAccountService.ErrorDTO;
using Especificacion = wsBroxel.wsOperaciones.Especificacion;
using ReturnExecution = wsBroxel.GeneralAccountService.ReturnExecution;
using wsBroxel.Dispatcher;
using wsBroxel.ComunicacionesBL;

namespace wsBroxel
{
    /// <summary>
    /// Summary description for Operaciones
    /// </summary>
    [WebService(Namespace = "wsBroxel")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BroxelService : WebService
    {
        private AutorizarRequest _autorizarReq;
        private AutorizarResponse _autorizarResp;
        //private  string aUrl="https://" + ConfigurationManager.AppSettings["CredencialHost"] + ":" + ConfigurationManager.AppSettings["CredencialPort"] + "/services/Autorizacion.AutorizacionHttpSoap11Endpoint";

        //produccion
        readonly AutorizacionPortType _autorizadorWs =
                new AutorizacionPortTypeClient("AutorizacionHttpSoap11Endpoint",
                    "https://" + ConfigurationManager.AppSettings["CredencialHost"] + ":" + ConfigurationManager.AppSettings["CredencialPort"] + "/services/Autorizacion.AutorizacionHttpSoap11Endpoint");

        //readonly ConsultasPortType _consultador =
        //        new ConsultasPortTypeClient("SOAP11Endpoint",
        //            "https://" + ConfigurationManager.AppSettings["CredencialHost"] + ":" + ConfigurationManager.AppSettings["CredencialPort"] + "/services/Consultas.SOAP11Endpoint");

        readonly newAutorizacion.AutorizacionPortType _newAutorizadorWs =
        new newAutorizacion.AutorizacionPortTypeClient("AutorizacionHttpsSoap11Endpoint1",
            "https://" + ConfigurationManager.AppSettings["CredencialNewHost"] + ":" + ConfigurationManager.AppSettings["CredencialNewPort"] + "/Autorizacion");


        

        readonly CASA_SRTMX_OperacionPortType _operadorWs = new CASA_SRTMX_OperacionPortTypeClient(
            "CASA_SRTMX_OperacionHttpsSoap11Endpoint",
            "https://" + ConfigurationManager.AppSettings["CredencialHost"] + ":" + ConfigurationManager.AppSettings["CredencialPort"] + "/services/CASA_SRTMX_Operacion.CASA_SRTMX_OperacionHttpsSoap11Endpoint"
            );

        readonly CASA_SRTMX_CuentaPortType _cuentaWs = new CASA_SRTMX_CuentaPortTypeClient(
            "CASA_SRTMX_CuentaHttpsSoap11Endpoint"
            , "https://" + ConfigurationManager.AppSettings["CredencialHost"] + ":" + ConfigurationManager.AppSettings["CredencialPort"] + "/services/CASA_SRTMX_Cuenta.CASA_SRTMX_CuentaHttpsSoap11Endpoint"
            );

        readonly IGeneralAccountService _cuentaWsPetrus = new GeneralAccountServiceClient("BasicHttpBinding_IGeneralAccountService", "http://" + ConfigurationManager.AppSettings["PetrusHost"] + ":" + ConfigurationManager.AppSettings["PetrusPort"] + "/IssuerCustomer.ClientWS.ServiceHost/Services/GeneralAccountService.svc");

        readonly IPaymentService _paymentPetrus = new PaymentServiceClient("BasicHttpBinding_IPaymentService", "http://" + ConfigurationManager.AppSettings["PetrusHost"] + ":" + ConfigurationManager.AppSettings["PetrusPort"] + "/IssuerCommercial.ClientWS.ServiceHost/Services/PaymentService.svc");

        private readonly string mxCurrency = "484";
        #region InternalMethods

        private OperarCuentaResponse ActivacionDeCuenta(OperarCuentaRequest request)
        {
            try
            {
                // Validacion de activacion de cuenta niveles de cuenta
                if (new GenericBL().ValidaCuentaEnCuarentena(request.NumCuenta))
                {
                    var errorCuarentena = new OperarCuentaResponse
                    {
                        CodigoRespuesta = 999,
                        FechaCreacion = DateTime.Now.ToString("O"),
                        NumeroAutorizacion = "000000",
                        Success = 0,
                        UserResponse =
                            "Cuenta bloqueada por prevención de fraude, complete sus datos personales para desbloquear."
                    };
                    GuardarBitacora(request, errorCuarentena);
                    return errorCuarentena;
                }
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => true;
                DesbloquearResponse dr = _cuentaWs.Desbloquear(new DesbloquearRequest1
                {
                    DesbloquearRequest = new DesbloquearRequest
                    {
                        Autenticacion = new Autenticacion
                        {
                            Usuario = "broxel",
                            Password = Helper.CipherPassCREA("bRoXeL_1.2.3.4"),
                        },

                        Cuenta = request.NumCuenta,
                        Originador = new Originador
                        {
                            Solicitante = request.Solicitante,
                            ZonaHoraria = "America/Mexico_City",
                        }
                    }
                }).DesbloquearResponse;
                OperarCuentaResponse desbloquear = new OperarCuentaResponse
                {
                    CodigoRespuesta = Convert.ToInt32(dr.Response.Codigo),
                    FechaCreacion = dr.Atencion.Inicio,
                    NumeroAutorizacion = dr.Response.TicketWS,
                    Success = dr.Response.Codigo == "00" ? 1 : 0,
                    UserResponse = dr.Response.Descripcion,
                };
                GuardarBitacora(request, desbloquear);
                return desbloquear;
            }
            catch (Exception ex)
            {
                OperarCuentaResponse desbloquear = new OperarCuentaResponse
                {
                    Success = 0,
                    UserResponse = "ErrorAlDesbloquear" + ex,
                    CodigoRespuesta = 978,
                };
                return desbloquear;
            }
        }

        private LimiteResponse ActualizarLimite(LimiteRequest request)
        {
            BroxelEntities ctx = new BroxelEntities();
            LimiteResponse response = new LimiteResponse();
            DateTime currentDate = DateTime.Now;
            try
            {
                var anterior =
                    ctx.Movimiento.Where(
                        x =>
                            x.NumCuenta == request.NumCuenta && x.TipoTransaccion == 35 &&
                            x.SubTipoTransaccion == request.Tipo)
                        .OrderByDescending(x => x.FechaHoraCreacion)
                        .Take(1)
                        .ToList();
                Parametros param = ctx.Parametros.FirstOrDefault(x => x.Parametro == "TiempoDisp");

                Debug.Assert(param != null, "param != null");
                if (!anterior.Any() || DateTime.Now.Subtract(Convert.ToDateTime(anterior[0].FechaHoraCreacion)).TotalMinutes > Convert.ToInt32(param.Valor))
                {
                    Movimiento mov = new Movimiento
                    {
                        idComercio = 12,
                        FechaHoraCreacion = currentDate,
                        TipoTransaccion = 35,
                        SubTipoTransaccion = request.Tipo,
                        idUsuario = request.UserID,
                        Tarjeta = Helper.TruncaTarjeta(request.Tarjeta.NumeroTarjeta),
                        CVC = "**" + request.Tarjeta.Cvc2.ToString(CultureInfo.InvariantCulture).Substring(3),
                        NombreTarjeta = request.Tarjeta.NombreTarjeta,
                        FechaExpira = request.Tarjeta.FechaExpira,
                        Monto = Convert.ToDecimal(request.Limite),
                        NumCuenta = request.Tarjeta.Cuenta
                    };
                    ctx.Movimiento.Add(mov);
                    ctx.SaveChanges();
                    try
                    {
                        _autorizarReq = new AutorizarRequest
                        {
                            Canal = "BroxelWeb",
                            TipoTransaccion = "35",
                            SubTipoTransaccion = request.Tipo.ToString(CultureInfo.InvariantCulture),
                            SecuenciaTransaccion = mov.idMovimiento.ToString(CultureInfo.InvariantCulture),
                            Comercio = "999999999000003",
                            FechaExpiracion = request.Tarjeta.FechaExpira,
                            Importe = request.Limite.ToString(CultureInfo.InvariantCulture),
                            FechaTransaccion = currentDate.ToString("yyyyMMdd"),
                            HoraTransaccion = currentDate.ToString("HHmmss"),
                            Terminal = "PC",
                            CodigoSeguridad = request.Tarjeta.Cvc2,
                            ModoIngreso = "1",
                            Tarjeta = request.Tarjeta.NumeroTarjeta,
                            CodigoMoneda = "484",
                        };
                        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                        var reqInt = new InspectorBehavior();
                        var handler = (AutorizacionPortTypeClient)_autorizadorWs;
                        handler.Endpoint.Behaviors.Clear();
                        handler.Endpoint.Behaviors.Add(reqInt);
                        var dateTimeInicio = DateTime.Now;
                        _autorizarResp = _autorizadorWs.Autorizar(_autorizarReq);
                        var dateTimeFin = DateTime.Now;
                        var reqXml = reqInt.LastRequestXml;
                        var resXml = reqInt.LastResponseXml;

                        try
                        {
                            ThreadPool.QueueUserWorkItem(delegate
                            {
                                new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 2, 1,
                                    reqXml, resXml, mov.idMovimiento);
                            });
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
                        }

                        int codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                        CodigosRespuestaSQL cr = ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
                        if (cr != null)
                            response.UserResponse = cr.Descripcion;
                        mov.NoAutorizacion = _autorizarResp.CodigoAutorizacion;
                        Debug.Assert(cr != null, "cr != null");
                        mov.MensajeError = cr.Descripcion;
                        mov.Autorizado = _autorizarResp.CodigoRespuesta.Trim() == ("-1");
                        response.Success = mov.Autorizado == true ? 1 : 0;
                        response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
                        response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                        response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
                        response.IdMovimiento = mov.idMovimiento;
                        ctx.SaveChanges();
                        //GuardarBitacora(request, response);
                    }
                    catch (Exception ex)
                    {
                        mov.NoAutorizacion = "-1";
                        mov.Autorizado = false;
                        mov.MensajeError = "Problema en WebService : " + ex;
                        ctx.SaveChanges();
                    }
                }
                else
                {
                    response.CodigoRespuesta = 152;
                    response.Success = 0;
                    response.UserResponse = "Debe dejar pasar " + param.Valor + " minutos antes de volver a ejecutar esta dispersion";
                }
            }
            /* MLS Cambio en congeladora, no estaba en la version productiva
            catch (DbEntityValidationException ex)
            {

                StringBuilder exception = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    exception.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        exception.AppendFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                Helper.SendMail("dispersiones@broxel.com", "aldo.garcia@broxel.com, mauricio.lopez@broxel.com, jesus.valdiviezo@broxel.com", "Error guardando ActualizarLimite ", "Error " + exception.ToString(),
                      "yMQ3E3ert6", "Errores ");
                response.UserResponse = "ERROR EN BASE DE DATOS";
            }*/
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
                response.CodigoRespuesta = 993;
                response.UserResponse = "ERROR EN BASE DE DATOS";
            }
            catch (Exception)
            {
                response.CodigoRespuesta = 997;
                response.UserResponse = "OTRO ERROR";
            }
            return response;
        }

        private LimiteResponse ActualizarLimiteNew(LimiteRequest request)
        {
            BroxelEntities ctx = new BroxelEntities();
            LimiteResponse response = new LimiteResponse();
            DateTime currentDate = DateTime.Now;
            try
            {
                var anterior =
                    ctx.Movimiento.Where(
                        x =>
                            x.NumCuenta == request.NumCuenta && x.TipoTransaccion == 35 &&
                            x.SubTipoTransaccion == request.Tipo)
                        .OrderByDescending(x => x.FechaHoraCreacion)
                        .Take(1)
                        .ToList();
                Parametros param = ctx.Parametros.FirstOrDefault(x => x.Parametro == "TiempoDisp");

                Debug.Assert(param != null, "param != null");
                if (!anterior.Any() || DateTime.Now.Subtract(Convert.ToDateTime(anterior[0].FechaHoraCreacion)).TotalMinutes > Convert.ToInt32(param.Valor))
                {
                    Movimiento mov = new Movimiento
                    {
                        idComercio = 12,
                        FechaHoraCreacion = currentDate,
                        TipoTransaccion = 35,
                        SubTipoTransaccion = request.Tipo,
                        idUsuario = request.UserID,
                        Tarjeta = Helper.TruncaTarjeta(request.Tarjeta.NumeroTarjeta),
                        CVC = "**" + request.Tarjeta.Cvc2.ToString(CultureInfo.InvariantCulture).Substring(3),
                        NombreTarjeta = request.Tarjeta.NombreTarjeta,
                        FechaExpira = request.Tarjeta.FechaExpira,
                        Monto = Convert.ToDecimal(request.Limite),
                        NumCuenta = request.Tarjeta.Cuenta
                    };
                    ctx.Movimiento.Add(mov);
                    ctx.SaveChanges();
                    try
                    {
                        if (request.Tarjeta.Procesador == 1)
                        {
                            var autorizarReq = new newAutorizacion.AutorizarRequest
                            {
                                Canal = "BroxelWeb",
                                TipoTransaccion = "35",
                                SubTipoTransaccion = request.Tipo.ToString(CultureInfo.InvariantCulture),
                                SecuenciaTransaccion = mov.idMovimiento.ToString(CultureInfo.InvariantCulture),
                                Comercio = "999999999000003",
                                FechaExpiracion = request.Tarjeta.FechaExpira,
                                Importe = request.Limite.ToString(CultureInfo.InvariantCulture),
                                FechaTransaccion = currentDate.ToString("yyyyMMdd"),
                                HoraTransaccion = currentDate.ToString("HHmmss"),
                                Terminal = "PC",
                                CodigoSeguridad = request.Tarjeta.Cvc2,
                                ModoIngreso = "1",
                                Tarjeta = request.Tarjeta.NumeroTarjeta,
                                CodigoMoneda = "484",
                            };

                            newAutorizacion.AutorizacionPortType newAutorizadorWs =
                            new newAutorizacion.AutorizacionPortTypeClient("AutorizacionHttpsSoap11Endpoint1",
                            "https://" + ConfigurationManager.AppSettings["CredencialNewHost"] + ":" + ConfigurationManager.AppSettings["CredencialNewPort"] + "/Autorizacion");

                            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                            var reqInt = new InspectorBehavior();
                            var handler = (newAutorizacion.AutorizacionPortTypeClient)newAutorizadorWs;
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
                                    new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 3, 2,
                                        reqXml, resXml, mov.idMovimiento);
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
                            mov.NoAutorizacion = autorizarResp.CodigoAutorizacion;
                            Debug.Assert(cr != null, "cr != null");
                            mov.MensajeError = cr.Descripcion;
                            mov.Autorizado = autorizarResp.CodigoRespuesta.Trim() == ("-1");
                            response.Success = mov.Autorizado == true ? 1 : 0;
                            response.NumeroAutorizacion = autorizarResp.CodigoAutorizacion;
                            response.CodigoRespuesta = Convert.ToInt32(autorizarResp.CodigoRespuesta);
                            response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
                            response.IdMovimiento = mov.idMovimiento;
                            ctx.SaveChanges();
                        }
                        else if (request.Tarjeta.Procesador == 2)
                        {
                            var acceptDto = new AcceptPaymentDTO
                            {
                                CardNumber = request.Tarjeta.NumeroTarjeta,
                                CurrencyCode = 484,
                                Amount = request.Limite,
                                PaymentDate = currentDate.ToString("ddMMyyyy HH:mm:ss"),
                                PaymentMethod = 7,
                                PaymentChannel = 2,
                                ExternalPaymentCode = mov.idMovimiento.ToString(CultureInfo.InvariantCulture),
                                IsConfirmation = false
                            };
                            var reqInt = new InspectorBehavior();
                            var handler = (PaymentServiceClient)_paymentPetrus;
                            handler.Endpoint.Behaviors.Clear();
                            handler.Endpoint.Behaviors.Add(reqInt);
                            var dateTimeInicio = DateTime.Now;
                            var pr = _paymentPetrus.AcceptPayment("103", acceptDto);
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

                            mov.Autorizado = pr.ReturnExecution == PaymentService.ReturnExecution.SUCCESS;
                            mov.NoAutorizacion = pr.ReturnExecution == PaymentService.ReturnExecution.SUCCESS
                                ? pr.paymentInfo.PaymentCode:"0";
                            mov.MensajeError = pr.ReturnExecution == PaymentService.ReturnExecution.SUCCESS
                                ? "Exito"
                                : ((PaymentService.ErrorDTO) pr.Errors[0]).ErrorText;
                            response.Success = mov.Autorizado == true ? 1 : 0;
                            response.NumeroAutorizacion = mov.NoAutorizacion;
                            response.CodigoRespuesta = mov.Autorizado == true ? -1 : 999;
                            response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
                            response.IdMovimiento = mov.idMovimiento;
                            response.UserResponse = mov.MensajeError;
                            ctx.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " Error general en invocar a credencial en ActualizarLimiteNew " + ex);
                        mov.NoAutorizacion = "-1";
                        mov.Autorizado = false;
                        var errMsg = "Problema en WebService : " + ex.Message;
                        mov.MensajeError = errMsg.Length > 100 ? errMsg.Substring(0, 99) : errMsg;
                        ctx.SaveChanges();
                    }
                }
                else
                {
                    response.CodigoRespuesta = 152;
                    response.Success = 0;
                    response.UserResponse = "Debe dejar pasar " + param.Valor + " minutos antes de volver a ejecutar esta dispersion";
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " Entity of type \"" + eve.Entry.Entity.GetType().Name + "\" in state \"" + eve.Entry.State + "\" has the following validation errors: ");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine("- Property: \"" + ve.PropertyName + "\", Error: \"" + ve.ErrorMessage + "\"");
                    }
                }
                response.CodigoRespuesta = 993;
                response.UserResponse = "ERROR EN BASE DE DATOS";
            }
            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " Error general en ActualizarLimiteNew " + e);
                response.CodigoRespuesta = 997;
                response.UserResponse = "OTRO ERROR";
            }
            return response;
        }


        private ComercioResponse Agregar(App_Code.Comercio request)
        {
            BroxelEntities ctx = new BroxelEntities();
            ComercioResponse response = new ComercioResponse();
            Comercio com = ctx.Comercio.FirstOrDefault(x => x.CodigoComercio == request.CodigoComercio);
            if (com != null)
            {
                response.Success = 0;
                return response;
            }
            com = ctx.Comercio.FirstOrDefault(x => x.idComercio == request.idComercio);
            if (com != null)
            {
                response.Success = 0;
                return response;
            }
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                ctx.Comercio.Add(new Comercio
                {
                    CodigoComercio = request.CodigoComercio,
                    idComercio = request.idComercio,
                    Comercio1 = request.NombreComercio
                });
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Success = 0;
                response.UserResponse = ex.ToString();
            }
            return response;
        }

        //private UsuarioResponse Agregar(App_Code.Usuario request)
        //{
        //    BroxelEntities _ctx = new BroxelEntities();
        //    UsuarioResponse response = new UsuarioResponse();
        //    Usuario us = _ctx.Usuario.FirstOrDefault(x => x.idUsuario == request.IdUsuario);
        //    if (us != null)
        //    {
        //        response.Success = 0;
        //        response.UserResponse = "Usuario existente";
        //        return response;
        //    }
        //    us = _ctx.Usuario.FirstOrDefault(x => x.Usuario1 == request.NombreUsuario);
        //    if (us != null)
        //    {
        //        response.Success = 0;
        //        response.UserResponse = "Usuario existente";
        //        return response;
        //    }
        //    try
        //    {
        //        _ctx.Usuario.Add(new Usuario
        //        {
        //            idUsuario = request.IdUsuario,
        //            idComercio = request.IdComercio,
        //            Usuario1 = request.NombreUsuario
        //        });
        //        _ctx.SaveChanges();
        //        response.Success = 1;
        //    }
        //    catch (Exception)
        //    {
        //        response.Success = 0;
        //    }
        //    return response;
        //}

        private OperarCuentaResponse BloqueoDeCuenta(OperarCuentaRequest request)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                BloquearResponse br = _cuentaWs.Bloquear(new BloquearRequest1
                {
                    BloquearRequest = new BloquearRequest
                    {
                        Autenticacion = new Autenticacion
                        {
                            Usuario = "broxel",
                            Password = Helper.CipherPassCREA("bRoXeL_1.2.3.4"),
                        },
                        Cuenta = request.NumCuenta,
                        Originador = new Originador
                        {
                            Solicitante = request.Solicitante,
                            ZonaHoraria = "America/Mexico_City",
                        }
                    }
                }).BloquearResponse;
                OperarCuentaResponse bloquear = new OperarCuentaResponse
                {
                    CodigoRespuesta = Convert.ToInt32(br.Response.Codigo),
                    FechaCreacion = br.Atencion.Inicio,
                    NumeroAutorizacion = br.Response.TicketWS,
                    Success = br.Response.Codigo == "00" ? 1 : 0,
                    UserResponse = br.Response.Descripcion,
                };
                GuardarBitacora(request, bloquear);
                return bloquear;
            }
            catch (Exception ex)
            {
                OperarCuentaResponse desbloquear = new OperarCuentaResponse
                {
                    Success = 0,
                    UserResponse = "ErrorAlBloquear" + ex,
                    CodigoRespuesta = 978,
                };
                return desbloquear;
            }
        }

        private NIPResponse CambiarNip(NIPRequest request, string host)
        {
            broxelco_rdgEntities _ctx = new broxelco_rdgEntities();
            BroxelEntities ctx = new BroxelEntities();
            NIPResponse response = new NIPResponse();
            if (request.NIPNuevo.Length != 4 | request.NIPNuevo.Length != 4)
                return new NIPResponse { CodigoRespuesta = 420, Success = 0 };
            DateTime currentDate = DateTime.Now;
            String numCuenta = Helper.GetCuentaFromTarjeta(request.Tarjeta.NumeroTarjeta);
            AutorizarRequest credencialReq = new AutorizarRequest
            {
                Canal = "BroxelWeb",
                TipoTransaccion = "257",
                SecuenciaTransaccion = "1",
                FechaTransaccion = currentDate.ToString("yyyyMMdd"),
                HoraTransaccion = currentDate.ToString("HHmmss"),
                Comercio = "999999999000003",
                Terminal = "PC",
                ModoIngreso = "1",
                Tarjeta = request.Tarjeta.NumeroTarjeta,
                FechaExpiracion = request.Tarjeta.FechaExpira,
                CodigoSeguridad = request.Tarjeta.Cvc2,
                NuevoPIN = Helper.CipherNIP(request.NIPNuevo)
            };
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;


            var reqInt = new InspectorBehavior();
            var handler = (AutorizacionPortTypeClient)_autorizadorWs;
            handler.Endpoint.Behaviors.Clear();
            handler.Endpoint.Behaviors.Add(reqInt);
            var dateTimeInicio = DateTime.Now;
            _autorizarResp = _autorizadorWs.Autorizar(credencialReq);
            var dateTimeFin = DateTime.Now;
            var reqXml = reqInt.LastRequestXml;
            var resXml = reqInt.LastResponseXml;

            int codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
            response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
            CodigosRespuestaSQL cr = ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
            if (cr != null)
                response.UserResponse = cr.Descripcion;
            if (_autorizarResp.CodigoRespuesta.Trim() == "-1")
            {
                response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
                response.Success = 1;
                //LAHA -> Enviar email de cambio de NIP.
                Trace.WriteLine("Host obtenido en cambio de NIP: " + host);
                if (ConfigurationManager.AppSettings["EnviarCorreoAviso"] == "1")
                {
                    string tarjeta = request.Tarjeta.NumeroTarjeta;
                    string ultimosDigitosTarjeta = tarjeta.Substring((tarjeta.Length - 4), 4);
                    var usuario = _ctx.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == request.UserID);
                    if (usuario != null )
                    {
                        Trace.WriteLine("Datos del usuario para cambiar NIP con el idUsuarioBroxel:" + request.UserID);
                        Trace.WriteLine("nombre: " + usuario.NombreCompleto);
                        Trace.WriteLine("Correo: " + usuario.CorreoElectronico);
                        new GenericBL().EmailCambioNIP(usuario.NombreCompleto, ultimosDigitosTarjeta, usuario.CorreoElectronico, host);
                    }
                    else
                    {
                        if (request.UserID != 2) //ID 2 es el monitoreo de cambio de Nip.
                        {
                            Trace.WriteLine("No se encontraron datos del usuario: " + request.UserID + " en la tabla de UsuariosOnlineBroxel para enviar el correo de cambio de NIP");
                            Helper.SendMail("broxelonline@broxel.com", "luis.huerta@broxel.com", "CAMBIO DE NIP", "No se encontraron datos del usuario: " + request.UserID + " en la tabla de UsuariosOnlineBroxel para enviar el correo de cambio de NIP", "67896789", "Broxel Fintech");
                        }
                        
                    }
                }
                //LAHA
            }
            Movimiento mov = new Movimiento
            {
                TipoTransaccion = 257,
                idUsuario = request.UserID,
                FechaHoraCreacion = DateTime.Now,
                Tarjeta = Helper.TruncaTarjeta(request.Tarjeta.NumeroTarjeta),
                CVC = "**" + request.Tarjeta.Cvc2.ToString(CultureInfo.InvariantCulture).Substring(2),
                NombreTarjeta = request.Tarjeta.NombreTarjeta,
                Autorizado = response.Success == 1,
                NoAutorizacion = response.NumeroAutorizacion.ToString(CultureInfo.InvariantCulture),
                MensajeError = response.UserResponse,
                NumCuenta = numCuenta,
            };
            try
            {
                ctx.Movimiento.Add(mov);
                ctx.SaveChanges();
                //GuardarBitacora(request, response);
            }
            /* MLS Cambio en congeladora, no estaba en la version productiva
            catch (DbEntityValidationException ex)
            {
                StringBuilder exception = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    exception.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        exception.AppendFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                Helper.SendMail("dispersiones@broxel.com", "aldo.garcia@broxel.com, mauricio.lopez@broxel.com, jesus.valdiviezo@broxel.com", "Error guardando CambiarNIP ", "Error " + exception.ToString(),
                      "yMQ3E3ert6", "Errores ");
            }*/
            catch (DbEntityValidationException e)
            {
                bool DoNothing = true;
            }
            try
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, numCuenta, 5, 1,
                        reqXml, resXml, mov.idMovimiento);
                });
            }
            catch (Exception e)
            {
                Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
            }
            return response;
        }

        private NIPResponse CambiarNipNew(NIPRequest request, string host)
        {
            var _ctx = new broxelco_rdgEntities();
            var ctx = new BroxelEntities();
            var response = new NIPResponse();
            if (request.NIPNuevo.Length != 4 | request.NIPNuevo.Length != 4)
                return new NIPResponse { CodigoRespuesta = 420, Success = 0 };
            var currentDate = DateTime.Now;
            var numCuenta = Helper.GetCuentaFromTarjeta(request.Tarjeta.NumeroTarjeta);
            var credencialReq = new newAutorizacion.AutorizarRequest
            {
                Canal = "BroxelWeb",
                TipoTransaccion = "257",
                SecuenciaTransaccion = "1",
                FechaTransaccion = currentDate.ToString("yyyyMMdd"),
                HoraTransaccion = currentDate.ToString("HHmmss"),
                Comercio = "999999999000003",
                Terminal = "PC",
                ModoIngreso = "1",
                Tarjeta = request.Tarjeta.NumeroTarjeta,
                FechaExpiracion = request.Tarjeta.FechaExpira,
                CodigoSeguridad = request.Tarjeta.Cvc2,
                NuevoPIN = Helper.CipherNIP(request.NIPNuevo)
            };
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;


            var reqInt = new InspectorBehavior();
            var handler = (newAutorizacion.AutorizacionPortTypeClient)_newAutorizadorWs;
            handler.Endpoint.Behaviors.Clear();
            handler.Endpoint.Behaviors.Add(reqInt);
            var dateTimeInicio = DateTime.Now;
            var autorizarResp = _newAutorizadorWs.Autorizar(credencialReq);
            var dateTimeFin = DateTime.Now;
            var reqXml = reqInt.LastRequestXml;
            var resXml = reqInt.LastResponseXml;

            int codresp = Convert.ToInt32(autorizarResp.CodigoRespuesta);
            response.CodigoRespuesta = Convert.ToInt32(autorizarResp.CodigoRespuesta);
            CodigosRespuestaSQL cr = ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
            if (cr != null)
                response.UserResponse = cr.Descripcion;
            if (autorizarResp.CodigoRespuesta.Trim() == "-1")
            {
                response.NumeroAutorizacion = autorizarResp.CodigoAutorizacion;
                response.Success = 1;
                //LAHA -> Enviar email de cambio de NIP.
                Trace.WriteLine("Host obtenido en cambio de NIP: " + host);
                if (ConfigurationManager.AppSettings["EnviarCorreoAviso"] == "1")
                {
                    string tarjeta = request.Tarjeta.NumeroTarjeta;
                    string ultimosDigitosTarjeta = tarjeta.Substring((tarjeta.Length - 4), 4);
                    var usuario = _ctx.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == request.UserID);
                    if (usuario != null)
                    {
                        Trace.WriteLine("Datos del usuario para cambiar NIP con el idUsuarioBroxel:" + request.UserID);
                        Trace.WriteLine("nombre: " + usuario.NombreCompleto);
                        Trace.WriteLine("Correo: " + usuario.CorreoElectronico);
                        new GenericBL().EmailCambioNIP(usuario.NombreCompleto, ultimosDigitosTarjeta, usuario.CorreoElectronico, host);
                    }
                    else
                    {
                        if (request.UserID != 2) //ID 2 es el monitoreo de cambio de Nip.
                        {
                            Trace.WriteLine("No se encontraron datos del usuario: " + request.UserID + " en la tabla de UsuariosOnlineBroxel para enviar el correo de cambio de NIP");
                            Helper.SendMail("broxelonline@broxel.com", "luis.huerta@broxel.com", "CAMBIO DE NIP", "No se encontraron datos del usuario: " + request.UserID + " en la tabla de UsuariosOnlineBroxel para enviar el correo de cambio de NIP", "67896789", "Broxel Fintech");
                        }

                    }
                }
                //LAHA
            }
            Movimiento mov = new Movimiento
            {
                TipoTransaccion = 257,
                idUsuario = request.UserID,
                FechaHoraCreacion = DateTime.Now,
                Tarjeta = Helper.TruncaTarjeta(request.Tarjeta.NumeroTarjeta),
                CVC = "**" + request.Tarjeta.Cvc2.ToString(CultureInfo.InvariantCulture).Substring(2),
                NombreTarjeta = request.Tarjeta.NombreTarjeta,
                Autorizado = response.Success == 1,
                NoAutorizacion = response.NumeroAutorizacion.ToString(CultureInfo.InvariantCulture),
                MensajeError = response.UserResponse,
                NumCuenta = numCuenta,
            };
            try
            {
                ctx.Movimiento.Add(mov);
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder exception = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    exception.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        exception.AppendFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                Helper.SendMail("dispersiones@broxel.com", "aldo.garcia@broxel.com, mauricio.lopez@broxel.com, jesus.valdiviezo@broxel.com", "Error guardando CambiarNIP ", "Error " + exception.ToString(),
                      "yMQ3E3ert6", "Errores ");
            }
            try
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, numCuenta, 18, 2,
                        reqXml, resXml, mov.idMovimiento);
                });
            }
            catch (Exception e)
            {
                Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
            }
            return response;
        }

        protected ConsultarPorCuentaResponse1 ConsultaPorCuenta(String cuenta, String nombreSolicitante, DateTime? desde = null, DateTime? hasta = null, int paginaInicio = 0, int paginaFin = 0, int tipo = 0, int subtipo = 0)
        {
            DateTime Desde = desde == null ? DateTime.Now.AddMonths(-1).AddDays(-15) : Convert.ToDateTime(desde);
            DateTime Hasta = hasta == null ? DateTime.Now : Convert.ToDateTime(hasta);
            String Pagina = (paginaInicio == 0 ? "1" : paginaInicio.ToString());
            int PaginaInicio = paginaInicio;
            int PaginaFin = (paginaFin == 0 ? paginaInicio == 0 ? 1 : paginaInicio : paginaFin);
            Boolean UnaPagina = Pagina.Equals(PaginaFin) | PaginaFin <= PaginaInicio;

            String tar;
            CodigoOperacion codigo = new CodigoOperacion();
            codigo = tipo != 0 ? new CodigoOperacion { Tipo = tipo.ToString(), SubTipo = subtipo == 0 ? subtipo.ToString() : "0" } : null;

            if (nombreSolicitante == null)
                nombreSolicitante = "000";
            if (nombreSolicitante.Length < 3)
                nombreSolicitante = nombreSolicitante.PadLeft(3).Replace(' ', '0');
            if (nombreSolicitante.Length >= 21)
                nombreSolicitante = nombreSolicitante.Substring(0, 21);

            var request = new ConsultarPorCuentaRequest1
            {
                ConsultarPorCuentaRequest = new ConsultarPorCuentaRequest
                {
                    Busqueda = new Busqueda
                    {
                        Periodo = new Periodo
                        {
                            Desde = Desde.ToString("yyyy-MM-dd HH:mm:ss"),
                            Hasta = Hasta.ToString("yyyy-MM-dd HH:mm:ss")
                        },
                        Pagina = PaginaInicio == 0 ? 1 + "" : Pagina,
                        Codigo = codigo,
                    },
                    Autenticacion = new wsOperaciones.Autenticacion
                    {
                        Usuario = "broxel",
                        Password = Helper.CipherPassCREA("bRoXeL_1.2.3.4"),
                    },
                    Cuenta = cuenta,
                    Originador = new wsOperaciones.Originador
                    {
                        Solicitante = nombreSolicitante,
                        ZonaHoraria = "America/Mexico_City",
                    }
                }
            };



            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var result = _operadorWs.ConsultarPorCuenta(request);
            if (result.ConsultarPorCuentaResponse.Response.Codigo != "00") return result;
            var paginas = 0;
            if (result.ConsultarPorCuentaResponse.Listado != null && result.ConsultarPorCuentaResponse.Listado.Referencia != null && result.ConsultarPorCuentaResponse.Listado.Referencia.Paginas != null
            && result.ConsultarPorCuentaResponse.Listado.Referencia.Paginas.Total != null)
            {
                if (PaginaInicio == 0)
                {
                    Console.WriteLine(result.ConsultarPorCuentaResponse.Listado.Referencia.Paginas.Total);
                    paginas = Int32.Parse(result.ConsultarPorCuentaResponse.Listado.Referencia.Paginas.Total);
                    PaginaInicio = paginas > 1 ? paginas - 1 : paginas;
                    request.ConsultarPorCuentaRequest.Busqueda.Pagina = PaginaInicio + "";
                    result = _operadorWs.ConsultarPorCuenta(request);
                }
                var lista = new List<Operacion>();

                if (result.ConsultarPorCuentaResponse.Response.Codigo == "00")
                {
                    foreach (var op in result.ConsultarPorCuentaResponse.Listado.Operaciones.Where(x => x.Codigo.Tipo != "129"))
                    {
                        tar = Helper.DechiperCREA(op.Tarjeta);
                        op.Tarjeta = "**** " + tar.Substring(12);
                        lista.Add(op);
                    }
                    paginas = Int32.Parse(result.ConsultarPorCuentaResponse.Listado.Referencia.Paginas.Total);
                    if (PaginaInicio == 0)
                        paginas--;
                    if (!UnaPagina)
                    {
                        if (PaginaFin > 1) paginas = PaginaFin - PaginaInicio;
                        for (int i = PaginaInicio + 1; i <= paginas; i++)
                        {
                            request.ConsultarPorCuentaRequest.Busqueda.Pagina = i.ToString();
                            var t = _operadorWs.ConsultarPorCuenta(request);
                            var count = 0;
                            foreach (var op in t.ConsultarPorCuentaResponse.Listado.Operaciones.Where(x => x.Codigo.Tipo != "129" && x.Codigo.Tipo != "33"))
                            {
                                count++;
                                tar = Helper.DechiperCREA(op.Tarjeta);
                                op.Tarjeta = "**** " + tar.Substring(12);
                                lista.Add(op);
                            }

                        }
                    }
                    result.ConsultarPorCuentaResponse.Listado.Referencia.Registros.Actual = (Convert.ToInt32(
                    result.ConsultarPorCuentaResponse.Listado.Referencia.Registros.Actual) + lista.Count()).ToString();
                    result.ConsultarPorCuentaResponse.Listado.Operaciones = lista.ToArray();
                }
            }

            BroxelEntities _broxelEntities = new BroxelEntities();
            Movimiento mov = new Movimiento
            {
                Autorizado = result.ConsultarPorCuentaResponse.Response.Codigo == "00",
                TipoTransaccion = 127,
                SubTipoTransaccion = 0,
                NumCuenta = cuenta,
            };
            try
            {
                _broxelEntities.Movimiento.Add(mov);
                _broxelEntities.SaveChanges();
            }
            catch (Exception e)
            {
                string DoNothing = e.ToString();
            }

            return result;
        }

        private ConsultarPorTarjetaResponse1 ConsultaPorTarjeta(String tarjeta, String nombreSolicitante, String desde = "", String hasta = "", String paginaInicio = "", String paginaFin = "", String tipo = "", String subtipo = "")
        {
            DateTime Desde = desde == "" ? DateTime.Now.AddMonths(-1).AddDays(-15) : Convert.ToDateTime(desde);
            DateTime Hasta = hasta == "" ? DateTime.Now : Convert.ToDateTime(hasta);
            String Pagina = paginaInicio == "" ? "1" : paginaInicio;

            int PaginaInicio = paginaInicio == "" ? 1 : Int32.Parse(paginaInicio);
            int PaginaFin = paginaFin == "" ?
                    paginaInicio == "" ? 1 : Int32.Parse(paginaInicio)
                : Int32.Parse(paginaFin);

            String tar;
            CodigoOperacion codigo = new CodigoOperacion();
            codigo = !string.IsNullOrEmpty(tipo) ? new CodigoOperacion { Tipo = tipo, SubTipo = !string.IsNullOrEmpty(subtipo) ? subtipo : "0" } : null;

            if (nombreSolicitante == null)
                nombreSolicitante = "000";
            if (nombreSolicitante.Length < 3)
                nombreSolicitante = nombreSolicitante.PadLeft(3).Replace(' ', '0');
            if (nombreSolicitante.Length >= 21)
                nombreSolicitante = nombreSolicitante.Substring(0, 22);

            var request = new ConsultarPorTarjetaRequest1
            {
                ConsultarPorTarjetaRequest = new ConsultarPorTarjetaRequest
                {
                    Busqueda = new Busqueda
                    {
                        Periodo = new Periodo
                        {
                            Desde = Desde.ToString("yyyy-MM-dd HH:mm:ss"),
                            Hasta = Hasta.ToString("yyyy-MM-dd HH:mm:ss")
                        },
                        Pagina = Pagina == "0" ? "1" : Pagina,
                        Codigo = codigo,
                    },
                    Autenticacion = new wsOperaciones.Autenticacion
                    {
                        Usuario = "broxel",
                        Password = Helper.CipherPassCREA("bRoXeL_1.2.3.4"),
                    },
                    Tarjeta = Helper.CipherPassCREA(tarjeta),
                    Originador = new wsOperaciones.Originador
                    {
                        Solicitante = nombreSolicitante,
                        ZonaHoraria = "America/Mexico_City",
                    }
                }
            };
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var result = _operadorWs.ConsultarPorTarjeta(request);
            var paginas = 0;
            var lista = new List<Operacion>();

            if (result.ConsultarPorTarjetaResponse.Response.Codigo == "00")
            {
                foreach (var op in result.ConsultarPorTarjetaResponse.Listado.Operaciones.Where(x => x.Codigo.Tipo != "129"))
                {
                    tar = Helper.DechiperCREA(op.Tarjeta);
                    op.Tarjeta = "**** " + tar.Substring(12);
                    lista.Add(op);
                }

                paginas = Int32.Parse(result.ConsultarPorTarjetaResponse.Listado.Referencia.Paginas.Total);
                for (int i = PaginaInicio + 1; i <= (PaginaFin < PaginaInicio ? 0 : PaginaFin - PaginaInicio); i++)
                {
                    request.ConsultarPorTarjetaRequest.Busqueda.Pagina = i.ToString();
                    var t = _operadorWs.ConsultarPorTarjeta(request);
                    foreach (var op in t.ConsultarPorTarjetaResponse.Listado.Operaciones.Where(x => x.Codigo.Tipo != "129" && x.Codigo.Tipo != "33"))
                    {
                        tar = Helper.DechiperCREA(op.Tarjeta);
                        op.Tarjeta = "**** " + tar.Substring(12);
                        lista.Add(op);
                    }
                }
                result.ConsultarPorTarjetaResponse.Listado.Referencia.Registros.Actual = (Convert.ToInt32(
                    result.ConsultarPorTarjetaResponse.Listado.Referencia.Registros.Actual) + lista.Count()).ToString();
                //result.ConsultarPorTarjetaResponse.Listado.Referencia.Paginas.Actual = result.ConsultarPorTarjetaResponse.Listado.Referencia.Paginas.Total;
                result.ConsultarPorTarjetaResponse.Listado.Operaciones = lista.ToArray();
            }
            return result;
        }

        private CargoDeleteResponse DeleteCargo(CargoEditRequest request)
        {
            BroxelEntities _ctx = new BroxelEntities();
            CargoDeleteResponse response = new CargoDeleteResponse();
            Movimiento cargo = _ctx.Movimiento.FirstOrDefault(m => m.idMovimiento == request.IdCargo);
            if (cargo != null)
            {
                DateTime currentTime = DateTime.Now;
                AnulacionN anul = new AnulacionN
                {
                    Fecha = DateTime.Now,
                    idTransaccion = request.IdCargo,
                    idUsuario = request.UserID,
                    TipoTransaccion = 3,
                };
                _ctx.AnulacionN.Add(anul);
                _ctx.SaveChanges();
                cargo.idComercio = Convert.ToInt32(cargo.idComercio);
                Comercio com = (from x in _ctx.Comercio where x.idComercio == cargo.idComercio select x).First();
                var procesador = Helper.GetEngineByCard(request.Tarjeta.NumeroTarjeta);

                if (procesador.Procesador == 1)
                {
                    var autorizarReq = new AutorizarRequest
                    {
                        Canal = "BroxelWeb",
                        TipoTransaccion = "3",
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
                        NombreComercio = com.Comercio1
                    };
                    ServicePointManager.ServerCertificateValidationCallback +=
                        (sender, cert, chain, sslPolicyErrors) => true;

                    var reqInt = new InspectorBehavior();
                var handler = (AutorizacionPortTypeClient)_autorizadorWs;
                handler.Endpoint.Behaviors.Clear();
                handler.Endpoint.Behaviors.Add(reqInt);
                var dateTimeInicio = DateTime.Now;
                _autorizarResp = _autorizadorWs.Autorizar(autorizarReq);
                var dateTimeFin = DateTime.Now;
                var reqXml = reqInt.LastRequestXml;
                var resXml = reqInt.LastResponseXml;

                    try
                    {
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 19, 2,
                                reqXml, resXml, 0, anul.idAnulacion);
                        });
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
                    }

                    anul.Autorizado = _autorizarResp.CodigoRespuesta.Trim() == ("-1");
                    anul.NumAutorizacion = Convert.ToInt32(_autorizarResp.CodigoAutorizacion);
                    _ctx.SaveChanges();
                    int codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                    CodigosRespuestaSQL cr = _ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
                    if (cr != null)
                        response.UserResponse = cr.Descripcion;
                    response.Success = anul.Autorizado == true ? 1 : 0;
                    response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
                    response.IdAnulacion = anul.idAnulacion;
                    response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                    response.FechaCreacion = Convert.ToDateTime(anul.Fecha).ToString("yyyy-MM-dd HH:mm:ss");
                    //GuardarBitacora(request, response);
                }
                if (procesador.Procesador == 2)
                {
                    Dispatcher.Dispatcher dis = new Dispatcher.Dispatcher();
                    string reference = dis.GetPetrusReferenceByIdCharge(request.IdCargo);
                    PetrusCancellationRequest petrusRequest = new PetrusCancellationRequest()
                    {
                        BranchIdentifier = dis.GetBranchByIdCommerce((long)cargo.idComercio),
                        Card = new Card()
                        {
                            Number = request.Tarjeta.NumeroTarjeta,
                            DueMonth = int.Parse(request.Tarjeta.FechaExpira.Substring(0, 2)),
                            DueYear = int.Parse(request.Tarjeta.FechaExpira.Substring(2, 2)),
                            SecurityCode = int.Parse(string.IsNullOrEmpty(request.Tarjeta.Cvc2) ? "0" : request.Tarjeta.Cvc2)
                        },
                        Transaction = new TransactionCancellation()
                        {
                            InstallmentQuantity = 1,
                            SalePlan = 2,
                            Amount = cargo.Monto != null ? int.Parse((cargo.Monto.Value * 100).ToString("#")) : 0,
                            CurrencyIsoCode = 484,
                            ReferenceNumber = reference,
                            TransactionType = 6 // NEED TO CHECK PETRUS CONSULTANT
                        }
                    };

                    //EXECUTE PETRUS
                    var petrusResponse = dis.PetrusExecuteCancellation(petrusRequest, anul.idAnulacion);


                    anul.Autorizado = (!petrusResponse.Errors.Any() && petrusResponse.Response.Code == "00");
                    anul.NumAutorizacion2 = petrusResponse.ReferenceNumber;
                    _ctx.SaveChanges();
                    response.Success = anul.Autorizado == true ? 1 : 0;
                    response.NumeroAutorizacion = petrusResponse.ReferenceNumber;
                    response.IdAnulacion = anul.idAnulacion;
                    response.CodigoRespuesta = (bool)anul.Autorizado ? -1 : 999;
                    response.FechaCreacion = Convert.ToDateTime(anul.Fecha).ToString("yyyy-MM-dd HH:mm:ss");
                    anul.MensajeRespuesta = (bool)anul.Autorizado ? "Transaccion Exitosa" : "Error en la transacción: " + dis.GetMessageFromResponseCode(petrusResponse.Response.Code, typeof(Dispatcher.ErrorCodes));
                    response.UserResponse = anul.MensajeRespuesta;
                    //GuardarBitacora(request, response);
                }
            }
            return response;
        }

        private CargoDeleteResponse DeleteCargoNew(CargoEditRequest request)
        {
            BroxelEntities _ctx = new BroxelEntities();
            CargoDeleteResponse response = new CargoDeleteResponse();
            Movimiento cargo = _ctx.Movimiento.FirstOrDefault(m => m.idMovimiento == request.IdCargo);
            if (cargo != null)
            {
                DateTime currentTime = DateTime.Now;
                AnulacionN anul = new AnulacionN
                {
                    Fecha = DateTime.Now,
                    idTransaccion = request.IdCargo,
                    idUsuario = request.UserID,
                    TipoTransaccion = 3,
                };
                _ctx.AnulacionN.Add(anul);
                _ctx.SaveChanges();
                cargo.idComercio = Convert.ToInt32(cargo.idComercio);
                Comercio com = (from x in _ctx.Comercio where x.idComercio == cargo.idComercio select x).First();
                var procesador = Helper.GetEngineByCard(request.Tarjeta.NumeroTarjeta);

                if (procesador.Procesador == 1)
                {
                    var autorizarReq = new newAutorizacion.AutorizarRequest
                    {
                        Canal = "BroxelWeb",
                        TipoTransaccion = "3",
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
                        NombreComercio = com.Comercio1
                    };
                    ServicePointManager.ServerCertificateValidationCallback +=
                        (sender, cert, chain, sslPolicyErrors) => true;

                    var reqInt = new InspectorBehavior();
                    var handler = (newAutorizacion.AutorizacionPortTypeClient)_newAutorizadorWs;
                    handler.Endpoint.Behaviors.Clear();
                    handler.Endpoint.Behaviors.Add(reqInt);
                    var dateTimeInicio = DateTime.Now;
                    var autorizarResp = _newAutorizadorWs.Autorizar(autorizarReq);
                    var dateTimeFin = DateTime.Now;
                    var reqXml = reqInt.LastRequestXml;
                    var resXml = reqInt.LastResponseXml;

                    try
                    {
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 19, 2,
                                reqXml, resXml, 0, anul.idAnulacion);
                        });
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
                    }

                    anul.Autorizado = autorizarResp.CodigoRespuesta.Trim() == ("-1");
                    anul.NumAutorizacion = Convert.ToInt32(autorizarResp.CodigoAutorizacion);
                    _ctx.SaveChanges();
                    int codresp = Convert.ToInt32(autorizarResp.CodigoRespuesta);
                    CodigosRespuestaSQL cr = _ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
                    if (cr != null)
                        response.UserResponse = cr.Descripcion;
                    response.Success = anul.Autorizado == true ? 1 : 0;
                    response.NumeroAutorizacion = autorizarResp.CodigoAutorizacion;
                    response.IdAnulacion = anul.idAnulacion;
                    response.CodigoRespuesta = Convert.ToInt32(autorizarResp.CodigoRespuesta);
                    response.FechaCreacion = Convert.ToDateTime(anul.Fecha).ToString("yyyy-MM-dd HH:mm:ss");
                    //GuardarBitacora(request, response);
                }
                if (procesador.Procesador == 2)
                {
                    Dispatcher.Dispatcher dis = new Dispatcher.Dispatcher();
                    string reference = dis.GetPetrusReferenceByIdCharge(request.IdCargo);
                    PetrusCancellationRequest petrusRequest = new PetrusCancellationRequest()
                    {
                        BranchIdentifier = dis.GetBranchByIdCommerce((long)cargo.idComercio),
                        Card = new Card()
                        {
                            Number = request.Tarjeta.NumeroTarjeta,
                            DueMonth = int.Parse(request.Tarjeta.FechaExpira.Substring(0, 2)),
                            DueYear = int.Parse(request.Tarjeta.FechaExpira.Substring(2, 2)),
                            SecurityCode = int.Parse(string.IsNullOrEmpty(request.Tarjeta.Cvc2) ? "0" : request.Tarjeta.Cvc2)
                        },
                        Transaction = new TransactionCancellation()
                        {
                            InstallmentQuantity = 1,
                            SalePlan = 2,
                            Amount = cargo.Monto != null ? int.Parse((cargo.Monto.Value * 100).ToString("#")) : 0,
                            CurrencyIsoCode = 484,
                            ReferenceNumber = reference,
                            TransactionType = 6 // NEED TO CHECK PETRUS CONSULTANT
                        }
                    };

                    //EXECUTE PETRUS
                    var petrusResponse = dis.PetrusExecuteCancellation(petrusRequest,cargo.idMovimiento);


                    anul.Autorizado = (!petrusResponse.Errors.Any() && petrusResponse.Response.Code == "00");
                    anul.NumAutorizacion2 = petrusResponse.ReferenceNumber;
                    _ctx.SaveChanges();
                    response.Success = anul.Autorizado == true ? 1 : 0;
                    response.NumeroAutorizacion = petrusResponse.ReferenceNumber;
                    response.IdAnulacion = anul.idAnulacion;
                    response.CodigoRespuesta = (bool)anul.Autorizado?-1:999;
                    response.FechaCreacion = Convert.ToDateTime(anul.Fecha).ToString("yyyy-MM-dd HH:mm:ss");
                    anul.MensajeRespuesta = (bool)anul.Autorizado ? "Transaccion Exitosa" : "Error en la transacción: " + dis.GetMessageFromResponseCode(petrusResponse.Response.Code, typeof(Dispatcher.ErrorCodes));
                    response.UserResponse = anul.MensajeRespuesta;
                    //GuardarBitacora(request, response);
                }
            }
            return response;
        }
        private CargoDeleteResponse DeletePago(CargoEditRequest request)
        {
            BroxelEntities _ctx = new BroxelEntities();
            CargoDeleteResponse response = new CargoDeleteResponse();
            Movimiento cargo = _ctx.Movimiento.FirstOrDefault(m => m.idMovimiento == request.IdCargo);
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
                _ctx.AnulacionN.Add(anul);
                _ctx.SaveChanges();
                cargo.idComercio = Convert.ToInt32(cargo.idComercio);
                Comercio com = (from x in _ctx.Comercio where x.idComercio == cargo.idComercio select x).First();
                _autorizarReq = new AutorizarRequest
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
                    NombreComercio = com.Comercio1
                };
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => true;

                var reqInt = new InspectorBehavior();
                var handler = (AutorizacionPortTypeClient)_autorizadorWs;
                handler.Endpoint.Behaviors.Clear();
                handler.Endpoint.Behaviors.Add(reqInt);
                var dateTimeInicio = DateTime.Now;
                _autorizarResp = _autorizadorWs.Autorizar(_autorizarReq);
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

                anul.Autorizado = _autorizarResp.CodigoRespuesta.Trim() == ("-1");
                anul.NumAutorizacion = Convert.ToInt32(_autorizarResp.CodigoAutorizacion);
                _ctx.SaveChanges();
                int codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                CodigosRespuestaSQL cr = _ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
                if (cr != null)
                    response.UserResponse = cr.Descripcion;
                response.Success = anul.Autorizado == true ? 1 : 0;
                response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
                response.IdAnulacion = anul.idAnulacion;
                response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                response.FechaCreacion = Convert.ToDateTime(anul.Fecha).ToString("yyyy-MM-dd HH:mm:ss");
                //GuardarBitacora(request, response);
            }
            return response;
        }
		private CargoDeleteResponse DeletePagoNew(CargoEditRequest request)
		{
			var d = new CargoDeleteResponse();
			return d = new ReversoBL().EliminarPago(request);
		}
		private NominarCuentaResponse Nominar(NominarCuentaRequest request, string numCuenta)
        {
            Trace.WriteLine(DateTime.Now.ToString("O") + "Dentro de Nominar, cuenta " + numCuenta);
            BroxelEntities _broxelEntities = new BroxelEntities();
            DateTime FechaHoraCreacion = DateTime.Now;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            String cuentaString = request.Cuenta.ToXML();
            cuentaString = cuentaString.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            cuentaString = cuentaString.Replace("<", "&lt;");
            cuentaString = cuentaString.Replace("/ >", "/>");
            cuentaString = cuentaString.Replace("/ >", "/>");
            cuentaString = cuentaString.Replace(" />", "/>");
            var xmlRequest = cuentaString;
            WebRequest webReq = HttpWebRequest.Create("https://" + ConfigurationManager.AppSettings["CredencialHost"] + ":" + ConfigurationManager.AppSettings["CredencialPort"] + "/services/RemoteOperations");
            XmlDocument soapRequestXml = new XmlDocument();
            String requestToString = String.Empty;
            String IdentificadorTransaccion;

            soapRequestXml.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                   "<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                        "<s:Body xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
                                        "<Execute xmlns=\"http://org.apache.synapse/xsd\">" +
                                            "<Operation>NominacionCuenta</Operation>" +
                                            "<Session xsi:nil=\"true\"/>" +
                                            "<PrevTransactionKey xsi:nil=\"true\"/>" +
                                            "<PrivateData xsi:nil=\"true\"/>" +
                                            "<Encoding xsi:nil=\"true\">XML</Encoding>" +
                                            "<Schema xsi:nil=\"true\">NominacionCuenta</Schema>" +
                                            "<Message>" + cuentaString + "</Message>" +
                                        "</Execute>" +
                                        "</s:Body>" +
                                   "</s:Envelope>");
            webReq.Method = "POST";
            Stream requestStream = webReq.GetRequestStream();
            soapRequestXml.Save(requestStream);

            WebResponse httpResponse = webReq.GetResponse();
            StreamReader responseStream = new StreamReader(httpResponse.GetResponseStream());
            String soapResponseString = responseStream.ReadToEnd();
            XmlDocument soapResponseXml = new XmlDocument();
            var xmlResponse = soapResponseString;
            soapResponseString = soapResponseString.Replace("xsd:", "");
            soapResponseXml.LoadXml(soapResponseString);

            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                soapRequestXml.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                requestToString = stringWriter.GetStringBuilder().ToString();
            }
            try
            {
                string xmlFragment = soapResponseXml.InnerXml;  //credencialResp.Movimientos.InnerXml.ToString();
                xmlFragment = xmlFragment.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "").Replace("<soapenv:Envelope xmlns:soapenv=\"http://www.w3.org/2003/05/soap-envelope\">", "").Replace("<soapenv:Body>", "").Replace("</soapenv:Body>", "").Replace("</soapenv:Envelope>", "").Replace("xmlns=\"http://ws.apache.org/ns/synapse\"", "");
                StringReader strReader = new StringReader(xmlFragment);
                Trace.WriteLine(DateTime.Now.ToString("O") + "Antes de deserializar: " + numCuenta);
                XDocument xmlDoc = XDocument.Load(strReader);
                var TarjetaArray = from X in xmlDoc.Descendants("Response") select new { ReturnCode = X.Element("ReturnCode").Value, ReturnMessage = X.Element("ReturnMessage").Value, Message = X.Element("Message").Value };
                //var IdentificadorTransaccion = from X in TarjetaArray.First().Message.Descendants() select new {IdentificadorTransaccion = X.Element("IdentificadorTransaccion").Value};
                var MessageString = TarjetaArray.First().Message;
                var PosIniciaIdentificador = MessageString.IndexOf("&lt;IdentificadorTransaccion&gt;");
                var PosFinalIdentificador = MessageString.IndexOf("&lt;/IdentificadorTransaccion&gt;");
                IdentificadorTransaccion = (PosIniciaIdentificador != PosFinalIdentificador && PosIniciaIdentificador != -1) ? MessageString.Substring(PosIniciaIdentificador + 32, PosFinalIdentificador - PosIniciaIdentificador - 32) : "999";

                Trace.WriteLine(DateTime.Now.ToString("O") + "Despues de deserializar : " + numCuenta);
                NominarCuentaResponse nominar = new NominarCuentaResponse
                {
                    CodigoRespuesta = Convert.ToInt32(TarjetaArray.First().ReturnCode),
                    UserResponse = TarjetaArray.First().ReturnMessage,
                    Success = TarjetaArray.First().ReturnCode == "-1" ? 1 : 0,
                    FechaCreacion = Convert.ToString(FechaHoraCreacion),
                    NumeroAutorizacion = IdentificadorTransaccion,
                };

                var _renominacion = new Renominacion
                {
                    Cuenta = numCuenta,
                    CodigoRespuesta = nominar.CodigoRespuesta.ToString(),
                    FechaHoraCreacion = FechaHoraCreacion,
                    MensajeRespuesta = nominar.UserResponse,
                    Request = requestToString,
                    Response = soapResponseXml.InnerXml,
                    IdentificadorTransaccion = IdentificadorTransaccion
                };
                _broxelEntities.Renominacion.Add(_renominacion);
                try
                {
                    _broxelEntities.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    StringBuilder exception = new StringBuilder();
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        exception.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            exception.AppendFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com, jesus.valdiviezo@broxel.com", "Error Renominando  ", "Error " + exception.ToString(),
                          "yMQ3E3ert6", "Errores ");
                }
                catch (Exception e)
                {
                    Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com, jesus.valdiviezo@broxel.com", "Error renominando 2  ", "Error " + e.ToString(),
                         "yMQ3E3ert6", "Errores ");
                }
                nominar.IdRenominacion = _renominacion.Id;
                return nominar;

            }
            catch (Exception ex)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + "Error al analizar respuesta de credencial renominacion cuenta " + numCuenta + ": request: " + xmlRequest + "; response: " + xmlResponse + " Error: " + ex);
                throw ex;
            }
        }

        private NominarCuentaResponse Nominar(NominarCuentaRequest request)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            String cuentaString = request.Cuenta.ToXML();
            cuentaString = cuentaString.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            cuentaString = cuentaString.Replace("<", "&lt;");
            cuentaString = cuentaString.Replace("/ >", "/>");
            cuentaString = cuentaString.Replace("/ >", "/>");
            cuentaString = cuentaString.Replace(" />", "/>");
            WebRequest webReq = HttpWebRequest.Create("https://" + ConfigurationManager.AppSettings["CredencialHost"] + ":" + ConfigurationManager.AppSettings["CredencialPort"] + "/services/RemoteOperations");
            XmlDocument soapRequestXml = new XmlDocument();

            soapRequestXml.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                   "<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                        "<s:Body xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
                                        "<Execute xmlns=\"http://org.apache.synapse/xsd\">" +
                                            "<Operation>NominacionCuenta</Operation>" +
                                            "<Session xsi:nil=\"true\"/>" +
                                            "<PrevTransactionKey xsi:nil=\"true\"/>" +
                                            "<PrivateData xsi:nil=\"true\"/>" +
                                            "<Encoding xsi:nil=\"true\">XML</Encoding>" +
                                            "<Schema xsi:nil=\"true\">NominacionCuenta</Schema>" +
                                            "<Message>" + cuentaString + "</Message>" +
                                        "</Execute>" +
                                        "</s:Body>" +
                                   "</s:Envelope>");
            webReq.Method = "POST";
            Stream requestStream = webReq.GetRequestStream();
            soapRequestXml.Save(requestStream);


            WebResponse httpResponse = webReq.GetResponse();
            StreamReader responseStream = new StreamReader(httpResponse.GetResponseStream());
            String soapResponseString = responseStream.ReadToEnd();
            XmlDocument soapResponseXml = new XmlDocument();
            soapResponseXml.LoadXml(soapResponseString);

            string xmlFragment = soapResponseXml.InnerXml;  //credencialResp.Movimientos.InnerXml.ToString();
            xmlFragment = xmlFragment.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "").Replace("<soapenv:Envelope xmlns:soapenv=\"http://www.w3.org/2003/05/soap-envelope\">", "").Replace("<soapenv:Body>", "").Replace("</soapenv:Body>", "").Replace("</soapenv:Envelope>", "").Replace("xmlns=\"http://ws.apache.org/ns/synapse\"", "");
            StringReader strReader = new StringReader(xmlFragment);

            XDocument xmlDoc = XDocument.Load(strReader);
            var TarjetaArray = from X in xmlDoc.Descendants("Response") select new { ReturnCode = X.Element("ReturnCode").Value, ReturnMessage = X.Element("ReturnMessage").Value };
            NominarCuentaResponse nominar = new NominarCuentaResponse
            {
                CodigoRespuesta = Convert.ToInt32(TarjetaArray.First().ReturnCode),
                UserResponse = TarjetaArray.First().ReturnMessage,
                Success = TarjetaArray.First().ReturnCode == "-1" ? 1 : 0,
                FechaCreacion = Convert.ToString(DateTime.Now),
            };
            //GuardarBitacora(request, nominar);
            return nominar;
        }


        /// <summary>
        /// Regresa los movimientos de la pagina indicada
        /// </summary>
        /// <param name="request">Request de tipo MovimientosRequest</param>
        /// <param name="pagina">Numero de pagina para traer los movimientos</param>
        /// <returns>MovimientosResponse</returns>
        private MovimientosResponse GetMovimientos(MovimientosRequest request, int pagina)
        {
            broxelco_rdgEntities _ctx = new broxelco_rdgEntities();
            MovimientosResponse response = new MovimientosResponse();
            DateTime currentDate = DateTime.Now;
            ListadoOperaciones lo = new ListadoOperaciones();

            if (!string.IsNullOrEmpty(request.NumCuenta))
            {
                var p = ConsultaPorCuenta(request.NumCuenta, request.Solicitante, null, null, 0, 0, 0, 0);
                if (p.ConsultarPorCuentaResponse.Response.Codigo == "00")
                {
                    response.CodigoRespuesta = -1;

                    lo = p.ConsultarPorCuentaResponse.Listado;
                    response.Paginas = Int32.Parse(p.ConsultarPorCuentaResponse.Listado.Referencia.Paginas.Total);
                    response.PaginaActual = Int32.Parse(p.ConsultarPorCuentaResponse.Listado.Referencia.Paginas.Actual);
                    //p.ConsultarPorCuentaResponse.Listado.Referencia.Paginas
                }
                else
                {
                    response.CodigoRespuesta = 999;
                    response.UserResponse = p.ConsultarPorCuentaResponse.Response.Descripcion;
                }
            }
            else
            {
                var p = ConsultaPorTarjeta(request.Tarjeta.NumeroTarjeta, request.Solicitante, "", "", pagina + "", pagina + "", "", "");
                if (p.ConsultarPorTarjetaResponse.Response.Codigo == "00")
                {
                    response.CodigoRespuesta = -1;
                    lo = p.ConsultarPorTarjetaResponse.Listado;
                    response.Paginas = Int32.Parse(p.ConsultarPorTarjetaResponse.Listado.Referencia.Paginas.Total);
                    response.PaginaActual = Int32.Parse(p.ConsultarPorTarjetaResponse.Listado.Referencia.Paginas.Actual);
                }
                else
                {
                    response.CodigoRespuesta = 999;
                    response.UserResponse = p.ConsultarPorTarjetaResponse.Response.Descripcion;
                }
            }
            if (response.CodigoRespuesta == -1)
            {
                CodigosRespuesta cr = _ctx.CodigosRespuesta.FirstOrDefault(x => x.Id == response.CodigoRespuesta);
                if (cr != null)
                    response.UserResponse = cr.Descripcion;
            }

            response.Success = response.CodigoRespuesta == -1 ? 1 : 0;
            if (lo.Operaciones == null) return response;
            foreach (var operacion in lo.Operaciones.Where(x => x.Neutralizada == "N").OrderByDescending(x => Convert.ToDateTime(x.Momento)))
            {
                response.Movimientos.Add(new App_Code.Movimiento
                {
                    Tarjeta = operacion.Tarjeta,
                    Aprobada = operacion.Respuesta.Codigo == "00",
                    Comercio = (operacion.Codigo.Tipo == "35" || (operacion.Codigo.Tipo == "33" && operacion.Codigo.SubTipo != "2")) ? ("SU PAGO - GRACIAS")
                    : (operacion.Codigo.Tipo == "257" ? "CAMBIO DE NIP"
                    : (operacion.Comercio.Codigo == "999999999001845" ? operacion.Codigo.Descripcion
                    : ((operacion.Codigo.Tipo == "1027" && operacion.Codigo.SubTipo == "0") ? "SU PAGO - GRACIAS"
                    : (operacion.Comercio.Codigo.StartsWith("999999") ? GetNombreComercioPorCodigo(operacion.Comercio.Codigo)
                    : operacion.Comercio.Descripcion)))),
                    Fecha = operacion.Momento,
                    Monto = (operacion.Codigo.Tipo == "35" || operacion.Codigo.Tipo == "33" || operacion.Codigo.Tipo == "1027") ? (Convert.ToDouble(operacion.Montos.Original.Importe) * -1).ToString() : operacion.Montos.Original.Importe,
                    Moneda = operacion.Montos.Original.Moneda.Descripcion,
                    MensajeRespuesta = operacion.Respuesta.Descripcion,
                });

            }
            return response;
        }

		private SaldoResponse GetSaldos(OperarCuentaRequest request)
		{
			SaldoResponse response = new SaldoResponse();
			if (request.Solicitante == null)
				request.Solicitante = "000";
			if (request.Solicitante.Length < 3)
				request.Solicitante = request.Solicitante.PadLeft(3).Replace(' ', '0');
			if (request.Solicitante.Length >= 21)
				request.Solicitante = request.Solicitante.Substring(0, 21);
		    try
		    {
		        var procesador = !string.IsNullOrEmpty(request.NumCuenta) ? Helper.GetTarjetaFromCuenta(request.NumCuenta).Procesador : Helper.GetTarjetaFromTarjeta(request.Tarjeta.NumeroTarjeta).Procesador;

		        switch (procesador)
		        {
		            case 1:
		                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
		                var cr = _cuentaWs.Consultar(new ConsultarRequest1
		                {
		                    ConsultarRequest = new ConsultarRequest
		                    {
		                        Autenticacion = new Autenticacion
		                        {
		                            Usuario = "broxel",
		                            Password = Helper.CipherPassCREA("bRoXeL_1.2.3.4"),
		                        },

		                        Cuenta = request.NumCuenta,
		                        Originador = new Originador
		                        {
		                            Solicitante = request.Solicitante,
		                            ZonaHoraria = "America/Mexico_City",
		                        }
		                    }
		                }).ConsultarResponse;

		                response.Success = cr.Response.Codigo == "00" ? 1 : 0;
		                response.CodigoRespuesta = cr.Response.Codigo == "00" ? -1 : Convert.ToInt32(cr.Response.Codigo);
		                response.UserResponse = cr.Response.Descripcion;
		                if (response.Success == 1)
		                {
		                    response.Saldos = new Saldos
		                    {
		                        DisponibleAdelantos = Convert.ToDecimal(cr.Cuenta.Disponibles.Adelantos),
		                        DisponibleCompras = Convert.ToDecimal(cr.Cuenta.Disponibles.Compras),
		                        DisponibleCuotas = Convert.ToDecimal(cr.Cuenta.Disponibles.Cuotas),
		                        DisponiblePrestamos = Convert.ToDecimal(cr.Cuenta.Disponibles.Prestamos),
		                        Saldo = Convert.ToDecimal(cr.Cuenta.Liquidacion.Saldo),
		                        LimiteCompra = Convert.ToDecimal(cr.Cuenta.Limites.Compras)
		                    };
		                    response.EstadoOperativo = cr.Cuenta.Estado.Descripcion;
		                    response.UltimaOperacionAprobada = cr.Cuenta.Actividad.MomentoUltimaOperacionAprobada;
		                }
		                break;
		            case 2:
		            
		                var balance = new GetBalanceDTO { InternalAccountNumber = request.NumCuenta };
		                var gb = _cuentaWsPetrus.GetBalance("103", balance);

		                response.Success = gb.ReturnExecution == ReturnExecution.SUCCESS ? 1 : 0;
		                response.CodigoRespuesta = gb.ReturnExecution == ReturnExecution.SUCCESS
		                    ? -1
		                    : 999;
		                response.UserResponse = gb.ReturnExecution == ReturnExecution.SUCCESS ? "Exito" : ((ErrorDTO)gb.Errors[0]).ErrorText;
		                if (response.Success == 1)
		                {
		                    response.Saldos = new Saldos
		                    {
		                        DisponibleAdelantos = gb.AccountBalance,
		                        DisponibleCompras = gb.AccountBalance,
		                        DisponibleCuotas = 0,
		                        DisponiblePrestamos = 0,
		                        Saldo = gb.AccountBalance,
		                        LimiteCompra = gb.CreditLimit,
		                    };
		                    //TODO Validar metodo que retorne estos datos
		                    response.EstadoOperativo = "Operativa";
		                }
		                break;
                    default:
                        throw new Exception("Procesador tipo " + procesador.ToString(CultureInfo.InvariantCulture) + " para la cuenta " + request.NumCuenta + " no está definido.");
		        }
		    }
		    catch (Exception e)
			{

				response.CodigoRespuesta = 997;
				response.Success = 0;
				response.UserResponse = ("Hubo un error al procesar la transacción. Reintente.");
				if (response.UserResponse.Length > 254)
					response.UserResponse = response.UserResponse.Substring(0, 254);
				Trace.WriteLine(DateTime.Now.ToString("O") + "GetSaldos: Error al realizar consulta de saldos: " + e);
			}
			GuardarBitacora(request, response);
			return response;
		}


		private void GuardarBitacora(Request request, Response response)
        {
            BroxelEntities _ctx = new BroxelEntities();
            StackFrame frame = new StackFrame(1);
            try
            {
                var numTarjeta = request.Tarjeta != null ? request.Tarjeta.NumeroTarjeta : "";
                if (numTarjeta.Length == 16)
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
            /*
            catch (DbEntityValidationException ex)
            {
                StringBuilder exception = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    exception.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        exception.AppendFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com, jesus.valdiviezo@broxel.com", "Error guardando Bitacora ", "Error " + exception.ToString(),
                      "yMQ3E3ert6", "Errores ");
            }
             */
            catch (DbEntityValidationException e)
            {
                //foreach (var eve in e.EntityValidationErrors)
                //{
                //    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                //            ve.PropertyName, ve.ErrorMessage);
                //    }
                //}
            }
            catch (Exception e)
            {
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Fallo en bitacora", e.ToString(), "67896789", "Broxel Online");
            }
        }

        private LimiteResponse ReverosoLimiteOri(LimiteRequest request, Movimiento mov)
        {
            BroxelEntities _ctx = new BroxelEntities();
            LimiteResponse response = new LimiteResponse();


            DateTime currentDate = DateTime.Now;
            try
            {
                AnulacionN anul = new AnulacionN
                {
                    TipoTransaccion = 33,
                    SubTipoTransaccion = Convert.ToInt32(request.Tipo.ToString(CultureInfo.InvariantCulture)),
                    Fecha = currentDate,
                    idTransaccion = mov.idMovimiento,
                    idUsuario = request.UserID,
                };
                _ctx.AnulacionN.Add(anul);
                _ctx.SaveChanges();
                try
                {
                    _autorizarReq = new AutorizarRequest
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
                    var handler = (AutorizacionPortTypeClient)_autorizadorWs;
                    handler.Endpoint.Behaviors.Clear();
                    handler.Endpoint.Behaviors.Add(reqInt);
                    var dateTimeInicio = DateTime.Now;
                    _autorizarResp = _autorizadorWs.Autorizar(_autorizarReq);
                    var dateTimeFin = DateTime.Now;
                    var reqXml = reqInt.LastRequestXml;
                    var resXml = reqInt.LastResponseXml;

                    try
                    {
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 12, 1,
                                reqXml, resXml, mov.idMovimiento, anul.idAnulacion);
                        });
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
                    }

                    int codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                    CodigosRespuestaSQL cr = _ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
                    if (cr != null)
                        response.UserResponse = cr.Descripcion;
                    anul.NumAutorizacion = Convert.ToInt32(_autorizarResp.CodigoAutorizacion);
                    anul.MensajeRespuesta = cr.Descripcion;
                    anul.Autorizado = _autorizarResp.CodigoRespuesta.Trim() == ("-1");
                    response.Success = anul.Autorizado == true ? 1 : 0;
                    response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
                    response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                    response.FechaCreacion = Convert.ToDateTime(anul.Fecha).ToString("yyyy-MM-dd HH:mm:ss");
                    response.IdMovimiento = anul.idAnulacion;
                    _ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    anul.NumAutorizacion = -1;
                    anul.Autorizado = false;
                    anul.MensajeRespuesta = "Problema en WebService : " + ex;
                    _ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                int a = 0;
                a++;
            }
            //GuardarBitacora(request, response);
            return response;
        }
		private LimiteResponse ReversoLimite(LimiteRequest request, Movimiento mov)
		{
			var r = new LimiteResponse();
			try
			{
				var rev = new ReversoBL().ReversoLimite(request, mov);
				if (r.Success == 1)
				{
					r.SaldoAntes = rev.SaldoAntes;
					r.SaldoDespues = rev.SaldoDespues;
					r.IdMovimiento = rev.IdMovimiento;
				}
				else
				{
					r.UserResponse = "Error al realizar el Reverso de la tarjeta: " + request.Tarjeta.NumeroTarjeta + "";
					r.CodigoRespuesta = 999;
					r.NumeroAutorizacion = "-1";
					r.SaldoAntes = 0;
					r.SaldoDespues = 0;
				}
				return r;
			}
			catch (Exception ex)
			{
				return r;
				int a = 0;
				a++;
			}
		}
		private LimiteResponse ReverosoLimiteNewOri(LimiteRequest request, Movimiento mov)
        {
            BroxelEntities _ctx = new BroxelEntities();
            LimiteResponse response = new LimiteResponse();
            DateTime currentDate = DateTime.Now;
            try
            {
                AnulacionN anul = new AnulacionN
                {
                    TipoTransaccion = 33,
                    SubTipoTransaccion = Convert.ToInt32(request.Tipo.ToString(CultureInfo.InvariantCulture)),
                    Fecha = currentDate,
                    idTransaccion = mov.idMovimiento,
                    idUsuario = request.UserID,
                };
                _ctx.AnulacionN.Add(anul);
                _ctx.SaveChanges();
                try
                {
                    var autorizarReq = new newAutorizacion.AutorizarRequest
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
                    var handler = (newAutorizacion.AutorizacionPortTypeClient)_newAutorizadorWs;
                    handler.Endpoint.Behaviors.Clear();
                    handler.Endpoint.Behaviors.Add(reqInt);
                    var dateTimeInicio = DateTime.Now;
                    var autorizarResp = _newAutorizadorWs.Autorizar(autorizarReq);
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
                    CodigosRespuestaSQL cr = _ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
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
                    _ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    anul.NumAutorizacion = -1;
                    anul.Autorizado = false;
                    anul.MensajeRespuesta = "Problema en WebService : " + ex;
                    _ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                int a = 0;
                a++;
            }
            //GuardarBitacora(request, response);
            return response;
        }
		private LimiteResponse ReversoLimiteNew(LimiteRequest request, Movimiento mov)
		{
			var r = new LimiteResponse();
			try
			{
				var rev = new ReversoBL().ReversoLimite(request, mov);
				if (r.Success == 1)
				{
					r.SaldoAntes = rev.SaldoAntes;
					r.SaldoDespues = rev.SaldoDespues;
					r.IdMovimiento = rev.IdMovimiento;
				}
				else
				{
					r.UserResponse = "Error al realizar el Reverso de la tarjeta: "+ request.Tarjeta.NumeroTarjeta +"";
					r.CodigoRespuesta = 999;
					r.NumeroAutorizacion = "-1";
					r.SaldoAntes = 0;
					r.SaldoDespues = 0;
				}
				return r;
			}
			catch (Exception ex)
			{
				return r;
				int a = 0;
				a++;
			}
		}
		#region SetCargo Methods
        /*
         * Que levante la mano el dueño.
		/// <summary>
		/// Creating a Movement by CargoRequest
		/// </summary>
		private Movimiento CreateMovement(BroxelEntities _ctx, CargoRequest request, DateTime currentDate)
        {
            Movimiento mov = new Movimiento
            {
                FechaExpira = request.Tarjeta.FechaExpira,
                Monto = request.Cargo.Monto,
                NoReferencia = request.Cargo.NoReferencia,
                NombreReferencia = request.Cargo.NombreReferencia,
                NombreTarjeta = request.Tarjeta.NombreTarjeta,
                FechaHoraCreacion = currentDate,
                idUsuario = request.UserID,
                ActivoLote = true,
                Tarjeta = Helper.TruncaTarjeta(request.Tarjeta.NumeroTarjeta),
                RegControl = false,
                Comercio = _ctx.Comercio.FirstOrDefault(c => c.idComercio == request.Cargo.IdComercio),
                NumCuenta = request.Tarjeta.Cuenta,
            };
            _ctx.Movimiento.Add(mov);
            _ctx.SaveChanges();

            return mov;
        }
        */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private CargoResponse SetCargo(CargoRequest request)
        {
            BroxelEntities _ctx = new BroxelEntities();
            CargoResponse response = new CargoResponse();
            try
            {
                DateTime currentDate = DateTime.Now;
                Movimiento mov = new Movimiento
                {
                    FechaExpira = request.Tarjeta.FechaExpira,
                    Monto = request.Cargo.Monto,
                    NoReferencia = request.Cargo.NoReferencia,
                    NombreReferencia = request.Cargo.NombreReferencia,
                    NombreTarjeta = request.Tarjeta.NombreTarjeta,
                    FechaHoraCreacion = currentDate,
                    TipoTransaccion = 1,
                    idUsuario = request.UserID,
                    UsuarioCreacion = _ctx.Usuario.FirstOrDefault(x => x.idUsuario == request.UserID).Usuario1,
                    ActivoLote = true,
                    Tarjeta = Helper.TruncaTarjeta(request.Tarjeta.NumeroTarjeta),
                    CVC = "**" + request.Tarjeta.Cvc2.ToString(CultureInfo.InvariantCulture).Substring(2),
                    RegControl = false,
                    Comercio = _ctx.Comercio.FirstOrDefault(c => c.idComercio == request.Cargo.IdComercio),
                    NumCuenta = request.Tarjeta.Cuenta,
                };
                _ctx.Movimiento.Add(mov);
                _ctx.SaveChanges();
                Comercio com = (from x in _ctx.Comercio where x.idComercio == request.Cargo.IdComercio select x).First();
                var disData = Helper.GetEngineByCard(request.Tarjeta.NumeroTarjeta);

                if (disData.Procesador == 1)
                {
                    try
                    {
                        _autorizarReq = new AutorizarRequest
                        {
                            SecuenciaTransaccion = mov.idMovimiento.ToString(CultureInfo.InvariantCulture),
                            Canal = "BroxelWeb",
                            TipoTransaccion = "1",
                            FechaTransaccion = currentDate.ToString("yyyyMMdd"),
                            HoraTransaccion = currentDate.ToString("HHmmss"),
                            Comercio = com.CodigoComercio,
                            NombreComercio = com.Comercio1,
                            Terminal = "PC",
                            ModoIngreso = "1",
                            Tarjeta = request.Tarjeta.NumeroTarjeta,
                            FechaExpiracion = request.Tarjeta.FechaExpira,
                            CodigoSeguridad = request.Tarjeta.Cvc2,
                            Importe = request.Cargo.Monto.ToString(CultureInfo.InvariantCulture),
                            CodigoMoneda = "484"
                        };
                        ServicePointManager.ServerCertificateValidationCallback +=
                            (sender, cert, chain, sslPolicyErrors) => true;

                        var reqInt = new InspectorBehavior();
                        var handler = (AutorizacionPortTypeClient)_autorizadorWs;
                        handler.Endpoint.Behaviors.Clear();
                        handler.Endpoint.Behaviors.Add(reqInt);
                        var dateTimeInicio = DateTime.Now;
                        _autorizarResp = _autorizadorWs.Autorizar(_autorizarReq);
                        var dateTimeFin = DateTime.Now;
                        var reqXml = reqInt.LastRequestXml;
                        var resXml = reqInt.LastResponseXml;

                        try
                        {
                            ThreadPool.QueueUserWorkItem(delegate
                            {
                                new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 14, 1,
                                    reqXml, resXml, mov.idMovimiento);
                            });
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
                        }

                    }
                    catch (Exception ex)
                    {
                        mov.NoAutorizacion = "-1";
                        mov.Autorizado = false;
                        mov.MensajeError = "Problema en WebService : " + ex;
                        _ctx.SaveChanges();
                        response.UserResponse = ex.ToString();
                        throw ex;
                    }
                    mov.Autorizado = _autorizarResp.CodigoRespuesta.Trim() == ("-1");
                    mov.NoAutorizacion = _autorizarResp.CodigoAutorizacion;
                    int codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                    CodigosRespuestaSQL cr = _ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
                    if (cr != null)
                        response.UserResponse = cr.Descripcion;
                    response.Success = mov.Autorizado == true ? 1 : 0;
                    response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
                    response.IdMovimiento = mov.idMovimiento;
                    response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                    response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
                    mov.MensajeError = cr.Descripcion;
                    _ctx.SaveChanges();
                }
                if (disData.Procesador == 2)
                {
                    PetrusChargeResponse petrusResponse = null;
                    Dispatcher.Dispatcher dis = new Dispatcher.Dispatcher();
                    try
                    {

                        Dispatcher.PetrusChargeRequest petrusRequest = new Dispatcher.PetrusChargeRequest()
                        {
                            BranchIdentifier = dis.GetBranchByIdCommerce(request.Cargo.IdComercio), //NEED TO CHECK PETRUS CONSULTANT
                            Card = new Dispatcher.Card()
                            {
                                Number = request.Tarjeta.NumeroTarjeta,
                                DueMonth = int.Parse(request.Tarjeta.FechaExpira.Substring(0, 2)), //Convert.ToDateTime(request.Tarjeta.FechaExpira, new CultureInfo("en-US")).Month,
                                DueYear = int.Parse(request.Tarjeta.FechaExpira.Substring(2, 2)), //int.Parse(Convert.ToDateTime(request.Tarjeta.FechaExpira, new CultureInfo("en-US")).ToString("yy")),
                                SecurityCode = int.Parse(string.IsNullOrEmpty(request.Tarjeta.Cvc2) ? "0" : request.Tarjeta.Cvc2)
                            },
                            Transaction = new Dispatcher.Transaction()
                            {
                                CurrencyIsoCode = 484,
                                InstallmentQuantity = 1, //NEED TO CHECK PETRUS CONSULTANT
                                SalePlan = 2, //NEED TO CHECK PETRUS CONSULTANT
                                Amount = int.Parse((request.Cargo.Monto * 100).ToString("#")), //REQUIRE CONVERT DECIMALS INTO STRING
                                TransactionType = 1,
                                AutoConfirmedTrx = 1, //NEED TO CHECK PETRUS CONSULTANT
                                PartialAuthorization = 1,
                                //MCC = 5072 //NEED TO CHECK
                            }
                        };
                        petrusResponse = dis.PetrusExecuteCharge(petrusRequest, mov.idMovimiento);
                    }
                    catch (Exception ex)
                    {
                        mov.NoAutorizacion = "-1";
                        mov.Autorizado = false;
                        mov.MensajeError = "Problema en WebService : " + ex;
                        _ctx.SaveChanges();
                        response.UserResponse = ex.ToString();
                        throw ex;
                    }
                    mov.Autorizado = (!petrusResponse.Errors.Any() && petrusResponse.Response.Code == "00");
                    mov.NoAutorizacion = petrusResponse.ReferenceNumber;
                    response.Saldo = petrusResponse.Ammount;
                    response.Success = mov.Autorizado == true ? 1 : 0;
                    response.NumeroAutorizacion = mov.NoAutorizacion;
                    response.IdMovimiento = mov.idMovimiento;
                    response.CodigoRespuesta = (bool)mov.Autorizado ? -1 : 999;
                    response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
                    mov.MensajeError = mov.Autorizado == true ? "Transacción Exitosa" : "Error en la transacción: " + dis.GetMessageFromResponseCode(petrusResponse.Response.Code, typeof(Dispatcher.ErrorCodes));
                    response.UserResponse = mov.MensajeError;
                    _ctx.SaveChanges();
                }

            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder exception = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    exception.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        exception.AppendFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com", "Error en cargo ", "Error " + exception.ToString(),
                      "yMQ3E3ert6", "Errores ");
                response.UserResponse = ex.ToString();
            }
            catch (Exception ex)
            {
                response.UserResponse = ex.ToString();
            }
            //GuardarBitacora(request, response);
            return response;
        }

        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private CargoResponse SetCargoNew(CargoRequest request)
        {
            var _ctx = new BroxelEntities();
            CargoResponse response = new CargoResponse();
            newAutorizacion.AutorizarResponse autorizarResp;
            
            try
            {
                DateTime currentDate = DateTime.Now;
                Movimiento mov = new Movimiento
                {
                    FechaExpira = request.Tarjeta.FechaExpira,
                    Monto = request.Cargo.Monto,
                    NoReferencia = request.Cargo.NoReferencia,
                    NombreReferencia = request.Cargo.NombreReferencia,
                    NombreTarjeta = request.Tarjeta.NombreTarjeta,
                    FechaHoraCreacion = currentDate,
                    TipoTransaccion = 1,
                    idUsuario = request.UserID,
                    UsuarioCreacion = _ctx.Usuario.FirstOrDefault(x => x.idUsuario == request.UserID).Usuario1,
                    ActivoLote = true,
                    Tarjeta = Helper.TruncaTarjeta(request.Tarjeta.NumeroTarjeta),
                    CVC = "**" + request.Tarjeta.Cvc2.ToString(CultureInfo.InvariantCulture).Substring(2),
                    RegControl = false,
                    Comercio = _ctx.Comercio.FirstOrDefault(c => c.idComercio == request.Cargo.IdComercio),
                    NumCuenta = request.Tarjeta.Cuenta,
                };
                _ctx.Movimiento.Add(mov);
                _ctx.SaveChanges();
                Comercio com = (from x in _ctx.Comercio where x.idComercio == request.Cargo.IdComercio select x).First();

                var disData = Helper.GetEngineByCard(request.Tarjeta.NumeroTarjeta);

                if (disData.Procesador == 1)
                {
                    try
                    {
                        var autorizarReq = new newAutorizacion.AutorizarRequest
                        {
                            SecuenciaTransaccion = mov.idMovimiento.ToString(CultureInfo.InvariantCulture),
                            Canal = "BroxelWeb",
                            TipoTransaccion = "1",
                            FechaTransaccion = currentDate.ToString("yyyyMMdd"),
                            HoraTransaccion = currentDate.ToString("HHmmss"),
                            Comercio = com.CodigoComercio,
                            NombreComercio = com.Comercio1,
                            Terminal = "PC",
                            ModoIngreso = "1",
                            Tarjeta = request.Tarjeta.NumeroTarjeta,
                            FechaExpiracion = request.Tarjeta.FechaExpira,
                            CodigoSeguridad = request.Tarjeta.Cvc2,
                            Importe = request.Cargo.Monto.ToString(CultureInfo.InvariantCulture),
                            CodigoMoneda = "484"
                        };
                        ServicePointManager.ServerCertificateValidationCallback +=
                            (sender, cert, chain, sslPolicyErrors) => true;

                        var reqInt = new InspectorBehavior();
                        var handler = (newAutorizacion.AutorizacionPortTypeClient)_newAutorizadorWs;
                        handler.Endpoint.Behaviors.Clear();
                        handler.Endpoint.Behaviors.Add(reqInt);
                        var dateTimeInicio = DateTime.Now;
                        autorizarResp = _newAutorizadorWs.Autorizar(autorizarReq);
                        var dateTimeFin = DateTime.Now;
                        var reqXml = reqInt.LastRequestXml;
                        var resXml = reqInt.LastResponseXml;

                        try
                        {
                            ThreadPool.QueueUserWorkItem(delegate
                            {
                                new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 20, 2,
                                    reqXml, resXml, mov.idMovimiento);
                            });
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
                        }

                    }
                    catch (Exception ex)
                    {
                        mov.NoAutorizacion = "-1";
                        mov.Autorizado = false;
                        mov.MensajeError = "Problema en WebService : " + ex;
                        _ctx.SaveChanges();
                        response.UserResponse = ex.ToString();
                        throw ex;
                    }
                    mov.Autorizado = autorizarResp.CodigoRespuesta.Trim() == ("-1");
                    mov.NoAutorizacion = autorizarResp.CodigoAutorizacion;
                    int codresp = Convert.ToInt32(autorizarResp.CodigoRespuesta);
                    CodigosRespuestaSQL cr = _ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
                    if (cr != null)
                        response.UserResponse = cr.Descripcion;
                    response.Success = mov.Autorizado == true ? 1 : 0;
                    response.NumeroAutorizacion = autorizarResp.CodigoAutorizacion;
                    response.IdMovimiento = mov.idMovimiento;
                    response.CodigoRespuesta = Convert.ToInt32(autorizarResp.CodigoRespuesta);
                    response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
                    mov.MensajeError = cr.Descripcion;
                    _ctx.SaveChanges();                    
                }
                if (disData.Procesador == 2)
                {
                    PetrusChargeResponse petrusResponse = null;
                    Dispatcher.Dispatcher dis = new Dispatcher.Dispatcher();
                    try
                    {
                        
                        Dispatcher.PetrusChargeRequest petrusRequest = new Dispatcher.PetrusChargeRequest()
                        {
                            BranchIdentifier = dis.GetBranchByIdCommerce(request.Cargo.IdComercio), //NEED TO CHECK PETRUS CONSULTANT
                            Card = new Dispatcher.Card()
                            {
                                Number = request.Tarjeta.NumeroTarjeta,
                                DueMonth = int.Parse(request.Tarjeta.FechaExpira.Substring(0, 2)), //Convert.ToDateTime(request.Tarjeta.FechaExpira, new CultureInfo("en-US")).Month,
                                DueYear = int.Parse(request.Tarjeta.FechaExpira.Substring(2, 2)), //int.Parse(Convert.ToDateTime(request.Tarjeta.FechaExpira, new CultureInfo("en-US")).ToString("yy")),
                                SecurityCode = int.Parse(string.IsNullOrEmpty(request.Tarjeta.Cvc2) ? "0" : request.Tarjeta.Cvc2)
                            },
                            Transaction = new Dispatcher.Transaction()
                            {
                                CurrencyIsoCode = 484,
                                InstallmentQuantity = 1, //NEED TO CHECK PETRUS CONSULTANT
                                SalePlan = 2, //NEED TO CHECK PETRUS CONSULTANT
                                Amount = int.Parse((request.Cargo.Monto * 100).ToString("#")), //REQUIRE CONVERT DECIMALS INTO STRING
                                TransactionType = 1,
                                AutoConfirmedTrx = 1, //NEED TO CHECK PETRUS CONSULTANT
                                PartialAuthorization = 1,
                                //MCC = 5072 //NEED TO CHECK
                            }
                        };
                        petrusResponse = dis.PetrusExecuteCharge(petrusRequest, mov.idMovimiento);
                    }
                    catch (Exception ex)
                    {
                        mov.NoAutorizacion = "-1";
                        mov.Autorizado = false;
                        mov.MensajeError = "Problema en WebService : " + ex;
                        _ctx.SaveChanges();
                        response.UserResponse = ex.ToString();
                        throw ex;
                    }
                    mov.Autorizado = (!petrusResponse.Errors.Any() && petrusResponse.Response.Code == "00");
                    mov.NoAutorizacion = petrusResponse.ReferenceNumber;
                    response.Saldo = petrusResponse.Ammount;
                    response.Success = mov.Autorizado == true ? 1 : 0;
                    response.NumeroAutorizacion = mov.NoAutorizacion;
                    response.IdMovimiento = mov.idMovimiento;
                    response.CodigoRespuesta = (bool)mov.Autorizado?-1:999;
                    response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
                    mov.MensajeError = mov.Autorizado == true ? "Transacción Exitosa" : "Error en la transacción: " + dis.GetMessageFromResponseCode(petrusResponse.Response.Code, typeof(Dispatcher.ErrorCodes));
                    response.UserResponse = mov.MensajeError;
                    _ctx.SaveChanges();                    
                }
            }
            catch (Exception ex)
            {
                response.UserResponse = ex.ToString();
            }
            //GuardarBitacora(request, response);
            return response;
        }


        /// <summary>
        /// Set charge for by Call center
        /// </summary>
        /// <param name="request"></param>
        /// <param name="idUsuarioCallCenter"></param>
        /// <returns></returns>
        private CargoResponse SetCargo(CargoRequest request, Int32 idUsuarioCallCenter)
        {
            BroxelEntities _ctx = new BroxelEntities();
            CargoResponse response = new CargoResponse();
            try
            {
                DateTime currentDate = DateTime.Now;
                Movimiento mov = new Movimiento
                {
                    FechaExpira = request.Tarjeta.FechaExpira,
                    Monto = request.Cargo.Monto,
                    NoReferencia = request.Cargo.NoReferencia,
                    NombreReferencia = request.Cargo.NombreReferencia,
                    NombreTarjeta = request.Tarjeta.NombreTarjeta,
                    idUsuario = request.UserID,
                    UsuarioCreacion = _ctx.Usuario.FirstOrDefault(x => x.idUsuario == idUsuarioCallCenter).Usuario1,
                    FechaHoraCreacion = currentDate,
                    UsuarioModificacion = request.Solicitante,
                    FechaHoraModificacion = currentDate,
                    ActivoLote = true,
                    Tarjeta = Helper.TruncaTarjeta(request.Tarjeta.NumeroTarjeta),
                    CVC = "**" + request.Tarjeta.Cvc2.ToString(CultureInfo.InvariantCulture).Substring(1),
                    RegControl = false,
                    Comercio = _ctx.Comercio.FirstOrDefault(c => c.idComercio == request.Cargo.IdComercio),
                    NumCuenta = request.Tarjeta.Cuenta,
                };
                _ctx.Movimiento.Add(mov);
                _ctx.SaveChanges();
                Comercio com = (from x in _ctx.Comercio where x.idComercio == request.Cargo.IdComercio select x).First();
                try
                {
                    _autorizarReq = new AutorizarRequest
                    {
                        SecuenciaTransaccion = mov.idMovimiento.ToString(CultureInfo.InvariantCulture),
                        Canal = "BroxelWeb",
                        TipoTransaccion = "1",
                        FechaTransaccion = currentDate.ToString("yyyyMMdd"),
                        HoraTransaccion = currentDate.ToString("HHmmss"),
                        Comercio = com.CodigoComercio,
                        NombreComercio = com.Comercio1,
                        Terminal = "PC",
                        ModoIngreso = "1",
                        Tarjeta = request.Tarjeta.NumeroTarjeta,
                        FechaExpiracion = request.Tarjeta.FechaExpira,
                        CodigoSeguridad = request.Tarjeta.Cvc2,
                        Importe = request.Cargo.Monto.ToString(CultureInfo.InvariantCulture),
                        CodigoMoneda = "484"
                    };
                    ServicePointManager.ServerCertificateValidationCallback +=
                        (sender, cert, chain, sslPolicyErrors) => true;

                    var reqInt = new InspectorBehavior();
                    var handler = (AutorizacionPortTypeClient)_autorizadorWs;
                    handler.Endpoint.Behaviors.Clear();
                    handler.Endpoint.Behaviors.Add(reqInt);
                    var dateTimeInicio = DateTime.Now;
                    _autorizarResp = _autorizadorWs.Autorizar(_autorizarReq);
                    var dateTimeFin = DateTime.Now;
                    var reqXml = reqInt.LastRequestXml;
                    var resXml = reqInt.LastResponseXml;

                    try
                    {
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 15, 1,
                                reqXml, resXml, mov.idMovimiento);
                        });
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
                    }

                }
                catch (Exception ex)
                {
                    mov.NoAutorizacion = "-1";
                    mov.Autorizado = false;
                    mov.MensajeError = "Problema en WebService : " + ex;
                    _ctx.SaveChanges();
                    response.UserResponse = ex.ToString();
                }
                mov.Autorizado = _autorizarResp.CodigoRespuesta.Trim() == ("-1");
                mov.NoAutorizacion = _autorizarResp.CodigoAutorizacion;
                int codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                CodigosRespuestaSQL cr = _ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
                if (cr != null)
                    response.UserResponse = cr.Descripcion;
                response.Success = mov.Autorizado == true ? 1 : 0;
                response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
                response.IdMovimiento = mov.idMovimiento;
                response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
                mov.MensajeError = cr.Descripcion;
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                response.UserResponse = ex.ToString();
            }
            //GuardarBitacora(request, response);
            return response;
        }

        private LimiteResponse SetPago(CargoRequest request)
        {
            BroxelEntities _ctx = new BroxelEntities();
            LimiteResponse response = new LimiteResponse();
            response.SaldoAntes = GetSaldosPorCuenta(request.NumCuenta).Saldos.DisponibleCompras;
            try
            {
                DateTime currentDate = DateTime.Now;
                var usuarioWs = "WsDefault";
                try
                {
                    usuarioWs = _ctx.Usuario.FirstOrDefault(x => x.idUsuario == request.UserID).Usuario1;
                }
                catch (Exception)
                {
                    usuarioWs = "WsDefault";
                }

                Movimiento mov = new Movimiento
                {
                    FechaExpira = request.Tarjeta.FechaExpira,
                    Monto = request.Cargo.Monto,
                    NoReferencia = request.Cargo.NoReferencia ?? "",
                    NombreReferencia = request.Cargo.NombreReferencia ?? "",
                    NombreTarjeta = request.Tarjeta.NombreTarjeta,
                    FechaHoraCreacion = currentDate,
                    TipoTransaccion = 1027,
                    idUsuario = request.UserID,
                    UsuarioCreacion = usuarioWs,
                    ActivoLote = true,
                    Tarjeta = Helper.TruncaTarjeta(request.Tarjeta.NumeroTarjeta),
                    CVC = "**" + request.Tarjeta.Cvc2.ToString(CultureInfo.InvariantCulture).Substring(2),
                    RegControl = false,
                    Comercio = _ctx.Comercio.FirstOrDefault(c => c.idComercio == request.Cargo.IdComercio),
                    NumCuenta = request.NumCuenta
                };
                _ctx.Movimiento.Add(mov);
                _ctx.SaveChanges();
                Comercio com = (from x in _ctx.Comercio where x.idComercio == request.Cargo.IdComercio select x).First();
                try
                {
                    _autorizarReq = new AutorizarRequest
                    {
                        SecuenciaTransaccion = mov.idMovimiento.ToString(CultureInfo.InvariantCulture),
                        Canal = "BroxelWeb",
                        TipoTransaccion = "1027",
                        FechaTransaccion = currentDate.ToString("yyyyMMdd"),
                        HoraTransaccion = currentDate.ToString("HHmmss"),
                        Comercio = com.CodigoComercio,
                        NombreComercio = com.Comercio1,
                        Terminal = "PC",
                        ModoIngreso = "1",
                        Tarjeta = request.Tarjeta.NumeroTarjeta,
                        FechaExpiracion = request.Tarjeta.FechaExpira,
                        CodigoSeguridad = request.Tarjeta.Cvc2,
                        Importe = request.Cargo.Monto.ToString(CultureInfo.InvariantCulture),
                        CodigoMoneda = "484"
                    };
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    var reqInt = new InspectorBehavior();
                    var handler = (AutorizacionPortTypeClient)_autorizadorWs;
                    handler.Endpoint.Behaviors.Clear();
                    handler.Endpoint.Behaviors.Add(reqInt);
                    var dateTimeInicio = DateTime.Now;
                    _autorizarResp = _autorizadorWs.Autorizar(_autorizarReq);
                    var dateTimeFin = DateTime.Now;
                    var reqXml = reqInt.LastRequestXml;
                    var resXml = reqInt.LastResponseXml;

                    try
                    {
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.NumCuenta, 16, 1,
                                reqXml, resXml, mov.idMovimiento);
                        });
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
                    }

                }
                catch (Exception ex)
                {
                    mov.NoAutorizacion = "-1";
                    mov.Autorizado = false;
                    mov.MensajeError = "Problema en WebService : " + ex;
                    _ctx.SaveChanges();
                    response.UserResponse = ex.ToString();
                    throw ex;
                }
                mov.Autorizado = _autorizarResp.CodigoRespuesta.Trim() == ("-1");
                mov.NoAutorizacion = _autorizarResp.CodigoAutorizacion;
                int codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                CodigosRespuestaSQL cr = _ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
                if (cr != null)
                    response.UserResponse = cr.Descripcion;
                response.Success = mov.Autorizado == true ? 1 : 0;
                response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
                response.IdMovimiento = mov.idMovimiento;
                response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
                mov.MensajeError = cr.Descripcion;
                _ctx.SaveChanges();
                response.SaldoDespues = GetSaldosPorCuenta(request.NumCuenta, "", "SetPago").Saldos.DisponibleCompras;
            }
            catch (Exception ex)
            {
                response.UserResponse = ex.ToString();
            }
            //GuardarBitacora(request, response);
            return response;
        }

		private LimiteResponse SetPagoY(CargoRequest request)
		{
			var ctx = new BroxelEntities();
			var response = new LimiteResponse();
			response.SaldoAntes = GetSaldosPorCuenta(request.NumCuenta,"","SetPago").Saldos.DisponibleCompras;
			try
			{
				DateTime currentDate = DateTime.Now;
				var usuarioWs = "WsDefault";
				try
				{
				    var usuario = ctx.Usuario.FirstOrDefault(x => x.idUsuario == request.UserID);
				    usuarioWs = usuario==null ? "WsDefault" : usuario.Usuario1;
				}
				catch (Exception)
				{
					usuarioWs = "WsDefault";
				}

				Movimiento mov = new Movimiento
				{
					FechaExpira = request.Tarjeta.FechaExpira,
					Monto = request.Cargo.Monto,
					NoReferencia = request.Cargo.NoReferencia ?? "",
					NombreReferencia = request.Cargo.NombreReferencia ?? "",
					NombreTarjeta = request.Tarjeta.NombreTarjeta,
					FechaHoraCreacion = currentDate,
					TipoTransaccion = 1027,
					idUsuario = request.UserID,
					UsuarioCreacion = usuarioWs,
					ActivoLote = true,
					Tarjeta = Helper.TruncaTarjeta(request.Tarjeta.NumeroTarjeta),
					CVC = "**" + request.Tarjeta.Cvc2.ToString(CultureInfo.InvariantCulture).Substring(2),
					RegControl = false,
					Comercio = ctx.Comercio.FirstOrDefault(c => c.idComercio == request.Cargo.IdComercio),
					NumCuenta = request.NumCuenta
				};
				ctx.Movimiento.Add(mov);
				ctx.SaveChanges();
				var com = (from x in ctx.Comercio where x.idComercio == request.Cargo.IdComercio select x).First();

				request.Tarjeta.Procesador = Helper.GetProcesadorFromTarjeta(request.Tarjeta.NumeroTarjeta);
				switch (request.Tarjeta.Procesador)
				{
				    case 1:
				    {
				        try
				        {
				            _autorizarReq = new AutorizarRequest
				            {
				                SecuenciaTransaccion = mov.idMovimiento.ToString(CultureInfo.InvariantCulture),
				                Canal = "BroxelWeb",
				                TipoTransaccion = "1027",
				                FechaTransaccion = currentDate.ToString("yyyyMMdd"),
				                HoraTransaccion = currentDate.ToString("HHmmss"),
				                Comercio = com.CodigoComercio,
				                NombreComercio = com.Comercio1,
				                Terminal = "PC",
				                ModoIngreso = "1",
				                Tarjeta = request.Tarjeta.NumeroTarjeta,
				                FechaExpiracion = request.Tarjeta.FechaExpira,
				                CodigoSeguridad = request.Tarjeta.Cvc2,
				                Importe = request.Cargo.Monto.ToString(CultureInfo.InvariantCulture),
				                CodigoMoneda = "484"
				            };
				            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

				            var reqInt = new InspectorBehavior();
				            var handler = (AutorizacionPortTypeClient)_autorizadorWs;
				            handler.Endpoint.Behaviors.Clear();
				            handler.Endpoint.Behaviors.Add(reqInt);
				            var dateTimeInicio = DateTime.Now;
				            _autorizarResp = _autorizadorWs.Autorizar(_autorizarReq);
				            var dateTimeFin = DateTime.Now;
				            var reqXml = reqInt.LastRequestXml;
				            var resXml = reqInt.LastResponseXml;

				            try
				            {
				                ThreadPool.QueueUserWorkItem(delegate
				                {
				                    new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.NumCuenta, 16, 1,
				                        reqXml, resXml, mov.idMovimiento);
				                });
				            }
				            catch (Exception e)
				            {
				                Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
				            }

				        }
				        catch (Exception ex)
				        {
				            mov.NoAutorizacion = "-1";
				            mov.Autorizado = false;
				            mov.MensajeError = "Problema en WebService : " + ex;
				            ctx.SaveChanges();
				            response.UserResponse = ex.ToString();
				            throw;
				        }
				        mov.Autorizado = _autorizarResp.CodigoRespuesta.Trim() == ("-1");
				        mov.NoAutorizacion = _autorizarResp.CodigoAutorizacion;
				        int codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
				        CodigosRespuestaSQL cr = ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
				        if (cr != null)
				        {
                            response.UserResponse = cr.Descripcion;
                            mov.MensajeError = cr.Descripcion;
				        }
				        response.Success = mov.Autorizado == true ? 1 : 0;
				        response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
				        response.IdMovimiento = mov.idMovimiento;
				        response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
				        response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
				        
				        ctx.SaveChanges();
				        response.SaldoDespues = GetSaldosPorCuenta(request.NumCuenta, "", "SetPago").Saldos.DisponibleCompras;
				    }
				    break;
				    case 2:
				        try
				        {
				            var acceptDto = new AcceptPaymentDTO
				            {
				                CardNumber = request.Tarjeta.NumeroTarjeta,
				                CurrencyCode = 484,
				                Amount = request.Cargo.Monto,
				                PaymentDate = currentDate.ToString("ddMMyyyy HH:mm:ss"),
				                PaymentMethod = 7,
				                PaymentChannel = 2,
				                ExternalPaymentCode = mov.idMovimiento.ToString(CultureInfo.InvariantCulture),
				                IsConfirmation = false
				            };
				            var reqInt = new InspectorBehavior();
				            var handler = (PaymentServiceClient)_paymentPetrus;
				            handler.Endpoint.Behaviors.Clear();
				            handler.Endpoint.Behaviors.Add(reqInt);
				            var dateTimeInicio = DateTime.Now;
				            var pr = _paymentPetrus.AcceptPayment("103", acceptDto);
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

				            mov.Autorizado = pr.ReturnExecution == PaymentService.ReturnExecution.SUCCESS;
				            mov.NoAutorizacion = pr.ReturnExecution == PaymentService.ReturnExecution.SUCCESS
				                ? pr.paymentInfo.PaymentCode : "0";
				            mov.MensajeError = pr.ReturnExecution == PaymentService.ReturnExecution.SUCCESS
				                ? "Exito"
				                : pr.Errors[0].ErrorText;
				            response.Success = mov.Autorizado == true ? 1 : 0;
				            response.NumeroAutorizacion = mov.NoAutorizacion;
				            response.CodigoRespuesta = mov.Autorizado == true ? -1 : 999;
				            response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
				            response.IdMovimiento = mov.idMovimiento;
				            response.UserResponse = mov.MensajeError;
				            ctx.SaveChanges();
				            response.SaldoDespues = GetSaldosPorCuenta(request.NumCuenta, "", "SetPago").Saldos.DisponibleCompras;
				        }
				        catch (Exception ex)
				        {
				            mov.NoAutorizacion = "-1";
				            mov.Autorizado = false;
				            mov.MensajeError = "Problema en WebService : " + ex;
				            ctx.SaveChanges();
				            response.UserResponse = ex.ToString();
				            throw;
				        }
				        break;
				}
			}
			catch (Exception ex)
			{
				response.UserResponse = ex.ToString();
			}
			return response;
		}
		public CargoResponse SetCargoConCuenta(Decimal monto, String nombreTarjeta, String numeroTarjeta, String cvc2, String fechaExpira, String numReferencia, String nombreReferencia, Int32 idUsuario, Int32 idComercio, String cuenta)//
        {
            if (monto >= 0.01M)
            {
                CargoRequest request = new CargoRequest
                {
                    Tarjeta = new Tarjeta(nombreTarjeta, numeroTarjeta, fechaExpira, cvc2, cuenta),
                    Cargo = new Cargo
                    {
                        Monto = Convert.ToDecimal(monto),
                        IdUsuario = idUsuario,
                        IdComercio = idComercio,
                        NombreReferencia = nombreReferencia,
                        NoReferencia = numReferencia
                    },
                    UserID = idUsuario
                };
                var escenario = ConfigurationManager.AppSettings["UseNewMethods"];
                CargoResponse response = escenario == "1" ? SetCargoNew(request) : SetCargo(request);
                return response;
            }
            return new CargoResponse();
        }

        #endregion


        
        private TransferenciaResponse TransferenciaDeCuentas(TransferenciaRequest request)
        {
            BroxelEntities _ctx = new BroxelEntities();
            TransferenciaResponse response = new TransferenciaResponse();
            try
            {
                var saldosAntes = GetSaldosPorCuenta(request.Tarjeta.Cuenta, "", "TransferenciaDeCuentasOrigenAntes");

                var mySql = new MySqlDataAccess();

                if (mySql.EsB2C(request.Tarjeta.Cuenta) && mySql.ValidaNivelCuarentenaDisposicion(request.Tarjeta.Cuenta))
                {
                    var limiteCreditoOriginal = Convert.ToDecimal(ConfigurationManager.AppSettings["SaldoInicialB2C"]);
                    if (saldosAntes.Saldos.LimiteCompra <= (limiteCreditoOriginal))
                    {
                        response.Success = 0;
                        response.UserResponse =
                            "No es posible transferir saldo a otra tarjeta solo con el saldo originalmente asignado";
                        return response;
                    }
                }

                response.SaldoOrigenAntes = saldosAntes.Saldos.DisponibleCompras;
                response.SaldoDestinoAntes = GetSaldosPorCuenta(request.TarjetaRecibe.Cuenta, "", "TransferenciaDeCuentasDestinoAntes").Saldos.DisponibleCompras;
                DateTime currentDate = DateTime.Now;
                Comercio com = (from x in _ctx.Comercio where x.idComercio == request.idComercio select x).First();
                if (com == null)
                    return response;
                Movimiento mov = new Movimiento()
                {
                    FechaExpira = request.Tarjeta.FechaExpira,
                    Monto = request.Monto,
                    NombreTarjeta = request.Tarjeta.NombreTarjeta,
                    FechaHoraCreacion = currentDate,
                    TipoTransaccion = 65,
                    idUsuario = request.UserID,
                    UsuarioCreacion = _ctx.Usuario.FirstOrDefault(x => x.idUsuario == request.UserID).Usuario1,
                    ActivoLote = true,
                    Tarjeta = Helper.TruncaTarjeta(request.Tarjeta.NumeroTarjeta),
                    CVC = "**" + request.Tarjeta.Cvc2.ToString(CultureInfo.InvariantCulture).Substring(2),
                    RegControl = false,
                    Comercio = com,
                    NumCuenta = request.Tarjeta.Cuenta
                };
                _ctx.Movimiento.Add(mov);
                _ctx.SaveChanges();
                if (request.Tarjeta.Procesador == 1 )
                {
                    var escenario = ConfigurationManager.AppSettings["UseNewMethodsTransfer"];

                    AutorizarResponse autResponse;
                    try
                    {
                        object autorizarReq;
                        if (escenario == "1")
                        {
                            autorizarReq = new newAutorizacion.AutorizarRequest
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
                                    ((newAutorizacion.AutorizarRequest)autorizarReq).FeeTransaccion = request.Comision.ToString(CultureInfo.InvariantCulture);
                                    break;
                                case 2:
                                    ((newAutorizacion.AutorizarRequest)autorizarReq).FeeProcesamientoTransaccion = request.Comision.ToString(CultureInfo.InvariantCulture);
                                    break;
                            }
                        }
                        else
                        {
                            autorizarReq = new AutorizarRequest
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
                                    ((AutorizarRequest)autorizarReq).FeeTransaccion = request.Comision.ToString(CultureInfo.InvariantCulture);
                                    break;
                                case 2:
                                    ((AutorizarRequest)autorizarReq).FeeProcesamientoTransaccion = request.Comision.ToString(CultureInfo.InvariantCulture);
                                    break;
                            }

                        }
                    
                        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                        var reqInt = new InspectorBehavior();
                        object handler;
                        if (escenario == "1")
                        {
                            handler = (newAutorizacion.AutorizacionPortTypeClient) _newAutorizadorWs;
                            ((newAutorizacion.AutorizacionPortTypeClient) handler).Endpoint.Behaviors.Clear();
                            ((newAutorizacion.AutorizacionPortTypeClient) handler).Endpoint.Behaviors.Add(reqInt);
                        }
                        else
                        {
                            handler = (AutorizacionPortTypeClient)_newAutorizadorWs;
                            ((AutorizacionPortTypeClient)handler).Endpoint.Behaviors.Clear();
                            ((AutorizacionPortTypeClient)handler).Endpoint.Behaviors.Add(reqInt);
                        }
                        
                        var dateTimeInicio = DateTime.Now;
                        object autorizarResp;
                        if(escenario=="1")
                            autorizarResp = _newAutorizadorWs.Autorizar((newAutorizacion.AutorizarRequest)autorizarReq);
                        else
                            autorizarResp = _autorizadorWs.Autorizar((AutorizarRequest)autorizarReq);
                        var dateTimeFin = DateTime.Now;
                        var reqXml = reqInt.LastRequestXml;
                        var resXml = reqInt.LastResponseXml;

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
                        if (escenario == "1")
                        {
                            autResponse = new AutorizarResponse
                            {
                                CodigoRespuesta = ((newAutorizacion.AutorizarResponse)autorizarResp).CodigoRespuesta,
                                CodigoAutorizacion = ((newAutorizacion.AutorizarResponse)autorizarResp).CodigoAutorizacion,
                                MensajeError = ((newAutorizacion.AutorizarResponse)autorizarResp).MensajeError
                            };
                        }
                        else
                        {
                            autResponse = new AutorizarResponse
                            {
                                CodigoRespuesta = ((AutorizarResponse)autorizarResp).CodigoRespuesta,
                                CodigoAutorizacion = ((AutorizarResponse)autorizarResp).CodigoAutorizacion,
                                MensajeError = ((AutorizarResponse)autorizarResp).MensajeError
                            };

                        }

                    }
                    catch (Exception ex)
                    {
                        mov.NoAutorizacion = "-1";
                        mov.Autorizado = false;
                        mov.MensajeError = "Problema en WebService : " + ex;
                        _ctx.SaveChanges();
                        response.UserResponse = ex.ToString();
                        throw ex;
                    }
                    mov.Autorizado = autResponse.CodigoRespuesta.Trim() == ("-1");
                    mov.NoAutorizacion = autResponse.CodigoAutorizacion;
                    int codresp = Convert.ToInt32(autResponse.CodigoRespuesta);
                    CodigosRespuestaSQL cr = _ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
                    if (cr != null)
                        response.UserResponse = cr.Descripcion;
                    if (response.UserResponse.Trim().ToUpper() != autResponse.MensajeError.Trim().ToUpper() && autResponse.CodigoRespuesta.Trim() != ("-1"))
                        response.UserResponse = autResponse.MensajeError.Trim().ToUpper();
                    response.Success = mov.Autorizado == true ? 1 : 0;
                    response.NumeroAutorizacion = autResponse.CodigoAutorizacion;
                    response.IdMovimiento = mov.idMovimiento;
                    response.CodigoRespuesta = Convert.ToInt32(autResponse.CodigoRespuesta);
                    response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
                    mov.MensajeError = cr.Descripcion;
                    response.SaldoOrigenDespues = GetSaldosPorCuenta(request.Tarjeta.Cuenta, "", "TransferenciaDeCuentasOrigenDespues").Saldos.DisponibleCompras;
                    response.SaldoDestinoDespues = GetSaldosPorCuenta(request.TarjetaRecibe.Cuenta, "", "TransferenciaDeCuentasDestinoDespues").Saldos.DisponibleCompras;
                    TransferenciasEntreCuentas transf = new TransferenciasEntreCuentas()
                    {
                        IdMovimiento = mov.idMovimiento,
                        IdComercio = request.idComercio,
                        IdUsuario = request.UserID,
                        NumCuentaOrigen = request.NumCuenta,
                        NumCuentaDestino = request.TarjetaRecibe.Cuenta,
                        SaldoOrigenAntes = response.SaldoOrigenAntes,
                        SaldoOrigenDespues = response.SaldoOrigenDespues,
                        SaldoDestinoAntes = response.SaldoDestinoAntes,
                        SaldoDestinoDespues = response.SaldoDestinoDespues,
                        Comision = request.Comision,
                    };
                    _ctx.TransferenciasEntreCuentas.Add(transf);
                    _ctx.SaveChanges();
                }
                else if(request.Tarjeta.Procesador == 2)//Petrus
                {
                    try
                    {
                        IMovementsService wsMovementsService = new wsMovementsService.MovementsServiceClient("BasicHttpBinding_IMovementsService", "http://" + ConfigurationManager.AppSettings["PetrusHost"] + ":" + ConfigurationManager.AppSettings["PetrusPort"] + "/IssuerCommercial.ClientWS.ServiceHost/Services/MovementsService.svc");
                        var reqInt = new InspectorBehavior();
                        var handler = (MovementsServiceClient)wsMovementsService;
                        handler.Endpoint.Behaviors.Clear();
                        handler.Endpoint.Behaviors.Add(reqInt);
                        var dateTimeInicio = DateTime.Now;
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

                        try
                        {
                            ThreadPool.QueueUserWorkItem(delegate
                            {
                                new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 17, 5,
                                    reqXml, resXml, mov.idMovimiento);
                            });
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
                        }


                        mov.Autorizado = transferencia.ReturnExecution.ToString() == "SUCCESS";
                        mov.NoAutorizacion = transferencia.ReturnExecution.ToString() == "SUCCESS" ? "SUCCESS" : "ERROR";
                        response.UserResponse = "Transferencia PetrusAPetrus:" + transferencia.ReturnExecution.ToString();
                        response.Success = mov.Autorizado == true ? 1 : 0;
                        response.NumeroAutorizacion = transferencia.ReturnExecution.ToString() == "SUCCESS" ? "SUCCESS" : "ERROR";
                        response.IdMovimiento = mov.idMovimiento;
                        response.CodigoRespuesta = transferencia.ReturnExecution.ToString() == "SUCCESS" ? -1 :999;
                        response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
                        mov.MensajeError = "Transferencia PetrusAPetrus:" + (transferencia.ReturnExecution.ToString() == "SUCCESS" ? transferencia.ReturnExecution.ToString() : transferencia.Errors[0].ErrorText);
                        response.SaldoOrigenDespues = GetSaldosPorCuenta(request.Tarjeta.Cuenta, "", "TransferenciaDeCuentasOrigenDespues").Saldos.DisponibleCompras;
                        response.SaldoDestinoDespues = GetSaldosPorCuenta(request.TarjetaRecibe.Cuenta, "", "TransferenciaDeCuentasDestinoDespues").Saldos.DisponibleCompras;
                        TransferenciasEntreCuentas transf = new TransferenciasEntreCuentas()
                        {
                            IdMovimiento = mov.idMovimiento,
                            IdComercio = request.idComercio,
                            IdUsuario = request.UserID,
                            NumCuentaOrigen = request.NumCuenta,
                            NumCuentaDestino = request.TarjetaRecibe.Cuenta,
                            SaldoOrigenAntes = response.SaldoOrigenAntes,
                            SaldoOrigenDespues = response.SaldoOrigenDespues,
                            SaldoDestinoAntes = response.SaldoDestinoAntes,
                            SaldoDestinoDespues = response.SaldoDestinoDespues,
                            Comision = request.Comision,
                        };
                        _ctx.TransferenciasEntreCuentas.Add(transf);
                        _ctx.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        mov.NoAutorizacion = "-1";
                        mov.Autorizado = false;
                        mov.MensajeError = "Problema en WebService : " + ex;
                        _ctx.SaveChanges();
                        response.UserResponse = ex.ToString();
                        throw ex;
                    }
                }
            }
            /* MLS Cambio en congeladora, no estaba en la version productiva
            catch (DbEntityValidationException ex)
            {
                StringBuilder exception = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    exception.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        exception.AppendFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                Helper.SendMail("dispersiones@broxel.com", "aldo.garcia@broxel.com, mauricio.lopez@broxel.com, jesus.valdiviezo@broxel.com", "Error TransferenciaDeCuentas ", "Error " + exception.ToString(),
                      "yMQ3E3ert6", "Errores ");
            }*/
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
            catch (Exception ex)
            {
                response.UserResponse = ex.ToString();
            }
            //GuardarBitacora(request, response);
            return response;
        }
		private TransferenciaResponse TransferenciaDeCuentasFac(TransferenciaRequest request)
		{
			BroxelEntities _ctx = new BroxelEntities();
			TransferenciaResponse response = new TransferenciaResponse();

			if (request.Tarjeta.Cuenta.Equals(request.TarjetaRecibe.Cuenta))
			{
				response.Success = 0;
				response.UserResponse = "Cuenta destino invalida, debe ser diferente a la cuenta origen";
				response.CodigoRespuesta = 999;
			}
			else
			{
				try
				{
					var saldosAntes = GetSaldosPorCuenta(request.Tarjeta.Cuenta, "", "TransferenciaDeCuentasOrigenAntes");

					var mySql = new MySqlDataAccess();

					if (mySql.EsB2C(request.Tarjeta.Cuenta) && mySql.ValidaNivelCuarentenaDisposicion(request.Tarjeta.Cuenta))
					{
						var limiteCreditoOriginal = Convert.ToDecimal(ConfigurationManager.AppSettings["SaldoInicialB2C"]);
						if (saldosAntes.Saldos.LimiteCompra <= (limiteCreditoOriginal))
						{
							response.Success = 0;
							response.UserResponse =
								"No es posible transferir saldo a otra tarjeta solo con el saldo originalmente asignado";
							return response;
						}
					}

					response.SaldoOrigenAntes = saldosAntes.Saldos.DisponibleCompras;
					response.SaldoDestinoAntes = GetSaldosPorCuenta(request.TarjetaRecibe.Cuenta, "", "TransferenciaDeCuentasDestinoAntes").Saldos.DisponibleCompras;
					DateTime currentDate = DateTime.Now;
					Comercio com = (from x in _ctx.Comercio where x.idComercio == request.idComercio select x).First();
					if (com == null)
						return response;
					Movimiento mov = new Movimiento()
					{
						FechaExpira = request.Tarjeta.FechaExpira,
						Monto = request.Monto,
						NombreTarjeta = request.Tarjeta.NombreTarjeta,
						FechaHoraCreacion = currentDate,
						TipoTransaccion = 65,
						idUsuario = request.UserID,
						UsuarioCreacion = _ctx.Usuario.FirstOrDefault(x => x.idUsuario == request.UserID).Usuario1,
						ActivoLote = true,
						Tarjeta = Helper.TruncaTarjeta(request.Tarjeta.NumeroTarjeta),
						CVC = "**" + request.Tarjeta.Cvc2.ToString(CultureInfo.InvariantCulture).Substring(2),
						RegControl = false,
						Comercio = com,
						NumCuenta = request.Tarjeta.Cuenta
					};
					_ctx.Movimiento.Add(mov);
					_ctx.SaveChanges();

					if (request.Tarjeta.Procesador == 1 && request.TarjetaRecibe.Procesador == 1)//Credencial-Credencial
					{
						try
						{
							var c2c = new TransferenciaBL().C2CCredencialCredencial(request, mov, com, currentDate);
							mov.Autorizado = c2c.CodigoRespuesta == -1; 
							mov.NoAutorizacion = c2c.NumeroAutorizacion;
							mov.MensajeError = "Transferencia CAC"+c2c.UserResponse;
							response.SaldoOrigenDespues = c2c.SaldoOrigenDespues;
							response.SaldoDestinoDespues = c2c.SaldoDestinoDespues;
						}
						catch (Exception ex)
						{
							mov.NoAutorizacion = "-1";
							mov.Autorizado = false;
							mov.MensajeError = "Problema en WebService : " + ex;
							_ctx.SaveChanges();
							response.UserResponse = ex.ToString();
							throw ex;
						}

					}
					else if (request.Tarjeta.Procesador == 2 && request.TarjetaRecibe.Procesador == 2)//Petrus-Petrus
					{
						try
						{
							var c2c = new TransferenciaBL().C2CPetrusPetrus(request, mov);
							mov.Autorizado = c2c.Success == 1 ? true : false;
							mov.NoAutorizacion = c2c.UserResponse.ToString() == "SUCCESS" ? "SUCCESS" : "ERROR";
							mov.MensajeError = "Transferencia PAP:" + (c2c.Success == 1 ? "SUCCESS" : c2c.UserResponse);
							response.SaldoOrigenDespues = c2c.SaldoOrigenDespues;
							response.SaldoDestinoDespues = c2c.SaldoDestinoDespues;
						}
						catch (Exception ex)
						{
							mov.NoAutorizacion = "-1";
							mov.Autorizado = false;
							mov.MensajeError = "Problema en WebService : " + ex;
							_ctx.SaveChanges();
							response.UserResponse = ex.ToString();
							throw ex;
						}
					}
					else //C2C Credencial-Petrus Petrus-Credencial
					{
						var c2c = new TransferenciaBL().C2CPetrusCredencial(request, mov);
						mov.Autorizado = c2c.Success == 1 ? true : false;
						mov.NoAutorizacion = c2c.UserResponse.ToString() == "SUCCESS" ? "SUCCESS" : "ERROR";
						mov.MensajeError = "Transferencia InterProcesadores:" + (c2c.Success == 1 ? "SUCCESS" : c2c.UserResponse);
						response.SaldoOrigenDespues = c2c.SaldoOrigenDespues;
						response.SaldoDestinoDespues = c2c.SaldoDestinoDespues;
					}

					TransferenciasEntreCuentas transf = new TransferenciasEntreCuentas()
					{
						IdMovimiento = mov.idMovimiento,
						IdComercio = request.idComercio,
						IdUsuario = request.UserID,
						NumCuentaOrigen = request.NumCuenta,
						NumCuentaDestino = request.TarjetaRecibe.Cuenta,
						SaldoOrigenAntes = response.SaldoOrigenAntes,
						SaldoOrigenDespues = response.SaldoOrigenDespues,
						SaldoDestinoAntes = response.SaldoDestinoAntes,
						SaldoDestinoDespues = response.SaldoDestinoDespues,
						Comision = request.Comision,
					};
					_ctx.TransferenciasEntreCuentas.Add(transf);
					_ctx.SaveChanges();

					if (response.Success == 1)
					{
						if (response.SaldoOrigenAntes == response.SaldoOrigenDespues || response.SaldoDestinoAntes == response.SaldoDestinoDespues)
						{
							Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com, jesus.valdiviezo@broxel.com", "Error TransferenciaDeCuentas  ", "En el método TransferenciaDeCuentas surgió un error, fue exitoso sin embargo, los saldos no indican ningun movimiento:" + response.UserResponse + "\nSaldoOrigenAntes: " + response.SaldoOrigenAntes + "\nSaldoOrigenDespues: " + response.SaldoOrigenDespues + "\nSaldoDestinoAntes: " + response.SaldoDestinoAntes + "\nSaldoDestinoDespues: " + response.SaldoDestinoDespues + "  " + response.UserResponse,
											"yMQ3E3ert6", "Errores ");
							Trace.WriteLine("En el método TransferenciaDeCuentas surgió un error, fue exitoso sin embargo, los saldos no indican ningun movimiento:" + response.UserResponse+"\nSaldoOrigenAntes: "+response.SaldoOrigenAntes+"\nSaldoOrigenDespues: "+response.SaldoOrigenDespues+"\nSaldoDestinoAntes: "+response.SaldoDestinoAntes+"\nSaldoDestinoDespues: "+response.SaldoDestinoDespues+"  "+response.UserResponse);
						}
					}
				}
				/* MLS Cambio en congeladora, no estaba en la version productiva
				catch (DbEntityValidationException ex)
				{
					StringBuilder exception = new StringBuilder();
					foreach (var eve in ex.EntityValidationErrors)
					{
						exception.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
						foreach (var ve in eve.ValidationErrors)
						{
							exception.AppendFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
						}
					}
					Helper.SendMail("dispersiones@broxel.com", "aldo.garcia@broxel.com, mauricio.lopez@broxel.com, jesus.valdiviezo@broxel.com", "Error TransferenciaDeCuentas ", "Error " + exception.ToString(),
						  "yMQ3E3ert6", "Errores ");
				}*/
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
				catch (Exception ex)
				{
					response.UserResponse = ex.ToString();
				}
			}
			//GuardarBitacora(request, response);
			return response;
		}
		public String GetNombreComercio(String ClaveComercio)
        {
            broxelco_rdgEntities _brmy = new broxelco_rdgEntities();
            var c = _brmy.Comercio.FirstOrDefault(x => x.Comercio1 == ClaveComercio);
            if (c != null)
                return c.razon_social;
            return ClaveComercio;
        }

        public String GetNombreComercioPorCodigo(String Codigo)
        {
            var c = Entities.Instance.Comercios.FirstOrDefault(x => x.CodigoComercio == Codigo);
            if (c != null)
                return c.razon_social;
            return Codigo;
        }

        //public string ObtenerDescripcionMovimiento(string tipo, string subtipo, string codigoComercio, string autorizacion,
        //    string codigoDescripcion, string comercioDescripcion, string montosOriginalMoneda, string montosOriginalImporte,
        //    string MontosOriginalMonedaDescripcion, string momento, List<TransferenciasDeTerceros> listDeTerceros,
        //    List<TransferenciasATerceros> listATerceros)  

        public string ObtenerDescripcionMovimiento(Operacion operacion, List<TransferenciasDeTerceros> listDeTerceros, List<TransferenciasATerceros> listATerceros)
        {
            string Descripcion = (operacion.Codigo.Tipo == "35" || (operacion.Codigo.Tipo == "33" && operacion.Codigo.SubTipo != "2")) ? ("SU PAGO - GRACIAS")
                     : (operacion.Codigo.Tipo == "67") ? "C2C DE " + ObtenerTitularTransferenciaRemitente(listDeTerceros, operacion.Autorizacion, true, Convert.ToDateTime(operacion.Momento))
                     : (operacion.Codigo.Tipo == "65") ? "C2C A " + ObtenerTitutarTransferenciaDestinatario(listATerceros, operacion.Autorizacion, true, Convert.ToDateTime(operacion.Momento))
                     : (operacion.Codigo.Tipo == "257" ? "CAMBIO DE NIP"
                     : (operacion.Comercio.Codigo == "999999999001845" ? operacion.Codigo.Descripcion
                     : ((operacion.Codigo.Tipo == "1027" && operacion.Codigo.SubTipo == "0") ? "SU PAGO - GRACIAS"
                     : (operacion.Comercio.Codigo.StartsWith("999999") ? GetNombreComercioPorCodigo(operacion.Comercio.Codigo)
                     : operacion.Comercio.Descripcion)))) + (operacion.Montos.Original.Moneda.Codigo == mxCurrency ? "" : " " +
                     operacion.Montos.Original.Importe + " " + operacion.Montos.Original.Moneda.Descripcion);

            return Descripcion;
        }

        public String GetMonedaAbr(String Codigo)
        {
            var element = Entities.Instance.Monedas.FirstOrDefault(m => m.NumMoneda == Codigo);
            return element != null ? element.CodMoneda : "XXX";
        }

        public bool GetIncrementoDeOper(Operacion operacion)
        {
            var tipo = 0;
            try
            {
                tipo = string.IsNullOrEmpty(operacion.Codigo.Tipo) ? 0 : Convert.ToInt32(operacion.Codigo.Tipo);
            }
            catch (Exception)
            {
                tipo = 0;
            }

            var subTipo = 0;
            try
            {
                subTipo = string.IsNullOrEmpty(operacion.Codigo.SubTipo) ? 0 : Convert.ToInt32(operacion.Codigo.SubTipo);
            }
            catch (Exception)
            {
                subTipo = 0;
            }

            var res =
                Entities.Instance.CodsTranWeb.FirstOrDefault(
                    c => c.TipoTransaccion == tipo && c.SubTipoTransaccion == subTipo);

            if (res != null)
            {
                return res.Incremento;
            }


            return Convert.ToDecimal(operacion.Montos.Original.Importe) < 0;
        }

        public string ObtenerTitularTransferenciaRemitente(List<TransferenciasDeTerceros> listTransfer, string codigoAut, bool bit, DateTime fechaHora)
        {
            //Trace.WriteLine(DateTime.Now.ToString("O") + " ObtenerTitularTransferenciaRemitente, valores: codigoAut= " + codigoAut + ", bit = " + bit + ", fechaHora = " + fechaHora.ToString("O"));
            try
            {
                var response = (from list in listTransfer
                                where list.CodigoAutorizacion == codigoAut && (list.FechaCreacion >= fechaHora.AddMinutes(-3) && list.FechaCreacion <= fechaHora.AddMinutes(3))
                                select new
                                {
                                    remitente = list.Remitente,
                                    destinatario = list.Destinatario
                                }).FirstOrDefault();

                if (response == null)
                    return "TRANSFERENCIA C2C";
                return bit ? response.remitente : response.destinatario;
            }

            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + " Error en ObtenerTitularTransferenciaRemitente: " + e);
                return "TRANSFERENCIA C2C";
            }
        }

        public string ObtenerConceptoRemitente(List<TransferenciasDeTerceros> listTransfer, string codigoAuto, DateTime fechaHora)
        {
            //Trace.WriteLine(DateTime.Now.ToString("O") + " ObtenerConceptoRemitente, valores: codigoAut= " + codigoAuto + ", fechaHora = " + fechaHora.ToString("O"));
            try
            {
                var response = (from list in listTransfer
                                where list.CodigoAutorizacion == codigoAuto && (list.FechaCreacion >= fechaHora.AddMinutes(-3) && list.FechaCreacion <= fechaHora.AddMinutes(3))
                                select new
                                {
                                    concepto = list.ConceptoTransferencia
                                }).FirstOrDefault();

                if (response == null)
                    return "TRANSFERENCIA C2C";

                return response.concepto;
            }

            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + " Error en ObtenerConceptoRemitente: " + e);
                return "TRANSFERENCIA C2C";
            }
        }

        public string ObtenerConceptoDestinatario(List<TransferenciasATerceros> listTransfer, string codigoAuto, DateTime fechaHora)
        {
            //Trace.WriteLine(DateTime.Now.ToString("O") + " ObtenerConceptoRemitente, valores: codigoAut= " + codigoAuto + ", fechaHora = " + fechaHora.ToString("O"));
            try
            {
                var response = (from list in listTransfer
                                where list.CodigoAutorizacion == codigoAuto && (list.FechaCreacion >= fechaHora.AddMinutes(-3) && list.FechaCreacion <= fechaHora.AddMinutes(3))
                                select new
                                {
                                    concepto = list.ConceptoTransferencia
                                }).FirstOrDefault();

                if (response == null)
                    return "TRANSFERENCIA C2C";

                return response.concepto;
            }

            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + " Error en ObtenerConceptoRemitente: " + e);
                return "TRANSFERENCIA C2C";
            }
        }

        public decimal ObtenerComisionDisposicion(List<CargosDisposiciones> cargos, string numAut)
        {
            var res = (decimal)0.0;
            try
            {
                var cargosAut = (from c1 in cargos where c1.NumAutorizacion == numAut select c1).FirstOrDefault();
                if (cargosAut == null)
                    return res;
                var cargosIdDisp =
                    (from c2 in cargos where c2.IdDisposicion == cargosAut.IdDisposicion select c2).ToList();
                if (cargosIdDisp.Count == 0)
                    return res;
                var cargoCom =
                    (from c3 in cargos
                     where c3.IdDisposicion == cargosAut.IdDisposicion && c3.NumAutorizacion != numAut
                     select c3).FirstOrDefault();
                return cargoCom == null ? res : cargoCom.Monto;
            }
            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + " Error en ObtenerComisionDisposicion: " + e);
                res = (decimal)0.0;
            }
            return res;
        }



        public decimal ObtenerComisionRemitente(List<TransferenciasDeTerceros> listComision, string codAut)
        {
            try
            {
                var response = (from list in listComision
                                where list.CodigoAutorizacion == codAut
                                select new
                                {
                                    comision = list.Comision
                                }).FirstOrDefault();

                if (response == null)
                    return 0;
                return response.comision;
            }

            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + " Error en ObtenerComisionRemitente: " + e);
                return 0;
            }
        }

        public decimal ObtenerComisionDestinatario(List<TransferenciasATerceros> listComision, string codAut)
        {
            try
            {
                var response = (from list in listComision
                                where list.CodigoAutorizacion == codAut
                                select new
                                {
                                    comision = list.Comision
                                }).FirstOrDefault();

                if (response == null)
                    return 0;
                return response.comision;
            }

            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + " Error en ObtenerComisionDestinatario: " + e);
                return 0;
            }
        }




        public string ObtenerTitutarTransferenciaDestinatario(List<TransferenciasATerceros> listTransfer, string codigoAut, bool bit, DateTime fechaHora)
        {
            try
            {
                var response = (from list in listTransfer
                                where list.CodigoAutorizacion == codigoAut && (list.FechaCreacion >= fechaHora && list.FechaCreacion <= fechaHora.AddMinutes(3))
                                select new
                                {
                                    remitente = list.Remitente,
                                    destinatario = list.Destinatario
                                }).FirstOrDefault();

                if (response == null)
                    return "TRANSFERENCIA C2C";
                return bit ? response.destinatario.ToUpper() : response.remitente.ToUpper();
            }

            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + " Error en ObtenerTitutarTransferenciaDestinatario: " + e);
                return "TRANSFERENCIA C2C";
            }
        }

        public string ObtenerDestinatarioDisposicion(List<TransferenciasDisposicion> listTransfer, string codigoAut, DateTime fechaHora)
        {
            try
            {
                var response = (from list in listTransfer
                                where list.NumAutorizacion == codigoAut // && (list.fechaHoraCreacion >= fechaHora && list.fechaHoraCreacion <= fechaHora.AddMinutes(3))
                                select new
                                {
                                    destinatario = list.Destinatario
                                }).FirstOrDefault();

                if (response == null)
                    return null;

                return response.destinatario.ToUpper();

            }

            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + " Error en ObtenerDestinatarioDisposicion: " + e);
                return null;
            }
        }

        public string ObtenerRemitenteAbono(List<TransferenciasAbonosACuenta> listAbonos, string codigoAut)
        {
            try
            {
                var response = (from list in listAbonos
                                where list.codigoAutorizacion == codigoAut
                                select new
                                {
                                    remitente = list.Remitente
                                }).FirstOrDefault();

                if (response == null)
                    return "TRANSFERENCIA STP";

                return response.remitente.ToUpper();
            }

            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + " Error en ObtenerRemitenteAbono: " + e);
                return "TRANSFERENCIA STP";
            }
        }



        public LimiteResponse ReversoLimiteForzado(String nombreTarjeta, String NumeroTarjeta, String FechaExpira, String CVC2, Int32 idMovimiento, Int32 idUsuario)
        {
            BroxelEntities _ctx = new BroxelEntities();
            Movimiento mov = _ctx.Movimiento.FirstOrDefault(x => x.idMovimiento == idMovimiento);
            var req = GetSaldosPorCuenta(mov.NumCuenta, "2", "ReversoLimiteForzadoAntes");
            LimiteResponse resp = new LimiteResponse();
            LimiteRequest request = new LimiteRequest
            {
                Tarjeta = new Tarjeta(nombreTarjeta, NumeroTarjeta, FechaExpira, CVC2),
                Tipo = Convert.ToInt32(mov.SubTipoTransaccion),
                Limite = Convert.ToDecimal(mov.Monto),
                UserID = idUsuario
            };
            var escenario = ConfigurationManager.AppSettings["UseNewDispersion"];
            resp = escenario == "1" ? ReversoLimiteNew(request, mov) : ReversoLimite(request, mov);

            if (mov.SubTipoTransaccion == 0)
                resp.SaldoAntes = req.Saldos.DisponibleCompras;
            else if (request.Tipo == 3)
                resp.SaldoAntes = req.Saldos.DisponibleAdelantos;
            req = GetSaldosPorCuenta(mov.NumCuenta, "2", "ReversoLimiteForzadoDespues");
            if (mov.SubTipoTransaccion == 0)
                resp.SaldoDespues = req.Saldos.DisponibleCompras;
            else if (request.Tipo == 3)
                resp.SaldoDespues = req.Saldos.DisponibleAdelantos;
            return resp;
        }

        #endregion

        #region ExposedWebMethods

        [WebMethod]
        public void RenominaComercio(int idComercio)
        {
            new VCardBL().RenominaClienteComercio(idComercio);
        }

        [WebMethod]
        public bool IngresaEnCuarentenaNiveles(string numCuenta, string usuario)
        {
            try
            {
                var saldos = GetSaldosPorCuenta(numCuenta, usuario, "IngresaEnCuarentenaNiveles");
                if (saldos == null)
                    return false;
                if (saldos.Success != 1)
                    return false;
                var genericBL = new GenericBL();
                genericBL.ValidaYCuarentena(numCuenta, saldos.Saldos.LimiteCompra, usuario);
                return genericBL.ValidaCuentaEnCuarentena(numCuenta);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al Ingresar en cuarentena la cuenta " + numCuenta + ": " + e);
                return false;
            }
        }

        [WebMethod]
        public bool SacaCuentaDeCuarentena(string numCuenta, string usuario)
        {
            try
            {
                if (new GenericBL().SacaCuentaCuarentena(numCuenta, usuario) > 0)
                    return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al sacar cuenta de cuarentena" + e);
                return false;
            }
            return false;
        }

        [WebMethod]
        public ComercioResponse AgregarComercio(Int32 idComercio, String codigoComercio, String nombreComercio)
        {
            return
                Agregar(new App_Code.Comercio
                {
                    idComercio = idComercio,
                    CodigoComercio = codigoComercio,
                    NombreComercio = nombreComercio
                });
        }

        [WebMethod]
        public LimiteResponse ActualizarLimiteATM(String nombreTarjeta, String numeroTarjeta, String fechaExpira, String cvc2, Decimal incrementoLimite, Int32 idUsuario, String cuenta, int procesador = 1)
        {
            // Petrus no aumenta limite ATM
            if(procesador!=1)
                return new LimiteResponse
            {
                CodigoRespuesta = -1,
                Success = 1,
                SaldoAntes = 0,
                SaldoDespues = 0,
                UserResponse = "Petrus no afecta ATM",
                NumeroAutorizacion = "000001",
            };

            var req = GetSaldosPorCuenta(cuenta, "2", "ActualizarLimiteATMAntes");
            if (req.Success == 1)
            {
                Decimal saldoAntes = req.Saldos.DisponibleAdelantos;
                LimiteRequest request = new LimiteRequest
                {
                    Tarjeta = new Tarjeta(nombreTarjeta, numeroTarjeta, fechaExpira, cvc2, cuenta),
                    Limite = incrementoLimite,
                    Tipo = 3,
                    UserID = idUsuario
                };
                var escenario = ConfigurationManager.AppSettings["UseNewDispersion"];
                LimiteResponse lr = escenario == "1" ? ActualizarLimiteNew(request) : ActualizarLimite(request);
                lr.SaldoAntes = saldoAntes;
                lr.SaldoDespues =
                    GetSaldosPorCuenta(cuenta, "2", "ActualizarLimiteATMDespues").Saldos.DisponibleAdelantos;
                return lr;
            }
            return new LimiteResponse
            {
                CodigoRespuesta = 122,
                Success = 0,
                SaldoAntes = 0,
                SaldoDespues = 0,
                UserResponse = "NO SE PUDO REVISAR EL SALDO ANTES DE EFECTUAR LA OPERACION",
            };
        }

        [WebMethod]
        public LimiteResponse NuevoLimiteATM(String nombreTarjeta, String numeroTarjeta, String fechaExpira, String cvc2, Decimal incrementoLimite, Int32 idUsuario, String cuenta, int procesador = 1)
        {
            // Petrus no aumenta limite ATM
            if (procesador != 1)
                return new LimiteResponse
                {
                    CodigoRespuesta = -1,
                    Success = 1,
                    SaldoAntes = 0,
                    SaldoDespues = 0,
                    UserResponse = "Petrus no afecta ATM",
                };

            var req = GetSaldosPorCuenta(cuenta, idUsuario.ToString(), "NuevoLimiteATMAntes");
            if (req.Success == 1)
            {
                if ((incrementoLimite - req.Saldos.DisponibleAdelantos) > 0)
                {
                    LimiteRequest request = new LimiteRequest
                    {
                        Tarjeta = new Tarjeta(nombreTarjeta, numeroTarjeta, fechaExpira, cvc2, cuenta),
                        Limite = incrementoLimite - req.Saldos.DisponibleAdelantos,
                        Tipo = 3,
                        UserID = idUsuario
                    };
                    var escenario = ConfigurationManager.AppSettings["UseNewDispersion"];
                    LimiteResponse lr = escenario == "1" ? ActualizarLimiteNew(request) : ActualizarLimite(request);
                    lr.SaldoAntes = req.Saldos.DisponibleAdelantos;
                    lr.SaldoDespues = GetSaldosPorCuenta(cuenta, idUsuario.ToString(), "NuevoLimiteATMDespues").Saldos.DisponibleAdelantos;
                    return lr;
                }
                return new LimiteResponse
                {
                    CodigoRespuesta = -1,
                    Success = 1,
                    SaldoAntes = req.Saldos.DisponibleAdelantos,
                    SaldoDespues = req.Saldos.DisponibleAdelantos,
                    UserResponse = "NO SE PUEDE DECREMENTAR EL LIMITE DE ATM",
                };
            }
            return new LimiteResponse
            {
                CodigoRespuesta = 122,
                Success = 0,
                SaldoAntes = 0,
                SaldoDespues = 0,
                UserResponse = "NO SE PUDO REVISAR EL SALDO ANTES DE EFECTUAR LA OPERACION",
            };
        }

        [WebMethod]
        public LimiteResponse ActualizarLimiteCredito(String nombreTarjeta, String numeroTarjeta, String fechaExpira, String cvc2, Decimal incrementoLimite, Int32 idUsuario, String cuenta, int procesador = 1)
        {
            var req = GetSaldosPorCuenta(cuenta, "2", "ActualizarLimiteCreditoAntes");
            if (req.Success == 1)
            {

                Decimal saldoAntes = req.Saldos.DisponibleCompras;

                LimiteRequest request = new LimiteRequest
                {
                    Tarjeta = new Tarjeta(nombreTarjeta, numeroTarjeta, fechaExpira, cvc2, cuenta, procesador),
                    Limite = incrementoLimite,
                    Tipo = 0,
                    UserID = idUsuario
                };
                var escenario = ConfigurationManager.AppSettings["UseNewDispersion"];
                LimiteResponse lr = escenario == "1" ? ActualizarLimiteNew(request) : ActualizarLimite(request);
                lr.SaldoAntes = saldoAntes;
                /* MLS Cambio para niveles de cuenta*/
                var saldosDespues = GetSaldosPorCuenta(cuenta, "2", "ActualizarLimiteCreditoDespues");
                try
                {
                    ThreadPool.QueueUserWorkItem(delegate
                    { new GenericBL().ValidaYCuarentena(cuenta, saldosDespues.Saldos.LimiteCompra, "WebServiceDispersion"); });
                }
                catch (Exception e)
                {
                    Trace.WriteLine("Error al validar niveles de usuario: " + e);
                }
                lr.SaldoDespues = saldosDespues.Saldos.DisponibleCompras;
                return lr;
            }
            return new LimiteResponse
            {
                CodigoRespuesta = 122,
                Success = 0,
                SaldoAntes = 0,
                SaldoDespues = 0,
                UserResponse = "NO SE PUDO REVISAR EL SALDO ANTES DE EFECTUAR LA OPERACION",
            };
        }

        [WebMethod]
        public LimiteResponse NuevoLimiteCredito(String nombreTarjeta, String numeroTarjeta, String fechaExpira, String cvc2, Decimal nuevoLimite, Int32 idUsuario, String cuenta = "")
        {
            var req = GetSaldosPorCuenta(cuenta, "2", "NuevoLimiteCreditoAntes");
            if (req.Success == 1)
            {
                if ((nuevoLimite - req.Saldos.DisponibleCompras) > 0)
                {
                    LimiteRequest request = new LimiteRequest
                    {
                        Tarjeta = new Tarjeta(nombreTarjeta, numeroTarjeta, fechaExpira, cvc2, cuenta),
                        Limite = nuevoLimite - req.Saldos.DisponibleCompras,
                        Tipo = 0,
                        UserID = idUsuario
                    };
                    var escenario = ConfigurationManager.AppSettings["UseNewDispersion"];
                    LimiteResponse lr = escenario == "1" ? ActualizarLimiteNew(request) : ActualizarLimite(request);
                    lr.SaldoAntes = req.Saldos.DisponibleCompras;
                    lr.SaldoDespues = GetSaldosPorCuenta(cuenta, "2", "NuevoLimiteCreditoDespues").Saldos.DisponibleCompras;
                    return lr;
                }
                return new LimiteResponse
                {
                    CodigoRespuesta = -1,
                    Success = 1,
                    SaldoAntes = req.Saldos.DisponibleCompras,
                    SaldoDespues = req.Saldos.DisponibleCompras,
                    UserResponse = "NO SE PUEDE DECREMENTAR EL LIMITE DE COMPRA",
                };
            }

            return new LimiteResponse
            {
                CodigoRespuesta = 122,
                Success = 0,
                SaldoAntes = 0,
                SaldoDespues = 0,
                UserResponse = "NO SE PUDO REVISAR EL SALDO ANTES DE EFECTUAR LA OPERACION",
            };

        }

        [WebMethod]
        public NIPResponse CambiarNip(String nombreTarjeta, String numeroTarjeta, String fechaExpira, String cvc2, String nipNuevo, Int32 idUsuario, String host)
        {
            NIPRequest request = new NIPRequest
            {
                Tarjeta = new Tarjeta(nombreTarjeta, numeroTarjeta, fechaExpira, cvc2),
                NIPNuevo = nipNuevo,
                UserID = idUsuario
            };
            var escenario = ConfigurationManager.AppSettings["UseNewMethods"];

            return escenario=="1"?CambiarNipNew(request,host):CambiarNip(request, host);
        }

        /// <summary>
        /// Obtiene Saldo por cuenta
        /// </summary>
        /// <param name="cuenta">Numero de cuenta</param>
        /// <param name="tipoSaldo">Tipo de saldo</param>
        /// <param name="idUsuario">Id de usuario</param>
        /// <returns>Saldo</returns>
        [WebMethod]
        public Decimal GetSaldoPorCuenta(String cuenta, Int32 tipoSaldo, Int32 idUsuario, String callerId = "")
        {
            string ip = "Unknown";

            try
            {
                if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                {
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

            }
            catch (Exception)
            {
                ip = "InternalReq";
            }

            var request = new OperarCuentaRequest { NumCuenta = cuenta, Solicitante = idUsuario.ToString(), Accion = "GetSaldoPorCuenta|Caller: " + callerId + "|ip: " + ip };
            var response = GetSaldos(request);
            if (response.Success != 1)
                return -1;
            switch (tipoSaldo)
            {
                case 1:
                    return response.Saldos.DisponibleAdelantos;
                case 2:
                    return response.Saldos.DisponibleCompras;
                case 3:
                    return response.Saldos.DisponibleCuotas;
                case 4:
                    return response.Saldos.DisponiblePrestamos;
                case 5:
                    return response.Saldos.LimiteCompra;
                case 6:
                    return response.Saldos.Saldo;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Sobrecarga de GetSaldoPorCuenta
        /// </summary>
        /// <param name="numeroCuenta">numero de cuenta</param>
        /// <param name="idUsuario">Id de usuario</param>
        /// <param name="callerId">Id del invocador</param>
        /// <returns>Saldo</returns>
        [WebMethod]
        public SaldoResponse GetSaldosPorCuenta(String numeroCuenta, String idUsuario = "", String callerId = "null")
        {
            string ip = "Unknown";
            try
            {
                if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                {
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

            }
            catch (Exception)
            {
                ip = "InternalReq";
            }
            return GetSaldos(new OperarCuentaRequest { NumCuenta = numeroCuenta, Solicitante = idUsuario, Accion = "GetSaldosPorCuenta|Caller: " + callerId + "|ip: " + ip });
        }

        [WebMethod]
        public NominarCuentaResponse NominacionTarjeta(String numeroDeCuenta, String colonia, String nombreCalle, String numTarjeta, String codigoPostal, String nombreMunicipio,
            String numeroCalle, String tipoDireccion, String codigoEstado, String codigoMunicipio, String numeroDeDocumento, String tipoDocumento,
            String grupoLiquidacion, String telefono, DateTime fechaNacimiento, String estadoCivil, String hijos, String ocupacion, String sexo,
            String denominacionTarjeta, String nombreCompletoTitular)
        {
            Tarjeta tarjeta = Helper.GetTarjetaFromCuenta(numeroDeCuenta);
            String relacion = "TITULAR";
            String termTarjeta = numTarjeta.Length == 4 ? numTarjeta : numTarjeta.Substring(numTarjeta.Length - 4);
            if (tarjeta.NumeroTarjeta.Substring(12, 4) != termTarjeta)
            {
                tarjeta = Helper.GetTarjetaFromCuentaYTerm(numeroDeCuenta, tarjeta.NumeroTarjeta.Substring(0, 6) + "** ****" + termTarjeta);
                relacion = "ADICIONAL";
            }

            if (tarjeta == null)
                return new NominarCuentaResponse
                {
                    CodigoRespuesta = 993,
                    FechaCreacion = DateTime.Now.ToString("yyyy-MM-dd"),
                    Success = 0,
                    UserResponse = "No se encontro la tarjeta"
                };

            String importeGarantia = "0";

            List<Telefono> telefonos = new List<Telefono>();
            telefonos.Add(new Telefono
            {
                Numero = telefono,
                Tipo = "OFICINA"
            });

            Cuenta cuenta = new Cuenta()
            {
                Denominacion = nombreCompletoTitular,
                NumeroCuenta = numeroDeCuenta,
                Direccion = new Direccion()
                {
                    Barrio = colonia,
                    Calle = nombreCalle,
                    CodigoPostal = codigoPostal,
                    Localidad = nombreMunicipio,
                    Numero = numeroCalle,
                    Pais = "484",
                    ReferenciaAdicional = "",
                    Provincia = codigoMunicipio,
                    Tipo = tipoDireccion,
                    Zona = codigoEstado
                },
                Documento = new Documento()
                {
                    Numero = numeroDeDocumento,
                    Observaciones = "",
                    Tipo = tipoDocumento,
                },
                GrupoLiquidacion = grupoLiquidacion,
                ImporteGarantia = importeGarantia,
                Telefonos = telefonos,
                Emails = new List<Email> { new Email() }
            };

            cuenta.Tarjetas.TarjetasCredencial.Add(new TarjetaCredencial()
            {
                Persona = new Persona()
                {
                    Direccion = new Direccion()
                    {
                        Barrio = colonia,
                        Calle = nombreCalle,
                        CodigoPostal = codigoPostal,
                        Localidad = nombreMunicipio,
                        Numero = numeroCalle,
                        Pais = "484",
                        ReferenciaAdicional = "",
                        Tipo = tipoDireccion,
                        Zona = codigoEstado,
                        Provincia = codigoMunicipio
                    },
                    FechaNacimiento = fechaNacimiento.ToString("yyyyMMdd"),
                    EstadoCivil = estadoCivil,
                    Hijos = hijos,
                    Nombre = denominacionTarjeta,
                    Ocupacion = ocupacion,
                    Sexo = sexo,
                    Relacion = relacion,
                    Documento = new Documento()
                    {
                        Numero = numeroDeDocumento,
                        Observaciones = "",
                        Tipo = tipoDocumento,
                    },
                },
                Denominacion = denominacionTarjeta,
                Numero = tarjeta.NumeroTarjeta,
                Tipo = relacion,
            });
            return Nominar(new NominarCuentaRequest
            {
                Cuenta = cuenta
            }, numeroDeCuenta);
            /* 
            return Nominar(new NominarCuentaRequest
            {
                Cuenta = cuenta
            });
             */
        }

        [WebMethod]
        public LimiteResponse ReversoLimite(String nombreTarjeta, String numeroTarjeta, String fechaExpira, String cvc2, Int32 idMovimiento, String numeroAutorizacion, Int32 idUsuario)
        {
            BroxelEntities _ctx = new BroxelEntities();
            Movimiento mov = _ctx.Movimiento.FirstOrDefault(x => x.idMovimiento == idMovimiento && x.NoAutorizacion == numeroAutorizacion);
            if (mov != null)
            {
                LimiteRequest request = new LimiteRequest
                {
                    Tarjeta = new Tarjeta(nombreTarjeta, numeroTarjeta, fechaExpira, cvc2),
                    Tipo = Convert.ToInt32(mov.SubTipoTransaccion),
                    Limite = Convert.ToDecimal(mov.Monto),
                    UserID = idUsuario
                };
                var escenario = ConfigurationManager.AppSettings["UseNewDispersion"];
                return escenario == "1" ? ReversoLimiteNew(request, mov) : ReversoLimite(request, mov);
            }
            return new LimiteResponse();
        }

        [WebMethod]
        public CargoResponse SetCargo(Decimal monto, String nombreTarjeta, String numeroTarjeta, String cvc2, String fechaExpira, String numReferencia, String nombreReferencia, Int32 idUsuario, Int32 idComercio)
        {
            Trace.WriteLine(DateTime.Now.ToString("O") + " - Dentro de SetCargo: monto = " + monto.ToString("C") + ", nombreTarjeta= " + nombreTarjeta + ", numeroTarjeta = " + numeroTarjeta + ", cvc2 = " + cvc2 + ", fechaExpira " + fechaExpira + ", numReferencia = " +numReferencia+ ", nombreReferencia = " + nombreReferencia + ", idUsuario = " +idUsuario.ToString(CultureInfo.InvariantCulture) + ", idComercio = " + idComercio.ToString(CultureInfo.InvariantCulture));
            if (monto >= 0.01M)
            {
                CargoRequest request = new CargoRequest
                {
                    Tarjeta = new Tarjeta(nombreTarjeta, numeroTarjeta, fechaExpira, cvc2, Helper.GetCuentaFromTarjeta(numeroTarjeta)),
                    Cargo = new Cargo
                    {
                        Monto = Convert.ToDecimal(monto),
                        IdUsuario = idUsuario,
                        IdComercio = idComercio,
                        NombreReferencia = nombreReferencia,
                        NoReferencia = numReferencia
                    },
                    UserID = idUsuario
                };
                var escenario = ConfigurationManager.AppSettings["UseNewMethods"];
                CargoResponse response = escenario=="1"?SetCargoNew(request):SetCargo(request);
                return response;
            }
            return new CargoResponse();
        }

        [WebMethod]
        public CargoResponse SetCargoPorCallCenter(Decimal monto, String nombreTarjeta, String numeroTarjeta, String cvc2, String fechaExpira, String numReferencia, String nombreReferencia, Int32 idUsuario, Int32 idComercio, Int32 idUsuarioCallCenter)
        {
            if (monto >= 0.01M)
            {
                CargoRequest request = new CargoRequest
                {
                    Tarjeta = new Tarjeta(nombreTarjeta, numeroTarjeta, fechaExpira, cvc2),
                    Cargo = new Cargo
                    {
                        Monto = Convert.ToDecimal(monto),
                        IdUsuario = idUsuario,
                        IdComercio = idComercio,
                        NombreReferencia = nombreReferencia,
                        NoReferencia = numReferencia,
                    },
                    UserID = idUsuario
                };
                CargoResponse response = SetCargo(request, idUsuarioCallCenter);
                return response;
            }
            return new CargoResponse();
        }

        [WebMethod]
        public CargoResponse SetCargoNumCuenta(int idSecure, string numCuenta, decimal monto, int idComercio, string numReferencia, string nombreReferencia)
        {
            //idsecure
            CargoResponse response = new CargoResponse { CodigoRespuesta = 0, Success = 0 };
            try
            {
                //validando idSecure
                var idUsuarioOnline = new IdSecureComp().GetIdUserValid(idSecure);

                if (idUsuarioOnline > 0)
                {
                    var saldo = GetSaldoPorCuenta(numCuenta, 2, 0, "BroxelService.SetCargoNumCuenta");
                    if (monto <= saldo)
                    {
                        //obtenemos datos de la tarjeta
                        var tarjeta = Helper.GetTarjetaFromCuenta(numCuenta);

                        if (tarjeta != null)
                        {

                            response = SetCargoConCuenta(monto, tarjeta.NombreTarjeta, tarjeta.NumeroTarjeta,
                                tarjeta.Cvc2,
                                tarjeta.FechaExpira, numReferencia, nombreReferencia, 99999, idComercio,
                                numCuenta);

                        }
                        else
                        {
                            response.UserResponse = "El número de cuenta no tiene tarjeta.";
                        }
                    }
                    else
                    {
                        response.UserResponse = "No cuenta con saldo Suficiente para realizar el cargo $(" + monto +
                                                ") saldo disponibles para compras $(" + saldo + ")";
                    }


                }
                else
                {
                    response.UserResponse = "Usuario invalido";
                }

            }
            catch (Exception e)
            {
                response.UserResponse = "Ocurrio un error al realizar el cargo.";

            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="monto"></param>
        /// <param name="nombreTarjeta"></param>
        /// <param name="numeroTarjeta"></param>
        /// <param name="cvc2"></param>
        /// <param name="fechaExpira"></param>
        /// <param name="numReferencia"></param>
        /// <param name="nombreReferencia"></param>
        /// <param name="idUsuario"></param>
        /// <param name="idComercio"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        [WebMethod]
        public CargoResponse SetCargoCuenta(Decimal monto, String nombreTarjeta, String numeroTarjeta, String cvc2, String fechaExpira, String numReferencia, String nombreReferencia, Int32 idUsuario, Int32 idComercio, String cuenta)//
        {
            try
            {
                if (monto >= 0.01M)
                {
                    var request = new CargoRequest
                    {
                        Tarjeta = new Tarjeta(nombreTarjeta, numeroTarjeta, fechaExpira, cvc2, cuenta),
                        Cargo = new Cargo
                        {
                            Monto = Convert.ToDecimal(monto),
                            IdUsuario = idUsuario,
                            IdComercio = idComercio,
                            NombreReferencia = nombreReferencia,
                            NoReferencia = numReferencia
                        },
                        UserID = idUsuario
                    };
                    var escenario = ConfigurationManager.AppSettings["UseNewMethods"];
                    CargoResponse response = escenario == "1" ? SetCargoNew(request) : SetCargo(request);
                    return response;
                }
                return new CargoResponse();
            }
            catch (Exception exception)
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + " SetCargoCuenta Error:" +
                                exception);
                throw;
            }
        }

        [WebMethod]
        public CargoDeleteResponse ReversoCargo(Int32 idMovimiento, String numeroTarjeta, String cvc2, String fechaExpira, Int32 idUsuario)
        {
            BroxelEntities _ctx = new BroxelEntities();
            AnulacionN anul = _ctx.AnulacionN.FirstOrDefault(x => x.idTransaccion == idMovimiento && x.Autorizado == true);
            if (anul == null)
            {
                var escenario = ConfigurationManager.AppSettings["UseNewMethods"];
                var req = new CargoEditRequest
                {
                    UserID = idUsuario,
                    IdCargo = idMovimiento,
                    Tarjeta = new Tarjeta(null, numeroTarjeta, fechaExpira, cvc2),
                };
                return escenario == "1"?DeleteCargoNew(req):DeleteCargo(req);
            }
            return new CargoDeleteResponse();
        }

        [WebMethod]
        public CargoDeleteResponse ReversoPago(Int32 idMovimiento, String numeroTarjeta, String cvc2, String fechaExpira, Int32 idUsuario)
        {
            BroxelEntities _ctx = new BroxelEntities();
            AnulacionN anul = _ctx.AnulacionN.FirstOrDefault(x => x.idTransaccion == idMovimiento && x.Autorizado == true);
            if (anul == null)
            {
                return DeletePago(new CargoEditRequest
                {
                    UserID = idUsuario,
                    IdCargo = idMovimiento,
                    Tarjeta = new Tarjeta(null, numeroTarjeta, fechaExpira, cvc2),
                });
            }
            return new CargoDeleteResponse();
        }

        [WebMethod]
        public OperarCuentaResponse ActivacionDeCuenta(String numCuenta, String nombreSolicitante)
        {
            return ActivacionDeCuenta(new OperarCuentaRequest
            {
                NumCuenta = numCuenta,
                Solicitante = nombreSolicitante,
                Accion = "ActivarCuenta"
            });
        }

        [WebMethod]
        public OperarCuentaResponse BloqueoDeCuenta(String numCuenta, String nombreSolicitante)
        {
            return BloqueoDeCuenta(new OperarCuentaRequest
            {
                NumCuenta = numCuenta,
                Solicitante = nombreSolicitante,
                Accion = "BloquearCuenta"
            });
        }

        [WebMethod]
        public LimiteResponse EfectuarPago(Decimal monto, String nombreTarjeta, String numeroTarjeta, String cvc2, String fechaExpira, Int32 idUsuario, Int32 idComercio, String cuenta)
        {
            if (monto >= 0.01M)
            {
                CargoRequest request = new CargoRequest
                {
                    Tarjeta = new Tarjeta(nombreTarjeta, numeroTarjeta, fechaExpira, cvc2),
                    NumCuenta = cuenta,
                    Cargo = new Cargo
                    {
                        Monto = Convert.ToDecimal(monto),
                        IdUsuario = idUsuario,
                        IdComercio = idComercio,
                    },
                    UserID = idUsuario
                };
                LimiteResponse response = SetPago(request);
                return response;
            }
            return new LimiteResponse();
        }

        [WebMethod]
        public Boolean Incremento(String folio, String emailNotificar)
        {
            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + "Ingreso en incremento");

            lock (Helper.tablaDispersion)
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + "broxelco_rdgEntities()");
                broxelco_rdgEntities dbHelper = new broxelco_rdgEntities();
                var dispersionSolicitud = dbHelper.dispersionesSolicitudes.Where(x => x.folio == folio).ToList();
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + " var dispersionSolicitud = dbHelper.dispersionesSolicitudes.Where(x => x.folio == folio).ToList();");
                if (dispersionSolicitud.Count == 1 && dispersionSolicitud[0].estado == "WebService" && dispersionSolicitud[0].tipo.ToUpper() == "INCREMENTO")
                {
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + "if (dispersionSolicitud.Count == 1 && dispersionSolicitud[0].estado == WebService && dispersionSolicitud[0].tipo.ToUpper() == INCREMENTO");
                    string claveCliente = dispersionSolicitud[0].claveCliente;
                    string producto = dispersionSolicitud[0].producto;
                    var detalleClientesBroxel = dbHelper.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == claveCliente && x.Producto == producto);
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + "var detalleClientesBroxel = dbHelper.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == claveCliente && x.Producto == producto);");
                    if (detalleClientesBroxel == null)
                    {
                        dispersionSolicitud[0].estado = "PENDIENTE";
                        dbHelper.SaveChanges();
                        Helper.SendMail("asignaciondelinea@broxel.com",
                       dispersionSolicitud[0].usuarioEjecucion + ", " + dispersionSolicitud[0].usuarioAprobacion + ", " +
                       dispersionSolicitud[0].usuarioCreacion, "Error en cliente-producto - Solitud de asignación " + dispersionSolicitud[0].folio,
                       "La solicitud con número " + dispersionSolicitud[0].folio +
                       " no puede ser ejecutada ya que no existe relación entre cliente-producto definido. <br><br>Favor de agregarlo<br>.",
                       "yMQ3E3ert6", "Broxel : Asignacion de Lineas");
                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + " if (detalleClientesBroxel == null)");
                        return false;
                    }
                    //Validacion de BackList
                    if (detalleClientesBroxel.BlackList)
                    {
                        var nombreCorto = "NO DEFINIDO";
                        var clienteBroxel =
                            dbHelper.clientesBroxel.FirstOrDefault(
                                c => c.claveCliente == detalleClientesBroxel.ClaveCliente);
                        if (clienteBroxel != null)
                            nombreCorto = clienteBroxel.NombreCorto;

                        dispersionSolicitud[0].estado = "PENDIENTE";
                        dbHelper.SaveChanges();
                        Helper.SendMail("dispersiones@broxel.com", "asignaciondelinea@broxel.com", "Error en cliente-producto - BlackList " + dispersionSolicitud[0].folio,
                       "La solicitud con número " + dispersionSolicitud[0].folio +
                       " no puede ser ejecutada ya que la relación entre cliente-producto se encuentra en blackList para el cliente " + detalleClientesBroxel.ClaveCliente +
                       "(" + nombreCorto + "). <br><br>Si esto es un error, favor de verificarlo<br>.",
                       "yMQ3E3ert6", "Broxel : Asignacion de Lineas");
                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + " if (detalleClientesBroxel.BlackList)");
                        return false;
                    }
                    int countEstadoWebService = dbHelper.dispersionesSolicitudes.Count(x => x.claveCliente == claveCliente && (x.estado == "Validando" || x.estado == "Ejecutando") && x.producto == producto);
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + " int countEstadoWebService = dbHelper.dispersionesSolicitudes.Count(x => x.claveCliente == claveCliente && (x.estado == Validando || x.estado == Ejecutando) && x.producto == producto);");
                    if (countEstadoWebService == 0)
                    {
                        dispersionSolicitud[0].estado = "Validando";
                        dbHelper.SaveChanges();
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            Helper.CorrerDispersion(folio, "INCREMENTO");
                        });
                        return true;
                    }
                    dispersionSolicitud[0].estado = detalleClientesBroxel.EncolaDispersiones ? "PorEjecutar" : "PENDIENTE";
                    dbHelper.SaveChanges();

                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + "  dbHelper.SaveChanges();");

                    if (!detalleClientesBroxel.EncolaDispersiones)
                    {
                        var to = dispersionSolicitud[0].usuarioEjecucion != "webservice" ? dispersionSolicitud[0].usuarioEjecucion + ", " + dispersionSolicitud[0].usuarioAprobacion + ", " + dispersionSolicitud[0].usuarioCreacion : ConfigurationManager.AppSettings["emailsAvisosDispersiones"];

                        Helper.SendMail("dispersiones@broxel.com", to
                            , "Solitud de asignación " + dispersionSolicitud[0].folio,
                            "Su solicitud con número " + dispersionSolicitud[0].folio +
                            " no puede ser ejecutada en este momento, debido a que se está ejecutando otra solicitud para el mismo cliente.<br><br> Una vez concluída la anterior podrá ejecutarla nuevamente.<br><br> Para volver a ejecutarla favor de ingresar al portal.",
                            "yMQ3E3ert6", "Broxel : Asignacion de Lineas");

                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + "  Helper.SendMail(");
                    }
                }
                return false;
            }
        }

        [WebMethod]
        public Boolean CargoMasivo(String folio)
        {
            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + "Ingreso en CargoMasivo");

            lock (Helper.tablaDispersion)
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + "BroxelSQLEntities()");
                BroxelSQLEntities sqlHelper = new BroxelSQLEntities();
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + "broxelco_rdgEntities()");
                broxelco_rdgEntities mysqlHelper = new broxelco_rdgEntities();
                var cargoSolicitud = sqlHelper.CargosSolicitudes.Where(x => x.Folio == folio).ToList();
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + " var dispersionSolicitud = dbHelper.dispersionesSolicitudes.Where(x => x.folio == folio).ToList();");
                if (cargoSolicitud.Count == 1 && cargoSolicitud[0].Estado == "WebService" && cargoSolicitud[0].Tipo.ToUpper() == "CARGO")
                {
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + "if (dispersionSolicitud.Count == 1 && dispersionSolicitud[0].estado == WebService && dispersionSolicitud[0].tipo.ToUpper() == INCREMENTO");
                    string claveCliente = cargoSolicitud[0].ClaveCliente;
                    string producto = cargoSolicitud[0].Producto;
                    var detalleClientesBroxel = mysqlHelper.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == claveCliente && x.Producto == producto);
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + "var detalleClientesBroxel = dbHelper.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == claveCliente && x.Producto == producto);");
                    if (detalleClientesBroxel == null)
                    {
                        cargoSolicitud[0].Estado = "PENDIENTE";
                        sqlHelper.SaveChanges();
                        Helper.SendMail("asignaciondelinea@broxel.com",
                       cargoSolicitud[0].UsuarioEjecucion + ", " + cargoSolicitud[0].UsuarioAprobacion + ", " +
                       cargoSolicitud[0].UsuarioCreacion, "Error en cliente-producto - Solitud de asignación " + cargoSolicitud[0].Folio,
                       "La solicitud con número " + cargoSolicitud[0].Folio +
                       " no puede ser ejecutada ya que no existe relación entre cliente-producto definido. <br><br>Favor de agregarlo<br>.",
                       "yMQ3E3ert6", "Broxel : Asignacion de Lineas");
                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + " if (detalleClientesBroxel == null)");
                        return false;
                    }
                    /*int countEstadoWebService = 0sqlHelper.CargosSolicitudes.Count(x => x.ClaveCliente == claveCliente && (x.Estado == "Validando" || x.Estado == "Ejecutando") && x.Producto == producto);
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + " int countEstadoWebService = dbHelper.dispersionesSolicitudes.Count(x => x.claveCliente == claveCliente && (x.estado == Validando || x.estado == Ejecutando) && x.producto == producto);");
                    if (countEstadoWebService == 0)
                    {
                     */
                    cargoSolicitud[0].Estado = "Validando";
                    sqlHelper.SaveChanges();
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        Helper.CorrerCargo(folio, "CARGO");
                    });
                    return true;/*
                    }
                    cargoSolicitud[0].Estado = detalleClientesBroxel.EncolaDispersiones ? "PorEjecutar" : "PENDIENTE";
                    sqlHelper.SaveChanges();

                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + "  sqlHelper.SaveChanges();");

                    Helper.SendMail("dispersiones@broxel.com",
                        cargoSolicitud[0].UsuarioEjecucion + ", " + cargoSolicitud[0].UsuarioAprobacion + ", " +
                        cargoSolicitud[0].UsuarioCreacion, "Solitud de asignación " + cargoSolicitud[0].Folio,
                        "Su solicitud con número " + cargoSolicitud[0].Folio +
                        " no puede ser ejecutada en este momento, debido a que se está ejecutando otra solicitud para el mismo cliente.<br><br> Una vez concluída la anterior podrá ejecutarla nuevamente.<br><br> Para volver a ejecutarla favor de ingresar al portal.",
                        "yMQ3E3ert6", "Broxel : Asignacion de Lineas");

                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + "  Helper.SendMail(");
                    */
                }
                return false;
            }
        }

        [WebMethod]
        public Boolean Transferir(String folio)
        {
            lock (Helper.tablaDispersion)
            {
                broxelco_rdgEntities dbHelper = new broxelco_rdgEntities();
                var transferenciaSolicitud = dbHelper.TransferenciasSolicitud.Where(x => x.Folio == folio).ToList();
                //JAVG //20150918 //Cambio solicitado por AGM: Se añade el tipo OneToOne
                if (transferenciaSolicitud.Count == 1 && transferenciaSolicitud[0].Estado == "WebService" &&
                        (transferenciaSolicitud[0].Tipo.ToUpper() == "ACUENTACONCENTRADORA" || transferenciaSolicitud[0].Tipo.ToUpper() == "CONCENTRADORAACUENTAS" || transferenciaSolicitud[0].Tipo.ToUpper() == "ONETOONE")
                    )
                {
                    string claveCliente = transferenciaSolicitud[0].ClaveCliente;
                    string producto = transferenciaSolicitud[0].Producto;
                    var detalleClientesBroxel = dbHelper.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == claveCliente && x.Producto == producto);
                    if (detalleClientesBroxel == null)
                    {
                        transferenciaSolicitud[0].Estado = "PENDIENTE";
                        dbHelper.SaveChanges();
                        Helper.SendMail("asignaciondelinea@broxel.com",
                       transferenciaSolicitud[0].UsuarioEjecucion + ", " + transferenciaSolicitud[0].UsuarioAprobacion + ", " +
                       transferenciaSolicitud[0].UsuarioCreacion, "Error en cliente-producto - Solitud de Transferencia " + transferenciaSolicitud[0].Folio,
                       "La solicitud con número " + transferenciaSolicitud[0].Folio +
                       " no puede ser ejecutada ya que no existe relación entre cliente-producto definido. <br><br>Favor de agregarlo<br>.",
                       "yMQ3E3ert6", "Broxel : Asignacion de Lineas");
                        return false;
                    }
                    int countEstadoWebService = dbHelper.TransferenciasSolicitud.Count(x => x.ClaveCliente == claveCliente && (x.Estado == "Validando" || x.Estado == "Ejecutando") && x.Producto == producto);
                    if (countEstadoWebService == 0)
                    {
                        transferenciaSolicitud[0].Estado = "Validando";
                        dbHelper.SaveChanges();
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            Helper.CorrerTransferencia(folio);
                        });
                        return true;
                    }
                    transferenciaSolicitud[0].Estado = "PENDIENTE";
                    dbHelper.SaveChanges();
                    Helper.SendMail("dispersiones@broxel.com",
                        transferenciaSolicitud[0].UsuarioEjecucion + ", " + transferenciaSolicitud[0].UsuarioAprobacion + ", " +
                        transferenciaSolicitud[0].UsuarioCreacion, "Solitud de Transferencia " + transferenciaSolicitud[0].Folio,
                        "Su solicitud con número " + transferenciaSolicitud[0].Folio +
                        " no puede ser ejecutada en este momento, debido a que se está ejecutando otra solicitud para el mismo cliente.<br><br> Una vez concluída la anterior podrá ejecutarla nuevamente.<br><br> Para volver a ejecutarla favor de ingresar al portal.",
                        "yMQ3E3ert6", "Broxel : Transferencia entre cuentas");
                }
                return false;
            }
        }

        [WebMethod]
        public Boolean Pago(String folio)
        {
            lock (Helper.tablaDispersion)
            {
                broxelco_rdgEntities dbHelper = new broxelco_rdgEntities();
                var pagoSolicitud = dbHelper.pagosSolicitudes.Where(x => x.folio == folio).ToList();
                if (pagoSolicitud.Count == 1 && pagoSolicitud[0].estado == "WebService" && pagoSolicitud[0].tipo.ToUpper() == "PAGO")
                {
                    string claveCliente = pagoSolicitud[0].claveCliente;
                    string producto = pagoSolicitud[0].producto;
                    var detalleClientesBroxel = dbHelper.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == claveCliente && x.Producto == producto);
                    if (detalleClientesBroxel == null)
                    {
                        pagoSolicitud[0].estado = "PENDIENTE";
                        dbHelper.SaveChanges();

                        Helper.SendMail("asignaciondelinea@broxel.com",
                        pagoSolicitud[0].usuarioEjecucion + ", " + pagoSolicitud[0].usuarioAprobacion + ", " +
                        pagoSolicitud[0].usuarioCreacion, "Error en cliente-producto - Solitud de pago " + pagoSolicitud[0].folio,
                        "La solicitud de pago con número " + pagoSolicitud[0].folio +
                        " no puede ser ejecutada ya que no existe relación entre cliente-producto definido. <br><br>Favor de agregarlo<br>.",
                        "yMQ3E3ert6", "Broxel : Asignacion de Lineas");
                        return false;
                    }
                    int countEstadoWebService = dbHelper.pagosSolicitudes.Count(x => x.claveCliente == claveCliente && x.producto == producto && (x.estado == "Validando" || x.estado == "Ejecutando"));
                    if (countEstadoWebService == 0)
                    {
                        pagoSolicitud[0].estado = "Validando";
                        dbHelper.SaveChanges();
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            Helper.CorrerPago(folio);
                        });
                        return true;
                    }
                    pagoSolicitud[0].estado = detalleClientesBroxel.EncolaDispersiones ? "PorEjecutar" : "PENDIENTE";
                    dbHelper.SaveChanges();
                    Helper.SendMail("dispersiones@broxel.com",
                        pagoSolicitud[0].usuarioEjecucion + ", " + pagoSolicitud[0].usuarioAprobacion + ", " +
                        pagoSolicitud[0].usuarioCreacion, "Solitud de asignación " + pagoSolicitud[0].folio,
                        "Su solicitud de pago con número " + pagoSolicitud[0].folio +
                        " no puede ser ejecutada en este momento, debido a que se está ejecutando otra solicitud para el mismo cliente.<br><br> Una vez concluída la anterior podrá ejecutarla nuevamente.<br><br> Para volver a ejecutarla favor de ingresar al portal.",
                        "yMQ3E3ert6", "Broxel : Asignacion de Lineas");
                }
                return false;
            }
        }

        [WebMethod]
        public Boolean Recarga(String folio, String emailNotificar)
        {
            lock (Helper.tablaDispersion)
            {
                broxelco_rdgEntities dbHelper = new broxelco_rdgEntities();
                var dispersionSolicitud = dbHelper.dispersionesSolicitudes.Where(x => x.folio == folio).ToList();
                if (dispersionSolicitud.Count == 1 && dispersionSolicitud[0].estado == "WebService" && dispersionSolicitud[0].tipo.ToUpper() == "RECARGA")
                {
                    string claveCliente = dispersionSolicitud[0].claveCliente;
                    string producto = dispersionSolicitud[0].producto;
                    var detalleClientesBroxel = dbHelper.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == claveCliente && x.Producto == producto);
                    if (detalleClientesBroxel == null)
                        return false;
                    //Validacion de BackList
                    if (detalleClientesBroxel.BlackList)
                    {
                        var nombreCorto = "NO DEFINIDO";
                        var clienteBroxel =
                            dbHelper.clientesBroxel.FirstOrDefault(
                                c => c.claveCliente == detalleClientesBroxel.ClaveCliente);
                        if (clienteBroxel != null)
                            nombreCorto = clienteBroxel.NombreCorto;

                        dispersionSolicitud[0].estado = "PENDIENTE";
                        dbHelper.SaveChanges();
                        Helper.SendMail("dispersiones@broxel.com", "asignaciondelinea@broxel.com", "Error en cliente-producto - BlackList " + dispersionSolicitud[0].folio,
                       "La solicitud con número " + dispersionSolicitud[0].folio +
                       " no puede ser ejecutada ya que la relación entre cliente-producto se encuentra en blackList para el cliente " + detalleClientesBroxel.ClaveCliente +
                       "(" + nombreCorto + "). <br><br>Si esto es un error, favor de verificarlo<br>.",
                       "yMQ3E3ert6", "Broxel : Asignacion de Lineas");
                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fffff") + " - " + " if (detalleClientesBroxel.BlackList)");
                        return false;
                    }

                    var solicitudesDeCliente = dbHelper.dispersionesSolicitudes.Where(x => x.claveCliente == claveCliente).ToList();
                    int countEstadoWebService = solicitudesDeCliente.Count(d => d.claveCliente == claveCliente && d.producto == producto && (d.estado == "Validando" || d.estado == "Ejecutando"));
                    if (countEstadoWebService == 0)
                    {
                        dispersionSolicitud[0].estado = "Validando";
                        dbHelper.SaveChanges();
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            Helper.CorrerDispersion(folio, "RECARGA");
                        });
                        return true;
                    }
                    dispersionSolicitud[0].estado = detalleClientesBroxel.EncolaDispersiones ? "PorEjecutar" : "PENDIENTE";
                    dbHelper.SaveChanges();

                    var to = dispersionSolicitud[0].usuarioEjecucion != "webservice" ? dispersionSolicitud[0].usuarioEjecucion + ", " + dispersionSolicitud[0].usuarioAprobacion + ", " + dispersionSolicitud[0].usuarioCreacion : ConfigurationManager.AppSettings["emailsAvisosDispersiones"];

                    Helper.SendMail("dispersiones@broxel.com", to, "Solitud de asignación " + dispersionSolicitud[0].folio,
                    "Su solicitud con número " + dispersionSolicitud[0].folio +
                    " no puede ser ejecutada en este momento, debido a que se está ejecutando otra solicitud para el mismo cliente.<br><br> Una vez concluída la anterior podrá ejecutarla nuevamente.<br><br> Para volver a ejecutarla favor de ingresar al portal.",
                    "yMQ3E3ert6", "Corporativos Broxel");
                }
                return false;
            }
        }

        [WebMethod]
        public Boolean ReversoDispersion(String folio, String emailNotificar)
        {
            lock (Helper.tablaDispersion)
            {
                broxelco_rdgEntities dbHelper = new broxelco_rdgEntities();
                var dispersionSolicitud = dbHelper.dispersionesSolicitudes.Where(x => x.folio == folio).ToList();
                if (dispersionSolicitud.Count == 1 && dispersionSolicitud[0].estado.ToUpper() == "WEBSERVICEREVERSO")
                {
                    string claveCliente = dispersionSolicitud[0].claveCliente;
                    string producto = dispersionSolicitud[0].producto;
                    var solicitudesDeCliente = dbHelper.dispersionesSolicitudes.Where(x => x.claveCliente == claveCliente && x.producto == producto).ToList();
                    int countEstadoWebService = solicitudesDeCliente.Count(d => d.estado == "Validando" || d.estado == "Ejecutando" || d.estado == "Reversando");
                    if (countEstadoWebService == 0)
                    {
                        dispersionSolicitud[0].estado = "Reversando";
                        dbHelper.SaveChanges();
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            Helper.CorrerReversa(folio);
                        });
                        return true;
                    }
                    dispersionSolicitud[0].estado = "ReversoAprobado";
                    dbHelper.SaveChanges();
                    Helper.SendMail("dispersiones@broxel.com", "asignacionesdelinea@broxel.com", "Solitud de asignación " + dispersionSolicitud[0].folio,
    "Su solicitud con número " + dispersionSolicitud[0].folio +
    " no puede ser reversada en este momento, debido a que se está ejecutando otra solicitud para el mismo cliente.<br><br> Una vez concluída la anterior podrá ejecutarla nuevamente.<br><br> Para volver a ejecutarla favor de ingresar al portal.",
    "yMQ3E3ert6", "Corporativos Broxel");
                }
                return false;
            }
        }

        [WebMethod]
        public Boolean Renominar(String folio, String emailNotificar)
        {
            lock (Helper.tablaDispersion)
            {
                broxelco_rdgEntities dbHelper = new broxelco_rdgEntities();
                var renominacionSolicitud = dbHelper.RenominacionSolicitudes.Where(x => x.Folio == folio).ToList();
                if (renominacionSolicitud.Count == 1 && renominacionSolicitud[0].Estado == "WebService" && renominacionSolicitud[0].Tipo.ToUpper() == "RENOMINACION")
                {
                    string claveCliente = renominacionSolicitud[0].ClaveCliente;
                    string producto = renominacionSolicitud[0].Producto;
                    var detalleClientesBroxel = dbHelper.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == claveCliente && x.Producto == producto);
                    if (detalleClientesBroxel == null)
                    {
                        renominacionSolicitud[0].Estado = "PENDIENTE";
                        dbHelper.SaveChanges();
                        Helper.SendMail("asignaciondelinea@broxel.com",
                       renominacionSolicitud[0].UsuarioEjecucion + ", " + renominacionSolicitud[0].UsuarioAprobacion + ", " +
                       renominacionSolicitud[0].UsuarioCreacion, "Error en cliente-producto - Solitud de renominacion " + renominacionSolicitud[0].Folio,
                       "La solicitud con número " + renominacionSolicitud[0].Folio +
                       " no puede ser ejecutada ya que no existe relación entre cliente-producto definido. <br><br>Favor de agregarlo<br>.",
                       "yMQ3E3ert6", "Broxel : Renominación de cuentas");
                        return false;
                    }
                    int countEstadoWebService = dbHelper.RenominacionSolicitudes.Count(x => x.ClaveCliente == claveCliente && (x.Estado == "Validando" || x.Estado == "Ejecutando") && x.Producto == producto);
                    if (countEstadoWebService == 0)
                    {
                        renominacionSolicitud[0].Estado = "Validando";
                        dbHelper.SaveChanges();
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            Helper.CorrerRenominacion(folio);
                        });
                        return true;
                    }
                    renominacionSolicitud[0].Estado = "PENDIENTE";
                    dbHelper.SaveChanges();
                    Helper.SendMail("dispersiones@broxel.com",
                        renominacionSolicitud[0].UsuarioEjecucion + ", " + renominacionSolicitud[0].UsuarioAprobacion + ", " +
                        renominacionSolicitud[0].UsuarioCreacion, "Solitud de renominación " + renominacionSolicitud[0].Folio,
                        "Su solicitud de renominación con número " + renominacionSolicitud[0].Folio +
                        " no puede ser ejecutada en este momento, debido a que se está ejecutando otra solicitud para el mismo cliente.<br><br> Una vez concluída la anterior podrá ejecutarla nuevamente.<br><br> Para volver a ejecutarla favor de ingresar al portal.",
                        "yMQ3E3ert6", "Broxel : Renominación de cuentas");
                }
                return false;
            }
        }

        [WebMethod]
        public Boolean Devolucion(String folio)
        {
            lock (Helper.tablaDispersion)
            {
                broxelco_rdgEntities dbHelper = new broxelco_rdgEntities();
                var devolucionSoliciutud = dbHelper.devolucionesSolicitudes.Where(x => x.folio == folio).ToList();
                if (devolucionSoliciutud.Count == 1 && devolucionSoliciutud[0].estado == "WebService" && devolucionSoliciutud[0].tipo.ToUpper() == "DEVOLUCION")
                {
                    string claveCliente = devolucionSoliciutud[0].claveCliente;
                    string producto = devolucionSoliciutud[0].producto;
                    var detalleClientesBroxel = dbHelper.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == claveCliente && x.Producto == producto);
                    if (detalleClientesBroxel == null)
                        return false;
                    var solicitudesDeCliente = dbHelper.dispersionesSolicitudes.Where(x => x.claveCliente == claveCliente).ToList();
                    int countEstadoWebService = solicitudesDeCliente.Count(d => d.claveCliente == claveCliente && d.producto == producto && (d.estado == "Validando" || d.estado == "Ejecutando"));
                    if (countEstadoWebService == 0)
                    {
                        devolucionSoliciutud[0].estado = "Validando";
                        dbHelper.SaveChanges();
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            Helper.CorrerDevolucion(folio);
                        });
                        return true;
                    }
                    devolucionSoliciutud[0].estado = "PENDIENTE";
                    dbHelper.SaveChanges();
                    Helper.SendMail("dispersiones@broxel.com",
    devolucionSoliciutud[0].usuarioEjecucion + ", " + devolucionSoliciutud[0].usuarioAprobacion + ", " +
    devolucionSoliciutud[0].usuarioCreacion, "Solitud de asignación " + devolucionSoliciutud[0].folio,
    "Su solicitud con número " + devolucionSoliciutud[0].folio +
    " no puede ser ejecutada en este momento, debido a que se está ejecutando otra solicitud para el mismo cliente.<br><br> Una vez concluída la anterior podrá ejecutarla nuevamente.<br><br> Para volver a ejecutarla favor de ingresar al portal.",
    "yMQ3E3ert6", "Corporativos Broxel");
                }
                return false;
            }
        }

        [WebMethod]
        public ConsultarPorCuentaResponse1 ConsultaPorCuentaMirror(String cuenta, String nombreSolicitante, DateTime desde, DateTime hasta, int paginaInicio, int tipo = 0)
        {
            return ConsultaPorCuenta(cuenta, nombreSolicitante, desde, hasta, paginaInicio, tipo: tipo);
        }

        [WebMethod]
        public Boolean RefreshData()
        {
            Entities entities = Entities.Instance;
            entities.RefreshData();
            entities = Entities.Instance;
            return true;
        }

        [WebMethod]
        public TransferenciaResponse TransferenciaDeCuentas(String cuentaOrigen, Decimal monto, String tarjetaDestino, String emailSolicitante, String tipo, Decimal porcentaje = 0)
        {
            Tarjeta TarjetaOrigen = Helper.GetTarjetaFromCuenta(cuentaOrigen);
            //Tarjeta TarjetaDestino = new Tarjeta(numTarjeta: tarjetaDestino, cuenta: Helper.GetCuentaFromTarjeta(tarjetaDestino),procesador=Helper.GetTarjetaFromCuenta());
            Tarjeta TarjetaDestino = Helper.GetTarjetaFromTarjeta(tarjetaDestino);
            if (TarjetaOrigen == null)
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontró la cuenta origen.",
                };
            if (String.IsNullOrEmpty(TarjetaDestino.Cuenta))
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontró la tarjeta destino.",
                };
            ComisionTransferencia comision = new ComisionTransferencia(cuentaOrigen, monto, TarjetaDestino.Cuenta, tipo);
            comision.InitializeComponents();
            if (!comision.TransferenciaEnvioValida)
                return new TransferenciaResponse
                {
                    CodigoRespuesta = 984,
                    UserResponse = "Este producto no acepta envio de transferencias.",
                    Comision = comision,
                };
            if (!comision.TransferenciaRecepcionValida)
                return new TransferenciaResponse
                {
                    CodigoRespuesta = 985,
                    UserResponse = "Este producto no acepta recepción de transferencias.",
                    Comision = comision,
                };

            TransferenciaRequest _transferenciaRequest = new TransferenciaRequest
            {
                idComercio = 1875,
                Accion = "TransferenciaEntreCuentas",
                Comision = comision.MontoComision,
                NumCuenta = cuentaOrigen,
                Monto = monto,
                Tarjeta = TarjetaOrigen,
                Solicitante = emailSolicitante,
                TarjetaRecibe = TarjetaDestino,
                TipoConceptoComision = comision.TipoConceptoComision,
                UserID = 3710,
                Porcentaje = porcentaje
            };
            TransferenciaResponse tr = TransferenciaDeCuentas(_transferenciaRequest);
            tr.Comision = comision;
            return tr;
        }

        [WebMethod]
        public ConsultarCodigosResponse1 ConsultaDeCodigos(string nombreSolicitante)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var p = _operadorWs.ConsultarCodigos(new ConsultarCodigosRequest1
            {
                ConsultarCodigosRequest = new ConsultarCodigosRequest
                {
                    Autenticacion = new wsOperaciones.Autenticacion
                    {
                        Usuario = "broxel",
                        Password = Helper.CipherPassCREA("bRoXeL_1.2.3.4"),
                    },
                    Originador = new wsOperaciones.Originador
                    {
                        Solicitante = nombreSolicitante,
                        ZonaHoraria = "America/Mexico_City",
                    }
                }
            });
            return p;
        }

        #region WrappersMovimientos


        // Movimientos OnlineBroxel y Apps Paginados
        // Movimientos Operaciones
        // Movimientos PortalesCoporativos
        // Todos por cuenta, con fecha inicio max a 6 meses, con fecha inicio input usuario max 6 meses, con fecha fin minimo hoy, con fecha fin input usuario max 5 meses

        [WebMethod]
        public MovimientoOnlineResponse GetMovimientosOperaciones(String numCuenta, DateTime fechaInicio, DateTime fechaFin, int paginaInicio, int paginaFin)
        {
            var consulta = ConsultaPorCuenta(numCuenta, "", fechaInicio, fechaFin, paginaInicio, paginaFin);
            MovimientoOnlineResponse response = new MovimientoOnlineResponse();
            var lo = consulta.ConsultarPorCuentaResponse.Listado;
            if (consulta.ConsultarPorCuentaResponse.Response.Codigo != "00")
            {
                response.Success = false;
                response.UserResponse = consulta.ConsultarPorCuentaResponse.Response.Descripcion;
                return response;
            }

            foreach (var op in lo.Operaciones.Where(x => x.Codigo.Tipo != "129" && x.Codigo.Tipo != "35" && x.Respuesta.Codigo == "00" && x.Neutralizada == "N"))
            {
                response.Movimientos.Add(new MovimientoOnline
                {
                    Cargo = Decimal.Parse(op.Montos.Original.Importe),//Decimal.Parse(op.Montos.Convertido.Importe),
                    Descripcion = op.Comercio.Descripcion,
                    Fecha = Convert.ToDateTime(op.Momento),
                    ImpOriginal = op.Montos.Original.Importe,
                    MonedaOriginal = op.Montos.Original.Moneda.Descripcion,
                    NumAutorizacion = op.Autorizacion,
                    NumTarjeta = op.Tarjeta
                });
            }
            response.Success = true;
            response.UserResponse = "Éxito";
            return response;
        }

        [WebMethod]
        public MovimientoOnlineResponse GetMovimientoOnlineBroxel(String numCuenta, DateTime fechaInicio, DateTime fechaFin, int pagina, string email)
        {

            /// Query que trae las transferencias

            var conn = new MySqlDataAccess();
            var listDeTerceros = conn.ObtenerTransferenciasDeTerceros(numCuenta, fechaInicio, fechaFin);
            var listATerceros = conn.ObtenerTransferenciasATerceros(numCuenta, fechaInicio, fechaFin);
            var listaCargosDisp = conn.ObtenerCargosDisposiciones(numCuenta, fechaInicio, fechaFin);
            var listDisposiciones = conn.ObtenerTransferenciasDisposicion(numCuenta, fechaInicio, fechaFin);
            var listAbonosCuenta = conn.ObtenerTrasnferenciasAbonos(numCuenta, fechaInicio, fechaFin);

            var consulta = ConsultaPorCuenta(numCuenta, email, fechaInicio, fechaFin, pagina, pagina);
            var response = new MovimientoOnlineResponse();
            var lo = consulta.ConsultarPorCuentaResponse.Listado;
            if (consulta.ConsultarPorCuentaResponse.Response.Codigo != "00")
            {
                response.Success = false;
                response.UserResponse = consulta.ConsultarPorCuentaResponse.Response.Descripcion;
                return response;
            }

            foreach (var operacion in lo.Operaciones.Where(x => x.Codigo.Tipo != "129" && (x.Codigo.Tipo != "35" || x.Codigo.SubTipo != "3") && x.Respuesta.Codigo == "00" && x.Neutralizada == "N"))
            {


                if (!(operacion.Montos.Original.Importe.Contains("4000") && operacion.Cuenta == "522611666" && operacion.Autorizacion.Contains("023341")))
                    response.Movimientos.Add(new MovimientoOnline
                    {
                        Cargo = operacion.Montos.Original.Moneda.Codigo == mxCurrency ? Decimal.Parse(operacion.Montos.Original.Importe) : Decimal.Parse(operacion.Montos.Convertido.Importe),
                        Descripcion = ObtenerDescripcionMovimiento(operacion, listDeTerceros, listATerceros),
                        //Descripcion = (operacion.Codigo.Tipo == "35" || (operacion.Codigo.Tipo == "33" && operacion.Codigo.SubTipo != "2")) ? ("SU PAGO - GRACIAS")
                        //: (operacion.Codigo.Tipo == "67") ? "C2C DE " + ObtenerTitularTransferenciaRemitente(listDeTerceros, operacion.Autorizacion, true, Convert.ToDateTime(operacion.Momento))
                        //: (operacion.Codigo.Tipo == "65") ? "C2C A " + ObtenerTitutarTransferenciaDestinatario(listATerceros, operacion.Autorizacion,true, Convert.ToDateTime(operacion.Momento))
                        //: (operacion.Codigo.Tipo == "257" ? "CAMBIO DE NIP" 
                        //: (operacion.Comercio.Codigo == "999999999001845" ? operacion.Codigo.Descripcion 
                        //: ((operacion.Codigo.Tipo == "1027" && operacion.Codigo.SubTipo == "0") ? "SU PAGO - GRACIAS" 
                        //: (operacion.Comercio.Codigo.StartsWith("999999") ? GetNombreComercioPorCodigo(operacion.Comercio.Codigo)
                        //: operacion.Comercio.Descripcion)))) + (operacion.Montos.Original.Moneda.Codigo == mxCurrency ? "" : " " +
                        //operacion.Montos.Original.Importe + " " + operacion.Montos.Original.Moneda.Descripcion),
                        Fecha = Convert.ToDateTime(operacion.Momento),
                        ImpOriginal = operacion.Montos.Original.Importe,
                        MonedaOriginal = GetMonedaAbr(operacion.Montos.Original.Moneda.Codigo),
                        NumAutorizacion = operacion.Autorizacion,
                        NumTarjeta = operacion.Tarjeta,
                        Destinatario = operacion.Codigo.Tipo == "67" ? ObtenerTitularTransferenciaRemitente(listDeTerceros, operacion.Autorizacion, false, Convert.ToDateTime(operacion.Momento)) : (operacion.Codigo.Tipo == "65") ? ObtenerTitutarTransferenciaDestinatario(listATerceros, operacion.Autorizacion, true, Convert.ToDateTime(operacion.Momento)) : operacion.Codigo.Tipo == "1" ? ObtenerDestinatarioDisposicion(listDisposiciones, operacion.Autorizacion, Convert.ToDateTime(operacion.Momento)) != null ? ObtenerDestinatarioDisposicion(listDisposiciones, operacion.Autorizacion, Convert.ToDateTime(operacion.Momento))
                        : ObtenerDescripcionMovimiento(operacion, listDeTerceros, listATerceros) : "",
                        Remitente = operacion.Codigo.Tipo == "67" ? ObtenerTitularTransferenciaRemitente(listDeTerceros, operacion.Autorizacion, true, Convert.ToDateTime(operacion.Momento)) : (operacion.Codigo.Tipo == "65") ? ObtenerTitutarTransferenciaDestinatario(listATerceros, operacion.Autorizacion, false, Convert.ToDateTime(operacion.Momento)) : (operacion.Codigo.Tipo == "35" || (operacion.Codigo.Tipo == "33" && operacion.Codigo.SubTipo != "2")) ? ObtenerRemitenteAbono(listAbonosCuenta, operacion.Autorizacion) : "",
                        Comision = operacion.Codigo.Tipo == "67" ? ObtenerComisionRemitente(listDeTerceros, operacion.Autorizacion) : (operacion.Codigo.Tipo == "65") ? ObtenerComisionDestinatario(listATerceros, operacion.Autorizacion) : (operacion.Codigo.Tipo == "1" ? ObtenerComisionDisposicion(listaCargosDisp, operacion.Autorizacion) : 0),
                        Incremento = GetIncrementoDeOper(operacion),
                        Concepto = operacion.Codigo.Tipo == "67" ? ObtenerConceptoRemitente(listDeTerceros, operacion.Autorizacion, Convert.ToDateTime(operacion.Momento)) : (operacion.Codigo.Tipo == "65") ? ObtenerConceptoDestinatario(listATerceros, operacion.Autorizacion, Convert.ToDateTime(operacion.Momento)) : ""
                    });
                /*
                response.Movimientos.Add(new MovimientoOnline
                {
                    Cargo = Decimal.Parse(operacion.Montos.Original.Importe),
                    Descripcion = (operacion.Codigo.Tipo == "35" || (operacion.Codigo.Tipo == "33" && operacion.Codigo.SubTipo != "2")) ? ("SU PAGO - GRACIAS")
                    : (operacion.Codigo.Tipo == "257" ? "CAMBIO DE NIP"
                    : (operacion.Comercio.Codigo == "999999999001845" ? operacion.Codigo.Descripcion
                    : ((operacion.Codigo.Tipo == "1027" && operacion.Codigo.SubTipo == "0") ? "SU PAGO - GRACIAS"
                    : (operacion.Comercio.Codigo.StartsWith("999999") ? GetNombreComercioPorCodigo(operacion.Comercio.Codigo)
                    : operacion.Comercio.Descripcion)))),
                    Fecha = Convert.ToDateTime(operacion.Momento),
                    ImpOriginal = operacion.Montos.Original.Importe,
                    MonedaOriginal = operacion.Montos.Original.Moneda.Descripcion,
                    NumAutorizacion = operacion.Autorizacion,
                    NumTarjeta = operacion.Tarjeta
                });*/
            }
            response.Success = true;
            response.UserResponse = "Éxito";
            response.PaginaActual = pagina;
            response.Paginas = Convert.ToInt32(consulta.ConsultarPorCuentaResponse.Listado.Referencia.Paginas.Total);
            return response;
        }

        /// <summary>
        /// Implementación para busqueda de movimientos por tipo
        /// </summary>
        /// <param name="numCuenta">Numero de cuenta</param>
        /// <param name="fechaInicio">Fecha Inicial</param>
        /// <param name="fechaFin">Fecha Final</param>
        /// <param name="pagina">Número de pagina</param>
        /// <param name="email">Correo Electronico</param>
        /// <param name="tipo">Tipo, valores 1:Movimientos, 2:Pagos, 3 Tranferencias</param>
        /// <returns></returns>
        [WebMethod]
        public MovimientoOnlineResponse GetMovimientoOnlineBroxelPorTipo(String numCuenta, DateTime fechaInicio, DateTime fechaFin, int pagina, string email, int tipo)
        {
            switch (tipo)
            {
                case 0:
                    fechaInicio = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"), "yyyy-MM-dd HH:mm:ss", null);
                    fechaFin = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"), "yyyy-MM-dd HH:mm:ss", null);
                    return GetMovimientosSinPagos(numCuenta, fechaInicio, fechaFin, pagina, email);
                case 1:
                    return GetMovimientosSinPagos(numCuenta, fechaInicio, fechaFin, pagina, email);
                case 2:
                case 3:
                    return GetPagosTransferencias(numCuenta, fechaInicio, fechaFin, pagina, tipo);
                default:
                    return new MovimientoOnlineResponse { Success = false, UserResponse = "El tipo de consulta solicitado no existe" };
            }
        }

        [WebMethod]
        public MovimientoOnlineResponse GetMovimientosConRango(String numCuenta, DateTime fechaDesde, DateTime fechaHasta)
        {
            var consulta = ConsultaPorCuenta(numCuenta, "", fechaDesde, fechaHasta);
            MovimientoOnlineResponse response = new MovimientoOnlineResponse();
            var lo = consulta.ConsultarPorCuentaResponse.Listado;
            if (consulta.ConsultarPorCuentaResponse.Response.Codigo != "00")
            {
                response.Success = false;
                response.UserResponse = consulta.ConsultarPorCuentaResponse.Response.Descripcion;
                return response;
            }

            foreach (
                var operacion in
                    lo.Operaciones.Where(
                        x => x.Codigo.Tipo != "129" && (x.Codigo.Tipo != "35" || x.Codigo.SubTipo != "3") && x.Respuesta.Codigo == "00" && x.Neutralizada == "N"))
            {
                if (!(operacion.Montos.Original.Importe.Contains("4000") && operacion.Cuenta == "522611666" && operacion.Autorizacion.Contains("023341")))
                    response.Movimientos.Add(new MovimientoOnline
                    {
                        Cargo = Decimal.Parse(operacion.Montos.Convertido.Importe),
                        Descripcion = (operacion.Codigo.Tipo == "35" || (operacion.Codigo.Tipo == "33" && operacion.Codigo.SubTipo != "2")) ? ("SU PAGO - GRACIAS")
                        : (operacion.Codigo.Tipo == "257" ? "CAMBIO DE NIP"
                        : (operacion.Comercio.Codigo == "999999999001845" ? operacion.Codigo.Descripcion
                        : ((operacion.Codigo.Tipo == "1027" && operacion.Codigo.SubTipo == "0") ? "SU PAGO - GRACIAS"
                        : (operacion.Comercio.Codigo.StartsWith("999999") ? GetNombreComercioPorCodigo(operacion.Comercio.Codigo)
                        : operacion.Comercio.Descripcion)))) + (operacion.Montos.Original.Moneda.Codigo == mxCurrency ? "" : " " +
                        operacion.Montos.Original.Importe + " " + operacion.Montos.Original.Moneda.Descripcion),
                        Fecha = Convert.ToDateTime(operacion.Momento),
                        ImpOriginal = operacion.Montos.Original.Importe,
                        MonedaOriginal = operacion.Montos.Original.Moneda.Descripcion,
                        NumAutorizacion = operacion.Autorizacion,
                        NumTarjeta = operacion.Tarjeta,
                    });
            }
            response.Success = true;
            response.UserResponse = "Éxito";
            return response;
        }

        [WebMethod]
        public MovimientosResponse GetMovimientosCuentaPorPagina(String cuenta, String nombreSolicitante, int pagina)
        {
            MovimientosRequest request = new MovimientosRequest
            {
                Solicitante = nombreSolicitante,
                Accion = "Get5Movimientos",
                NumCuenta = cuenta,
            };
            var consulta = ConsultaPorCuenta(cuenta, nombreSolicitante, DateTime.Now.AddMonths(-6), DateTime.Now, pagina, pagina);

            MovimientosResponse response = new MovimientosResponse();
            var lo = consulta.ConsultarPorCuentaResponse.Listado;
            if (consulta.ConsultarPorCuentaResponse.Response.Codigo != "00")
            {
                response.Success = 0;
                response.UserResponse = consulta.ConsultarPorCuentaResponse.Response.Descripcion;
                return response;
            }

            if (lo.Operaciones == null) return response;
            foreach (var operacion in lo.Operaciones.Where(x => x.Neutralizada == "N").OrderByDescending(x => Convert.ToDateTime(x.Momento)))
            {
                if (!(operacion.Montos.Original.Importe.Contains("4000") && operacion.Cuenta == "522611666" && operacion.Autorizacion.Contains("023341")))
                    response.Movimientos.Add(new App_Code.Movimiento
                    {
                        Tarjeta = operacion.Tarjeta,
                        Aprobada = operacion.Respuesta.Codigo == "00",
                        Comercio = (operacion.Codigo.Tipo == "35" || (operacion.Codigo.Tipo == "33" && operacion.Codigo.SubTipo != "2")) ? ("SU PAGO - GRACIAS")
                        : (operacion.Codigo.Tipo == "257" ? "CAMBIO DE NIP"
                        : (operacion.Comercio.Codigo == "999999999001845" ? operacion.Codigo.Descripcion
                        : ((operacion.Codigo.Tipo == "1027" && operacion.Codigo.SubTipo == "0") ? "SU PAGO - GRACIAS"
                        : (operacion.Comercio.Codigo.StartsWith("999999") ? GetNombreComercioPorCodigo(operacion.Comercio.Codigo)
                        : operacion.Comercio.Descripcion)))),
                        Fecha = operacion.Momento,
                        Monto = (operacion.Codigo.Tipo == "35" || operacion.Codigo.Tipo == "33" || operacion.Codigo.Tipo == "1027") ? (Convert.ToDouble(operacion.Montos.Original.Importe) * -1).ToString() : operacion.Montos.Original.Importe,
                        Moneda = operacion.Montos.Original.Moneda.Descripcion,
                        MensajeRespuesta = operacion.Respuesta.Descripcion,
                    });
            }
            response.UserResponse = "OK";
            return response;
        }

        [WebMethod]
        public MovimientosResponse Get5UltimosMovimientos(String numeroTarjeta, String nombreSolicitante)
        {
            MovimientosRequest request = new MovimientosRequest
            {
                Tarjeta = new Tarjeta(null, numeroTarjeta, null, null),
                Solicitante = nombreSolicitante,
                Accion = "Get5Movimientos"
            };
            return GetMovimientos(request, 0);
        }

        [WebMethod]
        public MovimientosResponse Get5UltimosMovimientosCuenta(String cuenta, String nombreSolicitante)
        {
            MovimientosRequest request = new MovimientosRequest
            {
                Solicitante = nombreSolicitante,
                Accion = "Get5Movimientos",
                NumCuenta = cuenta,
            };
            return GetMovimientos(request, 0);
        }

        [WebMethod]
        public MovimientosResponse GetUltimaPagMovimientosCuenta(String cuenta, String nombreSolicitante)
        {
            MovimientosRequest request = new MovimientosRequest
            {
                Solicitante = nombreSolicitante,
                Accion = "Get5Movimientos",
                NumCuenta = cuenta,
            };
            return GetMovimientos(request, 0);
        }


        [WebMethod]
        public MovimientosResponse Get5UltimosMovimientosPorCuentaYTerminacion(String numeroDeCuenta, String terminacion, Int32 idUsuario)
        {
            broxelco_rdgEntities _brmy = new broxelco_rdgEntities();
            String numTarjeta = "";
            try
            {
                var registros = _brmy.registri_broxel.Where(x => x.NrucO == numeroDeCuenta).ToList();
                if (registros.Count != 1)
                {
                    return new MovimientosResponse { CodigoRespuesta = 994, UserResponse = "NO SE PUDO IDENTIFICAR LA TARJETA" };
                }
                var registri = registros[0];
                try
                {
                    var maquila = _brmy.maquila.Where(
                        x => x.num_cuenta == numeroDeCuenta && x.nro_tarjeta.Substring(13, 4) == terminacion)
                        .OrderBy(x => x.id)
                        .ToList()
                        .Last();
                    numTarjeta = maquila.nro_tarjeta.Substring(0, 6);
                    numTarjeta += registri.folio_de_registro.Substring(5);
                    numTarjeta += maquila.nro_tarjeta.Substring(13);
                }
                catch (Exception ex)
                {
                    try
                    {
                        var registro = _brmy.registro_tc.Where(x => x.id.ToString() == registri.id_de_registro.ToString()).OrderBy(x => x.id).ToList().Last();
                        numTarjeta = registro.numero_tc.Substring(0, 6);
                        numTarjeta += registri.folio_de_registro.Substring(5);
                        numTarjeta += registro.numero_tc.Substring(13);
                    }
                    catch (Exception ex2)
                    {
                        try
                        {
                            var registro = _brmy.registro_tc.Where(
                               x => x.numero_de_cuenta == numeroDeCuenta && x.numero_tc.Substring(13, 4) == terminacion)
                               .OrderBy(x => x.id)
                               .ToList()
                               .Last();
                            numTarjeta = registro.numero_tc.Substring(0, 6);
                            numTarjeta += registri.folio_de_registro.Substring(5);
                            numTarjeta += registro.numero_tc.Substring(13);
                        }
                        catch (Exception ex3)
                        {
                            return new MovimientosResponse { CodigoRespuesta = 995, UserResponse = "NO SE ENCONTRARON DATOS DE LA TARJETA" };
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return new MovimientosResponse { CodigoRespuesta = 995, UserResponse = "NO SE ENCONTRARON DATOS DE LA TARJETA" };
            }
            MovimientosRequest request = new MovimientosRequest
            {
                Tarjeta = new Tarjeta("", numTarjeta, "", ""),
                UserID = idUsuario,
                Accion = "Get5Movimientos"
            };
            return GetMovimientos(request, 0);
        }

        #endregion

        #endregion
        private MovimientoOnlineResponse GetMovimientosSinPagos(String numCuenta, DateTime fechaInicio, DateTime fechaFin, int pagina, string email)
        {
            /// Query que trae las transferencias

            var conn = new MySqlDataAccess();
            var listDeTerceros = conn.ObtenerTransferenciasDeTerceros(numCuenta, fechaInicio, fechaFin);
            var listATerceros = conn.ObtenerTransferenciasATerceros(numCuenta, fechaInicio, fechaFin);
            var listaCargosDisp = conn.ObtenerCargosDisposiciones(numCuenta, fechaInicio, fechaFin);

            var consulta = ConsultaPorCuenta(numCuenta, email, fechaInicio, fechaFin, pagina, pagina);
            var response = new MovimientoOnlineResponse();
            var lo = consulta.ConsultarPorCuentaResponse.Listado;
            if (consulta.ConsultarPorCuentaResponse.Response.Codigo != "00")
            {
                response.Success = false;
                response.UserResponse = consulta.ConsultarPorCuentaResponse.Response.Descripcion;
                return response;
            }

            foreach (
                var operacion in
                    lo.Operaciones.Where(
                        x =>
                            x.Codigo.Tipo != "129" && (x.Codigo.Tipo != "35" || x.Codigo.SubTipo != "3") &&
                            x.Respuesta.Codigo == "00" && x.Neutralizada == "N" && x.Codigo.Tipo != "1027"))
            {
                if (!(operacion.Montos.Original.Importe.Contains("4000") && operacion.Cuenta == "522611666" && operacion.Autorizacion.Contains("023341")))
                    response.Movimientos.Add(new MovimientoOnline
                        {

                            Cargo = operacion.Montos.Original.Moneda.Codigo == mxCurrency ? Decimal.Parse(operacion.Montos.Original.Importe) : Decimal.Parse(operacion.Montos.Convertido.Importe),
                            Descripcion = (operacion.Codigo.Tipo == "35" || (operacion.Codigo.Tipo == "33" && operacion.Codigo.SubTipo != "2")) ? ("SU PAGO - GRACIAS")
                            : (operacion.Codigo.Tipo == "257" ? "CAMBIO DE NIP"
                            : (operacion.Comercio.Codigo == "999999999001845" ? operacion.Codigo.Descripcion
                            : (operacion.Comercio.Codigo.StartsWith("999999") ? GetNombreComercioPorCodigo(operacion.Comercio.Codigo)
                            : operacion.Comercio.Descripcion))) + (operacion.Montos.Original.Moneda.Codigo == mxCurrency ? "" : " " +
                            operacion.Montos.Original.Importe + " " + operacion.Montos.Original.Moneda.Descripcion),
                            Fecha = Convert.ToDateTime(operacion.Momento),
                            ImpOriginal = operacion.Montos.Original.Importe,
                            MonedaOriginal = GetMonedaAbr(operacion.Montos.Original.Moneda.Codigo),
                            NumAutorizacion = operacion.Autorizacion,
                            NumTarjeta = operacion.Tarjeta,
                            Destinatario = operacion.Codigo.Tipo == "67" ? ObtenerTitularTransferenciaRemitente(listDeTerceros, operacion.Autorizacion, false, Convert.ToDateTime(operacion.Momento)) : (operacion.Codigo.Tipo == "65") ? ObtenerTitutarTransferenciaDestinatario(listATerceros, operacion.Autorizacion, true, Convert.ToDateTime(operacion.Momento)) : "",
                            Remitente = operacion.Codigo.Tipo == "67" ? ObtenerTitularTransferenciaRemitente(listDeTerceros, operacion.Autorizacion, true, Convert.ToDateTime(operacion.Momento)) : (operacion.Codigo.Tipo == "65") ? ObtenerTitutarTransferenciaDestinatario(listATerceros, operacion.Autorizacion, false, Convert.ToDateTime(operacion.Momento)) : "",
                            Comision = operacion.Codigo.Tipo == "67" ? ObtenerComisionRemitente(listDeTerceros, operacion.Autorizacion) : (operacion.Codigo.Tipo == "65") ? ObtenerComisionDestinatario(listATerceros, operacion.Autorizacion) : (operacion.Codigo.Tipo == "1" ? ObtenerComisionDisposicion(listaCargosDisp, operacion.Autorizacion) : 0),
                            Incremento = GetIncrementoDeOper(operacion)
                        });
            }
            response.Success = true;
            response.UserResponse = "Éxito";
            response.PaginaActual = pagina;
            response.Paginas = Convert.ToInt32(consulta.ConsultarPorCuentaResponse.Listado.Referencia.Paginas.Total);
            return response;
        }

        private MovimientoOnlineResponse GetPagosTransferencias(String numCuenta, DateTime fechaInicio, DateTime fechaFin, int pagina, int tipo)
        {
            var response = new MovimientoOnlineResponse();
            try
            {
                var movs = new GenericBL().GetPagoTransferencias(numCuenta, fechaInicio, fechaFin, tipo);
                if (movs != null)
                {
                    foreach (var mov in movs)
                    {
                        mov.MonedaOriginal = GetMonedaAbr(mov.MonedaOriginal);
                    }
                    response.Movimientos = movs;
                    response.Success = true;
                    response.UserResponse = "Éxito";
                    response.PaginaActual = 1;
                    response.Paginas = 1;
                    return response;
                }
                response.Success = false;
                response.UserResponse = "No existen movimientos para los criterios utilizados, por favor reintente con otros criterios.";
                return response;
            }
            catch (Exception e)
            {
                response.Success = false;
                var strTipo = (tipo == 2) ? "pagos" : "transferencias";
                response.UserResponse = "Error al obtener los " + strTipo + ": " + e.Message;
                return response;
            }
        }

       

       

       

        /// <summary>
        /// Realiza un cargo a una cuenta determinada dependiendo si la cuenta tiene saldo suficiente.
        /// </summary>
        /// <param name="pNumCuenta">Número de cuenta.</param>
        /// <param name="pMontoDeduccion">Monto deduccion.</param>
        /// <param name="pNumReferencia">Numero de referencia</param>
        /// <param name="pNombreReferencia">Nombre de referencia</param>
        /// <param name="pIdUser">Identificador del usuario del cargo.</param>
        /// <param name="pIdComercio">Identificador del comercio que aparecerá en el cargo.</param>
        /// <param name="pSolicitante">Solicitante de la activación de la cuenta</param>
        /// <returns>Objeto tipo CargoResponse</returns>
        [WebMethod(MessageName = "RealizaCargoPrincipal")]
        public CargoResponse RealizaCargo(String pNumCuenta, Decimal pMontoDeduccion, String pNumReferencia, String pNombreReferencia, Int32 pIdUser, Int32 pIdComercio, String pSolicitante)
        {
            try
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                " Inicio RealizaCargo: Parametros: pNumCuenta:" + pNumCuenta + " pMontoDeduccion: " +
                                pMontoDeduccion + " pNumReferencia: " + pNumReferencia + " pNombreReferencia: " +
                                pNombreReferencia + " pIdUser: " + pIdUser + " pIdComercio: " + pIdComercio +
                                " pSolicitante: " + pSolicitante);



                //Se realiza la consulta de saldo
                var ip = "Unknown";
                try
                {
                    if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                    {
                        ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }

                }
                catch (Exception)
                {
                    ip = "InternalReq";
                }

                var request = new OperarCuentaRequest { NumCuenta = pNumCuenta, Solicitante = pIdUser.ToString(CultureInfo.InvariantCulture), Accion = "GetSaldoPorCuenta|Caller: " + "" + "|ip: " + ip };
                var response = GetSaldos(request);

                if (response.Success != 1)
                {
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                    " RealizaCargo Error: No se obtuvo la información del saldo");
                    return new CargoResponse
                    {
                        CodigoRespuesta = 999,
                        FechaCreacion = DateTime.Now.ToString("s"),
                        Success = 0,
                        UserResponse = "Error: No se obtuvo la información del saldo de la cuenta",
                        Saldo = 0
                    };
                }

                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                " RealizaCargo: Saldo Response: " + SerializeToXml.SerializeObject(response));


                //Se obtiene la respectiva tarjeta en base a la cuenta
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                "RealizaCargo: Helper.GetTarjetaFromCuenta(pNumCuenta)");

                var tarjeta = Helper.GetTarjetaFromCuenta(pNumCuenta);

                if (tarjeta != null)
                {
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                    "RealizaCargo: Tarjeta asociada a la cuenta" +
                                    SerializeToXml.SerializeObject(tarjeta));


                    //Se valida si la cuenta está bloqueada, si es así se desbloquea
                    var ctaBloqueada = !response.EstadoOperativo.ToLower().Equals("operativa");

                    ////Se obtiene el estado de la tarjeta
                    var tarjetaTrunca = tarjeta.NumeroTarjeta;

                    //Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                    //                "RealizaCargo: TruncaTarjeta: " + tarjetaTrunca);


                    var estadoTarjeta = new TcControlBL().ConsultaEstado(-500, tarjeta.NumeroTarjeta, pNumCuenta);

                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                    "RealizaCargo: estadoTarjeta: " + SerializeToXml.SerializeObject(estadoTarjeta));

                    if (estadoTarjeta.Success.Equals(1))
                    {

                        var tjBloqueada = !estadoTarjeta.EstadoOperativo.ToLower().Equals("operativa");
                        var estadoTarjetaAnterior = estadoTarjeta.CodigoEstadoOperativo;

                        //Si la cuenta o la tarjeta están bloqueadas
                        if (ctaBloqueada || tjBloqueada)
                        {
                            //Se verifica si la cuenta está bloqueada para desbloquearla
                            if (ctaBloqueada)
                            {
                                var resultActivacion = ActivacionDeCuenta(pNumCuenta, pSolicitante);

                                if (resultActivacion.Success.Equals(0))
                                {
                                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                                    "RealizaCargo: Error: No se pudo activar la cuenta ");

                                    return new CargoResponse
                                    {
                                        CodigoRespuesta = 999,
                                        FechaCreacion = DateTime.Now.ToString("s"),
                                        Success = 0,
                                        UserResponse = "Error: No se pudo realizar la activación de la cuenta",
                                        Saldo = 0
                                    };
                                }

                            }

                            //Se valida si la tarjeta es la bloqueada
                            if (tjBloqueada)
                            {
                                //Se realiza la activación de la tarjeta
                                var activacionTarjeta = new TcControlBL().CambiarEstado(-500, tarjetaTrunca, pNumCuenta,
                                    EstadosTarjetas.Operativa);

                                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                                "RealizaCargo: Resultado de la activación de la tarjeta: " +
                                                SerializeToXml.SerializeObject(activacionTarjeta));

                                if (activacionTarjeta.Success.Equals(0))
                                {
                                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                                    "RealizaCargo: Error: No se pudo activar la tarjeta");

                                    return new CargoResponse
                                    {
                                        CodigoRespuesta = 999,
                                        FechaCreacion = DateTime.Now.ToString("s"),
                                        Success = 0,
                                        UserResponse = "Error: No se pudo realizar la activación de la tarjeta",
                                        Saldo = 0
                                    };
                                }


                            }

                        }

                        CargoResponse resultCargo;

                        //Se valida el saldo versus el cargo a realizar
                        if (response.Saldos.DisponibleCompras >= pMontoDeduccion)
                        {
                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            " RealizaCargo: Saldo Mayor a Deducción");

                            resultCargo = SetCargoConCuenta(pMontoDeduccion, tarjeta.NombreTarjeta,
                                tarjeta.NumeroTarjeta,
                                tarjeta.Cvc2,
                                tarjeta.FechaExpira, pNumReferencia, pNombreReferencia, pIdUser, pIdComercio, pNumCuenta);

                            resultCargo.Saldo = pMontoDeduccion;

                        }
                        else
                        {


                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            " RealizaCargo: Saldo Menor a Deducción: Saldos.DisponibleCompras: " +
                                            response.Saldos.DisponibleCompras);

                            resultCargo = SetCargoConCuenta(response.Saldos.DisponibleCompras, tarjeta.NombreTarjeta,
                                tarjeta.NumeroTarjeta, tarjeta.Cvc2,
                                tarjeta.FechaExpira, pNumReferencia, pNombreReferencia, pIdUser, pIdComercio, pNumCuenta);

                            resultCargo.Saldo = response.Saldos.DisponibleCompras;
                        }

                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                        " RealizaCargo: resultCargo: " + SerializeToXml.SerializeObject(resultCargo));

                        //Se bloquea la cuenta en caso de que haya estado bloqueada
                        if (ctaBloqueada)
                        {
                            //Se realiza la activación de la cuenta
                            var resultBloqueo = BloqueoDeCuenta(pNumCuenta, pNombreReferencia);

                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            "RealizaCargo: Resultado del bloqueo de la cuenta: " +
                                            SerializeToXml.SerializeObject(resultBloqueo));
                        }

                        //Se cambia el estado de la tarjeta al estado anterior
                        if (tjBloqueada)
                        {

                            var cambioEdoTarjeta = new TcControlBL().CambiarEstado(-500, tarjetaTrunca, pNumCuenta,
                                estadoTarjetaAnterior);
                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            "RealizaCargo: Resultado del cambio de estado de la tarjeta: " +
                                            SerializeToXml.SerializeObject(cambioEdoTarjeta));

                        }

                        return resultCargo;
                    }
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                "RealizaCargo: estadoTarjeta.Success" + estadoTarjeta.Success);
                }
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                "RealizaCargo: No se encontró la información de la tarjeta");

                return new CargoResponse
                {
                    CodigoRespuesta = 999,
                    FechaCreacion = DateTime.Now.ToString("s"),
                    Success = 0,
                    UserResponse = "Error: No se obtuvo la información de la cuenta/tarjeta",
                    Saldo = 0
                };

            }
            catch (Exception exception)
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + " RealizaCargo Error: " + exception.Source + " Message: " + exception.Message + " - InnerException: " + (null != exception.InnerException ? exception.InnerException.StackTrace : string.Empty));
                return new CargoResponse
                {
                    CodigoRespuesta = 999,
                    FechaCreacion = DateTime.Now.ToString("s"),
                    Success = 0,
                    UserResponse = "Error: Ocurrió un error al realizar el cargo de esa cuenta",
                    Saldo = 0
                };
            }
        }

        /// <summary>
        /// Realiza un cargo a una cuenta determinada dependiendo si la cuenta tiene saldo suficiente.
        /// </summary>
        /// <param name="pNumCuenta">Número de cuenta.</param>
        /// <param name="pMontoDeduccion">Monto deduccion.</param>
        /// <param name="pNumReferencia">Numero de referencia</param>
        /// <param name="pNombreReferencia">Nombre de referencia</param>
        /// <param name="pIdUser">Identificador del usuario del cargo.</param>
        /// <param name="pIdComercio">Identificador del comercio que aparecerá en el cargo.</param>
        /// <param name="pSolicitante">Solicitante de la activación de la cuenta</param>
        /// <param name="_saldoMayorADeduccion">Bandera que valida si el saldo DisponibleCompras es mayor o igual al monto deducción para realizar el pago o no</param>
        /// <returns>Objeto tipo CargoResponse</returns>
        [WebMethod(MessageName = "SobrecargaValidaSaldoMayorADeduccion")]
        public CargoResponse RealizaCargo(String pNumCuenta, Decimal pMontoDeduccion, String pNumReferencia, String pNombreReferencia, Int32 pIdUser, Int32 pIdComercio, String pSolicitante, bool _saldoMayorADeduccion)
        {
            try
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                " Inicio RealizaCargo: Parametros: pNumCuenta:" + pNumCuenta + " pMontoDeduccion: " +
                                pMontoDeduccion + " pNumReferencia: " + pNumReferencia + " pNombreReferencia: " +
                                pNombreReferencia + " pIdUser: " + pIdUser + " pIdComercio: " + pIdComercio +
                                " pSolicitante: " + pSolicitante);
                //Se realiza la consulta de saldo
                var ip = "Unknown";
                try
                {
                    if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                    {
                        ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                }
                catch (Exception)
                {
                    ip = "InternalReq";
                }
                var request = new OperarCuentaRequest { NumCuenta = pNumCuenta, Solicitante = pIdUser.ToString(CultureInfo.InvariantCulture), Accion = "GetSaldoPorCuenta|Caller: " + "" + "|ip: " + ip };
                var response = GetSaldos(request);
                if (response.Success != 1)
                {
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                    " RealizaCargo Error: No se obtuvo la información del saldo");
                    return new CargoResponse
                    {
                        CodigoRespuesta = 999,
                        FechaCreacion = DateTime.Now.ToString("s"),
                        Success = 0,
                        UserResponse = "Error: No se obtuvo la información del saldo de la cuenta",
                        Saldo = 0
                    };
                }

                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                " RealizaCargo: Saldo Response: " + SerializeToXml.SerializeObject(response));
                //Se obtiene la respectiva tarjeta en base a la cuenta
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                "RealizaCargo: Helper.GetTarjetaFromCuenta(pNumCuenta)");
                var tarjeta = Helper.GetTarjetaFromCuenta(pNumCuenta);
                if (tarjeta != null)
                {
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                    "RealizaCargo: Tarjeta asociada a la cuenta" +
                                    SerializeToXml.SerializeObject(tarjeta));
                    //Se valida si la cuenta está bloqueada, si es así se desbloquea
                    var ctaBloqueada = !response.EstadoOperativo.ToLower().Equals("operativa");
                    ////Se obtiene el estado de la tarjeta
                    var tarjetaTrunca = tarjeta.NumeroTarjeta;
                    var estadoTarjeta = new TcControlBL().ConsultaEstado(-500, tarjeta.NumeroTarjeta, pNumCuenta);
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                    "RealizaCargo: estadoTarjeta: " + SerializeToXml.SerializeObject(estadoTarjeta));
                    if (estadoTarjeta.Success.Equals(1))
                    {
                        var tjBloqueada = !estadoTarjeta.EstadoOperativo.ToLower().Equals("operativa");
                        var estadoTarjetaAnterior = estadoTarjeta.CodigoEstadoOperativo;
                        //Si la cuenta o la tarjeta están bloqueadas
                        if (ctaBloqueada || tjBloqueada)
                        {
                            //Se verifica si la cuenta está bloqueada para desbloquearla
                            if (ctaBloqueada)
                            {
                                var resultActivacion = ActivacionDeCuenta(pNumCuenta, pSolicitante);
                                if (resultActivacion.Success.Equals(0))
                                {
                                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                                    "RealizaCargo: Error: No se pudo activar la cuenta ");
                                    return new CargoResponse
                                    {
                                        CodigoRespuesta = 999,
                                        FechaCreacion = DateTime.Now.ToString("s"),
                                        Success = 0,
                                        UserResponse = "Error: No se pudo realizar la activación de la cuenta",
                                        Saldo = 0
                                    };
                                }
                            }
                            //Se valida si la tarjeta es la bloqueada
                            if (tjBloqueada)
                            {
                                //Se realiza la activación de la tarjeta
                                var activacionTarjeta = new TcControlBL().CambiarEstado(-500, tarjetaTrunca, pNumCuenta,
                                    EstadosTarjetas.Operativa);
                                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                                "RealizaCargo: Resultado de la activación de la tarjeta: " +
                                                SerializeToXml.SerializeObject(activacionTarjeta));
                                if (activacionTarjeta.Success.Equals(0))
                                {
                                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                                    "RealizaCargo: Error: No se pudo activar la tarjeta");
                                    return new CargoResponse
                                    {
                                        CodigoRespuesta = 999,
                                        FechaCreacion = DateTime.Now.ToString("s"),
                                        Success = 0,
                                        UserResponse = "Error: No se pudo realizar la activación de la tarjeta",
                                        Saldo = 0
                                    };
                                }
                            }
                        }
                        CargoResponse resultCargo;
                        //Se valida el saldo versus el cargo a realizar
                        if (response.Saldos.DisponibleCompras >= pMontoDeduccion)
                        {
                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            " RealizaCargo: Saldo Mayor a Deducción");
                            resultCargo = SetCargoConCuenta(pMontoDeduccion, tarjeta.NombreTarjeta,
                                tarjeta.NumeroTarjeta,
                                tarjeta.Cvc2,
                                tarjeta.FechaExpira, pNumReferencia, pNombreReferencia, pIdUser, pIdComercio, pNumCuenta);
                            resultCargo.Saldo = pMontoDeduccion;
                        }
                        else
                        {
                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            " RealizaCargo: Saldo Menor a Deducción: Saldo insuficiente: " +
                                            response.Saldos.DisponibleCompras);
                            resultCargo = new CargoResponse
                            {
                                CodigoRespuesta = 999,
                                FechaCreacion = DateTime.Now.ToString("s"),
                                Success = 0,
                                UserResponse = "Error: No cuenta con el saldo suficiente para realizar el pago.",
                                Saldo = response.Saldos.DisponibleCompras
                            };
                        }
                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                        " RealizaCargo: resultCargo: " + SerializeToXml.SerializeObject(resultCargo));
                        //Se bloquea la cuenta en caso de que haya estado bloqueada
                        if (ctaBloqueada)
                        {
                            //Se realiza la activación de la cuenta
                            var resultBloqueo = BloqueoDeCuenta(pNumCuenta, pNombreReferencia);
                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            "RealizaCargo: Resultado del bloqueo de la cuenta: " +
                                            SerializeToXml.SerializeObject(resultBloqueo));
                        }
                        //Se cambia el estado de la tarjeta al estado anterior
                        if (tjBloqueada)
                        {
                            var cambioEdoTarjeta = new TcControlBL().CambiarEstado(-500, tarjetaTrunca, pNumCuenta,
                                estadoTarjetaAnterior);
                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            "RealizaCargo: Resultado del cambio de estado de la tarjeta: " +
                                            SerializeToXml.SerializeObject(cambioEdoTarjeta));
                        }
                        return resultCargo;
                    }
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                "RealizaCargo: estadoTarjeta.Success" + estadoTarjeta.Success);
                }
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                "RealizaCargo: No se encontró la información de la tarjeta");
                return new CargoResponse
                {
                    CodigoRespuesta = 999,
                    FechaCreacion = DateTime.Now.ToString("s"),
                    Success = 0,
                    UserResponse = "Error: No se obtuvo la información de la cuenta/tarjeta",
                    Saldo = 0
                };
            }
            catch (Exception exception)
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + " RealizaCargo Error: " + exception.Source + " Message: " + exception.Message + " - InnerException: " + (null != exception.InnerException ? exception.InnerException.StackTrace : string.Empty));
                return new CargoResponse
                {
                    CodigoRespuesta = 999,
                    FechaCreacion = DateTime.Now.ToString("s"),
                    Success = 0,
                    UserResponse = "Error: Ocurrió un error al realizar el cargo de esa cuenta",
                    Saldo = 0
                };
            }
        }

        [WebMethod]
        public void ReenviarCorreoDispersio(string folioDispersion)
        {
            using (var ctx = new broxelco_rdgEntities())
            {
                var solicitudDispersion = ctx.dispersionesSolicitudes.FirstOrDefault(x => x.folio == folioDispersion);

                if (solicitudDispersion == null)
                    return;

                decimal antesPOS = 0, despuesPOS = 0, antesATM = 0, despuesATM = 0, posSolicitado = 0, atmSolicitado = 0;
                var errores = new List<ErroresDispersion>();


                var dispersionesInternas =
                    ctx.dispersionesInternas.Where(x => x.idSolicitud == folioDispersion && (x.incrementoPOS > 0 || x.incrementoATM > 0)).ToList();
                if (dispersionesInternas.Count >= 1)
                {
                    antesPOS = (decimal)(dispersionesInternas.Sum(x => (x.antesPOS == null) ? 0 : x.antesPOS));
                    despuesPOS = (decimal)(dispersionesInternas.Sum(x => (x.despuesPOS == null) ? 0 : x.despuesPOS));
                    posSolicitado = (decimal)(dispersionesInternas.Sum(x => (x.incrementoPOS == null ? 0 : x.incrementoPOS)));
                    antesATM = (decimal)(dispersionesInternas.Sum(x => (x.antesATM == null) ? 0 : x.antesATM));
                    despuesATM = (decimal)(dispersionesInternas.Sum(x => (x.despuesATM == null) ? 0 : x.despuesATM));
                    atmSolicitado = (decimal)(dispersionesInternas.Sum(x => (x.incrementoATM == null ? 0 : x.incrementoATM)));
                }

                if (solicitudDispersion.estado == "CON ERRORES")
                {
                    var codigosRespuesta = ctx.CodigosRespuesta.ToList();
                    var codigos = dispersionesInternas.Select(x => x.codigoRespuestaPOS).Distinct();
                    codigos = codigos.Where(x => x != "-1").ToList();
                    foreach (var codig in codigos)
                    {
                        if (codig == "")
                            continue;
                        var codigo = Convert.ToInt32(codig);
                        var codigoDB = codigosRespuesta.FirstOrDefault(x => x.Id == codigo);
                        var e = new ErroresDispersion
                        {
                            CodigoRespuesta = codigo,
                            DescripcionCodigoResp = codigoDB != null ? codigoDB.Descripcion : "",
                            CausaComunCodigoResp = codigoDB != null ? codigoDB.CausaComun : ""
                        };
                        string codig1 = codig;
                        var cuentasConError = dispersionesInternas.Where(x => x.codigoRespuestaPOS == codig1).ToList();
                        foreach (var cuentaConError in cuentasConError)
                        {
                            e.CuentasConError.Add(cuentaConError.cuenta);
                        }
                        errores.Add(e);
                    }
                }
                Helper.EnviarMailDispersion(solicitudDispersion.email, folioDispersion, solicitudDispersion.tipo, despuesPOS - antesPOS,
                                            despuesATM - antesATM, solicitudDispersion.estado, dispersionesInternas.Count,
                                            solicitudDispersion.claveCliente, errores, posSolicitado, atmSolicitado);
            }

        }

        [WebMethod]
        public void EnviaAvisos(string numCuenta, decimal monto)
        {
            var mySql = new MySqlDataAccess();
            var idNivel = 1;
            var avisos = mySql.ObtieneNivelDeCuentaAvisos(idNivel, numCuenta);
            if (avisos.Count > 0)
                GenericBL.EnviaAvisosNivelCuenta(numCuenta, monto, avisos);
        }

        /// <summary>
        /// Método para obtener la información de la tarjeta.
        /// </summary>
        /// <param name="pNumCuenta">Identificador de la cuenta</param>
        /// <returns>Información de la tarjeta</returns>
        [WebMethod]
        public Tarjeta GetTarjeta(string pNumCuenta)
        {
            try
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                " Inicio GetTarjeta: Parametros: pNumCuenta:" + pNumCuenta);
                var tarjeta = Helper.GetTarjetaFromCuenta(pNumCuenta);
                if (tarjeta != null)
                {
                    return tarjeta;
                }
                else
                {
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                    "GetTarjeta: No se encontró la información de la tarjeta");
                    return null;
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + " GetTarjeta Error: " + exception.Source + " Message: " + exception.Message + " - InnerException: " + (null != exception.InnerException ? exception.InnerException.StackTrace : string.Empty));
                return null;
            }
        }
    }
}
