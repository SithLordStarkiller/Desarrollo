namespace ExamenMvcEf.ServiceRestful.Security
{
    using Models;
    using BusinessLogic.Securiry;
    using System.Collections.Generic;

    public class UsersSll : IUsersSll
    {
        public List<Users> GetAllUsers()
        {
            return new UsersBll().GetAllUsers();
        }

        public Users GetUsersById(int id)
        {
            return new UsersBll().GetUsersById(id);
        }
        public Users AddUser(Users user)
        {
            return new UsersBll().AddUser(user);
        }

        public int GetCountUsers()
        {
            return new UsersBll().GetCountUsers();
        }

        public Users UpdateUser(Users user)
        {
            return new UsersBll().UpdateUser(user);
        }
    }
}
