using System;

namespace SICOGUA.Seguridad
{
    public class clsEntUsuario
    {
        private int _idUsuario;
        private clsEntPerfil _perfil;
        private string _usuLogin;
        private string _usuContrasenia;
        private string _usuPaterno;
        private string _usuMaterno;
        private string _usuNombre;
        private string _usuSexo;
        private Int16 _administrador;
        private Guid _idEmpleado;
        private string _usuConfirmacion;

        public int IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }

        public clsEntPerfil Perfil
        {
            get { return _perfil; }
            set { _perfil = value; }
        }

        public string UsuLogin
        {
            get { return _usuLogin; }
            set { _usuLogin = value; }
        }

        public string UsuPaterno
        {
            get { return _usuPaterno; }
            set { _usuPaterno = value; }
        }

        public string UsuMaterno
        {
            get { return _usuMaterno; }
            set { _usuMaterno = value; }
        }

        public string UsuNombre
        {
            get { return _usuNombre; }
            set { _usuNombre = value; }
        }

        public string UsuContrasenia
        {
            get { return _usuContrasenia; }
            set { _usuContrasenia = value; }
        }

        public string UsuSexo
        {
            get { return _usuSexo; }
            set { _usuSexo = value; }
        }

        public short Administrador
        {
            get { return _administrador; }
            set { _administrador = value; }
        }

        public Guid IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public string UsuConfirmacion
        {
            get { return _usuConfirmacion; }
            set { _usuConfirmacion = value; }
        }
    }
}
