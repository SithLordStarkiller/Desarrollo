﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExamenCsi.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ExamenCsiEntities : DbContext
    {
        public ExamenCsiEntities()
            : base("name=ExamenCsiEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        //public virtual DbSet<UsCatTipoUsuario> UsCatTipoUsuario { get; set; }
        //public virtual DbSet<UsUsuario> UsUsuario { get; set; }
    
        public virtual ObjectResult<Nullable<decimal>> Usp_UsUsuarioInsertar(string usuario, string contrasena, Nullable<int> idTipoUsuario)
        {
            var usuarioParameter = usuario != null ?
                new ObjectParameter("Usuario", usuario) :
                new ObjectParameter("Usuario", typeof(string));
    
            var contrasenaParameter = contrasena != null ?
                new ObjectParameter("Contrasena", contrasena) :
                new ObjectParameter("Contrasena", typeof(string));
    
            var idTipoUsuarioParameter = idTipoUsuario.HasValue ?
                new ObjectParameter("IdTipoUsuario", idTipoUsuario) :
                new ObjectParameter("IdTipoUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("Usp_UsUsuarioInsertar", usuarioParameter, contrasenaParameter, idTipoUsuarioParameter);
        }
    
        public virtual ObjectResult<Usp_UsUsuarioObtenerTodos_Result> Usp_UsUsuarioObtenerTodos()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Usp_UsUsuarioObtenerTodos_Result>("Usp_UsUsuarioObtenerTodos");
        }
    }
}
