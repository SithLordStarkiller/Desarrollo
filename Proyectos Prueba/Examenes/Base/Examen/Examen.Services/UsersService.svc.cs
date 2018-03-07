namespace Examen.Services
{
    using Models;
    using BusinessLayer;

    public class UsersService : IUsersService
    {
        public UsUsuario GetUser(string user, string password)
        {
            return new UsersBll().GetUser(user, password);
        }
    }
}
