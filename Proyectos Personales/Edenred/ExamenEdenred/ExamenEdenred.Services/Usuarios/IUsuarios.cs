namespace ExamenEdenred.Services.Usuarios
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using Entities.Entities;
    
    [ServiceContract]
    public interface IUsuarios
    {
        [OperationContract]
        UsUsuarios ExisteUsuario(int idUsuario);

        [OperationContract]
        bool GuardaArchivo(string texto);

        [OperationContract]
        List<UsCatTipoUsuarios> ObtenCatTipoUsuarios();
    }
}
