using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Net;
using ComCredencial.GenericBL;
using ComCredencial.newWsAutorizacion;
using ComCredencial.newWsCuenta;
using ComCredencial.newWsOperaciones;
using ComCredencial.RequestResponses;

using Autenticacion = ComCredencial.newWsCuenta.Autenticacion;
using ConsultarRequest = ComCredencial.newWsCuenta.ConsultarRequest;
using ConsultarRequest1 = ComCredencial.newWsCuenta.input3;
using BloquearRequest1 = ComCredencial.newWsCuenta.input2;
using DesbloquearRequest1 = ComCredencial.newWsCuenta.input1;
using Originador = ComCredencial.newWsCuenta.Originador;
using Autorizacion = ComCredencial.newWsAutorizacion;
using ConsultarPorCuentaRequest1 = ComCredencial.newWsOperaciones.input2;
using ConsultarPorCuentaResponse1 = ComCredencial.newWsOperaciones.output2;
using ConsultarPorTarjetaRequest1 = ComCredencial.newWsOperaciones.input3;
using ConsultarPorTarjetaResponse1 = ComCredencial.newWsOperaciones.output3;

namespace ComCredencial
{
    public class NewCredencial
    {
        private AutorizarRequest _autorizarReq;
        private AutorizarResponse _autorizarResp;

        #region NewCredencial

        #region autorizadorWs

