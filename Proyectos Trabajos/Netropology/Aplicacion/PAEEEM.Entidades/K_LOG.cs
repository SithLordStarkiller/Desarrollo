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
    
    public partial class K_LOG
    {
        public decimal IDSECUENCIA { get; set; }
        public System.DateTime FECHA_ADICION { get; set; }
        public int IDUSUARIO { get; set; }
        public int IDROL { get; set; }
        public Nullable<int> IDEMPRESA { get; set; }
        public Nullable<int> IDREGION { get; set; }
        public Nullable<int> IDZONA { get; set; }
        public byte IDTIPOPROCESO { get; set; }
        public byte IDTAREA { get; set; }
        public string TAREA_LOTE_NOCRED { get; set; }
        public string MOTIVO { get; set; }
        public string NOTA { get; set; }
        public string DATOSANTERIORES { get; set; }
        public string DATOSACTUALES { get; set; }
        public Nullable<int> Secuencia_E_Alta { get; set; }
        public Nullable<int> Secuencia_E_Baja { get; set; }
    
        public virtual CAT_TAREAS_PROCESOS CAT_TAREAS_PROCESOS { get; set; }
        public virtual US_ROL US_ROL { get; set; }
        public virtual US_USUARIO US_USUARIO { get; set; }
    }
}