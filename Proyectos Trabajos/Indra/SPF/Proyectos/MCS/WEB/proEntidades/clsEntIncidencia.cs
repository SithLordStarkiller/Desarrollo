using System;

namespace SICOGUA.Entidades
{
    public class clsEntIncidencia
    {
        private Guid _idEmpleado;
        private int _idIncidencia;
        private Guid _idEmpleadoAutoriza;
        private string _sEmpleadoAutoriza;
        private short _idTipoIncidencia;
        private DateTime _incFechaInicial;
        private string _sFechaInicial;
        private DateTime _incFechaFinal;
        private string _sFechaFinal;
        private string _incDescripcion;
        private string _incNoOficio;
        private clsEntTipoIncidencia _tipoIncidencia;

        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public int IdIncidencia
        {
            get { return _idIncidencia; }
            set { _idIncidencia = value; }
        }

        public Guid IdEmpleadoAutoriza
        {
            get { return _idEmpleadoAutoriza; }
            set { _idEmpleadoAutoriza = value; }
        }

        public short IdTipoIncidencia
        {
            get { return _idTipoIncidencia; }
            set { _idTipoIncidencia = value; }
        }

        public DateTime IncFechaInicial
        {
            get { return _incFechaInicial; }
            set { _incFechaInicial = value; }
        }

        public DateTime IncFechaFinal
        {
            get { return _incFechaFinal; }
            set { _incFechaFinal = value; }
        }

        public string IncDescripcion
        {
            get { return _incDescripcion; }
            set { _incDescripcion = value; }
        }

        public string IncNoOficio
        {
            get { return _incNoOficio; }
            set { _incNoOficio = value; }
        }

        public clsEntTipoIncidencia tipoIncidencia
        {
            get { return _tipoIncidencia; }
            set { _tipoIncidencia = value; }
        }

        public string sFechaInicial
        {
            get { return _sFechaInicial; }
            set { _sFechaInicial = value; }
        }

        public string sFechaFinal
        {
            get { return _sFechaFinal; }
            set { _sFechaFinal = value; }
        }

        public string sEmpleadoAutoriza
        {
            get { return _sEmpleadoAutoriza; }
            set { _sEmpleadoAutoriza = value; }
        }
    }
}
