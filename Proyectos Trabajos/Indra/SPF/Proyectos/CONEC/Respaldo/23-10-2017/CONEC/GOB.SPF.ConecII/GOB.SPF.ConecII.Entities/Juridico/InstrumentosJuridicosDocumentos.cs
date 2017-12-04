using System;

namespace GOB.SPF.ConecII.Entities.Juridico
{
    public class InstrumentosJuridicosDocumentos : TEntity
    {
        public InstrumentosJuridicosDetalle InstrumentosJuridicosDetalle { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public Estatus Estatus { get; set; }
        public int DocumentoSoporte { get; set; }
        public DateTime? FechaCarga { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        public bool? Activo { get; set; }
    }
}
