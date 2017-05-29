using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.SolicitudBL.Model
{
    public class RegistraClienteSinTarjetaModel
    {
        public int idUser { get; set; }
        public string pNombre { get; set; }
        public string sNombre { get; set; }
        public string aPaterno { get; set; }
        public string aMaterno { get; set; }
        public string rfc { get; set; }
        public string calle { get; set; }
        public string sexo { get; set; }
        public string numeroExt { get; set; }
        public string numeroInt { get; set; }
        public string codigoPostal { get; set; }
        public string colonia { get; set; }
        public string delegacionMunicipio { get; set; }
        public string ciudad { get; set; }
        public string estado { get; set; }
        public string usuario { get; set; }
        public string celular { get; set; }
        public string telefono { get; set; }
        public string fechaNacimiento { get; set; }
        public string contrasenia { get; set; }
        public string noEmpleado { get; set; }
        public int idApp { get; set; }
        public bool enviaSeedRegistro { get; set; }
    }
}