﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace wsBroxel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class broxelco_rdgEntities : DbContext
    {
        public broxelco_rdgEntities()
            : base("name=broxelco_rdgEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<dispersionesSolicitudes> dispersionesSolicitudes { get; set; }
        public DbSet<disponibles> disponibles { get; set; }
        public DbSet<fechas_corte_x_grupos> fechas_corte_x_grupos { get; set; }
        public DbSet<ind_movimientos> ind_movimientos { get; set; }
        public DbSet<phpSession> phpSession { get; set; }
        public DbSet<registro_de_cuenta> registro_de_cuenta { get; set; }
        public DbSet<vw_maquila> vw_maquila { get; set; }
        public DbSet<correr_disponibles> correr_disponibles { get; set; }
        public DbSet<dispositivos> dispositivos { get; set; }
        public DbSet<estadosCredencial> estadosCredencial { get; set; }
        public DbSet<vw_registri_broxel> vw_registri_broxel { get; set; }
        public DbSet<vw_EdoCuenta> vw_EdoCuenta { get; set; }
        public DbSet<log_registro_de_cuenta> log_registro_de_cuenta { get; set; }
        public DbSet<bancos_stp> bancos_stp { get; set; }
        public DbSet<ComisionesB1010> ComisionesB1010 { get; set; }
        public DbSet<clientesBroxel> clientesBroxel { get; set; }
        public DbSet<UsuariosOnlineCLABE> UsuariosOnlineCLABE { get; set; }
        public DbSet<reversos> reversos { get; set; }
        public DbSet<vw_registri> vw_registri { get; set; }
        public DbSet<catalogo_acceso_comercios> catalogo_acceso_comercios { get; set; }
        public DbSet<ClientesComisiones> ClientesComisiones { get; set; }
        public DbSet<pagosSolicitudes> pagosSolicitudes { get; set; }
        public DbSet<dispersionesWE> dispersionesWE { get; set; }
        public DbSet<devolucionesSolicitudes> devolucionesSolicitudes { get; set; }
        public DbSet<registri_broxel> registri_broxel { get; set; }
        public DbSet<session_dash> session_dash { get; set; }
        public DbSet<TransferenciasSolicitud> TransferenciasSolicitud { get; set; }
        public DbSet<HistoricoComisionesPublidea> HistoricoComisionesPublidea { get; set; }
        public DbSet<LogActividadesOperaciones> LogActividadesOperaciones { get; set; }
        public DbSet<OrdenDeTrabajo> OrdenDeTrabajo { get; set; }
        public DbSet<DisposicionesEfectivo> DisposicionesEfectivo { get; set; }
        public DbSet<Transferencias> Transferencias { get; set; }
        public DbSet<Monedas> Monedas { get; set; }
        public DbSet<CodigosRespuesta> CodigosRespuesta { get; set; }
        public DbSet<CargosDisposicionesEfectivo> CargosDisposicionesEfectivo { get; set; }
        public DbSet<cat_incremento> cat_incremento { get; set; }
        public DbSet<LogUsuariosOnlineBroxel> LogUsuariosOnlineBroxel { get; set; }
        public DbSet<RenominacionSolicitudes> RenominacionSolicitudes { get; set; }
        public DbSet<UsuariosOnlineBroxel> UsuariosOnlineBroxel { get; set; }
        public DbSet<ParametrosServicio> ParametrosServicio { get; set; }
        public DbSet<AgrupacionClientes> AgrupacionClientes { get; set; }
        public DbSet<ciudadesCredencial> ciudadesCredencial { get; set; }
        public DbSet<devolucionesInternas> devolucionesInternas { get; set; }
        public DbSet<pagosInternos> pagosInternos { get; set; }
        public DbSet<OnLineFavoritos> OnLineFavoritos { get; set; }
        public DbSet<logAltaBajaCuentaGrupo> logAltaBajaCuentaGrupo { get; set; }
        public DbSet<CuentasCobrables> CuentasCobrables { get; set; }
        public DbSet<MailingThemesOnline> MailingThemesOnline { get; set; }
        public DbSet<MailConfig> MailConfig { get; set; }
        public DbSet<PagoServiciosCtrl> PagoServiciosCtrl { get; set; }
		
		public DbSet<TarjetasBroxelLayout> TarjetasBroxelLayout { get; set; }
        public DbSet<CreaClienteSinTarjetaLog> CreaClienteSinTarjetaLog { get; set; }
        public DbSet<registro_tc> registro_tc { get; set; }
        public DbSet<CatCLienteLayout> CatCLienteLayout { get; set; }
        public DbSet<RenominacionesInternas> RenominacionesInternas { get; set; }
        public DbSet<clientesBrxLayout> clientesBrxLayout { get; set; }
        public DbSet<gruposComercios> gruposComercios { get; set; }
        public DbSet<clienteBroxelLayout> clienteBroxelLayout { get; set; }
        public DbSet<ControlCuentasDetalle> ControlCuentasDetalle { get; set; }
        public DbSet<DetalleClientesBroxel> DetalleClientesBroxel { get; set; }
        public DbSet<productos_broxel> productos_broxel { get; set; }
        public DbSet<ControlCuentas> ControlCuentas { get; set; }
        public DbSet<MovimientosClientes> MovimientosClientes { get; set; }
        public DbSet<TarjetasFisicasAdicionales> TarjetasFisicasAdicionales { get; set; }
        public DbSet<CodigosTransaccionWebService> CodigosTransaccionWebService { get; set; }
        public DbSet<mobile_session> mobile_session { get; set; }
        public DbSet<accessos_clientes> accessos_clientes { get; set; }
        public DbSet<TransferenciasDetalle> TransferenciasDetalle { get; set; }
        public DbSet<DireccionEnvioTarjetaFisica> DireccionEnvioTarjetaFisica { get; set; }
        public DbSet<maquila> maquila { get; set; }
        public DbSet<Comercio11> Comercio { get; set; }
        public DbSet<dispersionesInternas> dispersionesInternas { get; set; }
    }
}
