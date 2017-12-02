using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntMunicipio
    {
        private short _idPais;
        private short _idEstado;
        private short _idMunicipio;
        private string _munDescripcion;
        private Boolean _munVigente;
        

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

        public short IdMunicipio
        {
            get { return _idMunicipio; }
            set { _idMunicipio = value; }
        }

        public string MunDescripcion
        {
            get { return _munDescripcion; }
            set { _munDescripcion = value; }
        }

        public Boolean MunVigente
        {
            get { return _munVigente; }
            set { _munVigente = value; }
        }

    }
}
