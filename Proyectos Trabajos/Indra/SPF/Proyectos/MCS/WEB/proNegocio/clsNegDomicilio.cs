using System;
using System.Collections.Generic;
using System.Data;
using SICOGUA.Datos;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;

namespace SICOGUA.Negocio
{
    public class clsNegDomicilio
    {
        public static void buscarCodigoPostalNeg(int idAsentamiento, ref DataSet aseCodigo, clsEntSesion objSesion)
        {
            clsDatCatalogos.consultarCodigoPostal(idAsentamiento, ref aseCodigo, objSesion);
        }
    }
}