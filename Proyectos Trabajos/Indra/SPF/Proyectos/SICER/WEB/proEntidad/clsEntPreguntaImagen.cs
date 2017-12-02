using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proEntidad
{
    public class clsEntPreguntaImagen
    {
        public int idFuncion { get; set; }
        public int idPregunta { get; set; }
        public string preDescripcion { get; set; } //Se ocupa por si hay algun error
        public string imagen { get; set; }
        
    }
}
