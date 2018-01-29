namespace ExamenMvcEf.DataAccess.Security
{
    using Models;

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
    }
}