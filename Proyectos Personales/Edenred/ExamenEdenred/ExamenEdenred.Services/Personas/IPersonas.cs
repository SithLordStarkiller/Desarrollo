using System.ServiceModel;

namespace ExamenEdenred.Services.Personas
{
    [ServiceContract]
    public interface IPersonas
    {
        [OperationContract]
        bool EliminaPersona(int idPersona);
    }
}
