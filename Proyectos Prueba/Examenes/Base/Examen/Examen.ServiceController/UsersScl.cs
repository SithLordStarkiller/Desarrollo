namespace Examen.ServiceController
{
    using Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserScl
    {
        private readonly UsersServiceClient.UsersServiceClient _servicio;

        public UserScl()
        {
            _servicio = new UsersServiceClient.UsersServiceClient("BasicHttpBinding_IUsersService");
        }

        public Task<UsUsuario> GetUser()
        {
            return Task.Run(() => _servicio.GetUser("ecruzlagunes","1234"));
        }
    }
}
