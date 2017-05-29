using System;
using System.Collections.Generic;
using System.Linq;

namespace wsBroxel.App_Code.Online
{
    [Serializable]
    public class MovimientoOnline
    {
        public DateTime Fecha { get; set; }
        public String Descripcion { get; set; }
        public String RFC { get; set; }
        public String NumAutorizacion { get; set; }
        public Decimal Cargo { get; set; }
        public String MonedaOriginal { get; set; }
        public String ImpOriginal { get; set; }
        public String NumTarjeta { get; set; }
        // MLS Indicador de TX
        public bool Incremento { get; set; }
        // MLS ReferenciaTransferencia
        public string Referencia { set; get; }
        // JJ Por puto no le puse firma.
        public string Remitente { get; set; }
        public string Destinatario { get; set; }
        public decimal Comision { get; set; }
        public string Concepto { get; set; }
        
        public MovimientoOnline()
        {
            NumAutorizacion = "-";
            Descripcion = "-";
            RFC = "-";
            Descripcion = "-";
            Cargo = 0.0M;
            NumTarjeta = "";
            Referencia = "";
            Remitente = "";
            Destinatario = "";
            Comision = 0;
            Concepto = "";
        }
    }

    [Serializable]
    public class MovimientoOnlineResponse : OnlineResponse
    {
        public List<MovimientoOnline> Movimientos = new List<MovimientoOnline>();
        public int Paginas { get; set; }
        public int PaginaActual { get; set; }

        public MovimientoOnlineResponse()
        {

        }
    }
}