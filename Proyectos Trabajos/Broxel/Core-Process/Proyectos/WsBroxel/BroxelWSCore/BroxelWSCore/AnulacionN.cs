//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BroxelWSCore
{
    using System;
    using System.Collections.Generic;
    
    public partial class AnulacionN
    {
        public int idAnulacion { get; set; }
        public int idTransaccion { get; set; }
        public Nullable<int> TipoTransaccion { get; set; }
        public Nullable<int> SubTipoTransaccion { get; set; }
        public Nullable<bool> Autorizado { get; set; }
        public Nullable<int> NumAutorizacion { get; set; }
        public Nullable<int> CodigoRespuesta { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string MensajeRespuesta { get; set; }
    }
}