namespace DalUnitOfWork.DataAccess
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IRepositoryDimEmployee Employees { get; }

        int Complete();
    }
}
