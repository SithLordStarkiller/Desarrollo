using System.Runtime.Serialization;

namespace ExamenEdenred.Entities.Entities
{
    [DataContract]
    public class PerPersonas
    {
        [DataMember]
        public int IdPersona { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Sexo { get; set; }

        [DataMember]
        public int? Edad { get; set; }
    }
}
