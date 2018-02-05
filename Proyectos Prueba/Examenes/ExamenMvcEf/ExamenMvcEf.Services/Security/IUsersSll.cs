namespace ExamenMvcEf.Services.Security
{
    using Models;

    using System.ServiceModel;    

    [ServiceContract]
    public interface IUsersSll
    {
        Users AddUser(Users user);
    }
}
