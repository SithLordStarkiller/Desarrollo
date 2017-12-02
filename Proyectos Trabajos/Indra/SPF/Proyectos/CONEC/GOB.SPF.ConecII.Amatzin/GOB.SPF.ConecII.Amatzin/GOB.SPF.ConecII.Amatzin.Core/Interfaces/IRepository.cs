using GOB.SPF.ConecII.Amatzin.Entities;
namespace GOB.SPF.ConecII.Amatzin.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Result<TEntity> Insertar(TEntity entity);
        Result<TEntity> ObtenerPorId(object id);
        Result<TEntity> Actualizar(TEntity entity);
        Result<TEntity> ObtenerTodos();

        Result<TEntity> ConsultarHistoricoPorId(object id);
    }
}
