using System;

namespace ComCredencial.App_Code
{
    [Serializable]
    public class Tarjeta
    {
        public String FechaExpira { get; set; }
        public String Cvc2 { get; set; }
        public String NumeroTarjeta { get; set; }
        public String NombreTarjeta { get; set; }
        public String Cuenta { get; set; }

        public Tarjeta(string nombreTarjeta = "", string numTarjeta = "", string fechaExpira = "", string cvc2 = "")
        {
            NombreTarjeta = nombreTarjeta;
            NumeroTarjeta = numTarjeta;
            FechaExpira = fechaExpira;
            Cvc2 = cvc2;
        }

        public Tarjeta(string nombreTarjeta = "", string numTarjeta = "", string fechaExpira = "", string cvc2 = "", string cuenta = "")
        {
            NombreTarjeta = nombreTarjeta;
            NumeroTarjeta = numTarjeta;
            FechaExpira = fechaExpira;
            Cvc2 = cvc2;
            Cuenta = cuenta;
        }

        public Tarjeta()
        {
        }

        public override String ToString()
        {
            return "T: " + NumeroTarjeta + " C: " + Cvc2 + " F: " + FechaExpira + " N: " + NombreTarjeta;
        }
    }
}
