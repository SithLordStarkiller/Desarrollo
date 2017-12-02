using System;

namespace SICOGUA.Entidades
{
    public class clsEntReporteTurno
    {
        private int  _idServicio;
        private int _idInstalacion;
        private int _intMes;
        private int _intAnio;
        private string _strSPF;
        private string _strContratante;
        private string _strObservaciones;

        public int idServicio
        {
            get { return _idServicio; }
            set { _idServicio = value; }
        }
        public int idInstalacion
        {
            get { return _idInstalacion; }
            set { _idInstalacion = value; }
        }
        public int intMes
        {
            get { return _intMes; }
            set { _intMes = value; }
        }
        public int intAnio
        {
            get { return _intAnio; }
            set { _intAnio = value; }
        }
        public string strSPF
        {
            get { return _strSPF; }
            set { _strSPF = value; }
        }
        public string strContratante
        {
            get { return _strContratante; }
            set { _strContratante = value; }
        }
        public string strObservaciones
        {
            get { return _strObservaciones; }
            set { _strObservaciones = value; }
        }
    }
}
