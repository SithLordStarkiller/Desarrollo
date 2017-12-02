using System;

namespace SICOGUA.Entidades
{
    public class clsEntRevisionDocumentos
    {
        private Guid _idEmpleado;
        private int _idRevision;
        private DateTime _rdFechaActa;
        private string _rdNoOficio;
        private DateTime _rdFechaOficio;
        private byte[] _rdOficio;
        private int _idRevisionTipoDocumento;
        private int _idRevisionDocumento;
        private string _rdObservaciones;
        private int _idEmpleadoAsignacion;

        public int idEmpleadoAsignacion
        {
            get { return _idEmpleadoAsignacion; }
            set { _idEmpleadoAsignacion = value; }
        }

        public int idRevisionDocumento
        {
            get { return _idRevisionDocumento; }
            set { _idRevisionDocumento = value; }
        }

        public Guid idEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado= value; }
        }


        public int idRevisionTipoDocumento
        {
            get { return _idRevisionTipoDocumento; }
            set { _idRevisionTipoDocumento = value; }
        }

        public int idRevision
        {
            get { return _idRevision; }
            set { _idRevision = value; }
        }

        public DateTime rdFechaActa
        {
            get { return _rdFechaActa; }
            set { _rdFechaActa = value; }
        }

        public string rdNoOficio
        {
            get { return _rdNoOficio; }
            set { _rdNoOficio = value; }
        }

        public DateTime rdFechaOficio
        {
            get { return _rdFechaOficio; }
            set { _rdFechaOficio = value; }
        }
            
     
            
        public byte[] rdOficio
        {
            get { return _rdOficio; }
            set { _rdOficio= value; }
        }


        public string rdObservaciones
        {
            get { return _rdObservaciones; }
            set { _rdObservaciones = value; }
        }





    }
}
