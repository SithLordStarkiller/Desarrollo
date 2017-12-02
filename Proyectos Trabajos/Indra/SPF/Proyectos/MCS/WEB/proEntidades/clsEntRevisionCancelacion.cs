using System;

namespace SICOGUA.Entidades
{
    public class clsEntRevisionCancelacion
    {
        private int _idRevisionDocumento;
        private string _rcObservaciones;
        private DateTime _rcFechaCancelacion;
        private Guid _idEmpleado;
        private int _idRevision;
        private int _idEmpleadoAsignacion;

        public int idEmpleadoAsignacion
        {
            get { return _idEmpleadoAsignacion; }
            set { _idEmpleadoAsignacion = value; }
        }

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

        public int idRevisionDocumento
        {
            get { return _idRevisionDocumento; }
            set { _idRevisionDocumento = value; }
        }

        public string rcObservaciones
        {
            get { return _rcObservaciones; }
            set { _rcObservaciones = value; }
        }

        public DateTime rcFechaCancelacion
        {
            get { return _rcFechaCancelacion; }
            set { _rcFechaCancelacion = value; }
        }

        

    }
}
