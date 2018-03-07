namespace Examen.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    internal interface IRepositorio<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> Add(TEntity agregar);

        Task<bool> Remove(TEntity eliminar);

        Task<bool> Update(TEntity actualizar);

        Task<TEntity> FindBy(Expression<Func<TEntity, bool>> criterio);

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> listaCriterio);

        Task<List<TEntity>> GetAll();
    }
}
