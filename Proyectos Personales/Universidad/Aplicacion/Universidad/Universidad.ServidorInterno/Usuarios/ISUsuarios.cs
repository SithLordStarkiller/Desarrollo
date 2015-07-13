namespace Universidad.ServidorInterno.Usuarios
{
    using System.ServiceModel;
    using Entidades;

    
    [ServiceContract]
    public interface ISUsuarios
    {
        [OperationContract]
        US_USUARIOS ObtenUsuario(string usuario);
    }
}
