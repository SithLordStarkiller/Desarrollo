﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Suncorp.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SuncorpEntities : DbContext
    {
        public SuncorpEntities()
            : base("name=SuncorpEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<LogCatTipoLog> LogCatTipoLog { get; set; }
        public virtual DbSet<LogLogger> LogLogger { get; set; }
        public virtual DbSet<UsCatNivelUsuario> UsCatNivelUsuario { get; set; }
        public virtual DbSet<UsCatTipoUsuario> UsCatTipoUsuario { get; set; }
        public virtual DbSet<UsEstatusUsuario> UsEstatusUsuario { get; set; }
        public virtual DbSet<UsUsuarios> UsUsuarios { get; set; }
    }
}
