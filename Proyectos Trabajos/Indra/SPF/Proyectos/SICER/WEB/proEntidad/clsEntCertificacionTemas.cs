using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proEntidad
{
    public class clsEntCertificacionTemas
    {

        public int idTema { get; set; }
        public int idCertificacionTema { get; set; }
        public int idCertificacion { get; set; }
        public string temDescripcion { get; set; }
        public string temCodigo { get; set; }
        public bool temVigente { get; set; }
        public byte ctOrden { get; set; }
        public short ctAleatorias { get; set; }
        public short ctCorrectas { get; set; }
        public short ctTiempo { get; set; }
        public int idTematemporal { get; set; }
        public bool ctActivo { get; set; }
        public List<clsEntCertificacionTemas> lstResponse { get; set; }

    }

    
}
