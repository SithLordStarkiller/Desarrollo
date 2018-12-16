namespace ExamenMvcEf.DataAccess
{
    using Helpers;

    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Data.Entity;
    using System.Threading.Tasks;

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly Logger _logLogger = new Logger();

        protected readonly DbContext Context;

        protected GenericRepository(DbContext context)
        {
            context.Configuration.ProxyCreationEnabled = false;
            Context = context;
        }

        public TEntity Add(TEntity t)
        {
            try
            {
                return Context.Set<TEntity>().Add(t);
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al insertar en entityFramework", "", ex, Context.ToString()));
                throw;
            }
        }

        public List<TEntity> AddRange(List<TEntity> list)
        {
            try
            {
                return Context.Set<TEntity>().AddRange(list).ToList();
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al insertar lista de objetos en entityFramework", list.ToString(), ex, Context.ToString()));
                throw;
            }
        }

        public int Count()
        {
            try
            {
                return Context.Set<TEntity>().Count();
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, $"Error al realizar conteoen la tabla en entityFramework", GetType().ToString(), ex, Context.ToString()));
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                return await Context.Set<TEntity>().CountAsync();
            }
            catch (Exception ex)
            {
                await Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al realizar conteo de registros asincrona en entityFramework", GetType().ToString(), ex, Context.ToString()));
                throw;
            }
        }

        public TEntity Remove(TEntity entity)
        {
            try
            {
                return Context.Set<TEntity>().Remove(entity);
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al remover objeto de la base de datos en entityFramework", entity.GetType().ToString(), ex, Context.ToString()));
                throw;
            }
        }

        public TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            try
            {
                return Context.Set<TEntity>().Find(match);
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al encontrar objeto de la base de datos en entityFramework", match.ToString(), ex, Context.ToString()));
                throw;
            }
        }

        public List<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            try
            {
                return Context.Set<TEntity>().Where(match).ToList();
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al encontrar lista de objeto de la base de datos en entityFramework", match.ToString(), ex, Context.ToString()));
                throw;
            }
        }

        public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            try
            {
                return await Context.Set<TEntity>().Where(match).ToListAsync();
            }
            catch (Exception ex)
            {
                await Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al encontrar lista de objeto de forma asincrona de la base de datos en entityFramework", match.ToString(), ex, Context.ToString()));

                throw ex;
            }
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            try
            {
                return await Context.Set<TEntity>().FindAsync(match);
            }
            catch (Exception ex)
            {
                await Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al encontrar objeto de la base de datos en entityFramework", match.ToString(), ex, Context.ToString()));
                throw;
            }
        }

        public List<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return Context.Set<TEntity>().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al encontrar lista de objetos de la base de datos en entityFramework", predicate.ToString(), ex, Context.ToString()));
                throw;
            }
        }

        public async Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await Context.Set<TEntity>().Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                await Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al encontrar lista de objetos asincrono de la base de datos en entityFramework", predicate.ToString(), ex, Context.ToString()));
                throw;
            }
        }

        public TEntity Get(int id)
        {
            try
            {
                return Context.Set<TEntity>().Find(id);
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al encontrar objeto por id de la base de datos en entityFramework", $"Id = {id}, Objeto = {this}", ex, Context.ToString()));
                throw;
            }
        }

        public List<TEntity> GetAll()
        {
            try
            {
                return Context.Set<TEntity>().ToList();
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al encontrar objeto de la base de datos en entityFramework", "", ex, Context.ToString()));
                throw;
            }
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            try
            {
                return await Context.Set<TEntity>().ToListAsync();
            }
            catch (Exception ex)
            {
                await Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al encontrar lista de registros de la base de datos en entityFramework", "", ex, Context.ToString()));
                throw;
            }
        }

        public List<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                IQueryable<TEntity> queryable = Context.Set<TEntity>();

                //foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
                //{
                //    queryable = queryable.Include(includeProperty);
                //}

                queryable = includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));

                return queryable.ToList();
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al encontrar objeto por propiedades en la base de datos en entityFramework", includeProperties.ToString(), ex, Context.ToString()));
                throw;
            }
        }

        public async Task<TEntity> GetAsync(int id)
        {
            try
            {
                return await Context.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                await Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al encontrar objeto asincrona de la base de datos en entityFramework", $"Id = {id}, Objeto = {this}", ex, Context.ToString()));
                throw;
            }
        }

        public TEntity Update(TEntity t, object key)
        {
            try
            {
                if (t == null)
                    return null;

                TEntity exist = Context.Set<TEntity>().Find(key);

                if (exist != null)
                {
                    Context.Entry(exist).CurrentValues.SetValues(t);
                }

                return t;
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al actualizar objeto asincrona de la base de datos en entityFramework", $"Id = {key}, Objeto = {t}", ex, Context.ToString()));
                throw;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity t, object key)
        {
            try
            {
                if (t == null)
                    return null;

                TEntity exist = await Context.Set<TEntity>().FindAsync(key);

                if (exist != null)
                {
                    Context.Entry(exist).CurrentValues.SetValues(t);
                }

                return t;
            }
            catch (Exception ex)
            {
                await Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al actualizar objeto asincrona de la base de datos en entityFramework", $"Id = {key}, Objeto = {this}", ex, Context.ToString()));
                throw;
            }
        }

        public List<TEntity> RemoveRange(List<TEntity> entity)
        {
            try
            {
                return Context.Set<TEntity>().RemoveRange(entity).ToList();
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al remover objeto asincrona de la base de datos en entityFramework", $"Objeto = {entity}", ex, Context.ToString()));
                throw;
            }
        }
    }
}