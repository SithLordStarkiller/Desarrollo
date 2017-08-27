namespace Certificacion.AccesoDatos
{
    using System.Collections.Generic;
    using System.Linq;
    using Modelos;

    public class EmpleadosDa
    {
        public List<Empleados> ObtenesListaEmpleados()
        {
            using (var contexto = new ModelDbEntities())
            {
                return contexto.Empleados.ToList();
            }
        }

        public Empleados InsertaEmpleado(Empleados empleado)
        {
            using (var contexto = new ModelDbEntities())
            {
                var entidad = contexto.Empleados.Add(empleado);
                contexto.SaveChanges();
                return entidad;
            }
        }
    }
}
