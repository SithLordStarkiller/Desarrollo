using System;

namespace wsBroxel.App_Code
{
    [Serializable]
    public class Cargo
    {
        public int Indice { get; set; }
        public int IdMovimiento { get; set; }
        public Decimal Monto { get; set; }
        public String MontoCFormato { get; set; }
        public String NoReferencia { get; set; }
        public String NombreReferencia { get; set; }
        public Int32 IdUsuario { get; set; }
        public Int32 IdTipoMovimiento { get; set; }
        public Decimal MontoDisponible { get; set; }
        public Int32 IdComercio { get; set; }
        public bool Autorizado { get; set; }
        public Int32 IdLote { get; set; }
        public Int32 IdUsuarioCallCenter { get; set; }
    }

    [Serializable]
    public class CargoRequest : Request
    {
        public Cargo Cargo { get; set; }
    }

    [Serializable]
    public class CargoResponse : App_Code.Response
    {
        public Int32 IdMovimiento { get; set; }
        public Decimal Saldo { get; set; }
    }

    [Serializable]
    public class CargoEditRequest : Request
    {
        public int IdCargo { get; set; }
        public new Tarjeta Tarjeta { get; set; }

    }

    [Serializable]
    public class CargoDeleteResponse : App_Code.Response
    {
        public Int32 IdAnulacion { get; set; }
    }
}