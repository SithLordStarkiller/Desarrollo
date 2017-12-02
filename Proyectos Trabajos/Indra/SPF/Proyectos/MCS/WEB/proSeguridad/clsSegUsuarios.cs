using System.Collections;

namespace SICOGUA.Seguridad
{
    public class clsSegUsuarios
    {
        private readonly Hashtable _htUsuarios = new Hashtable();

        private clsSegUsuarios()
        {
            _htUsuarios.Add("jesus", "123456");
            _htUsuarios.Add("alma", "123456");
            _htUsuarios.Add("sandy", "123456");
            _htUsuarios.Add("jorge", "123456");
            _htUsuarios.Add("prueba", "ssycse");
            _htUsuarios.Add("lupita", "123456");
        }

        public static bool leerUsuario(string usuario, string contrasenia)
        {
            clsSegUsuarios objUsuario = new clsSegUsuarios();

            if (objUsuario._htUsuarios.ContainsKey(usuario))
            {
                if (objUsuario._htUsuarios[usuario].ToString() == contrasenia)
                {
                    return true;
                }
            }

            return false;
        }
    }
}