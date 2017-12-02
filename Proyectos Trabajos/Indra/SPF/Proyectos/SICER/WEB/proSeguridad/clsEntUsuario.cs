using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proSeguridad
{
    public class clsEntUsuario
    {
        public short IdUsuario
        {
            get;
            set;
        }

        public clsEntPerfil Perfil
        {
            get;
            set;
        }

        public string UsuLogin
        {
            get;
            set;
        }

        public string UsuPaterno
        {
            get;
            set;
        }

        public string UsuMaterno
        {
            get;
            set;
        }

        public string UsuNombre
        {
            get;
            set;
        }

        public string UsuContrasenia
        {
            get;
            set;
        }

        public string UsuSexo
        {
            get;
            set;
        }

        public short Administrador
        {
            get;
            set;
        }

        public Guid IdEmpleado
        {
            get;
            set;
        }

        public string UsuConfirmacion
        {
            get;
            set;
        }
    }
}
