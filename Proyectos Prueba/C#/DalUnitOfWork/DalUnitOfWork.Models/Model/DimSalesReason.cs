//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DalUnitOfWork.Models.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class DimSalesReason
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DimSalesReason()
        {
            this.FactInternetSales = new HashSet<FactInternetSales>();
        }
    
        public int SalesReasonKey { get; set; }
        public int SalesReasonAlternateKey { get; set; }
        public string SalesReasonName { get; set; }
        public string SalesReasonReasonType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FactInternetSales> FactInternetSales { get; set; }
    }
}
