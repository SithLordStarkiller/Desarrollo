using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PubliPayments.Entidades.Originacion
{
    class PlazoModel
    {
        public string GastosApertura { get; set; }
        public string NombreTitular { get; set; }
        public string Plazo { get; set; }
        public string Contarias { get; set; }
        public string Montocredito { get; set; }
        public string PagoMensual { get; set; }
        public string MontoManoObra { get; set; }
        public string NumRegistroPatronal { get; set; }
        public string PuntMin { get; set; }
        public string PuntTotal { get; set; }
        public string Curp { get; set; }
        public string Rfc{ get; set; }
        public string NombreEmpresa { get; set; }

        public PlazoModel(Dictionary<String,String> respuesta)
        {
            var plazoSelec = 0;



        }
    }
}
