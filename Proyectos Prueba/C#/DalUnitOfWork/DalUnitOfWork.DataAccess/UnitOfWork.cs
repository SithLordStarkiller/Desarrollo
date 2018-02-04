namespace DalUnitOfWork.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly AdventureworksEntities _context;

        public UnitOfWork(AdventureworksEntities dbContext)
        {
            _context = dbContext;

            Employees = new RepositoryDimEmployee(_context);
        }

        public IRepositoryDimEmployee Employees { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
