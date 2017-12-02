using System;
using System.Collections.Generic;
using REA.Entidades;

namespace SICOGUA.Entidades
{
    public class clsEntIncosistencia
    {
        private string _zonDescripcion;
        private string _serDescripcion;
        private string _insNombre;
        private string _eaFechaIngreso;
        private string _eaFechaBaja;
        private Guid _idEmpleado;
        private string _nombreEmpleado;
        private string _nombreCaptura;
        private bool _permisoCambiar;
        private int _idServicio;
        private int _idInstalacion;
        private int _idHorario;
        private int _idFuncionAsignacion;
        private int _cambiar;
        public string zonDescripcion
        {
            get { return _zonDescripcion; }
            set { _zonDescripcion = value; }
        }

        public string serDescripcion
        {
            get { return _serDescripcion; }
            set { _serDescripcion = value; }
        }
        public string insNombre
        {
            get { return _insNombre; }
            set { _insNombre = value; }
        }
        public string eaFechaIngreso
        {
            get { return _eaFechaIngreso; }
            set { _eaFechaIngreso = value; }
        }
        public string eaFechaBaja
        {
            get { return _eaFechaBaja; }
            set { _eaFechaBaja = value; }
        }
        public Guid idEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }
        public string empleadoNombre
        {
            get { return _nombreEmpleado; }
            set { _nombreEmpleado = value; }
        }
        public string capturaNombre
        {
            get { return _nombreCaptura; }
            set { _nombreCaptura = value; }
        }
        public bool permisoCambiar
        {
            get { return _permisoCambiar; }
            set { _permisoCambiar = value; }
        }
        public int idServicio
        {
            get { return _idServicio ; }
            set { _idServicio  = value; }
        }
        public int idInstalacion
        {
            get { return _idInstalacion; }
            set { _idInstalacion = value; }
        }
        public int idHorario
        {
            get { return _idHorario; }
            set { _idHorario = value; }
        }
        public int idFuncionAsignacion
        {
            get { return _idFuncionAsignacion; }
            set { _idFuncionAsignacion = value; }
        }
        public int cambiar
        {
            get { return _cambiar; }
            set { _cambiar = value; }
        }
    }
}
