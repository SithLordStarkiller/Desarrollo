namespace Broxel.Services.Usuarios
{
    using System.ServiceModel;
    using System.Collections.Generic;
    using Entities.Entidades;
    using Entities;

    [ServiceContract]
    public interface IUsuariosWcf
    {
        [OperationContract]
        UsUsuariosEntity ObtenUsUsuarionPorLogin(string usuario, string contrasena);

        [OperationContract]
        List<USUSUARIOS> InsertaListaUsuarios(List<USUSUARIOS> listaUsuarios);
    }
}
