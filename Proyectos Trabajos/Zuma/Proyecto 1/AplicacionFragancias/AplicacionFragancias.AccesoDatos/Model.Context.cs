﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AplicacionFragancias.Entidades;

namespace AplicacionFragancias.AccesoDatos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FraganciasEntities : DbContext
    {
        public FraganciasEntities()
            : base("name=FraganciasEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ALM_ALMECENES> ALM_ALMECENES { get; set; }
        public virtual DbSet<COM_CAT_ESTATUS_COMPRA> COM_CAT_ESTATUS_COMPRA { get; set; }
        public virtual DbSet<COM_CAT_ESTATUS_PRODUCTO> COM_CAT_ESTATUS_PRODUCTO { get; set; }
        public virtual DbSet<COM_CAT_PRESENTACION> COM_CAT_PRESENTACION { get; set; }
        public virtual DbSet<COM_CAT_TIPO_OPERACION> COM_CAT_TIPO_OPERACION { get; set; }
        public virtual DbSet<COM_CAT_UNIDADES_MEDIDA> COM_CAT_UNIDADES_MEDIDA { get; set; }
        public virtual DbSet<COM_ENTREGAS_PRODUCTO> COM_ENTREGAS_PRODUCTO { get; set; }
        public virtual DbSet<COM_ORDENCOMPRA> COM_ORDENCOMPRA { get; set; }
        public virtual DbSet<COM_PRODUCTOS> COM_PRODUCTOS { get; set; }
        public virtual DbSet<COM_PRODUCTOS_PEDIDOS> COM_PRODUCTOS_PEDIDOS { get; set; }
        public virtual DbSet<COM_PROVEEDORES> COM_PROVEEDORES { get; set; }
        public virtual DbSet<COM_PROVEEDORES_CONTACTOS> COM_PROVEEDORES_CONTACTOS { get; set; }
        public virtual DbSet<CON_CONTACTO> CON_CONTACTO { get; set; }
        public virtual DbSet<DIR_CAT_COLONIAS> DIR_CAT_COLONIAS { get; set; }
        public virtual DbSet<DIR_CAT_DELG_MUNICIPIO> DIR_CAT_DELG_MUNICIPIO { get; set; }
        public virtual DbSet<DIR_CAT_ESTADO> DIR_CAT_ESTADO { get; set; }
        public virtual DbSet<DIR_CAT_TIPO_ASENTAMIENTO> DIR_CAT_TIPO_ASENTAMIENTO { get; set; }
        public virtual DbSet<DIR_CAT_TIPO_ZONA> DIR_CAT_TIPO_ZONA { get; set; }
        public virtual DbSet<DIR_DIRECCIONES> DIR_DIRECCIONES { get; set; }
        public virtual DbSet<FAC_CAT_CONDICIONES_PAGO> FAC_CAT_CONDICIONES_PAGO { get; set; }
        public virtual DbSet<FAC_CAT_IMPUESTO> FAC_CAT_IMPUESTO { get; set; }
        public virtual DbSet<FAC_CAT_MONEDA> FAC_CAT_MONEDA { get; set; }
        public virtual DbSet<FAC_TIPO_CAMBIO_HISTORICO> FAC_TIPO_CAMBIO_HISTORICO { get; set; }
        public virtual DbSet<LOG_COM_OPERACIONES> LOG_COM_OPERACIONES { get; set; }
        public virtual DbSet<LOG_OPERACIONES> LOG_OPERACIONES { get; set; }
        public virtual DbSet<PER_PERSONA> PER_PERSONA { get; set; }
        public virtual DbSet<SIS_MENUARBOL> SIS_MENUARBOL { get; set; }
        public virtual DbSet<SIS_PERFILES_MENU> SIS_PERFILES_MENU { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TEL_TELEFONOS> TEL_TELEFONOS { get; set; }
        public virtual DbSet<US_CAT_PERFILES> US_CAT_PERFILES { get; set; }
        public virtual DbSet<US_PERFILES> US_PERFILES { get; set; }
        public virtual DbSet<US_USUARIOS> US_USUARIOS { get; set; }
    }
}
