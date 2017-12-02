using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mvcSICER.proEntidad
{
    public class clsEntBusqueda
    {
        public string empPaterno { get; set; }
        public string empMaterno { get; set; }
        public string empNombre { get; set; }
        public string empCURP { get; set; }
        public string empRFC { get; set; }
        public string empNumero { get; set; }
        public string partricipante { get; set; }
        public List<clsEntBusqueda> lstResponse { get; set; }
    }
}
