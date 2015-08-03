namespace Universidad.ServidorInterno.Usuarios
{
    using System.ServiceModel;
    using Entidades;

    
    [ServiceContract]
    public interface ISUsuarios
    {
        [OperationContract]
        US_USUARIOS ObtenUsuario(string usuario);

        [OperationContract]
        US_USUARIOS ObtenUsuarioPorId(int idUsuario);

        [OperationContract]
        US_USUARIOS CrearCuantaUsuario(US_USUARIOS usuario, string personaId);

        [OperationContract]
        US_USUARIOS ActualizaCuentaUsuario(US_USUARIOS usuario);

    }
}
