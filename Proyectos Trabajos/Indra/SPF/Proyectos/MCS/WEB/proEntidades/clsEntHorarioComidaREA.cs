using System;


namespace REA.Entidades
{
   public class clsEntHorarioComidaREA
    {
        private int _idHorario;
        private byte _idHorarioComida;
        private DateTime _hcComidaEntrada;
        private DateTime _hcComidaSalida;
        private byte _hcMinuto;
        private bool _hcVigente;
        private bool _hcTiempoComida;


        public int IdHorario
        {
            get { return _idHorario; }
            set { _idHorario = value; }
        }

        public byte idHorarioComida
        {
            get { return _idHorarioComida; }
            set { _idHorarioComida = value; }
        }

        public DateTime hcComidaEntrada
        {
            get { return _hcComidaEntrada; }
            set { _hcComidaEntrada = value; }
        }

        public DateTime hcComidaSalida
        {
            get { return _hcComidaSalida; }
            set { _hcComidaSalida = value; }
        }

        public byte hcMinuto
        {
            get { return _hcMinuto; }
            set { _hcMinuto = value; }

        }

        public bool hcVigente
        {
            get { return _hcVigente; }
            set { _hcVigente = value; }
        }

        public bool hcTiempoComida
        {
            get { return _hcTiempoComida; }
            set { _hcTiempoComida = value; }
        }
    }
}
