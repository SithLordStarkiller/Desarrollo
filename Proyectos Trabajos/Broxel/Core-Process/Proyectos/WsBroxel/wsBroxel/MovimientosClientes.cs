//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace wsBroxel
{
    using System;
    using System.Collections.Generic;
    
    public partial class MovimientosClientes
    {
        public long Id { get; set; }
        public Nullable<int> IdDetalleCliente { get; set; }
        public Nullable<int> IdComision { get; set; }
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public string CodigoComision { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }
        public Nullable<System.DateTime> FechaHora { get; set; }
        public string UsuarioCreacion { get; set; }
        public Nullable<double> SubTotal { get; set; }
        public Nullable<double> IVA { get; set; }
        public Nullable<double> Total { get; set; }
        public string FolioDispersion { get; set; }
    }
}
