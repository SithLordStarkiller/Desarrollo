using System;

namespace SICOGUA.Entidades
{
    public class clsEntPuesto
    {
        private int _idPuesto;
        private clsEntJerarquia _jerarquia;
        private clsEntNivel _nivel;
        private string _pueDescripcion;
        private decimal _pueSueldo;
        private Boolean _pueVigente;

        public int IdPuesto
        {
            get { return _idPuesto; }
            set { _idPuesto = value; }
        }

        public string PueDescripcion
        {
            get { return _pueDescripcion; }
            set { _pueDescripcion = value; }
        }

        public decimal PueSueldo
        {
            get { return _pueSueldo; }
            set { _pueSueldo = value; }
        }

        public Boolean PueVigente
        {
            get { return _pueVigente; }
            set { _pueVigente = value; }
        }

        public clsEntJerarquia Jerarquia
        {
            get { return _jerarquia; }
            set { _jerarquia = value; }
        }

        public clsEntNivel Nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }
    }
}
