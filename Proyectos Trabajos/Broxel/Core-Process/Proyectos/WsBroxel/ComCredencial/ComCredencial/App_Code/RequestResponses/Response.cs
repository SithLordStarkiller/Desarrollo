using System;

namespace ComCredencial.RequestResponses
{
    public abstract class Response
    {
        private bool _success;

        public Response()
        {
            CodigoRespuesta = 999;
            UserResponse = "ERROR AL PROCESAR LA TRANSACCION";
            NumeroAutorizacion = "-1";
        }

        public Int32 Success
        {
            get { return _success ? 1 : 0; }
            set { _success = value == 1; }
        }

        public String NumeroAutorizacion { get; set; }
        public Int32 CodigoRespuesta { get; set; }
        public String UserResponse { get; set; }
        public String FechaCreacion { get; set; }
    }

    [Serializable]
    public class TransferenciaResponse : Response
    {
        public Int32 IdMovimiento { get; set; }
        public Decimal SaldoOrigenAntes { get; set; }
        public Decimal SaldoOrigenDespues { get; set; }
        public Decimal SaldoDestinoAntes { get; set; }
        public Decimal SaldoDestinoDespues { get; set; }
        public ComisionTransferencia Comision { get; set; }
    }

    [Serializable]
    public class NIPResponse : Response
    {

    }
}
