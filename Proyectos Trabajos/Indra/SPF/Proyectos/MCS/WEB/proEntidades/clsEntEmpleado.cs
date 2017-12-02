using System;
using System.Collections.Generic;
using REA.Entidades;

namespace SICOGUA.Entidades
{
    public class clsEntEmpleado
    {
        private Guid _idEmpleado; // Refractor/Encapsulated Field..
        private Guid _idUsuario;

        private short _idPais;
        private string _pais;
        private short _idEstado;
        private string _estado;
        private short _idMunicipio;
        private string _municipio;
        private string _sangre;
        private string _empPaterno;
        private string _empMaterno;
        private string _empNombre;
        private int _empNumero;
        private string _empRFC;
        private char _empSexo;
        private string _empSexoValor;
        private string _empCURP;
        private DateTime _empFechaIngreso;
        private DateTime _empFechaBaja;
        private DateTime _empFechaNacimiento;
        private string _empCUIP;
        private DateTime _fechaIniCom;
        private int _idJerarquia;
        private string _jerDescripcion;
        private int _tipo;
        private DateTime _fechaFinAsignacion;
        private int _idRevision;

        private int _idRenuncia;
        private string _observaciones;


        private List<clsEntEmpleadoAsignacion> _empleadoAsignacion;
        private clsEntEmpleadoRecibeParteNovedad _empleadoRecibeParteNovedad;

        //-------------Agregue
        private clsEntEmpleadoAsignacion _empleadoAsignacion2;

        private clsEntEmpleadoEnviaParteNovedad _empleadoEnviaParteNovedad;
        private clsEntEmpleadoDescripcionFisica _empleadoDescripcionFisica;
        private clsEntEmpleadoMediaFiliacion _empleadoMediaFiliacion;
        private clsEntEmpleadoAsignacionOS _empleadoAsignacionOS;
        private clsEntEmpleadoHorario _empleadoHorario;
        private clsEntPaseLista _paseLista;
        private clsEntAsistencia _Asistencia;
        private clsEntEmpleadoPuesto _empleadoPuesto;
        private clsEntEmpleadoAutorizaIncidencia _empleadoAutorizaIncidencia;
        private List<clsEntIncidencia> _incidencia;



        //agregado 18-04-2011
        private int _empLOC;
        private int _empCurso;
        private string _empCartilla;

        //agregado 26092012
        private List<clsEntIncidenciaREA> _incidenciaREA;
        private clsEntAsignacionHorarioREA _horarioREA;

        private byte[] _empFoto;

        public DateTime fechaFinAsignacion
        {
            get { return _fechaFinAsignacion; }
            set { _fechaFinAsignacion = value; }
        }

        public int IdJerarquia
        {
            get { return _idJerarquia; }
            set { _idJerarquia = value; }
        }

        public string jerDescripcion
        {
            get { return _jerDescripcion; }
            set { _jerDescripcion = value; }
        }

        public int tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public Guid IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }

        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public DateTime fechaIniCom
        {
            get { return _fechaIniCom; }
            set { _fechaIniCom = value; }
        }




        public short IdPais
        {
            get { return _idPais; }
            set { _idPais = value; }
        }

