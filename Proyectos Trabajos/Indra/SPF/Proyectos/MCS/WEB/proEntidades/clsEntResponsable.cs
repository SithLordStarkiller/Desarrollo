using System;
using System.Collections.Generic;

namespace SICOGUA.Entidades
{
    public class clsEntResponsable
    {
        private Guid        _idEmpleado;
        private string      _riNombre;
        private string      _riObservaciones;
        private bool        _riVigente;

        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public string riNombre
        {
            get { return _riNombre; }
            set { _riNombre = value; }
        }

        public string riObservaciones
        {
            get { return _riObservaciones; }
            set { _riObservaciones = value; }
        }


        public bool riVigente
        {
            get { return _riVigente; }
            set { _riVigente = value; }
        }
    }
}