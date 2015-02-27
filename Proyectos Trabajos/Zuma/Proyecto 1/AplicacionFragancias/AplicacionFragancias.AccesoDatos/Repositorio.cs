using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicacionFragancias.AccesoDatos;


namespace AplicacionFragancias.AccesoDatos
{
    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        private FraganciasEntities Contexto;

        private DbSet<TEntity> EntitySet
        {
            get { return Contexto.Set<TEntity>(); }
        }

        public Repositorio()
        {
            Contexto = new FraganciasEntities();
            Contexto.Configuration.ProxyCreationEnabled = false;
        }


        public TEntity Agregar(TEntity agregar)
        {
            TEntity Resultado = null;

            try
            {
                EntitySet.Add(agregar);
                Contexto.SaveChanges();
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
                Resultado = Contexto.SaveChanges() > 0;

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
                Contexto.Entry<TEntity>(actualizar).State = System.Data.Entity.EntityState.Modified;
                Resultado = Contexto.SaveChanges() > 0;

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
            if (Contexto != null)
                Contexto.Dispose();
        }
    }
}
