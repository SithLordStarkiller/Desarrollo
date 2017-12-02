using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntParteNovedad
    {
        private Guid _idParteNovedades;
        private DateTime _pnFecha;
        private int _idServicio;
        private string _pnEntradaFuerza;
        private string _pnSalidaFuerza;
        private string _pnAltas;
        private string _pnBajas;

        private string _pnNotaFaltistasPrimerDia;
        private string _pnNotaFaltistasSegundoDia;
        private string _pnNotaFaltistasTercerDia;
        private string _pnNotaFaltistasCuartoDia;
        private string _pnNotaRetardos;
        private string _pnNotaExceptuados;
        private string _pnNotaPresentesPrimerDia;
        private string _pnNotaPresentesSegundoDia;
        private string _pnNotaPresentesTercerDia;
        private string _pnNotaPresentesLicenciasMedicas;
        private string _pnNotaLicenciasMedicas;
        private string _pnNotaPresentesVacaciones;
        private string _pnNotaVacaciones;
        private string _pnNotaPresentesLicenciaMedica;
   
        private string _pnObservacion;
        private string _pnCopia;
        private Guid _idEmpleadoReporte;
        private Guid _idEmpleadoRecibe;

        public Guid IdParteNovedades
        {
            get { return _idParteNovedades; }
            set { _idParteNovedades = value; }
        }

        public DateTime PnFecha
        {
            get { return _pnFecha; }
            set { _pnFecha = value; }
        }

        public int IdServicio
        {
            get { return _idServicio; }
            set { _idServicio = value; }
        }

        public string PnEntradaFuerza
        {
            get { return _pnEntradaFuerza; }
            set { _pnEntradaFuerza = value; }
        }

        public string PnSalidaFuerza
        {
            get { return _pnSalidaFuerza; }
            set { _pnSalidaFuerza = value; }
        }

        public string PnAltas
        {
            get { return _pnAltas; }
            set { _pnAltas = value; }
        }

        public string PnBajas
        {
            get { return _pnBajas; }
            set { _pnBajas = value; }
        }

        public string PnObservacion
        {
            get { return _pnObservacion; }
            set { _pnObservacion = value; }
        }

        public string PnCopia
        {
            get { return _pnCopia; }
            set { _pnCopia = value; }
        }

        public Guid IdEmpleadoReporte
        {
            get { return _idEmpleadoReporte; }
            set { _idEmpleadoReporte = value; }
        }

        public Guid IdEmpleadoRecibe
        {
            get { return _idEmpleadoRecibe; }
            set { _idEmpleadoRecibe = value; }
        }

        public string PnNotaFaltistasPrimerDia
        {
            get { return _pnNotaFaltistasPrimerDia; }
            set { _pnNotaFaltistasPrimerDia = value; }
        }

        public string PnNotaFaltistasSegundoDia
        {
            get { return _pnNotaFaltistasSegundoDia; }
            set { _pnNotaFaltistasSegundoDia = value; }
        }

        public string PnNotaFaltistasTercerDia
        {
            get { return _pnNotaFaltistasTercerDia; }
            set { _pnNotaFaltistasTercerDia = value; }
        }

        public string PnNotaFaltistasCuartoDia
        {
            get { return _pnNotaFaltistasCuartoDia; }
            set { _pnNotaFaltistasCuartoDia = value; }
        }

        public string PnNotaRetardos
        {
            get { return _pnNotaRetardos; }
            set { _pnNotaRetardos = value; }
        }

        public string PnNotaExceptuados
        {
            get { return _pnNotaExceptuados; }
            set { _pnNotaExceptuados = value; }
        }

        public string PnNotaPresentesPrimerDia
        {
            get { return _pnNotaPresentesPrimerDia; }
            set { _pnNotaPresentesPrimerDia = value; }
        }

        public string PnNotaPresentesSegundoDia
        {
            get { return _pnNotaPresentesSegundoDia; }
            set { _pnNotaPresentesSegundoDia = value; }
        }

        public string PnNotaPresentesTercerDia
        {
            get { return _pnNotaPresentesTercerDia; }
            set { _pnNotaPresentesTercerDia = value; }
        }

        public string PnNotaPresentesLicenciasMedicas
        {
            get { return _pnNotaPresentesLicenciasMedicas; }
            set { _pnNotaPresentesLicenciasMedicas = value; }
        }

        public string PnNotaLicenciasMedicas
        {
            get { return _pnNotaLicenciasMedicas; }
            set { _pnNotaLicenciasMedicas = value; }
        }

        public string PnNotaPresentesVacaciones
        {
            get { return _pnNotaPresentesVacaciones; }
            set { _pnNotaPresentesVacaciones = value; }
        }

        public string PnNotaVacaciones
        {
            get { return _pnNotaVacaciones; }
            set { _pnNotaVacaciones = value; }
        }

        public string PnNotaPresentesLicenciaMedica
        {
            get { return _pnNotaPresentesLicenciaMedica; }
            set { _pnNotaPresentesLicenciaMedica = value; }
        }
    }
}