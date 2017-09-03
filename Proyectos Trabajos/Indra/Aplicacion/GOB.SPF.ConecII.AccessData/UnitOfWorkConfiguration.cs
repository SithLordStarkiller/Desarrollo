namespace GOB.SPF.ConecII.AccessData
{
    using System;
    using System.Data;

    public class UnitOfWorkConfiguration : IUnitOfWork
    {
        public UnitOfWorkConfiguration(IDbConnection connection, bool hasConnection)
        {
            Connection = connection;
            HasConnection = hasConnection;
            Transaction = connection.BeginTransaction();
        }

        protected bool HasConnection { get; set; }

        protected IDbTransaction Transaction { get; set; }

        protected IDbConnection Connection { get; set; }

        public IDbCommand CreateCommand()
        {
            var command = Connection.CreateCommand();
            command.Transaction = Transaction;
            return command;
        }

        public void SaveChanges()
        {
            if (Transaction == null)
            {
                throw new InvalidOperationException(
                    "No existe una transaccion");
            }

            Transaction.Commit();
            Transaction = null;
        }

        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
                Transaction = null;
            }

            if (Connection == null || !HasConnection)
                return;

            Connection.Close();
            Connection = null;
        }
    }
}
