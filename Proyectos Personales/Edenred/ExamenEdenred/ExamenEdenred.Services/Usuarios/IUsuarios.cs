namespace ExamenEdenred.Services.Usuarios
{
    using System.ServiceModel;
    using Entities.Entities;
    
    [ServiceContract]
    public interface IUsuarios
    {
        [OperationContract]
        UsUsuarios ExisteUsuario(int idUsuario);
    }
}
