using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntAgrupamiento
    {
        private short _idZona;
        private short _idAgrupamiento;
        private string _agrDescripcion;
        private Guid _idEmpleado;
        private Boolean _agrVigente;


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

        public string AgrDescripcion
        {
            get { return _agrDescripcion; }
            set { _agrDescripcion = value; }
        }

        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public Boolean AgrVigente
        {
            get { return _agrVigente; }
            set { _agrVigente = value; }
        }

    }
}
