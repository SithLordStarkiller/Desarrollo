using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SICOGUA.Datos;
using System.Data;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;

namespace SICOGUA.proNegocio
{
    public class clsNegAsistencia
    {
        public static void insertarAsistenciaSalidaCompleto(DataTable dtPaseLista, DateTime fecha, clsEntSesion objSesion)
        {
            clsDatPaseLista.insertarAistenciaSalidaCompleto(dtPaseLista, fecha, objSesion);
        }
        public static void insertarPaseListaCompleto(DataTable dtPaseLista, DateTime fecha, clsEntSesion objSesion)
        {
            clsDatPaseLista.insertarPaseListaCompleto(dtPaseLista, fecha, objSesion);
        }
        public static bool asistenciaBiometriocMCS(int idServicio, int idInstalacion,int idHorario, clsEntSesion objSesion)
        {
            bool MCS = clsDatPaseLista.asistenciaBiometriocMCS(idServicio, idInstalacion,idHorario, objSesion);
            return MCS;
        }
        public static bool horarioAbiertoMCS(int idHorario, clsEntSesion objSesion)
        {
            bool MCS = clsDatPaseLista.horarioAbiertoMCS(idHorario, objSesion);
            return MCS;
        }
        public static bool desabilitarAsistenciaTiempo(DateTime fecha, int idHorario, clsEntSesion objSesion)
        {
            bool MCS = clsDatPaseLista.desabilitarAsistenciaTiempo(fecha, idHorario, objSesion);
            return MCS;
        }
        public static void validarSancion(Guid idEmpleado, ref bool booSuspension, ref bool booInhabilitacion, clsEntSesion objSesion)
        {
            clsDatAsistencia.validarSancion(idEmpleado, ref booSuspension, ref booInhabilitacion, objSesion);
        }

        //public static string insertarListaAsistencia(List<clsEntEmpleadosListaGenerica> lstHorario, clsEntSesion objSesion, ref int registro, ref int error)
        //{
        //    return clsDatAsistencia.insertarListaAsistencia(lstHorario, objSesion,ref registro,ref error);
        //}


        /*Cambio integración CONEC mayo 2016*/
        public static bool validaInstalacionCONEC(int idServicio, int idInstalacion, clsEntSesion objSesion)
        {
            bool respuesta = clsDatAsistencia.validaInstalacionCONEC(idServicio, idInstalacion, objSesion);
            return respuesta;
        }

        /*ACTUALIZACIÓN Febrero 2017*/
        public static bool paseListaHibrido(int idServicio, int idZona, int idInstalacion, clsEntSesion objSesion)
        {
            bool hibrido = clsDatPaseLista.paseListaHibrido(idServicio, idZona, idInstalacion, objSesion);
            return hibrido;
        }

        /* ACTUALIZACIÓN MARZO 2017 PARA QUITAR INCONSISTENCIAS */
        public static string insertarListaAsistencia(List<clsEntEmpleadosListaGenerica> lstHorario, clsEntSesion objSesion, ref int registro, ref int error, ref int inconsistencia, ref int cambioInconsistencia)
        {
            return clsDatAsistencia.insertarListaAsistencia(lstHorario, objSesion, ref registro, ref error, ref inconsistencia, ref cambioInconsistencia);
        }
    }
}
