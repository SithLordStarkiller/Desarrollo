using System.ServiceModel;
using Universidad.Entidades;

namespace Universidad.ServidorInterno.Login_S
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IS_Login" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IS_Login
    {
        [OperationContract]
        US_USUARIOS LoginAdministrador(string Usuario, string Contrasena);

        [OperationContract]
        PER_PERSONAS ObtenPersona(US_USUARIOS usuario);

        [OperationContract]
        bool Funciona();

        [OperationContract]
        PER_PERSONAS ObtenPersonas(US_USUARIOS usuario);
    }
}
