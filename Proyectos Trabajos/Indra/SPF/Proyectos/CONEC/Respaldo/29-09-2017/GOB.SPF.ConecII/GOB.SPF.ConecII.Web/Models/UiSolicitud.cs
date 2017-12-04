using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiSolicitud
    {
        public byte[] foto { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string apPaterno { get; set; }
        public string apMaterno { get; set; }
        public string nombre { get; set; }
        public string lugarNacimiento { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string curp { get; set; }
        public string gradoEscolar { get; set; }
        public string entidadFederativa { get; set; }
        public string municipioDelegacion { get; set; }
        public string colonia { get; set; }
        public string calle { get; set; }
        public string numeroExterior { get; set; }
        public string numeroInterior { get; set; }
        public string codigoPostal { get; set; }
        public string anoServicio { get; set; }
        public string cuip { get; set; }
        public string edad { get; set; }
        public string sexo { get; set; }
        public string rfc { get; set; }
        public string grado { get; set; }
        public string cargo { get; set; }
        public string telefonoCasa { get; set; }
        public string telefonoCelular { get; set; }
        public string telefonoLaboral { get; set; }
        public string emailPersonal { get; set; }
        public string emailLaboral { get; set; }
    }
}