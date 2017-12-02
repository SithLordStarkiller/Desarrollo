using System;

namespace SICOGUA.Entidades
{
    public class clsEntReporteListado
    {
        private clsEntZona _zona;
        private clsEntServicio _servicio;
        private clsEntInstalacion _instalacion;
        private DateTime _fechaReporte;
        private int _idServicio;
        public int idEstatus { get; set; }

        public clsEntZona Zona
        {
            get { return _zona; }
            set { _zona = value; }
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

        public DateTime FechaReporte
        {
            get { return _fechaReporte; }
            set { _fechaReporte = value; }
        }

        public int IdServicio
        {
            get { return _idServicio; }
            set { _idServicio = value; }
        }
    }
}
