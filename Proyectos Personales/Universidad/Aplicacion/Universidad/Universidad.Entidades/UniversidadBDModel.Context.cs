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
        public DbSet<AUL_AULA_CLASES> AUL_AULA_CLASES { get; set; }
        public DbSet<AUL_CAT_TIPO_AULA> AUL_CAT_TIPO_AULA { get; set; }
        public DbSet<CAL_ALUMNO_KARDEX> CAL_ALUMNO_KARDEX { get; set; }
        public DbSet<CAL_CALIFICACION_CLASE> CAL_CALIFICACION_CLASE { get; set; }
        public DbSet<CAL_CALIFICACIONES> CAL_CALIFICACIONES { get; set; }
        public DbSet<CAL_CALIFICACIONES_FECHAS> CAL_CALIFICACIONES_FECHAS { get; set; }
        public DbSet<CAL_CAT_TIPO_EVALUACION> CAL_CAT_TIPO_EVALUACION { get; set; }
        public DbSet<CAR_CAT_CARRERAS> CAR_CAT_CARRERAS { get; set; }
        public DbSet<CAR_CAT_ESPECIALIDAD> CAR_CAT_ESPECIALIDAD { get; set; }
        public DbSet<CLA_CLASE> CLA_CLASE { get; set; }
        public DbSet<CLA_HORARIO> CLA_HORARIO { get; set; }
        public DbSet<DIR_CAT_COLONIAS> DIR_CAT_COLONIAS { get; set; }
        public DbSet<DIR_CAT_DELG_MUNICIPIO> DIR_CAT_DELG_MUNICIPIO { get; set; }
        public DbSet<DIR_CAT_ESTADO> DIR_CAT_ESTADO { get; set; }
        public DbSet<DIR_CAT_TIPO_ASENTAMIENTO> DIR_CAT_TIPO_ASENTAMIENTO { get; set; }
        public DbSet<DIR_CAT_TIPO_ZONA> DIR_CAT_TIPO_ZONA { get; set; }
        public DbSet<DIR_DIRECCIONES> DIR_DIRECCIONES { get; set; }
        public DbSet<GEN_CAT_SEMESTRE_PERIODOS> GEN_CAT_SEMESTRE_PERIODOS { get; set; }
        public DbSet<HOR_CAT_DIAS_FESTIVOS> HOR_CAT_DIAS_FESTIVOS { get; set; }
        public DbSet<HOR_CAT_DIAS_SEMANA> HOR_CAT_DIAS_SEMANA { get; set; }
        public DbSet<HOR_CAT_HORAS> HOR_CAT_HORAS { get; set; }
        public DbSet<HOR_CAT_TIPO_DIA_FERIADO> HOR_CAT_TIPO_DIA_FERIADO { get; set; }
        public DbSet<HOR_CAT_TURNO> HOR_CAT_TURNO { get; set; }
        public DbSet<HOR_HORAS_POR_DIA> HOR_HORAS_POR_DIA { get; set; }
        public DbSet<MAT_ARBOL_MATERIA> MAT_ARBOL_MATERIA { get; set; }
        public DbSet<MAT_CAT_CREDITOS_POR_HORAS> MAT_CAT_CREDITOS_POR_HORAS { get; set; }
        public DbSet<MAT_CAT_MATERIAS> MAT_CAT_MATERIAS { get; set; }
        public DbSet<MAT_HORARIO_POR_MATERIA> MAT_HORARIO_POR_MATERIA { get; set; }
        public DbSet<MAT_MATERIAS_POR_CARRERA> MAT_MATERIAS_POR_CARRERA { get; set; }
        public DbSet<PER_CAT_NACIONALIDAD> PER_CAT_NACIONALIDAD { get; set; }
        public DbSet<PER_CAT_TELEFONOS> PER_CAT_TELEFONOS { get; set; }
        public DbSet<PER_CAT_TIPO_PERSONA> PER_CAT_TIPO_PERSONA { get; set; }
        public DbSet<PER_FOTOGRAFIA> PER_FOTOGRAFIA { get; set; }
        public DbSet<PER_MEDIOS_ELECTRONICOS> PER_MEDIOS_ELECTRONICOS { get; set; }
        public DbSet<PER_PERSONAS> PER_PERSONAS { get; set; }
        public DbSet<PRO_PROFESOR> PRO_PROFESOR { get; set; }
        public DbSet<SIS_AADM_APLICACIONES> SIS_AADM_APLICACIONES { get; set; }
        public DbSet<SIS_AADM_ARBOLMENUS> SIS_AADM_ARBOLMENUS { get; set; }
        public DbSet<SIS_CAT_TABPAGES> SIS_CAT_TABPAGES { get; set; }
        public DbSet<SIS_WADM_ARBOLMENU_MVC> SIS_WADM_ARBOLMENU_MVC { get; set; }
        public DbSet<SIS_WADM_PERMISOS_ARBOLMENU_MVC> SIS_WADM_PERMISOS_ARBOLMENU_MVC { get; set; }
        public DbSet<US_CAT_ESTATUS_USUARIO> US_CAT_ESTATUS_USUARIO { get; set; }
        public DbSet<US_CAT_NIVEL_USUARIO> US_CAT_NIVEL_USUARIO { get; set; }
        public DbSet<US_CAT_TIPO_USUARIO> US_CAT_TIPO_USUARIO { get; set; }
        public DbSet<US_USUARIOS> US_USUARIOS { get; set; }
    }
}
