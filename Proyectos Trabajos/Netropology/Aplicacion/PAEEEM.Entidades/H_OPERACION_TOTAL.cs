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
    
    public partial class H_OPERACION_TOTAL
    {
        public int ID_H_OPERACION_TOT { get; set; }
        public string No_Credito { get; set; }
        public byte IDTIPOHORARIO { get; set; }
        public Nullable<int> Id_Credito_Sustitucion { get; set; }
        public Nullable<int> ID_CREDITO_PRODUCTO { get; set; }
        public Nullable<double> HORAS_SEMANA { get; set; }
        public Nullable<double> SEMANAS_AÑO { get; set; }
        public Nullable<double> HORAS_AÑO { get; set; }
        public Nullable<byte> IDCONSECUTIVO { get; set; }
    
        public virtual K_CREDITO_PRODUCTO K_CREDITO_PRODUCTO { get; set; }
        public virtual K_CREDITO_SUSTITUCION K_CREDITO_SUSTITUCION { get; set; }
        public virtual TIPOS_HORARIOS TIPOS_HORARIOS { get; set; }
    }
}
