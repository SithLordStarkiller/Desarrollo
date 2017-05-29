using System;
using System.Linq;
using ComCredencial.RequestResponses;
using ComCredencial.wsOperaciones;
using ComCredencial.wsAutorizacion;
using System.Globalization;
using System.Net;

namespace ComCredencial
{
    public class Bussines
    {
        private AutorizarRequest _autorizarReq;
        private AutorizarResponse _autorizarResp;

        public ConsultarPorCuentaResponse1 ConsultaPorCuenta(ConsultarPorCuentaRequest1 request)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback +=
                        (sender, cert, chain, sslPolicyErrors) => true;
                CASA_SRTMX_OperacionPortType operadorWs = new CASA_SRTMX_OperacionPortTypeClient();

                return operadorWs.ConsultarPorCuenta(request);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public CargoResponse SetCargo(CargoRequest request)
        {
            var ctx = new BroxelEntities();
            var response = new CargoResponse();
            try
            {
                AutorizacionPortType autorizadorWs = new AutorizacionPortTypeClient();
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
                    ServicePointManager.ServerCertificateValidationCallback +=
                        (sender, cert, chain, sslPolicyErrors) => true;

                    _autorizarResp = autorizadorWs.Autorizar(_autorizarReq);
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
                if (cr != null)
                    response.UserResponse = cr.Descripcion;
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
            return response;

        }


    }
}
