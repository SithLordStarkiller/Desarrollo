using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Servicios
    {
        public int Identificador { get; set; }
        public int IdTipoServicio { get; set; }
        public int IdCuota { get; set; }
        public int IdTipoInstalacion { get; set; }
        public int NumeroPersonas { get; set; }
        public int HorasCurso { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Observaciones { get; set; }
        public bool TieneComite { get; set; }
        public string ObservacionesComite { get; set; }
        public string BienCustodia { get; set; }
        public DateTime FechaComite { get; set; }
    }
}
