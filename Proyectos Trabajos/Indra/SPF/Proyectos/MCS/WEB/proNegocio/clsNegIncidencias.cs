using System.Collections.Generic;
using SICOGUA.Datos;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;

namespace SICOGUA.Negocio
{
    public class clsNegIncidencias
    {
        public static bool insertarRecursoHumanoIncidencias(clsEntEmpleado objEmpleado, List<clsEntIncidencia> eliminarIncidencias, clsEntSesion objSesion)
        {
            return clsDatIncidencias.insertarRecursoHumanoIncidencias(objEmpleado, eliminarIncidencias, objSesion);
        }

        public static void consultarIncidencias(ref clsEntEmpleado objEmpleado, clsEntSesion objSesion)
        {
            //dsGuardas._recursoHumano_incidenciaDataTable dsIncidencias = new dsGuardas._recursoHumano_incidenciaDataTable();
            List<clsEntIncidencia> lisIncidencias = new List<clsEntIncidencia>();

            clsDatIncidencias.consultarIncidencias(objEmpleado.IdEmpleado, ref lisIncidencias, objSesion);

            objEmpleado.Incidencias =lisIncidencias;
            
            //if (dsIncidencias.Rows.Count > 0)
            //{
            //    foreach (dsGuardas._recursoHumano_incidenciaRow obj in dsIncidencias.Rows)
            //    {
            //        clsEntIncidencia objIncidencia = new clsEntIncidencia();

            //        objIncidencia.tipoIncidencia = new clsEntTipoIncidencia();

            //        objIncidencia.IdEmpleado = obj.idEmpleado;
            //        objIncidencia.IdIncidencia = obj.idIncidencia;
            //        objIncidencia.IdEmpleadoAutoriza = obj.idEmpleadoAutoriza;
            //        objIncidencia.sEmpleadoAutoriza = obj.empleadoAutoriza;
            //        objIncidencia.IdTipoIncidencia = obj.idTipoIncidencia;
            //        objIncidencia.tipoIncidencia.TiDescripcion = obj.tipoIncidencia;
            //        objIncidencia.IncFechaInicial = obj.incFechaInicial;
            //        objIncidencia.sFechaInicial = obj.incFechaInicial.ToShortDateString() == "01/01/0001" || obj.incFechaInicial.ToShortDateString() == "01/01/1900" ? "" : obj.incFechaInicial.ToShortDateString();
            //        objIncidencia.IncFechaFinal = obj.incFechaFinal;
            //        objIncidencia.sFechaFinal = obj.incFechaFinal.ToShortDateString() == "01/01/0001" || obj.incFechaFinal.ToShortDateString() == "01/01/1900" ? "" : obj.incFechaFinal.ToShortDateString();
            //        objIncidencia.IncDescripcion = obj.incDescripcion;
            //        objIncidencia.IncNoOficio = obj.incNoOficio;

            //        objEmpleado.Incidencias.Add(objIncidencia);
            //    }
            //}
        }
    }
}
