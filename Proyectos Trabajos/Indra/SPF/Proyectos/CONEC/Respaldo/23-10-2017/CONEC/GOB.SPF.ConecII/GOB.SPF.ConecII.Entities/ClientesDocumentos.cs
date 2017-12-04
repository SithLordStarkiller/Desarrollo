using GOB.SPF.ConecII.Entities.Amatzin;
using Newtonsoft.Json;

namespace GOB.SPF.ConecII.Entities
{
    public class ClientesDocumentos : Documento
    {
        public ClientesDocumentos()
        {
            Cliente = new Cliente();
            TipoDocumento = new TipoDocumento();
        }
        public int Identificador { get; set; }
        [JsonProperty(ItemReferenceLoopHandling = ReferenceLoopHandling.Serialize, ReferenceLoopHandling = ReferenceLoopHandling.Ignore, IsReference = true, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public Cliente Cliente { get; set; }
        public bool Activo { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
    }
}
