﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CrudMySql.Entidades;

namespace CrudMySql.AccesoDatos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class worldEntities : DbContext
    {
        public worldEntities()
            : base("name=worldEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<us_cat_rol> us_cat_rol { get; set; }
        public DbSet<us_usuarios> us_usuarios { get; set; }
    }
}
