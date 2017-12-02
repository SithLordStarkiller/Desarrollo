using System;
using System;
using System.Collections.Generic;
using System.Text;
using SICOGUA.Entidades;

namespace SICOGUA.Entidades
{
    public class clsEntAnexoTecnico
    {
        private clsEntInstalacion _instalacion;
        private clsEntServicio _servicio;
        private DateTime _fechaInicio;
        private DateTime _fechaFin;
        private string _strConvenio;
        private List<clsEntAnexoJerarquiaHorario> _anexoJerarquiaHorario;
        private clsEntZona _zona;
      
        
        public clsEntInstalacion instalacion
        {
            get { return _instalacion; }
            set { _instalacion = value; }
        }
        public clsEntServicio servicio
        {
            get { return _servicio; }
            set { _servicio = value; }
        }
        public DateTime fechaInicio
        {
            get { return _fechaInicio; }
            set { _fechaInicio = value; }
        }
        public DateTime fechaFin
        {
            get { return _fechaFin; }
            set { _fechaFin = value; }
        }
        public string strConvenio
        {
            get { return _strConvenio; }
            set { _strConvenio = value; }
        }
        public List<clsEntAnexoJerarquiaHorario> anexoJerarquiaHorario
        {
            get { return _anexoJerarquiaHorario; }
            set { _anexoJerarquiaHorario = value; }
        }
        public clsEntZona zona
        {
            get { return _zona; }
            set { _zona = value; }
        }
   
    }
    public class clsEntAnexoJerarquiaHorario
    {
        public int _totalHombres;
        public int _totalMujeres;
        public int _totalIndistinto;
        public int _idTipoHorario;
        public int _idTurno;
        public int _idJerarquia;
        private clsEntDia _dia;
        private clsEntJerarquia _jerarquia;
        private int _masculinoLunes;
        private int _femeninoLunes;
        private int _indistintoLunes;
        private int _masculinoMartes;
        private int _femeninoMartes;
        private int _indistintoMartes;
        private int _masculinoMiercoles;
        private int _femeninoMiercoles;
        private int _indistintoMiercoles;
        private int _masculinoJueves;
        private int _femeninoJueves;
        private int _indistintoJueves;
        private int _masculinoViernes;
        private int _femeninoViernes;
        private int _indistintoViernes;
        private int _masculinoSabado;
        private int _femeninoSabado;
        private int _indistintoSabado;
        private int _masculinoDomingo;
        private int _femeninoDomingo;
        private int _indistintoDomingo;
        private string _lunes;
        private string _martes;
        private string _miercoles;
        private string _jueves;
        private string _viernes;
        private string _sabado;
        private string _domingo;
        private bool _vigente;
        private string _thHorario;
        private string _thTurno;
        private string _jerDescripcion;


        public int idTipoHorario
        {
            get { return _idTipoHorario; }
            set { _idTipoHorario = value; }
        }
        public int idTurno
        {
            get { return _idTurno; }
            set { _idTurno = value; }
        }
        public int idJerarquia
        {
            get { return _idJerarquia; }
            set { _idJerarquia = value; }
        }

        public int totalHombres 
        {
            get { return _totalHombres; }
            set { _totalHombres = value;}
        }
        public int totalMujeres
        {
            get { return _totalMujeres; }
            set { _totalMujeres = value; }
        }
        public int totalIndistinto
        {
            get { return _totalIndistinto; }
            set { _totalIndistinto = value; }
        }

