using System.Collections.Generic;

namespace PubliPayments.Entidades
{
    public class EntMarcadorGps
    {
        public int Id { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string FechaInicial { get; set; }
        public string FechaFinal { get; set; }
        public string FechaRecepcion { get; set; }
        public string Referencia { get; set; }
    }

    public class EntGrupoMarcadoresGps
    {
        public List<EntMarcadorGps> ListaMarcadores { get; set; }
        public int Color { get; set; }
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
    }
}