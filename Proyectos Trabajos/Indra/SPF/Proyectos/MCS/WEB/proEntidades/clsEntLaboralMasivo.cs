using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntLaboralMasivo
    {
        private Guid _idEmpleado;
        private short _idEmpleadoPuesto;
        private short _idPuesto;
        private short _idHorario;
        private short _idTipoHorario;
        private DateTime _fechaIngreso; 


        public Guid idEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public short idEmpleadoPuesto
        {
            get { return _idEmpleadoPuesto; }
            set { _idEmpleadoPuesto = value; }
        }

        public short idPuesto
        {
            get { return _idPuesto; }
            set { _idPuesto = value; }
        }

        public short idHorario
        {
            get { return _idHorario; }
            set { _idHorario = value; }
        }

        public short idTipoHorario
        {
            get { return _idTipoHorario; }
            set { _idTipoHorario = value; }
        }

        public DateTime fechaIngreso
        {
            get { return _fechaIngreso; }
            set { _fechaIngreso = value; }
        }

    

    }
}