namespace Suncorp.Services.ServiciosWcf
{
    using System.ServiceModel;


    [ServiceContract]
    public interface IInventario
    {
        [OperationContract]
        void DoWork();
    }
}
