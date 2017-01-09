using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Broxel.DataAccess
{
    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        private readonly BroxelEntities _contexto;

        private DbSet<TEntity> EntitySet => _contexto.Set<TEntity>();

        public Repositorio()
        {
            _contexto = new BroxelEntities();
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

        //public async Task<List<TEntity>> InsertarMultiple(List<TEntity> listaAgregar)
        //{
        //    var diccionario = new List<TEntity>();

        //    try
        //    {
        //        foreach (var item in listaAgregar)
        //        {
        //            EntitySet.Add(item);
        //        }

        //        await _contexto.SaveChangesAsync();
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return diccionario;
        //}

        Task<bool> IRepositorio<TEntity>.Eliminar(TEntity eliminar)
        {
            throw new NotImplementedException();
        }

        //public Task<Dictionary<bool, TEntity>> EliminarMultiple(List<TEntity> listaEliminar)
        //{
        //    throw new NotImplementedException();
        //}

        Task<bool> IRepositorio<TEntity>.Actualizar(TEntity actualizar)
        {
            throw new NotImplementedException();
        }

        //public Task<Dictionary<bool, TEntity>> ActualizarMultiple(List<TEntity> listaActualizar)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<TEntity> Consulta(Expression<Func<TEntity, bool>> criterio)
        {
            throw new NotImplementedException();
        }

        //public Task<List<TEntity>> ConsultaLista(Expression<Func<TEntity, bool>> listaCriterio)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<List<TEntity>> ObtenerTabla()
        {
            throw new NotImplementedException();
        }

        //public bool Eliminar(TEntity elminar)
        //{
        //    bool Resultado = false;

        //    try
        //    {

        //        EntitySet.Attach(elminar);
        //        EntitySet.Remove(elminar);
        //        Resultado = _contexto.SaveChanges() > 0;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return Resultado;
        //}

        //public bool Actualizar(TEntity actualizar)
        //{
        //    bool Resultado = false;

        //    try
        //    {
        //        EntitySet.Attach(actualizar);
        //        _contexto.Entry<TEntity>(actualizar).State = System.Data.Entity.EntityState.Modified;
        //        Resultado = _contexto.SaveChanges() > 0;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return Resultado;
        //}

        //public TEntity Extraer(System.Linq.Expressions.Expression<Func<TEntity, bool>> criterio)
        //{
        //    TEntity Resultado = null;

        //    try
        //    {
        //        Resultado = EntitySet.FirstOrDefault(criterio);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return Resultado;
        //}

        //public List<TEntity> Filtro(System.Linq.Expressions.Expression<Func<TEntity, bool>> criterio)
        //{
        //    List<TEntity> Resultado = null;

        //    try
        //    {
        //        Resultado = EntitySet.Where(criterio).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return Resultado;
        //}

        //public List<TEntity> TablaCompleta()
        //{
        //    List<TEntity> Resultado = null;

        //    try
        //    {
        //        Resultado = EntitySet.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return Resultado;
        //}

        //public void Dispose()
        //{
        //    if (_contexto != null)
        //        _contexto.Dispose();
        //}
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

