namespace GOB.SPF.ConecII.AccessData
{
    using System;
    using System.Data;

    public class UnitOfWorkCatalog : IUnitOfWork
    {
        public UnitOfWorkCatalog(IDbConnection connection, bool hasConnection)
        {
            _connection = connection;
            _hasConnection = hasConnection;
            _transaction = connection.BeginTransaction();
        }

        protected bool _hasConnection { get; set; }

        protected IDbTransaction _transaction { get; set; }

        protected IDbConnection _connection { get; set; }        

        public IDbCommand CreateCommand()
        {
            var command = _connection.CreateCommand();
            command.Transaction = _transaction;
            return command;
        }

        public void SaveChanges()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException(
                    "No existe una transaccion");
            }

            _transaction.Commit();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }

            if (_connection != null && _hasConnection)
            {
                _connection.Close();
                _connection = null;
            }
        }
    }
}
