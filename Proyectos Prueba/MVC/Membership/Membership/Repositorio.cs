namespace Membership
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        private readonly MembershipEntities _contexto;

        private DbSet<TEntity> EntitySet => _contexto.Set<TEntity>();

        public Repositorio()
        {
            _contexto = new MembershipEntities();
            _contexto.Configuration.ProxyCreationEnabled = false;
        }

        public async Task<TEntity> Insertar(TEntity agregar)
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

        public async Task<bool> Eliminar(TEntity eliminar)
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

        public async Task<bool> Actualizar(TEntity actualizar)
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

        public async Task<TEntity> Consulta(Expression<Func<TEntity, bool>> criterio)
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

        public async Task<List<TEntity>> ConsultaLista(Expression<Func<TEntity, bool>> listaCriterio)
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

        public async Task<List<TEntity>> ObtenerTabla()
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