using System;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Entities
{
    public class CotizacionDetalle
    {
        public Cuota Couta { get; set; }
        public Turno Turno { get; set; }
        public int? Cantidad { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? Cancelado { get; set; }
        public bool? AplicaFli { get; set; }
        public bool? Lunes { get; set; }
        public bool? Martes { get; set; }
        public bool? Miercoles { get; set; }
        public bool? Jueves { get; set; }
        public bool? Viernes { get; set; }
        public bool? Sabado { get; set; }
        public bool? Domingo { get; set; }
        public List<GastoInherente> GastoInherentes { get; set; }
        /// <summary>
        /// Solo los servicios de seguridad llevan estado de fuerza.
        /// </summary>
        public EstadoDeFuerza EstadoDeFuerza { get; set; }
    }
}
