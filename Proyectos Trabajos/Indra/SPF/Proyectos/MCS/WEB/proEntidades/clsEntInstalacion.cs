using System;
namespace SICOGUA.Entidades
{
    [Serializable]
    public class clsEntInstalacion
    {
        private int         _idServicio;
        private int         _idInstalacion;
        private Guid        _idEmpleado;
        private int         _idZona;
        private string      _insNombre;
        private bool        _insVigente;
        private DateTime    _insFechaInicio;
        private DateTime    _insFechaFin;
        private double       _insLongitud;
        private double       _insLatitud;
        private string      _insConvenio;
        private int         _insElementosTurno;
        private int         _insElementosArmados;
        private int         _insElementosMasculino;
        private int         _insElementosFemenino;
        private string      _insColindancias;
        private string      _insFunciones;
        private string      _insDescripcion;
        private int         _idServicioAntes;
        private int         _operacion;
        private int _idClasificacion;
        private int _idInstalacionEquipo;
        private int _idTipoInstalacion;
        private clsEntServicio _servicio;
        private clsEntZona _zona;
        //private clsEntZonaHoraria _ZonaHoraria;

        public int idClasificacion
        {
            get { return _idClasificacion; }
            set { _idClasificacion = value; }
        }
        public int IdServicio
        {
            get { return _idServicio; }
            set { _idServicio = value; }
        }

        public int IdInstalacion
        {
            get { return _idInstalacion; }
            set { _idInstalacion = value; }
        }

        public Guid idEmpelado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public int idZona
        {
            get { return _idZona; }
            set { _idZona = value; }
        }

        public string InsNombre
        {
            get { return _insNombre; }
            set { _insNombre = value; }
        }

        public bool insVigente
        {
            get { return _insVigente; }
            set { _insVigente = value; }
        }

        public DateTime insFechaInicio
        {
            get { return _insFechaInicio; }
            set { _insFechaInicio = value; }
        }

        public DateTime insFechaFin
        {
            get { return _insFechaFin; }
            set { _insFechaFin = value; }
        }

        public double insLogitud
        {
            get { return _insLongitud; }
            set { _insLongitud = value; }
        }

        public double insLatitud
        {
            get { return _insLatitud; }
            set { _insLatitud = value; }
        }

        public string insConvenio
        {
            get { return _insConvenio; }
            set { _insConvenio = value; }
        }

        public int insElementosTurno
        {
            get { return _insElementosTurno; }
            set { _insElementosTurno = value; }
        }

        public int insElementosArmados
        {
            get { return _insElementosArmados; }
            set { _insElementosArmados = value; }
        }

        public int insElementosMasculino
        {
            get { return _insElementosMasculino; }
            set { _insElementosMasculino = value; }
        }

        public int insElementosFemenino
        {
            get { return _insElementosFemenino; }
            set { _insElementosFemenino = value; }
        }

        public string insColindancias
        {
            get { return _insColindancias; }
            set { _insColindancias = value; }
        }

        public string insFunciones
        {
            get { return _insFunciones; }
            set { _insFunciones = value; }
        }

        public string insDescripcion
        {
            get { return _insDescripcion; }
            set { _insDescripcion = value; }
        }

        public int IdServicioAntes
        {
            get { return _idServicioAntes; }
            set { _idServicioAntes = value; }
        }

                public int operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
        }



                public int idInstalacionEquipo
        {
            get { return _idInstalacionEquipo; }
            set { _idInstalacionEquipo = value; }
        }

                public int idTipoInstalacion
                {
                    get { return _idTipoInstalacion; }
                    set { _idTipoInstalacion = value; }
                }

                public clsEntServicio Servicio
                {
                    get { return _servicio; }
                    set { _servicio = value; }
                }

                public clsEntZona Zona
                {
                    get { return _zona; }
                    set { _zona = value; }
                }
        //public clsEntZonaHoraria ZonaHoraria
        //{
        //    get { return _ZonaHoraria; }
        //    set { _ZonaHoraria = value; }
        //}

    }
}