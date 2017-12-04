namespace GOB.SPF.ConecII.Entities
{
    public class ConfiguracionCampoServicio : TEntity
    {
        public int IdTipoServicio { get; set; }
        public bool Cuota { get; set; }
        public bool TipoInstalacion { get; set; }
        public bool NumeroPersonas { get; set; }
        public bool HorasCurso { get; set; }
        public bool FechaInicial { get; set; }
        public bool FechaFinal { get; set; }
        public bool BienCustodia { get; set; }
        public bool Observaciones { get; set; }
        public bool InstalacionesCliente { get; set; }
        public bool Documentos { get; set; }
        public bool Viable { get; set; }
        public bool TieneComite { get; set; }
        public bool FechaComite { get; set; }
        public bool ObservacionesComite { get; set; }
        public bool InstalacionesClienteMultiple { get; set; }
        public bool PeriodoCapacitacion { get; set; }
        public bool EstadoDeFuerza { get; set; }
        public bool Signatarios { get; set; }
        public bool CuotaInstalacion { get; set; }
        public bool EstacionMonitoreoLocal { get; set; }
    }
}
