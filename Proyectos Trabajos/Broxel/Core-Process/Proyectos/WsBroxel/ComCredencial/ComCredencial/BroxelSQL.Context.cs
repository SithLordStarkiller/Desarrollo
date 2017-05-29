﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComCredencial
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class BroxelSQLEntities : DbContext
    {
        public BroxelSQLEntities()
            : base("name=BroxelSQLEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CargosDetalle> CargosDetalle { get; set; }
        public DbSet<CargosSolicitudes> CargosSolicitudes { get; set; }
    
        public virtual int SpInsertarCargoSinDetalle(string claveCliente, string producto, Nullable<int> idComercio, string nombreComercio, string estado, string tipo, string emailNotificacion, string usuarioCreacion, Nullable<byte> cargoRemanente, Nullable<bool> activaCuentas, ObjectParameter folio, Nullable<bool> cuentasMultiples, ObjectParameter idSolicitud)
        {
            var claveClienteParameter = claveCliente != null ?
                new ObjectParameter("ClaveCliente", claveCliente) :
                new ObjectParameter("ClaveCliente", typeof(string));
    
            var productoParameter = producto != null ?
                new ObjectParameter("Producto", producto) :
                new ObjectParameter("Producto", typeof(string));
    
            var idComercioParameter = idComercio.HasValue ?
                new ObjectParameter("IdComercio", idComercio) :
                new ObjectParameter("IdComercio", typeof(int));
    
            var nombreComercioParameter = nombreComercio != null ?
                new ObjectParameter("NombreComercio", nombreComercio) :
                new ObjectParameter("NombreComercio", typeof(string));
    
            var estadoParameter = estado != null ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(string));
    
            var tipoParameter = tipo != null ?
                new ObjectParameter("Tipo", tipo) :
                new ObjectParameter("Tipo", typeof(string));
    
            var emailNotificacionParameter = emailNotificacion != null ?
                new ObjectParameter("EmailNotificacion", emailNotificacion) :
                new ObjectParameter("EmailNotificacion", typeof(string));
    
            var usuarioCreacionParameter = usuarioCreacion != null ?
                new ObjectParameter("UsuarioCreacion", usuarioCreacion) :
                new ObjectParameter("UsuarioCreacion", typeof(string));
    
            var cargoRemanenteParameter = cargoRemanente.HasValue ?
                new ObjectParameter("CargoRemanente", cargoRemanente) :
                new ObjectParameter("CargoRemanente", typeof(byte));
    
            var activaCuentasParameter = activaCuentas.HasValue ?
                new ObjectParameter("ActivaCuentas", activaCuentas) :
                new ObjectParameter("ActivaCuentas", typeof(bool));
    
            var cuentasMultiplesParameter = cuentasMultiples.HasValue ?
                new ObjectParameter("CuentasMultiples", cuentasMultiples) :
                new ObjectParameter("CuentasMultiples", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpInsertarCargoSinDetalle", claveClienteParameter, productoParameter, idComercioParameter, nombreComercioParameter, estadoParameter, tipoParameter, emailNotificacionParameter, usuarioCreacionParameter, cargoRemanenteParameter, activaCuentasParameter, folio, cuentasMultiplesParameter, idSolicitud);
        }
    
        public virtual int SpInsertarCargoSolicitud(string claveCliente, string numCuenta, string producto, Nullable<int> idComercio, string nombreComercio, string estado, string tipo, string emailNotificacion, string usuarioCreacion, Nullable<byte> cargoRemanente, Nullable<decimal> importe, Nullable<bool> activaCuentas, ObjectParameter folio)
        {
            var claveClienteParameter = claveCliente != null ?
                new ObjectParameter("ClaveCliente", claveCliente) :
                new ObjectParameter("ClaveCliente", typeof(string));
    
            var numCuentaParameter = numCuenta != null ?
                new ObjectParameter("NumCuenta", numCuenta) :
                new ObjectParameter("NumCuenta", typeof(string));
    
            var productoParameter = producto != null ?
                new ObjectParameter("Producto", producto) :
                new ObjectParameter("Producto", typeof(string));
    
            var idComercioParameter = idComercio.HasValue ?
                new ObjectParameter("IdComercio", idComercio) :
                new ObjectParameter("IdComercio", typeof(int));
    
            var nombreComercioParameter = nombreComercio != null ?
                new ObjectParameter("NombreComercio", nombreComercio) :
                new ObjectParameter("NombreComercio", typeof(string));
    
            var estadoParameter = estado != null ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(string));
    
            var tipoParameter = tipo != null ?
                new ObjectParameter("Tipo", tipo) :
                new ObjectParameter("Tipo", typeof(string));
    
            var emailNotificacionParameter = emailNotificacion != null ?
                new ObjectParameter("EmailNotificacion", emailNotificacion) :
                new ObjectParameter("EmailNotificacion", typeof(string));
    
            var usuarioCreacionParameter = usuarioCreacion != null ?
                new ObjectParameter("UsuarioCreacion", usuarioCreacion) :
                new ObjectParameter("UsuarioCreacion", typeof(string));
    
            var cargoRemanenteParameter = cargoRemanente.HasValue ?
                new ObjectParameter("CargoRemanente", cargoRemanente) :
                new ObjectParameter("CargoRemanente", typeof(byte));
    
            var importeParameter = importe.HasValue ?
                new ObjectParameter("Importe", importe) :
                new ObjectParameter("Importe", typeof(decimal));
    
            var activaCuentasParameter = activaCuentas.HasValue ?
                new ObjectParameter("ActivaCuentas", activaCuentas) :
                new ObjectParameter("ActivaCuentas", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpInsertarCargoSolicitud", claveClienteParameter, numCuentaParameter, productoParameter, idComercioParameter, nombreComercioParameter, estadoParameter, tipoParameter, emailNotificacionParameter, usuarioCreacionParameter, cargoRemanenteParameter, importeParameter, activaCuentasParameter, folio);
        }
    
        public virtual int SpInsertarDetalleCargo(Nullable<int> idSolicitudCargo, string claveCliente, string numCuenta, string producto, string referencia, Nullable<decimal> importe)
        {
            var idSolicitudCargoParameter = idSolicitudCargo.HasValue ?
                new ObjectParameter("IdSolicitudCargo", idSolicitudCargo) :
                new ObjectParameter("IdSolicitudCargo", typeof(int));
    
            var claveClienteParameter = claveCliente != null ?
                new ObjectParameter("ClaveCliente", claveCliente) :
                new ObjectParameter("ClaveCliente", typeof(string));
    
            var numCuentaParameter = numCuenta != null ?
                new ObjectParameter("NumCuenta", numCuenta) :
                new ObjectParameter("NumCuenta", typeof(string));
    
            var productoParameter = producto != null ?
                new ObjectParameter("Producto", producto) :
                new ObjectParameter("Producto", typeof(string));
    
            var referenciaParameter = referencia != null ?
                new ObjectParameter("Referencia", referencia) :
                new ObjectParameter("Referencia", typeof(string));
    
            var importeParameter = importe.HasValue ?
                new ObjectParameter("Importe", importe) :
                new ObjectParameter("Importe", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpInsertarDetalleCargo", idSolicitudCargoParameter, claveClienteParameter, numCuentaParameter, productoParameter, referenciaParameter, importeParameter);
        }
    }
}
