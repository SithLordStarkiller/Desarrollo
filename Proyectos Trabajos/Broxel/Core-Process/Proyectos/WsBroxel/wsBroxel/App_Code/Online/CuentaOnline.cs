using System;

namespace wsBroxel.App_Code.Online
{
    [Serializable]
    public class CuentaOnline
    {
        public String NumTarjeta { get; set; }
        public String NombreCuenta{get;set; }
        public String NumCuenta { get; set; }
        public String TipoCuenta { get; set; }
        public String Prod { get; set; }
        public String TipoProd { get; set; }
        public Boolean DisponeCredito { get; set; }
        public Boolean TransfiereOnline { get; set; }
        public Boolean RecibeTransferencia { get; set; }
        public Boolean CambiaNIP { get; set; }

        public String EstadoOperativo { get; set; }
        //public SaldoOnlineResponse Saldos { get; set; }
        // MLS Cambio 3 Mostrar CLABE
        public String CLABE { set; get; }
        public String Alias { set; get; }
        public bool PagarServicios { get; set; }
        public bool P2P_Activo { get; set; }
        public bool PermiteP2P { get; set; }
        // MLS Cambio 3 Mostrar CLABE
        public int MostrarDatosTarjeta { set; get; }
        public CuentaOnline()
        {
            DisponeCredito = false;
            TransfiereOnline = false;
            RecibeTransferencia = false;
            TipoProd = "E";
        }
    }
}