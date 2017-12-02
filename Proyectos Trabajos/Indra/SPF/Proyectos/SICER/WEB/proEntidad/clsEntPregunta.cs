using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proEntidad
{
    public class clsEntPregunta
    {

       public int idPregunta { get; set; }
       public int idFuncion { get; set; }
       public string preDescripcion { get; set; }
       public string  preCodigo { get; set; }
       public bool preActiva { get; set; }
       public bool preObligatoria { get; set; }
       public string preTipoArchivo { get; set; }
       public string preNombreArchivo{ get; set; }
       public string identificadorImagen{ get; set; }
       public string imagen{ get; set; }
       public bool preVigente { get; set; }
       public int idFuncionTemporal { get; set; }
       public int idPreguntaTemporal { get; set; }
    }
}
