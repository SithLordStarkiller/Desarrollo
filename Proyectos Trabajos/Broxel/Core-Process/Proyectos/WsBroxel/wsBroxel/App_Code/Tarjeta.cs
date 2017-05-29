using System;

namespace wsBroxel.App_Code
{
    [Serializable]
    public class Tarjeta
    {
        public String FechaExpira { get; set; }
        public String Cvc2 { get; set; }
        public String NumeroTarjeta { get; set; }
        public String NombreTarjeta { get; set; }
        public String Cuenta { get; set; }
        public int Procesador { get; set; }
        /*
        public Tarjeta(string nombreTarjeta="", string numTarjeta="", string fechaExpira="", string cvc2="")
        {
            NombreTarjeta = nombreTarjeta;
            NumeroTarjeta = numTarjeta;
            FechaExpira = fechaExpira;
            Cvc2 = cvc2;
        }

        public Tarjeta(string nombreTarjeta="", string numTarjeta="", string fechaExpira="", string cvc2="", string cuenta="")
        {
            NombreTarjeta = nombreTarjeta;
            NumeroTarjeta = numTarjeta;
            FechaExpira = fechaExpira;
            Cvc2 = cvc2;
            Cuenta = cuenta;
        }
        */
        public Tarjeta(string nombreTarjeta = "", string numTarjeta = "", string fechaExpira = "", string cvc2 = "", string cuenta = "", int procesador = 1)
        {
            NombreTarjeta = nombreTarjeta;
            NumeroTarjeta = numTarjeta;
            FechaExpira = fechaExpira;
            Cvc2 = cvc2;
            Cuenta = cuenta;
            Procesador = procesador;
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