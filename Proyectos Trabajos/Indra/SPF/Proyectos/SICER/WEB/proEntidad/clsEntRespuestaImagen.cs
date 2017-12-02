using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proEntidad
{
   public class clsEntRespuestaImagen
    {
        public int idFuncion { get; set; }
        public int idRespuesta { get; set; }
        public string resDescripcion { get; set; } //Se ocupa por si hay algun error
        public string imagen { get; set; }
       // public string resExplicacion { get; set; }
       // public bool resCorrecta { get; set; }
       // public bool resActiva { get; set; }
       // public string resTipoArchivo { get; set; }
       // public string resNombreArchivo { get; set; }
       // public bool resVigente { get; set; }
       // public int idPreguntaTemporal { get; set; }
       // public int idRespuestaTemporal { get; set; }

    }
}

