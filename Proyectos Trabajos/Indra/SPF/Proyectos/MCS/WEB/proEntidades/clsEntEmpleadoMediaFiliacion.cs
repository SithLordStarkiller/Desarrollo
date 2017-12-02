using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntEmpleadoMediaFiliacion
    {
        private Guid _idEmpleado;
        private byte _idTipoCaracteristica;
        private byte _idMediaFiliacion;


        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public byte IdMediaFiliacion
        {
            get { return _idMediaFiliacion; }
            set { _idMediaFiliacion = value; }
        }

        public byte IdTipoCaracteristica
        {
            get { return _idTipoCaracteristica; }
            set { _idTipoCaracteristica = value; }
        }

        
    }
}
