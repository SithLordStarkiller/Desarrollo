using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proEntidad
{
    public class clsEntTemaFuncion
    {
       public int  idFuncion { get; set; }
       public string funNombre { get; set; }
       public short funAleatorias { get; set; }
       public short funCorrectas { get; set; }
       public short funTiempo { get; set; }
       public byte funOrden { get; set; }
       public string funCodigo { get; set; }
       public bool tfActivo  { get; set; }
        public int idFunciontemporal { get; set; }
        public int idTematemporal { get; set; }
        public int idTema { get; set; }
        public int idTemaFuncion { get; set; }
        public List<clsEntTemaFuncion> lstResponse { get; set; }
    }
}
