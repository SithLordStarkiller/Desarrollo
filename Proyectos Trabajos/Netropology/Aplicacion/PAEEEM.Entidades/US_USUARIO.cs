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
    
    public partial class US_USUARIO
    {
        public US_USUARIO()
        {
            this.ACCIONES_USUARIO = new HashSet<ACCIONES_USUARIO>();
            this.CRE_ValidacionRFC = new HashSet<CRE_ValidacionRFC>();
            this.CRE_ValidacionRFC1 = new HashSet<CRE_ValidacionRFC>();
            this.H_LOG_ACTIVIDAD = new HashSet<H_LOG_ACTIVIDAD>();
            this.K_LOG = new HashSet<K_LOG>();
            this.US_USUARIO_PERMISO = new HashSet<US_USUARIO_PERMISO>();
        }
    
        public int Id_Usuario { get; set; }
        public Nullable<int> Id_Rol { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Contrasena { get; set; }
        public string CorreoElectronico { get; set; }
        public string Numero_Telefono { get; set; }
        public string Nombre_Completo_Usuario { get; set; }
        public string Estatus { get; set; }
        public Nullable<int> Id_Departamento { get; set; }
        public string Tipo_Usuario { get; set; }
        public Nullable<int> ID_VENDEDOR { get; set; }
    
        public virtual ICollection<ACCIONES_USUARIO> ACCIONES_USUARIO { get; set; }
        public virtual ICollection<CRE_ValidacionRFC> CRE_ValidacionRFC { get; set; }
        public virtual ICollection<CRE_ValidacionRFC> CRE_ValidacionRFC1 { get; set; }
        public virtual ICollection<H_LOG_ACTIVIDAD> H_LOG_ACTIVIDAD { get; set; }
        public virtual ICollection<K_LOG> K_LOG { get; set; }
        public virtual US_ROL US_ROL { get; set; }
        public virtual ICollection<US_USUARIO_PERMISO> US_USUARIO_PERMISO { get; set; }
        public virtual VENDEDORES VENDEDORES { get; set; }
    }
}