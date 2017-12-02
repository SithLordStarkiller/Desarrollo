using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntDireccionGeneral
    {
        private byte _idDireccionGeneral;
        private string _dgDescripcion;
        private Boolean _dgVigente;


        public byte IdDireccionGeneral
        {
            get { return _idDireccionGeneral; }
            set { _idDireccionGeneral = value; }
        }

        public string DgDescripcion
        {
            get { return _dgDescripcion; }
            set { _dgDescripcion = value; }
        }

        public Boolean DgVigente
        {
            get { return _dgVigente; }
            set { _dgVigente = value; }
        }
        
    }
}
