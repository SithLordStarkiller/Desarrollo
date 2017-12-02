using System;
using System.Collections.Generic;
using System.Text;

namespace REA.Entidades
{
   public  class clsEntIncidenciaREA
    {
        private Guid _idEmpleado;
        private string _tiDescripcion;
        private DateTime _incFechaInicio;
        private DateTime _incFechaFin;
        private string _licenciaMedica;
        private string _riesgo;
        private string _gravidez;
        private string _vacacion;
        private string _permiso;
        private Int16 _idTipoIncidencia;
        private int _idIncidencia;
        private int _idUsuarioValida;
        private string _tipDescripcion;
        private string _incObservacion;
        private Boolean _incValida;
        private int _idUsuario;
        private Guid _idEmpleadoValida;

        public Guid idEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value;}
        }

        public string tiDescripcion
        {
            get { return _tiDescripcion; }
            set { _tiDescripcion = value; }
        }

        public DateTime incFechaInicio
        {
            get { return _incFechaInicio; }
            set { _incFechaInicio = value; }
        }

        public DateTime incFechaFin
        {
            get { return _incFechaFin; }
            set { _incFechaFin = value; }
        }

        public string licenciaMedica
        {
            get { return _licenciaMedica; }
            set { _licenciaMedica = value; }
        }

        public string riesgo
        {
            get { return _riesgo; }
            set { _riesgo = value; }
        }

        public string gravidez
        {
            get { return _gravidez; }
            set { _gravidez = value; }
        }

        public string vacacion
        {
            get { return _vacacion; }
            set { _vacacion = value; }
        }

        public string permiso
        {
            get { return _permiso; }
            set { _permiso = value; }
        }

        public Int16 idTipoIncidencia
        {
            get { return _idTipoIncidencia; }
            set {_idTipoIncidencia = value;}
        }

        public int idIncidencia
        {
            get { return _idIncidencia; }
            set { _idIncidencia = value; }
        }

        public int idUsuarioValida
        {
            get { return _idUsuarioValida; }
            set { _idUsuarioValida = value; }
        }

        public string tipDescripcion
        {
            get { return _tipDescripcion; }
            set { _tipDescripcion = value; }
        }

        public string incObservacionValida
        {
            get { return _incObservacion; }
            set { _incObservacion = value; }
        }

        public Boolean incValida
        {
            get { return _incValida; }
            set { _incValida = value; }
        }

        public int idUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }

        public Guid idEmpleadoValida
        {
            get { return _idEmpleadoValida; }
            set { _idEmpleadoValida = value; }
        }
    }
}
