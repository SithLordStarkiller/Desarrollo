namespace Broxel.BusinessLogic.Usuarios
{
    using DataAccess.Usuarios;
    using Entities;
    using Entities.Entidades;
    using System.Collections.Generic;

    #region UsUsuario

    public class BlUsUsuario
    {
        public UsUsuariosEntity ObtenUsUsuarion(string usuario, string contrasena)
        {
            return new DaUsUsuarios().ObtenUsUsuarionPorLogin(usuario, contrasena);
        }

        public USUSUARIOS InsertaUsuario(USUSUARIOS usuario)
        {
            return new DaUsUsuarios().InsertaUsuarioLinQ(usuario).Result;
        }

        public List<USUSUARIOS> InsertaListaUsuarios(List<USUSUARIOS> listaUsuarios)
        {
            return new DaUsUsuarios().InsertaListaUsuariosLinQ(listaUsuarios);
        }
    }

    #endregion
}
