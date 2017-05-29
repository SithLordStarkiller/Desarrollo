using System;
using System.Collections.Generic;

namespace ComCredencial.RequestResponses
{
    [Serializable]
    public class Movimiento
    {
        public String Tarjeta { get; set; }
        public String Comercio { get; set; }
        public String Fecha { get; set; }
        public String Monto { get; set; }
        public Boolean Aprobada { get; set; }
        public String Moneda { get; set; }
        public String MensajeRespuesta { get; set; }
    }

    [Serializable]
    public class MovimientosRequest : Request
    {

    }

    [Serializable]
    public class MovimientosResponse : Response
    {
        public List<Movimiento> Movimientos { get; set; }
        public int Paginas { get; set; }
        public int PaginaActual { get; set; }

        public MovimientosResponse()
        {
            Movimientos = new List<Movimiento>();
        }
    }
}
