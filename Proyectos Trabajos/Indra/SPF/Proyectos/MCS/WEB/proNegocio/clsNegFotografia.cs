using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using SICOGUA.Datos;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;


namespace SICOGUA.Negocio
{
    public class clsNegFotografia
    {
        public static void consultarPersonaFoto(Guid idEmpleado, ref clsEntEmpleado objEmpleado, clsEntSesion objSesion)
        {
            clsDatFotografia.consultarPersonaFoto(idEmpleado, ref objEmpleado, objSesion);
        }
    }
}
