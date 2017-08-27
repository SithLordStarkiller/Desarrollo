namespace Certificacion.LogicaNegocios
{
    using System.Collections.Generic;
    using AccesoDatos;
    using Modelos;

    public class EmpleadosLn
    {
        public List<Empleados> ObtenerEmpleados()
        {
            return new EmpleadosDa().ObtenesListaEmpleados();
        }

        public Empleados InsertaEmpleado(Empleados empleado)
        {
            return new EmpleadosDa().InsertaEmpleado(empleado);
        }
    }
}
