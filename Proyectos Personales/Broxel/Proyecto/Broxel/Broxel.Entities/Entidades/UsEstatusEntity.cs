namespace Broxel.Entities.Entidades
{
    using System.Runtime.Serialization;

    [DataContract]
    public class UsEstatusEntity
    {
        [DataMember]
        public int IdEstatus { get; set; }

        [DataMember]
        public string Estatus { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        public UsEstatusEntity()
        {
        }

        public UsEstatusEntity(int idEstatus, string estatus, string descripcion)
        {
            IdEstatus = idEstatus;
            Estatus = estatus;
            Descripcion = descripcion;
        }
    }
}
