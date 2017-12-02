using System;

namespace SICOGUA.Entidades
{
    public class clsEntReporteIncidente
    {
        private int _idIncidente;
        private clsEntServicio _servicio;
        private clsEntInstalacion _instalacion;
        private DateTime _riFechaHora;

        private clsEntJerarquia _jerarquiaEmpleadoInvolucrado;
        private clsEntZona _zonaEmpleadoInvolucrado;
        private string _nombreEmpleadoInvolucrado;
        private Guid _idEmpleadoInvolucrado;
        private Int16 _idEmpleadoPuestoInvolucrado;
        private Int32 _noEmpleadoInvolucrado;

        private string _riLugar;
        private string _riActividad;
        private string _riUniforme;
        private string _riDesarrolloConsecuencia;
        private string _riLesion;
        private string _riUbicacionCadaverLesionado;
        private string _riAccionVsAgresor;

        private clsEntJerarquia _jerarquiaEmpleadoTomaNota;
        private clsEntZona _zonaEmpleadoTomaNota;
        private string _nombreEmpleadoTomaNota;
        private Guid _idEmpleadoTomaNota;
        private Int16 _idEmpleadoPuestoTomaNota;
        private Int32 _noEmpleadoTomaNota;
        private string _riAccionMando;

        private clsEntJerarquia _jerarquiaEmpleadoAutor;
        private clsEntZona _zonaEmpleadoAutor;
        private string _nombreEmpleadoAutor;
        private Guid _idEmpleadoAutor;
        private Int16 _idEmpleadoPuestoAutor;
        private Int32 _noEmpleadoAutor;

        private clsEntJerarquia _jerarquiaEmpleadoSuperior;
        private clsEntZona _zonaEmpleadoSuperior;
        private string _nombreEmpleadoSuperior;
        private Guid _idEmpleadoSuperior;
        private Int16 _idEmpleadoPuestoSuperior;
        private Int32 _noEmpleadoSuperior;

        private string _riDanioMaterial;
        private double _riMonto;

        private string _tarjeta;
        private string _sede;
        private string _resumen;

        public int IdIncidente
        {
            get { return _idIncidente; }
            set { _idIncidente = value; }
        }

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

        public DateTime RiFechaHora
        {
            get { return _riFechaHora; }
            set { _riFechaHora = value; }
        }

        public Guid IdEmpleadoInvolucrado
        {
            get { return _idEmpleadoInvolucrado; }
            set { _idEmpleadoInvolucrado = value; }
        }

        public short IdEmpleadoPuestoInvolucrado
        {
            get { return _idEmpleadoPuestoInvolucrado; }
            set { _idEmpleadoPuestoInvolucrado = value; }
        }

        public string RiLugar
        {
            get { return _riLugar; }
            set { _riLugar = value; }
        }

        public string RiActividad
        {
            get { return _riActividad; }
            set { _riActividad = value; }
        }

        public string RiUniforme
        {
            get { return _riUniforme; }
            set { _riUniforme = value; }
        }

        public string RiDesarrolloConsecuencia
        {
            get { return _riDesarrolloConsecuencia; }
            set { _riDesarrolloConsecuencia = value; }
        }

        public string RiLesion
        {
            get { return _riLesion; }
            set { _riLesion = value; }
        }

        public string RiUbicacionCadaverLesionado
        {
            get { return _riUbicacionCadaverLesionado; }
            set { _riUbicacionCadaverLesionado = value; }
        }

        public string RiAccionVsAgresor
        {
            get { return _riAccionVsAgresor; }
            set { _riAccionVsAgresor = value; }
        }

        public Guid IdEmpleadoTomaNota
        {
            get { return _idEmpleadoTomaNota; }
            set { _idEmpleadoTomaNota = value; }
        }

        public short IdEmpleadoPuestoTomaNota
        {
            get { return _idEmpleadoPuestoTomaNota; }
            set { _idEmpleadoPuestoTomaNota = value; }
        }

        public string RiAccionMando
        {
            get { return _riAccionMando; }
            set { _riAccionMando = value; }
        }

