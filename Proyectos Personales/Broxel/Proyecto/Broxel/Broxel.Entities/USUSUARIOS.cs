//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Broxel.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class USUSUARIOS
    {
        public int IDUSUARIO { get; set; }
        public Nullable<int> IDESTATUS { get; set; }
        public string USUARIO { get; set; }
        public string CONTRASENA { get; set; }
    
        public virtual USESTATUS USESTATUS { get; set; }
    }
}
