using System;
namespace SICOGUA.Entidades
{
    public class clsEntDomicilio
    {
        private int             _idInstalacion;
        private int             _idDomicilio;
        private int             _idServicio;
        private int             _idAsentamiento;
        private int             _idEstado;
        private int             _idMunicipio;
        private string          _domCalle;
        private string          _domNumeroExterior;
        private string          _domNumeroInterior;
        private bool            _domVigente;
        private string          _domCp;


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

        public int idAsentamiento
        {
            get { return _idAsentamiento; }
            set { _idAsentamiento = value; }
        }

        public int idEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }

        public int idMunicipio
        {
            get { return _idMunicipio; }
            set { _idMunicipio = value; }
        }

        public string domCalle
        {
            get { return _domCalle; }
            set { _domCalle = value; }
        }

        public string domNumeroExterior
        {
            get { return _domNumeroExterior; }
            set { _domNumeroExterior = value; }
        }

        public string domNumeroInterior
        {
            get { return _domNumeroInterior; }
            set { _domNumeroInterior = value; }
        }

        public bool domVigente
        {
            get { return _domVigente; }
            set { _domVigente = value; }
        }

        public string domCp
        {
            get { return _domCp; }
            set { _domCp = value; }
        }

        public int idDomicilio
        {
            get { return _idDomicilio; }
            set { _idDomicilio= value; }
        }
    }
}