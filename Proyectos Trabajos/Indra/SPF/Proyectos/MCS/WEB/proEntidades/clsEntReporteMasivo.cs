using System;

namespace SICOGUA.Entidades
{
    public class clsEntReporteMasivo
    {

        private DateTime _fechaInicio;
        private DateTime _fechaFin;

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


    }
}
