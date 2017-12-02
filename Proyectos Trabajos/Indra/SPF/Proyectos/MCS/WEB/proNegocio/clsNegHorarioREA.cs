using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using SICOGUA.Datos;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using REA.Entidades;
using REA.Datos;

using System.Text;

namespace REA.Negocio
{
   public class clsNegHorarioREA
    {
       

       public static  REA.Entidades.clsEntHorario consultarServicioInstalacionHor(int intServicio, int intInstalacion, clsEntSesion objSesion)
       {
           //clsDatHorarioREA objDatREA = new clsDatHorarioREA();

           REA.Entidades.clsEntHorario objHorarioRea = clsDatHorarioREA.consultarServicioInstalacion(intServicio, intInstalacion, objSesion);
            return objHorarioRea;
          

           
       }

       public static int insertarHorario(REA.Entidades.clsEntHorario objHorario, clsEntHorarioComidaREA objHorarioComida, clsEntSesion objSesion, string strMensaje)
       {
           
           
           int idHorario = clsDatHorarioREA.insertarHorario(objHorario, objHorarioComida,objSesion);
           switch (idHorario)
           {
               case -1:
                   strMensaje = "No se pudo concluir el registro.";
                   return 0;

               default:
                   strMensaje = "Registro finalizado correctamente.";
                   return idHorario;
           }
       }

       public static List<REA.Entidades.clsEntHorario> obtenerHorarioServicioInstalacion(int intIdServicio, int intIdInstalacion, clsEntSesion objSesion)
       {
           List<REA.Entidades.clsEntHorario> lsHorarios = new List<Entidades.clsEntHorario>();
           lsHorarios = clsDatHorarioREA.obtenerHorarioServicioInstalacion(intIdServicio, intIdInstalacion, objSesion);
           return lsHorarios;
        }

       public static REA.Entidades.clsEntHorario obtenerHorarioPorId(int intidHorario, clsEntSesion objSesion)
       {
           REA.Entidades.clsEntHorario objHorario = new REA.Entidades.clsEntHorario();
           objHorario = clsDatHorarioREA.obtenerDatosHorario(intidHorario, objSesion);
           return objHorario;
       }

       public static List<clsEntInstalacion> consultarInstalacion(int idServicio, int intInstalacion, char chVigente, clsEntSesion objSesion)
       {
           List<clsEntInstalacion> lsInstalacion = new List<clsEntInstalacion>();
           lsInstalacion = clsDatInstalacion.consultarInstalacionREA(idServicio, intInstalacion, chVigente, objSesion);
               return lsInstalacion;
       }
    }
}
