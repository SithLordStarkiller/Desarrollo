//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComCredencial
{
    using System;
    using System.Collections.Generic;
    
    public partial class gruposComercios
    {
        public int idGrupo { get; set; }
        public string nombreGrupo { get; set; }
        public string representanteGrupo { get; set; }
        public string emailRepresentante { get; set; }
        public string telefonoRepresentante { get; set; }
        public string tipoTelefono { get; set; }
        public string claveClienteBroxel { get; set; }
        public string numCuentaBroxel { get; set; }
        public Nullable<sbyte> unaCuentaPorGrupo { get; set; }
        public bool AceptaTYCParaTodosComercios { get; set; }
    }
}
