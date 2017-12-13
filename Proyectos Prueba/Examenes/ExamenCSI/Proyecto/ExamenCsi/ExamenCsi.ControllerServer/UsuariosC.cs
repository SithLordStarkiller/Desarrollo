using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenCsi.ControllerServer.UsuariosServer;
using ExamenCsi.Entities;

namespace ExamenCsi.ControllerServer
{
    public class UsuariosC
    {
        private readonly UsuariosServer.UsuariosClient _servicio;

        public UsuariosC()
        {
            //var configServicios = new UsuariosClient();
            _servicio = new UsuariosClient("BasicHttpBinding_IUsuarios");
        }

        public Task<int> InsertaUsuario(UsUsuario usuario)
        {
            return Task.Run(() => _servicio.InsertarUsuario(usuario));
        }
    }
}
