namespace SunCorp.BusinessLogic.EntitiesBussinessLogic
{
    using Entities.Entities;
    using DataAccessSqlServer.EntitiesDataAccesss;

    public class UsUsuarioBussiness
    {
        public UsUsuarios GetUsUsuarios(string user)
        {
            return new UsUsuariosAccess().GetUsUsuario(user);
        }
    }
}
