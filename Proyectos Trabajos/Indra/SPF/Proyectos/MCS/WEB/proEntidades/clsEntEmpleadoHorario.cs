using System;

namespace SICOGUA.Entidades
{
    public class clsEntEmpleadoHorario
    {
        private Guid _idEmpleado;
        private short _idEmpleadoHorario;
        private short _idHorario;
        private DateTime _ehFechaingreso;
        private DateTime _ehFechaBaja;

        private clsEntHorario _horario;

        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public short IdEmpleadoHorario
        {
            get { return _idEmpleadoHorario; }
            set { _idEmpleadoHorario = value; }
        }

        public short IdHorario
        {
            get { return _idHorario; }
            set { _idHorario = value; }
        }

        public DateTime EhFechaingreso
        {
            get { return _ehFechaingreso; }
            set { _ehFechaingreso = value; }
        }

        public DateTime EhFechaBaja
        {
            get { return _ehFechaBaja; }
            set { _ehFechaBaja = value; }
        }

        public clsEntHorario horario
        {
            get { return _horario; }
            set { _horario = value; }
        }
    }
}
