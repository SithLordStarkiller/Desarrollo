//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace srvBroxel.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class UsuariosOnlineBroxel
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string NombreCompleto { get; set; }
        public string Sexo { get; set; }
        public string CorreoElectronico { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public string RFC { get; set; }
        public string Telefono { get; set; }
        public string CP { get; set; }
        public string Celular { get; set; }
        public bool Activo { get; set; }
        public string palabraClave { get; set; }
    }
}
