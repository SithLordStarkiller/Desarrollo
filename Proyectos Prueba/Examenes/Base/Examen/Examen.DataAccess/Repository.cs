namespace Examen.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        private readonly ExamenEntities _contexto;

        private DbSet<TEntity> EntitySet => _contexto.Set<TEntity>();

        public Repositorio()
        {
            _contexto = new ExamenEntities();
            _contexto.Configuration.ProxyCreationEnabled = false;
        }

        public async Task<TEntity> Add(TEntity agregar)
        {
            TEntity resultado;

            try
            {
                EntitySet.Add(agregar);
                await _contexto.SaveChangesAsync();
                resultado = agregar;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }

        public async Task<bool> Remove(TEntity eliminar)
        {
            bool resultado;

            try
            {
                EntitySet.Attach(eliminar);
                EntitySet.Remove(eliminar);
                resultado = await _contexto.SaveChangesAsync() > 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }

        public async Task<bool> Update(TEntity actualizar)
        {
            bool resultado;

            try
            {
                EntitySet.Attach(actualizar);
                _contexto.Entry(actualizar).State = EntityState.Modified;
                resultado = await _contexto.SaveChangesAsync() > 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }

        public async Task<TEntity> FindBy(Expression<Func<TEntity, bool>> criterio)
        {
            TEntity resultado;

            try
            {
                resultado = await EntitySet.FirstOrDefaultAsync(criterio);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> listaCriterio)
        {
            List<TEntity> resultado;

            try
            {
                resultado = await EntitySet.Where(listaCriterio).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }

        public async Task<List<TEntity>> GetAll()
        {
            List<TEntity> resultado;

            try
            {
                resultado = await EntitySet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }
        public void Dispose()
        {
            _contexto?.Dispose();
        }
    }
}
