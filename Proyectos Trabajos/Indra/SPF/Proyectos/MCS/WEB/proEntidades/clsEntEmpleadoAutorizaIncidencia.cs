using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntEmpleadoAutorizaIncidencia
    {
        private Guid _idEmpleado;
        private DateTime _eaFechaIngreso;
        private DateTime _eaFechaBaja;


        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public DateTime EaFechaIngreso
        {
            get { return _eaFechaIngreso; }
            set { _eaFechaIngreso = value; }
        }

        public DateTime EaFechaBaja
        {
            get { return _eaFechaBaja; }
            set { _eaFechaBaja = value; }
        }
    }
}
