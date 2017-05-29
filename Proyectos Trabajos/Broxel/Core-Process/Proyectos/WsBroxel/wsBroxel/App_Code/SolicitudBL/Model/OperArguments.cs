using System.Collections.Generic;

namespace wsBroxel.App_Code.SolicitudBL.Model
{
    public class OperArguments
    {
        public long Folio { set; get; }
        public string NumeroCuenta { set; get; }
        public decimal Monto { set; get; }
        public string IdUser { set; get; }
        public string Password { set; get; }
        public string NumeroTarjeta { set; get; }
        public string Token { set; get; }
        public string IpFrom { set; get; }
        public int IdTransacFrom { set; get; }
        public string ClabeCliente { set; get; }
        public TiposOperacion TipoOper { set; get; }
        public List<string> Cuentas { set; get; }
        public int Locale { set; get; }
        public int IdOperation { set; get; }
        public string Referencia { set; get; }
        public List<string> FolioSolicitudCargo { get; set; }
        public string Producto { get; set; }
        public string Canal { get; set; }
    }
}