namespace ExamenMvcEf.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity t);
        List<TEntity> AddRange(List<TEntity> list);

        int Count();
        Task<int> CountAsync();

        TEntity Remove(TEntity entity);
        List<TEntity> RemoveRange(List<TEntity> entity);

        TEntity Find(Expression<Func<TEntity, bool>> match);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);

        List<TEntity> FindAll(Expression<Func<TEntity, bool>> match);
        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);

        List<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);

        List<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync();
        List<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity Update(TEntity t, object key);
        Task<TEntity> UpdateAsync(TEntity t, object key);
    }
}
