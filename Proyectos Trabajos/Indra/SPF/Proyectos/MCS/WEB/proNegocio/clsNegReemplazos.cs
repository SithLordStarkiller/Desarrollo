using System;
using System.Collections.Generic;
using System.Data;
using SICOGUA.Datos;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;

namespace SICOGUA.Negocio
{
    public class clsNegReemplazos
    {
        public static List<clsEntReemplazo> listaReemplazo(clsEntSesion objSesion)
        {
            return clsDatReemplazo.listaReemplazos(objSesion);
        }
        public static List<clsEntReemplazoTabla> lisReemplazoTabla(List<clsEntReemplazo> lisReemplazos)
        {
            List<clsEntReemplazoTabla> lisTabla = new List<clsEntReemplazoTabla>();
            foreach (clsEntReemplazo objReemplazo in lisReemplazos)
            {
                clsEntReemplazoTabla objReemplazoTabla = new clsEntReemplazoTabla
                {
                    empMaterno = objReemplazo.integranteReemplazar.EmpMaterno,
                    empNombre = objReemplazo.integranteReemplazar.EmpNombre,
                    empPaterno = objReemplazo.integranteReemplazar.EmpPaterno,
                    idEmpleado = objReemplazo.integranteReemplazar.IdEmpleado,
                    idInstalacion = objReemplazo.integranteReemplazar.EmpleadoAsignacion[0].Instalacion.IdInstalacion,
                    idServicio = objReemplazo.integranteReemplazar.EmpleadoAsignacion[0].Servicio.idServicio,
                    insNombre = objReemplazo.integranteReemplazar.EmpleadoAsignacion[0].Instalacion.InsNombre,
                    serDescripcion = objReemplazo.integranteReemplazar.EmpleadoAsignacion[0].Servicio.serDescripcion,
                    intContador = lisTabla.Count + 1,
                    idEmpleadoReemplazo = Guid.Empty,
                    empMaternoReemplazo = "",
                    empNombreReemplazo = "",
                    empPaternoReemplazo = ""
                };
                lisTabla.Add(objReemplazoTabla);
            }
            return lisTabla;
        }
        public static string insertaReemplazo(List<clsEntReemplazo> lisReemplazos, clsEntSesion objSesion)
        {
            string strRegresa = "";
            foreach (clsEntReemplazo objReemplazo in lisReemplazos)
            {
                if (objReemplazo.integranteReemplazo != null && objReemplazo.integranteReemplazo.IdEmpleado.ToString().Length > 0)
                {
                    if (clsDatReemplazo.insertaReemplazo(objReemplazo, objSesion) == false)
                    {
                        strRegresa = strRegresa + "      " + "Error en el registro " + objReemplazo.integranteReemplazar.EmpPaterno + " " + objReemplazo.integranteReemplazar.EmpMaterno + " " + objReemplazo.integranteReemplazar.EmpNombre;

                    }
                }
            }

            return strRegresa;
        }
    }
}
