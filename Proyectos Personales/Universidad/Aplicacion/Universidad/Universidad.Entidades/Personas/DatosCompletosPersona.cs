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
        public string FechaNacimiento { get; set; }

        [DataMember]
        public string FechaIngreso { get; set; }

        [DataMember]
        public string Sexo { get; set; }

        [DataMember]
        public string Curp { get; set; }

        [DataMember]
        public string Rfc { get; set; }

        [DataMember]
        public string Nss { get; set; }

        [DataMember]
        public string TipoPersona { get; set; }

        //Direccion
        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public string Municipio { get; set; }

        [DataMember]
        public string Colonia { get; set; }

        [DataMember]
        public string CodigoPostal { get; set; }

        [DataMember]
        public string Calle { get; set; }

        [DataMember]
        public string NoInt { get; set; }

        [DataMember]
        public string NoExt { get; set; }

        [DataMember]
        public string Referencias { get; set; }

        //Telefonos

        [DataMember]
        public string TelefonoFijoDomicilio { get; set; }

        [DataMember]
        public string TelefonoFijoTrabajo { get; set; }

        [DataMember]
        public string TelefonoMovilPersonal { get; set; }

        [DataMember]
        public string TelefonoMovilTrabajo { get; set; }

        [DataMember]
        public string Fax { get; set; }
    }
}
