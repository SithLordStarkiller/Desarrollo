//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComCredencial
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transferencias
    {
        public int Id { get; set; }
        public string ClaveRastreo { get; set; }
        public string NumCuenta { get; set; }
        public string ReferenciaPago { get; set; }
        public string Empresa { get; set; }
        public string Banco { get; set; }
        public string Clabe { get; set; }
        public string NombreBeneficiario { get; set; }
        public Nullable<decimal> Monto { get; set; }
        public string RFC { get; set; }
        public string Email { get; set; }
        public string Motivo { get; set; }
        public Nullable<System.DateTime> FechaPago { get; set; }
        public Nullable<int> NumEnvio { get; set; }
        public string Status { get; set; }
        public Nullable<long> IdDisposicion { get; set; }
        public string ConceptoPago { get; set; }
        public Nullable<int> ReferenciaNumerica { get; set; }
        public string CuentaOrdenante { get; set; }
        public string RfcCurpOrdenante { get; set; }
    }
}
