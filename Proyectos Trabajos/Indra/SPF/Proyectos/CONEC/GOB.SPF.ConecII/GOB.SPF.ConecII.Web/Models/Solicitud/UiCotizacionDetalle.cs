using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models.Solicitud
{
    public class UiCotizacionDetalle
    {
        public UiCuota Couta { get; set; }
        public UiTurno Turno { get; set; }
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
        public List<UiGastosInherente> GastoInherentes { get; set; }

        /// <summary>
        /// Solo los servicios de seguridad llevan estado de fuerza.
        /// </summary>
        public UiEstadoDeFuerza EstadoDeFuerza { get; set; }
    }
}