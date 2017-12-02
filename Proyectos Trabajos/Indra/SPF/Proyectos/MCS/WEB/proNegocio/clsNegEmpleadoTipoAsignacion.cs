using System;
using System.Collections.Generic;
using System.Data;
using SICOGUA.Datos;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;

namespace SICOGUA.Negocio
{
    public class clsNegEmpleadoTipoAsignacion
    {
        public static List<clsEntEmpleadoTipoAsignacion> lisEmpleadoTipoAsignacion(clsEntSesion objSesion, int idTipoAsignacion, int idServicio, int idInstalacion, int empNumero, string empPaterno, string empMaterno, string empNombre, string empRFC, int intFiltroAsignacion)
        {
            return clsDatEmpleadoTipoAsignacion.listaEmpleadoTipoAsignacion(objSesion, idTipoAsignacion, idServicio, idInstalacion, empNumero, empPaterno, empMaterno, empNombre, empRFC, intFiltroAsignacion);
        }

        public static string insertaEmpleadoTipoAsignacion(List<clsEntEmpleadoTipoAsignacion> lisEmpleadoTipoAsignacion, clsEntSesion objSesion)
        {
            string strRegresa = "";
            foreach (clsEntEmpleadoTipoAsignacion objEmpleadoTipoAsignacion in lisEmpleadoTipoAsignacion)
            {
                if (clsDatEmpleadoTipoAsignacion.insertaempleadoTipoAsignacion(objEmpleadoTipoAsignacion, objSesion) == false)
                {
                    strRegresa = strRegresa + "      " + "Error en el registro " + objEmpleadoTipoAsignacion.empPaterno;

                }
            }


            return strRegresa;
        }
    }
}

