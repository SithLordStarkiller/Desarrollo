using System;
using System.Runtime.Serialization;

namespace Universidad.Entidades.Personas
{
    [DataContract]
    public class DatosCompletosPersona
    {
        //Datos

        [DataMember]
        public int IdPersona { get; set; }

        [DataMember]
        public string IdLinkPersona { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string ApellidoP { get; set; }

        [DataMember]
        public string ApellidoM { get; set; }

        [DataMember]
        public string NombreCompleto { get; set; }

        [DataMember]
        public DateTime FechaNacimiento { get; set; }

        [DataMember]
        public DateTime FechaIngreso { get; set; }

        [DataMember]
        public string Sexo { get; set; }

        [DataMember]
        public string Curp { get; set; }

        [DataMember]
        public string Rfc { get; set; }

        [DataMember]
        public string Nss { get; set; }

        //Direccion
        [DataMember]
        public string Estado { get; set; }
    }
}
