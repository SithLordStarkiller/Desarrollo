namespace Examen.DataAccess
{
    using Models;

    using System.Threading.Tasks;

    public class UsersEntities
    {
        private readonly ExamenEntities _contexto = new ExamenEntities();

        #region Usuario

        public async Task<UsUsuario> GetUser(string user, string password)
        {
            UsUsuario userResult = new UsUsuario();

            using (var aux = new Repositorio<UsUsuario>())
            {
                userResult = await aux.FindBy(x => x.Usuario == user && x.Contrasena == password);
            }

            return userResult;
        }

        #endregion
    }
}
