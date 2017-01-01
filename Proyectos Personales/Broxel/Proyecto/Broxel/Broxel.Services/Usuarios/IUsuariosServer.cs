namespace Broxel.Services.Usuarios
{
    using System.ServiceModel;
    using Entities.Entidades;

    [ServiceContract]
    public interface IUsuariosServer
    {
        [OperationContract]
        UsUsuarios ObtenUsUsuarionPorLogin(string usuario, string contrasena);
    }
}
