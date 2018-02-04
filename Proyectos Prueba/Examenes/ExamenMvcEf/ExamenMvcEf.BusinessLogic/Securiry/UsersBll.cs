using ExamenMvcEf.Models;
using ExamenMvcEf.DataAccess.Security;
namespace ExamenMvcEf.BusinessLogic.Securiry
{
    public class UsersBll
    {
        public Users AddUser(Users user)
        {
            return new UsersDal().Add(user);
        }
    }
}
