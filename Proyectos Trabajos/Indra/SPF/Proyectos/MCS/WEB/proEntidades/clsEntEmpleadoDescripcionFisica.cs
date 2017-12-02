using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntEmpleadoDescripcionFisica
    {
        private Guid _idEmpleado;
        private decimal _edEstatura;
        private decimal _edPeso;
        private string _edSenia;


        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public decimal EdEstatura
        {
            get { return _edEstatura; }
            set { _edEstatura = value; }
        }

        public decimal EdPeso
        {
            get { return _edPeso; }
            set { _edPeso = value; }
        }

        public string EdSenia
        {
            get { return _edSenia; }
            set { _edSenia = value; }
        }

    }
}
