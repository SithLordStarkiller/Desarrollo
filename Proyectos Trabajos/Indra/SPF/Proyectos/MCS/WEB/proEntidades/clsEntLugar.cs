using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntLugar
    {
        private int _idLugar;
        private string _lugDescripcion;
        private Boolean _lugVigente;



        public int IdLugar
        {
            get { return _idLugar; }
            set { _idLugar = value; }
        }

        public string LugDescripcion
        {
            get { return _lugDescripcion; }
            set { _lugDescripcion = value; }
        }

        public Boolean LugVigente
        {
            get { return _lugVigente; }
            set { _lugVigente = value; }
        }


    }
}
