namespace Broxel.Services.Usuarios
{
    using System.ServiceModel;
    using System.Collections.Generic;
    using Entities;

    [ServiceContract]
    public interface IUsuariosWcf
    {
        [OperationContract]
        USUSUARIOS ObtenUsUsuarionPorLogin(string usuario, string contrasena);

        [OperationContract]
        List<USUSUARIOS> InsertaListaUsuarios(List<USUSUARIOS> listaUsuarios);
    }
}
