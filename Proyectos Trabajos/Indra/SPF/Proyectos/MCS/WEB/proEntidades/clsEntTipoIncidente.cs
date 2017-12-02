using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntTipoIncidente
    {
        private short _idTipoIncidente;
        private string _titDescripcion;
        private Boolean _titVigente;


        public short IdTipoIncidente
        {
            get { return _idTipoIncidente; }
            set { _idTipoIncidente = value; }
        }

        public string TitDescripcion
        {
            get { return _titDescripcion; }
            set { _titDescripcion = value; }
        }

        public Boolean TitVigente
        {
            get { return _titVigente; }
            set { _titVigente = value; }
        }

    }
}
