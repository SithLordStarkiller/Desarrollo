using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntTipoIncidencia
    {
        private short _idTipoIncidencia;
        private string _tiDescripcion;
        private Boolean _tiVigente;


        public short IdTipoIncidencia
        {
            get { return _idTipoIncidencia; }
            set { _idTipoIncidencia = value; }
        }

        public string TiDescripcion
        {
            get { return _tiDescripcion; }
            set { _tiDescripcion = value; }
        }

        public Boolean TiVigente
        {
            get { return _tiVigente; }
            set { _tiVigente = value; }
        }
    }
}
