using System;

namespace SICOGUA.Seguridad
{
    public class clsEntSesion
    {
        public enum tipoEstatus
        {
            Activa = 1,
            Finalizada = 2
        }

        private clsEntUsuario _usuario;
        public clsEntUsuario usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        private string _sessionId;
        public string sessionId
        {
            get { return _sessionId; }
            set { _sessionId = value; }
        }

        private string _ip;
        public string ip
        {
            get { return _ip; }
            set { _ip = value; }
        }

        private int _intentos;
        public int intentos
        {
            get { return _intentos; }
            set { _intentos = value; }
        }

        private DateTime _inicio;
        public DateTime inicio
        {
            get { return _inicio; }
            set { _inicio = value; }
        }

        private tipoEstatus _estatus;
        public tipoEstatus estatus
        {
            get { return _estatus; }
            set { _estatus = value; }
        }
    }
}
