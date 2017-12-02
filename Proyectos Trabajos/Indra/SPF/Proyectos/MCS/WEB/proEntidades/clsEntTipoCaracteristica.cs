using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntTipoCaracteristica
    {
        private byte _idTipoCaracteristica;
        private string _tcDescripcion;
        private Boolean _tcVigente;


        public byte IdTipoCaracteristica
        {
            get { return _idTipoCaracteristica; }
            set { _idTipoCaracteristica = value; }
        }

        public string TcDescripcion
        {
            get { return _tcDescripcion; }
            set { _tcDescripcion = value; }
        }

        public Boolean TcVigente
        {
            get { return _tcVigente; }
            set { _tcVigente = value; }
        }

    }
}
