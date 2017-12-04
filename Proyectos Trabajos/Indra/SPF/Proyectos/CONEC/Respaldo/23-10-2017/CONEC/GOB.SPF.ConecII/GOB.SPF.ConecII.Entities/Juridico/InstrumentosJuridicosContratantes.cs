namespace GOB.SPF.ConecII.Entities.Juridico
{
    public class InstrumentosJuridicosContratantes : TEntity
    {
        public InstrumentosJuridicosDetalle InstrumentosJuridicosDetalle { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public Externo Contratante { get; set; }
        public bool? Activo { get; set; }
    }
}