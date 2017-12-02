using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntTipoHorario
    {
        private short _idTipoHorario;
        private string _thDescripcion;
        private byte _thJornada;
        private byte _thDescanso;
        private Boolean _thIncluyeComida;
        private byte _thTiempoComida;
        private byte _thTolerancia;
        private byte _thRetardo;
        private Boolean _thMixto;
        private Boolean _thVigente;


        public short IdTipoHorario
        {
            get { return _idTipoHorario; }
            set { _idTipoHorario = value; }
        }

        public string ThDescripcion
        {
            get { return _thDescripcion; }
            set { _thDescripcion = value; }
        }

        public byte ThJornada
        {
            get { return _thJornada; }
            set { _thJornada = value; }
        }

        public byte ThDescanso
        {
            get { return _thDescanso; }
            set { _thDescanso = value; }
        }

        public Boolean ThIncluyeComida
        {
            get { return _thIncluyeComida; }
            set { _thIncluyeComida = value; }
        }

        public byte ThTiempoComida
        {
            get { return _thTiempoComida; }
            set { _thTiempoComida = value; }
        }

        public byte ThTolerancia
        {
            get { return _thTolerancia; }
            set { _thTolerancia = value; }
        }

        public byte ThRetardo
        {
            get { return _thRetardo; }
            set { _thRetardo = value; }
        }

        public Boolean ThMixto
        {
            get { return _thMixto; }
            set { _thMixto = value; }
        }

        public Boolean ThVigente
        {
            get { return _thVigente; }
            set { _thVigente = value; }
        }
    }
}
