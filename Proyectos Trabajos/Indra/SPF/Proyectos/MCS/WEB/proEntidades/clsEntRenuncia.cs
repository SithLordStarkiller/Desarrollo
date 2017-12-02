using System;

namespace SICOGUA.Entidades
{
    public class clsEntRenuncia
    {
        private  DateTime _fechaRenuncia;
        private string _noOficio;
        private DateTime _fechaOficio;
        private string _observaciones;
        private byte[] _oficioAdjunto;
        private Guid _idEmpleado;
        private int _idRenuncia;
        private int _idServicio;
        private int _idInstalacion;
        private int _idEmpleadoAsignacion;


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

        public int idRenuncia
        {
            get { return _idRenuncia; }
            set { _idRenuncia = value; }
        }

        public Guid idEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public DateTime fechaRenuncia
        {
            get { return _fechaRenuncia; }
            set { _fechaRenuncia = value; }
        }

        public string noOficio
        {
            get { return _noOficio; }
            set { _noOficio = value; }
        }

        public DateTime fechaOficio
        {
            get { return _fechaOficio; }
            set { _fechaOficio = value; }
        }

        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        public byte[] oficioAdjunto
        {
            get { return _oficioAdjunto; }
            set { _oficioAdjunto = value; }
        }

        
    }
}
