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
    
    public partial class H_LOG_ACTIVIDAD
    {
        public int Id_Job { get; set; }
        public int Cve_Proceso { get; set; }
        public int Id_Usuario { get; set; }
        public string No_Credito { get; set; }
        public string Id_Usuario_Update { get; set; }
        public Nullable<int> Id_Departamento { get; set; }
        public string Tipo_Departamento { get; set; }
        public string Id_Estatus { get; set; }
        public System.DateTime Dt_Fecha_Update { get; set; }
        public string Dx_Razon { get; set; }
        public string ATB01 { get; set; }
        public string ATB02 { get; set; }
        public string ATB03 { get; set; }
    
        public virtual CAT_PROCESO_LOG CAT_PROCESO_LOG { get; set; }
        public virtual US_USUARIO US_USUARIO { get; set; }
    }
}