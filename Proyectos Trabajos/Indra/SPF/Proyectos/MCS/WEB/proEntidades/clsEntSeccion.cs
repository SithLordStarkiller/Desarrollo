using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntSeccion
    {
        private short _idZona;
        private short _idAgrupamiento;
        private short _idCompania;
        private short _idSeccion;
        private string _secDescripcion;
        private Guid _idEmpleado;
        private Boolean _secVigente;


        public short IdZona
        {
            get { return _idZona; }
            set { _idZona = value; }
        }

        public short IdAgrupamiento
        {
            get { return _idAgrupamiento; }
            set { _idAgrupamiento = value; }
        }

        public short IdCompania
        {
            get { return _idCompania; }
            set { _idCompania = value; }
        }

        public short IdSeccion
        {
            get { return _idSeccion; }
            set { _idSeccion = value; }
        }

        public string SecDescripcion
        {
            get { return _secDescripcion; }
            set { _secDescripcion = value; }
        }

        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public Boolean SecVigente
        {
            get { return _secVigente; }
            set { _secVigente = value; }
        }

    }
}
