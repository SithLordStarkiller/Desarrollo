using System;
using System.ComponentModel.DataAnnotations;

namespace PubliPayments.Entidades
{
    public class ValorRespuesta
    {
        [Key]
        public int IdOrden { get; set; }
        public int IdCampo { get; set; }
        public String Valor { get; set; }
        public String FechaRecepcion { get; set; }
        public String NombreCampo { get; set;}

        public override string ToString()
        {
            return FechaRecepcion+"|"+IdCampo+"|"+IdOrden+"|"+Valor+"|"+NombreCampo;
        }
    }

    public class DiccionarioDictamenes
    {
        [Key]
        public int IdCampo { get; set; }
        public String Nombre { get; set; }
    }
}