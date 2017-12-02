using System;

namespace SICOGUA.Entidades
{
    public class clsEntAccion
    {
        private short _idAccion;
        private string _accDescripcion;
        private Boolean _accVigente;



        public short IdAccion
        {
            get { return _idAccion; }
            set { _idAccion = value; }
        }

        public string AccDescripcion
        {
            get { return _accDescripcion; }
            set { _accDescripcion = value; }
        }

        public Boolean AccVigente
        {
            get { return _accVigente; }
            set { _accVigente = value; }
        }

    }
}
