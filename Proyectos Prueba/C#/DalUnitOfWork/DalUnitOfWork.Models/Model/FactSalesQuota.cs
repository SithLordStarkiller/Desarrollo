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
    
    public partial class FactSalesQuota
    {
        public int SalesQuotaKey { get; set; }
        public int EmployeeKey { get; set; }
        public int DateKey { get; set; }
        public short CalendarYear { get; set; }
        public byte CalendarQuarter { get; set; }
        public decimal SalesAmountQuota { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual DimDate DimDate { get; set; }
        public virtual DimEmployee DimEmployee { get; set; }
    }
}
