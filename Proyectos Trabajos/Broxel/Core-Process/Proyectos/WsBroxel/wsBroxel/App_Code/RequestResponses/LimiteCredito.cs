using System;

namespace wsBroxel.App_Code
{
    public class LimiteRequest : Request
    {
        public Decimal Limite { get; set; }
        public Int32 Tipo { get; set; } // Limite Compra=0, ATM = 3,
    }

    public class LimiteResponse : App_Code.Response
    {
        public Int32 IdMovimiento { get; set; }
        public Decimal SaldoAntes { get; set; }
        public Decimal SaldoDespues { get; set; }
    }
}
