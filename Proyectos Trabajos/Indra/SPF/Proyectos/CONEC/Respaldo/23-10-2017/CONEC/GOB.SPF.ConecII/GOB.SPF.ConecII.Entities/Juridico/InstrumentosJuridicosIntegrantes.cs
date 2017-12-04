using System;

namespace GOB.SPF.ConecII.Entities.Juridico
{
    public class InstrumentosJuridicosIntegrantes : TEntity
    {
        public InstrumentosJuridicosDetalle InstrumentoJuridicoDetalle { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public Guid Integrante { get; set; }
        public Guid Cargo { get; set; }
        public Jerarquia Jerarquia { get; set; }
        public bool? EsFirmanteSuplente { get; set; }
        public bool? EsJefeFirmante { get; set; }
        public bool? Activo { get; set; }
        public TipoAccion TipoAccion { get; set; }
    }
}
