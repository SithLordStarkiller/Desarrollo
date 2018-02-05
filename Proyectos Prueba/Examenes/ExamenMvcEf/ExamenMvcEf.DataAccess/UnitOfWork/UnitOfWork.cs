namespace ExamenMvcEf.DataAccess.UnitOfWork
{
    using Security;

    public class UnitOfWork : IUnitOfWork
    {
        public readonly ExamenMvcEfEntities _context;

        public UnitOfWork(ExamenMvcEfEntities dbContext)
        {
            _context = dbContext;

            Users = new RepositoryUsers(_context);
        }

        public IRepositoryUsers Users { get; private set; }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
