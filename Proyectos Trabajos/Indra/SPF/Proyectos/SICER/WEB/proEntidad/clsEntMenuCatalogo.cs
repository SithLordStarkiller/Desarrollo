using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proEntidad
{
    public class clsEntMenuCatalogo
    {
        public string menDescripcion { get; set; }
        public int idMenu { get; set; }
        public bool activo { get; set; }
        public bool pmCaptura { get; set; }
        public bool pmModifica { get; set; }
        public string menPadre { get; set; }
        public bool tipo { get; set; }
        public string menIcon { get; set; }
        public string menIconContenedor { get; set; }
    }
}
