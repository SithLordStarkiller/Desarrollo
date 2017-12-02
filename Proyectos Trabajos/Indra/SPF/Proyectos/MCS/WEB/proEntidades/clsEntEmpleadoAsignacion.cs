using System;
using proEntidades;
using System.Collections.Generic;

namespace SICOGUA.Entidades
{
    public class clsEntEmpleadoAsignacion
    {
        private Guid _idEmpleado;
        private short _idEmpleadoAsignacion;
        private clsEntServicio _servicio;
        private clsEntInstalacion _instalacion;
        private DateTime _eaFechaIngreso;
        private string _fechaIngreso;
        private DateTime _eaFechaBaja;
        private string _fechaBaja;
        private int _idFuncionAsignacion;
        private string _funcionAsignacion;
        private int _funcionClasificacion;
        private int _tipoOperacion;
        private string _fechaModificacion;
        private string _nombreUsuario;
        //se agregó el campo de oficio 18/12/2012
        private string _oficio;
        //

        public string fechaModificacion
        {
            get { return _fechaModificacion; }
            set { _fechaModificacion = value; }
        }

        public string nombreUsuario
        {
            get { return _nombreUsuario; }
            set { _nombreUsuario = value; }
        }

        public int tipoOperacion
        {
            get { return _tipoOperacion; }
            set { _tipoOperacion = value; }
        }

        public int IdFuncionAsignacion
        {
            get { return _idFuncionAsignacion; }
            set { _idFuncionAsignacion = value; }
        }
        public int funcionClasificacion
        {
            get { return _funcionClasificacion; }
            set { _funcionClasificacion = value; }
        }

        public string funcionAsignacion
        {
            get { return _funcionAsignacion; }
            set { _funcionAsignacion = value; }
        }




        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public short IdEmpleadoAsignacion
        {
            get { return _idEmpleadoAsignacion; }
            set { _idEmpleadoAsignacion = value; }
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

        public clsEntServicio Servicio
        {
            get { return _servicio; }
            set { _servicio = value; }
        }

        public clsEntInstalacion Instalacion
        {
            get { return _instalacion; }
            set { _instalacion = value; }
        }

        public string FechaIngreso
        {
            get { return _fechaIngreso; }
            set { _fechaIngreso = value; }
        }

        public string FechaBaja
        {
            get { return _fechaBaja; }
            set { _fechaBaja = value; }
        }
        private List<clsEntEmpleadoHorarioREA> _horarios;
        public List<clsEntEmpleadoHorarioREA> horarios
        {
            get { return _horarios; }
            set { _horarios = value; }
        }
        private List<clsEntEmpleadoHorarioREA> _horariosOriginal;
        public List<clsEntEmpleadoHorarioREA> horariosOriginal
        {
            get { return _horariosOriginal; }
            set { _horariosOriginal = value; }
        }
        //se agregó el campo de oficio 18/12/2012
        public string oficio
        {
            get { return _oficio; }
            set { _oficio = value; }
        }
        //
    }
}
