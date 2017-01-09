namespace Broxel.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    internal interface IRepositorio<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> Insertar(TEntity agregar);

        //Task<List<TEntity>> InsertarMultiple(List<TEntity> listaAgregar);

        Task<bool> Eliminar(TEntity eliminar);

        //Task<Dictionary<bool,TEntity>> EliminarMultiple(List<TEntity> listaEliminar);

        Task<bool> Actualizar(TEntity actualizar);

        //Task<Dictionary<bool, TEntity>> ActualizarMultiple(List<TEntity> listaActualizar);

        Task<TEntity> Consulta(Expression<Func<TEntity, bool>> criterio);

        //Task<List<TEntity>> ConsultaLista(Expression<Func<TEntity, bool>> listaCriterio);

        Task<List<TEntity>> ObtenerTabla();
    }
}
