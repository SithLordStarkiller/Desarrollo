namespace Suncorp.Services.ServiciosWcf
{
    using System.ServiceModel;
    using Models;

    [ServiceContract]
    public interface IUsuariosWcf
    {

        [OperationContract]
        UsUsuarios ObtenerUsuarioLogin(string usuario, string contrasena);
    }
}
