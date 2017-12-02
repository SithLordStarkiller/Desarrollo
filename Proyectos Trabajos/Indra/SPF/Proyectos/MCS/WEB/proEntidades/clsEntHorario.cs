using System;

namespace SICOGUA.Entidades
{
    public class clsEntHorario
    {
        private short _idHorario;
        private string _horHoraEntrada;
        private string _horHoraComida;
        private Boolean _horLunes;
        private Boolean _horMartes;
        private Boolean _horMiercoles;
        private Boolean _horJueves;
        private Boolean _horViernes;
        private Boolean _horSabado;
        private Boolean _horDomingo;
        private Boolean _horFestivo;
        private Boolean _horVigente;
       

        public  Guid idEmpleado {get; set;}
        public int  idHorario {get;set;}
        public DateTime ahFechaInicio { get; set; }
        public DateTime ahFechaFin { get; set; }
        public DateTime fechaCierreAsignacion { get; set; }
        public int idServicio { get; set; }
        public int idInstalacion { get; set; }

        private clsEntTipoHorario _tipoHorario;
        private clsEntHorarioComplemento _horarioComplemento;

        public short IdHorario
        {
            get { return _idHorario; }
            set { _idHorario = value; }
        }

        public string HorHoraEntrada
        {
            get { return _horHoraEntrada; }
            set { _horHoraEntrada = value; }
        }

        public string HorHoraComida
        {
            get { return _horHoraComida; }
            set { _horHoraComida = value; }
        }

        public Boolean HorLunes
        {
            get { return _horLunes; }
            set { _horLunes = value; }
        }

        public Boolean HorMartes
        {
            get { return _horMartes; }
            set { _horMartes = value; }
        }

        public Boolean HorMiercoles
        {
            get { return _horMiercoles; }
            set { _horMiercoles = value; }
        }

        public Boolean HorJueves
        {
            get { return _horJueves; }
            set { _horJueves = value; }
        }

        public Boolean HorViernes
        {
            get { return _horViernes; }
            set { _horViernes = value; }
        }

        public Boolean HorSabado
        {
            get { return _horSabado; }
            set { _horSabado = value; }
        }

        public Boolean HorDomingo
        {
            get { return _horDomingo; }
            set { _horDomingo = value; }
        }

        public Boolean HorFestivo
        {
            get { return _horFestivo; }
            set { _horFestivo = value; }
        }

        public Boolean HorVigente
        {
            get { return _horVigente; }
            set { _horVigente = value; }
        }

        public clsEntTipoHorario tipoHorario
        {
            get { return _tipoHorario; }
            set { _tipoHorario = value; }
        }

        public clsEntHorarioComplemento horarioComplemento
        {
            get { return _horarioComplemento; }
            set { _horarioComplemento = value; }
        }
    }
}
