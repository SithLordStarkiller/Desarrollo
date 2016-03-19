namespace SunCorp.BusinessLogic.EntitiesBussinessLogic
{
    using Entities.Entities;
    using DataAccessSqlServer.EntitiesDataAccesss;

    public class UsUsuarioBussiness
    {
        public UsUsuarios GetUsUsuarios(string user, string password)
        {
            return new UsUsuariosAccess().GetUsUsuario(user, password);
        }
    }
}
