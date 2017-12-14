using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ExamenCsi.BusinessLogic;
using ExamenCsi.Entities;

namespace ExamenCsi.Server
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Usuarios" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Usuarios.svc o Usuarios.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Usuarios : IUsuarios
    {
        public int InsertarUsuario(UsUsuario usuario)
        {
            return new UsuariosB().InsertarUsuario(usuario);
        }

        public List<UsUsuario> UsuariosObtenerTodos()
        {
            return new UsuariosB().UsuariosObtenerTodos();
        }
    }
}
