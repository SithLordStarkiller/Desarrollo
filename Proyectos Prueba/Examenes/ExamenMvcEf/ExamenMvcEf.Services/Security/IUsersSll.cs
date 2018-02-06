namespace ExamenMvcEf.Services.Security
{
    using Models;

    using System.ServiceModel;    

    [ServiceContract]
    public interface IUsersSll
    {
        [OperationContract]
        Users AddUser(Users user);

        [OperationContract]
        int GetCountUsers();

        [OperationContract]
        Users UpdateUser(Users user);
    }
}
