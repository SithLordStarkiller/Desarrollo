using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntTipoAsistencia
    {
        private byte _idTipoAsistencia;
        private string _taDescripcion;
        private Boolean _taVigente;


        public byte IdTipoAsistencia
        {
            get { return _idTipoAsistencia; }
            set { _idTipoAsistencia = value; }
        }

        public string TaDescripcion
        {
            get { return _taDescripcion; }
            set { _taDescripcion = value; }
        }

        public Boolean TaVigente
        {
            get { return _taVigente; }
            set { _taVigente = value; }
        }
    }
}
