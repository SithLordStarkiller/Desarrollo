using System;

namespace SICOGUA.Entidades
{
    public class clsEntPaseLista
    {
        private Guid _idEmpleado;
        private short _idEmpleadoHorario;
        private int _idPaseLista;
        private byte _idTipoAsistencia;
        private int _idIncidencia;
        private DateTime _plFecha;
        private string _strTipoAsistencia;
        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public short IdEmpleadoHorario
        {
            get { return _idEmpleadoHorario; }
            set { _idEmpleadoHorario = value; }
        }

        public int IdPaseLista
        {
            get { return _idPaseLista; }
            set { _idPaseLista = value; }
        }

        public byte IdTipoAsistencia
        {
            get { return _idTipoAsistencia; }
            set { _idTipoAsistencia = value; }
        }

        public DateTime PlFecha
        {
            get { return _plFecha; }
            set { _plFecha = value; }
        }

        public int IdIncidencia
        {
            get { return _idIncidencia; }
            set { _idIncidencia = value; }
        }
        public string strTipoAsistencia
        {
            get { return _strTipoAsistencia; }
            set { _strTipoAsistencia = value; }
        }
    }
}
