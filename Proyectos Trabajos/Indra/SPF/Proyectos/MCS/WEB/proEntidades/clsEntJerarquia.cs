using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntJerarquia
    {
        private byte _idJerarquia;
        private string _jerDescripcion;
        private decimal _jerSueldo;
        private Boolean _jerVigente;


        public byte IdJerarquia
        {
            get { return _idJerarquia; }
            set { _idJerarquia = value; }
        }

        public string JerDescripcion
        {
            get { return _jerDescripcion; }
            set { _jerDescripcion = value; }
        }

        public decimal JerSueldo
        {
            get { return _jerSueldo; }
            set { _jerSueldo = value; }
        }

        public Boolean JerVigente
        {
            get { return _jerVigente; }
            set { _jerVigente = value; }
        }
    }
}
