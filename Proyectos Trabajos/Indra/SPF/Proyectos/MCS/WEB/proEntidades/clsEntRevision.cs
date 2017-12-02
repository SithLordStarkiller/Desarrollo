using System;

namespace SICOGUA.Entidades
{
    public class clsEntRevision
    {
 
        private Guid _idEmpleado;
        private int _idRevision;
        private int _idEmpleadoAsignacion;
        private int _idServicio;
        private int _idInstalacion;


        public Guid idEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }


        public int idRevision
        {
            get { return _idRevision; }
            set { _idRevision = value; }
        }

        public int idEmpleadoAsignacion
        {
            get { return _idEmpleadoAsignacion; }
            set { _idEmpleadoAsignacion = value; }
        }

        public int idServicio
        {
            get { return _idServicio; }
            set { _idServicio = value; }
        }

        public int idInstalacion
        {
            get { return _idInstalacion; }
            set { _idInstalacion = value; }
        }

        


    }
}
