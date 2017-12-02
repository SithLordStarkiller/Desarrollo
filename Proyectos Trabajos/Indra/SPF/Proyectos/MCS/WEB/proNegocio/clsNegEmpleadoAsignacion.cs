using System.Collections.Generic;
using SICOGUA.Datos;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using System.Data;
using System.Data.Common;

namespace SICOGUA.Negocio
{
    class clsNegEmpleadoAsignacion
    {
        public static void consultarEmpleadoAsignacion(ref clsEntEmpleado objEmpleado, clsEntSesion objSesion)
        {
            objEmpleado.EmpleadoAsignacion = new List<clsEntEmpleadoAsignacion>();
            clsEntEmpleadoAsignacion objAsignacion;

        
          dsGuardas._empleado_empleadoAsignacionDataTable dtEmpleadoAsignacion = new dsGuardas._empleado_empleadoAsignacionDataTable();
           
            clsDatEmpleadoAsignacion.consultarEmpleadoAsignacion(objEmpleado.IdEmpleado, ref dtEmpleadoAsignacion, objSesion);

            if (dtEmpleadoAsignacion.Rows.Count > 0)
            {
                foreach (dsGuardas._empleado_empleadoAsignacionRow obj in dtEmpleadoAsignacion.Rows)
                {
                    objAsignacion = new clsEntEmpleadoAsignacion();
                    objAsignacion.Servicio = new clsEntServicio();
                    objAsignacion.Instalacion = new clsEntInstalacion();

                    objAsignacion.IdEmpleado = obj.idEmpleado;
                    objAsignacion.IdEmpleadoAsignacion = obj.idEmpleadoAsignacion;
                    objAsignacion.Servicio.idServicio = obj.idServicio;
                    objAsignacion.Servicio.serDescripcion = obj.serDescripcion;
                    objAsignacion.Instalacion.IdInstalacion = obj.idInstalacion;
                    objAsignacion.Instalacion.InsNombre = obj.insNombre;
                    objAsignacion.funcionAsignacion = obj.funcionAsignacion;
                    objAsignacion.EaFechaIngreso = obj.eaFechaIngreso;
                    objAsignacion.EaFechaBaja = obj.eaFechaBaja;
                    objAsignacion.IdFuncionAsignacion = obj.idFuncionAsignacion;
                    objAsignacion.FechaIngreso = obj.eaFechaIngreso.ToShortDateString();
                    objAsignacion.FechaBaja = obj.eaFechaBaja.ToShortDateString() == "01/01/1900" ? "" : obj.eaFechaBaja.ToShortDateString();
                    objAsignacion.fechaModificacion = obj.fechaModificacion.ToShortDateString() == "01/01/1900" ? "" : obj.fechaModificacion.ToString();
                    objAsignacion.nombreUsuario = obj.nombreUsuario;
                    List<clsEntEmpleadoHorarioREA> lisHorarios = new List<clsEntEmpleadoHorarioREA>();
                    lisHorarios = clsDatEmpleadoHorarioREA.consultaHorarios(objSesion, obj.idEmpleado, objAsignacion.EaFechaIngreso, objAsignacion.EaFechaBaja, obj.idServicio,obj.idInstalacion);
                    objAsignacion.horarios = lisHorarios;
                    objAsignacion.horariosOriginal = lisHorarios;
                    objAsignacion.oficio = clsDatEmpleadoAsignacion.consultarOficioAsignacion(objAsignacion.IdEmpleado, objAsignacion.IdEmpleadoAsignacion, objSesion);
                    objEmpleado.EmpleadoAsignacion.Add(objAsignacion);

                 

                }
            }
        }
    }
}
