using System;


namespace REA.Entidades
{
    public class clsEntTipoHorarioREA
    {
        private byte _idTipoHorario;
        private string _thDescripcion;
        private byte _thDescanso;
        private byte _thJornada;
        private char _thTurno;



        public byte idTipoHorario
        {
            get { return _idTipoHorario; }
            set { _idTipoHorario = value; }
        }

        public string thDescripcion
        {
            get { return _thDescripcion; }
            set { _thDescripcion = value; }

        }

        public byte thDescanso
        {
            get { return _thDescanso; }
            set { _thDescanso = value; }
        }

        public byte thJornada
        {
            get { return _thJornada; }
            set { _thJornada = value; }
        }

        public char thTurno
        {
            get { return _thTurno; }
            set { _thTurno = value; }
        }
    }
}
