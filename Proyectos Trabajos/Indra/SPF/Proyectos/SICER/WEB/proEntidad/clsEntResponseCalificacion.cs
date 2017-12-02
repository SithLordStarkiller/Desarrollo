using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proEntidad
{
    public class clsEntResponseCalificacion
    {
        public int idTema { get; set; }
        public int numero { get; set; }//Numeración de los temas
        public int preguntasCorrectas { get; set; }
        public int preguntasNecesarias { get; set; }
        public int preguntasPresentadas { get; set; }

        //public int cantidadPreguntas { get; set; }
        public string alerta { get; set; }

    }
}
