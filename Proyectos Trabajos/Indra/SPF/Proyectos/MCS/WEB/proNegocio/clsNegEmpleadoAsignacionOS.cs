using SICOGUA.Datos;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using proDatos;

namespace SICOGUA.Negocio
{
    public class clsNegEmpleadoAsignacionOS
    {
        public static void consultarEmpleadoAsignacionOS(ref clsEntEmpleado objEmpleado, clsEntSesion objSesion)
        {
            objEmpleado.EmpleadoAsignacionOS = new clsEntEmpleadoAsignacionOS();
            objEmpleado.EmpleadoAsignacionOS.Zona = new clsEntZona();
            objEmpleado.EmpleadoAsignacionOS.Agrupamiento = new clsEntAgrupamiento();
            objEmpleado.EmpleadoAsignacionOS.Compania = new clsEntCompania();
            objEmpleado.EmpleadoAsignacionOS.Seccion = new clsEntSeccion();
            objEmpleado.EmpleadoAsignacionOS.Peloton = new clsEntPeloton();

            dsGuardas._operacionServicio_empleadoAsignacionOSDataTable dtEmpleadoAsignacionOS = new dsGuardas._operacionServicio_empleadoAsignacionOSDataTable();

            clsDatEmpleadoAsignacionOS.consultarEmpleadoAsignacionOS(objEmpleado.IdEmpleado, ref dtEmpleadoAsignacionOS, objSesion);
           
            if (dtEmpleadoAsignacionOS.Rows.Count > 0)
            {
                foreach (dsGuardas._operacionServicio_empleadoAsignacionOSRow obj in dtEmpleadoAsignacionOS.Rows)
                {
                    objEmpleado.EmpleadoAsignacionOS.IdEmpleado = obj.idEmpleado;
                    objEmpleado.EmpleadoAsignacionOS.IdEmpleadoAsignacionOS = obj.idEmpleadoAsignacionOS;
                    objEmpleado.EmpleadoAsignacionOS.Zona.IdZona = obj.idZona;
                    objEmpleado.EmpleadoAsignacionOS.Agrupamiento.IdAgrupamiento = obj.idAgrupamiento;
                    objEmpleado.EmpleadoAsignacionOS.Compania.IdCompania = obj.idCompania;
                    objEmpleado.EmpleadoAsignacionOS.Seccion.IdSeccion = obj.idSeccion;
                    objEmpleado.EmpleadoAsignacionOS.Peloton.IdPeloton = obj.idPeloton;
                    objEmpleado.EmpleadoAsignacionOS.EmoFechaIngreso = obj.emoFechaIngreso;
                    objEmpleado.EmpleadoAsignacionOS.EmoFechaBaja = obj.emoFechaBaja;
                }
            }
        }
    }
}
