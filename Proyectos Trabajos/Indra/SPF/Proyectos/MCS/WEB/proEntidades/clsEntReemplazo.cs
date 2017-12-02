using System;
using System.Collections.Generic;
using System.Text;
using SICOGUA.Entidades;

namespace SICOGUA.Entidades
{
    public class clsEntReemplazo
    {
        private clsEntEmpleado _integranteReemplazar;
        private clsEntEmpleado _integranteReemplazo;

        public clsEntEmpleado integranteReemplazar
        {
            get { return _integranteReemplazar; }
            set { _integranteReemplazar = value; }
        }

        public clsEntEmpleado integranteReemplazo
        {
            get { return _integranteReemplazo; }
            set { _integranteReemplazo = value; }
        }
    }
    public class clsEntReemplazoTabla
    {
        private Guid _idEmpleado;
        private string _empPaterno;
        private string _empMaterno;
        private string _empNombre;
        private Guid _idEmpleadoReemplazo;
        private string _empPaternoReemplazo;
        private string _empMaternoReemplazo;
        private string _empNombreReemplazo;

        // datos de la asignacion actual
        private string _serDescripcion;
        private string _insNombre;
        private int _idServicio;
        private int _idInstalacion;

        private int _intContador;
        public Guid idEmpleado
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

        public string serDescripcion
        {
            get { return _serDescripcion; }
            set { _serDescripcion = value; }
        }
        public string insNombre
        {
            get { return _insNombre; }
            set { _insNombre = value; }
        }
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
        public int intContador
        {
            get { return _intContador; }
            set { _intContador = value; }
        }
        public Guid idEmpleadoReemplazo
        {
            get { return _idEmpleadoReemplazo; }
            set { _idEmpleadoReemplazo = value; }
        }
        public string empPaternoReemplazo
        {
            get { return _empPaternoReemplazo; }
            set { _empPaternoReemplazo = value; }
        }
        public string empMaternoReemplazo
        {
            get { return _empMaternoReemplazo; }
            set { _empMaternoReemplazo = value; }
        }
        public string empNombreReemplazo
        {
            get { return _empNombreReemplazo; }
            set { _empNombreReemplazo = value; }
        }
    }
}
