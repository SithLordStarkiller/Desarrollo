using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CrudMySql.AccesoDatos
{
    interface IRepositorio<TEntity> : IDisposable where TEntity : class
    {
        TEntity Agregar(TEntity toAgregar);

        bool Eliminar(TEntity toElminar);

        bool Actualizar(TEntity toActualizar);

        TEntity Extraer(Expression<Func<TEntity, bool>> criterio);

        List<TEntity> Filtro(Expression<Func<TEntity, bool>> criterio);
    }
}
