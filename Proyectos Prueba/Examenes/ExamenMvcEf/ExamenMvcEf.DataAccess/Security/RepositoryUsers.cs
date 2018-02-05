namespace ExamenMvcEf.DataAccess.Security
{
    using Models;

    public class RepositoryUsers : GenericRepository<Users>, IRepositoryUsers
    {
        public ExamenMvcEfEntities UserContext
        {
            get { return Context as ExamenMvcEfEntities; }
        }

        public RepositoryUsers(ExamenMvcEfEntities context) : base(context)
        {
        }
    }
}
