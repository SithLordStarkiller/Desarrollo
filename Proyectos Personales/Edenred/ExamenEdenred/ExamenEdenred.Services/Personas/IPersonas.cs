namespace ExamenEdenred.Services.Personas
{
    using System.ServiceModel;

    [ServiceContract]
    public interface IPersonas
    {
        [OperationContract]
        bool EliminaPersona(int idPersona);
    }
}
