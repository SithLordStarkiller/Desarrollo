namespace ExamenMvcEf.Services.Security
{
    using Models;
    using BusinessLogic.Securiry;

    public class UsersSll : IUsersSll
    {
        public Users AddUser(Users user)
        {
            return new UsersBll().AddUser(user);
        }
    }
}
