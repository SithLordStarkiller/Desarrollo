namespace ExamenMvc.Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity t);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> list);

        int Count();
        Task<int> CountAsync();

        TEntity Remove(TEntity entity);
        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entity);

        TEntity Find(Expression<Func<TEntity, bool>> match);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> match);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);

        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);

        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity Update(TEntity t, object key);
        Task<TEntity> UpdateAsync(TEntity t, object key);
    }
}
