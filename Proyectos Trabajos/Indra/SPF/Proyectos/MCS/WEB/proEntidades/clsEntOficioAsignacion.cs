using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntOficioAsignacion
    {
        private Guid _idEmpleado;
        private Int16 _idEmpleadoAsignacion;
        private Guid _idEmpUsuarioResponsable;
        private byte[] _oficioAsignacion;
        private DateTime _fechaCarga;


        public Guid idEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public Int16 idEmpleadoAsignacion
        {
            get { return _idEmpleadoAsignacion; }
            set { _idEmpleadoAsignacion = value; }
        }

        public Guid idEmpUsuarioResponsable
        {
            get { return _idEmpUsuarioResponsable; }
            set { _idEmpUsuarioResponsable = value; }
        }

        public byte[] oficioAsignacion
        {
            get { return _oficioAsignacion; }
            set { _oficioAsignacion = value; }
        }

        public DateTime fechaCarga
        {
            get { return _fechaCarga; }
            set { _fechaCarga = value; }
        }
    }
}





