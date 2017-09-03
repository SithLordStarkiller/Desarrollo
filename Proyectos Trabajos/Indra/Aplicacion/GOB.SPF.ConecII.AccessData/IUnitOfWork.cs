namespace GOB.SPF.ConecII.AccessData
{
    using System;
    using System.Data;

    public interface IUnitOfWork: IDisposable
    {
        IDbCommand CreateCommand();
        void SaveChanges();
    }
}
