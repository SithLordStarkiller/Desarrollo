using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proSeguridad
{
    public class clsEntSesion
    {
        public enum TipoEstatus
        {
            Activa = 1,
            Finalizada = 2
        }
        public clsEntUsuario Usuario
        {
            get;
            set;
        }
        public string SessionId
        {
            get;
            set;
        }
        public string Ip
        {
            get;
            set;
        }
        public int Intentos
        {
            get;
            set;
        }
        public DateTime Inicio
        {
            get;
            set;
        }
        public TipoEstatus Estatus
        {
            get;
            set;
        }
    }
}
