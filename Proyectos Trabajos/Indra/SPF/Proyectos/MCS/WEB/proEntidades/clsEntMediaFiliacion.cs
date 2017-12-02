using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntMediaFiliacion
    {
        private byte _idTipoCaracteristica;
        private byte _idMediaFiliacion;
        private string _mfDescripcion;
        private Boolean _mfvigente;

        public byte IdTipoCaracteristica
        {
            get { return _idTipoCaracteristica; }
            set { _idTipoCaracteristica = value; }
        }

        public byte IdMediaFiliacion
        {
            get { return _idMediaFiliacion; }
            set { _idMediaFiliacion = value; }
        }

        public string MfDescripcion
        {
            get { return _mfDescripcion; }
            set { _mfDescripcion = value; }
        }

        public Boolean Mfvigente
        {
            get { return _mfvigente; }
            set { _mfvigente = value; }
        }


    }
}
