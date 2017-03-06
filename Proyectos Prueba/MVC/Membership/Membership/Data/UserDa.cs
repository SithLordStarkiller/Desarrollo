namespace Membership
{
    using System;
    using System.Threading.Tasks;

    public class UserDa
    {
        public async Task<USERS> GetUserByLogin(string user, string password)
        {
            try
            {
                using (var proceso = new Repositorio<USERS>())
                {
                    var resultado = await proceso.Consulta(x => x.USERNAME == user && x.PASSWORD == password);
                    return resultado;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}