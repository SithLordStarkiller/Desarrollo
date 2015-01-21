using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Universidad.Entidades;
using Universidad.LogicaNegocios.LoginL;
using Newtonsoft.Json;

namespace Universidad.ServidorInterno.Login_S
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "S_Login" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione S_Login.svc o S_Login.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class S_Login : IS_Login
    {
        public string LoginAdministrador(string usuario, string contrasena)
        {
            var login = new LoginL().LoginAdminitradorUsuarios(usuario, contrasena);
            return JsonConvert.SerializeObject(login);
        }

        public string ObtenPersona(US_USUARIOS usuario)
        {
            var persona = new LoginL().ObtenPersona(usuario);
            return JsonConvert.SerializeObject(persona);
        }

        public bool Funciona()
        {
            return true;
        }
    }
}
