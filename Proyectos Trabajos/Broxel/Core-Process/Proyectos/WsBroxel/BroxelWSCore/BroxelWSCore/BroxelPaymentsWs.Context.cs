﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BroxelWSCore
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BroxelPaymentsWsEntities : DbContext
    {
        public BroxelPaymentsWsEntities()
            : base("name=BroxelPaymentsWsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AnulacionN> AnulacionN { get; set; }
        public virtual DbSet<CodigosRespuesta> CodigosRespuesta { get; set; }
        public virtual DbSet<Comercio> Comercio { get; set; }
        public virtual DbSet<LogTransacciones> LogTransacciones { get; set; }
        public virtual DbSet<Movimiento> Movimiento { get; set; }
        public virtual DbSet<Parametros> Parametros { get; set; }
        public virtual DbSet<Renominacion> Renominacion { get; set; }
        public virtual DbSet<TransferenciasEntreCuentas> TransferenciasEntreCuentas { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}