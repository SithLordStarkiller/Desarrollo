using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntEmpleadoRecibeParteNovedad
    {
        private Guid _idEmpleado;
        private DateTime _erpFechaIngrese;
        private DateTime _erpFechaBaja;


        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public DateTime ErpFechaIngrese
        {
            get { return _erpFechaIngrese; }
            set { _erpFechaIngrese = value; }
        }

        public DateTime ErpFechaBaja
        {
            get { return _erpFechaBaja; }
            set { _erpFechaBaja = value; }
        }
    }
}
