using System;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Entities.Juridico
{
    public class InstrumentosJuridicos : TEntity
    {
        public int? IdInstrumentoJuridicoAnterior { get; set; }
        public Cotizacion Cotizacion { get; set; }
        public Externo Solicitante { get; set; }
        public Estatus Estatus { get; set; }
        public string Nombre { get; set; }
        public string NumeroInstrumento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime? FechaEnvioCliente { get; set; }
        public DateTime? FechaRecepcionCliente { get; set; }
        public decimal? MontoMaximo { get; set; }
        public decimal? MontoMinimo { get; set; }
        public decimal? MontoFijo { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public MotivosCancelacion MotivosCancelacion { get; set; }
        public TiposContratos TiposContratos { get; set; }
        public TiposInstrumentoJuridico TiposInstrumentoJuridico { get; set; }
        public List<InstrumentosJuridicosDetalle> InstrumentosJuridicosDetalle { get; set; }
        public List<InstrumentosJuridicosObservaciones> InstrumentosJuridicosObservaciones { get; set; }
    }
}
