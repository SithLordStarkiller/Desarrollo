//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExamenMvcEf.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        public int IdUser { get; set; }
        public int IdTypeUser { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    
        public virtual CatTypeUser CatTypeUser { get; set; }
    }
}
