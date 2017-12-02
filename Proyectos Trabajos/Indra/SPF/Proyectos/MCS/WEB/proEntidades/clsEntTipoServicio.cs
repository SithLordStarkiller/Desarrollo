using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntTipoServicio
    {
        private Int32 _idTipoServicio;
        private string _tsDescripcion;
        private Boolean _tsVigente;


        public Int32 idTipoServicio
        {
            get { return _idTipoServicio; }
            set { _idTipoServicio = value; }
        }

        public string tsDescripcion
        {
            get { return _tsDescripcion; }
            set { _tsDescripcion = value; }
        }

        public Boolean tsVigente
        {
            get { return _tsVigente; }
            set { _tsVigente = value; }
        }

    }
}
