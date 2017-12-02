using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntFirmanteOficioAsignacion
    {

        private Guid _idEmpleado;
        private string _empPaterno;
        private string _empMaterno;
        private string _empNombre;
        private string _empNombreCompleto;
        private int _idJerarquia;
        private string _jerDescripcion;
        private string _puestoDescripcion;
        private string _citaAusencia;


        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }


        public string empPaterno
        {
            get { return _empPaterno; }
            set { _empPaterno = value; }
        }

        public string empMaterno
        {
            get { return _empMaterno; }
            set { _empMaterno = value; }
        }

        public string empNombre
        {
            get { return _empNombre; }
            set { _empNombre = value; }
        }

        public string empNombreCompleto
        {
            get { return _empNombreCompleto; }
            set { _empNombreCompleto = value; }
        }


        public int idJerarquia
        {
            get { return _idJerarquia; }
            set { _idJerarquia = value; }
        }

        public string jerDescripcion
        {
            get { return _jerDescripcion; }
            set { _jerDescripcion = value; }
        }

        public string puestoDescripcion
        {
            get { return _puestoDescripcion; }
            set { _puestoDescripcion = value; }
        }

        public string citaAusencia
        {
            get { return _citaAusencia; }
            set { _citaAusencia = value; }
        }
    }
}
