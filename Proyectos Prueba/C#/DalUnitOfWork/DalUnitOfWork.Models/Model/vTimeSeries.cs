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
    
    public partial class vTimeSeries
    {
        public string ModelRegion { get; set; }
        public Nullable<int> TimeIndex { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public short CalendarYear { get; set; }
        public byte Month { get; set; }
        public Nullable<System.DateTime> ReportingDate { get; set; }
    }
}
