﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Universidad.Entidades
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UniversidadBDEntities : DbContext
    {
        public UniversidadBDEntities()
            : base("name=UniversidadBDEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ALU_ALUMNOS> ALU_ALUMNOS { get; set; }
        public DbSet<ALU_HORARIO> ALU_HORARIO { get; set; }
        public DbSet<ALU_KARDEX> ALU_KARDEX { get; set; }
        public DbSet<CAR_CAT_CARRERAS> CAR_CAT_CARRERAS { get; set; }
        public DbSet<CLA_CAT_HORAS> CLA_CAT_HORAS { get; set; }
        public DbSet<CLA_CLASES> CLA_CLASES { get; set; }
        public DbSet<CLA_HOR_CLASES> CLA_HOR_CLASES { get; set; }
        public DbSet<DIR_CAT_CODIGO_POSTAL> DIR_CAT_CODIGO_POSTAL { get; set; }
        public DbSet<DIR_CAT_DELG_MUNICIPIO> DIR_CAT_DELG_MUNICIPIO { get; set; }
        public DbSet<DIR_CAT_ESTADO> DIR_CAT_ESTADO { get; set; }
        public DbSet<DIR_DIRECCIONES> DIR_DIRECCIONES { get; set; }
        public DbSet<MAT_CAT_MATERIAS> MAT_CAT_MATERIAS { get; set; }
        public DbSet<PER_CAT_NACIONALIDAD> PER_CAT_NACIONALIDAD { get; set; }
        public DbSet<PER_CAT_TELEFONOS> PER_CAT_TELEFONOS { get; set; }
        public DbSet<PER_CAT_TIPO_PERSONA> PER_CAT_TIPO_PERSONA { get; set; }
        public DbSet<PER_MEDIOS_ELECTRONICOS> PER_MEDIOS_ELECTRONICOS { get; set; }
        public DbSet<PER_PERSONAS> PER_PERSONAS { get; set; }
        public DbSet<PRO_PROFESORES> PRO_PROFESORES { get; set; }
        public DbSet<SIS_AADM_MENUS> SIS_AADM_MENUS { get; set; }
        public DbSet<SIS_CAT_TABLAS> SIS_CAT_TABLAS { get; set; }
        public DbSet<US_CAT_ESTATUS_USUARIO> US_CAT_ESTATUS_USUARIO { get; set; }
        public DbSet<US_CAT_NIVEL_USUARIO> US_CAT_NIVEL_USUARIO { get; set; }
        public DbSet<US_CAT_TIPO_USUARIO> US_CAT_TIPO_USUARIO { get; set; }
        public DbSet<US_HISTORIAL> US_HISTORIAL { get; set; }
        public DbSet<US_USUARIOS> US_USUARIOS { get; set; }
    }
}
