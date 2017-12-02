using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntPeloton
    {
        private short _idZona;
        private short _idAgrupamiento;
        private short _idCompania;
        private short _idSeccion;
        private short _idPeloton;
        private string _pelDescripcion;
        private Guid _idEmpleado;
        private Boolean _pelVigente;


        public short IdZona
        {
            get { return _idZona; }
            set { _idZona = value; }
        }

        public short IdAgrupamiento
        {
            get { return _idAgrupamiento; }
            set { _idAgrupamiento = value; }
        }

        public short IdCompania
        {
            get { return _idCompania; }
            set { _idCompania = value; }
        }

        public short IdSeccion
        {
            get { return _idSeccion; }
            set { _idSeccion = value; }
        }

        public short IdPeloton
        {
            get { return _idPeloton; }
            set { _idPeloton = value; }
        }

        public string PelDescripcion
        {
            get { return _pelDescripcion; }
            set { _pelDescripcion = value; }
        }

        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public Boolean PelVigente
        {
            get { return _pelVigente; }
            set { _pelVigente = value; }
        }


    }
}
