using System.Collections.Generic;
using SICOGUA.Datos;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;


namespace SICOGUA.Negocio
{
    public class clsNegEmpleadoPuesto
    {
        public static void consultarEmpleadoPuesto(ref clsEntEmpleado objEmpleado, clsEntSesion objSesion)
        {
            objEmpleado.EmpleadoPuesto = new clsEntEmpleadoPuesto();
            objEmpleado.EmpleadoPuesto.Puesto = new clsEntPuesto();
            objEmpleado.EmpleadoPuesto.Puesto.Jerarquia = new clsEntJerarquia();
            objEmpleado.EmpleadoPuesto.Puesto.Nivel = new clsEntNivel();
            objEmpleado.EmpleadoHorario = new clsEntEmpleadoHorario();
            objEmpleado.EmpleadoHorario.horario = new clsEntHorario();
            objEmpleado.EmpleadoHorario.horario.tipoHorario = new clsEntTipoHorario();
            

            dsGuardas._empleado_empleadoPuestoDataTable dtEmpleadoPuesto = new dsGuardas._empleado_empleadoPuestoDataTable();

            clsDatEmpleadoPuesto.consultarEmpleadoPuesto(objEmpleado.IdEmpleado, ref dtEmpleadoPuesto, objSesion);

            if (dtEmpleadoPuesto.Rows.Count > 0)
            {
                foreach (dsGuardas._empleado_empleadoPuestoRow obj in dtEmpleadoPuesto.Rows)
                {
                    objEmpleado.EmpleadoPuesto.IdEmpleado = obj.idEmpleado;
                    objEmpleado.EmpleadoPuesto.IdEmpleadoPuesto = obj.idEmpleadoPuesto;
                    objEmpleado.EmpleadoPuesto.Puesto.IdPuesto = obj.idPuesto;
                    objEmpleado.EmpleadoPuesto.Puesto.Jerarquia.IdJerarquia = obj.idJerarquia;
                    objEmpleado.EmpleadoPuesto.Puesto.Jerarquia.JerDescripcion = obj.jerDescripcion;
                    objEmpleado.EmpleadoPuesto.Puesto.Nivel.IdNivel = obj.idNivel;
                    //objEmpleado.EmpleadoHorario.horario.IdHorario = obj.idHorario;
                    //objEmpleado.EmpleadoHorario.horario.tipoHorario.IdTipoHorario = obj.idTipoHorario;
                    objEmpleado.EmpleadoPuesto.EpFechaIngreso = obj.epFechaIngreso;
                    objEmpleado.EmpleadoPuesto.EpFechaBaja = obj.epFechaBaja;
                    objEmpleado.EmpFechaBaja =  obj.empFechaBaja;
                    objEmpleado.EmpleadoPuesto.Puesto.PueDescripcion = obj.pueDescripcion;
                    //objEmpleado.EmpleadoHorario.EhFechaingreso = obj.ehFechaingreso;
                    //objEmpleado.EmpleadoHorario.EhFechaingreso = obj.ehFechaingreso;

                }
            }
        }
    }
}
