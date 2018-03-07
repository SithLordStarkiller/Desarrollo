namespace Examen.Services
{
    using Models;

    using System.ServiceModel;

    [ServiceContract]
    public interface IUsersService
    {
        [OperationContract]
        UsUsuario GetUser(string user, string password);
    }
}
