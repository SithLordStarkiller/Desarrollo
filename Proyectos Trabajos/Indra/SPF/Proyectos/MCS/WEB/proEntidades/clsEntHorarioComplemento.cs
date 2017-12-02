using System;

namespace SICOGUA.Entidades
{
    public class clsEntHorarioComplemento
    {
        private short _idHorario;
        private short _idTipoHorario;
        private byte _idHorariocomplemento;
        private string _hcHoraEntrada;
        private string _hcHoraComida;
        private Boolean _hcLunes;
        private Boolean _hcMartes;
        private Boolean _hcMiercoles;
        private Boolean _hcJueves;
        private Boolean _hcViernes;
        private Boolean _hcSabado;
        private Boolean _hcDomingo;


        public short IdHorario
        {
            get { return _idHorario; }
            set { _idHorario = value; }
        }

        public short IdTipoHorario
        {
            get { return _idTipoHorario; }
            set { _idTipoHorario = value; }
        }

        public byte IdHorariocomplemento
        {
            get { return _idHorariocomplemento; }
            set { _idHorariocomplemento = value; }
        }

        public string HcHoraEntrada
        {
            get { return _hcHoraEntrada; }
            set { _hcHoraEntrada = value; }
        }

        public string HcHoraComida
        {
            get { return _hcHoraComida; }
            set { _hcHoraComida = value; }
        }

        public Boolean HcLunes
        {
            get { return _hcLunes; }
            set { _hcLunes = value; }
        }

        public Boolean HcMartes
        {
            get { return _hcMartes; }
            set { _hcMartes = value; }
        }

        public Boolean HcMiercoles
        {
            get { return _hcMiercoles; }
            set { _hcMiercoles = value; }
        }

        public Boolean HcJueves
        {
            get { return _hcJueves; }
            set { _hcJueves = value; }
        }

        public Boolean HcViernes
        {
            get { return _hcViernes; }
            set { _hcViernes = value; }
        }

        public Boolean HcSabado
        {
            get { return _hcSabado; }
            set { _hcSabado = value; }
        }

        public Boolean HcDomingo
        {
            get { return _hcDomingo; }
            set { _hcDomingo = value; }
        }
     }
}
