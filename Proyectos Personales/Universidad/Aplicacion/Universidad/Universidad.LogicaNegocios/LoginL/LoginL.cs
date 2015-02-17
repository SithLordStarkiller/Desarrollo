
using Universidad.AccesoDatos.ControlUsuarios.LoginA;
using Universidad.Entidades;

namespace Universidad.LogicaNegocios.LoginL
{
    public class LoginL
    {
        #region Login Administrativos

        public US_USUARIOS LoginAdminitradorUsuarios(string usuario, string contrasena)
        {
            //var login = LoginA.ClassInstance.LoginAdministrador(usuario, contrasena);
            var login = LoginA.ClassInstance.LoginAdministradorLinq(usuario, contrasena);
            return login;
        }

        public PER_PERSONAS ObtenPersona(US_USUARIOS usuario)
        {
            return LoginA.ClassInstance.ObtenPersonaLinq(usuario);
        }

        #endregion
    }
}