        public short IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }

        public short IdMunicipio
        {
            get { return _idMunicipio; }
            set { _idMunicipio = value; }
        }

        public string EmpPaterno
        {
            get { return _empPaterno; }
            set { _empPaterno = value; }
        }

        public string EmpMaterno
        {
            get { return _empMaterno; }
            set { _empMaterno = value; }
        }

        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        public int idRevision
        {
            get { return _idRevision; }
            set { _idRevision = value; }
        }
        public int idRenuncia
        {
            get { return _idRenuncia; }
            set { _idRenuncia = value; }
        }

        public string EmpNombre
        {
            get { return _empNombre; }
            set { _empNombre = value; }
        }

        public int EmpNumero
        {
            get { return _empNumero; }
            set { _empNumero = value; }
        }

        public string EmpRFC
        {
            get { return _empRFC; }
            set { _empRFC = value; }
        }

        public char EmpSexo
        {
            get { return _empSexo; }
            set { _empSexo = value; }
        }

        public string EmpCURP
        {
            get { return _empCURP; }
            set { _empCURP = value; }
        }

        public DateTime EmpFechaIngreso
        {
            get { return _empFechaIngreso; }
            set { _empFechaIngreso = value; }
        }

        public DateTime EmpFechaBaja
        {
            get { return _empFechaBaja; }
            set { _empFechaBaja = value; }
        }

        public DateTime EmpFechaNacimiento
        {
            get { return _empFechaNacimiento; }
            set { _empFechaNacimiento = value; }
        }

        public string EmpCUIP
        {
            get { return _empCUIP; }
            set { _empCUIP = value; }
        }

        public List<clsEntEmpleadoAsignacion> EmpleadoAsignacion
        {
            get { return _empleadoAsignacion; }
            set { _empleadoAsignacion = value; }
        }

        public clsEntEmpleadoRecibeParteNovedad EmpleadoRecibeParteNovedad
        {
            get { return _empleadoRecibeParteNovedad; }
            set { _empleadoRecibeParteNovedad = value; }
        }
        public clsEntEmpleadoEnviaParteNovedad EmpleadoEnviaParteNovedad
        {
            get { return _empleadoEnviaParteNovedad; }
            set { _empleadoEnviaParteNovedad = value; }
        }

        public clsEntEmpleadoDescripcionFisica EmpleadoDescripcionFisica
        {
            get { return _empleadoDescripcionFisica; }
            set { _empleadoDescripcionFisica = value; }
        }

        public clsEntEmpleadoMediaFiliacion EmpleadoMediaFiliacion
        {
            get { return _empleadoMediaFiliacion; }
            set { _empleadoMediaFiliacion = value; }
        }

        public clsEntEmpleadoAsignacionOS EmpleadoAsignacionOS
        {
            get { return _empleadoAsignacionOS; }
            set { _empleadoAsignacionOS = value; }
        }

        public clsEntEmpleadoHorario EmpleadoHorario
        {
            get { return _empleadoHorario; }
            set { _empleadoHorario = value; }
        }

        public clsEntPaseLista PaseLista
        {
            get { return _paseLista; }
            set { _paseLista = value; }
        }

        public clsEntAsistencia Asistencia
        {
            get { return _Asistencia; }
            set { _Asistencia = value; }
        }

        public clsEntEmpleadoPuesto EmpleadoPuesto
        {
            get { return _empleadoPuesto; }
            set { _empleadoPuesto = value; }
        }

        public clsEntEmpleadoAutorizaIncidencia EmpleadoAutorizaIncidencia
        {
            get { return _empleadoAutorizaIncidencia; }
            set { _empleadoAutorizaIncidencia = value; }
        }

        public List<clsEntIncidencia> Incidencias
        {
            get { return _incidencia; }
            set { _incidencia = value; }
        }


        public string Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }

        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public string Municipio
        {
            get { return _municipio; }
            set { _municipio = value; }
        }

        public string Sangre
        {
            get { return _sangre; }
            set { _sangre = value; }
        }

        public string EmpSexoValor
        {
            get { return _empSexoValor; }
            set { _empSexoValor = value; }
        }

        //------------agregue
        public clsEntEmpleadoAsignacion EmpleadoAsignacion2
        {
            get { return _empleadoAsignacion2; }
            set { _empleadoAsignacion2 = value; }
        }

        //agregado 18 de abril de 2011

        public int EmpLOC
        {
            get { return _empLOC; }
            set { _empLOC = value; }
        }

        public int EmpCurso
        {
            get { return _empCurso; }
            set { _empCurso = value; }
        }

        public string EmpCartilla
        {
            get { return _empCartilla; }
            set { _empCartilla = value; }
        }
     
        //agregado 26092012
        public List<clsEntIncidenciaREA> incidenciaREA
        {
            get { return _incidenciaREA; }
            set { _incidenciaREA = value; }
         }

        public clsEntAsignacionHorarioREA horarioREA
        {
            get { return _horarioREA; }
            set { _horarioREA = value; }
        }
        public byte[] empFoto
        {
            get { return _empFoto; }
            set { _empFoto = value; }
        }
    }
}