        //1------------------------------------------------
        /// <summary>
        /// Actualiza el Saldo Límite
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public LimiteResponse ActualizarLimite(LimiteRequest request)
        {
            var ctx = new BroxelEntities();
            var response = new LimiteResponse();
            var currentDate = DateTime.Now;
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
                var param = ctx.Parametros.FirstOrDefault(x => x.Parametro == "TiempoDisp");

                Debug.Assert(param != null, "param != null");
                if (!anterior.Any() || DateTime.Now.Subtract(Convert.ToDateTime(anterior[0].FechaHoraCreacion)).TotalMinutes > Convert.ToInt32(param.Valor))
                {
                    var mov = new Movimiento
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

                        AutorizacionPortType autorizadorWs = new AutorizacionPortTypeClient();
                        var reqInt = new InspectorBehavior();
                        var handler = (AutorizacionPortTypeClient)autorizadorWs;
                        handler.Endpoint.Behaviors.Clear();
                        handler.Endpoint.Behaviors.Add(reqInt);
                        var dateTimeInicio = DateTime.Now;
                        _autorizarResp = autorizadorWs.Autorizar(_autorizarReq);
                        var dateTimeFin = DateTime.Now;
                        var reqXml = reqInt.LastRequestXml;
                        var resXml = reqInt.LastResponseXml;

                        try
                        {
                            ThreadPool.QueueUserWorkItem(delegate
                            {
                                new GenericBL.GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 2, 1,
                                    reqXml, resXml, mov.idMovimiento);
                            });
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
                        }

                        var codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                        var cr = ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
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

        // 2-----------------------------------------------
        public NIPResponse CambiarNip(NIPRequest request)
        {
            var ctx = new BroxelEntities();
            var response = new NIPResponse();
            if (request.NIPNuevo.Length != 4 | request.NIPNuevo.Length != 4)
                return new NIPResponse { CodigoRespuesta = 420, Success = 0 };
            var currentDate = DateTime.Now;
            var numCuenta = Helper.GetCuentaFromTarjeta(request.Tarjeta.NumeroTarjeta);
            var credencialReq = new AutorizarRequest
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
                NuevoPIN = Helper.CipherNip(request.NIPNuevo)
            };
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            AutorizacionPortType autorizadorWs = new AutorizacionPortTypeClient();
            var reqInt = new InspectorBehavior();
            var handler = (AutorizacionPortTypeClient)autorizadorWs;
            handler.Endpoint.Behaviors.Clear();
            handler.Endpoint.Behaviors.Add(reqInt);
            var dateTimeInicio = DateTime.Now;
            _autorizarResp = autorizadorWs.Autorizar(credencialReq);
            var dateTimeFin = DateTime.Now;
            var reqXml = reqInt.LastRequestXml;
            var resXml = reqInt.LastResponseXml;

            var codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
            response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
            var cr = ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
            if (cr != null)
                response.UserResponse = cr.Descripcion;
            if (_autorizarResp.CodigoRespuesta.Trim() == "-1")
            {
                response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
                response.Success = 1;
            }
            var mov = new Movimiento
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
                var DoNothing = true;
            }
            try
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    new GenericBL.GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, numCuenta, 5, 1,
                        reqXml, resXml, mov.idMovimiento);
                });
            }
            catch (Exception e)
            {
                Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
            }
            return response;
        }

        // 3---------------------------------------------
        public CargoDeleteResponse DeleteCargo(CargoEditRequest request)
        {
            var ctx = new BroxelEntities();
            var response = new CargoDeleteResponse();
            var cargo = ctx.Movimiento.FirstOrDefault(m => m.idMovimiento == request.IdCargo);
            if (cargo == null) return response;
            var anul = new AnulacionN
            {
                Fecha = DateTime.Now,
                idTransaccion = request.IdCargo,
                idUsuario = request.UserID,
                TipoTransaccion = 3,
            };
            ctx.AnulacionN.Add(anul);
            ctx.SaveChanges();
            cargo.idComercio = Convert.ToInt32(cargo.idComercio);
            var com = (from x in ctx.Comercio where x.idComercio == cargo.idComercio select x).First();
            _autorizarReq = new AutorizarRequest
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
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            AutorizacionPortType autorizadorWs = new AutorizacionPortTypeClient();
            var reqInt = new InspectorBehavior();
            var handler = (AutorizacionPortTypeClient)autorizadorWs;
            handler.Endpoint.Behaviors.Clear();
            handler.Endpoint.Behaviors.Add(reqInt);
            var dateTimeInicio = DateTime.Now;
            _autorizarResp = autorizadorWs.Autorizar(_autorizarReq);
            var dateTimeFin = DateTime.Now;
            var reqXml = reqInt.LastRequestXml;
            var resXml = reqInt.LastResponseXml;

            try
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    new GenericBL.GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 9, 1,
                        reqXml, resXml, 0, anul.idAnulacion);
                });
            }
            catch (Exception e)
            {
                Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
            }

            anul.Autorizado = _autorizarResp.CodigoRespuesta.Trim() == ("-1");
            anul.NumAutorizacion = Convert.ToInt32(_autorizarResp.CodigoAutorizacion);
            ctx.SaveChanges();
            var codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
            var cr = ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
            if (cr != null)
                response.UserResponse = cr.Descripcion;
            response.Success = anul.Autorizado == true ? 1 : 0;
            response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
            response.IdAnulacion = anul.idAnulacion;
            response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
            response.FechaCreacion = Convert.ToDateTime(anul.Fecha).ToString("yyyy-MM-dd HH:mm:ss");
            //GuardarBitacora(request, response);
            return response;
        }

        // 4---------------------------------------------
        public CargoDeleteResponse DeletePago(CargoEditRequest request)
        {
            var ctx = new BroxelEntities();
            var response = new CargoDeleteResponse();
            var cargo = ctx.Movimiento.FirstOrDefault(m => m.idMovimiento == request.IdCargo);
            if (cargo == null) return response;
            var anul = new AnulacionN
            {
                Fecha = DateTime.Now,
                idTransaccion = request.IdCargo,
                idUsuario = request.UserID,
                TipoTransaccion = 1043,
            };
            ctx.AnulacionN.Add(anul);
            ctx.SaveChanges();
            cargo.idComercio = Convert.ToInt32(cargo.idComercio);
            var com = (from x in ctx.Comercio where x.idComercio == cargo.idComercio select x).First();
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
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            AutorizacionPortType autorizadorWs = new AutorizacionPortTypeClient();
            var reqInt = new InspectorBehavior();
            var handler = (AutorizacionPortTypeClient)autorizadorWs;
            handler.Endpoint.Behaviors.Clear();
            handler.Endpoint.Behaviors.Add(reqInt);
            var dateTimeInicio = DateTime.Now;
            _autorizarResp = autorizadorWs.Autorizar(_autorizarReq);
            var dateTimeFin = DateTime.Now;
            var reqXml = reqInt.LastRequestXml;
            var resXml = reqInt.LastResponseXml;

            try
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    new GenericBL.GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 10, 1,
                        reqXml, resXml, 0, anul.idAnulacion);
                });
            }
            catch (Exception e)
            {
                Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
            }

            anul.Autorizado = _autorizarResp.CodigoRespuesta.Trim() == ("-1");
            anul.NumAutorizacion = Convert.ToInt32(_autorizarResp.CodigoAutorizacion);
            ctx.SaveChanges();
            var codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
            var cr = ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
            if (cr != null)
                response.UserResponse = cr.Descripcion;
            response.Success = anul.Autorizado == true ? 1 : 0;
            response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
            response.IdAnulacion = anul.idAnulacion;
            response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
            response.FechaCreacion = Convert.ToDateTime(anul.Fecha).ToString("yyyy-MM-dd HH:mm:ss");
            //GuardarBitacora(request, response);
            return response;
        }

        //5----------------------------------------------
        public LimiteResponse ReversoLimite(LimiteRequest request, Movimiento mov)
        {
            var ctx = new BroxelEntities();
            var response = new LimiteResponse();
            var currentDate = DateTime.Now;
            try
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

                    AutorizacionPortType autorizadorWs = new AutorizacionPortTypeClient();
                    var reqInt = new InspectorBehavior();
                    var handler = (AutorizacionPortTypeClient)autorizadorWs;
                    handler.Endpoint.Behaviors.Clear();
                    handler.Endpoint.Behaviors.Add(reqInt);
                    var dateTimeInicio = DateTime.Now;
                    _autorizarResp = autorizadorWs.Autorizar(_autorizarReq);
                    var dateTimeFin = DateTime.Now;
                    var reqXml = reqInt.LastRequestXml;
                    var resXml = reqInt.LastResponseXml;

                    try
                    {
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            new GenericBL.GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 12, 1,
                                reqXml, resXml, mov.idMovimiento, anul.idAnulacion);
                        });
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
                    }

                    var codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                    var cr = ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
                    if (cr != null) response.UserResponse = cr.Descripcion;
                    anul.NumAutorizacion = Convert.ToInt32(_autorizarResp.CodigoAutorizacion);
                    if (cr != null) anul.MensajeRespuesta = cr.Descripcion;
                    anul.Autorizado = _autorizarResp.CodigoRespuesta.Trim() == ("-1");
                    response.Success = anul.Autorizado == true ? 1 : 0;
                    response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
                    response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                    response.FechaCreacion = Convert.ToDateTime(anul.Fecha).ToString("yyyy-MM-dd HH:mm:ss");
                    response.IdMovimiento = anul.idAnulacion;
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    anul.NumAutorizacion = -1;
                    anul.Autorizado = false;
                    anul.MensajeRespuesta = "Problema en WebService : " + ex;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var a = 0;
                a++;
            }
            //GuardarBitacora(request, response);
            return response;
        }

        //6----------------------------------------------
        public CargoResponse SetCargo(CargoRequest request)
        {
            var ctx = new BroxelEntities();
            var response = new CargoResponse();
            try
            {
                var currentDate = DateTime.Now;
                var mov = new Movimiento
                {
                    FechaExpira = request.Tarjeta.FechaExpira,
                    Monto = request.Cargo.Monto,
                    NoReferencia = request.Cargo.NoReferencia,
                    NombreReferencia = request.Cargo.NombreReferencia,
                    NombreTarjeta = request.Tarjeta.NombreTarjeta,
                    FechaHoraCreacion = currentDate,
                    TipoTransaccion = 1,
                    idUsuario = request.UserID,
                    UsuarioCreacion = ctx.Usuario.FirstOrDefault(x => x.idUsuario == request.UserID).Usuario1,
                    ActivoLote = true,
                    Tarjeta = Helper.TruncaTarjeta(request.Tarjeta.NumeroTarjeta),
                    CVC = "**" + request.Tarjeta.Cvc2.ToString(CultureInfo.InvariantCulture).Substring(2),
                    RegControl = false,
                    Comercio = ctx.Comercio.FirstOrDefault(c => c.idComercio == request.Cargo.IdComercio),
                    NumCuenta = request.Tarjeta.Cuenta,
                };
                ctx.Movimiento.Add(mov);
                ctx.SaveChanges();
                var com = (from x in ctx.Comercio where x.idComercio == request.Cargo.IdComercio select x).First();
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
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    AutorizacionPortType autorizadorWs = new AutorizacionPortTypeClient();
                    var reqInt = new InspectorBehavior();
                    var handler = (AutorizacionPortTypeClient)autorizadorWs;
                    handler.Endpoint.Behaviors.Clear();
                    handler.Endpoint.Behaviors.Add(reqInt);
                    var dateTimeInicio = DateTime.Now;
                    _autorizarResp = autorizadorWs.Autorizar(_autorizarReq);
                    var dateTimeFin = DateTime.Now;
                    var reqXml = reqInt.LastRequestXml;
                    var resXml = reqInt.LastResponseXml;

                    try
                    {
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            new GenericBL.GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 14, 1,
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
                    throw ex;
                }
                mov.Autorizado = _autorizarResp.CodigoRespuesta.Trim() == ("-1");
                mov.NoAutorizacion = _autorizarResp.CodigoAutorizacion;
                var codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                var cr = ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
                if (cr != null) response.UserResponse = cr.Descripcion;
                response.Success = mov.Autorizado == true ? 1 : 0;
                response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
                response.IdMovimiento = mov.idMovimiento;
                response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
                if (cr != null) mov.MensajeError = cr.Descripcion;
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                response.UserResponse = ex.ToString();
            }
            //GuardarBitacora(request, response);
            return response;
        }

        //7----------------------------------------------
        public TransferenciaResponse TransferenciaDeCuentas(TransferenciaRequest request)
        {
            var ctx = new BroxelEntities();
            var response = new TransferenciaResponse();
            try
            {
                response.SaldoOrigenAntes = GetSaldosPorCuenta(request.Tarjeta.Cuenta, "", "TransferenciaDeCuentasOrigenAntes").Saldos.DisponibleCompras;
                response.SaldoDestinoAntes = GetSaldosPorCuenta(request.TarjetaRecibe.Cuenta, "", "TransferenciaDeCuentasDestinoAntes").Saldos.DisponibleCompras;
                var currentDate = DateTime.Now;
                var com = (from x in ctx.Comercio where x.idComercio == request.idComercio select x).First();
                if (com == null) return response;
                var mov = new Movimiento()
                {
                    FechaExpira = request.Tarjeta.FechaExpira,
                    Monto = request.Monto,
                    NombreTarjeta = request.Tarjeta.NombreTarjeta,
                    FechaHoraCreacion = currentDate,
                    TipoTransaccion = 65,
                    idUsuario = request.UserID,
                    UsuarioCreacion = ctx.Usuario.FirstOrDefault(x => x.idUsuario == request.UserID).Usuario1,
                    ActivoLote = true,
                    Tarjeta = Helper.TruncaTarjeta(request.Tarjeta.NumeroTarjeta),
                    CVC = "**" + request.Tarjeta.Cvc2.ToString(CultureInfo.InvariantCulture).Substring(2),
                    RegControl = false,
                    Comercio = com,
                    NumCuenta = request.Tarjeta.Cuenta
                };
                ctx.Movimiento.Add(mov);
                ctx.SaveChanges();
                try
                {
                    _autorizarReq = new AutorizarRequest
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
                            _autorizarReq.FeeTransaccion = request.Comision.ToString(CultureInfo.InvariantCulture);
                            break;
                        case 2:
                            _autorizarReq.FeeProcesamientoTransaccion = request.Comision.ToString(CultureInfo.InvariantCulture);
                            break;
                    }
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    AutorizacionPortType autorizadorWs = new AutorizacionPortTypeClient();
                    var reqInt = new InspectorBehavior();
                    var handler = (AutorizacionPortTypeClient)autorizadorWs;
                    handler.Endpoint.Behaviors.Clear();
                    handler.Endpoint.Behaviors.Add(reqInt);
                    var dateTimeInicio = DateTime.Now;
                    _autorizarResp = autorizadorWs.Autorizar(_autorizarReq);
                    var dateTimeFin = DateTime.Now;
                    var reqXml = reqInt.LastRequestXml;
                    var resXml = reqInt.LastResponseXml;

                    try
                    {
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            new GenericBL.GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 17, 1,
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
                    throw ex;
                }
                mov.Autorizado = _autorizarResp.CodigoRespuesta.Trim() == ("-1");
                mov.NoAutorizacion = _autorizarResp.CodigoAutorizacion;
                var codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                var cr = ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
                if (cr != null) response.UserResponse = cr.Descripcion;
                if (!String.Equals(response.UserResponse.Trim(), _autorizarResp.MensajeError.Trim(), StringComparison.CurrentCultureIgnoreCase) && _autorizarResp.CodigoRespuesta.Trim() != ("-1"))
                    response.UserResponse = _autorizarResp.MensajeError.Trim().ToUpper();
                response.Success = mov.Autorizado == true ? 1 : 0;
                response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
                response.IdMovimiento = mov.idMovimiento;
                response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
                if (cr != null) mov.MensajeError = cr.Descripcion;
                response.SaldoOrigenDespues = GetSaldosPorCuenta(request.Tarjeta.Cuenta, "", "TransferenciaDeCuentasOrigenDespues").Saldos.DisponibleCompras;
                response.SaldoDestinoDespues = GetSaldosPorCuenta(request.TarjetaRecibe.Cuenta, "", "TransferenciaDeCuentasDestinoDespues").Saldos.DisponibleCompras;
                var transf = new TransferenciasEntreCuentas()
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
                ctx.TransferenciasEntreCuentas.Add(transf);
                ctx.SaveChanges();
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

        //8----------------------------------------------
        public CargoResponse SetCargo(CargoRequest request, Int32 idUsuarioCallCenter)
        {
            var ctx = new BroxelEntities();
            var response = new CargoResponse();
            try
            {
                var currentDate = DateTime.Now;
                var mov = new Movimiento
                {
                    FechaExpira = request.Tarjeta.FechaExpira,
                    Monto = request.Cargo.Monto,
                    NoReferencia = request.Cargo.NoReferencia,
                    NombreReferencia = request.Cargo.NombreReferencia,
                    NombreTarjeta = request.Tarjeta.NombreTarjeta,
                    idUsuario = request.UserID,
                    UsuarioCreacion = ctx.Usuario.FirstOrDefault(x => x.idUsuario == idUsuarioCallCenter).Usuario1,
                    FechaHoraCreacion = currentDate,
                    UsuarioModificacion = request.Solicitante,
                    FechaHoraModificacion = currentDate,
                    ActivoLote = true,
                    Tarjeta = Helper.TruncaTarjeta(request.Tarjeta.NumeroTarjeta),
                    CVC = "**" + request.Tarjeta.Cvc2.ToString(CultureInfo.InvariantCulture).Substring(1),
                    RegControl = false,
                    Comercio = ctx.Comercio.FirstOrDefault(c => c.idComercio == request.Cargo.IdComercio),
                    NumCuenta = request.Tarjeta.Cuenta,
                };
                ctx.Movimiento.Add(mov);
                ctx.SaveChanges();
                var com = (from x in ctx.Comercio where x.idComercio == request.Cargo.IdComercio select x).First();
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
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    AutorizacionPortType autorizadorWs = new AutorizacionPortTypeClient();
                    var reqInt = new InspectorBehavior();
                    var handler = (AutorizacionPortTypeClient)autorizadorWs;
                    handler.Endpoint.Behaviors.Clear();
                    handler.Endpoint.Behaviors.Add(reqInt);
                    var dateTimeInicio = DateTime.Now;
                    _autorizarResp = autorizadorWs.Autorizar(_autorizarReq);
                    var dateTimeFin = DateTime.Now;
                    var reqXml = reqInt.LastRequestXml;
                    var resXml = reqInt.LastResponseXml;

                    try
                    {
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            new GenericBL.GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.Tarjeta.Cuenta, 15, 1,
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
                }
                mov.Autorizado = _autorizarResp.CodigoRespuesta.Trim() == ("-1");
                mov.NoAutorizacion = _autorizarResp.CodigoAutorizacion;
                var codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                var cr = ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
                if (cr != null) response.UserResponse = cr.Descripcion;
                response.Success = mov.Autorizado == true ? 1 : 0;
                response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
                response.IdMovimiento = mov.idMovimiento;
                response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
                if (cr != null) mov.MensajeError = cr.Descripcion;
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                response.UserResponse = ex.ToString();
            }
            //GuardarBitacora(request, response);
            return response;
        }

        //9----------------------------------------------
        public LimiteResponse SetPago(CargoRequest request)
        {
            var ctx = new BroxelEntities();
            var response = new LimiteResponse
            {
                SaldoAntes = GetSaldosPorCuenta(request.NumCuenta).Saldos.DisponibleCompras
            };
            try
            {
                var currentDate = DateTime.Now;
                string usuarioWs;
                try
                {
                    usuarioWs = ctx.Usuario.FirstOrDefault(x => x.idUsuario == request.UserID).Usuario1;
                }
                catch (Exception)
                {
                    usuarioWs = "WsDefault";
                }

                var mov = new Movimiento
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

                    AutorizacionPortType autorizadorWs = new AutorizacionPortTypeClient();
                    var reqInt = new InspectorBehavior();
                    var handler = (AutorizacionPortTypeClient)autorizadorWs;
                    handler.Endpoint.Behaviors.Clear();
                    handler.Endpoint.Behaviors.Add(reqInt);
                    var dateTimeInicio = DateTime.Now;
                    _autorizarResp = autorizadorWs.Autorizar(_autorizarReq);
                    var dateTimeFin = DateTime.Now;
                    var reqXml = reqInt.LastRequestXml;
                    var resXml = reqInt.LastResponseXml;

                    try
                    {
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            new GenericBL.GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, request.NumCuenta, 16, 1,
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
                    throw ex;
                }
                mov.Autorizado = _autorizarResp.CodigoRespuesta.Trim() == ("-1");
                mov.NoAutorizacion = _autorizarResp.CodigoAutorizacion;
                var codresp = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                var cr = ctx.CodigosRespuestaSQL.FirstOrDefault(x => x.Id == codresp);
                if (cr != null) response.UserResponse = cr.Descripcion;
                response.Success = mov.Autorizado == true ? 1 : 0;
                response.NumeroAutorizacion = _autorizarResp.CodigoAutorizacion;
                response.IdMovimiento = mov.idMovimiento;
                response.CodigoRespuesta = Convert.ToInt32(_autorizarResp.CodigoRespuesta);
                response.FechaCreacion = Convert.ToDateTime(mov.FechaHoraCreacion).ToString("yyyy-MM-dd HH:mm:ss");
                if (cr != null) mov.MensajeError = cr.Descripcion;
                ctx.SaveChanges();
                response.SaldoDespues = GetSaldosPorCuenta(request.NumCuenta, "", "SetPago").Saldos.DisponibleCompras;
            }
            catch (Exception ex)
            {
                response.UserResponse = ex.ToString();
            }
            //GuardarBitacora(request, response);
            return response;
        }

        #endregion

        #region operadorWs

        //1----------------------------------------------------
        protected ConsultarPorCuentaResponse1 ConsultaPorCuenta(String cuenta, String nombreSolicitante, DateTime? desde = null, DateTime? hasta = null, int paginaInicio = 0, int paginaFin = 0, int tipo = 0, int subtipo = 0)
        {
            var des = desde == null ? DateTime.Now.AddMonths(-1).AddDays(-15) : Convert.ToDateTime(desde);
            var has = hasta == null ? DateTime.Now : Convert.ToDateTime(hasta);
            var pag = (paginaInicio == 0 ? "1" : paginaInicio.ToString(CultureInfo.InvariantCulture));
            var pagInicio = paginaInicio;
            var pagFin = (paginaFin == 0 ? paginaInicio == 0 ? 1 : paginaInicio : paginaFin);
            var unaPagina = pag.Equals(pagFin) | pagFin <= pagInicio;

            var codigo = tipo != 0 ? new CodigoOperacion { Tipo = tipo.ToString(CultureInfo.InvariantCulture), SubTipo = subtipo == 0 ? subtipo.ToString(CultureInfo.InvariantCulture) : "0" } : null;

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
                            Desde = des.ToString("yyyy-MM-dd HH:mm:ss"),
                            Hasta = has.ToString("yyyy-MM-dd HH:mm:ss")
                        },
                        Pagina = pagInicio == 0 ? 1 + "" : pag,
                        Codigo = codigo,
                    },
                    Autenticacion = new newWsOperaciones.Autenticacion
                    {
                        Usuario = "broxel",
                        Password = Helper.CipherPassCrea("bRoXeL_1.2.3.4"),
                    },
                    Cuenta = cuenta,
                    Originador = new newWsOperaciones.Originador
                    {
                        Solicitante = nombreSolicitante,
                        ZonaHoraria = "America/Mexico_City",
                    }
                }
            };
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            CASA_SRTMX_OperacionPortType operadorWs = new CASA_SRTMX_OperacionPortTypeClient();
            var result = operadorWs.ConsultarPorCuenta(request);
            if (result.ConsultarPorCuentaResponse.Response.Codigo != "00") return result;
            if (result.ConsultarPorCuentaResponse.Listado != null && result.ConsultarPorCuentaResponse.Listado.Referencia != null && result.ConsultarPorCuentaResponse.Listado.Referencia.Paginas != null
            && result.ConsultarPorCuentaResponse.Listado.Referencia.Paginas.Total != null)
            {
                int paginas;
                if (pagInicio == 0)
                {
                    Console.WriteLine(result.ConsultarPorCuentaResponse.Listado.Referencia.Paginas.Total);
                    paginas = Int32.Parse(result.ConsultarPorCuentaResponse.Listado.Referencia.Paginas.Total);
                    pagInicio = paginas > 1 ? paginas - 1 : paginas;
                    request.ConsultarPorCuentaRequest.Busqueda.Pagina = pagInicio + "";
                    result = operadorWs.ConsultarPorCuenta(request);
                }
                var lista = new List<Operacion>();

                if (result.ConsultarPorCuentaResponse.Response.Codigo == "00")
                {
                    String tar;
                    foreach (var op in result.ConsultarPorCuentaResponse.Listado.Operaciones.Where(x => x.Codigo.Tipo != "129"))
                    {
                        tar = Helper.DechiperCrea(op.Tarjeta);
                        op.Tarjeta = "**** " + tar.Substring(12);
                        lista.Add(op);
                    }
                    paginas = Int32.Parse(result.ConsultarPorCuentaResponse.Listado.Referencia.Paginas.Total);
                    if (pagInicio == 0)
                        paginas--;
                    if (!unaPagina)
                    {
                        if (pagFin > 1) paginas = pagFin - pagInicio;
                        for (var i = pagInicio + 1; i <= paginas; i++)
                        {
                            request.ConsultarPorCuentaRequest.Busqueda.Pagina = i.ToString(CultureInfo.InvariantCulture);
                            var t = operadorWs.ConsultarPorCuenta(request);
                            var count = 0;
                            foreach (var op in t.ConsultarPorCuentaResponse.Listado.Operaciones.Where(x => x.Codigo.Tipo != "129" && x.Codigo.Tipo != "33"))
                            {
                                count++;
                                tar = Helper.DechiperCrea(op.Tarjeta);
                                op.Tarjeta = "**** " + tar.Substring(12);
                                lista.Add(op);
                            }

                        }
                    }
                    result.ConsultarPorCuentaResponse.Listado.Referencia.Registros.Actual = (Convert.ToInt32(
                    result.ConsultarPorCuentaResponse.Listado.Referencia.Registros.Actual) + lista.Count()).ToString(CultureInfo.InvariantCulture);
                    result.ConsultarPorCuentaResponse.Listado.Operaciones = lista.ToArray();
                }
            }

            var broxelEntities = new BroxelEntities();
            var mov = new Movimiento
            {
                Autorizado = result.ConsultarPorCuentaResponse.Response.Codigo == "00",
                TipoTransaccion = 127,
                SubTipoTransaccion = 0,
                NumCuenta = cuenta,
            };
            try
            {
                broxelEntities.Movimiento.Add(mov);
                broxelEntities.SaveChanges();
            }
            catch (Exception e)
            {
                var DoNothing = e.ToString();
            }

            return result;
        }

        //2---------------------------------------------------
        public ConsultarPorTarjetaResponse1 ConsultaPorTarjeta(String tarjeta, String nombreSolicitante, String desde = "", String hasta = "", String paginaInicio = "", String paginaFin = "", String tipo = "", String subtipo = "")
        {
            var des = desde == "" ? DateTime.Now.AddMonths(-1).AddDays(-15) : Convert.ToDateTime(desde);
            var has = hasta == "" ? DateTime.Now : Convert.ToDateTime(hasta);
            var pag = paginaInicio == "" ? "1" : paginaInicio;

            var pagInicio = paginaInicio == "" ? 1 : Int32.Parse(paginaInicio);
            var pagFin = paginaFin == "" ?
                    paginaInicio == "" ? 1 : Int32.Parse(paginaInicio)
                : Int32.Parse(paginaFin);

            var codigo = !string.IsNullOrEmpty(tipo) ? new CodigoOperacion { Tipo = tipo, SubTipo = !string.IsNullOrEmpty(subtipo) ? subtipo : "0" } : null;

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
                            Desde = des.ToString("yyyy-MM-dd HH:mm:ss"),
                            Hasta = has.ToString("yyyy-MM-dd HH:mm:ss")
                        },
                        Pagina = pag == "0" ? "1" : pag,
                        Codigo = codigo,
                    },
                    Autenticacion = new newWsOperaciones.Autenticacion
                    {
                        Usuario = "broxel",
                        Password = Helper.CipherPassCrea("bRoXeL_1.2.3.4"),
                    },
                    Tarjeta = Helper.CipherPassCrea(tarjeta),
                    Originador = new newWsOperaciones.Originador
                    {
                        Solicitante = nombreSolicitante,
                        ZonaHoraria = "America/Mexico_City",
                    }
                }
            };
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            CASA_SRTMX_OperacionPortType operadorWs = new CASA_SRTMX_OperacionPortTypeClient();
            var result = operadorWs.ConsultarPorTarjeta(request);
            var lista = new List<Operacion>();

            if (result.ConsultarPorTarjetaResponse.Response.Codigo == "00")
            {
                String tar;
                foreach (var op in result.ConsultarPorTarjetaResponse.Listado.Operaciones.Where(x => x.Codigo.Tipo != "129"))
                {
                    tar = Helper.DechiperCrea(op.Tarjeta);
                    op.Tarjeta = "**** " + tar.Substring(12);
                    lista.Add(op);
                }

                for (int i = pagInicio + 1; i <= (pagFin < pagInicio ? 0 : pagFin - pagInicio); i++)
                {
                    request.ConsultarPorTarjetaRequest.Busqueda.Pagina = i.ToString(CultureInfo.InvariantCulture);
                    var t = operadorWs.ConsultarPorTarjeta(request);
                    foreach (var op in t.ConsultarPorTarjetaResponse.Listado.Operaciones.Where(x => x.Codigo.Tipo != "129" && x.Codigo.Tipo != "33"))
                    {
                        tar = Helper.DechiperCrea(op.Tarjeta);
                        op.Tarjeta = "**** " + tar.Substring(12);
                        lista.Add(op);
                    }
                }
                result.ConsultarPorTarjetaResponse.Listado.Referencia.Registros.Actual = (Convert.ToInt32(
                    result.ConsultarPorTarjetaResponse.Listado.Referencia.Registros.Actual) + lista.Count()).ToString(CultureInfo.InvariantCulture);
                //result.ConsultarPorTarjetaResponse.Listado.Referencia.Paginas.Actual = result.ConsultarPorTarjetaResponse.Listado.Referencia.Paginas.Total;
                result.ConsultarPorTarjetaResponse.Listado.Operaciones = lista.ToArray();
            }
            return result;
        }

        #endregion

        #region cuentaWs

        //1-----------------------------------------------------
        public OperarCuentaResponse ActivacionDeCuenta(OperarCuentaRequest request)
        {
            try
            {
                // Validacion de activacion de cuenta niveles de cuenta
                if (new GenericBL.GenericBL().ValidaCuentaEnCuarentena(request.NumCuenta))
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
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                CASA_SRTMX_CuentaPortType cuentaWs = new CASA_SRTMX_CuentaPortTypeClient();
                var dr = cuentaWs.Desbloquear(new DesbloquearRequest1
                {
                    DesbloquearRequest = new DesbloquearRequest
                    {
                        Autenticacion = new Autenticacion
                        {
                            Usuario = "broxel",
                            Password = Helper.CipherPassCrea("bRoXeL_1.2.3.4"),
                        },

                        Cuenta = request.NumCuenta,
                        Originador = new Originador
                        {
                            Solicitante = request.Solicitante,
                            ZonaHoraria = "America/Mexico_City",
                        }
                    }
                }).DesbloquearResponse;
                var desbloquear = new OperarCuentaResponse
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
                var desbloquear = new OperarCuentaResponse
                {
                    Success = 0,
                    UserResponse = "ErrorAlDesbloquear" + ex,
                    CodigoRespuesta = 978,
                };
                return desbloquear;
            }
        }

        //2----------------------------------------------------
        public OperarCuentaResponse BloqueoDeCuenta(OperarCuentaRequest request)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                CASA_SRTMX_CuentaPortType cuentaWs = new CASA_SRTMX_CuentaPortTypeClient();
                var br = cuentaWs.Bloquear(new BloquearRequest1
                {
                    BloquearRequest = new BloquearRequest
                    {
                        Autenticacion = new Autenticacion
                        {
                            Usuario = "broxel",
                            Password = Helper.CipherPassCrea("bRoXeL_1.2.3.4"),
                        },
                        Cuenta = request.NumCuenta,
                        Originador = new Originador
                        {
                            Solicitante = request.Solicitante,
                            ZonaHoraria = "America/Mexico_City",
                        }
                    }
                }).BloquearResponse;
                var bloquear = new OperarCuentaResponse
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
                var desbloquear = new OperarCuentaResponse
                {
                    Success = 0,
                    UserResponse = "ErrorAlBloquear" + ex,
                    CodigoRespuesta = 978,
                };
                return desbloquear;
            }
        }

        //3---------------------------------------------------
        public SaldoResponse GetSaldos(OperarCuentaRequest request)
        {
            var response = new SaldoResponse();
            if (request.Solicitante == null)
                request.Solicitante = "000";
            if (request.Solicitante.Length < 3)
                request.Solicitante = request.Solicitante.PadLeft(3).Replace(' ', '0');
            if (request.Solicitante.Length >= 21)
                request.Solicitante = request.Solicitante.Substring(0, 21);
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                CASA_SRTMX_CuentaPortType cuentaWs = new CASA_SRTMX_CuentaPortTypeClient();
                var cr = cuentaWs.Consultar(new ConsultarRequest1
                {
                    ConsultarRequest = new ConsultarRequest
                    {
                        Autenticacion = new Autenticacion
                        {
                            Usuario = "broxel",
                            Password = Helper.CipherPassCrea("bRoXeL_1.2.3.4"),
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
            }
            catch (Exception e)
            {
                response.CodigoRespuesta = 997;
                response.Success = 0;
                response.UserResponse = ("ERROR AL PROCESAR LA TRANSACCION: " + e.Message).Substring(0, 254);
                if (response.UserResponse.Length > 254)
                    response.UserResponse = response.UserResponse.Substring(0, 254);
                /*
                response.CodigoRespuesta = -1;
                response.Success = 1;
                response.Saldos.DisponibleAdelantos = 10000;
                response.Saldos.DisponibleCompras = 10000;
                response.Saldos.DisponibleCuotas = 1000;
                response.Saldos.DisponiblePrestamos = 10000;
                 */
            }
            GuardarBitacora(request, response);
            return response;
        }

        #endregion

        #region BroxelService

        /// <summary>
        /// Regresa los movimientos de la pagina indicada
        /// </summary>
        /// <param name="request">Request de tipo MovimientosRequest</param>
        /// <param name="pagina">Numero de pagina para traer los movimientos</param>
        /// <returns>MovimientosResponse</returns>
        public MovimientosResponse GetMovimientos(MovimientosRequest request, int pagina)
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
                response.Movimientos.Add(new RequestResponses.Movimiento
                {
                    Tarjeta = operacion.Tarjeta,
                    Aprobada = operacion.Respuesta.Codigo == "00",
                    Comercio = (operacion.Codigo.Tipo == "35" || (operacion.Codigo.Tipo == "33" && operacion.Codigo.SubTipo != "2")) ? ("ABONO A CUENTA")
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

        #endregion

        #region Métodos Privados

        /// <summary>
        /// Sobrecarga de GetSaldoPorCuenta
        /// </summary>
        /// <param name="numeroCuenta">numero de cuenta</param>
        /// <param name="idUsuario">Id de usuario</param>
        /// <param name="callerId">Id del invocador</param>
        /// <returns>Saldo</returns>
        public SaldoResponse GetSaldosPorCuenta(String numeroCuenta, String idUsuario = "", String callerId = "null")
        {
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

            return GetSaldos(new OperarCuentaRequest { NumCuenta = numeroCuenta, Solicitante = idUsuario, Accion = "GetSaldosPorCuenta|Caller: " + callerId + "|ip: " + ip });
        }

        private static void GuardarBitacora(Request request, Response response)
        {
            var ctx = new BroxelEntities();
            var frame = new StackFrame(1);
            try
            {
                var numTarjeta = request.Tarjeta != null ? request.Tarjeta.NumeroTarjeta : "";
                if (numTarjeta.Length == 16)
                    numTarjeta = numTarjeta.Substring(0, 6) + "** ****" + numTarjeta.Substring(12, 4);
                var ltsq = new LogTransaccionesSQL
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
                ctx.LogTransaccionesSQL.Add(ltsq);
                ctx.SaveChanges();
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

        public String GetNombreComercioPorCodigo(String codigo)
        {
            var brmy = new broxelco_rdgEntities();
            var c = brmy.Comercio.FirstOrDefault(x => x.CodigoComercio == codigo);
            if (c != null)
                return c.razon_social;
            return codigo;
        }

        #endregion

        #endregion
    }
}
