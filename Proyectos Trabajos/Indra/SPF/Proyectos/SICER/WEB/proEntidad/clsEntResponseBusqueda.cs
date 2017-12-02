using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proEntidad
{
    public class clsEntResponseBusqueda
    {
        public List<spuConsultarDatosIntegranteInternoExterno_Result> lstResponse { get; set; }
        public string alerta { get; set; }
        public int total { get; set; }
    }
}
