using System;
using SICOGUA.Entidades;

namespace REA.Entidades
{
    public class clsEntHorario
    {
        private int  _idHorario;
        private clsEntServicio _servicio;
        private clsEntInstalacion _instalacion;
        private clsEntZona _zona;
        private string _tipoInstalacion;
        private string _horNombre;
        private string _horDescripcion;
        private char _horTipo;
        private DateTime _horFechaInicio;
        private byte _horJornada;
        private byte _horDescanso;
        private byte _horTolerancia;
        private byte _horRetardo;
        private Boolean _horSalidaLaboral;
        private char _horModulo;
        private byte _horTiempoMCS;
        private Boolean _horVigente;
        private Boolean _horDiaFestivo;
        private DateTime _horHoraSalidaL;
        private DateTime _horHoraEntradaL;
        private DateTime _horHoraSalidaM;
        private DateTime _horHoraEntradaM;
        private DateTime _horHoraSalidaMi;
        private DateTime _horHoraEntradaMi;
        private DateTime _horHoraSalidaJue;
        private DateTime _horHoraEntradaJue;
        private DateTime _horHoraSalidaVie;
        private DateTime _horHoraEntradaVie;
        private DateTime _horHoraSalidaSab;
        private DateTime _horHoraEntradaSab;
        private DateTime _horHoraSalidaDom;
        private DateTime _horHoraEntradaDom;
        private Boolean _horLunes;
        private Boolean _horMartes;
        private Boolean _horMiercoles;
        private Boolean _horJueves;
        private Boolean _horViernes;
        private Boolean _horSabado;
        private Boolean _horDomingo;
        private clsEntHorarioComidaREA _horarioComida;
        private Boolean _horFinesSemana;
        private Boolean _horAbierto;
        private clsEntTipoHorarioREA _tipoHorario;
           

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

        public clsEntHorarioComidaREA Comida
        {
            get { return _horarioComida; }
            set { _horarioComida = value; }
        }

        public int idHorario
        {
            get { return _idHorario; }
            set { _idHorario = value; }
        }

        public string horNombre
        {
            get { return _horNombre; }
            set { _horNombre = value; }
        }

        public string horDescripcion
        {
            get { return _horDescripcion; }
            set { _horDescripcion = value; }
        }

        public char horTipo
        {
            get { return _horTipo; }
            set { _horTipo = value;  }
        }

        public DateTime horFechaInicio
        {
            get { return _horFechaInicio; }
            set { _horFechaInicio = value; }
        }

        public byte horJornada
        {
            get { return _horJornada; }
            set { _horJornada = value; }
        }

        public byte horDescanso
        {
            get { return _horDescanso; }
            set { _horDescanso = value; }
        }

        public byte horTolerancia
        {
            get { return _horTolerancia; }
            set { _horTolerancia = value; }
        }

        public byte horRetardo
        {
            get { return _horRetardo; }
            set { _horRetardo = value; }
        }

        public Boolean horSalidaLaboral
        {
            get { return _horSalidaLaboral; }
            set { _horSalidaLaboral = value; }
        }

        public char horModulo
        {
            get { return _horModulo; }
            set { _horModulo = value; }
        }

        public byte horTiempoMCS
        {
            get { return _horTiempoMCS; }
            set { _horTiempoMCS = value; }
        }

        public Boolean horVigente
        {
            get { return _horVigente; }
            set { _horVigente = value; }
        }

        public Boolean horDiaFestivo
        {
            get { return _horDiaFestivo; }
            set { _horDiaFestivo = value; }
        }

        public DateTime horHoraSalidaL
        {
            get { return _horHoraSalidaL; }
            set { _horHoraSalidaL = value; }
                 
        }

        public DateTime horHoraEntradaL
        {
            get { return _horHoraEntradaL; }
            set { _horHoraEntradaL = value; }
        }

        public DateTime horHoraSalidaM
        {
            get { return _horHoraSalidaM; }
            set { _horHoraSalidaM = value; }

        }

        public DateTime horHoraEntradaM
        {
            get { return _horHoraEntradaM; }
            set { _horHoraEntradaM = value; }
        }


        public DateTime horHoraSalidaMi
        {
            get { return _horHoraSalidaMi; }
            set { _horHoraSalidaMi = value; }

        }

        public DateTime horHoraEntradaMi
        {
            get { return _horHoraEntradaMi; }
            set { _horHoraEntradaMi = value; }
        }

        public DateTime horHoraSalidaJue
        {
            get { return _horHoraSalidaJue; }
            set { _horHoraSalidaJue = value; }

        }

        public DateTime horHoraEntradaJue
        {
            get { return _horHoraEntradaJue; }
            set { _horHoraEntradaJue = value; }
        }

        public DateTime horHoraSalidaVie
        {
            get { return _horHoraSalidaVie; }
            set { _horHoraSalidaVie = value; }

        }

        public DateTime horHoraEntradaVie
        {
            get { return _horHoraEntradaVie; }
            set { _horHoraEntradaVie = value; }
        }


        public DateTime horHoraSalidaSa
        {
            get { return _horHoraSalidaSab; }
            set { _horHoraSalidaSab = value; }

        }

        public DateTime horHoraEntradaSa
        {
            get { return _horHoraEntradaSab; }
            set { _horHoraEntradaSab = value; }
        }

        public DateTime horHoraEntradaDom
        {
            get { return _horHoraEntradaDom; }
            set { _horHoraEntradaDom = value; }
        }

        public DateTime horHoraSalidaDom
        {
            get { return _horHoraSalidaDom; }
            set { _horHoraSalidaDom = value; }
        }

        public Boolean horLunes
        {
            get { return _horLunes; }
            set { _horLunes = value; }
        }

        public Boolean horMartes  
        {
            get { return _horMartes; }
            set { _horMartes = value; }
        }


        public Boolean horMiercoles
        {
            get { return _horMiercoles; }
            set { _horMiercoles = value; }
        }


        public Boolean horJueves
        {
            get { return _horJueves; }
            set { _horJueves = value; }
        }

        public Boolean horViernes
        {
            get { return _horViernes; }
            set { _horViernes = value; }
        }

        public Boolean horSabado
        {
            get { return _horSabado; }
            set { _horSabado = value; }
        }

        public Boolean horDomingo
        {
            get { return _horDomingo; }
            set { _horDomingo = value; }
        }

        public clsEntZona Zona
        {
            get { return _zona; }
            set { _zona = value; }
        }

        public string tipoInstalacion
        {
            get { return _tipoInstalacion; }
            set { _tipoInstalacion = value; }
        }

        public Boolean horFinesSemana
        {
            get { return _horFinesSemana; }
            set { _horFinesSemana = value; }
        }

        public Boolean horAbierto
        {
            get { return _horAbierto; }
            set { _horAbierto = value; }

        }

        public clsEntTipoHorarioREA tipoHorarioREA
        {
            get { return _tipoHorario; }
            set { _tipoHorario = value; }
        }
    }
}

