using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntPais
    {
        private short _idPais;
        private string _paiDescripcion;
        private Boolean _paiVigente;
        

        public short IdPais
        {
            get { return _idPais; }
            set { _idPais = value; }
        }

        public string PaiDescripcion
        {
            get { return _paiDescripcion; }
            set { _paiDescripcion = value; }
        }

        public Boolean PaiVigente
        {
            get { return _paiVigente; }
            set { _paiVigente = value; }
        }

    }
}
