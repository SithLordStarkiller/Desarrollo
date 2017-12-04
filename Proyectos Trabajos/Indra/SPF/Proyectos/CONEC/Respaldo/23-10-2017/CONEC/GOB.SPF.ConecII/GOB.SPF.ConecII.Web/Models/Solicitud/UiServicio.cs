using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using GOB.SPF.ConecII.Web.Models.Solicitud;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiServicio : UiEntity
    {
        public int Identificador { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Integrantes { get; set; }
        public List<UiServicioDocumento> Documentos { get; set; }
        public int NumeroPersonas { get; set; }
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string Observaciones { get; set; }
        public bool TieneComite { get; set; }
        public string ObservacionesComite { get; set; }
        public string BienCustodia { get; set; }
        public bool Viable { get; set; }
        public DateTime FechaComite { get; set; }
        public UiCuota Cuota { get; set; }
        public UiTipoServicio TipoServicio { get; set; }
        public int HorasCurso { get; set; }
        public UiTipoInstalacionesCapacitacion TipoInstalacionesCapacitacion { get; set; }
        public List<UiAsistente> Asistentes { get; set; }
        public List<UiAcuerdo> Acuerdos { get; set; }
        public List<UiInstalacion> Instalaciones { get; set; }
        public UiEstatus Estatus { get; set; }
        public List<UiServicioInstalacion> ServicioInstalaciones { get; set; }
    }
}