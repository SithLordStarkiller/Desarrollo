using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntZona
    {
        private short _idZona;
        private string _zonDescripcion;
        private Boolean _zonVigente;

                public short IdZona
        {
            get { return _idZona; }
            set { _idZona = value; }
        }

        public string ZonDescripcion
        {
            get { return _zonDescripcion; }
            set { _zonDescripcion = value; }
        }

        public Boolean ZonVigente
        {
            get { return _zonVigente; }
            set { _zonVigente = value; }
        }

        


    }
}