        public Guid IdEmpleadoAutor
        {
            get { return _idEmpleadoAutor; }
            set { _idEmpleadoAutor = value; }
        }

        public short IdEmpleadoPuestoAutor
        {
            get { return _idEmpleadoPuestoAutor; }
            set { _idEmpleadoPuestoAutor = value; }
        }

        public Guid IdEmpleadoSuperior
        {
            get { return _idEmpleadoSuperior; }
            set { _idEmpleadoSuperior = value; }
        }

        public short IdEmpleadoPuestoSuperior
        {
            get { return _idEmpleadoPuestoSuperior; }
            set { _idEmpleadoPuestoSuperior = value; }
        }

        public string RiDanioMaterial
        {
            get { return _riDanioMaterial; }
            set { _riDanioMaterial = value; }
        }

        public double RiMonto
        {
            get { return _riMonto; }
            set { _riMonto = value; }
        }

        public clsEntZona ZonaEmpleadoInvolucrado
        {
            get { return _zonaEmpleadoInvolucrado; }
            set { _zonaEmpleadoInvolucrado = value; }
        }

        public clsEntJerarquia JerarquiaEmpleadoInvolucrado
        {
            get { return _jerarquiaEmpleadoInvolucrado; }
            set { _jerarquiaEmpleadoInvolucrado = value; }
        }

        public string NombreEmpleadoInvolucrado
        {
            get { return _nombreEmpleadoInvolucrado; }
            set { _nombreEmpleadoInvolucrado = value; }
        }

        public int NoEmpleadoInvolucrado
        {
            get { return _noEmpleadoInvolucrado; }
            set { _noEmpleadoInvolucrado = value; }
        }

        public clsEntJerarquia JerarquiaEmpleadoTomaNota
        {
            get { return _jerarquiaEmpleadoTomaNota; }
            set { _jerarquiaEmpleadoTomaNota = value; }
        }

        public clsEntZona ZonaEmpleadoTomaNota
        {
            get { return _zonaEmpleadoTomaNota; }
            set { _zonaEmpleadoTomaNota = value; }
        }

        public string NombreEmpleadoTomaNota
        {
            get { return _nombreEmpleadoTomaNota; }
            set { _nombreEmpleadoTomaNota = value; }
        }

        public int NoEmpleadoTomaNota
        {
            get { return _noEmpleadoTomaNota; }
            set { _noEmpleadoTomaNota = value; }
        }

        public clsEntJerarquia JerarquiaEmpleadoAutor
        {
            get { return _jerarquiaEmpleadoAutor; }
            set { _jerarquiaEmpleadoAutor = value; }
        }

        public clsEntZona ZonaEmpleadoAutor
        {
            get { return _zonaEmpleadoAutor; }
            set { _zonaEmpleadoAutor = value; }
        }

        public string NombreEmpleadoAutor
        {
            get { return _nombreEmpleadoAutor; }
            set { _nombreEmpleadoAutor = value; }
        }

        public int NoEmpleadoAutor
        {
            get { return _noEmpleadoAutor; }
            set { _noEmpleadoAutor = value; }
        }

        public clsEntJerarquia JerarquiaEmpleadoSuperior
        {
            get { return _jerarquiaEmpleadoSuperior; }
            set { _jerarquiaEmpleadoSuperior = value; }
        }

        public clsEntZona ZonaEmpleadoSuperior
        {
            get { return _zonaEmpleadoSuperior; }
            set { _zonaEmpleadoSuperior = value; }
        }

        public string NombreEmpleadoSuperior
        {
            get { return _nombreEmpleadoSuperior; }
            set { _nombreEmpleadoSuperior = value; }
        }

        public int NoEmpleadoSuperior
        {
            get { return _noEmpleadoSuperior; }
            set { _noEmpleadoSuperior = value; }
        }

        public string Tarjeta
        {
            get { return _tarjeta; }
            set { _tarjeta = value; }
        }

        public string Sede
        {
            get { return _sede; }
            set { _sede = value; }
        }

        public string Resumen
        {
            get { return _resumen; }
            set { _resumen = value; }
        }
    }
}
