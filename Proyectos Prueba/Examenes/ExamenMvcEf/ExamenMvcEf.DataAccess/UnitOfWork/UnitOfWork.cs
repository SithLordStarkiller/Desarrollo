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

        public readonly ExamenMvcEfEntities _context;

        public UnitOfWork(ExamenMvcEfEntities dbContext)
        {
            _context = dbContext;

            Users = new RepositoryUsers(_context);
        }

        public IRepositoryUsers Users { get; private set; }

        public int Commit()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al completar transaccion en entityFramework", "", ex, _context.ToString()));
                throw ex;
            }
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al completar transaccion asyncrona en entityFramework", "", ex, _context.ToString()));
                throw ex;
            }
        }

        public void Dispose()
        {
            try
            {
                _context.Dispose();
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al descartar cambios en entityFramework", "", ex, _context.ToString()));
                throw ex;
            }
        }
    }
}
