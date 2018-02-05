namespace ExamenMvcEf.DataAccess.UnitOfWork
{
    using Security;

    using System;

    public interface IUnitOfWork : IDisposable
    {
        IRepositoryUsers Users { get; }

        int Commit();
    }
}
