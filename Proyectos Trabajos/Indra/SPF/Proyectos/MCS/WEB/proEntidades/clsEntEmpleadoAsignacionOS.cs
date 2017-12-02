using System;

namespace SICOGUA.Entidades
{
    public class clsEntEmpleadoAsignacionOS
    {
        private Guid _idEmpleado;
        private short _idEmpleadoAsignacionOS;
        private clsEntZona _zona;
        private clsEntAgrupamiento _agrupamiento;
        private clsEntCompania _compania;
        private clsEntSeccion _seccion;
        private clsEntPeloton _peloton;
        private DateTime _emoFechaIngreso;
        private DateTime _emoFechaBaja;

        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public short IdEmpleadoAsignacionOS
        {
            get { return _idEmpleadoAsignacionOS; }
            set { _idEmpleadoAsignacionOS = value; }
        }

        public DateTime EmoFechaIngreso
        {
            get { return _emoFechaIngreso; }
            set { _emoFechaIngreso = value; }
        }

        public DateTime EmoFechaBaja
        {
            get { return _emoFechaBaja; }
            set { _emoFechaBaja = value; }
        }

        public clsEntZona Zona
        {
            get { return _zona; }
            set { _zona = value; }
        }

        public clsEntAgrupamiento Agrupamiento
        {
            get { return _agrupamiento; }
            set { _agrupamiento = value; }
        }

        public clsEntCompania Compania
        {
            get { return _compania; }
            set { _compania = value; }
        }

        public clsEntSeccion Seccion
        {
            get { return _seccion; }
            set { _seccion = value; }
        }

        public clsEntPeloton Peloton
        {
            get { return _peloton; }
            set { _peloton = value; }
        }
    }
}
