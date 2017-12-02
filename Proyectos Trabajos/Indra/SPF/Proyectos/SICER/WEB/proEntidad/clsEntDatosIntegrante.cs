using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proEntidad
{
    public class clsEntDatosIntegrante
    {
        public string idEmpleado { get; set; }
        //public int idRegistro { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string nombre { get; set; }
        public string numempleado { get; set; }
        public string curp { get; set; }
        public string rfc { get; set; }
        public string cuip { get; set; }
        public string loc { get; set; }
        public int idGrado { get; set; }
        public string gradoDesc { get; set; }
        public int idCargo { get; set; }
        public string cargoDesc { get; set; }
        public int edad { get; set; }
        public string escolaridad { get; set; }
        public string telcasa { get; set; }
        public string emailpersonal { get; set; }
        public string telcelular { get; set; }
        public string emaillaboral { get; set; }
        public string tellaboral { get; set; }

        public string calle { get; set; }
        public string numext { get; set; }
        public string numint { get; set; }
        public string colonia { get; set; }
        public string codpostal { get; set; }
        public int idestado { get; set; }
        public int idDelMunicipio { get; set; }

        //public DateTime fechaingreso { get; set; }
        //public int idnivelSeg { get; set; }
        //public int iddependendiaexterna { get; set; }
        //public int idinstitucionexterna { get; set; }
    }
}
