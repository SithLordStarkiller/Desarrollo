using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SICOGUA.Datos;
using SICOGUA.Seguridad;

namespace SICOGUA.Negocio
{
    public class clsNegCredencial
    {
        public static string[] consultarCredenciales(clsEntSesion objSesion)
        {
            return clsDatCredencial.consultarCredenciales(objSesion);
        }
    }
}
