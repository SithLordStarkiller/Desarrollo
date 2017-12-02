using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntEmpleadoEnviaParteNovedad
    {
        private Guid _idEmpleado;
        private DateTime _eepFechaIngreso;
        private DateTime _eepFechaBaja;
        

        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public DateTime EepFechaIngreso
        {
            get { return _eepFechaIngreso; }
            set { _eepFechaIngreso = value; }
        }

        public DateTime EepFechaBaja
        {
            get { return _eepFechaBaja; }
            set { _eepFechaBaja = value; }
        }
    }
}
