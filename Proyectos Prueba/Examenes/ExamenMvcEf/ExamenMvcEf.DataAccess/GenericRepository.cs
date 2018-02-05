namespace ExamenMvcEf.DataAccess
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public GenericRepository(DbContext context)
        {
            Context = context;
        }

        public TEntity Add(TEntity t)
        {
            return Context.Set<TEntity>().Add(t);
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> list)
        {
            return Context.Set<TEntity>().AddRange(list);
        }

        public int Count()
        {
            return Context.Set<TEntity>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await Context.Set<TEntity>().CountAsync();
        }

        public TEntity Remove(TEntity entity)
        {
            return Context.Set<TEntity>().Remove(entity);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return Context.Set<TEntity>().Find(match);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return Context.Set<TEntity>().Where(match).ToList();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await Context.Set<TEntity>().Where(match).ToListAsync();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await Context.Set<TEntity>().FindAsync(match);
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        public async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = Context.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return queryable.ToList();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public TEntity Update(TEntity t, object key)
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

        public async Task<TEntity> UpdateAsync(TEntity t, object key)
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

        public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entity)
        {
            return Context.Set<TEntity>().RemoveRange(entity);
        }
    }
}