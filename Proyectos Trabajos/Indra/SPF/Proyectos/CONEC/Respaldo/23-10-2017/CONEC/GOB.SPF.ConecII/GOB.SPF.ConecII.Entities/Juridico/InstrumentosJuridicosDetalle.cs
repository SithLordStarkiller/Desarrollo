using System;
using Newtonsoft.Json;

namespace GOB.SPF.ConecII.Entities.Juridico
{
    public class InstrumentosJuridicosDetalle : TEntity
    {
        public TipoServicio TipoServicio { get; set; }
        public TiposPago TipoPago { get; set; }
        public Periodo PeriodoGeneracionRecibo { get; set; }
        public Periodo IdPeriodoGeneracionReporte { get; set; }
        public int TiempoValidacionCliente { get; set; }
        public int TiempoValidacionAreaValidadora { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime? FechaContinuacion { get; set; }
        public bool? Activo { get; set; }
    }
}
