using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Servicio : Genericos.SubRegistro<int>
    {
        public int Identificador { get; set; }
        public int IdSolicitud { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Integrantes { get; set; }
        public List<ServicioDocumento> Documentos { get; set; }
        public ServicioDocumento Documento { get; set; }
        public int NumeroPersonas { get; set; }
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string Observaciones { get; set; }
        public bool TieneComite { get; set; }
        public string ObservacionesComite { get; set; }
        public string BienCustodia { get; set; }
        public bool Viable { get; set; }
        public bool? Cancelado { get; set; }
        public DateTime? FechaComite { get; set; }
        public Cuota Cuota { get; set; }
        public TipoServicio TipoServicio { get; set; }
        public int HorasCurso { get; set; }
        public TipoInstalacionesCapacitacion TipoInstalacionesCapacitacion { get; set; }
        public List<Asistente> Asistentes { get; set; }
        public List<Acuerdo> Acuerdos { get; set; }
        public List<ServicioInstalacion> Instalaciones { get; set; }
        public Estatus Estatus { get; set; }
    }
}
