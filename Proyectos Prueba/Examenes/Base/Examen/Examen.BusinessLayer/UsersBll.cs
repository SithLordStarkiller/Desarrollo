namespace Examen.BusinessLayer
{
    using Models;
    using DataAccess;

    public class UsersBll
    {
        public UsUsuario GetUser(string user, string password)
        {
            return new UsersEntities().GetUser(user, password).Result;
        }
    }
}
