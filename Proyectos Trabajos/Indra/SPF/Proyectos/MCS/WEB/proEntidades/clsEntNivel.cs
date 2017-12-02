using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntNivel
    {
        private short _idNivel;
        private string _nivDescripcion;
        private Boolean _nivVigente;


        public short IdNivel
        {
            get { return _idNivel; }
            set { _idNivel = value; }
        }

        public string NivDescripcion
        {
            get { return _nivDescripcion; }
            set { _nivDescripcion = value; }
        }

        public Boolean NivVigente
        {
            get { return _nivVigente; }
            set { _nivVigente = value; }
        }
    }
}
