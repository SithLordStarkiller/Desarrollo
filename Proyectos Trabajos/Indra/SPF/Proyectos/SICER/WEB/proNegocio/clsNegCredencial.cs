using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proDatos;

namespace proNegocio
{
    public class clsNegCredencial
    {
        public static string[] consultarCredenciales()
        {
            return clsDatCredencial.consultarCredenciales();
        }
    }
}
