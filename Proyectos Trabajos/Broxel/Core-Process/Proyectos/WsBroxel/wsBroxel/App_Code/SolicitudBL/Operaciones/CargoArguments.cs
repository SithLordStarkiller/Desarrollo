using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.SolicitudBL.Operaciones
{
    public class CargoArguments
    {
        public string NumeroTarjeta { set; get; }
        public string Cvv2 { set; get; }
        public string NumeroCaja { set; get; }
        public string NumeroTienda { set; get; }
        public DateTime FechaOperacionOrigen { set; get; }
        public string TransaccionPos { set; get; }
        public Decimal Monto { set; get; }
        public string Folio { set; get; }
        public string Cuenta { set; get; }
        public string IdUser { set; get; }
        public int TipoOperacion { set; get; }
        public string AutOrigen { set; get; }
        public string FechaVigencia { set; get; }
    }
}