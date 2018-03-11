namespace ExamenMvcEf.DataAccess.UnitOfWork
{
    using Helpers;
    using Security;

    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly Logger _logLogger = new Logger();

        public readonly ExamenMvcEfEntities Context;

        public UnitOfWork(ExamenMvcEfEntities dbContext)
        {
            Context = dbContext;
            
            Users = new RepositoryUsers(Context);
        }

        public IRepositoryUsers Users { get; set; }

        public int Commit()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al completar transaccion en entityFramework", "", ex, Context.ToString()));
                throw;
            }
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al completar transaccion asyncrona en entityFramework", "", ex, Context.ToString()));
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                Context.Dispose();
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al descartar cambios en entityFramework", "", ex, Context.ToString()));
                throw;
            }
        }
    }
}
