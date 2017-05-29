using System;

namespace wsBroxel.App_Code.Online
{
    [Serializable]
    public class SaldoOnlineResponse 
    {
        public DateTime DiaCorte { get; set; }
        public Double PagoMinimo { get; set; }
        public DateTime FechaLimitePago { get; set; }
        public Decimal LimiteCredito { get; set; }
        public Decimal CreditoDisp { get; set; }
        public Decimal Saldo { get; set; }
        public Boolean EstadoOperativo {get; set; }
        public Decimal SaldoActual { get; set; }
        public Decimal SaldoAlCorte { get; set; }
        public Boolean Success { get; set; }
    }
}