        public clsEntDia dia
        {
            get { return _dia; }
            set { _dia = value; }
        }
        public clsEntJerarquia jerarquia
        {
            get { return _jerarquia; }
            set { _jerarquia = value; }
        }
        public int masculinoLunes
        {
            get { return _masculinoLunes; }
            set { _masculinoLunes = value; }
        }
        public int femeninoLunes
        {
            get { return _femeninoLunes; }
            set { _femeninoLunes = value; }
        }
        public int indistintoLunes
        {
            get { return _indistintoLunes; }
            set { _indistintoLunes = value; }
        }
        public int masculinoMartes
        {
            get { return _masculinoMartes; }
            set { _masculinoMartes = value; }
        }
        public int femeninoMartes
        {
            get { return _femeninoMartes; }
            set { _femeninoMartes = value; }
        }
        public int indistintoMartes
        {
            get { return _indistintoMartes; }
            set { _indistintoMartes = value; }
        }
        public int masculinoMiercoles
        {
            get { return _masculinoMiercoles; }
            set { _masculinoMiercoles = value; }
        }
        public int femeninoMiercoles
        {
            get { return _femeninoMiercoles; }
            set { _femeninoMiercoles = value; }
        }
        public int indistintoMiercoles
        {
            get { return _indistintoMiercoles; }
            set { _indistintoMiercoles = value; }
        }
        public int masculinoJueves
        {
            get { return _masculinoJueves; }
            set { _masculinoJueves = value; }
        }
        public int femeninoJueves
        {
            get { return _femeninoJueves; }
            set { _femeninoJueves = value; }
        }
        public int indistintoJueves
        {
            get { return _indistintoJueves; }
            set { _indistintoJueves = value; }
        }
        public int masculinoViernes
        {
            get { return _masculinoViernes; }
            set { _masculinoViernes = value; }
        }
        public int femeninoViernes
        {
            get { return _femeninoViernes; }
            set { _femeninoViernes = value; }
        }
        public int indistintoViernes
        {
            get { return _indistintoViernes; }
            set { _indistintoViernes = value; }
        }
        public int masculinoSabado
        {
            get { return _masculinoSabado; }
            set { _masculinoSabado = value; }
        }
        public int femeninoSabado
        {
            get { return _femeninoSabado; }
            set { _femeninoSabado = value; }
        }
        public int indistintoSabado
        {
            get { return _indistintoSabado; }
            set { _indistintoSabado = value; }
        }
        public int masculinoDomingo
        {
            get { return _masculinoDomingo; }
            set { _masculinoDomingo = value; }
        }
        public int femeninoDomingo
        {
            get { return _femeninoDomingo; }
            set { _femeninoDomingo = value; }
        }
        public int indistintoDomingo
        {
            get { return _indistintoDomingo; }
            set { _indistintoDomingo = value; }
        }
        public string lunes 
        {
            get { return _lunes; }
            set { _lunes = value; }
        }
        public string martes
        {
            get { return _martes; }
            set { _martes = value; }
        }
        public string miercoles
        {
            get { return _miercoles; }
            set { _miercoles = value; }
        }
        public string jueves
        {
            get { return _jueves; }
            set { _jueves = value; }
        }
        public string viernes
        {
            get { return _viernes; }
            set { _viernes = value; }
        }
        public string sabado
        {
            get { return _sabado; }
            set { _sabado = value; }
        }
        public string domingo
        {
            get { return _domingo; }
            set { _domingo = value; }
        }
        public bool vigente
        {
            get { return _vigente; }
            set { _vigente = value; }
        }
        public string thHorario
        {
            get { return _thHorario; }
            set { _thHorario = value; }
        }
        public string thTurno
        {
            get { return _thTurno; }
            set { _thTurno = value; }
        }
        public string jerDescripcion
        {
            get { return _jerDescripcion; }
            set { _jerDescripcion = value; }
        }
    }
    public class clsEntDia
    {
        private int _idDia;
        private string _diaDescripcion;
        private string _diaIniciales;

        public int idDia
        {
            get { return _idDia; }
            set { _idDia = value; }
        }
        public string diaDescripcion
        {
            get { return _diaDescripcion; }
            set { _diaDescripcion = value; }
        }

        public string diaIniciales
        {
            get { return _diaIniciales; }
            set { _diaIniciales = value; }
        }
    }
}