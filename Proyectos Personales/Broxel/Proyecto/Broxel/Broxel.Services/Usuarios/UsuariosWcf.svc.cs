using Newtonsoft.Json;

namespace Broxel.Services.Usuarios
{
    using Entities;
    using BusinessLogic.Usuarios;
    using System.Collections.Generic;

    public class UsuariosServer : IUsuariosWcf
    {
        public USUSUARIOS ObtenUsUsuarionPorLogin(string usuario, string contrasena)
        {
            var a =  new BlUsUsuario().ObtenUsUsuarionPorLogin(usuario,contrasena);
            var serial = JsonConvert.SerializeObject(a);

            return a;
        }

        public List<USUSUARIOS> InsertaListaUsuarios(List<USUSUARIOS> listaUsuarios)
        {
            return new BlUsUsuario().InsertaListaUsuarios(listaUsuarios);
        }
    }
}
