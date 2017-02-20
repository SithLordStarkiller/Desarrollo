namespace Broxel.BusinessLogic.Usuarios
{
    using DataAccess.Usuarios;
    using Entities;
    using System.Collections.Generic;


    #region UsUsuario

    public class BlUsUsuario
    {
        public USUSUARIOS ObtenUsUsuarionPorLogin(string usuario, string contrasena)
        {
            return new DaUsUsuarios().ObtenUsUsuarionPorLoginAdo(usuario, contrasena);
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
