using System;

namespace SICOGUA.Entidades
{
    public class clsEntServicio
    {
        private int         _operacion;
        private int         _idServicio;
        private int         _idTipoServicio;
        private Guid        _idEmpleado;    
        private string      _serDescripcion;
        private bool        _serVigente;
        private string      _serRazonSocial;
        private string      _serRfc;
        private DateTime    _serFechaInicio;
        private DateTime    _serFechaFin;
        private string      _serObservaciones;
        private byte[]      _serLogotipo;
        private string      _serPaginaWeb;
        private int         _idCategoriaServicio;
        private clsEntTipoServicio _obTipoServicio;

        public int idServicio
        {
            get { return _idServicio; }
            set { _idServicio = value; }
        }

        public int IdTipoServicio
        {
            get { return _idTipoServicio; }
            set { _idTipoServicio = value; }
        }

        public Guid idEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public string serDescripcion
        {
            get { return _serDescripcion; }
            set { _serDescripcion = value; }
        }

        public bool serVigente
        {
            get { return _serVigente; }
            set { _serVigente = value; }
        }


        public string serRazonSocial
        {
            get { return _serRazonSocial; }
            set { _serRazonSocial = value; }
        }


        public string serRfc
        {
            get { return _serRfc; }
            set { _serRfc = value; }
        }


        public DateTime serFechaInicio
        {
            get { return _serFechaInicio; }
            set { _serFechaInicio = value; }
        }


        public DateTime serFechaFin
        {
            get { return _serFechaFin; }
            set { _serFechaFin = value; }
        }


        public string serObservaciones
        {
            get { return _serObservaciones; }
            set { _serObservaciones = value; }
        }


        public byte[] serLogotipo
        {
            get { return _serLogotipo; }
            set { _serLogotipo = value; }
        }


        public string serPaginaWeb
        {
            get { return _serPaginaWeb; }
            set { _serPaginaWeb = value; }
        }


        public int operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
        }

        public int idCategoriaServicio
        {
            get { return _idCategoriaServicio; }
            set { _idCategoriaServicio = value; }
        }
        public clsEntTipoServicio objTipoServicio
        {
            get { return _obTipoServicio; }
            set { _obTipoServicio = value; }
        }
    }
}
