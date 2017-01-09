namespace Broxel.Services.Usuarios
{
    using Entities.Entidades;
    using Entities;
    using BusinessLogic.Usuarios;
    using System.Collections.Generic;

    public class UsuariosServer : IUsuariosWcf
    {
        public UsUsuariosEntity ObtenUsUsuarionPorLogin(string usuario, string contrasena)
        {
            return new BlUsUsuario().ObtenUsUsuarion(usuario,contrasena);
        }

        public List<USUSUARIOS> InsertaListaUsuarios(List<USUSUARIOS> listaUsuarios)
        {
            return new BlUsUsuario().InsertaListaUsuarios(listaUsuarios);
        }
    }
}
