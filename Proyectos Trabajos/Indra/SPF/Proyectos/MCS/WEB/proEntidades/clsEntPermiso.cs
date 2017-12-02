using System;

namespace SICOGUA.Seguridad
{
    public class clsEntPermiso
    {
        private Int16 _idPermiso;
        private Int16 _idPerfil;
        private Int16 _idOperacion;

        public short IdPermiso
        {
            get { return _idPermiso; }
            set { _idPermiso = value; }
        }

        public short IdPerfil
        {
            get { return _idPerfil; }
            set { _idPerfil = value; }
        }

        public short IdOperacion
        {
            get { return _idOperacion; }
            set { _idOperacion = value; }
        }
    }
}
