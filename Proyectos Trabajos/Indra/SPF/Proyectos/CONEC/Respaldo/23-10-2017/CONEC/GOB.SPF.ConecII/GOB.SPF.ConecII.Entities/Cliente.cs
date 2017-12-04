using System.Collections.Generic;
using GOB.SPF.ConecII.Entities.Amatzin;
using Newtonsoft.Json;

namespace GOB.SPF.ConecII.Entities
{
    public class Cliente : TEntity
    {
        public Cliente()
        {
            RegimenFiscal = new RegimenFiscal();
            Sector = new Sector();
            Instalaciones=new List<Instalacion>();
        }
        public string RazonSocial { get; set; }
        public string NombreCorto { get; set; }
        public RegimenFiscal RegimenFiscal { get; set; }
        public Sector Sector { get; set; }
        public string Rfc { get; set; }
        public bool? IsActive { get; set; }
        public DomicilioFiscal DomicilioFiscal { get; set; }
        [JsonProperty(ItemReferenceLoopHandling = ReferenceLoopHandling.Serialize, ReferenceLoopHandling = ReferenceLoopHandling.Ignore, IsReference = true, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public List<Solicitud> Solicitud { get; set; }
        public List<Externo> Solicitantes { get; set; }
        public List<Externo> Contactos { get; set; }
        public List<Instalacion> Instalaciones { get; set; }
        [JsonProperty(ItemReferenceLoopHandling = ReferenceLoopHandling.Serialize, ReferenceLoopHandling = ReferenceLoopHandling.Ignore, IsReference = true, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public List<ClientesDocumentos> Documentos { get; set; }                                                                                                                                                                     
    }
}
