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
    
    public partial class TR_PARAMETROS_GLOBALES
    {
        public int IDPARAMETRO { get; set; }
        public int IDSECCION { get; set; }
        public string DESCRIPCION { get; set; }
        public string VALOR { get; set; }
        public bool ESTATUS { get; set; }
        public System.DateTime FECHA_ADICION { get; set; }
        public string ADICIONADO_POR { get; set; }
        public Nullable<bool> PARAMETRO_MODIFICABLE { get; set; }
    
        public virtual GL_SECCIONES GL_SECCIONES { get; set; }
    }
}
