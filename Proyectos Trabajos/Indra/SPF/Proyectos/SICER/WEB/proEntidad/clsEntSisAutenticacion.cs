using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proSeguridad;

namespace proEntidad
{
    public class clsEntSisAutenticacion
    {
        public bool response { get; set; }
        public string message { get; set; }
        public clsEntSesion objSesion { get; set; }
       // public spuConsultarUsuarioRfc_Result obj { get; set; }
    }
}
