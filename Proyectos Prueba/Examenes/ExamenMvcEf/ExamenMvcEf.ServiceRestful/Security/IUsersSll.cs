namespace ExamenMvcEf.ServiceRestful.Security
{
    using Models;

    using System.ServiceModel;
    using System.ServiceModel.Web;
    using System.Collections.Generic;

    [ServiceContract]
    public interface IUsersSll
    {
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Users> GetAllUsers();

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Users GetUsersById(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Users AddUser(Users user);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        int GetCountUsers();

        [OperationContract]
        [WebInvoke(Method = "UPDATE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Users UpdateUser(Users user);
    }
}
