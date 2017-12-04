namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using Interfaces.Genericos;
    using System.Collections.Generic;

    public interface IRepository<TEntity> where TEntity : class
    {
        int Insertar(TEntity entity);
        TEntity ObtenerPorId(long Identificador);

        int CambiarEstatus(TEntity entity);

        int Actualizar(TEntity entity);

        IEnumerable<TEntity> Obtener(IPaging paging);

        IEnumerable<TEntity> ObtenerPorCriterio(IPaging paging, TEntity entity);
    }

    public interface IRepository<TKey, TValue>
    {
        void Insertar(TValue valor, TKey identificador);
        TValue ObtenerPorId(TKey Identificador);
        void Actualizar(TKey identificador, TValue valor);
    }
}
