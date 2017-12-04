using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.AccessData
{
    internal class UnitOfWorkSecurity : IUnitOfWork
    {
        protected bool IsTransaction { get; }
        protected bool HasConnection { get; set; }
        protected IDbTransaction Transaction { get; set; }
        protected IDbConnection Connection { get; set; }

        public UnitOfWorkSecurity(IDbConnection connection, bool isTransaction)
        {
            IsTransaction = isTransaction;
            Connection = connection;
            if (isTransaction)
                Transaction = connection.BeginTransaction();
        }

        public IDbCommand CreateCommand()
        {
            var command = (IDbCommand)new SqlCommand();
            command.Connection = Connection;
            if (IsTransaction)
                command.Transaction = Transaction;
            return command;
        }

        public void SaveChanges()
        {
            if (IsTransaction && Transaction == null)
                throw new InvalidOperationException(
                    "No existe una transaccion");

            if (IsTransaction)
                Transaction.Commit();

            Transaction = null;
        }

        public void Dispose()
        {
            if (IsTransaction)
                Transaction?.Rollback();

            Transaction = null;
            Connection.Dispose();
        }
    }
}
