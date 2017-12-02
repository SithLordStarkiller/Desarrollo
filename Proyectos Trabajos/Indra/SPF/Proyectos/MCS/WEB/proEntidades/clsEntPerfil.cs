using System;

namespace SICOGUA.Seguridad
{
    public class clsEntPerfil
    {
        private Int16 _idPerfil;
        private string _perDescripcion;

        public short IdPerfil
        {
            get { return _idPerfil; }
            set { _idPerfil = value; }
        }

        public string PerDescripcion
        {
            get { return _perDescripcion; }
            set { _perDescripcion = value; }
        }
    }
}
