using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code
{
    public class TarjetaEmbosado
    {
        public String NumeroTarjeta { get; set; }
        public String DenominacionPlastico { get; set; }
        public String NombreTarjetahabiente { get; set; }
        public String FechaExpiracion { get; set; } //MMAA
        public String CVC1 { get; set; } //Aparece en el track
        public String CVC2 { get; set; } //Aparece en el inverso
        public String CodigoProducto { get; set; }
        public String CuartaLinea { get; set; }
        public int TitularAdicional { get; set; } //0 o 1
    }
}