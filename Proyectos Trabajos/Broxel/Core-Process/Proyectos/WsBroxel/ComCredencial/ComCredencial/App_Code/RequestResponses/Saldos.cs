using System;

namespace ComCredencial.RequestResponses
{
    [Serializable]
    public class Saldos
    {
        public Decimal DisponibleAdelantos { get; set; }
        public Decimal DisponibleCompras { get; set; }
        public Decimal DisponibleCuotas { get; set; }
        public Decimal DisponiblePrestamos { get; set; }
        public Decimal LimiteCompra { get; set; }
        public Decimal Saldo { get; set; }
        public Decimal SaldoActual { get; set; }
    }

/*
    [Serializable]
    public class SaldoRequest : Request
    {
        public string NumeroCuenta { get; set; }
    }
*/

    [Serializable]
    public class SaldoResponse : Response
    {
        public Saldos Saldos { get; set; }
        public String EstadoOperativo { get; set; }
        public String UltimaOperacionAprobada { get; set; }

        public SaldoResponse()
        {
            Saldos = new Saldos();
        }

        public override string ToString()
        {
            return "DC: " + Saldos.DisponibleCompras + " DA: " + Saldos.DisponibleAdelantos + " EO: " + EstadoOperativo + " UOA: " + UltimaOperacionAprobada;
        }
    }
}
