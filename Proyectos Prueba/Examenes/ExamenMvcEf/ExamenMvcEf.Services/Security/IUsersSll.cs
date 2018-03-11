namespace ExamenMvcEf.Services.Security
{
    using Models;

    using System.ServiceModel;
    using System.Collections.Generic;

    [ServiceContract]
    public interface IUsersSll
    {
        [OperationContract]
        List<Users> GetAllUsers();

        [OperationContract]
        Users GetUsersById(int id);

        [OperationContract]
        Users AddUser(Users user);

        [OperationContract]
        int GetCountUsers();

        [OperationContract]
        Users UpdateUser(Users user);
    }
}
