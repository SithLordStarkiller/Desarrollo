namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System.Collections.Generic;

    public interface IRepository<TEntity> where TEntity : class
    {
        int Insertar(TEntity entity);
        TEntity ObtenerPorId(long Identificador);

        int CambiarEstatus(TEntity entity);

        int Actualizar(TEntity entity);

        IEnumerable<TEntity> Obtener(Paging paging);

        IEnumerable<TEntity> ObtenerPorCriterio(Paging paging, TEntity entity);
    }
}
