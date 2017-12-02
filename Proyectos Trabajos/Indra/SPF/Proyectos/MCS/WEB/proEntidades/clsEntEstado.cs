using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntEstado
    {
        private short _idPais;
        private short _idEstado;
        private string _estDescripcion;
        private Boolean _estVigente;


        public short IdPais
        {
            get { return _idPais; }
            set { _idPais = value; }
        }

        public short IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }

        public string EstDescripcion
        {
            get { return _estDescripcion; }
            set { _estDescripcion = value; }
        }

        public Boolean EstVigente
        {
            get { return _estVigente; }
            set { _estVigente = value; }
        }
              
    }
}
