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
            TEntity Resultado = null;

            try
            {
                EntitySet.Add(agregar);
                _contexto.SaveChanges();
                Resultado = agregar;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Resultado;
        }

        public bool Eliminar(TEntity elminar)
        {
            bool Resultado = false;

            try
            {

                EntitySet.Attach(elminar);
                EntitySet.Remove(elminar);
                Resultado = _contexto.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Resultado;
        }

        public bool Actualizar(TEntity actualizar)
        {
            bool Resultado = false;

            try
            {
                EntitySet.Attach(actualizar);
                _contexto.Entry<TEntity>(actualizar).State = EntityState.Modified;
                Resultado = _contexto.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Resultado;
        }

        public TEntity Extraer(System.Linq.Expressions.Expression<Func<TEntity, bool>> criterio)
        {
            TEntity Resultado = null;

            try
            {
                Resultado = EntitySet.FirstOrDefault(criterio);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Resultado;
        }

        public List<TEntity> Filtro(System.Linq.Expressions.Expression<Func<TEntity, bool>> criterio)
        {
            List<TEntity> Resultado = null;

            try
            {
                Resultado = EntitySet.Where(criterio).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Resultado;
        }
        
        public List<TEntity> TablaCompleta()
        {
            List<TEntity> Resultado = null;

            try
            {
                Resultado = EntitySet.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Resultado;
        }

        public void Dispose()
        {
            if (_contexto != null)
                _contexto.Dispose();
        }
    }
}
