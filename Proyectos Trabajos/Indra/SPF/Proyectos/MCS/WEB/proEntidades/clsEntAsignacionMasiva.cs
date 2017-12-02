using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntAsignacionMasiva
    {
        private Guid _idEmpleado;
        private short _idEmpleadoAsignacion;
        private int _idServicio;
        private int _idInstalacion;
        private DateTime _fechaIngreso;
        private DateTime _fechaBaja;
        private int _idHorario;
        private Guid _idUsuario;
        private int _idFuncionAsignacion;
        private int _operacion;
        private DateTime _fechaModificacionMasiva;
        private DateTime _fechaModificacion;
        private DateTime _fechaCierreAsignacion;
        public string eaOficio { get; set; }

        public DateTime fechaCierreAsignacion
        {
            get { return _fechaCierreAsignacion; }
            set { _fechaCierreAsignacion = value; }
        }

        public DateTime fechaModificacion
        {
            get { return _fechaModificacion; }
            set { _fechaModificacion = value; }
        }

        public DateTime fechaModificacionMasiva
        {
            get { return _fechaModificacionMasiva; }
            set { _fechaModificacionMasiva = value; }
        }

        public Guid idEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public short idEmpleadoAsignacion
        {
            get { return _idEmpleadoAsignacion; }
            set { _idEmpleadoAsignacion = value; }
        }

        public int idServicio
        {
            get { return _idServicio; }
            set { _idServicio = value; }
        }

        public int idInstalacion
        {
            get { return _idInstalacion; }
            set { _idInstalacion = value; }
        }

        public DateTime fechaIngreso
        {
            get { return _fechaIngreso; }
            set { _fechaIngreso = value; }
        }

        public DateTime fechaBaja
        {
            get { return _fechaBaja; }
            set { _fechaBaja = value; }
        }

        public int idHorario
        {
            get { return _idHorario; }
            set { _idHorario = value; }
        }

        public Guid idUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario= value; }
        }

        public int idFuncionAsignacion
        {
            get { return _idFuncionAsignacion; }
            set { _idFuncionAsignacion = value; }
        }

        public int operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
        }

    }
}