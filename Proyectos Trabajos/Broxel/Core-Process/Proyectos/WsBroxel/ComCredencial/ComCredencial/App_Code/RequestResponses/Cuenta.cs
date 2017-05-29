using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ComCredencial.RequestResponses
{
    public class Cuenta
    {
        [XmlElement("Numero")]
        public String NumeroCuenta { get; set; }
        public String Denominacion { get; set; }
        public Direccion Direccion { get; set; }
        public Documento Documento { get; set; }
        public String ImporteGarantia { get; set; }
        public String GrupoLiquidacion { get; set; }
        
        public Tarjetas Tarjetas { get; set; }
        public List<Telefono> Telefonos { get; set; }
        [XmlElement("emails")]
        public List<Email> Emails { get; set; }
        
        public string ToXML()
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(GetType());
            serializer.Serialize(stringwriter, this);
            return stringwriter.ToString();
        }

        public static Cuenta LoadFromXMLString(string xmlText)
        {
            var stringReader = new System.IO.StringReader(xmlText);
            var serializer = new XmlSerializer(typeof(Cuenta));
            return serializer.Deserialize(stringReader) as Cuenta;
        }

        public Cuenta()
        {
            Tarjetas= new Tarjetas {TarjetasCredencial = new List<TarjetaCredencial>()};
            Emails = new List<Email>();
            Telefonos = new List<Telefono>();
        }
    }

    public class Direccion
    {
        public String Calle { get; set; }
        public String Numero { get; set; }
        public String ReferenciaAdicional { get; set; }
        public String Localidad { get; set; }
        public String CodigoPostal { get; set; }
        public String Provincia { get; set; }
        public String Barrio { get; set; }
        public String Zona { get; set; }
        public String Pais { get; set; }
        public String Tipo { get; set; }
    }

    public class Documento
    {
        public String Tipo { get; set; }
        public String Numero { get; set; }
        public String Observaciones { get; set; }
    }

    public class Telefono
    {
        public String Tipo { get; set; }
        public String Numero { get; set; }
    }

    public class TarjetaCredencial
    {
        public String Tipo { get; set; }
        public String Numero { get; set; }
        public String Denominacion { get; set; }
        public Persona Persona { get; set; }
        public Sinonimo Sinonimo { get; set; }
    }

    public class Tarjetas
    {
        [XmlElement("Tarjeta")]
        public List<TarjetaCredencial> TarjetasCredencial { get; set; }
    }

    public class Persona
    {
        public String Nombre { get; set; }
        public Direccion Direccion { get; set; }
        public String FechaNacimiento { get; set; }
        public String Sexo { get; set; }
        public Documento Documento { get; set; }
        public String Hijos { get; set; }
        public String EstadoCivil { get; set; }
        public String Ocupacion { get; set; }
        public String Relacion { get; set; }
    }

    public class Email
    {
        [XmlElement("email")]
        public String mail { get; set; }

        public Email()
        {
            mail = "0";
        }
    }

    public class Sinonimo
    {
        public String Canal { get; set; }
        public String Identificador { get; set; }
    }

    [Serializable]
    public class OperarCuentaRequest : Request
    {
        
    }

    [Serializable]
    public class OperarCuentaResponse : Response
    {

    }

    [Serializable]
    public class OperarTarjetaRequest : Request
    {

    }

    [Serializable]
    public class OperarTarjetaResponse : Response
    {
        public string EstadoOperativo { get; set; }
        public string CodigoEstadoOperativo { get; set; }
    }

    [Serializable]
    public class NominarCuentaRequest : Request
    {
        public Cuenta Cuenta { get; set; }
    }

    [Serializable]
    public class NominarCuentaResponse : Response
    {
        public Int32 IdRenominacion { get; set; } 
        public override string ToString()
        {
            return "F :" + FechaCreacion + " C:" + CodigoRespuesta + " NA:" + NumeroAutorizacion + " S:" + Success +
                   " UR:" + UserResponse;
        }
    }
}
