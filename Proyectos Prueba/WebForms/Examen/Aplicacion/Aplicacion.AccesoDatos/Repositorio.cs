using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Aplicacion.AccesoDatos
{
    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        private readonly ExamenEntities _contexto;

        private DbSet<TEntity> EntitySet
        {
            get { return _contexto.Set<TEntity>(); }
        }

        public Repositorio()
        {
            _contexto = new ExamenEntities();
            _contexto.Configuration.ProxyCreationEnabled = false;
        }


        public TEntity Agregar(TEntity agregar)
        {
            TEntity resultado;

            try
            {
                EntitySet.Add(agregar);
                _contexto.SaveChanges();
                resultado = agregar;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }

        public bool Eliminar(TEntity elminar)
        {
            bool resultado;

            try
            {

                EntitySet.Attach(elminar);
                EntitySet.Remove(elminar);
                resultado = _contexto.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }

        public bool Actualizar(TEntity actualizar)
        {
            bool resultado;

            try
            {
                EntitySet.Attach(actualizar);
                _contexto.Entry<TEntity>(actualizar).State = EntityState.Modified;
                resultado = _contexto.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        public TEntity Extraer(System.Linq.Expressions.Expression<Func<TEntity, bool>> criterio)
        {
            TEntity resultado;

            try
            {
                resultado = EntitySet.FirstOrDefault(criterio);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }

        public List<TEntity> Filtro(System.Linq.Expressions.Expression<Func<TEntity, bool>> criterio)
        {
            List<TEntity> resultado;

            try
            {
                resultado = EntitySet.Where(criterio).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }
        
        public List<TEntity> TablaCompleta()
        {
            List<TEntity> resultado;

            try
            {
                resultado = EntitySet.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }

        public void Dispose()
        {
            if (_contexto != null)
                _contexto.Dispose();
        }
    }
}
