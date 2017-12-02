using System;

namespace SICOGUA.Entidades
{
    public class clsEntGuardarMasiva
    {
 
        private string _servicio;
        private string _instalacion;
        private string _cantidad;




        public string servicio
        {
            get { return _servicio; }
            set { _servicio = value; }
        }

        public string instalacion
        {
            get { return _instalacion; }
            set { _instalacion = value; }
        }

        public string cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

   

    }
}
