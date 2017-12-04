using System.Collections.Generic;

namespace GOB.SPF.ConecII.Entities
{
    public class Cliente : TEntity
    {
        public Cliente()
        {
            RegimenFiscal = new RegimenFiscal();
            Sector = new Sector();
        }
        public string RazonSocial { get; set; }
        public string NombreCorto { get; set; }
        public RegimenFiscal RegimenFiscal { get; set; }
        public Sector Sector { get; set; }
        public string Rfc { get; set; }
        public bool? IsActive { get; set; }
        public DomicilioFiscal DomicilioFiscal { get; set; }

        public List<Externo> Solicitantes { get; set; }
        public List<Externo> Contactos { get; set; }
    }
}
