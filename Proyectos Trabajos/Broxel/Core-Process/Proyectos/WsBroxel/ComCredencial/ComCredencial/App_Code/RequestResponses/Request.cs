using System;

using ComCredencial.App_Code;

namespace ComCredencial.RequestResponses
{
    public abstract class Request
    {
        private string _nombreSolicitante;

        public Tarjeta Tarjeta { get; set; }
        public Int32 UserID { get; set; }

        public String Accion { get; set; }
        public String NumCuenta { get; set; }

        public String Solicitante
        {
            get
            {
                if (_nombreSolicitante == null)
                    return "000";
                if (_nombreSolicitante.Length < 3)
                    return _nombreSolicitante.PadLeft(3).Replace(' ', '0');
                if (_nombreSolicitante.Length >= 21)
                    return _nombreSolicitante.Substring(0, 21);
                return _nombreSolicitante;
            }
            set { _nombreSolicitante = value; }
        }
    }

    [Serializable]
    public class TransferenciaRequest : Request
    {
        public Tarjeta TarjetaRecibe { get; set; }
        public Decimal Monto { get; set; }
        public Int32 idComercio { get; set; }
        public Decimal Comision { get; set; }
        public Int32 TipoConceptoComision { get; set; }
    }

    [Serializable]
    public class NIPRequest : Request
    {
        public String NIPNuevo { get; set; }
    }
}
