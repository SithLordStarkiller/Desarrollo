using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsBroxel.App_Code.SolicitudBL;
using wsBroxel.App_Code.VCBL;

namespace wsBroxel.App_Code.Online
{
    public static class OnlineHelper
    {
        //MLS Por cada cuenta se deberá enviar la CLABE configurada para ella, aún no están definidas pero en maquila ya se encuentra la columna para este fin
        public static CuentaOnline GetCuentaLogin(String NumCuenta)
        {
            broxelco_rdgEntities _broxelCo = new broxelco_rdgEntities();
            BroxelService webService = new BroxelService();

            var Cuentas = (from a in _broxelCo.maquila
                           join b in _broxelCo.productos_broxel
                               on a.producto equals b.codigo
                           where a.num_cuenta == NumCuenta
                           select new
                           {
                               a.num_cuenta,
                               a.CLABE,
                               a.nombre_titular,
                               a.producto,
                               a.nro_tarjeta,
                               b.branding,
                               b.Descripcion,
                               a.clave_cliente
                           }).ToList();

            CuentaOnline co = new CuentaOnline();
            //co.Saldos = new SaldoOnlineResponse();
            if (Cuentas.Count == 1)
            {
                var Cuenta = Cuentas[0];
                var detalleClienteBroxel = _broxelCo.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == Cuenta.clave_cliente && x.Producto == Cuenta.producto);
                var producto = _broxelCo.productos_broxel.FirstOrDefault(x => x.codigo == Cuenta.producto);
                if (producto == null || detalleClienteBroxel == null)
                {
                    co.DisponeCredito = false;
                    co.TransfiereOnline = false;
                }
                else
                {
                    if (new VCardBL().EsVc(NumCuenta))
                        co.TipoProd = "V";
                    else
                        co.TipoProd = producto.tipo == "Credito" ? "C" : "E";

                    co.DisponeCredito = (( detalleClienteBroxel.TipoComisionDisposicion == 1 || detalleClienteBroxel.TipoComisionDisposicion == 2 ) || ((detalleClienteBroxel.TipoComisionDisposicion == 3 || detalleClienteBroxel.TipoComisionDisposicion == 4) && (producto.DisposicionEfectivo == 3 || producto.DisposicionEfectivo == 4)));
                    co.TransfiereOnline = (( detalleClienteBroxel.TipoComisionTransferencia == 1 || detalleClienteBroxel.TipoComisionTransferencia == 2 ) || ( (detalleClienteBroxel.TipoComisionTransferencia == 3 || detalleClienteBroxel.TipoComisionTransferencia == 4) && (producto.TipoComisionTransferencia == 3 || producto.TipoComisionTransferencia == 4)));
                    co.RecibeTransferencia = (detalleClienteBroxel.RecibeTransferencia == 1 || producto.RecibeTransferencia == 1);
                    co.CambiaNIP = ((detalleClienteBroxel.CambioDeNip == 1 ) || ( (detalleClienteBroxel.CambioDeNip == 2) && (producto.CambioDeNip == 1)));
                    if (producto.PagarServicios)
                    {
                        co.PagarServicios = detalleClienteBroxel.PagarServicios;
                    }

                    //Establece si a nivel producto tiene funcionalidad P2P.
                    if (producto.PermiteP2P)
                    {
                        // Establece si a nivel cliente-producto tiene funcionalidad P2P.
                        if (detalleClienteBroxel.PermiteP2P)
                        {
                            co.PermiteP2P = detalleClienteBroxel.PermiteP2P;

                            var p2p_activo = _broxelCo.accessos_clientes.FirstOrDefault(s => s.cuenta == NumCuenta);
                            
                            co.P2P_Activo = p2p_activo != null ? p2p_activo.P2P_Activo : false;
                        }
                        else
                        {
                            co.PermiteP2P = false;
                        }
                    }
                    else
                    {
                        co.PermiteP2P = producto.PermiteP2P;
                    }
                  

                }

                var numTar = _broxelCo.TarjetasFisicasAdicionales.FirstOrDefault(s => s.NumCuenta == NumCuenta);

                co.NumCuenta = Cuenta.num_cuenta;
                co.CLABE = Cuenta.CLABE;
                co.NombreCuenta = Cuenta.nombre_titular;
                // Elimina el branding si ya esta en la descripcion
                co.TipoCuenta = (Cuenta.Descripcion.Contains(Cuenta.branding)?"":Cuenta.branding) + " " + Cuenta.Descripcion;
                co.Prod = Cuenta.producto;
                co.NumTarjeta = Cuenta.producto != "K166" ? Cuenta.nro_tarjeta : numTar!= null ? numTar.Tarjeta : Cuenta.nro_tarjeta;
                co.MostrarDatosTarjeta = new VCardBL().EsMostrarInformacion(NumCuenta) ? 1 : 0;
            }
            return co;
        }
        
        public static SaldoOnlineResponse GetSaldosCuenta(String NumCuenta)
        {
            SaldoOnlineResponse saldos = new SaldoOnlineResponse {Success = false};
            broxelco_rdgEntities _broxelCo = new broxelco_rdgEntities();
            BroxelService webService = new BroxelService();

            var Cuentas = (from a in _broxelCo.maquila
                           join b in _broxelCo.productos_broxel
                               on a.producto equals b.codigo
                           where a.num_cuenta == NumCuenta
                           select new
                           {
                               a.num_cuenta,
                               a.nombre_titular,
                               a.producto,
                               a.nro_tarjeta,
                               b.branding,
                               b.Descripcion,
                               a.clave_cliente
                           }).ToList()[0];

            var Cuenta = Cuentas;
            var tipoCuenta = "";
            var res = webService.GetSaldosPorCuenta(NumCuenta, "", "SaldoOnlineResponse");
            if (res.Success == 1)
            {
                var tipo = new MySqlDataAccess().GetTipo(NumCuenta);
                tipoCuenta = tipo;
                saldos.CreditoDisp = res.Saldos.DisponibleCompras;
                saldos.LimiteCredito = res.Saldos.LimiteCompra;
                saldos.Saldo = saldos.LimiteCredito - saldos.CreditoDisp;
                saldos.EstadoOperativo = res.EstadoOperativo.ToUpper() == "OPERATIVA";
                if (tipo == "Credito" && saldos.CreditoDisp < 0)
                    saldos.CreditoDisp = 0;
                saldos.Success = true;
            }
            try
            {
                var edoCuenta =
                    _broxelCo.log_registro_de_cuenta.Where(x => x.numero_de_cuenta == Cuenta.num_cuenta)
                        .OrderByDescending(x => x.fecha_ultima_liquidacion)
                        .Take(1).ToArray()[0];
                saldos.DiaCorte = Convert.ToDateTime(edoCuenta.fecha_ultima_liquidacion);
                saldos.FechaLimitePago = Convert.ToDateTime(edoCuenta.fecha_vto_liq_actual);
                saldos.PagoMinimo = Convert.ToDouble(edoCuenta.importe_pago_minimo);
                if (tipoCuenta == "Credito" && saldos.CreditoDisp < 0)
                    saldos.SaldoActual = (saldos.CreditoDisp*(-1)) + Convert.ToDecimal(edoCuenta.importe_saldo_actual ?? 0);
                saldos.SaldoAlCorte = Convert.ToDecimal(edoCuenta.importe_saldo_actual ?? 0);

            }
            catch (Exception e)
            {
                bool DoNothing;
            }

            return saldos;
        }
    }
}