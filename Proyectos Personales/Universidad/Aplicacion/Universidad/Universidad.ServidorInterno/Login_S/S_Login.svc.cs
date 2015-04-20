using Universidad.Entidades;
using Universidad.LogicaNegocios.LoginL;

namespace Universidad.ServidorInterno.Login_S
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "S_Login" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione S_Login.svc o S_Login.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class S_Login : IS_Login
    {
        public US_USUARIOS LoginAdministrador(string usuario, string contrasena)
        {
            return new LoginL().LoginAdminitradorUsuarios(usuario, contrasena);
        }

        public PER_PERSONAS ObtenPersona(US_USUARIOS usuario)
        {
            return new LoginL().ObtenPersona(usuario);
        }

        public PER_PERSONAS ObtenPersonas(US_USUARIOS usuario)
        {
            return new LoginL().ObtenPersona(usuario);
        }

        public bool Funciona()
        {
            return true;
        }
    }
}
