namespace ExamenMvcEf.DataAccess.Security
{
    using Models;

    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;


    public class UsersDal : GenericRepository<Users>
    {

        public UsersDal(DbContext context) : base(context)
        {
        }

        public override Users Get(int id)
        {
            var query = GetAll().FirstOrDefault(b => b.IdUser == id);
            return query;
        }

        public async Task<Users> GetSingleAsyn(int id)
        {
            return await _context.Set<Users>().FindAsync(id);
        }

        public Users AddUser(Users user)
        {
            return base.Add(user);
        }

        public int AddUsers(List<Users> userList)
        {
            var result = _context.Set<Users>().AddRange(userList);
            return 1;
        }
    }
}