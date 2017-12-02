using System;

namespace SICOGUA.Entidades
{
    public class clsEntEmpleadoPuesto
    {
        private Guid _idEmpleado;
        private short _idEmpleadoPuesto;
        private clsEntPuesto _puesto;
        private clsEntHorario _horario;
        private byte _idDireccionGeneral;
        private DateTime _epFechaIngreso;
        private DateTime _epFechaBaja;

        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public short IdEmpleadoPuesto
        {
            get { return _idEmpleadoPuesto; }
            set { _idEmpleadoPuesto = value; }
        }

        public byte IdDireccionGeneral
        {
            get { return _idDireccionGeneral; }
            set { _idDireccionGeneral = value; }
        }

        public DateTime EpFechaIngreso
        {
            get { return _epFechaIngreso; }
            set { _epFechaIngreso = value; }
        }

        public DateTime EpFechaBaja
        {
            get { return _epFechaBaja; }
            set { _epFechaBaja = value; }
        }

        public clsEntPuesto Puesto
        {
            get { return _puesto; }
            set { _puesto = value; }
        }

        public clsEntHorario Horario
        {
            get { return _horario; }
            set { _horario = value; }
        }
    }
}
