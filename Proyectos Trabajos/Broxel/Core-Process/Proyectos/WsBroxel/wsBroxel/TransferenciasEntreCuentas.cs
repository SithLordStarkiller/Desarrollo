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
    
    public partial class TransferenciasEntreCuentas
    {
        public int Id { get; set; }
        public int IdMovimiento { get; set; }
        public int IdComercio { get; set; }
        public int IdUsuario { get; set; }
        public string NumCuentaOrigen { get; set; }
        public string NumCuentaDestino { get; set; }
        public decimal SaldoOrigenAntes { get; set; }
        public decimal SaldoOrigenDespues { get; set; }
        public decimal SaldoDestinoAntes { get; set; }
        public decimal SaldoDestinoDespues { get; set; }
        public decimal Comision { get; set; }
    }
}
