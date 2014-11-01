//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PAEEEM.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class MRV_HORARIOS_OPERACION
    {
        public int IdHorarioOperacion { get; set; }
        public int IdCuestionario { get; set; }
        public Nullable<int> IdEquipoCuestionario { get; set; }
        public byte IDTIPOHORARIO { get; set; }
        public byte ID_DIA_OPERACION { get; set; }
        public string Hora_Inicio { get; set; }
        public Nullable<decimal> Horas_Laborales { get; set; }
        public string ValorHorario { get; set; }
        public Nullable<bool> Estatus { get; set; }
        public Nullable<System.DateTime> FechaAdicion { get; set; }
        public string AdicionadoPor { get; set; }
    
        public virtual MRV_CUESTIONARIO MRV_CUESTIONARIO { get; set; }
        public virtual MRV_EQUIPOS_CUESTIONARIO MRV_EQUIPOS_CUESTIONARIO { get; set; }
        public virtual DIAS_OPERACION DIAS_OPERACION { get; set; }
        public virtual TIPOS_HORARIOS TIPOS_HORARIOS { get; set; }
    }
}
