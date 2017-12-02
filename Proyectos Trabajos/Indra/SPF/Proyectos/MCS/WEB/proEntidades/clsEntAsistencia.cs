using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntAsistencia
    {
        private Guid _idEmpleado;
        private short _idEmpleadoHorario;
        private int _idAsistencia;
        private byte _idTipoAsistencia;
        private DateTime _asiEntrada;
        private DateTime _asiSalida;
        private DateTime _asiEntradaComida;
        private DateTime _asiSalidaComida;
        private string _strTipoAsistencia;


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

        public int IdAsistencia
        {
            get { return _idAsistencia; }
            set { _idAsistencia = value; }
        }

        public byte IdTipoAsistencia
        {
            get { return _idTipoAsistencia; }
            set { _idTipoAsistencia = value; }
        }

        public DateTime AsiEntrada
        {
            get { return _asiEntrada; }
            set { _asiEntrada = value; }
        }

        public DateTime AsiSalida
        {
            get { return _asiSalida; }
            set { _asiSalida = value; }
        }

        public DateTime AsiEntradaComida
        {
            get { return _asiEntradaComida; }
            set { _asiEntradaComida = value; }
        }

        public DateTime AsiSalidaComida
        {
            get { return _asiSalidaComida; }
            set { _asiSalidaComida = value; }
        }
        public string strTipoAsistencia 
        {
            get { return _strTipoAsistencia; }
            set { _strTipoAsistencia = value; }
        }
    }
}
