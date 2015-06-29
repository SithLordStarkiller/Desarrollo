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

        [DataMember]
        public string TipoPersona { get; set; }

        [DataMember]
        public string Nacionalidad { get; set; }

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

        //Medios electronicos

        [DataMember]
        public string CorreoUniversidad { get; set; }

        [DataMember]
        public string CorreoPersonal { get; set; }

        [DataMember]
        public string RedSocial1 { get; set; }

        [DataMember]
        public string RedSocial2 { get; set; }

        //Fotorafia

        [DataMember]
        public string NombreFoto { get; set; }

        [DataMember]
        public string ExtencionFoto { get; set; }

        [DataMember]
        public byte[] Fotografia{ get; set; }
    }
}
